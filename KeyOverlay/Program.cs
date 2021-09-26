using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using KeyOverlay.Input;
using KeyOverlay.Json;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SilkyNvg;
using SilkyNvg.Rendering.OpenGL;
using SilkyNvg.Text;

namespace KeyOverlay
{
    class Program
    {
        private static GL _gl = null!;
        private static Nvg _nvg = null!;
        private static Stopwatch _timer = null!;

        private static PerformanceGraph _frameGraph = null!;
        private static PerformanceGraph _cpuGraph = null!;
        private static IWindow _window = null!;
        private static readonly JsonSerializerOptions JsonSettings = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters =
            {
                new ColourConverter(),
                new OverlayKeyArrayConverter()
            }
        };

        static void Main()
        {
            var configFileExists = File.Exists("config1.json");
            _settings = configFileExists
                ? JsonSerializer.Deserialize<Settings>(File.ReadAllText("config1.json"), JsonSettings)! 
                : new Settings();

            if (!configFileExists)
            {
                File.WriteAllText("config1.json", JsonSerializer.Serialize(_settings, JsonSettings));
            }
            
            _frameGraph = new PerformanceGraph(PerformanceGraph.GraphRenderStyle.Fps, "Frame Time");
            _cpuGraph = new PerformanceGraph(PerformanceGraph.GraphRenderStyle.Ms, "CPU Time");

            var windowOptions = WindowOptions.Default;
            windowOptions.ShouldSwapAutomatically = true;
            windowOptions.Title = "KeyOverlay";
            windowOptions.VSync = false;
            windowOptions.PreferredDepthBufferBits = 24;
            windowOptions.PreferredStencilBufferBits = 8;
            windowOptions.TransparentFramebuffer = _settings.TransparentWindow;
            WindowExt.PrioritizeGlobal();

            _window = Window.Create(windowOptions);
            _window.Load += WindowOnLoad;
            _window.Render += WindowOnRender;
            _window.Closing += WindowOnClosing;
            _window.Run();

            _window.Dispose();
        }

        private static bool alreadySetPos = false;
        private static void ProcessNewSettings(string? configFileName)
        {
            var onLoad = string.IsNullOrWhiteSpace(configFileName);
            if (!onLoad)
            {
                _settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText(configFileName), JsonSettings)!;
            }
            
            //This will tell us that we wasn't given an fps, default to the monitor if possible
            if (!_settings.MaxFps.HasValue)
            {
                if (_window.Monitor is { VideoMode: { RefreshRate: { } } })
                {
                    if (!alreadySetPos)
                    {
                        _window.Position = _window.Monitor.Bounds.Max - _window.Size;
                        alreadySetPos = true;
                    }
                    _window.FramesPerSecond = _window.Monitor.VideoMode.RefreshRate.Value;
                }
            }
            else
            {
                _window.FramesPerSecond = _settings.MaxFps.Value;
            }
            
            _window.Size = new Vector2D<int>(_settings.Width, _settings.Height);
            if (!onLoad)
            {
                _ui.UpdateSettings(_settings);
            }
        }

        private static void WindowOnClosing()
        {
            _timer.Stop();
            _ui.Dispose();
            _nvg.Dispose();
            _gl.Dispose();

            Console.WriteLine("Average Frame Time: " + _frameGraph.GraphAverage * 1000.0f + " ms");
            Console.WriteLine("        CPU Time:   " + _cpuGraph.GraphAverage * 1000.0f + " ms");
        }

        private static UI _ui = null!;
        private static Settings _settings = null!;
        private static void WindowOnRender(double delta)
        {
            var startTime = _timer.Elapsed.TotalSeconds;
            if (!_window.IsVisible)
            {
                _timer.Restart();
                return;
            }
            foreach (var keyboard in _inputs.Keyboards)
            {
                CheckInput(keyboard);
            }

            var t = (float)delta;
            
            Vector2D<float> winSize = _window.Size.As<float>();
            Vector2D<float> fbSize = _window.FramebufferSize.As<float>();

            float pxRatio = fbSize.X / winSize.X;
            float pyRatio = fbSize.Y / winSize.Y;
            
            _gl.Viewport(0, 0, (uint)winSize.X, (uint)winSize.Y);
            _gl.ClearColor(_settings.BackgroundColour);
            _gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            _nvg.BeginFrame(winSize.As<float>(), pxRatio);

            _ui.Render(winSize.X, winSize.Y, t, pxRatio, pyRatio);
            if (_settings.DebugStats)
            {
                _frameGraph.Render(5.0f, 5.0f, _nvg);
                _cpuGraph.Render(5.0f + (winSize.X >= 410 ? 200.0f : 0) + 5.0f, 5.0f + (winSize.X < 410 ? 38.0f : 0), _nvg);
            }

            _nvg.EndFrame();

            var cpuTime = (float)(_timer.Elapsed.TotalSeconds - startTime);
            _timer.Restart();
            
            _frameGraph.Update(t);
            _cpuGraph.Update(cpuTime);
        }

        private static void CheckInput(IKeyboard keyboard)
        {
            if (!keyboard.IsKeyPressed(Key.ShiftLeft) && !keyboard.IsKeyPressed(Key.ShiftRight))
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                if (SwapSettingsOnKey(Key.Number0 + i, keyboard, i))
                {
                    i = 10;
                }
            }
        }

        public static bool SwapSettingsOnKey(Key key, IKeyboard keyboard, int index)
        {
            if (keyboard.IsKeyPressed(key))
            {
                var filename = $"config{index}.json";
                if (File.Exists(filename))
                {
                    ProcessNewSettings(filename);
                    return true;
                }
            }

            return false;
        }

        private static IInputContext _inputs = null!;
        private static void WindowOnLoad()
        {
            _inputs = _window.CreateInput();
            _gl = _window.CreateOpenGL();
            OpenGLRenderer nvgRenderer = new(CreateFlags.StencilStrokes | CreateFlags.Debug, _gl);
            _nvg = Nvg.Create(nvgRenderer);
            _ui = new UI(_nvg, _settings, _inputs);

            AddFont("icons", "./fonts/entypo.ttf");
            AddFont("sans", "./fonts/Roboto-Regular.ttf");
            AddFont("sans-bold", "./fonts/Roboto-Bold.ttf");
            AddFont("emoji", "./fonts/NotoEmoji-Regular.ttf");
            ProcessNewSettings(null);
            
            _timer = Stopwatch.StartNew();
        }

        private static void AddFont(string name, string filepath)
        {
            var wasSuccessful = _nvg.CreateFont(name, filepath);
            if (wasSuccessful == -1)
            {
                Console.Error.WriteLine("Could not add {0}.", name);
                Environment.Exit(-1);
            }
        }
    }
}
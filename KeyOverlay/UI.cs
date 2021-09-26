using System;
using System.Linq;
using Silk.NET.Input;
using Silk.NET.Maths;
using SilkyNvg;
using SilkyNvg.Graphics;
using SilkyNvg.Images;
using SilkyNvg.Paths;
using SilkyNvg.Text;

namespace KeyOverlay
{
    public class UI : IDisposable
    {
        private readonly Nvg _nvg;
        private readonly IInputContext _inputContext;

        private Settings _settings;
        private int? _image;
        private int? _imageWidth;
        private int? _imageHeight;
        public UI(Nvg nvg, Settings settings, IInputContext inputContext)
        {
            _nvg = nvg;
            _settings = settings;
            _inputContext = inputContext;
            if (string.IsNullOrWhiteSpace(settings.BackgroundLocation))
            {
                return;
            }
            
            UpdateImage(settings.BackgroundLocation);
        }

        public void UpdateSettings(Settings newSettings)
        {
            _settings = newSettings;
            UpdateImage(newSettings.BackgroundLocation);
        }
        
        public void UpdateImage(string? location)
        {
            if (_image.HasValue)
            {
                _nvg.DeleteImage(_image.Value);
            }
            if (string.IsNullOrWhiteSpace(location))
            {
                _image = null;
                return;
            }
            
            _image = _nvg.CreateImage(location, 0);
            //TODO: Get image width/height
            _imageWidth = 1378;
            _imageHeight = 777;
            if (_image == 0)
            {
                Console.Error.WriteLine("Wasn't able to create image");
                Environment.Exit(-1);
            }
        }
        
        public void Render(float winWidth, float winHeight, float deltaTime, float ratioX, float ratioY)
        {
            if (winHeight == 0 || winWidth == 0)
            {
                return;
            }

            if (_image.HasValue)
            {
                _nvg.BeginPath();
                _nvg.Rect(0, 0, winWidth, winHeight);
                //TODO: Center image
                _nvg.FillPaint(Paint.ImagePattern(0, 0, _imageWidth.Value, _imageHeight.Value, 0, _image.Value, 1));
                _nvg.Fill();
            }
            
            //How much we will be taking up in the width
            var keySize = _settings.KeySize + (_settings.OutlineThickness * 2);

            //How much we will take on the width
            var neededWidth = keySize *_settings.Keys.Length;
            neededWidth += _settings.Spacing * (_settings.Keys.Length - 1);

            var moveDist = deltaTime * _settings.BarSpeed;
            
            var y = winHeight - keySize + 2;
            float x = _settings.Alignment switch
            {
                Alignment.Middle => (winWidth - neededWidth) / 2,
                Alignment.Right => winWidth - neededWidth,
                _ => 0f
            };
            foreach (var key in _settings.Keys)
            {
                if (_inputContext.Keyboards[0].IsKeyPressed(key.Key))
                {
                    if (!key.Hold)
                    {
                        key.Counter++;
                        key.Bars.Add(
                            new Rectangle<float>(
                                new Vector2D<float>(x, y), 
                                new Vector2D<float>(keySize, 0)));
                    }
                    
                    key.Hold = true;
                    var rec = key.Bars[^1];
                    rec.Size = new Vector2D<float>(rec.Size.X, rec.Size.Y += moveDist);
                    key.Bars[^1] = rec;
                }
                else
                {
                    key.Hold = false;
                }

                if (key.Bars.Any() 
                    && key.Bars.First().Origin.Y + key.Bars.First().Size.Y < 0)
                {
                    key.Bars.RemoveAt(0);
                }

                DrawKey(key, keySize, _settings.OutlineThickness, x, y, winHeight, moveDist);
                x += _settings.Spacing + keySize;
            }

            if (_settings.Fading)
            {
                var h = winHeight * 0.25F;
                var w = winWidth / 2;
                var grad = Paint.LinearGradient(w, 0, w, h, _image.HasValue ? Colour.Black : _settings.BackgroundColour, Transparent);
                
                _nvg.BeginPath();
                _nvg.Rect(0, 0, winWidth, h);
                _nvg.FillPaint(grad);
                _nvg.Fill();
            }
        }
        private static readonly Colour Transparent = new Colour(0, 0, 0, 0);
        
        private void DrawBars(OverlayKey key, float extra)
        {
            foreach (var bar in key.Bars)
            {
                _nvg.BeginPath();
                _nvg.Rect(new Vector2D<float>(bar.Origin.X, bar.Origin.Y - extra), bar.Size);
                _nvg.FillColour(_settings.BarColour);
                _nvg.Fill();
            }
        }
        
        private void DrawKey(OverlayKey key, float size, float border, float x, float y, float winHeight, float moveDist)
        {
            var extraMovement = 0F;
            x += border / 2;
            if (_settings.ShowKeyCount)
            {
                _nvg.FontSize(20);
                _nvg.FontFace("sans");
                _nvg.FillColour(_settings.FontColour);
                
                var counter = key.Counter.ToString();
                _nvg.TextBounds(0, 0, counter, out var countBounds);
                var xCountPos = x - (border / 2) + ((size - countBounds.Size.X) / 2);
                var yCountPos = winHeight - 10;
                _nvg.Text(xCountPos, yCountPos, counter);

                extraMovement = countBounds.Size.Y + 10;
                y -= extraMovement;
            }
            
            _nvg.BeginPath();
            _nvg.Rect(x, y, size - border, size - border);
            _nvg.FillColour(key.Hold ? _settings.BarColour : _settings.KeyBackgroundColour);
            _nvg.Fill();
            
            _nvg.StrokeWidth(border);
            _nvg.StrokeColour(_settings.BorderColour);
            _nvg.Stroke();

            _nvg.FontSize(30);
            _nvg.FontFace("sans");
            _nvg.FillColour(_settings.FontColour);

            _nvg.TextBounds(0, 0, key.KeyLetter, out var bounds);
            var xPos = x - (border / 2) + ((size - bounds.Size.X) / 2);
            var yPos = y - (border) + bounds.Size.Y + ((size - bounds.Size.Y) / 2);
            _nvg.Text(xPos, yPos, key.KeyLetter);
            
            for (int i = 0; i < key.Bars.Count; i++)
            {
                var rec = key.Bars[i];
                rec.Origin = new Vector2D<float>(rec.Origin.X, rec.Origin.Y -= moveDist);
                key.Bars[i] = rec;
            }
            DrawBars(key, extraMovement);
        }
        
        public void Dispose()
        {
            if (_image.HasValue)
            {
                _nvg.DeleteImage(_image.Value);
            }
        }
    }
}
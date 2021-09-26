using Silk.NET.Input;
using SilkyNvg;

namespace KeyOverlay
{
    public class Settings
    {
        //What keys to listen for
        public OverlayKey[] Keys { get; init; } = { new OverlayKey(Key.Z), new OverlayKey(Key.X) };
        
        //Max FPS
        public int? MaxFps { get; init; }
        
        //Height of Window
        public int Height { get; init; } = 700;

        //Width of Window
        public int Width { get; init; } = 240;
        
        //If we should show how many times we have pressed a key
        public bool ShowKeyCount { get; init; }
        
        //How big the key UI is
        public float KeySize { get; init; } = 70;
        
        //How fast the bar should move up
        public float BarSpeed { get; init; } = 600;
        
        //The spacing between the keys
        public float Spacing { get; init; } = 25;
        
        //How t h i c k the box should be
        public float OutlineThickness { get; init; } = 5;
        
        //If we should have some kind of fading at the top
        public bool Fading { get; init; } = true;
        
        //The background colour
        public Colour BackgroundColour { get; init; } = Colour.Black;
        
        //The background for the key
        public Colour KeyBackgroundColour { get; init; } = new Colour(Colour.Black, 50);

        //The colour for the border
        public Colour BorderColour { get; init; } = Colour.White;

        //The bar colour
        public Colour BarColour { get; init; } = new Colour(Colour.White, 100);
        
        //The colour for the font in the key
        public Colour FontColour { get; init; } = Colour.White;
        
        //Where the background is if we want a background
        public string? BackgroundLocation { get; init; }
        
        //If we should show the window transparent
        public bool TransparentWindow { get; init; }

        //If we should show debug stats
        public bool DebugStats { get; init; }

        public Alignment Alignment { get; init; } = Alignment.Middle;
    }

    public enum Alignment
    {
        Left,
        Middle,
        Right
    }
}
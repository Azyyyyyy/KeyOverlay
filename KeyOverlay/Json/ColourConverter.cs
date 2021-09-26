using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using SilkyNvg;

namespace KeyOverlay.Json
{
    public class ColourConverter : JsonConverter<Colour>
    {
        //Input/Output should be r,g,b,a (.e.g. 255,0,0,255)
        public override Colour Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString().AsSpan();
            
            var redIndex = s.IndexOf(',');
            var red = byte.Parse(s[..redIndex]);
            s = s[(redIndex + 1)..];

            var greenIndex = s.IndexOf(',');
            var green = byte.Parse(s[..greenIndex]);
            s = s[(greenIndex + 1)..];
            
            var blueIndex = s.IndexOf(',');
            var blue = byte.Parse(s[..blueIndex]);
            s = s[(blueIndex + 1)..];
            
            var alpha = byte.Parse(s);
            return new Colour(red, green, blue, alpha);
        }

        public override void Write(Utf8JsonWriter writer, Colour value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{255 * value.R:0},{255 * value.G:0},{255 * value.B:0},{255 * value.A:0}");
        }
    }
}
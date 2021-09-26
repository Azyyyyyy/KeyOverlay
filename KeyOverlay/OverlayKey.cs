using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using KeyOverlay.Input;
using Silk.NET.Input;
using Silk.NET.Maths;

namespace KeyOverlay
{
    public class OverlayKey
    {
        [JsonConstructor]
        public OverlayKey(Key key)
        {
            Key = key;
            KeyLetter = key.GetString();
        }
        
        public Key Key { get; init; }
        
        [JsonIgnore]
        public int Counter { get; set; }
        
        [JsonIgnore]
        public bool Hold { get; set; }
        
        [JsonIgnore]
        public string KeyLetter { get; init; }

        //Most players can tap at 20/s and hopefully most bars will only stay around for 5 seconds max
        [JsonIgnore]
        public List<Rectangle<float>> Bars { get; } = new List<Rectangle<float>>(100);
    }
}
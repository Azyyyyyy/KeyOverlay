﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using KeyOverlay.Input;
using Silk.NET.Input;

namespace KeyOverlay.Json
{
    public class OverlayKeyArrayConverter : JsonConverter<OverlayKey[]>
    {
        public override OverlayKey[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new Exception("We should start on the array!");
            }
            reader.Read();
            
            var keys = new List<OverlayKey>(2);
            while (reader.TokenType != JsonTokenType.EndArray)
            {
                var s = reader.GetString();
                if (string.IsNullOrWhiteSpace(s))
                {
                    if (options.IgnoreNullValues)
                    {
                        continue;
                    }

                    throw new Exception("Key was null");
                }
                
                keys.Add(new OverlayKey(s.GetKey()));
                if (!reader.Read())
                {
                    throw new Exception("Failed to read next value");
                }
            }

            return keys.ToArray();
        }

        public override void Write(Utf8JsonWriter writer, OverlayKey[] keys, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var key in keys)
            {
                writer.WriteStringValue(key.Key.GetString());
            }
            writer.WriteEndArray();
        }
    }
}
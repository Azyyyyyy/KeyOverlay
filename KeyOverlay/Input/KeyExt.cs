﻿using System;
using Silk.NET.Input;

namespace KeyOverlay.Input
{
    //TODO: Finish this
    public static class KeyExt
    {
        public static string GetString(this Key key)
        {
            switch (key)
            {
                case Key.Unknown:
                    return "NA";
                case Key.Space:
                    return "Sp";
                case Key.Apostrophe:
                    return "'";
                case Key.Comma:
                    return ",";
                case Key.Minus:
                    return "-";
                case Key.Period:
                    return ">";
                case Key.Slash:
                    return "/";
                case Key.Semicolon:
                    return "\"";
                case Key.Equal:
                    return "=";
                case Key.LeftBracket:
                    break;
                case Key.BackSlash:
                    break;
                case Key.RightBracket:
                    break;
                case Key.GraveAccent:
                    break;
                case Key.World1:
                    break;
                case Key.World2:
                    break;
                case Key.Escape:
                    break;
                case Key.Enter:
                    break;
                case Key.Tab:
                    break;
                case Key.Backspace:
                    break;
                case Key.Insert:
                    break;
                case Key.Delete:
                    break;
                case Key.PageUp:
                    break;
                case Key.PageDown:
                    break;
                case Key.PrintScreen:
                    break;
                case Key.Home:
                case Key.End:
                case Key.Pause:
                case Key.CapsLock:
                case Key.ScrollLock:
                case Key.NumLock:
                case Key.Right:
                case Key.Left:
                case Key.Down:
                case Key.Up:
                case Key.Number0:
                case Key.Number1:
                case Key.Number2:
                case Key.Number3:
                case Key.Number4:
                case Key.Number5:
                case Key.Number6:
                case Key.Number7:
                case Key.Number8:
                case Key.Number9:
                case Key.A:
                case Key.B:
                case Key.C:
                case Key.D:
                case Key.E:
                case Key.F:
                case Key.G:
                case Key.H:
                case Key.I:
                case Key.J:
                case Key.K:
                case Key.L:
                case Key.M:
                case Key.N:
                case Key.O:
                case Key.P:
                case Key.Q:
                case Key.R:
                case Key.S:
                case Key.T:
                case Key.U:
                case Key.V:
                case Key.W:
                case Key.X:
                case Key.Y:
                case Key.Z:
                case Key.F1:
                case Key.F2:
                case Key.F3:
                case Key.F4:
                case Key.F5:
                case Key.F6:
                case Key.F7:
                case Key.F8:
                case Key.F9:
                case Key.F10:
                case Key.F11:
                case Key.F12:
                case Key.F13:
                case Key.F14:
                case Key.F15:
                case Key.F16:
                case Key.F17:
                case Key.F18:
                case Key.F19:
                case Key.F20:
                case Key.F21:
                case Key.F22:
                case Key.F23:
                case Key.F24:
                case Key.F25:
                    return Enum.GetName(key)!;
                case Key.Keypad0:
                    return "K0";
                case Key.Keypad1:
                    return "K1";
                case Key.Keypad2:
                    return "K2";
                case Key.Keypad3:
                    return "K3";
                case Key.Keypad4:
                    return "K4";
                case Key.Keypad5:
                    return "K5";
                case Key.Keypad6:
                    return "K6";
                case Key.Keypad7:
                    return "K7";
                case Key.Keypad8:
                    return "K9";
                case Key.Keypad9:
                    return "K9";
                case Key.KeypadDecimal:
                    break;
                case Key.KeypadDivide:
                    break;
                case Key.KeypadMultiply:
                    break;
                case Key.KeypadSubtract:
                    break;
                case Key.KeypadAdd:
                    break;
                case Key.KeypadEnter:
                    break;
                case Key.KeypadEqual:
                    break;
                case Key.ShiftLeft:
                    break;
                case Key.ControlLeft:
                    break;
                case Key.AltLeft:
                    break;
                case Key.SuperLeft:
                    break;
                case Key.ShiftRight:
                    break;
                case Key.ControlRight:
                    break;
                case Key.AltRight:
                    break;
                case Key.SuperRight:
                    break;
                case Key.Menu:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
            throw new ArgumentOutOfRangeException(nameof(key), key, null);
        }
        
        public static Key GetKey(this string s)
        {
            switch (s)
            {
                case "NA":
                    return Key.Unknown;
                case "Sp":
                    return Key.Space;
                case "'":
                    return Key.Apostrophe;
                case ",":
                    return Key.Comma;
                case "-":
                    return Key.Minus;
                case ">":
                    return Key.Period;
                case "/":
                    return Key.Slash;
                case "\"":
                    return Key.Semicolon;
                case "=":
                    return Key.Equal;
                /*case Key.LeftBracket:
                    break;
                case Key.BackSlash:
                    break;
                case Key.RightBracket:
                    break;
                case Key.GraveAccent:
                    break;
                case Key.World1:
                    break;
                case Key.World2:
                    break;
                case Key.Escape:
                    break;
                case Key.Enter:
                    break;
                case Key.Tab:
                    break;
                case Key.Backspace:
                    break;
                case Key.Insert:
                    break;
                case Key.Delete:
                    break;
                case Key.PageUp:
                    break;
                case Key.PageDown:
                    break;
                case Key.PrintScreen:
                    break;
                case Key.Keypad0:
                    return "K0";
                case Key.Keypad1:
                    return "K1";
                case Key.Keypad2:
                    return "K2";
                case Key.Keypad3:
                    return "K3";
                case Key.Keypad4:
                    return "K4";
                case Key.Keypad5:
                    return "K5";
                case Key.Keypad6:
                    return "K6";
                case Key.Keypad7:
                    return "K7";
                case Key.Keypad8:
                    return "K9";
                case Key.Keypad9:
                    return "K9";
                case Key.KeypadDecimal:
                    break;
                case Key.KeypadDivide:
                    break;
                case Key.KeypadMultiply:
                    break;
                case Key.KeypadSubtract:
                    break;
                case Key.KeypadAdd:
                    break;
                case Key.KeypadEnter:
                    break;
                case Key.KeypadEqual:
                    break;
                case Key.ShiftLeft:
                    break;
                case Key.ControlLeft:
                    break;
                case Key.AltLeft:
                    break;
                case Key.SuperLeft:
                    break;
                case Key.ShiftRight:
                    break;
                case Key.ControlRight:
                    break;
                case Key.AltRight:
                    break;
                case Key.SuperRight:
                    break;
                case Key.Menu:
                    break;*/
                default:
                    return Enum.Parse<Key>(s);
            }
        }
    }
}
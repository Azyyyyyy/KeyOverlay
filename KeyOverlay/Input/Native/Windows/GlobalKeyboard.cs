using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Silk.NET.Input;

namespace KeyOverlay.Input.Native.Windows
{
    public class GlobalKeyboard : IKeyboard
    {
        public string Name => "Global Keyboard";
        public int Index => 0;
        public bool IsConnected => true;
        public bool IsKeyPressed(Key key)
        {
            //TODO: See why this get's buffered?
            var vKey = KeyToVKey(key);
            if (vKey == -1)
            {
                return false;
            }
            
            var state = GetAsyncKeyState(vKey);
            return state != 0;
        }

        public void BeginInput()
        { }

        public void EndInput()
        { }

        public event Action<IKeyboard, Key, int>? KeyDown;
        public event Action<IKeyboard, Key, int>? KeyUp;
        public event Action<IKeyboard, char>? KeyChar;

        //TODO: Maybe hook up events?
        //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmessage ?
        public IReadOnlyList<Key> SupportedKeys { get; } = new[]
        {
            Key.Space,
            Key.Slash,
            Key.Number0,
            Key.Number1,
            Key.Number2,
            Key.Number3,
            Key.Number4,
            Key.Number5,
            Key.Number6,
            Key.Number7,
            Key.Number8,
            Key.Number9,
            Key.A,
            Key.B,
            Key.C,
            Key.D,
            Key.E,
            Key.F,
            Key.G,
            Key.H,
            Key.I,
            Key.J,
            Key.K,
            Key.L,
            Key.M,
            Key.N,
            Key.O,
            Key.P,
            Key.Q,
            Key.R,
            Key.S,
            Key.T,
            Key.U,
            Key.V,
            Key.W,
            Key.X,
            Key.Y,
            Key.Z,
            Key.Escape,
            Key.Enter,
            Key.Tab,
            Key.Backspace,
            Key.Insert,
            Key.Delete,
            Key.Right,
            Key.Left,
            Key.Down,
            Key.Up,
            Key.PageUp,
            Key.PageDown,
            Key.Home,
            Key.End,
            Key.CapsLock,
            Key.ScrollLock,
            Key.NumLock,
            Key.PrintScreen,
            Key.Pause,
            Key.F1,
            Key.F2,
            Key.F3,
            Key.F4,
            Key.F5,
            Key.F6,
            Key.F7,
            Key.F8,
            Key.F9,
            Key.F10,
            Key.F11,
            Key.F12,
            Key.F13,
            Key.F14,
            Key.F15,
            Key.F16,
            Key.F17,
            Key.F18,
            Key.F19,
            Key.F20,
            Key.F21,
            Key.F22,
            Key.F23,
            Key.F24,
            Key.Keypad0,
            Key.Keypad1,
            Key.Keypad2,
            Key.Keypad3,
            Key.Keypad4,
            Key.Keypad5,
            Key.Keypad6,
            Key.Keypad7,
            Key.Keypad8,
            Key.Keypad9,
            Key.KeypadDecimal,
            Key.KeypadDivide,
            Key.KeypadMultiply,
            Key.KeypadSubtract,
            Key.KeypadAdd,
            Key.KeypadEnter,
            Key.ShiftLeft,
            Key.ControlLeft,
            Key.SuperLeft,
            Key.ShiftRight,
            Key.ControlRight,
            Key.SuperRight,
            Key.Comma,
            Key.Minus,
            Key.Period,
        };

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetAsyncKeyState(int nVirtKey);
        
        //Started at looking at https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-tvtt/261ddfb0-ce10-4380-9b7a-4b50f482b8ec but
        //found https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        private int KeyToVKey(Key key)
        {
            //Clear: 0x000C
            //Control: 0x11
            //Alt: 0x12
            //Select: 0x0029
            //Print: 0x002A
            //Execute: 0x002B
            //Help: 0x002F
            //Apps: 0x005D
            //Sleep: 0x005F
            //Separator: 0x006C
            //Left Menu: 0x00A4
            //Right Menu: 0x00A5
            return key switch
            {
                Key.Unknown => -1,
                Key.Space => 0x0020,
                Key.Apostrophe => -1,
                Key.Comma => 0xBC,
                Key.Minus => 0xBD,
                Key.Period => 0xBE,
                Key.Slash => 0x10,
                Key.Number0 => 0x0030,
                Key.Number1 => 0x0031,
                Key.Number2 => 0x0032,
                Key.Number3 => 0x0033,
                Key.Number4 => 0x0034,
                Key.Number5 => 0x0035,
                Key.Number6 => 0x0036,
                Key.Number7 => 0x0037,
                Key.Number8 => 0x0038,
                Key.Number9 => 0x0039,
                Key.Semicolon => -1,
                Key.Equal => -1,
                Key.A => 0x0041,
                Key.B => 0x0042,
                Key.C => 0x0043,
                Key.D => 0x0044,
                Key.E => 0x0045,
                Key.F => 0x0046,
                Key.G => 0x0047,
                Key.H => 0x0048,
                Key.I => 0x0049,
                Key.J => 0x004A,
                Key.K => 0x004B,
                Key.L => 0x004C,
                Key.M => 0x004D,
                Key.N => 0x004E,
                Key.O => 0x004F,
                Key.P => 0x0050,
                Key.Q => 0x0051,
                Key.R => 0x0052,
                Key.S => 0x0053,
                Key.T => 0x0054,
                Key.U => 0x0055,
                Key.V => 0x0056,
                Key.W => 0x0057,
                Key.X => 0x0058,
                Key.Y => 0x0059,
                Key.Z => 0x005A,
                Key.LeftBracket => -1,
                Key.BackSlash => -1,
                Key.RightBracket => -1,
                Key.GraveAccent => -1,
                Key.World1 => -1,
                Key.World2 => -1,
                Key.Escape => 0x001B,
                Key.Enter => 0x0D,
                Key.Tab => 0x0009,
                Key.Backspace => 0x0008,
                Key.Insert => 0x002D,
                Key.Delete => 0x002E,
                Key.Right => 0x0027,
                Key.Left => 0x0025,
                Key.Down => 0x0028,
                Key.Up => 0x0026,
                Key.PageUp => 0x0021,
                Key.PageDown => 0x0022,
                Key.Home => 0x0024,
                Key.End => 0x0023,
                Key.CapsLock => 0x0014,
                Key.ScrollLock => 0x0091,
                Key.NumLock => 0x0090,
                Key.PrintScreen => 0x002C,
                Key.Pause => 0x0013,
                Key.F1 => 0x0070,
                Key.F2 => 0x0071,
                Key.F3 => 0x0072,
                Key.F4 => 0x0073,
                Key.F5 => 0x0074,
                Key.F6 => 0x0075,
                Key.F7 => 0x0076,
                Key.F8 => 0x0077,
                Key.F9 => 0x0078,
                Key.F10 => 0x0079,
                Key.F11 => 0x007A,
                Key.F12 => 0x007B,
                Key.F13 => 0x007C,
                Key.F14 => 0x007D,
                Key.F15 => 0x007E,
                Key.F16 => 0x007F,
                Key.F17 => 0x0080,
                Key.F18 => 0x0081,
                Key.F19 => 0x0082,
                Key.F20 => 0x0083,
                Key.F21 => 0x0084,
                Key.F22 => 0x0085,
                Key.F23 => 0x0086,
                Key.F24 => 0x0087,
                Key.F25 => -1,
                Key.Keypad0 => 0x0060,
                Key.Keypad1 => 0x0061,
                Key.Keypad2 => 0x0062,
                Key.Keypad3 => 0x0063,
                Key.Keypad4 => 0x0064,
                Key.Keypad5 => 0x0065,
                Key.Keypad6 => 0x0066,
                Key.Keypad7 => 0x0067,
                Key.Keypad8 => 0x0068,
                Key.Keypad9 => 0x0069,
                Key.KeypadDecimal => 0x006E,
                Key.KeypadDivide => 0x006F,
                Key.KeypadMultiply => 0x006A,
                Key.KeypadSubtract => 0x006D,
                Key.KeypadAdd => 0x006B,
                Key.KeypadEnter => 0x0D,
                Key.KeypadEqual => -1,
                Key.ShiftLeft => 0x00A0,
                Key.ControlLeft => 0x00A2,
                Key.AltLeft => -1,
                Key.SuperLeft => 0x005B,
                Key.ShiftRight => 0x00A1,
                Key.ControlRight => 0x00A3,
                Key.AltRight => -1,
                Key.SuperRight => 0x005C,
                Key.Menu => -1,
                _ => -1
            };
        }
    }
}
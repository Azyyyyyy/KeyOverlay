using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using KeyOverlay.Input.Native.Windows;
using Silk.NET.Input;

namespace KeyOverlay.Input
{
    public class GlobalInputContext : IInputContext
    {
        public GlobalInputContext()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Keyboards = new[] { new GlobalKeyboard() };
            }
            //TODO: Add other OS's?
        }
        
        public void Dispose()
        { }

        public IReadOnlyList<IKeyboard> Keyboards { get; } = ArraySegment<IKeyboard>.Empty;
        public IReadOnlyList<IMouse> Mice { get; } = ArraySegment<IMouse>.Empty;

        //We don't deal with these
        public nint Handle { get; } = nint.MinValue;
        public IReadOnlyList<IInputDevice> OtherDevices { get; } = ArraySegment<IInputDevice>.Empty;
        public IReadOnlyList<IGamepad> Gamepads { get; } = ArraySegment<IGamepad>.Empty;
        public IReadOnlyList<IJoystick> Joysticks { get; } = ArraySegment<IJoystick>.Empty;
        public event Action<IInputDevice, bool>? ConnectionChanged;
    }
}
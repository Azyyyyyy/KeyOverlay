﻿using Silk.NET.Input;

namespace KeyOverlay.Input
{
    public class WindowExt
    {
        public static void PrioritizeGlobal()
        {
            InputWindowExtensions.Add(new GlobalInput());
        }
    }
}
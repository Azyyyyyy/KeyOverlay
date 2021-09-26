﻿using Silk.NET.OpenGL;
using SilkyNvg;

namespace KeyOverlay
{
    public static class GLExt
    {
        public static void ClearColor(this GL gl, Colour colour) =>
            gl.ClearColor(colour.R, colour.G, colour.B, colour.A);
    }
}
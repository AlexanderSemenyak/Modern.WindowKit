﻿using Modern.WindowKit.Platform;

namespace Modern.WindowKit.Controls.Platform.Surfaces
{
    public interface IFramebufferPlatformSurface
    {
        /// <summary>
        /// Provides a framebuffer descriptor for drawing.
        /// </summary>
        /// <remarks>
        /// Contents should be drawn on actual window after disposing
        /// </remarks>
        ILockedFramebuffer Lock();
    }
}

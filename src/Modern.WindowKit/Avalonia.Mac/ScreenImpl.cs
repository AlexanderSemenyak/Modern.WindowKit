﻿using System;
using System.Collections.Generic;
using Avalonia.Native.Interop;
using Modern.WindowKit.Platform;

namespace Modern.WindowKit.Native
{
    class ScreenImpl : IScreenImpl, IDisposable
    {
        private IAvnScreens _native;

        public ScreenImpl(IAvnScreens native)
        {
            _native = native;
        }

        public int ScreenCount => _native.ScreenCount;

        public IReadOnlyList<Screen> AllScreens
        {
            get
            {
                if (_native != null)
                {
                    var count = ScreenCount;
                    var result = new Screen[count];

                    for (int i = 0; i < count; i++)
                    {
                        var screen = _native.GetScreen(i);

                        result[i] = new Screen(
                            screen.PixelDensity,
                            screen.Bounds.ToAvaloniaPixelRect(),
                            screen.WorkingArea.ToAvaloniaPixelRect(),
                            screen.Primary.FromComBool());
                    }

                    return result;
                }

                return Array.Empty<Screen>();
            }
        }

        public void Dispose ()
        {
            _native?.Dispose();
            _native = null;
        }
    }
}

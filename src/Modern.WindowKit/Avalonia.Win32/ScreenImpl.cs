﻿using System;
using System.Collections.Generic;
using Modern.WindowKit.Platform;
using Modern.WindowKit.Win32.Interop;
using static Modern.WindowKit.Win32.Interop.UnmanagedMethods;

namespace Modern.WindowKit.Win32
{
    public class ScreenImpl : IScreenImpl
    {
        public int ScreenCount
        {
            get => GetSystemMetrics(SystemMetric.SM_CMONITORS);
        }

        private Screen[] _allScreens;
        public IReadOnlyList<Screen> AllScreens
        {
            get
            {
                if (_allScreens == null)
                {
                    int index = 0;
                    Screen[] screens = new Screen[ScreenCount];
                    EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
                        (IntPtr monitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr data) =>
                        {
                            MONITORINFO monitorInfo = MONITORINFO.Create();
                            if (GetMonitorInfo(monitor, ref monitorInfo))
                            {
                                var dpi = 1.0;

                                var shcore = LoadLibrary("shcore.dll");
                                var method = GetProcAddress(shcore, nameof(GetDpiForMonitor));
                                if (method != IntPtr.Zero)
                                {
                                    GetDpiForMonitor(monitor, MONITOR_DPI_TYPE.MDT_EFFECTIVE_DPI, out var x, out _);
                                    dpi = (double)x;
                                }
                                else
                                {
                                    var hdc = GetDC(IntPtr.Zero);

                                    double virtW = GetDeviceCaps(hdc, DEVICECAP.HORZRES);
                                    double physW = GetDeviceCaps(hdc, DEVICECAP.DESKTOPHORZRES);

                                    dpi = (96d * physW / virtW);

                                    ReleaseDC(IntPtr.Zero, hdc);
                                }

                                RECT bounds = monitorInfo.rcMonitor;
                                RECT workingArea = monitorInfo.rcWork;
                                PixelRect avaloniaBounds = bounds.ToPixelRect();
                                PixelRect avaloniaWorkArea = workingArea.ToPixelRect();
                                screens[index] =
                                    new WinScreen(dpi / 96.0d, avaloniaBounds, avaloniaWorkArea, monitorInfo.dwFlags == 1,
                                        monitor);
                                index++;
                            }
                            return true;
                        }, IntPtr.Zero);
                    _allScreens = screens;
                }
                return _allScreens;
            }
        }

        public void InvalidateScreensCache()
        {
            _allScreens = null;
        }
    }
}

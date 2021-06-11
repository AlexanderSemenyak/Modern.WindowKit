﻿#nullable disable

// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.ComponentModel;

namespace Modern.WindowKit.Input
{
    //[Flags, Obsolete("Use KeyModifiers and PointerPointProperties")]
    //internal enum InputModifiers
    //{
    //    None = 0,
    //    Alt = 1,
    //    Control = 2,
    //    Shift = 4,
    //    Windows = 8,
    //    LeftMouseButton = 16,
    //    RightMouseButton = 32,
    //    MiddleMouseButton = 64
    //}

    [Flags]
    public enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Meta = 8,
    }

    [Flags]
    public enum KeyStates
    {
        None = 0,
        Down = 1,
        Toggled = 2,
    }

    [Flags]
    public enum RawInputModifiers
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Meta = 8,
        LeftMouseButton = 16,
        RightMouseButton = 32,
        MiddleMouseButton = 64,
        XButton1MouseButton = 128,
        XButton2MouseButton = 256,
        KeyboardMask = Alt | Control | Shift | Meta
    }

    public static class KeyModifiersUtils
    {
        public static KeyModifiers ConvertToKey(RawInputModifiers modifiers) =>
            (KeyModifiers)(modifiers & RawInputModifiers.KeyboardMask);
    }

    public interface IKeyboardDevice : IInputDevice, INotifyPropertyChanged
    {
    //    IInputElement FocusedElement { get; }

    //    void SetFocusedElement(
    //        IInputElement element, 
    //        NavigationMethod method,
    //        InputModifiers modifiers);
    }
}

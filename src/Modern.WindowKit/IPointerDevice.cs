﻿#nullable disable

// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
//using Modern.WindowKit.VisualTree;

namespace Modern.WindowKit.Input
{
    public interface IPointerDevice : IInputDevice
    {
        //[Obsolete("Use IPointer")]
        //IInputElement Captured { get; }
        
        //[Obsolete("Use IPointer")]
        //void Capture(IInputElement control);

        //[Obsolete("Use PointerEventArgs.GetPosition")]
        //Point GetPosition(IVisual relativeTo);
    }
}

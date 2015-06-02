using System;

namespace MonoForce.Controls
{
    /// </summary>
    /// Enumerates GUI control events.
    /// <summary>
    public enum Message
    {
        Click,
        MouseDown,
        MouseUp,
        MousePress,
        MouseMove,
        MouseOver,
        MouseOut,
        MouseScroll,
        KeyDown,
        KeyUp,
        KeyPress,
        GamePadDown,
        GamePadUp,
        GamePadPress
    }

    /// </summary>
    /// Enumerates GUI control states.
    /// <summary>
    public enum ControlState
    {
        /// </summary>
        /// The control is enabled.
        /// <summary>
        Enabled,

        /// </summary>
        /// Mouse cursor is directly over the control.
        /// <summary>
        Hovered,

        /// </summary>
        /// The control is pressed down.
        /// <summary>
        Pressed,

        /// </summary>
        /// The control has input focus.
        /// <summary>
        Focused,

        /// </summary>
        /// The control is disabled and cannot be used.
        /// <summary>
        Disabled
    }

    /// </summary>
    /// Describes how control content is aligned within the control.
    /// <summary>
    public enum Alignment
    {
        /// </summary>
        /// No alignment. Defaults to TopLeft?
        /// <summary>
        None,

        /// </summary>
        /// Content is left-aligned at the top of the control.
        /// <summary>
        TopLeft,

        /// </summary>
        /// Content is centered at the top of the control.
        /// <summary>
        TopCenter,

        /// </summary>
        /// Content is right-aligned at the top of the control.
        /// <summary>
        TopRight,

        /// </summary>
        /// Content is left-aligned in the center of the control.
        /// <summary>
        MiddleLeft,

        /// </summary>
        /// Content is centered in the control.
        /// <summary>
        MiddleCenter,

        /// </summary>
        /// Content is right-aligned in the center of the control.
        /// <summary>
        MiddleRight,

        /// </summary>
        /// Content is left-aligned at the bottom of the control.
        /// <summary>
        BottomLeft,

        /// </summary>
        /// Content is centered at the bottom of the control.
        /// <summary>
        BottomCenter,

        /// </summary>
        /// Content is right-aligned at the bottom of the control.
        /// <summary>
        BottomRight
    }

    /// </summary>
    /// Indicates which dialog button was pressed.
    /// <summary>
    public enum ModalResult
    {
        /// </summary>
        /// No alignment. Defaults to TopLeft?
        /// <summary>
        None,
        Ok,
        Cancel,
        Yes,
        No,
        Abort,
        Retry,
        Ignore
    }

    /// </summary>
    /// Indicates the orientation of stack panels and scroll bars.
    /// <summary>
    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    /// </summary>
    /// Indicates which scroll bars are present in a text box.
    /// <summary>
    public enum ScrollBars
    {
        /// </summary>
        /// No alignment. Defaults to TopLeft?
        /// <summary>
        None,
        Vertical,
        Horizontal,
        Both
    }

    /// </summary>
    /// Describes how a control is anchored within its parent container.
    /// <summary>
    [Flags]
    public enum Anchors
    {
        None = 0x00,
        Left = 0x01,
        Top = 0x02,
        Right = 0x04,
        Bottom = 0x08,
        Horizontal = Left | Right,
        Vertical = Top | Bottom,
        All = Left | Top | Right | Bottom
    }


    /// </summary>
    /// Describes a control's margins.
    /// <summary>
    public struct Margins
    {
        /// </summary>
        /// Gets the total horizontal margin amount.
        /// <summary>
        public int Horizontal
        {
            get { return (Left + Right); }
        }

        /// </summary>
        /// Gets the total vertical margin amount.
        /// <summary>
        public int Vertical
        {
            get { return (Top + Bottom); }
        }

        /// </summary>
        /// Bottom side margin value.
        /// <summary>
        public int Bottom;

        /// </summary>
        /// Left side margin value.
        /// <summary>
        public int Left;

        /// </summary>
        /// Right side margin value.
        /// <summary>
        public int Right;

        /// </summary>
        /// Top side margin value.
        /// <summary>
        public int Top;

        /// <param name="bottom">Bottom side margin amount.</param>
        /// <param name="right">Right side margin amount.</param>
        /// <param name="top">Top side margin amount.</param>
        /// <param name="left">Left side margin amount.</param>
        /// </summary>
        /// Creates a Margins object with the specified values.
        /// <summary>
        public Margins(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }

    /// </summary>
    /// Describes the size of a control.
    /// <summary>
    public struct Size
    {
        /// </summary>
        /// Returns a Size object with null dimensions.
        /// <summary>
        public static Size Zero
        {
            get { return new Size(0, 0); }
        }

        /// </summary>
        /// Height of the control.
        /// <summary>
        public int Height;

        /// </summary>
        /// Width of the control.
        /// <summary>
        public int Width;

        /// <param name="height">Height of the object.</param>
        /// <param name="width">Width of the object.</param>
        /// </summary>
        /// Creates a new Size object with the specified dimensions.
        /// <summary>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
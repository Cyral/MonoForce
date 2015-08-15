using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoForce.Controls
{
    /// <summary>
    /// Defines the input devices a Neoforce Application supports.
    /// </summary>
    [Flags]
    public enum InputMethods
    {
        None = 0x00,
        Keyboard = 0x01,
        Mouse = 0x02,
        All = Keyboard | Mouse
    }

    /// <summary>
    /// Identifies a particular button on a mouse.
    /// </summary>
    public enum MouseButton
    {
        None = 0,
        Left,
        Right,
        Middle,
        XButton1,
        XButton2
    }

    public enum MouseScrollDirection
    {
        None = 0,
        Down = 1,
        Up = 2
    }

    /// <summary>
    /// Defines the input offset and ratio to use when rescaling controls in the render target.
    /// </summary>
    public struct InputOffset
    {
        /// <summary>
        /// Target Width / Actual Screen Width.
        /// </summary>
        public float RatioX;

        /// <summary>
        /// Target Height / Actual Screen Height.
        /// </summary>
        public float RatioY;

        /// <summary>
        /// Mouse position X offset.
        /// </summary>
        public int X;

        /// <summary>
        /// Mouse position Y offset.
        /// </summary>
        public int Y;

        /// <param name="ry">Y ratio.</param>
        /// <param name="rx">X ratio.</param>
        /// <param name="y">Mouse position Y offset.</param>
        /// <param name="x">Mouse position X offset.</param>
        /// <summary>
        /// Creates a new instance of the InputOffset class.
        /// </summary>
        public InputOffset(int x, int y, float rx, float ry)
        {
            X = x;
            Y = y;
            RatioX = rx;
            RatioY = ry;
        }
    }


    public class InputSystem : Disposable
    {
        /// <summary>
        /// will register a repeated press event when held down.
        /// Indicates how much delay (in ms) there will be before a key/button
        /// </summary>
        private const int RepeatDelay = 500;

        /// <summary>
        /// press events after the initial RepeatDelay timer has expired.
        /// Indicates how much delay (in ms) there will be between repeated key/button
        /// </summary>
        private const int RepeatRate = 50;

        /// <summary>
        /// Sets or gets input methods allowed for navigation.
        /// </summary>
        public virtual InputMethods InputMethods
        {
            get { return inputMethods; }
            set { inputMethods = value; }
        }

        /// <summary>
        /// Sets or gets input offset and ratio when rescaling controls in render target.
        /// </summary>
        public virtual InputOffset InputOffset
        {
            get { return inputOffset; }
            set { inputOffset = value; }
        }

        /// <summary>
        /// an action.
        /// Indicates how far a thumbstick must be moved from center position to register
        /// </summary>
        private readonly float ClickThreshold = 0.5f;

        /// <summary>
        /// List to track the state and repeat timers of all keyboard keys.
        /// </summary>
        private readonly List<InputKey> keys = new List<InputKey>();

        /// <summary>
        /// Application's GUI manager.
        /// </summary>
        private readonly Manager manager;

        /// <summary>
        /// List to track the state and repeat timers of all mouse buttons.
        /// </summary>
        private readonly List<InputMouseButton> mouseButtons = new List<InputMouseButton>();

        /// <summary>
        /// Specifies what input devices can be used to navigate the application's controls.
        /// </summary>
        private InputMethods inputMethods = InputMethods.All;

        /// <summary>
        /// Input offset and ratio to use when rescaling controls in the render target.
        /// </summary>
        private InputOffset inputOffset = new InputOffset(0, 0, 1.0f, 1.0f);

        /// <summary>
        /// Current mouse state.
        /// </summary>
        private MouseState mouseState;

        /// <param name="offset">???</param>
        /// <param name="manager">Application's GUI manager.</param>
        /// <summary>
        /// Creates the Input System.
        /// </summary>
        public InputSystem(Manager manager, InputOffset offset)
        {
            inputOffset = offset;
            this.manager = manager;
        }

        public InputSystem(Manager manager) : this(manager, new InputOffset(0, 0, 1.0f, 1.0f))
        {
        }

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <param name="state">Current mouse state.</param>
        /// <summary>
        /// Updates the mouse device.
        /// </summary>
        private void UpdateMouse(MouseState state, GameTime gameTime)
        {
            if ((state.X != mouseState.X) || (state.Y != mouseState.Y))
            {
                var e = new MouseEventArgs();


                var btn = MouseButton.None;
                if (state.LeftButton == ButtonState.Pressed) btn = MouseButton.Left;
                else if (state.RightButton == ButtonState.Pressed) btn = MouseButton.Right;
                else if (state.MiddleButton == ButtonState.Pressed) btn = MouseButton.Middle;
                else if (state.XButton1 == ButtonState.Pressed) btn = MouseButton.XButton1;
                else if (state.XButton2 == ButtonState.Pressed) btn = MouseButton.XButton2;


                BuildMouseEvent(state, btn, ref e);
                if (MouseMove != null)
                {
                    MouseMove.Invoke(this, e);
                }
            }


// Mouse wheel position changed
            if (state.ScrollWheelValue != mouseState.ScrollWheelValue)
            {
                var e = new MouseEventArgs();
                var direction = state.ScrollWheelValue < mouseState.ScrollWheelValue
                    ? MouseScrollDirection.Down
                    : MouseScrollDirection.Up;


                BuildMouseEvent(state, MouseButton.None, direction, ref e);


                MouseScroll?.Invoke(this, e);
            }


            UpdateButtons(state, gameTime);


            mouseState = state;
        }

        #region Nested type: Class

        /// <summary>
        /// Represents a key, its state, and repeat delay timer.
        /// </summary>
        private class InputKey
        {
            /// <summary>
            /// Timer used to delay firing of repeat key presses.
            /// </summary>
            public double Countdown = RepeatDelay;

            /// <summary>
            /// Key that this object represents.
            /// </summary>
            public Keys Key = Keys.None;

            /// <summary>
            /// Indicates if the key is pressed or released.
            /// </summary>
            public bool Pressed;
        }

        #endregion

        #region Nested type: Class

        /// <summary>
        /// Represents the state of the mouse device and the current mouse cursor position.
        /// </summary>
        private class InputMouse
        {
            /// <summary>
            /// Current mouse cursor position.
            /// </summary>
            public Point Position = new Point(0, 0);

            /// <summary>
            /// Current mouse state.
            /// </summary>
            public MouseState State = new MouseState();
        }

        #endregion

        #region Nested type: Class

        /// <summary>
        /// Represents a mouse button, its state, and repeat delay timer.
        /// </summary>
        private class InputMouseButton
        {
            /// <summary>
            /// The mouse button this object represents.
            /// </summary>
            public MouseButton Button = MouseButton.None;

            /// <summary>
            /// Timer used to delay firing of repeat key presses.
            /// </summary>
            public double Countdown = RepeatDelay;

            /// <summary>
            /// Indicates if the key is pressed or released.
            /// </summary>
            public bool Pressed;

            /// <summary>
            /// Creates a default instance of the InputMouseButton class.
            /// </summary>
            public InputMouseButton()
            {
            }

            /// <param name="button">Mouse button this instance will represent.</param>
            /// <summary>
            /// Creates an instance of the InputMouseButton class set for the specified mouse button.
            /// </summary>
            public InputMouseButton(MouseButton button)
            {
                Button = button;
            }
        }

        #endregion

        /// <summary>
        /// Initializes the input system.
        /// </summary>
        public virtual void Initialize()
        {
            keys.Clear();
            mouseButtons.Clear();

#if (!XBOX && !XBOX_FAKE)
// Initialize the keys list.
            foreach (var str in Enum.GetNames(typeof (Keys)))
            {
                var key = new InputKey {Key = (Keys) Enum.Parse(typeof (Keys), str)};
                keys.Add(key);
            }
// Initialize the mouse buttons list.
            foreach (var str in Enum.GetNames(typeof (MouseButton)))
            {
                var btn = new InputMouseButton();
                btn.Button = (MouseButton) Enum.Parse(typeof (MouseButton), str);
                mouseButtons.Add(btn);
            }
        }

        /// <summary>
        /// Occurs when a key enters the pressed state.
        /// </summary>
        public event KeyEventHandler KeyDown;

        /// <summary>
        /// Occurs after a key down event and also occurs for repeat key press events.
        /// </summary>
        public event KeyEventHandler KeyPress;

        /// <summary>
        /// Occurs when a key leaves the pressed state.
        /// </summary>
        public event KeyEventHandler KeyUp;

        /// <summary>
        /// Occurs when a mouse button enters the pressed state.
        /// </summary>
        public event MouseEventHandler MouseDown;

        /// <summary>
        /// Occurs when the mouse is moved.
        /// </summary>
        public event MouseEventHandler MouseMove;

        /// <summary>
        /// Occurs after a mouse down event and occurs for repeat mouse press events.
        /// </summary>
        public event MouseEventHandler MousePress;

        /// <summary>
        /// Occurs when the mouse is scrolled.
        /// </summary>
        public event MouseEventHandler MouseScroll;

        /// <summary>
        /// Occurs when a mouse button leaves the pressed state.
        /// </summary>
        public event MouseEventHandler MouseUp;

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <param name="state">Keyboard state used to update the current state of the keyboard.</param>
        /// <summary>
        /// Updates the current keyboard state with the supplied arguments.
        /// </summary>
        public virtual void SendKeyboardState(KeyboardState state, GameTime gameTime)
        {
            UpdateKeys(state, gameTime);
        }

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <param name="state">Mouse state to use to update the current mouse state.</param>
        /// <summary>
        /// Updates the current mouse state using the supplied arguments.
        /// </summary>
        public virtual void SendMouseState(MouseState state, GameTime gameTime)
        {
            UpdateMouse(state, gameTime);
        }


        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <summary>
        /// Updates the state of supported input devices.
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
#if (!XBOX && !XBOX_FAKE)
// Mouse and keyboard support on Windows only.
            var ms = Mouse.GetState();
            var ks = Keyboard.GetState();
#endif


            {
#if (!XBOX && !XBOX_FAKE)
// Update the mouse and keyboard device states on Windows, if the application uses them.
                if ((inputMethods & InputMethods.Mouse) == InputMethods.Mouse) UpdateMouse(ms, gameTime);
                if ((inputMethods & InputMethods.Keyboard) == InputMethods.Keyboard) UpdateKeys(ks, gameTime);
#endif
            }
        }


        /// <param name="e">Mouse event arguments.</param>
        /// <summary>
        /// Adjusts the position of the mouse cursor to keep it within the client region of the window.
        /// </summary>
        private void AdjustPosition(ref MouseEventArgs e)
        {
            var screen = manager.Game.Window.ClientBounds;

            if (e.Position.X < 0) e.Position.X = 0;
            if (e.Position.Y < 0) e.Position.Y = 0;
            if (e.Position.X >= screen.Width) e.Position.X = screen.Width - 1;
            if (e.Position.Y >= screen.Height) e.Position.Y = screen.Height - 1;
        }


        private void BuildMouseEvent(MouseState state, MouseButton button, ref MouseEventArgs e)
        {
            e.State = state;
            e.Button = button;


            e.Position = new Point(state.X, state.Y);
            AdjustPosition(ref e);


            e.Position = RecalcPosition(e.Position);
            e.State = new MouseState(e.Position.X, e.Position.Y, e.State.ScrollWheelValue, e.State.LeftButton,
                e.State.MiddleButton, e.State.RightButton, e.State.XButton1, e.State.XButton2);


            var pos = RecalcPosition(new Point(mouseState.X, mouseState.Y));
            e.Difference = new Point(e.Position.X - pos.X, e.Position.Y - pos.Y);
        }


        private void BuildMouseEvent(MouseState state, MouseButton button, MouseScrollDirection direction,
            ref MouseEventArgs e)
        {
            BuildMouseEvent(state, button, ref e);


            e.ScrollDirection = direction;
        }

        /// <returns>Returns the adjusted mouse position.</returns>
        /// <param name="pos">Original mouse position.</param>
        /// <summary>
        /// Adjusts the mouse position to account for rescaling on the render target.
        /// </summary>
        private Point RecalcPosition(Point pos)
        {
            return new Point((int) ((pos.X - InputOffset.X)/InputOffset.RatioX),
                (int) ((pos.Y - InputOffset.Y)/InputOffset.RatioY));
        }


        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <param name="state">Current state of the mouse device.</param>
        /// <summary>
        /// Updates the state of the Mouse buttons.
        /// </summary>
        private void UpdateButtons(MouseState state, GameTime gameTime)
        {
#if (!XBOX && !XBOX_FAKE)


            var e = new MouseEventArgs();


// Update the state of the buttons in the mouse button list.
            foreach (var btn in mouseButtons)
            {
// Current button state of the current gamepad button.
                var bs = ButtonState.Released;

                if (btn.Button == MouseButton.Left) bs = state.LeftButton;
                else if (btn.Button == MouseButton.Right) bs = state.RightButton;
                else if (btn.Button == MouseButton.Middle) bs = state.MiddleButton;
                else if (btn.Button == MouseButton.XButton1) bs = state.XButton1;
                else if (btn.Button == MouseButton.XButton2) bs = state.XButton2;
                else continue;

// Current state button pressed?
                var pressed = (bs == ButtonState.Pressed);
                if (pressed)
                {
// Update the repeat delay timer for the associated button.
                    var ms = gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (pressed) btn.Countdown -= ms;
                }

// Button was just pressed?
                if ((pressed) && (!btn.Pressed))
                {
// "Press" the associated button.
                    btn.Pressed = true;
                    BuildMouseEvent(state, btn.Button, ref e);

// Fire the MouseDown and MousePress events.
                    if (MouseDown != null) MouseDown.Invoke(this, e);
// Fire the repeat MousePress event.
                }
// Button was just released?
                else if ((!pressed) && (btn.Pressed))
                {
// "Release" the associated button and reset the repeat delay timer.
                    btn.Pressed = false;
                    btn.Countdown = RepeatDelay;
                    BuildMouseEvent(state, btn.Button, ref e);
// Fire the MouseUp event.
                    if (MouseUp != null) MouseUp.Invoke(this, e);
                }
// Button is held down and it's time to fire a repeat press event?
                else if (btn.Pressed && btn.Countdown < 0)
                {
// Update event args and reset timer.
                    e.Button = btn.Button;
                    btn.Countdown = RepeatRate;
                    BuildMouseEvent(state, btn.Button, ref e);

// Fire the repeat MousePress event.
                    if (MousePress != null) MousePress.Invoke(this, e);
                }
            }

#endif
        }


        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <param name="state">Current keyboard state.</param>
        /// <summary>
        /// Updates the state of the keys in the list.
        /// </summary>
        private void UpdateKeys(KeyboardState state, GameTime gameTime)
        {
            var e = new KeyEventArgs();


// Is CapsLock on?
            e.Caps = (((ushort) NativeMethods.GetKeyState(0x14)) & 0xffff) != 0;


// Check for modifier key presses.
            foreach (var key in state.GetPressedKeys())
            {
                if (key == Keys.LeftAlt || key == Keys.RightAlt) e.Alt = true;
                else if (key == Keys.LeftShift || key == Keys.RightShift) e.Shift = true;
                else if (key == Keys.LeftControl || key == Keys.RightControl) e.Control = true;
            }


// Update the rest of the key states.
            foreach (var key in keys)
            {
// Ignore modifier keys, they're already handled.
                if (key.Key == Keys.LeftAlt || key.Key == Keys.RightAlt ||
                    key.Key == Keys.LeftShift || key.Key == Keys.RightShift ||
                    key.Key == Keys.LeftControl || key.Key == Keys.RightControl)
                {
                    continue;
                }


// Is the current key state key pressed?
                var pressed = state.IsKeyDown(key.Key);


// Update the repeat delay timer for the associated button.
                var ms = gameTime.ElapsedGameTime.TotalMilliseconds;
                if (pressed) key.Countdown -= ms;


// Key was just pressed?
                if ((pressed) && (!key.Pressed))
                {
// Update the state of the associated key.
                    key.Pressed = true;
                    e.Key = key.Key;


// Fire the KeyDown and initial KeyPress events.
                    if (KeyDown != null) KeyDown.Invoke(this, e);
// Fire the KeyPress event again.
                    if (KeyPress != null) KeyPress.Invoke(this, e);
                }
// Key was just released?
                else if ((!pressed) && (key.Pressed))
                {
// Update the state of the associated key and reset the repeat delay timer.
                    key.Pressed = false;
                    key.Countdown = RepeatDelay;
                    e.Key = key.Key;


// Fire the KeyUp event.
                    if (KeyUp != null) KeyUp.Invoke(this, e);
                }
// Key is held down and it's time to fire an additional KeyPress event?
                else if (key.Pressed && key.Countdown < 0)
                {
// Reset the repeat delay timer.
                    key.Countdown = RepeatRate;
                    e.Key = key.Key;


// Fire the KeyPress event again.
                    if (KeyPress != null) KeyPress.Invoke(this, e);
                }
            }
#endif
        }

        #region Nested type: Class

        #endregion
    }
}
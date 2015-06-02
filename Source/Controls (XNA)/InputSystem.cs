// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

namespace MonoForce.Controls
{
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

    /// </summary>
    /// Defines the input devices a Neoforce Application supports.
    /// <summary>
    [Flags]
    public enum InputMethods
    {
        None = 0x00,
        Keyboard = 0x01,
        Mouse = 0x02,
        GamePad = 0x04,
        All = Keyboard | Mouse | 0x04
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
    }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

    /// </summary>
    /// Identifies a particular button on a mouse.
    /// <summary>
    public enum MouseButton
    {
        None = 0,
        Left,
        Right,
        Middle,
        XButton1,
        XButton2
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
    }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

    public enum MouseScrollDirection
    {
        None = 0,
        Down = 1,
        Up = 2
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
    }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

    /// </summary>
    /// Identifies a particular button on an Xbox 360 gamepad.
    /// <summary>
    public enum GamePadButton
    {
        None = 0,
        Start = 6,
        Back,
        Up,
        Down,
        Left,
        Right,
        A,
        B,
        X,
        Y,
        BigButton,
        LeftShoulder,
        RightShoulder,
        LeftTrigger,
        RightTrigger,
        LeftStick,
        RightStick,
        LeftStickLeft,
        LeftStickRight,
        LeftStickUp,
        LeftStickDown,
        RightStickLeft,
        RightStickRight,
        RightStickUp,
        RightStickDown
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
    }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

    /// </summary>
    /// Identifies the index of the player who has input focus.
    /// <summary>
    public enum ActivePlayer
    {
        None = -1,
        One = 0,
        Two = 1,
        Three = 2,
        Four = 3
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
    }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

    /// </summary>
    /// Stores the thumb stick and trigger values of a gamepad.
    /// <summary>
    public struct GamePadVectors
    {
        /// </summary>
        /// Left thumb stick axis values.
        /// <summary>
        public Vector2 LeftStick;

        /// </summary>
        /// Left trigger value.
        /// <summary>
        public float LeftTrigger;

        /// </summary>
        /// Right thumb stick axis values.
        /// <summary>
        public Vector2 RightStick;

        /// </summary>
        /// Right trigger value.
        /// <summary>
        public float RightTrigger;
    }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

    /// </summary>
    /// Defines the input offset and ratio to use when rescaling controls in the render target.
    /// <summary>
    public struct InputOffset
    {
        /// </summary>
        /// Target Width / Actual Screen Width.
        /// <summary>
        public float RatioX;

        /// </summary>
        /// Target Height / Actual Screen Height.
        /// <summary>
        public float RatioY;

        /// </summary>
        /// Mouse position X offset.
        /// <summary>
        public int X;

        /// </summary>
        /// Mouse position Y offset.
        /// <summary>
        public int Y;

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// <param name="ry">Y ratio.</param>
        /// <param name="rx">X ratio.</param>
        /// <param name="y">Mouse position Y offset.</param>
        /// <param name="x">Mouse position X offset.</param>
        /// </summary>
        /// Creates a new instance of the InputOffset class.
        /// <summary>
        public InputOffset(int x, int y, float rx, float ry)
        {
            X = x;
            Y = y;
            RatioX = rx;
            RatioY = ry;
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }
    }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

    public class InputSystem : Disposable
    {
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// </summary>
        /// will register a repeated press event when held down.
        /// Indicates how much delay (in ms) there will be before a key/button
        /// <summary>
        private const int RepeatDelay = 500;

        /// </summary>
        /// press events after the initial RepeatDelay timer has expired.
        /// Indicates how much delay (in ms) there will be between repeated key/button
        /// <summary>
        private const int RepeatRate = 50;

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// </summary>
        /// Gets or sets the index of the player who has input focus.
        /// <summary>
        public virtual ActivePlayer ActivePlayer
        {
            get { return activePlayer; }
            set { activePlayer = value; }
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// </summary>
        /// Sets or gets input methods allowed for navigation.
        /// <summary>
        public virtual InputMethods InputMethods
        {
            get { return inputMethods; }
            set { inputMethods = value; }
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// </summary>
        /// Sets or gets input offset and ratio when rescaling controls in render target.
        /// <summary>
        public virtual InputOffset InputOffset
        {
            get { return inputOffset; }
            set { inputOffset = value; }
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

        /// </summary>
        /// an action.
        /// Indicates how far a thumbstick must be moved from center position to register
        /// <summary>
        private readonly float ClickThreshold = 0.5f;

        /// </summary>
        /// List to track the state and repeat timers of all gamepad buttons.
        /// <summary>
        private readonly List<InputGamePadButton> gamePadButtons = new List<InputGamePadButton>();

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// </summary>
        /// List to track the state and repeat timers of all keyboard keys.
        /// <summary>
        private readonly List<InputKey> keys = new List<InputKey>();

        /// </summary>
        /// Application's GUI manager.
        /// <summary>
        private readonly Manager manager;

        /// </summary>
        /// List to track the state and repeat timers of all mouse buttons.
        /// <summary>
        private readonly List<InputMouseButton> mouseButtons = new List<InputMouseButton>();

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

#if (!XBOX && !XBOX_FAKE)
        /// </summary>
        /// The focused form of the application when running on Windows.
        /// <summary>
        private readonly Form window;
#endif

        /// </summary>
        /// Index of the player with input focus over the application.
        /// <summary>
        private ActivePlayer activePlayer = ActivePlayer.None;

        /// </summary>
        /// Current gamepad state.
        /// <summary>
        private GamePadState gamePadState;

        /// </summary>
        /// Specifies what input devices can be used to navigate the application's controls.
        /// <summary>
        private InputMethods inputMethods = InputMethods.All;

        /// </summary>
        /// Input offset and ratio to use when rescaling controls in the render target.
        /// <summary>
        private InputOffset inputOffset = new InputOffset(0, 0, 1.0f, 1.0f);

        /// </summary>
        /// Current mouse state.
        /// <summary>
        private MouseState mouseState;

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// <param name="offset">???</param>
        /// <param name="manager">Application's GUI manager.</param>
        /// </summary>
        /// Creates the Input System.
        /// <summary>
        public InputSystem(Manager manager, InputOffset offset)
        {
            inputOffset = offset;
            this.manager = manager;
#if (!XBOX && !XBOX_FAKE)
            window = manager.Window;
#endif
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        public InputSystem(Manager manager) : this(manager, new InputOffset(0, 0, 1.0f, 1.0f))
        {
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

        /// </summary>
        /// Occurs when a gamepad button enters the pressed state.
        /// <summary>
        public event GamePadEventHandler GamePadDown;

        /// </summary>
        /// Occurs when the values of the gamepad thumb sticks change.
        /// <summary>
        public event GamePadEventHandler GamePadMove;

        /// </summary>
        /// Occurs after a gamepad down event and occurs for repeat button press events.
        /// <summary>
        public event GamePadEventHandler GamePadPress;

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// </summary>
        /// Occurs when a gamepad button leaves the pressed state.
        /// <summary>
        public event GamePadEventHandler GamePadUp;

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// </summary>
        /// Initializes the input system.
        /// <summary>
        public virtual void Initialize()
        {
            keys.Clear();
            mouseButtons.Clear();
            gamePadButtons.Clear();
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

#if (!XBOX && !XBOX_FAKE)
// Initialize the keys list.
            foreach (var str in Enum.GetNames(typeof (Keys)))
            {
                var key = new InputKey();
                key.Key = (Keys)Enum.Parse(typeof (Keys), str);
                keys.Add(key);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
            }
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Initialize the mouse buttons list.
            foreach (var str in Enum.GetNames(typeof (MouseButton)))
            {
                var btn = new InputMouseButton();
                btn.Button = (MouseButton)Enum.Parse(typeof (MouseButton), str);
                mouseButtons.Add(btn);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
            }
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Initialize the gamepad buttons list.
            foreach (var str in Enum.GetNames(typeof (GamePadButton)))
            {
                var btn = new InputGamePadButton();
                btn.Button = (GamePadButton)Enum.Parse(typeof (GamePadButton), str);
                gamePadButtons.Add(btn);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
            }
#else
// Initialize the gamepad buttons list.
gamePadButtons.Add(new InputGamePadButton(GamePadButton.None));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.Start));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.Back));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.Up));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.Down));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.Left));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.Right));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.A));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.B));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.X));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.Y));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.BigButton));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.LeftShoulder));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.RightShoulder));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.LeftTrigger));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.RightTrigger));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.LeftStick));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.RightStick));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.LeftStickLeft));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.LeftStickRight));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.LeftStickUp));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.LeftStickDown));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.RightStickLeft));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.RightStickRight));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.RightStickUp));
gamePadButtons.Add(new InputGamePadButton(GamePadButton.RightStickDown));
#endif
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// </summary>
        /// Occurs when a key enters the pressed state.
        /// <summary>
        public event KeyEventHandler KeyDown;

        /// </summary>
        /// Occurs after a key down event and also occurs for repeat key press events.
        /// <summary>
        public event KeyEventHandler KeyPress;

        /// </summary>
        /// Occurs when a key leaves the pressed state.
        /// <summary>
        public event KeyEventHandler KeyUp;

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// </summary>
        /// Occurs when a mouse button enters the pressed state.
        /// <summary>
        public event MouseEventHandler MouseDown;

        /// </summary>
        /// Occurs when the mouse is moved.
        /// <summary>
        public event MouseEventHandler MouseMove;

        /// </summary>
        /// Occurs after a mouse down event and occurs for repeat mouse press events.
        /// <summary>
        public event MouseEventHandler MousePress;

        /// </summary>
        /// Occurs when the mouse is scrolled.
        /// <summary>
        public event MouseEventHandler MouseScroll;

        /// </summary>
        /// Occurs when a mouse button leaves the pressed state.
        /// <summary>
        public event MouseEventHandler MouseUp;

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <param name="state">Gamepad state to use to update the current gamepad state values.</param>
        /// <param name="playerIndex">PlayerIndex of the gamepad to update.</param>
        /// </summary>
        /// Updates the state of the specified gamepad using the supplied arguments.
        /// <summary>
        public virtual void SendGamePadState(PlayerIndex playerIndex, GamePadState state, GameTime gameTime)
        {
            UpdateGamePad(playerIndex, state, gameTime);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <param name="state">Keyboard state used to update the current state of the keyboard.</param>
        /// </summary>
        /// Updates the current keyboard state with the supplied arguments.
        /// <summary>
        public virtual void SendKeyboardState(KeyboardState state, GameTime gameTime)
        {
            UpdateKeys(state, gameTime);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <param name="state">Mouse state to use to update the current mouse state.</param>
        /// </summary>
        /// Updates the current mouse state using the supplied arguments.
        /// <summary>
        public virtual void SendMouseState(MouseState state, GameTime gameTime)
        {
            UpdateMouse(state, gameTime);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// </summary>
        /// Updates the state of supported input devices.
        /// <summary>
        public virtual void Update(GameTime gameTime)
        {
#if (!XBOX && !XBOX_FAKE)
// Mouse and keyboard support on Windows only.
            var ms = Mouse.GetState();
            var ks = Keyboard.GetState(PlayerIndex.One);
#endif
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

#if (!XBOX && !XBOX_FAKE)
            if (window.Focused)
#endif
            {
#if (!XBOX && !XBOX_FAKE)
// Update the mouse and keyboard device states on Windows, if the application uses them.
                if ((inputMethods & InputMethods.Mouse) == InputMethods.Mouse) UpdateMouse(ms, gameTime);
                if ((inputMethods & InputMethods.Keyboard) == InputMethods.Keyboard) UpdateKeys(ks, gameTime);
#endif
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
            }
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// <param name="e">Mouse event arguments.</param>
        /// </summary>
        /// Adjusts the position of the mouse cursor to keep it within the client region of the window.
        /// <summary>
        private void AdjustPosition(ref MouseEventArgs e)
        {
            var screen = manager.Game.Window.ClientBounds;
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            if (e.Position.X < 0) e.Position.X = 0;
            if (e.Position.Y < 0) e.Position.Y = 0;
            if (e.Position.X >= screen.Width) e.Position.X = screen.Width - 1;
            if (e.Position.Y >= screen.Height) e.Position.Y = screen.Height - 1;
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// <param name="e">GamePadEventArgs to update with the supplied values.</param>
        /// <param name="button">Gamepad button to assign to the event args.</param>
        /// <param name="state">Current gamepad state to grab stick and trigger values from.</param>
        /// </summary>
        /// Updates the GamePadEventArgs with the specified button, stick and trigger values.
        /// <summary>
        private void BuildGamePadEvent(GamePadState state, GamePadButton button, ref GamePadEventArgs e)
        {
            e.State = state;
            e.Button = button;
            e.Vectors.LeftStick = new Vector2(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);
            e.Vectors.RightStick = new Vector2(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
            e.Vectors.LeftTrigger = state.Triggers.Left;
            e.Vectors.RightTrigger = state.Triggers.Right;
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        private void BuildMouseEvent(MouseState state, MouseButton button, ref MouseEventArgs e)
        {
            e.State = state;
            e.Button = button;
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            e.Position = new Point(state.X, state.Y);
            AdjustPosition(ref e);
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            e.Position = RecalcPosition(e.Position);
            e.State = new MouseState(e.Position.X, e.Position.Y, e.State.ScrollWheelValue, e.State.LeftButton,
                e.State.MiddleButton, e.State.RightButton, e.State.XButton1, e.State.XButton2);
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            var pos = RecalcPosition(new Point(mouseState.X, mouseState.Y));
            e.Difference = new Point(e.Position.X - pos.X, e.Position.Y - pos.Y);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        private void BuildMouseEvent(MouseState state, MouseButton button, MouseScrollDirection direction,
            ref MouseEventArgs e)
        {
            BuildMouseEvent(state, button, ref e);
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            e.ScrollDirection = direction;
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// otherwise it returns ButtonState.Released.
        /// </returns>
        /// <returns>
        /// Returns ButtonState.Pressed if the value of the specified button exceeds the ClickThreshold value;
        /// <param name="state">Gamepad state to check the specified button on.</param>
        /// <param name="button">Left/Right thumbstick direction or trigger button to get the state of.</param>
        /// </summary>
        /// Checks the specified thumbstick or trigger button and returns its ButtonState.
        /// <summary>
        private ButtonState GetVectorState(GamePadButton button, GamePadState state)
        {
            var ret = ButtonState.Released;
            var down = false;
            var t = ClickThreshold;
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            switch (button)
            {
                case GamePadButton.LeftStickLeft:
                    down = state.ThumbSticks.Left.X < -t;
                    break;
                case GamePadButton.LeftStickRight:
                    down = state.ThumbSticks.Left.X > t;
                    break;
                case GamePadButton.LeftStickUp:
                    down = state.ThumbSticks.Left.Y > t;
                    break;
                case GamePadButton.LeftStickDown:
                    down = state.ThumbSticks.Left.Y < -t;
                    break;
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

                case GamePadButton.RightStickLeft:
                    down = state.ThumbSticks.Right.X < -t;
                    break;
                case GamePadButton.RightStickRight:
                    down = state.ThumbSticks.Right.X > t;
                    break;
                case GamePadButton.RightStickUp:
                    down = state.ThumbSticks.Right.Y > t;
                    break;
                case GamePadButton.RightStickDown:
                    down = state.ThumbSticks.Right.Y < -t;
                    break;
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

                case GamePadButton.LeftTrigger:
                    down = state.Triggers.Left > t;
                    break;
                case GamePadButton.RightTrigger:
                    down = state.Triggers.Right > t;
                    break;
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
            }
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            ret = down ? ButtonState.Pressed : ButtonState.Released;
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            return ret;
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// <returns>Returns the adjusted mouse position.</returns>
        /// <param name="pos">Original mouse position.</param>
        /// </summary>
        /// Adjusts the mouse position to account for rescaling on the render target.
        /// <summary>
        private Point RecalcPosition(Point pos)
        {
            return new Point((int)((pos.X - InputOffset.X) / InputOffset.RatioX),
                (int)((pos.Y - InputOffset.Y) / InputOffset.RatioY));
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

//    PlayerIndex index = PlayerIndex.One;
//{
//if ((inputMethods & InputMethods.GamePad) == InputMethods.GamePad)
//// Update the state of the active gamepad, if the application supports it.

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <param name="state">Current state of the mouse device.</param>
        /// </summary>
        /// Updates the state of the Mouse buttons.
        /// <summary>
        private void UpdateButtons(MouseState state, GameTime gameTime)
        {
#if (!XBOX && !XBOX_FAKE)
//    PlayerIndex index = PlayerIndex.One;
//{
//if ((inputMethods & InputMethods.GamePad) == InputMethods.GamePad)
//// Update the state of the active gamepad, if the application supports it.

            var e = new MouseEventArgs();
//    PlayerIndex index = PlayerIndex.One;
//{
//if ((inputMethods & InputMethods.GamePad) == InputMethods.GamePad)
//// Update the state of the active gamepad, if the application supports it.

// Update the state of the buttons in the mouse button list.
            foreach (var btn in mouseButtons)
            {
// Current button state of the current gamepad button.
                var bs = ButtonState.Released;
//    PlayerIndex index = PlayerIndex.One;
//{
//if ((inputMethods & InputMethods.GamePad) == InputMethods.GamePad)
//// Update the state of the active gamepad, if the application supports it.

                if (btn.Button == MouseButton.Left) bs = state.LeftButton;
                else if (btn.Button == MouseButton.Right) bs = state.RightButton;
                else if (btn.Button == MouseButton.Middle) bs = state.MiddleButton;
                else if (btn.Button == MouseButton.XButton1) bs = state.XButton1;
                else if (btn.Button == MouseButton.XButton2) bs = state.XButton2;
                else continue;
//    PlayerIndex index = PlayerIndex.One;
//{
//if ((inputMethods & InputMethods.GamePad) == InputMethods.GamePad)
//// Update the state of the active gamepad, if the application supports it.

// Current state button pressed?
                var pressed = (bs == ButtonState.Pressed);
                if (pressed)
                {
// Update the repeat delay timer for the associated button.
                    var ms = gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (pressed) btn.Countdown -= ms;
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
                }
//    }
//        index = Gamer.SignedInGamers[i].PlayerIndex;
//        int i = 0; // Have to be done this way, else it crashes for player other than one
//    {
//    if (Gamer.SignedInGamers.Count > 0 && activePlayer == ActivePlayer.None)
//    // use the index of the first player in the SignedInGamers list.
//    // If gamers are signed in and an active player is not defined,

// Button was just pressed?
                if ((pressed) && (!btn.Pressed))
                {
// "Press" the associated button.
                    btn.Pressed = true;
                    BuildMouseEvent(state, btn.Button, ref e);
//    }
//        index = (PlayerIndex)activePlayer;
//    {
//    else if (activePlayer != ActivePlayer.None)
//    // If an active player is specified, use that player index to update.

// Fire the MouseDown and MousePress events.
                    if (MouseDown != null) MouseDown.Invoke(this, e);
// Fire the repeat MousePress event.
                    if (MousePress != null) MousePress.Invoke(this, e);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
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

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <param name="state">The new state to assign to the gamepad.</param>
        /// <param name="playerIndex">PlayerIndex of the gamepad to update the state of.</param>
        /// </summary>
        /// applicable gamepad events as needed.
        /// Updates the state of the gamepad with the specified player index and raises
        /// <summary>
        private void UpdateGamePad(PlayerIndex playerIndex, GamePadState state, GameTime gameTime)
        {
            var e = new GamePadEventArgs(playerIndex);
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Check for changes in stick and trigger values.
            if (state.ThumbSticks.Left != gamePadState.ThumbSticks.Left ||
                state.ThumbSticks.Right != gamePadState.ThumbSticks.Right ||
                state.Triggers.Left != gamePadState.Triggers.Left ||
                state.Triggers.Right != gamePadState.Triggers.Right)
            {
                BuildGamePadEvent(state, GamePadButton.None, ref e);
                if (GamePadMove != null) GamePadMove.Invoke(this, e);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
            }
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Update the states of the gamepad buttons in the list.
            foreach (var btn in gamePadButtons)
            {
// Current button state of the current gamepad button.
                var bs = ButtonState.Released;
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

                if (btn.Button == GamePadButton.None) continue;
                if (btn.Button == GamePadButton.A) bs = state.Buttons.A;
                else if (btn.Button == GamePadButton.B) bs = state.Buttons.B;
                else if (btn.Button == GamePadButton.Back) bs = state.Buttons.Back;
                else if (btn.Button == GamePadButton.Down) bs = state.DPad.Down;
                else if (btn.Button == GamePadButton.Left) bs = state.DPad.Left;
                else if (btn.Button == GamePadButton.Right) bs = state.DPad.Right;
                else if (btn.Button == GamePadButton.Start) bs = state.Buttons.Start;
                else if (btn.Button == GamePadButton.Up) bs = state.DPad.Up;
                else if (btn.Button == GamePadButton.X) bs = state.Buttons.X;
                else if (btn.Button == GamePadButton.Y) bs = state.Buttons.Y;
                else if (btn.Button == GamePadButton.BigButton) bs = state.Buttons.BigButton;
                else if (btn.Button == GamePadButton.LeftShoulder) bs = state.Buttons.LeftShoulder;
                else if (btn.Button == GamePadButton.RightShoulder) bs = state.Buttons.RightShoulder;
                else if (btn.Button == GamePadButton.LeftStick) bs = state.Buttons.LeftStick;
                else if (btn.Button == GamePadButton.RightStick) bs = state.Buttons.RightStick;
                else bs = GetVectorState(btn.Button, state);
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Current state button pressed?
                var pressed = (bs == ButtonState.Pressed);
                if (pressed)
                {
// Update the repeat delay timer for the associated button.
                    var ms = gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (pressed) btn.Countdown -= ms;
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
                }
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Button was just pressed?
                if ((pressed) && (!btn.Pressed))
                {
// "Press" the associated button.
                    btn.Pressed = true;
                    BuildGamePadEvent(state, btn.Button, ref e);
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Fire the GamePadDown event and the initial GamePadPress event.
                    if (GamePadDown != null) GamePadDown.Invoke(this, e);
// Fire the repeated GamePadPress event.
                    if (GamePadPress != null) GamePadPress.Invoke(this, e);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
                }
// Button was just released?
                else if ((!pressed) && (btn.Pressed))
                {
// "Release" the associated button and reset the repeat delay timer.
                    btn.Pressed = false;
                    btn.Countdown = RepeatDelay;
                    BuildGamePadEvent(state, btn.Button, ref e);
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Fire the GamePadUp event.
                    if (GamePadUp != null) GamePadUp.Invoke(this, e);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
                }
// Button is held down and it's time to fire a repeat press event?
                else if (btn.Pressed && btn.Countdown < 0)
                {
// Update event args and reset timer.
                    e.Button = btn.Button;
                    btn.Countdown = RepeatRate;
                    BuildGamePadEvent(state, btn.Button, ref e);
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Fire the repeated GamePadPress event.
                    if (GamePadPress != null) GamePadPress.Invoke(this, e);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
                }
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
            }
            gamePadState = state;
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <param name="state">Current keyboard state.</param>
        /// </summary>
        /// Updates the state of the keys in the list.
        /// <summary>
        private void UpdateKeys(KeyboardState state, GameTime gameTime)
        {
#if (!XBOX && !XBOX_FAKE)
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            var e = new KeyEventArgs();
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Is CapsLock on?
            e.Caps = (((ushort)NativeMethods.GetKeyState(0x14)) & 0xffff) != 0;
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Check for modifier key presses.
            foreach (var key in state.GetPressedKeys())
            {
                if (key == Keys.LeftAlt || key == Keys.RightAlt) e.Alt = true;
                else if (key == Keys.LeftShift || key == Keys.RightShift) e.Shift = true;
                else if (key == Keys.LeftControl || key == Keys.RightControl) e.Control = true;
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
            }
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Update the rest of the key states.
            foreach (var key in keys)
            {
// Ignore modifier keys, they're already handled.
                if (key.Key == Keys.LeftAlt || key.Key == Keys.RightAlt ||
                    key.Key == Keys.LeftShift || key.Key == Keys.RightShift ||
                    key.Key == Keys.LeftControl || key.Key == Keys.RightControl)
                {
                    continue;
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
                }
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Is the current key state key pressed?
                var pressed = state.IsKeyDown(key.Key);
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Update the repeat delay timer for the associated button.
                var ms = gameTime.ElapsedGameTime.TotalMilliseconds;
                if (pressed) key.Countdown -= ms;
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Key was just pressed?
                if ((pressed) && (!key.Pressed))
                {
// Update the state of the associated key.
                    key.Pressed = true;
                    e.Key = key.Key;
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Fire the KeyDown and initial KeyPress events.
                    if (KeyDown != null) KeyDown.Invoke(this, e);
// Fire the KeyPress event again.
                    if (KeyPress != null) KeyPress.Invoke(this, e);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
                }
// Key was just released?
                else if ((!pressed) && (key.Pressed))
                {
// Update the state of the associated key and reset the repeat delay timer.
                    key.Pressed = false;
                    key.Countdown = RepeatDelay;
                    e.Key = key.Key;
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Fire the KeyUp event.
                    if (KeyUp != null) KeyUp.Invoke(this, e);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
                }
// Key is held down and it's time to fire an additional KeyPress event?
                else if (key.Pressed && key.Countdown < 0)
                {
// Reset the repeat delay timer.
                    key.Countdown = RepeatRate;
                    e.Key = key.Key;
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Fire the KeyPress event again.
                    if (KeyPress != null) KeyPress.Invoke(this, e);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
                }
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
            }
#endif
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <param name="state">Current mouse state.</param>
        /// </summary>
        /// Updates the mouse device.
        /// <summary>
        private void UpdateMouse(MouseState state, GameTime gameTime)
        {
#if (!XBOX && !XBOX_FAKE)
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            if ((state.X != mouseState.X) || (state.Y != mouseState.Y))
            {
                var e = new MouseEventArgs();
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

                var btn = MouseButton.None;
                if (state.LeftButton == ButtonState.Pressed) btn = MouseButton.Left;
                else if (state.RightButton == ButtonState.Pressed) btn = MouseButton.Right;
                else if (state.MiddleButton == ButtonState.Pressed) btn = MouseButton.Middle;
                else if (state.XButton1 == ButtonState.Pressed) btn = MouseButton.XButton1;
                else if (state.XButton2 == ButtonState.Pressed) btn = MouseButton.XButton2;
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

                BuildMouseEvent(state, btn, ref e);
                if (MouseMove != null)
                {
                    MouseMove.Invoke(this, e);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
                }
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
            }
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// Mouse wheel position changed
            if (state.ScrollWheelValue != mouseState.ScrollWheelValue)
            {
                var e = new MouseEventArgs();
                var direction = state.ScrollWheelValue < mouseState.ScrollWheelValue
                    ? MouseScrollDirection.Down
                    : MouseScrollDirection.Up;
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

                BuildMouseEvent(state, MouseButton.None, direction, ref e);
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

                if (MouseScroll != null)
                {
                    MouseScroll.Invoke(this, e);
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
                }
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
            }
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            UpdateButtons(state, gameTime);
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            mouseState = state;
// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

#endif
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
        }

        #region Nested type: Class

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// </summary>
        /// Represents a gamepad button, its state, and repeat delay timer.
        /// <summary>
        private class InputGamePadButton
        {
            /// </summary>
            /// Gamepad button this object represents.
            /// <summary>
            public GamePadButton Button = GamePadButton.None;

            /// </summary>
            /// Timer used to delay firing of repeat key presses.
            /// <summary>
            public double Countdown = RepeatDelay;

            /// </summary>
            /// Indicates if the key is pressed or released.
            /// <summary>
            public bool Pressed;

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            /// </summary>
            /// Creates a default instance of the InputGamePadButton class.
            /// <summary>
            public InputGamePadButton()
            {
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
            }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            /// <param name="button">Button this instance will represent.</param>
            /// </summary>
            /// Creates an InputGamePadButton instance for the specified button.
            /// <summary>
            public InputGamePadButton(GamePadButton button)
            {
                Button = button;
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
            }
        }

        #endregion

        #region Nested type: Class

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// </summary>
        /// Represents a key, its state, and repeat delay timer.
        /// <summary>
        private class InputKey
        {
            /// </summary>
            /// Timer used to delay firing of repeat key presses.
            /// <summary>
            public double Countdown = RepeatDelay;

            /// </summary>
            /// Key that this object represents.
            /// <summary>
            public Keys Key = Keys.None;

            /// </summary>
            /// Indicates if the key is pressed or released.
            /// <summary>
            public bool Pressed;
        }

        #endregion

        #region Nested type: Class

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// </summary>
        /// Represents the state of the mouse device and the current mouse cursor position.
        /// <summary>
        private class InputMouse
        {
            /// </summary>
            /// Current mouse cursor position.
            /// <summary>
            public Point Position = new Point(0, 0);

            /// </summary>
            /// Current mouse state.
            /// <summary>
            public MouseState State = new MouseState();
        }

        #endregion

        #region Nested type: Class

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

        /// </summary>
        /// Represents a mouse button, its state, and repeat delay timer.
        /// <summary>
        private class InputMouseButton
        {
            /// </summary>
            /// The mouse button this object represents.
            /// <summary>
            public MouseButton Button = MouseButton.None;

            /// </summary>
            /// Timer used to delay firing of repeat key presses.
            /// <summary>
            public double Countdown = RepeatDelay;

            /// </summary>
            /// Indicates if the key is pressed or released.
            /// <summary>
            public bool Pressed;

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            /// </summary>
            /// Creates a default instance of the InputMouseButton class.
            /// <summary>
            public InputMouseButton()
            {
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
            }

// return;
//if (manager.UseGuide && Guide.IsVisible)
// Do nothing if the guide can be used and the guide is displayed.

            /// <param name="button">Mouse button this instance will represent.</param>
            /// </summary>
            /// Creates an instance of the InputMouseButton class set for the specified mouse button.
            /// <summary>
            public InputMouseButton(MouseButton button)
            {
                Button = button;
//}
//    UpdateGamePad(index, gs, gameTime);
//    GamePadState gs = GamePad.GetState(index);
            }
        }

        #endregion
    }
}
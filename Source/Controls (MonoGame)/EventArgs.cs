

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace MonoForce.Controls
{


public class EventArgs: System.EventArgs
{


/// </summary>
/// has been handled or not.
/// Indicates if the event the arguments belong with
/// <summary>
public bool Handled = false;



/// </summary>
/// Creates a new EventArgs instance.
/// <summary>
public EventArgs()
{
}

}

public class KeyEventArgs: EventArgs
{

/// </summary>
/// Key for the event.
/// <summary>
public Keys Key = Keys.None;
/// </summary>
/// Indicates if the Control key modifier is pressed.
/// <summary>
public bool Control = false;
/// </summary>
/// Indicates if the Shift key modifier is pressed.
/// <summary>
public bool Shift = false;
/// </summary>
/// Indicates if the Alt key modifier is pressed.
/// <summary>
public bool Alt = false;
/// </summary>
/// Indicates if Caps Lock key is enabled.
/// <summary>
public bool Caps = false;



/// </summary>
/// Creates a new instance of the KeyEventArgs class.
/// <summary>
public KeyEventArgs()
{
}

/// <param name="key">Key argument for the event.</param>
/// </summary>
/// Creates a new instance of the KeyEventArgs class for the specified key.
/// <summary>
public KeyEventArgs(Keys key)
{
Key = key;
Control = false;
Shift = false;
Alt = false;
Caps = false;
}

/// <param name="caps">Indicates if Caps Lock is enabled.</param>
/// <param name="alt">Indicates if the Alt key modifier is pressed.</param>
/// <param name="shift">Indicates if the Shift key modifier is pressed.</param>
/// <param name="control">Indicates if the Control key modifier is pressed.</param>
/// <param name="key">Key argument for the event.</param>
/// </summary>
/// Creates a new instance of the KeyEventArgs class for the specified key and modifiers.
/// <summary>
public KeyEventArgs(Keys key, bool control, bool shift, bool alt, bool caps)
{
Key = key;
Control = control;
Shift = shift;
Alt = alt;
Caps = caps;
}

}

public class MouseEventArgs: EventArgs
{

/// </summary>
/// Mouse state at the time of the event.
/// <summary>
public MouseState State = new MouseState();
/// </summary>
/// Mouse button state at the time of the event.
/// <summary>
public MouseButton Button = MouseButton.None;
/// </summary>
/// Mouse cursor position.
/// <summary>
public Point Position = new Point(0, 0);
/// </summary>
/// Mouse cursor position delta.
/// <summary>
public Point Difference = new Point(0, 0);
/// </summary>
/// Mouse scroll direction
/// <summary>
public MouseScrollDirection ScrollDirection = MouseScrollDirection.None;



public MouseEventArgs()
{
}

public MouseEventArgs(MouseState state, MouseButton button, Point position)
{
State = state;
Button = button;
Position = position;
}

/// <param name="scrollDirection">Mouse scroll direction at the time of the event.</param>
/// <param name="position">Mosue cursor position at the time of the event.</param>
/// <param name="button">Mouse button state at the time of the event.</param>
/// <param name="state">Mouse state at the time of the event.</param>
/// Creates a new initialized instace of the MouseEventArgs class.
/// <summary>
public MouseEventArgs(MouseState state, MouseButton button, Point position, MouseScrollDirection scrollDirection)
: this(state, button, position)
{
ScrollDirection = scrollDirection;
}

public MouseEventArgs(MouseEventArgs e)
: this(e.State, e.Button, e.Position)
{
Difference = e.Difference;
ScrollDirection = e.ScrollDirection;
}


}

/// </summary>
/// Event arguments for gamepad related events.
/// <summary>
public class GamePadEventArgs : EventArgs
{

/// </summary>
/// Index of the player the gamepad is associated with.
/// <summary>
public PlayerIndex PlayerIndex = PlayerIndex.One;
/// </summary>
/// State of the gamepad at the time of the event.
/// <summary>
public GamePadState State = new GamePadState();
/// </summary>
/// Gamepad button pressed for the event arguments.
/// <summary>
public GamePadButton Button = GamePadButton.None;
/// </summary>
/// Values of the gamepad sticks and trigs.
/// <summary>
public GamePadVectors Vectors;



/*
public GamePadEventArgs()
{
}*/

/// <param name="playerIndex">Player index of the gamepad.</param>
/// </summary>
/// Creates a new instance of the GamePadEventArgs class for the specified player.
/// <summary>
public GamePadEventArgs(PlayerIndex playerIndex)
{
PlayerIndex = playerIndex;
}

/// <param name="button">Button pressed for the event.</param>
/// <param name="playerIndex">Player index of the gamepad.</param>
/// </summary>
/// Creates a new instance of the GamePadEventArgs class for the specified player.
/// <summary>
public GamePadEventArgs(PlayerIndex playerIndex, GamePadButton button)
{
PlayerIndex = playerIndex;
Button = button;
}


}

public class DrawEventArgs: EventArgs
{

/// </summary>
/// Rendering object for the draw event.
/// <summary>
public Renderer Renderer = null;
/// </summary>
/// Destination region where drawing will occur.
/// <summary>
public Rectangle Rectangle = Rectangle.Empty;
/// </summary>
/// Snapshot of the application's timing values.
/// <summary>
public GameTime GameTime = null;



/// </summary>
/// Creates a new default instance of the DrawEventArgs class.
/// <summary>
public DrawEventArgs()
{
}

/// <param name="gameTime">Snapshot of the application's timing values.</param>
/// <param name="rectangle">Destination region where drawing will occur.</param>
/// <param name="renderer">Render management object for the event.</param>
/// </summary>
/// Creates an initialized instance of the DrawEventArgs class.
/// <summary>
public DrawEventArgs(Renderer renderer, Rectangle rectangle, GameTime gameTime)
{
Renderer = renderer;
Rectangle = rectangle;
GameTime = gameTime;
}

}

public class ResizeEventArgs: EventArgs
{

/// </summary>
/// New width of the object being resized.
/// <summary>
public int Width = 0;
/// </summary>
/// New height of the object being resized.
/// <summary>
public int Height = 0;
/// </summary>
/// Previous width of the object being resized.
/// <summary>
public int OldWidth = 0;
/// </summary>
/// Previous height of the object being resized.
/// <summary>
public int OldHeight = 0;



/// </summary>
/// Creates a new default instance of the ResizeEventArgs class.
/// <summary>
public ResizeEventArgs()
{
}

/// <param name="oldHeight"></param>
/// <param name="oldWidth"></param>
/// <param name="height"></param>
/// <param name="width"></param>
/// </summary>
/// Creates an initialized instance of the ResizeEventArgs class.
/// <summary>
public ResizeEventArgs(int width, int height, int oldWidth, int oldHeight)
{
Width = width;
Height = height;
OldWidth = oldWidth;
OldHeight = oldHeight;
}

}

public class MoveEventArgs: EventArgs
{

/// </summary>
/// Current X position of the object being moved.
/// <summary>
public int Left = 0;
/// </summary>
/// Current Y position of the object being moved.
/// <summary>
public int Top = 0;
/// </summary>
/// Previous X position of the object being moved.
/// <summary>
public int OldLeft = 0;
/// </summary>
/// Previous Y position of the object being moved.
/// <summary>
public int OldTop = 0;



/// </summary>
/// Creates a default instance of the MoveEventArgs class.
/// <summary>
public MoveEventArgs()
{
}

/// <param name="oldTop">Previous Y position of the object being moved.</param>
/// <param name="oldLeft">Previous X position of the object being moved.</param>
/// <param name="top">Current Y position of the object being moved.</param>
/// <param name="left">Current X position of the object being moved.</param>
/// </summary>
/// Creates an initialized instance of the MoveEventArgs class.
/// <summary>
public MoveEventArgs(int left, int top, int oldLeft, int oldTop)
{
Left = left;
Top = top;
OldLeft = oldLeft;
OldTop = oldTop;
}

}

public class DeviceEventArgs: EventArgs
{

/// </summary>
/// Arguments for the graphics manager PreparingDeviceSettings event.
/// <summary>
public PreparingDeviceSettingsEventArgs DeviceSettings = null;



/// </summary>
/// Creates a default instance of the DeviceEventArgs class.
/// <summary>
public DeviceEventArgs()
{
}

/// <param name="deviceSettings">Arguments for the graphics manager PreparingDeviceSettings event.</param>
/// </summary>
/// Creates an initialized instance of the DeviceEventArgs class.
/// <summary>
public DeviceEventArgs(PreparingDeviceSettingsEventArgs deviceSettings)
{
DeviceSettings = deviceSettings;
}

}

public class WindowClosingEventArgs: EventArgs
{

/// </summary>
/// Indicates if the window closing operation should be canceled.
/// <summary>
public bool Cancel = false;



/// </summary>
/// Creates a new instance of the WindowClosingEventArgs class.
/// <summary>
public WindowClosingEventArgs()
{
}

}

public class WindowClosedEventArgs: EventArgs
{

/// </summary>
/// Indicates if the unmanaged window resources should be released.
/// <summary>
public bool Dispose = false;



/// </summary>
/// Creates a new instance of the WindowClosedEventArgs class.
/// <summary>
public WindowClosedEventArgs()
{
}

}

/// </summary>
/// Event arguments for console message events.
/// <summary>
public class ConsoleMessageEventArgs : EventArgs
{

/// </summary>
/// Console message for this event.
/// <summary>
public ConsoleMessage Message;



/// </summary>
/// Creates a default instance of the ConsoleMessageEventArgs class.
/// <summary>
public ConsoleMessageEventArgs()
{
}

/// <param name="message">Console message for the event.</param>
/// </summary>
/// Creates an initialized instance of the ConsoleMessageEventArgs class.
/// <summary>
public ConsoleMessageEventArgs(ConsoleMessage message)
{
Message = message;
}

}



}

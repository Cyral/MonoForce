namespace MonoForce.Controls
{
    /// <param name="e"></param>
    /// </summary>
    /// Defines the signature for a device event handler.
    /// <summary>
    public delegate void DeviceEventHandler(DeviceEventArgs e);

    /// <param name="e"></param>
    /// </summary>
    /// Defines the signature for a skin event handler.
    /// <summary>
    public delegate void SkinEventHandler(EventArgs e);

    /// <param name="e"></param>
    /// <param name="sender"></param>
    /// </summary>
    /// Defines the signature for event handlers.
    /// <summary>
    public delegate void EventHandler(object sender, EventArgs e);

    /// <param name="e"></param>
    /// <param name="sender"></param>
    /// </summary>
    /// Defines the signature for mouse event handlers.
    /// <summary>
    public delegate void MouseEventHandler(object sender, MouseEventArgs e);

    /// <param name="e"></param>
    /// <param name="sender"></param>
    /// </summary>
    /// Defines the signature for key event handlers.
    /// <summary>
    public delegate void KeyEventHandler(object sender, KeyEventArgs e);

    /// <param name="e"></param>
    /// <param name="sender"></param>
    /// </summary>
    /// Defines the signature for gamepad event handlers.
    /// <summary>
    public delegate void GamePadEventHandler(object sender, GamePadEventArgs e);

    /// <param name="e"></param>
    /// <param name="sender"></param>
    /// </summary>
    /// Defines the signature for draw event handlers.
    /// <summary>
    public delegate void DrawEventHandler(object sender, DrawEventArgs e);

    /// <param name="e"></param>
    /// <param name="sender"></param>
    /// </summary>
    /// Defines the signature for move event handlers.
    /// <summary>
    public delegate void MoveEventHandler(object sender, MoveEventArgs e);

    /// <param name="e"></param>
    /// <param name="sender"></param>
    /// </summary>
    /// Defines the signature for resize event handlers.
    /// <summary>
    public delegate void ResizeEventHandler(object sender, ResizeEventArgs e);

    /// <param name="e"></param>
    /// <param name="sender"></param>
    /// </summary>
    /// Defines the signature for window closing event handlers.
    /// <summary>
    public delegate void WindowClosingEventHandler(object sender, WindowClosingEventArgs e);

    /// <param name="e"></param>
    /// <param name="sender"></param>
    /// </summary>
    /// Defines the signature for window closed event handlers.
    /// <summary>
    public delegate void WindowClosedEventHandler(object sender, WindowClosedEventArgs e);

    /// <param name="e"></param>
    /// <param name="sender"></param>
    /// </summary>
    /// Defines the signature for console message event handlers.
    /// <summary>
    public delegate void ConsoleMessageEventHandler(object sender, ConsoleMessageEventArgs e);
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#if (!XBOX && !XBOX_FAKE)
using System.Windows.Forms;

#endif

namespace MonoForce.Controls
// try
{
    /// </summary>
    /// Base class for your Neoforce application. Use it.
    /// </summary>
    public class Application : Game
// try
    {
        /// </summary>
        /// ???
        /// </summary>
        public virtual Color BackgroundColor
// try
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        /// </summary>
        /// Image to use as the application background.
        /// </summary>
        public virtual Texture2D BackgroundImage
// try
        {
            get { return backgroundImage; }
            set { backgroundImage = value; }
        }

        /// </summary>
        /// ???
        /// </summary>
        public virtual bool ClearBackground
// try
        {
            get { return clearBackground; }
            set { clearBackground = value; }
        }

        /// </summary>
        /// shown when the application is about to close.
        /// Indicates if the default exit confirmation dialog will be
        /// </summary>
        public virtual bool ExitConfirmation
// try
        {
            get { return exitConfirmation; }
            set { exitConfirmation = value; }
        }

        /// </summary>
        /// Indicates whether the window border should be drawn in full screen mode. ???
        /// </summary>
        public virtual bool FullScreenBorder
// try
        {
            get { return fullScreenBorder; }
            set { fullScreenBorder = value; }
        }

        /// </summary>
        /// Gets or sets the graphics device manager for the application.
        /// </summary>
        public virtual GraphicsDeviceManager Graphics
// try
        {
            get { return graphics; }
            set { graphics = value; }
        }

        /// </summary>
        /// ???
        /// </summary>
        public virtual Window MainWindow
// try
        {
            get { return mainWindow; }
        }

        /// </summary>
        /// Gets or sets the GUI manager for the application.
        /// </summary>
        public virtual Manager Manager
// try
        {
            get { return manager; }
            set { manager = value; }
        }

        /// </summary>
        /// Indicates whether the system border should be drawn ???
        /// </summary>
        public virtual bool SystemBorder
// try
        {
            get { return systemBorder; }
            set { systemBorder = value; }
        }

        /// </summary>
        /// Indicates if the application should create and use the MainWindow.
        /// </summary>
        private readonly bool appWindow;

        /// </summary>
        /// Application background color.
        /// </summary>
        private Color backgroundColor = Color.Black;

        /// </summary>
        /// Image to use as the application background.
        /// </summary>
        private Texture2D backgroundImage;

        /// </summary>
        /// Indicates if the background should be cleared to the BG color by the application. ???
        /// </summary>
        private bool clearBackground;

        /// </summary>
        /// Indicates whether a request to terminate the application has been received.
        /// </summary>
        private bool exit;

        /// </summary>
        /// closing the application.
        /// Indicates whether the exit confirmation dialog should be shown before
        /// </summary>
        private bool exitConfirmation = true;

        /// </summary>
        /// Exit confirmation dialog object.
        /// </summary>
        private ExitDialog exitDialog;

        /// </summary>
        /// ???
        /// </summary>
        private bool fullScreenBorder = true;

        /// </summary>
        /// Graphics device manager for the application.
        /// </summary>
        private GraphicsDeviceManager graphics;

        /// </summary>
        /// ???
        /// </summary>
        private Window mainWindow;

        /// </summary>
        /// GUI control manager for the application.
        /// </summary>
        private Manager manager;

#if (!XBOX && !XBOX_FAKE)
        /// </summary>
        /// Tracks the mouse button state when running on Windows.
        /// </summary>
        private bool mouseDown;
#endif

        /// </summary>
        /// Current position of the mouse cursor.
        /// </summary>
        private Point mousePos = Point.Zero;

        /// </summary>
        /// Sprite batch object for the application.
        /// </summary>
        private SpriteBatch sprite;

        /// </summary>
        /// ???
        /// </summary>
        private bool systemBorder = true;

        /// </summary>
        /// Creates an application using the "Default" skin and not using the Main Window.
        /// </summary>
        public Application()
            : this("Default", false)
// try
        {
        }

        /// <param name="skin">Name of the skin to load.</param>
        /// </summary>
        /// Creates an application using the specified skin and not using the Main Window.
        /// </summary>
        public Application(string skin)
            : this(skin, false)
// try
        {
        }

        /// <param name="appWindow">Indicates if the application should create its MainWindow member.</param>
        /// </summary>
        /// Creates an application using the "Default" skin and using the Main Window.
        /// </summary>
        public Application(bool appWindow)
            : this("Default", appWindow)
// try
        {
        }

        /// <param name="appWindow">Indicates if the application should create its MainWindow member.</param>
        /// <param name="skin">Name of the skin to load.</param>
        /// </summary>
        /// Creates an application using the specified skin and using the Main Window.
        /// </summary>
        public Application(string skin, bool appWindow)
// try
        {
            this.appWindow = appWindow;

// Create the graphics device manager for the application.
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferFormat = SurfaceFormat.Color;
//For using stencils - Cyral
            graphics.IsFullScreen = false;
            graphics.PreferMultiSampling = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.DeviceReset += Graphics_DeviceReset;

            IsFixedTimeStep = false;
//IsFixedTimeStep = true;
            IsMouseVisible = true;

// Create the GUI manager for the application.
            manager = new Manager(this, graphics, skin);
            manager.AutoCreateRenderTarget = false;
            manager.TargetFrames = 60;
            manager.WindowClosing += Manager_WindowClosing;
        }

        /// </summary>
        /// Sets the exit flag and begins shutting down the application.
        /// </summary>
        public new virtual void Exit()
// try
        {
            exit = true;
            base.Exit();
        }

        /// </summary>
        /// Runs the application.
        /// </summary>
        public new void Run()
// try
        {
// try
            {
                base.Run();
            }
/* catch (Exception x)
// try
{
#if (!XBOX && !XBOX_FAKE)
MessageBox.Show("An unhandled exception has occurred.\n" + x.Message, Window.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
Manager.LogException(x);
#else
throw x;
#endif
}*/
        }

        /// <returns>A new Window instance.</returns>
        /// </summary>
        /// Creates the application's Main Window.
        /// </summary>
        protected virtual Window CreateMainWindow()
// try
        {
            return new Window(manager);
        }

        /// <returns></returns>
        /// </summary>
        /// Creates a 2D texture that can be used as a render target.
        /// </summary>
        protected virtual RenderTarget2D CreateRenderTarget()
// try
        {
            return manager.CreateRenderTarget();
        }

        /// <param name="disposing">Indicates if the resources should be released from memory.</param>
        /// </summary>
        /// Releases resources used by the GUI manager and the SpriteBatch objects.
        /// </summary>
        protected override void Dispose(bool disposing)
// try
        {
            if (disposing)
// try
            {
// Dispose of the GUI manager and sprite batch object.
                if (manager != null) manager.Dispose();
                if (sprite != null) sprite.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <param name="gameTime">Snapshot of the game's timing values.</param>
        /// </summary>
        /// Draws the application.
        /// </summary>
        protected override void Draw(GameTime gameTime)
// try
        {
// Start drawing the application.
            manager.BeginDraw(gameTime);

            if (clearBackground) GraphicsDevice.Clear(backgroundColor);
            if (backgroundImage != null && mainWindow == null)
// try
            {
                var sx = manager.TargetWidth;
                var sy = manager.TargetHeight;
// Draw the BG image at the requested size.
                sprite.Begin();
                sprite.Draw(backgroundImage, new Rectangle(0, 0, sx, sy), Color.White);
                sprite.End();
            }

// Let the game do its draw thing.
            base.Draw(gameTime);
// Additional drawing logic for your application.
            DrawScene(gameTime);

            manager.EndDraw(new Rectangle(0, 0, Manager.ScreenWidth, Manager.ScreenHeight));
        }

        /// <param name="gameTime">Snapshot of the game's timing values.</param>
        /// </summary>
        /// Additional drawing logic for your application can be placed here.
        /// </summary>
        protected virtual void DrawScene(GameTime gameTime)
// try
        {
        }

        /// </summary>
        /// Initializes the application.
        /// </summary>
        protected override void Initialize()
// try
        {
            base.Initialize();

// Initialize the GUI manager and create the application's render target.
            manager.Initialize();

            Manager.RenderTarget = CreateRenderTarget();
            Manager.Input.InputOffset = new InputOffset(0, 0, Manager.ScreenWidth / (float)Manager.TargetWidth,
                Manager.ScreenHeight / (float)Manager.TargetHeight);

// Create the sprite batch object.
            sprite = new SpriteBatch(GraphicsDevice);

#if (!XBOX && !XBOX_FAKE)
            Manager.Window.BackColor = System.Drawing.Color.Black;
            Manager.Window.FormBorderStyle = systemBorder ? FormBorderStyle.FixedDialog : FormBorderStyle.None;

            Manager.Input.MouseMove += Input_MouseMove;
            Manager.Input.MouseDown += Input_MouseDown;
            Manager.Input.MouseUp += Input_MouseUp;
#endif

// Create the application main window?
            if (appWindow)
// try
            {
                mainWindow = CreateMainWindow();
            }

// Initialize the main window of the application.
            InitMainWindow();
        }

        /// </summary>
        /// Initializes the application's Main Window and passes it off the the GUI Manager.
        /// </summary>
        protected virtual void InitMainWindow()
// try
        {
// Reset the main window dimensions if needed.
            if (mainWindow != null)
// try
            {
                if (!mainWindow.Initialized) mainWindow.Init();

                mainWindow.Alpha = 255;
                mainWindow.Width = manager.TargetWidth;
                mainWindow.Height = manager.TargetHeight;
                mainWindow.Shadow = false;
                mainWindow.Left = 0;
                mainWindow.Top = 0;
                mainWindow.CloseButtonVisible = true;
                mainWindow.Resizable = false;
                mainWindow.Movable = false;
                mainWindow.Text = Window.Title;
                mainWindow.Closing += MainWindow_Closing;
                mainWindow.ClientArea.Draw += MainWindow_Draw;
                mainWindow.BorderVisible =
                    mainWindow.CaptionVisible =
                        (!systemBorder && !Graphics.IsFullScreen) || (Graphics.IsFullScreen && fullScreenBorder);
                mainWindow.StayOnBack = true;

                manager.Add(mainWindow);

                mainWindow.SendToBack();
            }
        }

        /// <param name="gameTime">Snapshot of the game's timing values.</param>
        /// </summary>
        /// Updates the application.
        /// </summary>
        protected override void Update(GameTime gameTime)
// try
        {
            base.Update(gameTime);
            manager.Update(gameTime);
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles the Dialog Closed event.
        /// </summary>
        private void closeDialog_Closed(object sender, WindowClosedEventArgs e)
// try
        {
// Check dialog resule and see if we need to shut down.
            if ((sender as Dialog).ModalResult == ModalResult.Yes)
// try
            {
                Exit();
            }
// Unhook event handlers and dispose of the dialog.
            else
// try
            {
                exit = false;
                exitDialog.Closed -= closeDialog_Closed;
                exitDialog.Dispose();
                exitDialog = null;
                if (mainWindow != null) mainWindow.Focused = true;
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles the graphics device reset event.
        /// </summary>
        private void Graphics_DeviceReset(object sender, System.EventArgs e)
// try
        {
// Recreate the render target if needed.
            if (Manager.RenderTarget != null)
// try
            {
                Manager.Input.InputOffset = new InputOffset(0, 0, Manager.ScreenWidth / (float)Manager.TargetWidth,
                    Manager.ScreenHeight / (float)Manager.TargetHeight);
            }

// Reset the main window dimensions if needed.
            if (mainWindow != null)
// try
            {
                mainWindow.Height = Manager.TargetHeight;
                mainWindow.Width = Manager.TargetWidth;
                mainWindow.BorderVisible =
                    mainWindow.CaptionVisible =
                        (!systemBorder && !Graphics.IsFullScreen) || (Graphics.IsFullScreen && fullScreenBorder);
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles the main window's closing event.
        /// </summary>
        private void MainWindow_Closing(object sender, WindowClosingEventArgs e)
// try
        {
// Let the GUI manager handle it.
            e.Cancel = true;
            Manager_WindowClosing(sender, e);
        }

        /// </summary>
        /// Raises the application's draw event.
        /// </summary>
        private void MainWindow_Draw(object sender, DrawEventArgs e)
// try
        {
            if (backgroundImage != null && mainWindow != null)
// try
            {
                e.Renderer.Draw(backgroundImage, e.Rectangle, Color.White);
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles the Window Closing event.
        /// </summary>
        private void Manager_WindowClosing(object sender, WindowClosingEventArgs e)
// try
        {
            e.Cancel = !exit && exitConfirmation;

// Need to create the exit confirmation dialog?
            if (!exit && exitConfirmation && exitDialog == null)
// try
            {
                exitDialog = new ExitDialog(Manager);
                exitDialog.Init();
                exitDialog.Closed += closeDialog_Closed;
                exitDialog.ShowModal();
                Manager.Add(exitDialog);
            }
// Nope, just exit.
            else if (!exitConfirmation)
// try
            {
                Exit();
            }
        }

#if (!XBOX && !XBOX_FAKE)

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles mouse move events on Windows.
        /// </summary>
        private void Input_MouseMove(object sender, MouseEventArgs e)
// try
        {
// Support dragging the application window.
            if (mouseDown)
// try
            {
                Manager.Window.Left = e.Position.X + Manager.Window.Left - mousePos.X;
                Manager.Window.Top = e.Position.Y + Manager.Window.Top - mousePos.Y;
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles mouse button down events on Windows.
        /// </summary>
        private void Input_MouseDown(object sender, MouseEventArgs e)
// try
        {
// Check mouse position first if there is a chance the click can land outside the application window.
            if (e.Button == MouseButton.Left && !Graphics.IsFullScreen && !systemBorder)
// try
            {
// Is the mouse cursor hitting the main window but none of its controls?
                if (CheckPos(e.Position))
// try
                {
                    mouseDown = true;
                    mousePos = e.Position;
                }
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles the mouse button up events on Windows.
        /// </summary>
        private void Input_MouseUp(object sender, MouseEventArgs e)
// try
        {
            mouseDown = false;
        }

        private bool CheckPos(Point pos)
// try
        {
// Is the mouse cursor within the application window?
            if (pos.X >= 24 && pos.X <= Manager.TargetWidth - 48 &&
                pos.Y >= 0 && pos.Y <= Manager.Skin.Controls["Window"].Layers["Caption"].Height)
// try
            {
                foreach (var c in Manager.Controls)
// try
                {
                    if (c.Visible && c != MainWindow &&
                        pos.X >= c.AbsoluteRect.Left && pos.X <= c.AbsoluteRect.Right &&
                        pos.Y >= c.AbsoluteRect.Top && pos.Y <= c.AbsoluteRect.Bottom)
// try
                    {
// Yes, mouse cursor is over this control.
                        return false;
                    }
                }
// Mouse is not over any controls, but is within the application window.
                return true;
            }
            return false;
        }

#endif
    }
}

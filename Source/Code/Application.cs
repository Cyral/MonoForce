#region Using
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoForce.Controls;
using System;

#if (!XBOX && !XBOX_FAKE)
using System.Windows.Forms;
#endif
#endregion

namespace MonoForce.Controls
{
    /// <summary>
    /// Base class for developing a MonoForce application.
    /// Use it in place of Game in YourGame : Game (YourGame : Application)
    /// </summary>
    public class Application : Game
    {

        #region Properties
        /// <summary>
        /// Gets or sets the graphics device manager for the application.
        /// </summary>
        public virtual GraphicsDeviceManager Graphics { get; set; }

        /// <summary>
        /// Gets or sets the GUI manager for the application. 
        /// </summary>
        public virtual Manager Manager { get; set; }

        /// <summary>
        /// Gets or sets if the graphics device should be cleared each frame
        /// </summary>
        public virtual bool ClearBackground { get; set; }

        /// <summary>
        /// Color to clear the graphics device with
        /// </summary>
        public virtual Color BackgroundColor { get; set; }

        /// <summary>
        /// Image to use as the application background.
        /// </summary>
        public virtual Texture2D BackgroundImage { get; set; }

        /// <summary>
        /// The main window that is tied to the default MonoGame window
        /// Override CreateMainWindow to set this
        /// </summary>
        public virtual Window MainWindow { get; private set; }

        /// <summary>
        /// Indicates whether the system border should be drawn
        /// </summary>
        public virtual bool SystemBorder { get; set; }

        /// <summary>
        /// Indicates whether the window border should be drawn in full screen mode. ???
        /// </summary>
        public virtual bool FullScreenBorder { get; set; }

        /// <summary>
        /// Indicates if the default exit confirmation dialog will be 
        /// shown when the application is about to close.
        /// </summary>
        public virtual bool ExitConfirmation { get; set; }

        /// <summary>
        /// Message to display on exit confirmation, leave as "" for default
        /// </summary>
        public virtual string ExitMessage { get; set; }

        #endregion

        #region Fields
        /// <summary>
        /// Sprite batch object for the application. 
        /// </summary>
        private SpriteBatch sprite;

        /// <summary>
        /// Indicates if the application should create and use the MainWindow. 
        /// </summary>
        private bool appWindow = false;

        /// <summary>
        /// Current position of the mouse cursor.
        /// </summary>
        private Point mousePos = Point.Zero;

        /// <summary>
        /// Tracks the mouse button state
        /// </summary>
        private bool mouseDown = false;

        /// <summary>
        /// Indicates whether a request to terminate the application has been received.
        /// </summary>
        private bool exit = false;

        /// <summary>
        /// Exit confirmation dialog object.
        /// </summary>
        private ExitDialog exitDialog = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an application using the "Default" skin and not using the Main Window.
        /// </summary>
        public Application()
            : this("Default", false)
        {
        }

        /// <summary>
        /// Creates an application using the specified skin and not using the Main Window.
        /// </summary>
        /// <param name="skin">Name of the skin to load.</param>
        public Application(string skin)
            : this(skin, false)
        {
        }

        /// <summary>
        /// Creates an application using the "Default" skin and using the Main Window.
        /// </summary>
        /// <param name="appWindow">Indicates if the application should create its MainWindow member.</param>
        public Application(bool appWindow)
            : this("Default", appWindow)
        {
        }

        /// <summary>
        /// Creates an application using the specified skin and using the Main Window.
        /// </summary>
        /// <param name="skin">Name of the skin to load.</param>
        /// <param name="appWindow">Indicates if the application should create its MainWindow member.</param>
        public Application(string skin, bool appWindow)
        {
            this.appWindow = appWindow;

            // Create the graphics device manager for the application.
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = 1024;
            Graphics.PreferredBackBufferHeight = 768;
            Graphics.PreferredBackBufferFormat = SurfaceFormat.Color;
            Graphics.IsFullScreen = false;
            Graphics.PreferMultiSampling = false;
            Graphics.SynchronizeWithVerticalRetrace = false;
            Graphics.DeviceReset += new EventHandler<System.EventArgs>(Graphics_DeviceReset);

            BackgroundColor = Color.Black;

            //IsFixedTimeStep = true;
            IsMouseVisible = true;

            // Create the GUI manager for the application.
            Manager = new Manager(this, Graphics, skin);
            Manager.AutoCreateRenderTarget = false;
            Manager.TargetFrames = 60;
            Manager.WindowClosing += new WindowClosingEventHandler(Manager_WindowClosing);
        }

        #endregion

        #region Destructors

        /// <summary>
        /// Releases resources used by the GUI manager and the SpriteBatch objects.
        /// </summary>
        /// <param name="disposing">Indicates if the resources should be released from memory.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose of the GUI manager and sprite batch object.
                if (Manager != null) Manager.Dispose();
                if (sprite != null) sprite.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the application.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            // Initialize the GUI manager and create the application's render target.
            Manager.Initialize();
            Manager.RenderTarget = CreateRenderTarget();
            Manager.Input.InputOffset = new InputOffset(0, 0, Manager.ScreenWidth / (float)Manager.TargetWidth, Manager.ScreenHeight / (float)Manager.TargetHeight);

            // Create the sprite batch object.
            sprite = new SpriteBatch(GraphicsDevice);

            //TODO: Mono compatibility
            Manager.Window.BackColor = System.Drawing.Color.Black;
            Manager.Window.FormBorderStyle = SystemBorder ? System.Windows.Forms.FormBorderStyle.FixedDialog : System.Windows.Forms.FormBorderStyle.None;

            // Wire up the mouse event handlers if running on Windows.
            Manager.Input.MouseMove += new MouseEventHandler(Input_MouseMove);
            Manager.Input.MouseDown += new MouseEventHandler(Input_MouseDown);
            Manager.Input.MouseUp += new MouseEventHandler(Input_MouseUp);

            // Create the application main window?
            if (appWindow)
            {
                MainWindow = CreateMainWindow();
            }

            // Initialize the main window of the application.
            InitMainWindow();
        }

        /// <summary>
        /// Updates the application.
        /// </summary>
        /// <param name="gameTime">Snapshot of the game's timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Manager.Update(gameTime);
        }

        /// <summary>
        /// Draws the application.
        /// </summary>
        /// <param name="gameTime">Snapshot of the game's timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Start drawing the application.
            Manager.BeginDraw(gameTime);

            // Should the application clear the backbuffer?
            if (ClearBackground)
            {
                GraphicsDevice.Clear(Color.Black);

            }

            // BG image and no main window.
            if (BackgroundImage != null && MainWindow == null)
            {
                // Draw the BG image at the requested size. 
                sprite.Begin();
                sprite.Draw(BackgroundImage, Vector2.Zero);
                sprite.End();
            }


            // Let the game do its draw thing.
            base.Draw(gameTime);

            // Additional drawing logic for your application.
            DrawScene(gameTime);

            Manager.EndDraw(new Rectangle(0, 0, Manager.ScreenWidth, Manager.ScreenHeight));
        }

        /// <summary>
        /// Creates the application's Main Window.
        /// </summary>
        /// <returns>A new Window instance.</returns>
        protected virtual Window CreateMainWindow()
        {
            return new Window(Manager);
        }

        /// <summary>
        /// Initializes the application's Main Window and passes it off the the GUI Manager.
        /// </summary>
        protected virtual void InitMainWindow()
        {
            if (MainWindow != null)
            {
                if (!MainWindow.Initialized) MainWindow.Init();

                MainWindow.Alpha = 255;
                MainWindow.Width = Manager.TargetWidth;
                MainWindow.Height = Manager.TargetHeight;
                MainWindow.Shadow = false;
                MainWindow.Left = 0;
                MainWindow.Top = 0;
                MainWindow.CloseButtonVisible = true;
                MainWindow.Resizable = false;
                MainWindow.Movable = false;
                MainWindow.Text = this.Window.Title;
                MainWindow.Closing += new WindowClosingEventHandler(MainWindow_Closing);
                MainWindow.ClientArea.Draw += new DrawEventHandler(MainWindow_Draw);
                MainWindow.BorderVisible = MainWindow.CaptionVisible = (!SystemBorder && !Graphics.IsFullScreen) || (Graphics.IsFullScreen && FullScreenBorder);
                MainWindow.StayOnBack = true;

                Manager.Add(MainWindow);

                MainWindow.SendToBack();
            }
        }

        /// <summary>
        /// Raises the application's draw event.
        /// </summary>
        private void MainWindow_Draw(object sender, DrawEventArgs e)
        {
            if (BackgroundImage != null && MainWindow != null)
            {
                e.Renderer.Draw(BackgroundImage, e.Rectangle, Color.White);
            }
        }

        /// <summary>
        /// Sets the exit flag and begins shutting down the application.
        /// </summary>
        public new virtual void Exit()
        {
            exit = true;
            base.Exit();
        }

        /// <summary>
        /// Handles the Window Closing event. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Manager_WindowClosing(object sender, WindowClosingEventArgs e)
        {
            e.Cancel = !exit && ExitConfirmation;

            // Need to create the exit confirmation dialog?
            if (!exit && ExitConfirmation && exitDialog == null)
            {
                exitDialog = new ExitDialog(Manager, ExitMessage);
                exitDialog.Init();
                exitDialog.Closed += new WindowClosedEventHandler(closeDialog_Closed);
                exitDialog.ShowModal();
                Manager.Add(exitDialog);
            }
            // If not, just exit.
            else if (!ExitConfirmation)
            {
                Exit();
            }
        }

        private void closeDialog_Closed(object sender, WindowClosedEventArgs e)
        {
            if ((sender as Dialog).ModalResult == ModalResult.Yes)
            {
                Exit();
            }
            else
            {
                exit = false;
                exitDialog.Closed -= closeDialog_Closed;
                exitDialog.Dispose();
                exitDialog = null;
                if (MainWindow != null) MainWindow.Focused = true;
            }
        }

        /// <summary>
        /// Handles the main window's closing event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, WindowClosingEventArgs e)
        {
            // Let the GUI manager handle it.
            e.Cancel = true;
            Manager_WindowClosing(sender, e);
        }

        /// <summary>
        /// Handles the graphics device reset event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graphics_DeviceReset(object sender, System.EventArgs e)
        {
            // Recreate the render target if needed.
            if (Manager.RenderTarget != null)
            {
                Manager.RenderTarget.Dispose();
                Manager.RenderTarget = CreateRenderTarget();
                Manager.Input.InputOffset = new InputOffset(0, 0, Manager.ScreenWidth / (float)Manager.TargetWidth, Manager.ScreenHeight / (float)Manager.TargetHeight);
            }

            // Reset the main window dimensions if needed.
            if (MainWindow != null)
            {
                MainWindow.Height = Manager.TargetHeight;
                MainWindow.Width = Manager.TargetWidth;
                MainWindow.BorderVisible = MainWindow.CaptionVisible = (!SystemBorder && !Graphics.IsFullScreen) || (Graphics.IsFullScreen && FullScreenBorder);
            }
        }

        /// <summary>
        /// Creates a 2D texture that can be used as a render target.
        /// </summary>
        /// <returns></returns>
        protected virtual RenderTarget2D CreateRenderTarget()
        {
            return Manager.CreateRenderTarget();
        }

        /// <summary>
        /// Additional drawing logic for your application can be placed here.
        /// Use this to draw your game under the MonoForce UI
        /// </summary>
        /// <param name="gameTime">Snapshot of the game's timing values.</param>
        protected virtual void DrawScene(GameTime gameTime)
        {

        }

        /// <summary>
        /// Runs the application.
        /// </summary>
        public new void Run()
        {
            // try
            {
                base.Run();
            }
            /* catch (Exception x)
             {
              #if (!XBOX && !XBOX_FAKE)         
                MessageBox.Show("An unhandled exception has occurred.\n" + x.Message, Window.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Manager.LogException(x);
              #else
                throw x;
              #endif y
             }*/
        }

        /// <summary>
        /// Handles mouse move events on Windows.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Input_MouseMove(object sender, MouseEventArgs e)
        {
            // Support dragging the application window.
            if (mouseDown)
            {
                Manager.Window.Left = e.Position.X + Manager.Window.Left - mousePos.X;
                Manager.Window.Top = e.Position.Y + Manager.Window.Top - mousePos.Y;
            }
        }

        /// <summary>
        /// Handles mouse button down events on Windows.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Input_MouseDown(object sender, MouseEventArgs e)
        {
            // Check mouse position first if there is a chance the click can land outside the application window.
            if (e.Button == MouseButton.Left && !Graphics.IsFullScreen && !SystemBorder)
            {
                // Is the mouse cursor hitting the main window but none of its controls?
                if (CheckPos(e.Position))
                {
                    mouseDown = true;
                    mousePos = e.Position;
                }
            }
        }

        /// <summary>
        /// Handles the mouse button up events on Windows.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Input_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        /// <summary>
        /// Determines whether the mouse cursor is over the application Main Window (true) or
        /// if the mouse cursor is outside of the window or hovering a window control. (false)
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool CheckPos(Point pos)
        {
            // Is the mouse cursor within the application window?
            if (pos.X >= 24 && pos.X <= Manager.TargetWidth - 48 &&
                pos.Y >= 0 && pos.Y <= Manager.Skin.Controls["Window"].Layers["Caption"].Height)
            {
                foreach (Control c in Manager.Controls)
                {
                    // Is this a visible control other than the MainWindow?
                    // Is the mouse cursor within this control's boundaries?
                    if (c.Visible && c != MainWindow && c != MainWindow &&
                        pos.X >= c.AbsoluteRect.Left && pos.X <= c.AbsoluteRect.Right &&
                        pos.Y >= c.AbsoluteRect.Top && pos.Y <= c.AbsoluteRect.Bottom && !(c is Window))
                    {
                        // Yes, mouse cursor is over this control.
                        return false;
                    }
                }
                // Mouse is not over any controls, but is within the application window.
                return true;
            }

            else
            {
                // Mouse is outside of the application window.
                return false;
            }
        }

        #endregion

    }
}

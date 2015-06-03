using System.Windows.Forms;
using Microsoft.Xna.Framework;
using MonoForce.Controls;

namespace MonoForce.Demo
{
    /// <summary>
    /// MonoForce Demo (MonoGame version)
    /// </summary>
    public sealed class Central : Game
    {
        public static long Frames { get; set; }
        public static GraphicsDeviceManager Graphics { get; private set; }
        public MainWindow MainWindow { get; set; }
        public Manager Manager { get; }

        private int afps;
        private double et;
        private int fps;

        public Central()
        {
            Graphics = new GraphicsDeviceManager(this) {SynchronizeWithVerticalRetrace = false};

            IsFixedTimeStep = false;

            Manager = new Manager(this, "Default")
            {
                AutoCreateRenderTarget = true,
                LogUnhandledExceptions = false,
                ShowSoftwareCursor = true
            };
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            Graphics.PreferredBackBufferWidth = 1024;
            Graphics.PreferredBackBufferHeight = 768;
            Graphics.ApplyChanges();

            //Initialize MonoForce after loading skins.
            Manager.Initialize();

            //Create the main window for all content to be added to.
            MainWindow = new MainWindow(Manager);
            MainWindow.Init();

            //To emulate behavior of the Application class in the XNA version, create a full size window.
            MainWindow.ClearBackground = true;
            MainWindow.Resizable = false;
            MainWindow.Movable = false;
            MainWindow.CanFocus = false;
            MainWindow.StayOnBack = true;
            MainWindow.Left = MainWindow.Top = 0;
            MainWindow.Width = 1024;
            MainWindow.Height = 768;
            Manager.Add(MainWindow);
            MainWindow.SendToBack();
        }

        /// <summary>
        /// Update the game and UI.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            Manager.Update(gameTime);
            UpdateStats(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the game and UI.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            Frames += 1;

            Manager.BeginDraw(gameTime);
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            Manager.EndDraw();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Updates FPS, object count, etc.
        /// </summary>
        private void UpdateStats(GameTime time)
        {
            if (et >= 500 || et == 0)
            {
                if (MainWindow != null)
                {
                    MainWindow.lblObjects.Text = "Objects: " + Disposable.Count;
                    MainWindow.lblAvgFps.Text = "Average FPS: " + afps;
                    MainWindow.lblFps.Text = "Current FPS: " + fps;
                }

                if (time.TotalGameTime.TotalSeconds != 0)
                    afps = (int)(Frames / time.TotalGameTime.TotalSeconds);
                if (time.ElapsedGameTime.TotalMilliseconds != 0)
                    fps = (int)(1000 / time.ElapsedGameTime.TotalMilliseconds);

                et = 1;
            }
            et += time.ElapsedGameTime.TotalMilliseconds;
        }
    }
}
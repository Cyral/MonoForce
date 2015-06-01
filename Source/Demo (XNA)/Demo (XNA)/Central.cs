using Microsoft.Xna.Framework;
using MonoForce.Controls;

namespace MonoForce.Demo
{
    /// <summary>
    /// MonoForce Demo (Legacy XNA version)
    /// </summary>
    public sealed class Central : Application
    {
        public static long Frames { get; set; }

        private int afps;
        private double et;
        private int fps;

        public Central() : base(true)
        {
            SystemBorder = true;
            FullScreenBorder = false;
            ClearBackground = false;
            ExitConfirmation = false;
            Manager.TargetFrames = 60;
        }

        /// <summary>
        /// Creates the main window which all content is added to.
        /// </summary>
        protected override Window CreateMainWindow()
        {
            return new MainWindow(Manager);
        }

        /// <summary>
        /// Update the game and UI.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateStats(gameTime);
        }

        /// <summary>
        /// Draw the game and UI.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            Frames += 1;
            base.Draw(gameTime);
        }

        /// <summary>
        /// Updates FPS, object count, etc.
        /// </summary>
        private void UpdateStats(GameTime time)
        {
            var window = MainWindow as MainWindow;
            if (et >= 500 || et == 0)
            {
                if (MainWindow != null)
                {
                    window.lblObjects.Text = "Objects: " + Disposable.Count;
                    window.lblAvgFps.Text = "Average FPS: " + afps;
                    window.lblFps.Text = "Current FPS: " + fps;
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
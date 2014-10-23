using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoForce.Controls;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.GamerServices;

namespace MonoForce.Examples.ApplicationDemo
{
    /// <summary>
    /// A small demo to show the features of MonoForce and the use of the Application class
    /// </summary>
    public class ApplicationDemo : Application
    {
        #region Properties
        public static long TotalFrames { get; set; }
        #endregion

        #region Fields
        private int averageFps = 0;
        private int fps = 0;
        private double elapsedTime = 0;
        #endregion

        #region Constructors
        public ApplicationDemo()
            : base("Default", true)
        {
            SystemBorder = false;
            FullScreenBorder = false;
            ClearBackground = false;
            ExitConfirmation = false;
            Manager.TargetFrames = 60;
            IsFixedTimeStep = true;

            TotalFrames = 0;

            //Components.Add(new GamerServicesComponent(this));         
            //Manager.UseGuide = true;
        }
        #endregion

        #region Methods
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override Window CreateMainWindow()
        {
            return new MainWindow(Manager);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateStats(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            TotalFrames += 1;
            base.Draw(gameTime);
        }

        private void UpdateStats(GameTime time)
        {
            MainWindow window = MainWindow as MainWindow;

            //Display and update the UI control count and fps every 500ms
            if (elapsedTime >= 500 || elapsedTime == 0)
            {
                if (window != null)
                {
                    window.lblObjects.Text = "Objects: " + Disposable.Count.ToString();
                    window.lblAvgFps.Text = "Average FPS: " + averageFps.ToString();
                    window.lblFps.Text = "Current FPS: " + fps.ToString();
                }

                //Calculate FPS and average FPS since opening
                if (time.TotalGameTime.TotalSeconds != 0)
                {
                    averageFps = (int)(TotalFrames / time.TotalGameTime.TotalSeconds);
                }

                if (time.ElapsedGameTime.TotalMilliseconds != 0)
                {
                    fps = (int)(1000 / time.ElapsedGameTime.TotalMilliseconds);
                }

                elapsedTime = 1;
            }
            elapsedTime += time.ElapsedGameTime.TotalMilliseconds;
        }
        #endregion
    }
}
////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Central                                          //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: Central.cs                                   //
//                                                            //
//      Version: 0.7                                          //
//                                                            //
//         Date: 11/09/2010                                   //
//                                                            //
//       Author: Tom Shane                                    //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//  Copyright (c) by Tom Shane                                //
//                                                            //
////////////////////////////////////////////////////////////////

#region //// Using /////////////

////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoForce.Controls;
using System.Runtime.InteropServices;
////////////////////////////////////////////////////////////////////////////

#endregion

namespace MonoForce.Demo
{

    public class Game1: Game
    {

        #region //// Fields ////////////

        ////////////////////////////////////////////////////////////////////////////               
        private int afps = 0;
        private int fps = 0;
        private double et = 0;
        public static long Frames = 0;
        GraphicsDeviceManager graphics;
        public MainWindow MainWindow;

        Manager neoManager;

        public Manager NeoManager
        {
            get { return neoManager; }
            set { neoManager = value; }
        }
        ////////////////////////////////////////////////////////////////////////////

        #endregion

        #region //// Constructors //////

        ////////////////////////////////////////////////////////////////////////////    
        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);

            neoManager = new Manager(this, "Default");
            neoManager.AutoCreateRenderTarget = true;
            neoManager.TargetFrames = 60;
            neoManager.LogUnhandledExceptions = false;
            neoManager.ShowSoftwareCursor = true;
        }
        ////////////////////////////////////////////////////////////////////////////        

        #endregion

        #region //// Methods ///////////

        ////////////////////////////////////////////////////////////////////////////
        protected override void Initialize()
        {
            base.Initialize();
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        protected override void LoadContent()
        {
            base.LoadContent();
            neoManager.Initialize();
            MainWindow = new MainWindow(neoManager);
            NeoManager.Add(MainWindow);
        }
        ////////////////////////////////////////////////////////////////////////////    

        ////////////////////////////////////////////////////////////////////////////
        protected override void Update(GameTime gameTime)
        {
      
            neoManager.Update(gameTime);
            UpdateStats(gameTime);
            base.Update(gameTime);
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        protected override void Draw(GameTime gameTime)
        {
            Frames += 1;
            neoManager.BeginDraw(gameTime);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here


            neoManager.EndDraw();
            base.Draw(gameTime);
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////              
        private void UpdateStats(GameTime time)
        {
            MainWindow wnd = MainWindow as MainWindow;
            if (et >= 500 || et == 0)
            {
                if (wnd != null)
                {
                    wnd.lblObjects.Text = "Objects: " + Disposable.Count.ToString();
                    wnd.lblAvgFps.Text = "Average FPS: " + afps.ToString();
                    wnd.lblFps.Text = "Current FPS: " + fps.ToString();
                }

                if (time.TotalGameTime.TotalSeconds != 0)
                {
                    afps = (int)(Frames / time.TotalGameTime.TotalSeconds);
                }

                if (time.ElapsedGameTime.TotalMilliseconds != 0)
                {
                    fps = (int)(1000 / time.ElapsedGameTime.TotalMilliseconds);
                }

                et = 1;
            }
            et += time.ElapsedGameTime.TotalMilliseconds;
        }
        ////////////////////////////////////////////////////////////////////////////
        /*
            ////////////////////////////////////////////////////////////////////////////    
            protected override RenderTarget2D CreateRenderTarget()
            {
              return new RenderTarget2D(GraphicsDevice, 1024, 768, false, SurfaceFormat.Color, DepthFormat.None, 0, Manager.RenderTargetUsage);
            }
            ////////////////////////////////////////////////////////////////////////////
            */
        #endregion

    }
}
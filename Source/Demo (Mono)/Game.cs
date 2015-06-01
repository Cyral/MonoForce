#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoForce.Controls;

#endregion

namespace MonoForce.Demo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Manager neoManager;

        public Manager NeoManager
        {
            get { return neoManager; }
            set { neoManager = value; }
        }

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            neoManager = new Manager(this, "Default");
            neoManager.AutoCreateRenderTarget = true;
            neoManager.TargetFrames = 60;
            neoManager.LogUnhandledExceptions = false;
            neoManager.ShowSoftwareCursor = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            neoManager.Initialize();

            Window w = new Window(NeoManager);
            w.Init();
            w.Text = "Demo Window";
            w.Width = 400;
            NeoManager.Add(w);

            Button b = new Button(NeoManager);
            b.Init();
            b.Text = "Hello World";
            b.Width = 128;
            b.Top = b.Left = 16;
            w.Add(b);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            neoManager.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            neoManager.BeginDraw(gameTime);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here


            neoManager.EndDraw();
            base.Draw(gameTime);
        }
    }
}
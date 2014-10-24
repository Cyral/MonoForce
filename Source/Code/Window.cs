////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Controls                                         //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: Window.cs                                    //
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

#region Using

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace MonoForce.Controls
{
    public class WindowGamePadActions : GamePadActions
    {
        public GamePadButton Accept = GamePadButton.Start;
        public GamePadButton Cancel = GamePadButton.Back;
    }

    public class Window : ModalContainer
    {

        #region Constants

        /// <summary>
        /// String for accessing the skin object of the window.
        /// </summary>
        private const string skWindow = "Window";
        /// <summary>
        /// String for accessing the window layer.
        /// </summary>
        private const string lrWindow = "Control";
        /// <summary>
        /// String for accessing the window's caption area layer.
        /// </summary>
        private const string lrCaption = "Caption";
        /// <summary>
        /// String for accessing the window's top border layer.
        /// </summary>
        private const string lrFrameTop = "FrameTop";
        /// <summary>
        /// String for accessing the window's left border layer.
        /// </summary>
        private const string lrFrameLeft = "FrameLeft";
        /// <summary>
        /// String for accessing the window's right border layer.
        /// </summary>
        private const string lrFrameRight = "FrameRight";
        /// <summary>
        /// String for accessing the window's bottom border layer.
        /// </summary>
        private const string lrFrameBottom = "FrameBottom";
        /// <summary>
        /// String for accessing the window icon layer.
        /// </summary>
        private const string lrIcon = "Icon";
        /// <summary>
        /// String for accessing the skin object for the close button of the window.
        /// </summary>
        private const string skButton = "Window.CloseButton";
        /// <summary>
        /// String for accessing the window button control layer.
        /// </summary>
        private const string lrButton = "Control";
        /// <summary>
        /// String for accessing the skin object for the window shadow.
        /// </summary>
        private const string skShadow = "Window.Shadow";
        /// <summary>
        /// String for accessing the window shadow layer.
        /// </summary>
        private const string lrShadow = "Control";

        #endregion

        #region Fields
        private bool closeButtonVisible = true;
        private bool captionVisible = true;
        private bool borderVisible = true;
        private byte oldAlpha = 255;
        #endregion

        #region Events

        #endregion

        #region Properties

        /// <summary>
        /// The button that closes the window.
        /// </summary>
        public virtual Button CloseButton { get; set; }
        /// <summary>
        /// The icon image for the window.
        /// </summary>
        public virtual Texture2D Icon { get; set; }
        /// <summary>
        /// Indicates if the window shadow is drawn.
        /// </summary>
        public virtual bool Shadow { get; set; }
        /// <summary>
        /// Indicates if the window icon is drawn.
        /// </summary>
        public virtual bool IconVisible { get; set; }
        /// <summary>
        /// The alpha value used when dragging the window.
        /// </summary>
        public virtual byte DragAlpha { get; set; }
        /// <summary>
        /// Indicates if the window close button is drawn.
        /// </summary>
        public virtual bool CloseButtonVisible
        {
            get
            {
                return closeButtonVisible;
            }
            set
            {
                closeButtonVisible = value;
                if (CloseButton != null) CloseButton.Visible = value;
            }
        }
        /// <summary>
        /// Indicates if the window caption is drawn.
        /// </summary>
        public virtual bool CaptionVisible
        {
            get { return captionVisible; }
            set
            {
                captionVisible = value;
                AdjustMargins();
            }
        }
        /// <summary>
        /// Indicates if the window border is drawn.
        /// </summary>
        public virtual bool BorderVisible
        {
            get { return borderVisible; }
            set
            {
                borderVisible = value;
                AdjustMargins();
            }
        }
        #endregion

        #region Constructors

        public Window(Manager manager)
            : base(manager)
        {

			DragAlpha = 200;
			
			//Ensure that all the required layers are defined for the window's skin.
            CheckLayer(Skin, lrWindow);
            CheckLayer(Skin, lrCaption);
            CheckLayer(Skin, lrFrameTop);
            CheckLayer(Skin, lrFrameLeft);
            CheckLayer(Skin, lrFrameRight);
            CheckLayer(Skin, lrFrameBottom);
            CheckLayer(Manager.Skin.Controls[skButton], lrButton);
            CheckLayer(Manager.Skin.Controls[skShadow], lrShadow);

            SetDefaultSize(640, 480);
            SetMinimumSize(100, 75);

            //Setup the close button.
            CloseButton = new Button(manager);
            CloseButton.Skin = new SkinControl(Manager.Skin.Controls[skButton]);
            CloseButton.Init();
            CloseButton.Detached = true;
            CloseButton.CanFocus = false;
            CloseButton.Text = null;
            CloseButton.Click += new EventHandler(btnClose_Click);
            CloseButton.SkinChanged += new EventHandler(btnClose_SkinChanged);

            //Setup margins
            AdjustMargins();

            AutoScroll = true;
            Movable = true;
            Resizable = true;
            Center();

            Add(CloseButton, false);

            oldAlpha = Alpha;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Initializes the window.
        /// </summary>
        public override void Init()
        {
            base.Init();

            //Setup the close button.
            SkinLayer closeButton = CloseButton.Skin.Layers[lrButton];
            CloseButton.Width = closeButton.Width - CloseButton.Skin.OriginMargins.Horizontal;
            CloseButton.Height = closeButton.Height - CloseButton.Skin.OriginMargins.Vertical;
            CloseButton.Left = OriginWidth - Skin.OriginMargins.Right - CloseButton.Width + closeButton.OffsetX;
            CloseButton.Top = Skin.OriginMargins.Top + closeButton.OffsetY;
            CloseButton.Anchor = Anchors.Top | Anchors.Right;
        }
        /// <summary>
        /// Centres the window on the screen.
        /// </summary>
        public virtual void Center()
        {
            Left = (Manager.ScreenWidth / 2) - (Width / 2);
            Top = (Manager.ScreenHeight - Height) / 2;
        }
        /// <summary>
        /// Draws the window.
        /// </summary>
        /// <param name="renderer">Renderer instance.</param>
        /// <param name="gameTime">Current Game Time</param>
        internal override void Render(Renderer renderer, GameTime gameTime)
        {
            //Draw the shadow first, if the window is visible and has a shadow.
            if (Visible && Shadow)
            {
                SkinControl skin = Manager.Skin.Controls[skShadow];
                SkinLayer layer = skin.Layers[lrShadow];

                Color c = Color.FromNonPremultiplied(layer.States.Enabled.Color.R, layer.States.Enabled.Color.G, layer.States.Enabled.Color.B, Alpha);

                renderer.Begin(BlendingMode.Default);
                renderer.DrawLayer(layer, new Rectangle(Left - skin.OriginMargins.Left, Top - skin.OriginMargins.Top, Width + skin.OriginMargins.Horizontal, Height + skin.OriginMargins.Vertical), c, 0);
                renderer.End();
            }
            base.Render(renderer, gameTime);
        }
        /// <summary>
        /// Cleans up the window's resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// Draws the window and all child controls.
        /// </summary>
        /// <param name="renderer">Render management object.</param>
        /// <param name="rect">Destination region where the window will be drawn.</param>
        /// <param name="gameTime">The current game timing values.</param>
        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            SkinLayer l1 = captionVisible ? Skin.Layers[lrCaption] : Skin.Layers[lrFrameTop];
            SkinLayer l2 = Skin.Layers[lrFrameLeft];
            SkinLayer l3 = Skin.Layers[lrFrameRight];
            SkinLayer l4 = Skin.Layers[lrFrameBottom];
            SkinLayer l5 = Skin.Layers[lrIcon];

            LayerStates s1, s2, s3, s4;
            SpriteFont f1 = l1.Text.Font.Resource;
            Color c1 = l1.Text.Colors.Enabled;

            //Window has focus?
            if ((Focused || (Manager.FocusedControl != null && Manager.FocusedControl.Root == this.Root)) && ControlState != ControlState.Disabled)
            {
                s1 = l1.States.Focused;
                s2 = l2.States.Focused;
                s3 = l3.States.Focused;
                s4 = l4.States.Focused;
                c1 = l1.Text.Colors.Focused;
            }
            //Window is disabled?
            else if (ControlState == ControlState.Disabled)
            {
                s1 = l1.States.Disabled;
                s2 = l2.States.Disabled;
                s3 = l3.States.Disabled;
                s4 = l4.States.Disabled;
                c1 = l1.Text.Colors.Disabled;
            }
            //Window not active, or child control has focus?
            else
            {
                s1 = l1.States.Enabled;
                s2 = l2.States.Enabled;
                s3 = l3.States.Enabled;
                s4 = l4.States.Enabled;
                c1 = l1.Text.Colors.Enabled;
            }

            //Draw the window layer.
            renderer.DrawLayer(Skin.Layers[lrWindow], rect, Skin.Layers[lrWindow].States.Enabled.Color, Skin.Layers[lrWindow].States.Enabled.Index);

            //Draw the window border?
            if (borderVisible)
            {
                // Draw caption layer or top frame layer, then draw the left, right, and bottom frame layers.
                renderer.DrawLayer(l1, new Rectangle(rect.Left, rect.Top, rect.Width, l1.Height), s1.Color, s1.Index);
                renderer.DrawLayer(l2, new Rectangle(rect.Left, rect.Top + l1.Height, l2.Width, rect.Height - l1.Height - l4.Height), s2.Color, s2.Index);
                renderer.DrawLayer(l3, new Rectangle(rect.Right - l3.Width, rect.Top + l1.Height, l3.Width, rect.Height - l1.Height - l4.Height), s3.Color, s3.Index);
                renderer.DrawLayer(l4, new Rectangle(rect.Left, rect.Bottom - l4.Height, rect.Width, l4.Height), s4.Color, s4.Index);

                // Draw the window icon if there is one and the window caption is displayed.
                if (IconVisible && (Icon != null || l5 != null) && captionVisible)
                {
                    Texture2D i = (Icon != null) ? Icon : l5.Image.Resource;
                    renderer.Draw(i, GetIconRect(), Color.White);
                }

                int icosize = 0;
                if (l5 != null && IconVisible && captionVisible)
                {
                    icosize = l1.Height - l1.ContentMargins.Vertical + 4 + l5.OffsetX;
                }

                //Draw the close button if it is visible.
                int closesize = 0;
                if (CloseButton.Visible)
                {
                    closesize = CloseButton.Width - (CloseButton.Skin.Layers[lrButton].OffsetX);
                }

                // Create the rectangle defining the remaining caption area to draw text in.
                Rectangle r = new Rectangle(rect.Left + l1.ContentMargins.Left + icosize,
                                            rect.Top + l1.ContentMargins.Top,
                                            rect.Width - l1.ContentMargins.Horizontal - closesize - icosize,
                                            l1.Height - l1.ContentMargins.Top - l1.ContentMargins.Bottom);
                int ox = l1.Text.OffsetX;
                int oy = l1.Text.OffsetY;

                //Draw the window title in the caption area remaining.
                renderer.DrawString(f1, Text, r, c1, l1.Text.Alignment, ox, oy, true);
            }
        }
        /// <summary>
        /// Initializes the skin of the window.
        /// </summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls[skWindow]);
            AdjustMargins();
        }
        /// <summary>
        /// Handles resizing of the window.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(ResizeEventArgs e)
        {
            SetMovableArea();
            base.OnResize(e);
        }
        /// <summary>
        /// Handles for the window begins to move.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMoveBegin(EventArgs e)
        {
            base.OnMoveBegin(e);
            //Swap the alpha values.
            try
            {
                oldAlpha = Alpha;
                Alpha = DragAlpha;
            }
            catch
            {
            }
        }
        /// <summary>
        /// Handles window motion ending.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMoveEnd(EventArgs e)
        {
            base.OnMoveEnd(e);
            try
            {
                Alpha = oldAlpha;
            }
            catch
            {
            }
        }
        /// <summary>
        /// Handles double clicking.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            MouseEventArgs ex = (e is MouseEventArgs) ? (MouseEventArgs)e : new MouseEventArgs();

            //Window will close if the icon is double clicked.
            if (IconVisible && ex.Button == MouseButton.Left)
            {
                Rectangle r = GetIconRect();
                r.Offset(AbsoluteLeft, AbsoluteTop);
                if (r.Contains(ex.Position))
                {
                    Close();
                }
            }
        }
        /// <summary>
        /// Adjust the client area margins based on the caption area and window border.
        /// </summary>
        protected override void AdjustMargins()
        {

            if (captionVisible && borderVisible)
            {
                //Adjust margins to account for the caption area and window border.
                ClientMargins = new Margins(Skin.ClientMargins.Left, Skin.Layers[lrCaption].Height, Skin.ClientMargins.Right, Skin.ClientMargins.Bottom);
            }
            else if (!captionVisible && borderVisible)
            {
                //Adjust margins to account for the window border.
                ClientMargins = new Margins(Skin.ClientMargins.Left, Skin.ClientMargins.Top, Skin.ClientMargins.Right, Skin.ClientMargins.Bottom);
            }
            else if (!borderVisible)
            {
                //No margin.
                ClientMargins = new Margins(0, 0, 0, 0);
            }
            //Display the close button?
            if (CloseButton != null)
            {
                CloseButton.Visible = closeButtonVisible && captionVisible && borderVisible;
            }

            SetMovableArea();

            base.AdjustMargins();
        }
        /// <summary>
        /// Sets the region where the window can be moved to.
        /// </summary>
        private void SetMovableArea()
        {
            if (captionVisible && borderVisible)
            {
                MovableArea = new Rectangle(Skin.OriginMargins.Left, Skin.OriginMargins.Top, Width, Skin.Layers[lrCaption].Height - Skin.OriginMargins.Top);
            }
            else if (!captionVisible)
            {
                MovableArea = new Rectangle(0, 0, Width, Height);
            }
        }
        /// <summary>
        /// Handles reskinning the close button when the skin changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_SkinChanged(object sender, EventArgs e)
        {
            CloseButton.Skin = new SkinControl(Manager.Skin.Controls[skButton]);
        }
        /// <summary>
        /// Handles closing the window when the close button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close(ModalResult = ModalResult.Cancel);
        }
        /// <summary>
        /// Creates where the window icon should be drawn.
        /// </summary>
        /// <returns>The rectangle where the window icon should be drawn.</returns>
        private Rectangle GetIconRect()
        {
            SkinLayer l1 = Skin.Layers[lrCaption];
            SkinLayer l5 = Skin.Layers[lrIcon];

            //Icon will be scaled to fit in the space alloted by the caption bar.
            int s = l1.Height - l1.ContentMargins.Vertical;

            return new Rectangle(DrawingRect.Left + l1.ContentMargins.Left + l5.OffsetX,
                                 DrawingRect.Top + l1.ContentMargins.Top + l5.OffsetY,
                                 s, s);

        }

        #endregion
    }
}

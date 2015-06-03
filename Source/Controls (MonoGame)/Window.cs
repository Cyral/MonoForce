using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForce.Controls
{
    public class WindowGamePadActions : GamePadActions
    {
        public GamePadButton Accept = GamePadButton.Start;
        public GamePadButton Cancel = GamePadButton.Back;
    }

    public class Window : ModalContainer
    {
        private const string lrButton = "Control";
        private const string lrCaption = "Caption";
        private const string lrFrameBottom = "FrameBottom";
        private const string lrFrameLeft = "FrameLeft";
        private const string lrFrameRight = "FrameRight";
        private const string lrFrameTop = "FrameTop";
        private const string lrIcon = "Icon";
        private const string lrShadow = "Control";
        private const string lrWindow = "Control";
        private const string skButton = "Window.CloseButton";
        private const string skShadow = "Window.Shadow";
        private const string skWindow = "Window";

        /// </summary>
        /// Indicates if the window should draw its border.
        /// <summary>
        public virtual bool BorderVisible
        {
            get { return borderVisible && !clearBackground; }
            set
            {
                borderVisible = value;
// Set up window margins.
                AdjustMargins();
            }
        }

        /// </summary>
        /// Indicates if the window should draw its caption.
        /// <summary>
        public virtual bool CaptionVisible
        {
            get { return captionVisible && !clearBackground; }
            set
            {
                captionVisible = value;
// Set up window margins.
                AdjustMargins();
            }
        }

        /// </summary>
        /// Indicates if the window should draw its close button.
        /// <summary>
        public virtual bool CloseButtonVisible
        {
            get { return closeButtonVisible && !clearBackground; }
            set
            {
                closeButtonVisible = value;
                if (btnClose != null) btnClose.Visible = value;
            }
        }

        /// </summary>
        /// Gets or sets the alpha value that should be applied to the window during a drag operation.
        /// <summary>
        public virtual byte DragAlpha
        {
            get { return dragAlpha; }
            set { dragAlpha = value; }
        }

        /// </summary>
        /// Gets or sets the window icon image.
        /// <summary>
        public virtual Texture2D Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        /// </summary>
        /// Indicates if the window should draw its icon.
        /// <summary>
        public virtual bool IconVisible
        {
            get { return iconVisible; }
            set { iconVisible = value; }
        }

        /// </summary>
        /// Indicates if the window should draw its shadow.
        /// <summary>
        public virtual bool Shadow
        {
            get { return shadow && !clearBackground; }
            set { shadow = value; }
        }

        /// <summary>
        /// Indicates if the window has no visible background, shadow, or borders.
        /// </summary>
        public virtual bool ClearBackground
        {
            get { return clearBackground; }
            set
            {
                clearBackground = value;
                AdjustMargins();
            }
        }

        private readonly Button btnClose;

        /// </summary>
        /// Indicates if the window border is drawn.
        /// <summary>
        private bool borderVisible = true;

        /// </summary>
        /// Indicates if the window caption is drawn.
        /// <summary>
        private bool captionVisible = true;

        /// </summary>
        /// Indicates if the close button is drawn.
        /// <summary>
        private bool closeButtonVisible = true;

        /// </summary>
        /// Alpha value used when dragging the window.
        /// <summary>
        private byte dragAlpha = 200;

        /// </summary>
        /// Window icon image.
        /// <summary>
        private Texture2D icon;

        /// </summary>
        /// Indicates if the window icon is drawn.
        /// <summary>
        private bool iconVisible = true;

        /// </summary>
        /// Alpha value of the window.
        /// <summary>
        private float oldAlpha = 255;

        /// </summary>
        /// Indicates if the window shadow is drawn.
        /// <summary>
        private bool shadow = true;

        /// <summary>
        /// Indicates if the window has no visible background, shadow, or borders.
        /// </summary>
        private bool clearBackground = false;

        public Window(Manager manager) : base(manager)
        {
// Make sure all the required layers are defined for the window's skin.
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

            btnClose = new Button(manager);
            btnClose.Skin = new SkinControl(Manager.Skin.Controls[skButton]);
            btnClose.Init();
            btnClose.Detached = true;
            btnClose.CanFocus = false;
            btnClose.Text = null;
            btnClose.Click += btnClose_Click;
            btnClose.SkinChanged += btnClose_SkinChanged;

// Set up window margins.
            AdjustMargins();

            AutoScroll = true;
            Movable = true;
            Resizable = true;
            Center();

            Add(btnClose, false);

            oldAlpha = Alpha;
        }

        /// </summary>
        /// Centers the window on screen.
        /// <summary>
        public virtual void Center()
        {
            Left = (Manager.ScreenWidth / 2) - (Width / 2);
            Top = (Manager.ScreenHeight - Height) / 2;
        }

        /// </summary>
        /// Initializes the window.
        /// <summary>
        public override void Init()
        {
            base.Init();

            var l = btnClose.Skin.Layers[lrButton];
            btnClose.Width = l.Width - btnClose.Skin.OriginMargins.Horizontal;
            btnClose.Height = l.Height - btnClose.Skin.OriginMargins.Vertical;
            btnClose.Left = OriginWidth - Skin.OriginMargins.Right - btnClose.Width + l.OffsetX;
            btnClose.Top = Skin.OriginMargins.Top + l.OffsetY;
            btnClose.Anchor = Anchors.Top | Anchors.Right;
        }

        /// </summary>
        /// Initializes the skin of the window.
        /// <summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls[skWindow]);
// Set up window margins.
            AdjustMargins();
        }

        /// </summary>
        /// Adjusts the client area margins based on the visibility of the caption area and window border.
        /// <summary>
        protected override void AdjustMargins()
        {
            if (CaptionVisible && BorderVisible)
            {
// Adjust margins to account for the window border and caption area.
                ClientMargins = new Margins(Skin.ClientMargins.Left, Skin.Layers[lrCaption].Height,
                    Skin.ClientMargins.Right, Skin.ClientMargins.Bottom);
            }
            else if (!CaptionVisible && BorderVisible)
            {
// Adjust margins to account for the window border.
                ClientMargins = new Margins(Skin.ClientMargins.Left, Skin.ClientMargins.Top, Skin.ClientMargins.Right,
                    Skin.ClientMargins.Bottom);
            }
            else if (!BorderVisible)
            {
// Nothing to account for.
                ClientMargins = new Margins(0, 0, 0, 0);
            }

            if (btnClose != null)
            {
                btnClose.Visible = closeButtonVisible && CaptionVisible && BorderVisible;
            }

            SetMovableArea();

            base.AdjustMargins();
        }

        /// <param name="disposing"></param>
        /// </summary>
        /// Cleans up window resources.
        /// <summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        public override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            var l1 = captionVisible ? Skin.Layers[lrCaption] : Skin.Layers[lrFrameTop];
            var l2 = Skin.Layers[lrFrameLeft];
            var l3 = Skin.Layers[lrFrameRight];
            var l4 = Skin.Layers[lrFrameBottom];
            var l5 = Skin.Layers[lrIcon];
            LayerStates s1, s2, s3, s4;
            var f1 = l1.Text.Font.Resource;
            var c1 = l1.Text.Colors.Enabled;

// Window has focus?
            if ((Focused || (Manager.FocusedControl != null && Manager.FocusedControl.Root == Root)) &&
                ControlState != ControlState.Disabled)
            {
                s1 = l1.States.Focused;
                s2 = l2.States.Focused;
                s3 = l3.States.Focused;
                s4 = l4.States.Focused;
                c1 = l1.Text.Colors.Focused;
            }
// Window is disabled?
            else if (ControlState == ControlState.Disabled)
            {
                s1 = l1.States.Disabled;
                s2 = l2.States.Disabled;
                s3 = l3.States.Disabled;
                s4 = l4.States.Disabled;
                c1 = l1.Text.Colors.Disabled;
            }
// Window not active or child control has focus?
            else
            {
                s1 = l1.States.Enabled;
                s2 = l2.States.Enabled;
                s3 = l3.States.Enabled;
                s4 = l4.States.Enabled;
                c1 = l1.Text.Colors.Enabled;
            }

            if (!clearBackground)
            renderer.DrawLayer(Skin.Layers[lrWindow], rect, Skin.Layers[lrWindow].States.Enabled.Color,
                Skin.Layers[lrWindow].States.Enabled.Index);

// Need to draw the window border?
            if (BorderVisible)
            {
// Draw caption layer or top frame layer, then draw the left, right, and bottom frame layers.
                renderer.DrawLayer(l1, new Rectangle(rect.Left, rect.Top, rect.Width, l1.Height), s1.Color, s1.Index);
                renderer.DrawLayer(l2,
                    new Rectangle(rect.Left, rect.Top + l1.Height, l2.Width, rect.Height - l1.Height - l4.Height),
                    s2.Color, s2.Index);
                renderer.DrawLayer(l3,
                    new Rectangle(rect.Right - l3.Width, rect.Top + l1.Height, l3.Width,
                        rect.Height - l1.Height - l4.Height), s3.Color, s3.Index);
                renderer.DrawLayer(l4, new Rectangle(rect.Left, rect.Bottom - l4.Height, rect.Width, l4.Height),
                    s4.Color, s4.Index);

// Draw the window icon if there is one and the window caption is displayed.
                if (iconVisible && (icon != null || l5 != null) && CaptionVisible)
                {
                    var i = (icon != null) ? icon : l5.Image.Resource;
                    renderer.Draw(i, GetIconRect(), Color.White);
                }

                var icosize = 0;
                if (l5 != null && iconVisible && CaptionVisible)
                {
                    icosize = l1.Height - l1.ContentMargins.Vertical + 4 + l5.OffsetX;
                }
// Draw the close button if visible.
                var closesize = 0;
                if (btnClose.Visible)
                {
                    closesize = btnClose.Width - (btnClose.Skin.Layers[lrButton].OffsetX);
                }

// Create the rectangle defining the remaining caption area to draw text in.
                var r = new Rectangle(rect.Left + l1.ContentMargins.Left + icosize,
                    rect.Top + l1.ContentMargins.Top,
                    rect.Width - l1.ContentMargins.Horizontal - closesize - icosize,
                    l1.Height - l1.ContentMargins.Top - l1.ContentMargins.Bottom);
                var ox = l1.Text.OffsetX;
                var oy = l1.Text.OffsetY;
                renderer.DrawString(f1, Text, r, c1, l1.Text.Alignment, ox, oy, true);
            }
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles double click events for the window.
        /// <summary>
        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            var ex = (e is MouseEventArgs) ? (MouseEventArgs)e : new MouseEventArgs();

// Double clicking the Icon closes the window.
            if (IconVisible && ex.Button == MouseButton.Left)
            {
                var r = GetIconRect();
                r.Offset(AbsoluteLeft, AbsoluteTop);
                if (r.Contains(ex.Position))
                {
                    Close();
                }
            }
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handler for when the window starts a move event.
        /// <summary>
        protected override void OnMoveBegin(EventArgs e)
        {
            base.OnMoveBegin(e);

// Swap the current alpha values.
            try
            {
                oldAlpha = Alpha;
                Alpha = dragAlpha;
            }
            catch
            {
            }
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handler for when the window completes a move event.
        /// <summary>
        protected override void OnMoveEnd(EventArgs e)
        {
            base.OnMoveEnd(e);
// Swap the current alpha values.
            try
            {
                Alpha = oldAlpha;
            }
            catch
            {
            }
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles resizing of the window.
        /// <summary>
        protected override void OnResize(ResizeEventArgs e)
        {
            SetMovableArea();
            base.OnResize(e);
        }

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <param name="renderer">Render management object.</param>
        /// </summary>
        /// Draws the window.
        /// <summary>
        internal override void Render(Renderer renderer, GameTime gameTime)
        {
// Draw the shadow first if the window is displayed and the shadow is being used.
            if (Visible && Shadow)
            {
                var c = Manager.Skin.Controls[skShadow];
                var l = c.Layers[lrShadow];

                var cl = Color.FromNonPremultiplied(l.States.Enabled.Color.R, l.States.Enabled.Color.G,
                    l.States.Enabled.Color.B, Alpha > 255 ? 255 : (byte)Alpha);

                renderer.Begin(BlendingMode.Default);
                renderer.DrawLayer(l,
                    new Rectangle(Left - c.OriginMargins.Left, Top - c.OriginMargins.Top,
                        Width + c.OriginMargins.Horizontal, Height + c.OriginMargins.Vertical), cl, 0);
                renderer.End();
            }
// Draw the window.
            base.Render(renderer, gameTime);
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles closing the window when the close button is clicked.
        /// <summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close(ModalResult = ModalResult.Cancel);
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles reskinning the close button when the skin changes.
        /// <summary>
        private void btnClose_SkinChanged(object sender, EventArgs e)
        {
            btnClose.Skin = new SkinControl(Manager.Skin.Controls[skButton]);
        }

        /// <returns>Returns the window icon's destination region where it will be drawn. </returns>
        /// </summary>
        /// Creates the rectangle where the window icon should be displayed.
        /// <summary>
        private Rectangle GetIconRect()
        {
            var l1 = Skin.Layers[lrCaption];
            var l5 = Skin.Layers[lrIcon];

// Icon will be scaled to fit in the space alloted by the caption bar.
            var s = l1.Height - l1.ContentMargins.Vertical;
// Return the destination rectangle for the window icon. Left side of the window caption.
            return new Rectangle(DrawingRect.Left + l1.ContentMargins.Left + l5.OffsetX,
                DrawingRect.Top + l1.ContentMargins.Top + l5.OffsetY,
                s, s);
        }

        /// </summary>
        /// Sets the region where the window can be moved to.
        /// <summary>
        private void SetMovableArea()
        {
            if (CaptionVisible && BorderVisible)
            {
                MovableArea = new Rectangle(Skin.OriginMargins.Left, Skin.OriginMargins.Top, Width,
                    Skin.Layers[lrCaption].Height - Skin.OriginMargins.Top);
            }
            else if (!CaptionVisible)
            {
                MovableArea = new Rectangle(0, 0, Width, Height);
            }
        }
    }
}
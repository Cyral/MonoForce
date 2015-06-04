using Microsoft.Xna.Framework;

namespace MonoForce.Controls
{
    /// <summary>
    /// Stores the values of the container's horizontal and vertical scroll bars.
    /// </summary>
    public struct ScrollBarValue
    {
        public int Horizontal;
        public int Vertical;
    }

    public class Container : ClipControl
    {
        /// <summary>
        /// Indicates if scroll bars will be displayed/hidden automatically.
        /// </summary>
        public virtual bool AutoScroll
        {
            get { return autoScroll; }
            set { autoScroll = value; }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// Gets or sets the default control of the container.
        /// </summary>
        public virtual Control DefaultControl
        {
            get { return defaultControl; }
            set { defaultControl = value; }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// Gets or sets the main menu of the container control.
        /// </summary>
        public virtual MainMenu MainMenu
        {
            get { return mainMenu; }
            set
            {
                if (mainMenu != null)
                {
                    mainMenu.Resize -= Bars_Resize;
                    Remove(mainMenu);
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }
                mainMenu = value;

                if (mainMenu != null)
                {
                    Add(mainMenu, false);
                    mainMenu.Resize += Bars_Resize;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }
// Update margins to account for the removal of the scroll bar.
                AdjustMargins();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// Scroll by PageSize (true) or StepSize (false)
        /// </summary>
        public virtual bool ScrollAlot
        {
            get { return scrollAlot; }
            set { scrollAlot = value; }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// Gets the values of the container's horizontal and vertical scroll bars.
        /// </summary>
        public virtual ScrollBarValue ScrollBarValue
        {
            get
            {
                var scb = new ScrollBarValue();
                scb.Vertical = (sbVert != null ? sbVert.Value : 0);
                scb.Horizontal = (sbHorz != null ? sbHorz.Value : 0);
                return scb;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// Gets or sets the container's status bar control.
        /// </summary>
        public virtual StatusBar StatusBar
        {
            get
            {
                return statusBar;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
            set
            {
                if (statusBar != null)
                {
                    statusBar.Resize -= Bars_Resize;
                    Remove(statusBar);
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }
                statusBar = value;

                if (statusBar != null)
                {
                    Add(statusBar, false);
                    statusBar.Resize += Bars_Resize;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }
// Update margins to account for the removal of the scroll bar.
                AdjustMargins();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// Gets or sets the tool bar panel of the container control.
        /// </summary>
        public virtual ToolBarPanel ToolBarPanel
        {
            get
            {
                return toolBarPanel;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
            set
            {
                if (toolBarPanel != null)
                {
                    toolBarPanel.Resize -= Bars_Resize;
                    Remove(toolBarPanel);
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }
                toolBarPanel = value;

                if (toolBarPanel != null)
                {
                    Add(toolBarPanel, false);
                    toolBarPanel.Resize += Bars_Resize;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }
// Update margins to account for the removal of the scroll bar.
                AdjustMargins();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// Indicates if the container is visible.
        /// </summary>
        public override bool Visible
        {
            get
            {
                return base.Visible;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
            set
            {
                if (value)
                {
                    if (DefaultControl != null)
                    {
                        DefaultControl.Focused = true;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                    }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }
                base.Visible = value;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// Gets the container's horizontal scroll bar.
        /// </summary>
        protected virtual ScrollBar HorizontalScrollBar
        {
            get { return sbHorz; }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// Gets the container's vertical scroll bar.
        /// </summary>
        protected virtual ScrollBar VerticalScrollBar
        {
            get { return sbVert; }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// Container control's horizontal scroll bar.
        /// </summary>
        private readonly ScrollBar sbHorz;

        /// <summary>
        /// Container control's vertical scroll bar.
        /// </summary>
        private readonly ScrollBar sbVert;

        /// <summary>
        /// Indicates if the container will automatically show/hide the client area scroll bars as needed.
        /// </summary>
        private bool autoScroll;

        /// <summary>
        /// Container's default control.
        /// </summary>
        private Control defaultControl;

        /// <summary>
        /// Container control's main menu.
        /// </summary>
        private MainMenu mainMenu;

        /// <summary>
        /// Scroll by PageSize (true) or StepSize (false)
        /// </summary>
        private bool scrollAlot = true;

        /// <summary>
        /// Status bar control of the container.
        /// </summary>
        private StatusBar statusBar;

        /// <summary>
        /// Tool bar panel of the container control.
        /// </summary>
        private ToolBarPanel toolBarPanel;

        public Container(Manager manager) : base(manager)
        {
            sbVert = new ScrollBar(manager, Orientation.Vertical);
            sbVert.Init();
            sbVert.Detached = false;
            sbVert.Anchor = Anchors.Top | Anchors.Right | Anchors.Bottom;
            sbVert.ValueChanged += ScrollBarValueChanged;
            sbVert.Range = 0;
            sbVert.PageSize = 0;
// No scroll bar needed, no scroll value needed.
            sbVert.Value = 0;
            sbVert.Visible = false;

            sbHorz = new ScrollBar(manager, Orientation.Horizontal);
            sbHorz.Init();
            sbHorz.Detached = false;
            sbHorz.Anchor = Anchors.Right | Anchors.Left | Anchors.Bottom;
            sbHorz.ValueChanged += ScrollBarValueChanged;
            sbHorz.Range = 0;
            sbHorz.PageSize = 0;
// No scroll bar needed, no scroll value needed.
            sbHorz.Value = 0;
            sbHorz.Visible = false;

            Add(sbVert, false);
            Add(sbHorz, false);
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// (true) or a direct descendant of the container. (false)
        /// </param>
        /// <param name="client">
        /// Indicates if the control will be a child of the client area
        /// <param name="control">The child control to add to the container.</param>
        /// <summary>
        /// Adds a child control to the container.
        /// </summary>
        public override void Add(Control control, bool client)
        {
            base.Add(control, client);
            CalcScrolling();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// Initializes the container control.
        /// </summary>
        public override void Init()
        {
            base.Init();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// Invalidates the control and forces redrawing of it.
        /// </summary>
        public override void Invalidate()
        {
            base.Invalidate();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <param name="y">New vertical scroll bar value.</param>
        /// <param name="x">New horizontal scroll bar value.</param>
        /// <summary>
        /// Scrolls to the specified scroll bar positions.
        /// </summary>
        public virtual void ScrollTo(int x, int y)
        {
            sbVert.Value = y;
            sbHorz.Value = x;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <param name="control"></param>
        /// <summary>
        /// Adjusts the scroll bar values so the specified control is displayed in the client region.
        /// </summary>
        public virtual void ScrollTo(Control control)
        {
// Make sure the control exists inside the client area somewhere.
            if (control != null && ClientArea != null && ClientArea.Contains(control, true))
            {
// Scroll down?
                if (control.AbsoluteTop + control.Height > ClientArea.AbsoluteTop + ClientArea.Height)
                {
                    sbVert.Value = sbVert.Value + control.AbsoluteTop - ClientArea.AbsoluteTop - sbVert.PageSize +
                                   control.Height;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }
// Scroll up?
                else if (control.AbsoluteTop < ClientArea.AbsoluteTop)
                {
                    sbVert.Value = sbVert.Value + control.AbsoluteTop - ClientArea.AbsoluteTop;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }
// Scroll left?
                if (control.AbsoluteLeft + control.Width > ClientArea.AbsoluteLeft + ClientArea.Width)
                {
                    sbHorz.Value = sbHorz.Value + control.AbsoluteLeft - ClientArea.AbsoluteLeft - sbHorz.PageSize +
                                   control.Width;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }
// Scroll right?
                else if (control.AbsoluteLeft < ClientArea.AbsoluteLeft)
                {
                    sbHorz.Value = sbHorz.Value + control.AbsoluteLeft - ClientArea.AbsoluteLeft;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// Initializes the skin of the container control.
        /// </summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handler for when the container's skin is changed.
        /// </summary>
        protected internal override void OnSkinChanged(EventArgs e)
        {
            base.OnSkinChanged(e);
            if (sbVert != null && sbHorz != null)
            {
                sbVert.Visible = false;
                sbHorz.Visible = false;
                CalcScrolling();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <param name="gameTime"></param>
        /// <summary>
        /// Updates the container control and all child controls.
        /// </summary>
        protected internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// the tool bar panel, the status bar, and the scroll bars.
        /// Adjusts the container's margins to account for the visibility of the main menu,
        /// </summary>
        protected override void AdjustMargins()
        {
// Get the skin margin values.
            var m = Skin.ClientMargins;

            if (GetType() != typeof (Container))
            {
                m = ClientMargins;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }

// Main menu displaying?
            if (mainMenu != null && mainMenu.Visible)
            {
                if (!mainMenu.Initialized) mainMenu.Init();
// Position and size the main menu.
                mainMenu.Left = m.Left;
                mainMenu.Top = m.Top;
                mainMenu.Width = Width - m.Horizontal;
                mainMenu.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;

// Update the container's top margin to account for the space the main menu is occupying.
                m.Top += mainMenu.Height;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
// Tool bar panel displaying?
            if (toolBarPanel != null && toolBarPanel.Visible)
            {
                if (!toolBarPanel.Initialized) toolBarPanel.Init();
// Position and size the tool bar panel.
                toolBarPanel.Left = m.Left;
                toolBarPanel.Top = m.Top;
                toolBarPanel.Width = Width - m.Horizontal;
                toolBarPanel.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;

// Update the container's top margin to account for the space the tool bar panel is occupying.
                m.Top += toolBarPanel.Height;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
// Status bar displaying?
            if (statusBar != null && statusBar.Visible)
            {
                if (!statusBar.Initialized) statusBar.Init();
// Position and size the status bar.
                statusBar.Left = m.Left;
                statusBar.Top = Height - m.Bottom - statusBar.Height;
                statusBar.Width = Width - m.Horizontal;
                statusBar.Anchor = Anchors.Left | Anchors.Bottom | Anchors.Right;

// Update the container's bottom margin to account for the space the status bar is occupying.
                m.Bottom += statusBar.Height;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
// Vertical scroll bar displayed?
            if (sbVert != null && sbVert.Visible)
            {
// Update the container's right margin to account for the space the scroll bar is occupying.
                m.Right += (sbVert.Width + 2);
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
// Horizontal scroll bar displayed?
            if (sbHorz != null && sbHorz.Visible)
            {
// Update the container's right margin to account for the space the scroll bar is occupying.
                m.Bottom += (sbHorz.Height + 2);
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }

// Update the container's margins.
            ClientMargins = m;

// Adjust the position of the scroll bars.
            PositionScrollBars();

            base.AdjustMargins();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            base.DrawControl(renderer, rect, gameTime);
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles click events for the container control.
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            var ex = e as MouseEventArgs;
// Adjust mouse position based on scroll bar values.
            ex.Position = new Point(ex.Position.X + sbHorz.Value, ex.Position.Y + sbVert.Value);

            base.OnClick(e);
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        protected override void OnMouseScroll(MouseEventArgs e)
        {
            if (!ClientArea.Enabled)
                return;

// If current control doesn't scroll, scroll the parent control
            if (sbVert.Range - sbVert.PageSize < 1)
            {
                Control c = this;

                while (c != null)
                {
                    var p = c.Parent as Container;

                    if (p != null && p.Enabled)
                    {
                        p.OnMouseScroll(e);

                        break;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                    }

                    c = c.Parent;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }

                return;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }

            if (e.ScrollDirection == MouseScrollDirection.Down)
                sbVert.ScrollDown(ScrollAlot);
            else
                sbVert.ScrollUp(ScrollAlot);

            base.OnMouseScroll(e);
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles resizing of the container control.
        /// </summary>
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            CalcScrolling();

//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <summary>
        /// (Updates the container margins.)
        /// Handlers for when the main menu, tool bar panel, or status bar are resized.
        /// </summary>
        private void Bars_Resize(object sender, ResizeEventArgs e)
        {
// Update margins to account for the removal of the scroll bar.
            AdjustMargins();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// Updates the visibility of scroll bars based on client area actual and vitual dimensions.
        /// </summary>
        private void CalcScrolling()
        {
            if (sbVert != null && autoScroll)
            {
                var vis = sbVert.Visible;
// Should the vertical scroll bar be visible?
                sbVert.Visible = ClientArea.VirtualHeight > ClientArea.ClientHeight;
                if (ClientArea.VirtualHeight <= ClientArea.ClientHeight) sbVert.Value = 0;

// Visibility of the scroll bar has changed?
                if (vis != sbVert.Visible)
                {
// Hiding the scroll bar now?
                    if (!sbVert.Visible)
                    {
// Clear all the client area child controls' top modifier values. No scroll bar = No offset.
                        foreach (var c in ClientArea.Controls)
                        {
                            c.TopModifier = 0;
                            c.Invalidate();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                        }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                    }
// Update margins to account for the removal of the scroll bar.
                    AdjustMargins();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }

// Adjust the position of the scroll bars.
                PositionScrollBars();
// Clear all the client area child controls' top modifier values. No scroll bar = No offset.
                foreach (var c in ClientArea.Controls)
                {
                    c.TopModifier = -sbVert.Value;
                    c.Invalidate();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }

            if (sbHorz != null && autoScroll)
            {
                var vis = sbHorz.Visible;
// Should the horizontal scroll bar be visible?
                sbHorz.Visible = ClientArea.VirtualWidth > ClientArea.ClientWidth;
                if (ClientArea.VirtualWidth <= ClientArea.ClientWidth) sbHorz.Value = 0;

// Visibility of the scroll bar has changed?
                if (vis != sbHorz.Visible)
                {
// Hiding the scroll bar now?
                    if (!sbHorz.Visible)
                    {
// Clear all the client area child controls' top modifier values. No scroll bar = No offset.
                        foreach (var c in ClientArea.Controls)
                        {
                            c.LeftModifier = 0;
                            sbVert.Refresh();
                            c.Invalidate();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                        }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                    }
// Update margins to account for the removal of the scroll bar.
                    AdjustMargins();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }

// Adjust the position of the scroll bars.
                PositionScrollBars();
// Clear all the client area child controls' top modifier values. No scroll bar = No offset.
                foreach (var c in ClientArea.Controls)
                {
                    c.LeftModifier = -sbHorz.Value;
                    sbHorz.Refresh();
                    c.Invalidate();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
                }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <summary>
        /// Adjusts the position of the container's horizontal and vertical scroll bars.
        /// </summary>
        private void PositionScrollBars()
        {
            if (sbVert != null)
            {
// container's client area.
// Vertical scroll bar is positioned just to the right of the
                sbVert.Left = ClientLeft + ClientWidth + 1;
                sbVert.Top = ClientTop + 1;
// Shorten the scroll bar slightly if the horizontal scroll bar is also shown.
                var m = (sbHorz != null && sbHorz.Visible) ? 0 : 2;
                sbVert.Height = ClientArea.Height - m;
                sbVert.Range = ClientArea.VirtualHeight;
                sbVert.PageSize = ClientArea.ClientHeight;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }

            if (sbHorz != null)
            {
// container's client area.
// Horizontal scroll bar is positioned just underneath the
                sbHorz.Left = ClientLeft + 1;
                sbHorz.Top = ClientTop + ClientHeight + 1;
// Shorten the scroll bar slightly if the vertical scroll bar is also shown.
                var m = (sbVert != null && sbVert.Visible) ? 0 : 2;
                sbHorz.Width = ClientArea.Width - m;
                sbHorz.Range = ClientArea.VirtualWidth;
                sbHorz.PageSize = ClientArea.ClientWidth;
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
            }
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <summary>
        /// Handles changes in scroll bar values.
        /// </summary>
        private void ScrollBarValueChanged(object sender, EventArgs e)
        {
            CalcScrolling();
//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
        }
    }

//if (sbHorz != null) sbHorz.Value -= 1;
//if (sbVert != null) sbVert.Value -= 1;
// Crappy fix to certain scrolling issue
}

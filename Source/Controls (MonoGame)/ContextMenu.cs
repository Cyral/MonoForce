using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoForce.Controls
{
    /// </summary>
    /// Represents a control's context menu.
    /// <summary>
    public class ContextMenu : MenuBase
    {
        #region Fields
        /// </summary>
        /// Menu delay timer.
        /// <summary>
        private long timer = 0;
        /// </summary>
        /// Control associated with the context menu.
        /// <summary>
        private Control sender = null;
        #endregion

        #region Properties
        /// </summary>
        /// Gets or sets the control requesting the context menu be displayed.
        /// <summary>
        protected internal Control Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        #endregion

        #region Constructor
        /// </summary>
        /// Creates a new context menu control.
        /// <summary>
        /// <param name="manager">GUI manager for the context menu control.</param>
        public ContextMenu(Manager manager)
            : base(manager)
        {
            Visible = false;
            Detached = true;
            StayOnBack = true;

            Manager.Input.MouseDown += new MouseEventHandler(Input_MouseDown);
        }
        #endregion

        #region Destructors
        /// </summary>
        /// Releases resources used by the context menu control.
        /// <summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Manager.Input.MouseDown -= Input_MouseDown;
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Init
        /// </summary>
        /// Initializes the context menu control.
        /// <summary>
        public override void Init()
        {
            base.Init();
        }
        #endregion

        #region Init Skin
        /// </summary>
        /// Initializes the skin of the context menu control.
        /// <summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls["ContextMenu"]);
        }
        #endregion

        #region Draw Control
        /// </summary>
        /// Draws the context menu control.
        /// <summary>
        /// <param name="renderer">Render management object.</param>
        /// <param name="rect">Destination rectangle.</param>
        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        public override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            base.DrawControl(renderer, rect, gameTime);

            SkinLayer l1 = Skin.Layers["Control"];
            SkinLayer l2 = Skin.Layers["Selection"];

            int vsize = LineHeight();
            Color col = Color.White;

            // Draw each context menu entry.
            for (int i = 0; i < Items.Count; i++)
            {
                int mod = i > 0 ? 2 : 0;
                int left = rect.Left + l1.ContentMargins.Left + vsize;
                int h = vsize - mod - (i < (Items.Count - 1) ? 1 : 0);
                int top = rect.Top + l1.ContentMargins.Top + (i * vsize) + mod;


                if (Items[i].Separated && i > 0)
                {
                    Rectangle r = new Rectangle(left, rect.Top + l1.ContentMargins.Top + (i * vsize), LineWidth() - vsize + 4, 1);
                    renderer.Draw(Manager.Skin.Controls["Control"].Layers[0].Image.Resource, r, l1.Text.Colors.Enabled);
                }

                if (ItemIndex != i)
                {
                    if (Items[i].Enabled)
                    {
                        Rectangle r = new Rectangle(left, top, LineWidth() - vsize, h);
                        renderer.DrawString(this, l1, Items[i].Text, r, false, base.DrawFormattedText);
                        col = l1.Text.Colors.Enabled;
                    }
                    else
                    {
                        Rectangle r = new Rectangle(left + l1.Text.OffsetX,
                                                    top + l1.Text.OffsetY,
                                                    LineWidth() - vsize, h);
                        renderer.DrawString(l1.Text.Font.Resource, Items[i].Text, r, l1.Text.Colors.Disabled, l1.Text.Alignment, base.DrawFormattedText);
                        col = l1.Text.Colors.Disabled;
                    }
                }
                else
                {
                    if (Items[i].Enabled)
                    {
                        Rectangle rs = new Rectangle(rect.Left + l1.ContentMargins.Left,
                                                     top,
                                                     Width - (l1.ContentMargins.Horizontal - Skin.OriginMargins.Horizontal),
                                                     h);
                        renderer.DrawLayer(this, l2, rs);

                        Rectangle r = new Rectangle(left,
                                                    top, LineWidth() - vsize, h);

                        renderer.DrawString(this, l2, Items[i].Text, r, false, base.DrawFormattedText);
                        col = l2.Text.Colors.Enabled;
                    }
                    else
                    {
                        Rectangle rs = new Rectangle(rect.Left + l1.ContentMargins.Left,
                                                     top,
                                                     Width - (l1.ContentMargins.Horizontal - Skin.OriginMargins.Horizontal),
                                                     vsize);
                        renderer.DrawLayer(l2, rs, l2.States.Disabled.Color, l2.States.Disabled.Index);

                        Rectangle r = new Rectangle(left + l1.Text.OffsetX,
                                                    top + l1.Text.OffsetY,
                                                    LineWidth() - vsize, h);
                        renderer.DrawString(l2.Text.Font.Resource, Items[i].Text, r, l2.Text.Colors.Disabled, l2.Text.Alignment, base.DrawFormattedText);
                        col = l2.Text.Colors.Disabled;
                    }

                }

                // Menu item has an image icon?
                if (Items[i].Image != null)
                {
                    Rectangle r = new Rectangle(rect.Left + l1.ContentMargins.Left + 3,
                                                rect.Top + top + 3,
                                                LineHeight() - 6,
                                                LineHeight() - 6);
                    renderer.Draw(Items[i].Image, r, Color.White);
                }

                if (Items[i].Items != null && Items[i].Items.Count > 0)
                {
                    renderer.Draw(Manager.Skin.Images["Shared.ArrowRight"].Resource, rect.Left + LineWidth() - 4, rect.Top + l1.ContentMargins.Top + (i * vsize) + 8, col);
                }
            }
        }
        #endregion

        #region Line Height
        /// </summary>
        /// Gets the height of a single entry in the context menu.
        /// <summary>
        /// <returns>Returns the height of a single context menu entry.</returns>
        private int LineHeight()
        {
            int h = 0;
            if (Items.Count > 0)
            {
                SkinLayer l = Skin.Layers["Control"];
                h = (int)l.Text.Font.Resource.LineSpacing + 9;
            }
            return h;
        }
        #endregion

        #region Line Width
        /// </summary>
        /// Gets the width of the longest entry in the context menu.
        /// <summary>
        /// <returns>Returns the width of the longest entry in the context menu.</returns>
        private int LineWidth()
        {
            int w = 0;
            SkinFont font = Skin.Layers["Control"].Text.Font;
            if (Items.Count > 0)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    int wx = (int)font.Resource.MeasureRichString(Items[i].Text, Manager).X + 16;
                    if (wx > w) w = wx;
                }
            }

            w += 4 + LineHeight();

            return w;
        }
        #endregion

        #region Auto Size
        /// </summary>
        /// Calculates the dimensions of the context menu.
        /// <summary>
        private void AutoSize()
        {
            SkinText font = Skin.Layers["Control"].Text;
            if (Items != null && Items.Count > 0)
            {
                Height = (LineHeight() * Items.Count) + (Skin.Layers["Control"].ContentMargins.Vertical - Skin.OriginMargins.Vertical);
                Width = LineWidth() + (Skin.Layers["Control"].ContentMargins.Horizontal - Skin.OriginMargins.Horizontal) + font.OffsetX;
            }
            else
            {
                Height = 16;
                Width = 16;
            }
        }
        #endregion

        #region Track Item
        /// </summary>
        /// Determines which, if any, menu entry of the context menu is at the specified offset and updates
        /// the selected index, and raises the selected event for the new selection if needed.
        /// <summary>
        /// <param name="x">X position of the mouse as an offset from the context menu origins.</param>
        /// <param name="y">Y position of the mouse as an offset from the context menu origins.</param>
        private void TrackItem(int x, int y)
        {
            // Context menu has menu entries?
            if (Items != null && Items.Count > 0)
            {
                SkinText font = Skin.Layers["Control"].Text;
                int h = LineHeight();
                y -= Skin.Layers["Control"].ContentMargins.Top;

                // Y position / line height = Item Index.
                int i = (int)((float)y / h);

                // Item index within the limits of the menu entries list?
                if (i < Items.Count)
                {
                    // Item index is different from previous selection and the new item index is enabled?
                    if (i != ItemIndex && Items[i].Enabled)
                    {
                        // Hide the child menu of the previous selection if there was one.
                        if (ChildMenu != null)
                        {
                            this.HideMenu(false);
                        }

                        // And select the new menu entry.
                        if (i >= 0 && i != ItemIndex)
                        {
                            Items[i].SelectedInvoke(new EventArgs());
                        }

                        // Refocus the context menu, update the selected index, and update the delay timer.
                        Focused = true;
                        ItemIndex = i;
                        timer = (long)TimeSpan.FromTicks(DateTime.Now.Ticks).TotalMilliseconds;
                    }

                    // The new menu entry is not enabled, update the selected index to indicate an invalid selection.
                    else if (!Items[i].Enabled && ChildMenu == null)
                    {
                        ItemIndex = -1;
                    }
                }
                Invalidate();
            }
        }
        #endregion

        #region On Mouse Move Event Handler
        /// </summary>
        /// Handles mouse move events for the context menu.
        /// <summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // Update the selected item index. 
            base.OnMouseMove(e);
            TrackItem(e.Position.X, e.Position.Y);
        }
        #endregion

        #region Update
        /// </summary>
        /// Updates the context menu control.
        /// <summary>
        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        protected internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Calculate the dimensions of the context menu.
            AutoSize();

            // Update the menu delay timer.
            long time = (long)TimeSpan.FromTicks(DateTime.Now.Ticks).TotalMilliseconds;

            if (timer != 0 && time - timer >= Manager.MenuDelay && ItemIndex >= 0 && Items[ItemIndex].Items.Count > 0 && ChildMenu == null)
            {
                OnClick(new MouseEventArgs(new MouseState(), MouseButton.Left, Point.Zero));
            }
        }
        #endregion

        #region On Mouse Out Event Handler
        /// </summary>
        /// Handles mouse out events for the context menu.
        /// <summary>
        /// <param name="e"></param>
        protected override void OnMouseOut(MouseEventArgs e)
        {
            base.OnMouseOut(e);

            // Unset selected item index if the mouse cursor position is outside the 
            // context menu boundaries and there are no child menus displayed.
            if (!CheckArea(e.State.X, e.State.Y) && ChildMenu == null)
            {
                ItemIndex = -1;
            }
        }
        #endregion

        #region On Click Event Handler
        /// </summary>
        /// Handles mouse click events for the context menu.
        /// <summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            if (sender != null && !(sender is MenuBase)) sender.Focused = true;
            base.OnClick(e);
            timer = 0;

            MouseEventArgs ex = (e is MouseEventArgs) ? (MouseEventArgs)e : new MouseEventArgs();

            // Left button click or phantom click event from key/gamepad press event handlers?
            if (ex.Button == MouseButton.Left || ex.Button == MouseButton.None)
            {
                // Context menu entry is selected and enabled?
                if (ItemIndex >= 0 && Items[ItemIndex].Enabled)
                {
                    // Context menu entry has a child menu?
                    if (ItemIndex >= 0 && Items[ItemIndex].Items != null && Items[ItemIndex].Items.Count > 0)
                    {
                        // Yes, need to create the child menu first?
                        if (ChildMenu == null)
                        {
                            ChildMenu = new ContextMenu(Manager);
                            (ChildMenu as ContextMenu).RootMenu = this.RootMenu;
                            (ChildMenu as ContextMenu).ParentMenu = this;
                            (ChildMenu as ContextMenu).sender = sender;
                            ChildMenu.Items.AddRange(Items[ItemIndex].Items);
                            (ChildMenu as ContextMenu).AutoSize();
                        }

                        // Position and display the child menu. 
                        int y = AbsoluteTop + Skin.Layers["Control"].ContentMargins.Top + (ItemIndex * LineHeight());
                        (ChildMenu as ContextMenu).Show(sender, AbsoluteLeft + Width - 1, y);

                        // Select the first menu entry of the child menu.
                        if (ex.Button == MouseButton.None) (ChildMenu as ContextMenu).ItemIndex = 0;
                    }

                    else
                    {
                        // No child menu. Fire the menu entry's click event.
                        if (ItemIndex >= 0)
                        {
                            Items[ItemIndex].ClickInvoke(ex);
                        }

                        // Hide the menu.
                        if (RootMenu is ContextMenu)
                        {
                            (RootMenu as ContextMenu).HideMenu(true);
                        }

                        else if (RootMenu is MainMenu)
                        {
                            (RootMenu as MainMenu).HideMenu();
                        }
                    }
                }
            }
        }
        #endregion

        #region On Key Press Event Handler
        /// </summary>
        /// Handles key press events for the context menu.
        /// <summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyEventArgs e)
        {
            base.OnKeyPress(e);

            timer = 0;

            // Select next menu entry if the down arrow key or Tab is pressed.
            if (e.Key == Keys.Down || (e.Key == Keys.Tab && !e.Shift))
            {
                e.Handled = true;
                ItemIndex += 1;
            }

            // Select previous menu entry if the up arrow key or Shift+Tab is pressed.
            if (e.Key == Keys.Up || (e.Key == Keys.Tab && e.Shift))
            {
                e.Handled = true;
                ItemIndex -= 1;
            }

            // Wrap the index in range.
            if (ItemIndex > Items.Count - 1) ItemIndex = 0;
            if (ItemIndex < 0) ItemIndex = Items.Count - 1;

            // Open up the child menu, if there is one, when the right arrow key is pressed.
            if (e.Key == Keys.Right && Items[ItemIndex].Items.Count > 0)
            {
                e.Handled = true;
                OnClick(new MouseEventArgs(new MouseState(), MouseButton.None, Point.Zero));
            }

            // Move up a menu level if there is a parent menu when the left arrow key is pressed.
            if (e.Key == Keys.Left)
            {
                e.Handled = true;
                if (ParentMenu != null && ParentMenu is ContextMenu)
                {
                    (ParentMenu as ContextMenu).Focused = true;
                    (ParentMenu as ContextMenu).HideMenu(false);
                }
            }

            // Close the context menu when escape is pressed.
            if (e.Key == Keys.Escape)
            {
                e.Handled = true;
                if (ParentMenu != null) ParentMenu.Focused = true;
                HideMenu(true);
            }
        }
        #endregion

        #region On Game Pad Press Event Handler
        /// </summary>
        /// Handles button press events for the context menu.
        /// <summary>
        /// <param name="e"></param>
        protected override void OnGamePadPress(GamePadEventArgs e)
        {
            timer = 0;

            if (e.Button == GamePadButton.None) return;

            // Move to the next menu entry on Down or Right Shoulder button presses.
            if (e.Button == GamePadActions.Down || e.Button == GamePadActions.NextControl)
            {
                e.Handled = true;
                ItemIndex += 1;
            }

            // Move to the previous menu entry on Up or Left Shoulder button presses.
            else if (e.Button == GamePadActions.Up || e.Button == GamePadActions.PrevControl)
            {
                e.Handled = true;
                ItemIndex -= 1;
            }

            // Wrap selected index in range.
            if (ItemIndex > Items.Count - 1) ItemIndex = 0;
            if (ItemIndex < 0) ItemIndex = Items.Count - 1;

            // Open child menu, if there is one, when the Right button is pressed.
            if (e.Button == GamePadActions.Right && Items[ItemIndex].Items.Count > 0)
            {
                e.Handled = true;
                OnClick(new MouseEventArgs(new MouseState(), MouseButton.None, Point.Zero));
            }

            // Close child menu, if there is one, when the Left button is pressed.
            if (e.Button == GamePadActions.Left)
            {
                e.Handled = true;
                if (ParentMenu != null && ParentMenu is ContextMenu)
                {
                    (ParentMenu as ContextMenu).Focused = true;
                    (ParentMenu as ContextMenu).HideMenu(false);
                }
            }

            base.OnGamePadPress(e);
        }
        #endregion

        #region Hide Menu
        /// </summary>
        /// Hides the context menu or one of its child menus.
        /// <summary>
        /// <param name="hideCurrent">Indicates if the context menu should be hidden (true) or if only the child menu should be hidden. </param>
        public virtual void HideMenu(bool hideCurrent)
        {
            // Hide this menu?
            if (hideCurrent)
            {
                Visible = false;
                ItemIndex = -1;
            }

            // Need to clean up child menu?
            if (ChildMenu != null)
            {
                // Hide child and destroy it.
                (ChildMenu as ContextMenu).HideMenu(true);
                ChildMenu.Dispose();
                ChildMenu = null;
            }
        }
        #endregion

        #region Show
        /// </summary>
        /// Displays the context menu.
        /// <summary>
        public override void Show()
        {
            Show(null, Left, Top);
        }

        /// </summary>
        /// Displays the menu at the specified position.
        /// <summary>
        /// <param name="sender">Control requesting the context menu is displayed.</param>
        /// <param name="x">X position to display the context menu.</param>
        /// <param name="y">Y position to display the context menu.</param>
        public virtual void Show(Control sender, int x, int y)
        {
            // Calculate the dimenstions of the context menu.
            AutoSize();
            base.Show();

            // Initialize the context menu if needed.
            if (!Initialized)
            {
                Init();
            }

            // Sender has a Root container control specified?
            if (sender != null && sender.Root != null && sender.Root is Container)
            {
                // Add the context menu to the root container control list.
                (sender.Root as Container).Add(this, false);
            }
            else
            {
                // No root container, just add the context menu to the GUI manager's control list.
                Manager.Add(this);
            }

            this.sender = sender;

            // Adjust the context menu's position if it's now the child of a root container.
            if (sender != null && sender.Root != null && sender.Root is Container)
            {
                Left = x - Root.AbsoluteLeft;
                Top = y - Root.AbsoluteTop;
            }

            // Otherwise, just set it at the specified position.
            else
            {
                Left = x;
                Top = y;
            }

            // Make sure the context menu stays within the render target area.
            if (AbsoluteLeft + Width > Manager.TargetWidth)
            {
                Left = Left - Width;

                if (ParentMenu != null && ParentMenu is ContextMenu)
                {
                    Left = Left - ParentMenu.Width + 2;
                }
                else if (ParentMenu != null)
                {
                    Left = Manager.TargetWidth - (Parent != null ? Parent.AbsoluteLeft : 0) - Width - 2;
                }
            }

            if (AbsoluteTop + Height > Manager.TargetHeight)
            {
                Top = Top - Height;
                if (ParentMenu != null && ParentMenu is ContextMenu)
                {
                    Top = Top + LineHeight();
                }
                else if (ParentMenu != null)
                {
                    Top = ParentMenu.Top - Height - 1;
                }
            }

            // Give the context menu input focus.
            Focused = true;
        }
        #endregion

        #region Input Mouse Down Event Handler
        /// </summary>
        /// Handles mouse button down events for the context menu.
        /// <summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Input_MouseDown(object sender, MouseEventArgs e)
        {
            // Child menu displayed and parent menu clicked?
            if ((RootMenu is ContextMenu) && !(RootMenu as ContextMenu).CheckArea(e.Position.X, e.Position.Y) && Visible)
            {
                // Hide child menu.
                HideMenu(true);
            }

            // Parent is main menu and click is outside all menu areas?
            else if ((RootMenu is MainMenu) && RootMenu.ChildMenu != null && !(RootMenu.ChildMenu as ContextMenu).CheckArea(e.Position.X, e.Position.Y) && Visible)
            {
                // Hide main menu.
                (RootMenu as MainMenu).HideMenu();
            }
        }
        #endregion

        #region Check Area
        /// </summary>
        /// Determines if the specified point lies within the context menu or one of its child menus.
        /// <summary>
        /// <param name="x">X position to check.</param>
        /// <param name="y">Y position to check.</param>
        /// <returns>Returns true if the specified point is within the bounds of the context menu or one of its child menus.
        /// Returns false if the context menu is hidden or if the specified point is outside of the context menu boundaries.</returns>
        private bool CheckArea(int x, int y)
        {
            // Context menu is visible?
            if (Visible)
            {
                // Position specified is outside the bounds of the context menu?
                if (x <= AbsoluteLeft || x >= AbsoluteLeft + Width ||
                    y <= AbsoluteTop || y >= AbsoluteTop + Height)
                {
                    // The position does not lie within this context menu.
                    bool ret = false;

                    // Check for a hit on a child menu, if there is one.
                    if (ChildMenu != null)
                    {
                        ret = (ChildMenu as ContextMenu).CheckArea(x, y);
                    }

                    return ret;
                }

                // Position is within the bounds of this context menu.
                else
                {
                    return true;
                }
            }

            // Context menu is not even displayed.
            else
            {
                return false;
            }
        }
        #endregion
    }
}

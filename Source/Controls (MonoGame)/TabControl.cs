using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForce.Controls
{
    public class TabControlGamePadActions : GamePadActions
    {
        /// </summary>
        /// Button used to switch to the next tab. (RightTrigger)
        /// <summary>
        public GamePadButton NextTab = GamePadButton.RightTrigger;

        /// </summary>
        /// Button used to switch to the previous tab. (LeftTrigger)
        /// <summary>
        public GamePadButton PrevTab = GamePadButton.LeftTrigger;
    }

    public class TabPage : Control
    {
        /// </summary>
        /// Gets the header region of the tab page.
        /// <summary>
        protected internal Rectangle HeaderRect
        {
            get { return headerRect; }
        }

        /// </summary>
        /// Defines the header region of the tab page.
        /// <summary>
        private Rectangle headerRect = Rectangle.Empty;

        public TabPage(Manager manager) : base(manager)
        {
            Color = Color.Transparent;
            Passive = true;
            CanFocus = false;
        }

        /// <param name="first">Indicates if this is the first tab page header.</param>
        /// <param name="offset">Offset to apply from previous tab.</param>
        /// <param name="margins">Tab header content margins.</param>
        /// <param name="font">Font used to draw the header text.</param>
        /// <param name="prev">Header region of the previous tab page control.</param>
        /// </summary>
        /// Calculates the region where the tab page header will be displayed.
        /// <summary>
        protected internal void CalcRect(Rectangle prev, SpriteFont font, Margins margins, Point offset, bool first)
        {
            var size = (int)Math.Ceiling(font.MeasureString(Text).X) + margins.Horizontal;

            if (first) offset.X = 0;

// Set the header region.
            headerRect = new Rectangle(prev.Right + offset.X, prev.Top, size, prev.Height);
        }
    }

    public class TabControl : Container
    {
        /// </summary>
        /// Gets or sets the index of the selected tab page.
        /// <summary>
        public virtual int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (selectedIndex >= 0 && selectedIndex < tabPages.Count && value >= 0 && value < tabPages.Count)
                {
                    TabPages[selectedIndex].Visible = false;
                }
                if (value >= 0 && value < tabPages.Count)
                {
                    TabPages[value].Visible = true;
                    var c = TabPages[value].Controls as ControlsList;
                    if (c.Count > 0) c[0].Focused = true;
                    selectedIndex = value;
                    if (!Suspended) OnPageChanged(new EventArgs());
                }
            }
        }

        /// </summary>
        /// Gets or sets the selected tab page.
        /// <summary>
        public virtual TabPage SelectedPage
        {
            get { return tabPages[SelectedIndex]; }
            set
            {
// See if any of the tab page headers were clicked.
                for (var i = 0; i < tabPages.Count; i++)
                {
                    if (tabPages[i] == value)
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        /// </summary>
        /// Returns the list of tab pages belonging to the tab control as an array.
        /// <summary>
        public TabPage[] TabPages
        {
            get { return tabPages.ToArray(); }
        }

        /// </summary>
        /// List of tab pages that make up the tab control.
        /// <summary>
        private readonly List<TabPage> tabPages = new List<TabPage>();

        /// </summary>
        /// Index of the tab page header hovered by the mouse, if any.
        /// <summary>
        private int hoveredIndex = -1;

        /// </summary>
        /// Index of the selected tab page.
        /// <summary>
        private int selectedIndex;

        public TabControl(Manager manager) : base(manager)
        {
            GamePadActions = new TabControlGamePadActions();
            Manager.Input.GamePadDown += Input_GamePadDown;
            CanFocus = false;
        }

        /// <returns>Returns the created tab page.</returns>
        /// <param name="text">Tab page header text.</param>
        /// </summary>
        /// Creates a tab page with the specified header text and adds it to the tab control.
        /// <summary>
        public virtual TabPage AddPage(string text)
        {
            var p = AddPage();
            p.Text = text;

            return p;
        }

        /// <returns>Returns the created tab page.</returns>
        /// </summary>
        /// Creates a tab page with the default header text and adds it to the tab control.
        /// <summary>
        public virtual TabPage AddPage()
        {
            var page = new TabPage(Manager);
            page.Init();
            page.Left = 0;
            page.Top = 0;
            page.Width = ClientWidth;
            page.Height = ClientHeight;
            page.Anchor = Anchors.All;
            page.Text = "Tab " + (tabPages.Count + 1);
            page.Visible = false;
            Add(page, true);
            tabPages.Add(page);
            tabPages[0].Visible = true;

            return page;
        }

        /// </summary>
        /// Initializes the tab control.
        /// <summary>
        public override void Init()
        {
            base.Init();
        }

        /// </summary>
        /// Occurs when the selected tab page changes.
        /// <summary>
        public event EventHandler PageChanged;

        /// <param name="dispose">Indicates if the tab page should be disposed after removal.</param>
        /// <param name="page">Tab page to remove from the tab control.</param>
        /// </summary>
        /// Removes the specified tab page from the tab control and disposes it (if specified.)
        /// <summary>
        public virtual void RemovePage(TabPage page, bool dispose)
        {
            tabPages.Remove(page);
            if (dispose)
            {
                page.Dispose();
                page = null;
            }
            SelectedIndex = 0;
        }

        /// <param name="page">Tab page to remove from the tab control.</param>
        /// </summary>
        /// Removes the specified tab page from the control.
        /// <summary>
        public virtual void RemovePage(TabPage page)
        {
            RemovePage(page, true);
        }

        /// </summary>
        /// Initializes the skin of the tab control.
        /// <summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
        }

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            var l1 = Skin.Layers["Control"];
            var l2 = Skin.Layers["Header"];
            var col = Color != UndefinedColor ? Color : Color.White;

            var r1 = new Rectangle(rect.Left, rect.Top + l1.OffsetY, rect.Width, rect.Height - l1.OffsetY);
            if (tabPages.Count <= 0)
            {
                r1 = rect;
            }

            base.DrawControl(renderer, r1, gameTime);

// Has tab pages to draw?
            if (tabPages.Count > 0)
            {
                var prev = new Rectangle(rect.Left, rect.Top + l2.OffsetY, 0, l2.Height);
// See if any of the tab page headers were clicked.
                for (var i = 0; i < tabPages.Count; i++)
                {
                    var font = l2.Text.Font.Resource;
                    var margins = l2.ContentMargins;
                    var offset = new Point(l2.OffsetX, l2.OffsetY);
                    if (i > 0) prev = tabPages[i - 1].HeaderRect;

                    tabPages[i].CalcRect(prev, font, margins, offset, i == 0);
                }

                for (var i = tabPages.Count - 1; i >= 0; i--)
                {
// Get the layer color and index for the current tab page.
                    var li = tabPages[i].Enabled ? l2.States.Enabled.Index : l2.States.Disabled.Index;
                    var lc = tabPages[i].Enabled ? l2.Text.Colors.Enabled : l2.Text.Colors.Disabled;
// Is the current tab page header hovered?
                    if (i == hoveredIndex)
                    {
// Update index and color values.
                        li = l2.States.Hovered.Index;
                        lc = l2.Text.Colors.Hovered;
                    }


// Calculate the region where text is displayed in the header, respecting content margin values.
                    var m = l2.ContentMargins;
                    var rx = tabPages[i].HeaderRect;
                    var sx = new Rectangle(rx.Left + m.Left, rx.Top + m.Top, rx.Width - m.Horizontal,
                        rx.Height - m.Vertical);
// Draw the header for the unselected tab pages.
                    if (i != selectedIndex)
                    {
                        renderer.DrawLayer(l2, rx, col, li);
                        renderer.DrawString(l2.Text.Font.Resource, tabPages[i].Text, sx, lc, l2.Text.Alignment);
                    }
                }

// Calculate the region where text is displayed in the header, respecting content margin values.
                var mi = l2.ContentMargins;
                var ri = tabPages[selectedIndex].HeaderRect;
                var si = new Rectangle(ri.Left + mi.Left, ri.Top + mi.Top, ri.Width - mi.Horizontal,
                    ri.Height - mi.Vertical);
// Draw the header for the selected tab page.
                renderer.DrawLayer(l2, ri, col, l2.States.Focused.Index);
                renderer.DrawString(l2.Text.Font.Resource, tabPages[selectedIndex].Text, si, l2.Text.Colors.Focused,
                    l2.Text.Alignment, l2.Text.OffsetX, l2.Text.OffsetY, false);
            }
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles mouse down events for the tab control.
        /// <summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

// More than one tab page?
            if (tabPages.Count > 1)
            {
// Convert mouse position to relative offset.
                var p = new Point(e.State.X - Root.AbsoluteLeft, e.State.Y - Root.AbsoluteTop);
// See if any of the tab page headers were clicked.
                for (var i = 0; i < tabPages.Count; i++)
                {
                    var r = tabPages[i].HeaderRect;
// Select page if mouse position is within header.
                    if (p.X >= r.Left && p.X <= r.Right && p.Y >= r.Top && p.Y <= r.Bottom)
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles mouse move events for the tab control.
        /// <summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
// More than one tab page?
            if (tabPages.Count > 1)
            {
                var index = hoveredIndex;
// Convert mouse position to relative offset.
                var p = new Point(e.State.X - Root.AbsoluteLeft, e.State.Y - Root.AbsoluteTop);
// See if any of the tab page headers were clicked.
                for (var i = 0; i < tabPages.Count; i++)
                {
                    var r = tabPages[i].HeaderRect;
// Mouse is within the current header region?
                    if (p.X >= r.Left && p.X <= r.Right && p.Y >= r.Top && p.Y <= r.Bottom && tabPages[i].Enabled)
                    {
                        index = i;
                        break;
                    }
                    index = -1;
                }
// Update the hovered tab page header index?
                if (index != hoveredIndex)
                {
                    hoveredIndex = index;
                    Invalidate();
                }
            }
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handler for when a new tab page is selected.
        /// <summary>
        protected virtual void OnPageChanged(EventArgs e)
        {
            if (PageChanged != null) PageChanged.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles gamepad input for the tab control.
        /// <summary>
        private void Input_GamePadDown(object sender, GamePadEventArgs e)
        {
// Tab control has focus?
            if (Contains(Manager.FocusedControl, true))
            {
// Switch to the next tab page on RightTrigger presses.
                if (e.Button == (GamePadActions as TabControlGamePadActions).NextTab)
                {
                    e.Handled = true;
                    SelectedIndex += 1;
                }
// Switch to the previous tab page on LeftTrigger presses.
                else if (e.Button == (GamePadActions as TabControlGamePadActions).PrevTab)
                {
                    e.Handled = true;
                    SelectedIndex -= 1;
                }
            }
        }
    }
}
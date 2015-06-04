//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

namespace MonoForce.Controls
//if (itemIndex != value)
{
//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

    /// <summary>
    /// Represents a list box control. Single selection only.
    /// </summary>
    public class ListBox : Control
//if (itemIndex != value)
    {
//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        /// <summary>
        /// Indicates if the selection overlay should be drawn over the selected item.
        /// </summary>
        public virtual bool HideSelection
//if (itemIndex != value)
        {
            get { return hideSelection; }
            set
//if (itemIndex != value)
            {
                if (hideSelection != value)
//if (itemIndex != value)
                {
                    hideSelection = value;
                    Invalidate();
                    if (!Suspended) OnHideSelectionChanged(new EventArgs());
                }
            }
        }

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        /// <summary>
        /// Indicates if mouse over event can update the selected index.
        /// </summary>
        public virtual bool HotTrack
//if (itemIndex != value)
        {
            get { return hotTrack; }
            set
//if (itemIndex != value)
            {
                if (hotTrack != value)
//if (itemIndex != value)
                {
                    hotTrack = value;
                    if (!Suspended) OnHotTrackChanged(new EventArgs());
                }
            }
        }

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        /// <summary>
        /// Gets or sets the index of the selected list box item.
        /// </summary>
        public virtual int ItemIndex
//if (itemIndex != value)
        {
            get { return itemIndex; }
            set
//if (itemIndex != value)
            {
//if (itemIndex != value)
                {
                    if (value >= 0 && value < items.Count)
//if (itemIndex != value)
                    {
                        itemIndex = value;
                    }
// Empty collection. Default height to 32.
                    else
//if (itemIndex != value)
                    {
                        itemIndex = -1;
                    }
                    ScrollTo(itemIndex);
//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

                    if (!Suspended) OnItemIndexChanged(new EventArgs());
                }
            }
        }

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        /// <summary>
        /// Gets the list of items.
        /// </summary>
        public virtual List<object> Items
//if (itemIndex != value)
        {
            get { return items; }
            internal set { items = value; }
        }

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        public override int MinimumHeight
        {
            get { return base.MinimumHeight; }
            set
            {
                base.MinimumHeight = value;
                if (sbVert != null) sbVert.MinimumHeight = value;
            }
        }

        private readonly ClipBox pane;
        private readonly ScrollBar sbVert;
        private bool hideSelection = true;
        private bool hotTrack;
        private int itemIndex = -1;
//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        private List<object> items = new List<object>();
        private int itemsCount;
//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        /// <param name="manager">GUI manager for the control.</param>
        /// <summary>
        /// Creates a new ListBox control.
        /// </summary>
        public ListBox(Manager manager)
            : base(manager)
//if (itemIndex != value)
        {
            Width = 64;
            Height = 64;
            MinimumHeight = 16;
//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

// Set up the scroll bar.
            sbVert = new ScrollBar(Manager, Orientation.Vertical);
            sbVert.Init();
            sbVert.Parent = this;
            sbVert.Left = Left + Width - sbVert.Width - Skin.Layers["Control"].ContentMargins.Right;
            sbVert.Top = Top + Skin.Layers["Control"].ContentMargins.Top;
            sbVert.Height = Height - Skin.Layers["Control"].ContentMargins.Vertical;
            sbVert.Anchor = Anchors.Top | Anchors.Right | Anchors.Bottom;
            sbVert.PageSize = 25;
            sbVert.Range = 1;
            sbVert.PageSize = 1;
            sbVert.StepSize = 10;
//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

// Set up the clip box.
            pane = new ClipBox(manager);
            pane.Init();
            pane.Parent = this;
            pane.Top = Skin.Layers["Control"].ContentMargins.Top;
            pane.Left = Skin.Layers["Control"].ContentMargins.Left;
            pane.Width = Width - sbVert.Width - Skin.Layers["Control"].ContentMargins.Horizontal - 1;
            pane.Height = Height - Skin.Layers["Control"].ContentMargins.Vertical;
            pane.Anchor = Anchors.All;
            pane.Passive = true;
            pane.CanFocus = false;
            pane.Draw += DrawPane;
//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

            CanFocus = true;
            Passive = false;
        }

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        /// <param name="maxItems">Number of items that can be displayed without needing a scroll bar.</param>
        /// <summary>
        /// displayed in it without needing a scroll bar.
        /// Sizes the list pane so the specified number of items will be able to be
        /// </summary>
        public virtual void AutoHeight(int maxItems)
        {
// Collection has less than the maximum items specified?
            if (items != null && items.Count < maxItems) maxItems = items.Count;
// Adjust width of the pane to account for scroll bar visibility.
            if (maxItems < 3)
            {
//maxItems = 3;
                sbVert.Visible = false;
                pane.Width = Width - Skin.Layers["Control"].ContentMargins.Horizontal - 1;
            }
// Empty collection. Default height to 32.
            else
            {
                pane.Width = Width - sbVert.Width - Skin.Layers["Control"].ContentMargins.Horizontal - 1;
                sbVert.Visible = true;
            }
//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

// Get the list box font resource.
            var font = Skin.Layers["Control"].Text;
// should be able to display in it.
// height of the list pane based on the specified number of items that
// Non-empty collection? Measure the height of a line of font and set the
            if (items != null && items.Count > 0)
            {
                var h = (int)font.Font.Resource.MeasureString(items[0].ToString()).Y;
                Height = (h * maxItems) + (Skin.Layers["Control"].ContentMargins.Vertical);
                    // - Skin.OriginMargins.Vertical);
            }
// Empty collection. Default height to 32.
            else
            {
                Height = 32;
            }
        }

        /// <summary>
        /// Occurs when the hide selection value changes.
        /// </summary>
        public event EventHandler HideSelectionChanged;

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        /// <summary>
        /// Occurs when the hot tracking value changes.
        /// </summary>
        public event EventHandler HotTrackChanged;

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        /// <summary>
        /// Initializes the list box control.
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// Occurs when the selected item index changes.
        /// </summary>
        public event EventHandler ItemIndexChanged;

        /// <param name="index">Index to scroll to.</param>
        /// <summary>
        /// specified list item is visible in the list box.
        /// Adjusts the scroll bar value to make sure the
        /// </summary>
        public virtual void ScrollTo(int index)
        {
            ItemsChanged();
// Need to scroll up?
            if ((index * 10) < sbVert.Value)
            {
                sbVert.Value = index * 10;
            }
// Need to scroll down?
            else if (index >= (int)Math.Floor(((float)sbVert.Value + sbVert.PageSize) / 10f))
            {
                sbVert.Value = ((index + 1) * 10) - sbVert.PageSize;
            }
        }

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <summary>
        /// Updates the state of the list box and watches for changes in list size.
        /// </summary>
        protected internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

// List box is visible and list size has changed?
            if (Visible && items != null && items.Count != itemsCount)
            {
// Update count and adjust its scroll bar.
                itemsCount = items.Count;
                ItemsChanged();
            }
        }

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            sbVert.Invalidate();
            pane.Invalidate();
//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

            base.DrawControl(renderer, rect, gameTime);
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles gamepad button presses for the list box. Specifically, the up and down buttons.
        /// </summary>
        protected override void OnGamePadPress(GamePadEventArgs e)
        {
// Move selection down?
            if (e.Button == GamePadActions.Down)
            {
                e.Handled = true;
                itemIndex += sbVert.StepSize / 10;
            }
// Move selection up?
            else if (e.Button == GamePadActions.Up)
            {
                e.Handled = true;
                itemIndex -= sbVert.StepSize / 10;
            }

// Wrap index in collection range.
            if (itemIndex < 0) itemIndex = 0;
            else if (itemIndex >= Items.Count) itemIndex = Items.Count - 1;

            ItemIndex = itemIndex;
            base.OnGamePadPress(e);
        }

        /// <param name="e"></param>
        /// <summary>
        /// Raises the HideSelectionChanged event.
        /// </summary>
        protected virtual void OnHideSelectionChanged(EventArgs e)
        {
            if (HideSelectionChanged != null) HideSelectionChanged.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// <summary>
        /// Raises the HotTrackChanged event.
        /// </summary>
        protected virtual void OnHotTrackChanged(EventArgs e)
        {
            if (HotTrackChanged != null) HotTrackChanged.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// <summary>
        /// Raises the ItemIndexChangedEvent.
        /// </summary>
        protected virtual void OnItemIndexChanged(EventArgs e)
        {
            if (ItemIndexChanged != null) ItemIndexChanged.Invoke(this, e);
        }

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        /// <param name="e"></param>
        /// <summary>
        /// Handles key press events for the list box.
        /// </summary>
        protected override void OnKeyPress(KeyEventArgs e)
        {
// Scroll down?
            if (e.Key == Keys.Down)
            {
                e.Handled = true;
                itemIndex += sbVert.StepSize / 10;
            }
// Scroll up?
            else if (e.Key == Keys.Up)
            {
                e.Handled = true;
                itemIndex -= sbVert.StepSize / 10;
            }
// Page down?
            else if (e.Key == Keys.PageDown)
            {
                e.Handled = true;
                itemIndex += sbVert.PageSize / 10;
            }
// Page up?
            else if (e.Key == Keys.PageUp)
            {
                e.Handled = true;
                itemIndex -= sbVert.PageSize / 10;
            }
// Scroll to top of list?
            else if (e.Key == Keys.Home)
            {
                e.Handled = true;
                itemIndex = 0;
            }
// Scroll to bottom of list?
            else if (e.Key == Keys.End)
            {
                e.Handled = true;
                itemIndex = items.Count - 1;
            }

// Wrap index in collection range.
            if (itemIndex < 0) itemIndex = 0;
            else if (itemIndex >= Items.Count) itemIndex = Items.Count - 1;

            ItemIndex = itemIndex;

            base.OnKeyPress(e);
        }

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        /// <param name="e"></param>
        /// <summary>
        /// Handles mouse button down events for the list box.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

// Need to update the selected item?
            if (e.Button == MouseButton.Left || e.Button == MouseButton.Right)
            {
                TrackItem(e.Position.X, e.Position.Y);
            }
        }

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        /// <param name="e"></param>
        /// <summary>
        /// Handles mouse move events for the list box.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

// Update selection?
            if (hotTrack)
            {
                TrackItem(e.Position.X, e.Position.Y);
            }
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles mouse scroll events for the list box.
        /// </summary>
        protected override void OnMouseScroll(MouseEventArgs e)
        {
            Focused = true;

            if (e.ScrollDirection == MouseScrollDirection.Down)
            {
                e.Handled = true;
                itemIndex += sbVert.StepSize / 10;
            }
            else if (e.ScrollDirection == MouseScrollDirection.Up)
            {
                e.Handled = true;
                itemIndex -= sbVert.StepSize / 10;
            }

// Wrap index in collection range.
            if (itemIndex < 0) itemIndex = 0;
            else if (itemIndex >= Items.Count) itemIndex = Items.Count - 1;

            ItemIndex = itemIndex;

            base.OnMouseScroll(e);
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles resizing of the list box.
        /// </summary>
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            ItemsChanged();
        }

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        private void DrawPane(object sender, DrawEventArgs e)
        {
// should be able to display in it.
// height of the list pane based on the specified number of items that
// Non-empty collection? Measure the height of a line of font and set the
            if (items != null && items.Count > 0)
            {
// Get the list box font resource.
                var font = Skin.Layers["Control"].Text;
                var sel = Skin.Layers["ListBox.Selection"];
                var h = (int)font.Font.Resource.MeasureString(items[0].ToString()).Y;
                var v = (sbVert.Value / 10);
                var p = (sbVert.PageSize / 10);
                var d = (int)(((sbVert.Value % 10) / 10f) * h);
                var c = items.Count;
                var s = itemIndex;
//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

// Draw the visible collection items in the list pane.
                for (var i = v; i <= v + p + 1; i++)
                {
                    if (i < c)
                    {
                        e.Renderer.DrawString(this, Skin.Layers["Control"], items[i].ToString(),
                            new Rectangle(e.Rectangle.Left, e.Rectangle.Top - d + ((i - v) * h), e.Rectangle.Width, h),
                            false);
                    }
                }
// Draw selection overlay?
                if (s >= 0 && s < c && (Focused || !hideSelection))
                {
                    var pos = -d + ((s - v) * h);
// Selected index is visible?
                    if (pos > -h && pos < (p + 1) * h)
                    {
                        e.Renderer.DrawLayer(this, sel,
                            new Rectangle(e.Rectangle.Left, e.Rectangle.Top + pos, e.Rectangle.Width, h));
                        e.Renderer.DrawString(this, sel, items[s].ToString(),
                            new Rectangle(e.Rectangle.Left, e.Rectangle.Top + pos, e.Rectangle.Width, h), false);
                    }
                }
            }
        }

        private void ItemsChanged()
        {
// List box collection is non-empty?
            if (items != null && items.Count > 0)
            {
// Get the height of a list entry.
                var font = Skin.Layers["Control"].Text;
                var h = (int)font.Font.Resource.MeasureString(items[0].ToString()).Y;

// Get the height of the list box content area.
                var sizev = Height - Skin.Layers["Control"].ContentMargins.Vertical;
// Set up scroll values.
                sbVert.Range = items.Count * 10;
                sbVert.PageSize = (int)Math.Floor((float)sizev * 10 / h);
                Invalidate();
            }
// List box is empty, reset scroll bar values.
            else if (items == null || items.Count <= 0)
            {
                sbVert.Range = 1;
                sbVert.PageSize = 1;
                Invalidate();
            }
        }

//DrawPane(this, new DrawEventArgs(renderer, rect, gameTime));

        /// <param name="y">Mouse Y position.</param>
        /// <param name="x">Mouse X position.</param>
        /// <summary>
        /// Updates the list box selection when the mouse moves over one.
        /// </summary>
        private void TrackItem(int x, int y)
        {
// Collection is non-empty and position is within the list?
            if (items != null && items.Count > 0 && (pane.ControlRect.Contains(new Point(x, y))))
            {
// Get the list box font resource.
                var font = Skin.Layers["Control"].Text;
                var h = (int)font.Font.Resource.MeasureString(items[0].ToString()).Y;
                var d = (int)(((sbVert.Value % 10) / 10f) * h);
                var i = (int)Math.Floor((sbVert.Value / 10f) + ((float)y / h));
                if (i >= 0 && i < Items.Count && i >= (int)Math.Floor(sbVert.Value / 10f) &&
                    i < (int)Math.Ceiling((sbVert.Value + sbVert.PageSize) / 10f)) ItemIndex = i;
                Focused = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoForce.Controls

{
    /// </summary>
    /// Represents a list box control. Single selection only.
    /// <summary>
    public class ListBox : Control

    {
        /// </summary>
        /// Indicates if the selection overlay should be drawn over the selected item.
        /// <summary>
        public virtual bool HideSelection

        {
            get { return hideSelection; }
            set

            {
                if (hideSelection != value)

                {
                    hideSelection = value;
                    Invalidate();
                    if (!Suspended) OnHideSelectionChanged(new EventArgs());
                }
            }
        }

        /// </summary>
        /// Indicates if mouse over event can update the selected index.
        /// <summary>
        public virtual bool HotTrack

        {
            get { return hotTrack; }
            set

            {
                if (hotTrack != value)

                {
                    hotTrack = value;
                    if (!Suspended) OnHotTrackChanged(new EventArgs());
                }
            }
        }

        /// </summary>
        /// Gets or sets the index of the selected list box item.
        /// <summary>
        public virtual int ItemIndex

        {
            get { return itemIndex; }
            set

            {

                {
                    if (value >= 0 && value < items.Count)

                    {
                        itemIndex = value;
                    }
// Empty collection. Default height to 32.
                    else

                    {
                        itemIndex = -1;
                    }
                    ScrollTo(itemIndex);


                    if (!Suspended) OnItemIndexChanged(new EventArgs());
                }
            }
        }

        /// </summary>
        /// Gets the list of items.
        /// <summary>
        public virtual List<object> Items

        {
            get { return items; }
            internal set { items = value; }
        }

        public override int MinimumHeight
        {
            get { return base.MinimumHeight; }
            set
            {
                base.MinimumHeight = value;
                if (sbVert != null) sbVert.MinimumHeight = value;
            }
        }

        protected readonly ScrollBar sbVert;
        protected readonly ClipBox pane;
        protected bool hideSelection = true;
        protected bool hotTrack;
        protected int itemIndex = -1;
        protected List<object> items = new List<object>();
        private int itemsCount;

        /// <param name="manager">GUI manager for the control.</param>
        /// </summary>
        /// Creates a new ListBox control.
        /// <summary>
        public ListBox(Manager manager)
            : base(manager)

        {
            Width = 64;
            Height = 64;
            MinimumHeight = 16;


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


            CanFocus = true;
            Passive = false;
        }

        /// <param name="maxItems">Number of items that can be displayed without needing a scroll bar.</param>
        /// </summary>
        /// displayed in it without needing a scroll bar.
        /// Sizes the list pane so the specified number of items will be able to be
        /// <summary>
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

        public override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            sbVert.Invalidate();
            pane.Invalidate();


            base.DrawControl(renderer, rect, gameTime);
        }

        /// </summary>
        /// Occurs when the hide selection value changes.
        /// <summary>
        public event EventHandler HideSelectionChanged;

        /// </summary>
        /// Occurs when the hot tracking value changes.
        /// <summary>
        public event EventHandler HotTrackChanged;

        /// </summary>
        /// Initializes the list box control.
        /// <summary>
        public override void Init()
        {
            base.Init();
        }

        /// </summary>
        /// Occurs when the selected item index changes.
        /// <summary>
        public event EventHandler ItemIndexChanged;

        /// <param name="index">Index to scroll to.</param>
        /// </summary>
        /// specified list item is visible in the list box.
        /// Adjusts the scroll bar value to make sure the
        /// <summary>
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
        /// </summary>
        /// Updates the state of the list box and watches for changes in list size.
        /// <summary>
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

        /// <param name="e"></param>
        /// </summary>
        /// Handles gamepad button presses for the list box. Specifically, the up and down buttons.
        /// <summary>
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
        /// </summary>
        /// Raises the HideSelectionChanged event.
        /// <summary>
        protected virtual void OnHideSelectionChanged(EventArgs e)
        {
            if (HideSelectionChanged != null) HideSelectionChanged.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// </summary>
        /// Raises the HotTrackChanged event.
        /// <summary>
        protected virtual void OnHotTrackChanged(EventArgs e)
        {
            if (HotTrackChanged != null) HotTrackChanged.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// </summary>
        /// Raises the ItemIndexChangedEvent.
        /// <summary>
        protected virtual void OnItemIndexChanged(EventArgs e)
        {
            if (ItemIndexChanged != null) ItemIndexChanged.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles key press events for the list box.
        /// <summary>
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

        /// <param name="e"></param>
        /// </summary>
        /// Handles mouse button down events for the list box.
        /// <summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);


// Need to update the selected item?
            if (e.Button == MouseButton.Left || e.Button == MouseButton.Right)
            {
                TrackItem(e.Position.X, e.Position.Y);
            }
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles mouse move events for the list box.
        /// <summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);


// Update selection?
            if (hotTrack)
            {
                TrackItem(e.Position.X, e.Position.Y);
            }
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles mouse scroll events for the list box.
        /// <summary>
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
        /// </summary>
        /// Handles resizing of the list box.
        /// <summary>
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            ItemsChanged();
        }

        protected virtual void DrawPane(object sender, DrawEventArgs e)
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


// Draw the visible collection items in the list pane.
                for (var i = v; i <= v + p + 1; i++)
                {
                    if (i < c)
                    {
                        e.Renderer.DrawString(this, Skin.Layers["Control"], items[i].ToString(),
                            new Rectangle(e.Rectangle.Left, e.Rectangle.Top - d + ((i - v) * h), e.Rectangle.Width, h),
                            false, DrawFormattedText);
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
                            new Rectangle(e.Rectangle.Left, e.Rectangle.Top + pos, e.Rectangle.Width, h), false, DrawFormattedText);
                    }
                }
            }
        }

        protected virtual void ItemsChanged()
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

        /// <param name="y">Mouse Y position.</param>
        /// <param name="x">Mouse X position.</param>
        /// </summary>
        /// Updates the list box selection when the mouse moves over one.
        /// <summary>
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
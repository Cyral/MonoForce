using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoForce.Controls
{
    /// <summary>
    /// A listbox that can be used to create a list of any custom control, rather than simply text.
    /// </summary>
    /// <typeparam name="T">The type of control to use</typeparam>
    public class ControlList<T> : ListBox where T : Control
    {
        public ControlList(Manager manager)
            : base(manager)
        {

        }

        protected internal override void InitSkin()
        {
            Skin = Manager.Skin.Controls["ListBox"];
        }

        /// <summary>
        /// Draws the list pane containing the collection items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void DrawPane(object sender, DrawEventArgs e)
        {
            // Collection is non-empty?
            if (items != null && items.Count > 0)
            {
                SkinText font = Skin.Layers["Control"].Text;
                SkinLayer sel = Skin.Layers["ListBox.Selection"];
                int h = (items[0] as T).Height;
                int v = (sbVert.Value / 10);
                int p = (sbVert.PageSize / 10);
                int d = (int)(((sbVert.Value % 10) / 10f) * h);
                int c = items.Count;
                int s = itemIndex;
                // Draw selection overlay?
                if (s >= 0 && s < c && (Focused || !hideSelection))
                {
                    int pos = -d + ((s - v) * h);

                    // Selected index is visible?
                    if (pos > -h && pos < (p + 1) * h)
                    {
                        e.Renderer.DrawLayer(this, sel, new Rectangle(e.Rectangle.Left, e.Rectangle.Top + pos, e.Rectangle.Width, h));
                    }
                }
                // Draw the visible collection items in the list pane.
                for (int i = v; i <= v + p + 1; i++)
                {
                    if (i < c)
                    {
                        if (i < items.Count)
                        {
                            foreach (Control ctr in (items[i] as T).Controls)
                            {
                                ctr.DrawControl(e.Renderer,
                                    new Rectangle(e.Rectangle.Left + ctr.Left,
                                        (e.Rectangle.Top - d + ((i - v) * h)) + ctr.Top, e.Rectangle.Width, h),
                                    e.GameTime);
                            }
                        }
                    }
                }
                // Draw selection overlay?
                if (s >= 0 && s < c && (Focused || !hideSelection))
                {
                    int pos = -d + ((s - v) * h);

                    // Selected index is visible?
                    if (pos > -h && pos < (p + 1) * h)
                    {
                        e.Renderer.DrawLayer(sel, new Rectangle(e.Rectangle.Left, e.Rectangle.Top + pos, e.Rectangle.Width, h), Color.White * .2f, 0);
                    }
                }
            }
        }
        /// <summary>
        /// Sizes the list pane so the specified number of items will be able to be
        /// displayed in it without needing a scroll bar.
        /// </summary>
        /// <param name="maxItems">Number of items that can be displayed without needing a scroll bar.</param>
        public override void AutoHeight(int maxItems)
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

            else
            {
                pane.Width = Width - sbVert.Width - Skin.Layers["Control"].ContentMargins.Horizontal - 1;
                sbVert.Visible = true;
            }

            // Get the list box font resource.
            SkinText font = Skin.Layers["Control"].Text;

            // Non-empty collection? Measure the height of a line of font and set the 
            // height of the list pane based on the specified number of items that 
            // should be able to display in it. 
            if (items != null && items.Count > 0)
            {
                int h = (items[0] as T).Height;
                Height = (h * maxItems) + (Skin.Layers["Control"].ContentMargins.Vertical);// - Skin.OriginMargins.Vertical);
            }

            // Empty collection. Default height to 32.
            else
            {
                Height = 32;
            }
        }
        #region On Mouse btnDown Event Handler
        /// <summary>
        /// Handles mouse button down events for the list box.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // Need to update the selected item?
            if (e.Button == MouseButton.Left || e.Button == MouseButton.Right)
            {
                TrackItem(e.Position.X, e.Position.Y);
            }
        }
        #endregion

        #region Track Item
        /// <summary>
        /// Updates the list box selection when the mouse moves over one.
        /// </summary>
        /// <param name="x">Mouse X position.</param>
        /// <param name="y">Mouse Y position.</param>
        private void TrackItem(int x, int y)
        {
            // Collection is non-empty and position is within the list?
            if (items != null && items.Count > 0 && (pane.ControlRect.Contains(new Point(x, y))))
            {
                // Get the height of a list entry.

                int h = (Items[0] as T).Height;
                int d = (int)(((sbVert.Value % 10) / 10f) * h);
                int i = (int)Math.Floor((sbVert.Value / 10f) + ((float)y / h));

                // Index is in collection range and index is visible in the list box?
                if (i >= 0 && i < Items.Count &&
                    i >= (int)Math.Floor((float)sbVert.Value / 10f) &&
                    i < (int)Math.Ceiling((float)(sbVert.Value + sbVert.PageSize) / 10f))
                {
                    // Update the selected index.
                    ItemIndex = i;
                }

                Focused = true;
            }
        }
        #endregion

        #region On Mouse Move Event Handler
        /// <summary>
        /// Handles mouse move events for the list box.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // Update selection?
            if (hotTrack)
            {
                TrackItem(e.Position.X, e.Position.Y);
            }
        }
        #endregion

        protected internal override void Update(GameTime gameTime)
        {
            foreach (var control in Items)
            {
                var item = control as Control;
                item?.Update(gameTime);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Updates scroll bar values based on font size, list box size, and the number of collection items.
        /// </summary>
        protected override void ItemsChanged()
        {
            // List box collection is non-empty?
            if (items != null && items.Count > 0)
            {
                int h = (Items[0] as T).Height;

                // Get the height of the list box content area.
                int sizev = Height - Skin.Layers["Control"].ContentMargins.Vertical;

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
        //public override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        //{
        //    base.DrawControl(renderer, rect, gameTime);
        //}
    }
}

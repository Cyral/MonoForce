using System;
using Microsoft.Xna.Framework;

namespace MonoForce.Controls
{
    public class StackPanel : Container
    {
        private const int refreshTime = 300; //ms

        /// <summary>
        /// Should the stack panel refresh itself, when a control is added
        /// </summary>
        public bool AutoRefresh { get; set; }

        public Orientation Orientation
        {
            get { return orientation; }
            set
            {
                orientation = value;
                CalcLayout();
            }
        }

        private Orientation orientation;
        private TimeSpan refreshTimer;

        public StackPanel(Manager manager, Orientation orientation) : base(manager)
        {
            this.orientation = orientation;
            Color = Color.Transparent;
            AutoRefresh = true;
            refreshTimer = new TimeSpan(0, 0, 0, 0, refreshTime);
        }

        public override void Add(Control control)
        {
            base.Add(control);
            if (AutoRefresh) Refresh();
        }

        public override void Add(Control control, bool client)
        {
            base.Add(control, client);
            if (AutoRefresh) Refresh();
        }

        protected internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (AutoRefresh)
            {
                refreshTimer =
                    refreshTimer.Subtract(TimeSpan.FromMilliseconds(gameTime.ElapsedGameTime.TotalMilliseconds));
                if (refreshTimer.TotalMilliseconds <= 0.00)
                {
                    Refresh();
                    refreshTimer = new TimeSpan(0, 0, 0, 0, refreshTime);
                }
            }
        }

        public override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            base.DrawControl(renderer, rect, gameTime);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            CalcLayout();
            base.OnResize(e);
        }

        private void CalcLayout()
        {
            var top = Top;
            var left = Left;

            foreach (var c in ClientArea.Controls)
            {
                var m = c.Margins;

                if (orientation == Orientation.Vertical)
                {
                    top += m.Top;
                    c.Top = top;
                    top += c.Height;
                    top += m.Bottom;
                    c.Left = left;
                }

                if (orientation == Orientation.Horizontal)
                {
                    left += m.Left;
                    c.Left = left;
                    left += c.Width;
                    left += m.Right;
                    c.Top = top;
                }
            }
        }
    }
}

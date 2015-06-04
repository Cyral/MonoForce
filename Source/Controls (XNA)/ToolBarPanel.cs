using Microsoft.Xna.Framework;

namespace MonoForce.Controls
{
    public class ToolBarPanel : Control
    {
        public ToolBarPanel(Manager manager) : base(manager)
        {
            Width = 64;
            Height = 25;
        }

        /// <summary>
        /// Initializes the tool bar panel control.
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// Initializes the skin for the tool bar panel control.
        /// </summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls["ToolBarPanel"]);
        }

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <summary>
        /// Updates the tool bar panel control.
        /// </summary>
        protected internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            AlignBars();
        }

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            base.DrawControl(renderer, rect, gameTime);
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles resize events for the tool bar panel control.
        /// </summary>
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
        }

        /// <summary>
        /// Positions and sizes the tool bar panel's child tool bar controls.
        /// </summary>
        private void AlignBars()
        {
            var rx = new int[8];
            var h = 0;
            var rm = -1;

            foreach (var c in Controls)
            {
// This child control is a tool bar?
                if (c is ToolBar)
                {
                    var t = c as ToolBar;
                    if (t.FullRow) t.Width = Width;
// Position the tool bar.
                    t.Left = rx[t.Row];
                    t.Top = (t.Row * t.Height) + (t.Row > 0 ? 1 : 0);
                    rx[t.Row] += t.Width + 1;

                    if (t.Row > rm)
                    {
                        rm = t.Row;
                        h = t.Top + t.Height + 1;
                    }
                }
            }

            Height = h;
        }
    }
}

using Microsoft.Xna.Framework;

namespace MonoForce.Controls
{
    public class Label : Control
    {
        /// </summary>
        /// Indicates how the label's text is aligned.
        /// <summary>
        public virtual Alignment Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }

        /// </summary>
        /// Indicates if the label's text should be truncated with "..." if it's too large.
        /// <summary>
        public virtual bool Ellipsis
        {
            get { return ellipsis; }
            set { ellipsis = value; }
        }

        /// </summary>
        /// Indicates how the label's text is aligned.
        /// <summary>
        private Alignment alignment = Alignment.MiddleLeft;

        /// </summary>
        /// Indicates if the text should be truncated with "..."
        /// <summary>
        private bool ellipsis = true;

        public Label(Manager manager) : base(manager)
        {
            CanFocus = false;
            Passive = true;
            Width = 64;
            Height = 16;
        }

        /// </summary>
        /// Initializes the label control.
        /// <summary>
        public override void Init()
        {
            base.Init();
        }

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
//base.DrawControl(renderer, rect, gameTime);
            var s = new SkinLayer(Skin.Layers[0]);
            s.Text.Alignment = alignment;
            renderer.DrawString(this, s, Text, rect, true, 0, 0, ellipsis);
        }
    }
}
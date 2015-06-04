using Microsoft.Xna.Framework;

namespace MonoForce.Controls
{
    public class StatusBar : Control
    {
        public StatusBar(Manager manager) : base(manager)
        {
            Left = 0;
            Top = 0;
            Width = 64;
            Height = 24;
            CanFocus = false;
        }

        /// <summary>
        /// Initializes the status bar control.
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// Initializes the skin of the status bar control.
        /// </summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls["StatusBar"]);
        }

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            base.DrawControl(renderer, rect, gameTime);
        }
    }
}

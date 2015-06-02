using Microsoft.Xna.Framework;

namespace MonoForce.Controls
{
    public class SideBar : Panel
    {
        public SideBar(Manager manager) : base(manager)
        {
// CanFocus = true;
        }

        /// </summary>
        /// Initializes the side bar panel.
        /// <summary>
        public override void Init()
        {
            base.Init();
// CanFocus = true;
        }

        /// </summary>
        /// Initializes the skin of the side bar panel.
        /// <summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls["SideBar"]);
        }

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            base.DrawControl(renderer, rect, gameTime);
        }
    }
}
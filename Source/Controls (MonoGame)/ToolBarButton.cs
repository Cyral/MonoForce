using Microsoft.Xna.Framework;

namespace MonoForce.Controls
{
    public class ToolBarButton : Button
    {
        public ToolBarButton(Manager manager) : base(manager)
        {
            CanFocus = false;
            Text = "";
        }

        /// <summary>
        /// Initializes the tool bar button.
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// Initializes the skin of the tool bar button.
        /// </summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls["ToolBarButton"]);
        }

        public override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            base.DrawControl(renderer, rect, gameTime);
        }
    }
}

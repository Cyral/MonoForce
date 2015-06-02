using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoForce.Controls
{
    public class ToolTip : Control
    {
        /// </summary>
        /// Indicates whether the tool tip is visible or not.
        /// <summary>
        public override bool Visible
        {
            set
            {
                if (value && Text != null && Text != "" && Skin != null && Skin.Layers[0] != null)
                {
                    var size = Skin.Layers[0].Text.Font.Resource.MeasureString(Text);
                    Width = (int)size.X + Skin.Layers[0].ContentMargins.Horizontal;
                    Height = (int)size.Y + Skin.Layers[0].ContentMargins.Vertical;
                    Left = Mouse.GetState().X;
                    Top = Mouse.GetState().Y + 24;
                    base.Visible = value;
                }
                else
                {
                    base.Visible = false;
                }
            }
        }

        public ToolTip(Manager manager) : base(manager)
        {
            Text = "";
        }

        /// </summary>
        /// Initializes the tool tip control.
        /// <summary>
        public override void Init()
        {
            base.Init();
            CanFocus = false;
            Passive = true;
        }

        /// </summary>
        /// Initializes the skin of the tool tip control.
        /// <summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = Manager.Skin.Controls["ToolTip"];
        }

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            renderer.DrawLayer(this, Skin.Layers[0], rect);
            renderer.DrawString(this, Skin.Layers[0], Text, rect, true);
        }
    }
}
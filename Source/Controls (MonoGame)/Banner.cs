using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForce.Controls
{
    /// <summary>
    /// Represents a Banner control.
    /// </summary>
    public class Banner : Button
    {
        public string Title { get; set; }

        public Banner(Manager manager)
            : base(manager)
        {
            Title = "Title";
            Text = "Message";
        }

        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls["Banner"]);
        }

        protected override Rectangle DrawText(Renderer renderer, Rectangle rect, SkinLayer layer, int ox, int oy)
        {
            // Draw the button text.
            layer.Text.Font = Manager.Skin.Fonts["Default9"];
            renderer.DrawString(this, layer, Title, rect, true, ox, 0, base.DrawFormattedText);
            layer.Text.Font = Manager.Skin.Fonts["Default8"];
            renderer.DrawString(this, layer, Text, rect, true, ox, 14, base.DrawFormattedText);
            return rect;
        }
    }
}

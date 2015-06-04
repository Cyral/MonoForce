using Microsoft.Xna.Framework;

namespace MonoForce.Controls
{
    /// <summary>
    /// Defines how the GroupBox looks when rendered.
    /// </summary>
    public enum GroupBoxType
    {
        Normal,
        Flat
    }

    /// <summary>
    /// Represents a container used to group together related controls.
    /// </summary>
    public class GroupBox : Container
    {
        /// <summary>
        /// Gets or sets the group box type.
        /// </summary>
        public virtual GroupBoxType Type
        {
            get { return type; }
            set
            {
                type = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Defines the rendered look of the group box.
        /// </summary>
        private GroupBoxType type = GroupBoxType.Normal;

        /// <param name="manager">GUI manager for the group box.</param>
        /// <summary>
        /// Creates a new GroupBox container control.
        /// </summary>
        public GroupBox(Manager manager)
            : base(manager)
        {
            CheckLayer(Skin, "Control");
            CheckLayer(Skin, "Flat");

            CanFocus = false;
            Passive = true;
            Width = 64;
            Height = 64;
            BackColor = Color.Transparent;
        }

        /// <summary>
        /// Initializes the group box control.
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        protected internal override void OnSkinChanged(EventArgs e)
        {
            base.OnSkinChanged(e);
            AdjustClientMargins();
        }

        public override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            var layer = type == GroupBoxType.Normal ? Skin.Layers["Control"] : Skin.Layers["Flat"];
            var font = (layer.Text != null && layer.Text.Font != null) ? layer.Text.Font.Resource : null;
            var col = (layer.Text != null) ? layer.Text.Colors.Enabled : Color.White;
            var offset = new Point(layer.Text.OffsetX, layer.Text.OffsetY);
            var size = font.MeasureRichString(Text, Manager);
            size.Y = font.LineSpacing;
            var r = new Rectangle(rect.Left, rect.Top + (int)(size.Y / 2), rect.Width, rect.Height - (int)(size.Y / 2));

            renderer.DrawLayer(this, layer, r);

// Group box has header text to draw?
            if (font != null && Text != null && Text != "")
            {
                var bg = new Rectangle(r.Left + offset.X, (r.Top - (int)(size.Y / 2)) + offset.Y,
                    (int)size.X + layer.ContentMargins.Horizontal, (int)size.Y);
                renderer.DrawLayer(Manager.Skin.Controls["Control"].Layers[0], bg, new Color(64, 64, 64), 0);
                renderer.DrawString(this, layer, Text,
                    new Rectangle(r.Left, r.Top - (int)(size.Y / 2), (int)(size.X), (int)size.Y), true, 0, 0, false, DrawFormattedText);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            AdjustClientMargins();
        }

        private void AdjustClientMargins()
        {
            var layer = type == GroupBoxType.Normal ? Skin.Layers["Control"] : Skin.Layers["Flat"];
            var font = (layer.Text != null && layer.Text.Font != null) ? layer.Text.Font.Resource : null;
            var size = font.MeasureRichString(Text, Manager);
            var cm = ClientMargins;
            cm.Top = string.IsNullOrWhiteSpace(Text) ? ClientTop : (int)size.Y;
            ClientMargins = new Margins(cm.Left, cm.Top, cm.Right, cm.Bottom);
        }
    }
}

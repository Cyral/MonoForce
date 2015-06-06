using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace MonoForce.Controls
{
    /// <summary>
    /// A label that opens a link when clicked.
    /// </summary>
    public sealed class LinkLabel : Label
    {
        /// <summary>
        /// The URL or process to run.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Color to fade to on hover.
        /// </summary>
        public Color HoverColor { get; set; }

        public LinkLabel(Manager manager) : base(manager)
        {
            Passive = false;
            Color = new Color(25, 125, 255);
            HoverColor = new Color(5, 95, 235);
            TextColor = Color;
        }

        /// <summary>
        /// Initializes the skin of the track bar control.
        /// </summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls["Label"]);
        }

        protected override void OnClick(EventArgs e)
        {
            OpenLink();
            base.OnClick(e);
        }

        protected override void OnMouseOut(MouseEventArgs e)
        {
            TextColor = Color;
            Cursor = Manager.Skin.Cursors["Default"].Resource;
            base.OnMouseOut(e);
        }

        protected override void OnMouseOver(MouseEventArgs e)
        {
            TextColor = HoverColor;
            Cursor = Manager.Skin.Cursors["Move"].Resource;
            base.OnMouseOver(e);
        }

        private void OpenLink()
        {
            //TODO: This could be a vulverability if third-party plugins made mods (for a game), and made it run malicious commands.
            Process.Start(URL);
        }
    }
}

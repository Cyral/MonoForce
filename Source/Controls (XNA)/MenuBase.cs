using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForce.Controls
{
    public class MenuItem : Unknown
    {
        public object Tag { get; set; }

        /// <summary>
        /// Indicates if the menu item is able to be selected or not.
        /// </summary>
        public bool Enabled = true;

        /// <summary>
        /// Image to display to the left of the menu item.
        /// </summary>
        public Texture2D Image = null;

        /// <summary>
        /// List of child menu items belonging to this menu item.
        /// </summary>
        public List<MenuItem> Items = new List<MenuItem>();

        /// <summary>
        /// Indicates if the menu item appears after a menu separator. ???
        /// </summary>
        public bool Separated;

        /// <summary>
        /// Text to display for this menu item.
        /// </summary>
        public string Text = "MenuItem";

        /// <summary>
        /// Creates a new default menu item.
        /// </summary>
        public MenuItem()
        {
        }

        public MenuItem(string text) : this()
        {
            Text = text;
        }

        public MenuItem(string text, bool separated) : this(text)
        {
            Separated = separated;
        }

        /// <summary>
        /// Occurs when the menu item is clicked.
        /// </summary>
        public event EventHandler Click;

        /// <summary>
        /// Occurs when the menu item is selected.
        /// </summary>
        public event EventHandler Selected;

        /// <param name="e"></param>
        /// <summary>
        /// Raises the menu item click event.
        /// </summary>
        internal void ClickInvoke(EventArgs e)
        {
            if (Click != null) Click.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// <summary>
        /// Raises the menu item selected event.
        /// </summary>
        internal void SelectedInvoke(EventArgs e)
        {
            if (Selected != null) Selected.Invoke(this, e);
        }
    }

    public abstract class MenuBase : Control
    {
        /// <summary>
        /// Gets the list of menu items that make up the menu.
        /// </summary>
        public List<MenuItem> Items
        {
            get { return items; }
        }

        /// <summary>
        /// Gets or sets the menu's child menu.
        /// </summary>
        protected internal MenuBase ChildMenu
        {
            get { return childMenu; }
            set { childMenu = value; }
        }

        /// <summary>
        /// Gets or sets the menu's selected menu item index.
        /// </summary>
        protected internal int ItemIndex
        {
            get { return itemIndex; }
            set { itemIndex = value; }
        }

        /// <summary>
        /// Gets or sets the menu's parent menu.
        /// </summary>
        protected internal MenuBase ParentMenu
        {
            get { return parentMenu; }
            set { parentMenu = value; }
        }

        /// <summary>
        /// Gets or sets the menu's root menu.
        /// </summary>
        protected internal MenuBase RootMenu
        {
            get { return rootMenu; }
            set { rootMenu = value; }
        }

        /// <summary>
        /// List of menu items composing the menu.
        /// </summary>
        private readonly List<MenuItem> items = new List<MenuItem>();

        /// <summary>
        /// Child menu of this menu.
        /// </summary>
        private MenuBase childMenu;

        /// <summary>
        /// Selected menu item index.
        /// </summary>
        private int itemIndex = -1;

        /// <summary>
        /// Parent menu of this menu.
        /// </summary>
        private MenuBase parentMenu;

        /// <summary>
        /// Root menu of this menu.
        /// </summary>
        private MenuBase rootMenu;

        public MenuBase(Manager manager) : base(manager)
        {
            rootMenu = this;
        }
    }
}

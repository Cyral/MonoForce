

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MonoForce.Controls
{

public class MainMenu: MenuBase
{


/// </summary>
/// Array of rectangles.
/// <summary>
private Rectangle[] rs;
/// </summary>
/// Last selected menu entry index.
/// <summary>
private int lastIndex = -1;






public MainMenu(Manager manager): base(manager)
{
Left = 0;
Top = 0;
Height = 24;
Detached = false;
DoubleClicks = false;
StayOnBack = true;
}



/// <param name="disposing"></param>
/// </summary>
/// Cleans up after the main menu.
/// <summary>
protected override void Dispose(bool disposing)
{
if (disposing)
{
}
base.Dispose(disposing);
}



/// </summary>
/// Initializes the main menu.
/// <summary>
public override void Init()
{
base.Init();
}

/// </summary>
/// Initializes the skin for the main menu.
/// <summary>
protected internal override void InitSkin()
{
base.InitSkin();
Skin = new SkinControl(Manager.Skin.Controls["MainMenu"]);
}

protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
{
SkinLayer l1 = Skin.Layers["Control"];
SkinLayer l2 = Skin.Layers["Selection"];
rs = new Rectangle[Items.Count];

// Draw the menu background.
renderer.DrawLayer(this, l1, rect, ControlState.Enabled);

int prev = l1.ContentMargins.Left;
// Draw all menu entries.
for (int i = 0; i < Items.Count; i++)
{
MenuItem mi = Items[i];

int tw = (int)l1.Text.Font.Resource.MeasureString(mi.Text).X + l1.ContentMargins.Horizontal;
rs[i] = new Rectangle(rect.Left + prev, rect.Top + l1.ContentMargins.Top, tw, Height - l1.ContentMargins.Vertical);
prev += tw;

// Is not the selected entry?
if (ItemIndex != i)
{
// Draw in the enabled state?
if (mi.Enabled && Enabled)
{
renderer.DrawString(this, l1, mi.Text, rs[i], ControlState.Enabled, false);
}
// Draw in the disabled state?
else
{
renderer.DrawString(this, l1, mi.Text, rs[i], ControlState.Disabled, false);
}
}
// Draw in the disabled state?
else
{
// Draw enabled state with selection?
if (Items[i].Enabled && Enabled)
{
renderer.DrawLayer(this, l2, rs[i], ControlState.Enabled);
renderer.DrawString(this, l2, mi.Text, rs[i], ControlState.Enabled, false);
}
// Draw in the disabled state?
else
{
renderer.DrawLayer(this, l2, rs[i], ControlState.Disabled);
renderer.DrawString(this, l2, mi.Text, rs[i], ControlState.Disabled, false);
}
}
}
}

/// <param name="y">Y position to check.</param>
/// <param name="x">X position to check.</param>
/// </summary>
/// Highlights the menu item under the specified position.
/// <summary>
private void TrackItem(int x, int y)
{
// Menu has a list of menu items?
if (Items != null && Items.Count > 0 && rs != null)
{
// Force redraw and update selected index.
Invalidate();
// Determine which menu item is under the specified position.
for (int i = 0; i < rs.Length; i++)
{
// Point is within the bounds of the current menu item?
if (rs[i].Contains(x, y))
{
// Valid index and not the current selection?
if (i >= 0 && i != ItemIndex)
{
// Select this menu item.
Items[i].SelectedInvoke(new EventArgs());
}
// Update selected menu item index.
ItemIndex = i;
return;
}
}
if (ChildMenu == null) ItemIndex = -1;
}
}

/// <returns>Returns true.</returns>
/// <param name="y">Y position to check.</param>
/// <param name="x">X position to check.</param>
/// </summary>
/// Checks to see if the specified position is within the bounds of the menu or a child menu. ???
/// <summary>
private bool CheckArea(int x, int y)
{
return true;
}

/// <param name="e"></param>
/// </summary>
/// Handles mouse move events for the main menu.
/// <summary>
protected override void OnMouseMove(MouseEventArgs e)
{
base.OnMouseMove(e);
int i = lastIndex;

// Update selected index.
TrackItem(e.State.X - Root.AbsoluteLeft, e.State.Y - Root.AbsoluteTop);

// New item index is valid and not the same index as was previously selected and previous selection had a child menu?
if (ItemIndex >= 0 && (i == -1 || i != ItemIndex) && Items[ItemIndex].Items != null && Items[ItemIndex].Items.Count > 0 && ChildMenu != null)
{
// Hide the previous child menu.
HideSubMenu();
lastIndex = ItemIndex;
OnClick(e);
}
else if (ChildMenu != null && i != ItemIndex)
{
// Hide the previous child menu.
HideSubMenu();
Focused = true;
}
}

/// <param name="e"></param>
/// </summary>
/// Handles mouse button down events for the main menu.
/// <summary>
protected override void OnMouseDown(MouseEventArgs e)
{
base.OnMouseDown(e);
}

/// <param name="e"></param>
/// </summary>
/// Handles mouse out events for the main menu.
/// <summary>
protected override void OnMouseOut(MouseEventArgs e)
{
base.OnMouseOut(e);

OnMouseMove(e);
}

/// </summary>
/// Hides the currently visible child menu.
/// <summary>
private void HideSubMenu()
{
// Child menu to hide?
if (ChildMenu != null)
{
(ChildMenu as ContextMenu).HideMenu(true);
ChildMenu.Dispose();
ChildMenu = null;
}
}

/// </summary>
/// Hides the menu and any visible child menus.
/// <summary>
public virtual void HideMenu()
{
// Child menu to hide?
if (ChildMenu != null)
{
(ChildMenu as ContextMenu).HideMenu(true);
ChildMenu.Dispose();
ChildMenu = null;
}
if (Manager.FocusedControl is MenuBase) Focused = true;
// Force redraw and update selected index.
Invalidate();
ItemIndex = -1;
}

/// <param name="e"></param>
/// </summary>
/// Handles mouse button click events for the main menu.
/// <summary>
protected override void OnClick(EventArgs e)
{
base.OnClick(e);

MouseEventArgs ex = (e is MouseEventArgs) ? (MouseEventArgs)e : new MouseEventArgs();

// Left mouse button clicked?
if (ex.Button == MouseButton.Left || ex.Button == MouseButton.None)
{
// Selected item is in menu and usable?
if (ItemIndex >= 0 && Items[ItemIndex].Enabled)
{
// Selected menu entry has children?
if (ItemIndex >= 0 && Items[ItemIndex].Items != null && Items[ItemIndex].Items.Count > 0)
{
// Child menu to hide?
if (ChildMenu != null)
{
ChildMenu.Dispose();
ChildMenu = null;
}
// Display child menu entries for the selected entry.
ChildMenu = new ContextMenu(Manager);
(ChildMenu as ContextMenu).RootMenu = this;
(ChildMenu as ContextMenu).ParentMenu = this;
(ChildMenu as ContextMenu).Sender = this.Root;
ChildMenu.Items.AddRange(Items[ItemIndex].Items);

int y = Root.AbsoluteTop + rs[ItemIndex].Bottom + 1;
(ChildMenu as ContextMenu).Show(this.Root, Root.AbsoluteLeft + rs[ItemIndex].Left, y);
if (ex.Button == MouseButton.None) (ChildMenu as ContextMenu).ItemIndex = 0;
}
else
{
// No children. If index is valid, raise the selected entry's click event.
if (ItemIndex >= 0)
{
Items[ItemIndex].ClickInvoke(ex);
}
}
}
}
}

/// <param name="e"></param>
/// </summary>
/// Handles arrow key press events for the main menu.
/// <summary>
protected override void OnKeyPress(KeyEventArgs e)
{
base.OnKeyPress(e);

// Navigate the menu items on arrow left/right press.
if (e.Key == Keys.Right)
{
ItemIndex += 1;
e.Handled = true;
}
if (e.Key == Keys.Left)
{
ItemIndex -= 1;
e.Handled = true;
}

// Wrap selected index in range.
if (ItemIndex > Items.Count - 1) ItemIndex = 0;
if (ItemIndex < 0) ItemIndex = Items.Count - 1;

// Open the menu if down arrow is pressed.
if (e.Key == Keys.Down && Items.Count > 0 && Items[ItemIndex].Items.Count > 0)
{
e.Handled = true;
OnClick(new MouseEventArgs(new MouseState(), MouseButton.None, Point.Zero));
}
// Exit the menu on escape.
if (e.Key == Keys.Escape)
{
e.Handled = true;
ItemIndex = -1;
}
}

/// <param name="e"></param>
/// </summary>
/// Handles game pad button press events for the main menu.
/// <summary>
protected override void OnGamePadPress(GamePadEventArgs e)
{
base.OnGamePadPress(e);

// Left and right DPad buttons navigate through menus.
if (e.Button == GamePadActions.Right)
{
ItemIndex += 1;
e.Handled = true;
}
if (e.Button == GamePadActions.Left)
{
ItemIndex -= 1;
e.Handled = true;
}

// Wrap selected index in range.
if (ItemIndex > Items.Count - 1) ItemIndex = 0;
if (ItemIndex < 0) ItemIndex = Items.Count - 1;

// Open the selected menu on DPad Down.
if (e.Button == GamePadActions.Down && Items[ItemIndex].Items.Count > 0)
{
e.Handled = true;
OnClick(new MouseEventArgs(new MouseState(), MouseButton.None, Point.Zero));
}
}

/// <param name="e"></param>
/// </summary>
/// Handles focus gained events for the main menu.
/// <summary>
protected override void OnFocusGained(EventArgs e)
{
base.OnFocusGained(e);
if (ItemIndex < 0 && Items.Count > 0) ItemIndex = 0;
}

/// <param name="e"></param>
/// </summary>
/// Handles focus lost events for the main menu control.
/// <summary>
protected override void OnFocusLost(EventArgs e)
{
base.OnFocusLost(e);
if (ChildMenu == null || !ChildMenu.Visible) ItemIndex = -1;
}


}

}

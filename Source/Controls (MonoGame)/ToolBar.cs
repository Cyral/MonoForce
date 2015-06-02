

using Microsoft.Xna.Framework;


namespace MonoForce.Controls
{


public class ToolBar: Control
{


/// </summary>
/// Row of the tool bar panel this tool bar defines.
/// <summary>
private int row = 0;
/// </summary>
/// Indicates if the tool bar should take up the full width of its parent tool bar container.
/// <summary>
private bool fullRow = false;



/// </summary>
/// Gets or sets the row index this tool bar occupies in its parent container.
/// <summary>
public virtual int Row
{
get { return row; }
set
{
row = value;
if (row < 0) row = 0;
if (row > 7) row = 7;
}
}

/// </summary>
/// Indicates if the tool bar should stretch across the entire width of its container.
/// <summary>
public virtual bool FullRow
{
get { return fullRow; }
set { fullRow = value; }
}



public ToolBar(Manager manager): base(manager)
{
Left = 0;
Top = 0;
Width = 64;
Height = 24;
CanFocus = false;
}



/// </summary>
/// Initializes the tool bar control.
/// <summary>
public override void Init()
{
base.Init();
}

/// </summary>
/// Initializes the skin of the tool bar control.
/// <summary>
protected internal override void InitSkin()
{
base.InitSkin();
Skin = new SkinControl(Manager.Skin.Controls["ToolBar"]);
}

protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
{
base.DrawControl(renderer, rect, gameTime);
}


}

}

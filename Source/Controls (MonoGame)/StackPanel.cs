

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MonoForce.Controls
{

public class StackPanel: Container
{


private Orientation orientation;
public Orientation Orientation
{
get { return this.orientation; }
set
{
this.orientation = value;
this.CalcLayout();
}
}
private bool autoRefresh;

/// </summary>
/// Should the stack panel refresh itself, when a control is added
/// <summary>
public bool AutoRefresh
{
get { return autoRefresh; }
set { autoRefresh = value; }
}

private TimeSpan refreshTimer;
private const int refreshTime = 300; //ms


public StackPanel(Manager manager, Orientation orientation): base(manager)
{
this.orientation = orientation;
this.Color = Color.Transparent;
this.autoRefresh = true;
refreshTimer = new TimeSpan(0, 0, 0, 0, refreshTime);
}



private void CalcLayout()
{
int top = Top;
int left = Left;

foreach (Control c in ClientArea.Controls)
{
Margins m = c.Margins;

if (orientation == Orientation.Vertical)
{
top += m.Top;
c.Top = top;
top += c.Height;
top += m.Bottom;
c.Left = left;
}

if (orientation == Orientation.Horizontal)
{
left += m.Left;
c.Left = left;
left += c.Width;
left += m.Right;
c.Top = top;
}
}
}

protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
{
base.DrawControl(renderer, rect, gameTime);
}

protected override void OnResize(ResizeEventArgs e)
{
CalcLayout();
base.OnResize(e);
}

protected internal override void Update(GameTime gameTime)
{
base.Update(gameTime);

if (autoRefresh)
{
refreshTimer = refreshTimer.Subtract(TimeSpan.FromMilliseconds(gameTime.ElapsedGameTime.TotalMilliseconds));
if (refreshTimer.TotalMilliseconds <= 0.00)
{
Refresh();
refreshTimer = new TimeSpan(0, 0, 0, 0, refreshTime);
}
}
}

public override void Add(Control control)
{
base.Add(control);
if (autoRefresh) Refresh();
}

public override void Add(Control control, bool client)
{
base.Add(control, client);
if (autoRefresh) Refresh();
}


}

}

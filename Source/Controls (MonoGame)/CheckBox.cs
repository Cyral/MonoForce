

using Microsoft.Xna.Framework;


namespace MonoForce.Controls
{

public class CheckBox: ButtonBase
{


/// </summary>
/// String to reference the check box element in the skin file.
/// <summary>
private const string skCheckBox = "CheckBox";
/// </summary>
/// String to reference the control layer the checkbox is a part of.
/// <summary>
private const string lrCheckBox = "Control";
private const string lrChecked  = "Checked";



/// </summary>
/// Indicates if the control is checked (true) or unchecked (false.)
/// <summary>
private bool state = false;



/// </summary>
/// Gets or sets the checked state of the control.
/// <summary>
public virtual bool Checked
{
get
{
return state;
}
set
{
state = value;
Invalidate();
if (!Suspended) OnCheckedChanged(new EventArgs());
}
}



/// </summary>
/// Occurs when the control's check state value changes.
/// <summary>
public event EventHandler CheckedChanged;



public CheckBox(Manager manager): base(manager)
{
CheckLayer(Skin, lrChecked);

Width = 64;
Height = 16;
}



/// </summary>
/// Initializes the check box control.
/// <summary>
public override void Init()
{
base.Init();
}

/// </summary>
/// Initializes the check box control's skin.
/// <summary>
protected internal override void InitSkin()
{
base.InitSkin();
Skin = new SkinControl(Manager.Skin.Controls[skCheckBox]);
}

protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
{
// Grab the checked skin layer and skin font.
SkinLayer layer = Skin.Layers[lrChecked];
SkinText font = Skin.Layers[lrChecked].Text;

// Umm. See if we actually need the unchecked layer and font...
if (!state)
{
layer = Skin.Layers[lrCheckBox];
font = Skin.Layers[lrCheckBox].Text;
}

rect.Width = layer.Width;
rect.Height = layer.Height;
Rectangle rc = new Rectangle(rect.Left + rect.Width + 4, rect.Y,  Width - (layer.Width + 4), rect.Height);

renderer.DrawLayer(this, layer, rect);
renderer.DrawString(this, layer, Text, rc, false, 0, 0);
}

/// <param name="e"></param>
/// </summary>
/// Handles checkbox click events.
/// <summary>
protected override void OnClick(EventArgs e)
{
MouseEventArgs ex = (e is MouseEventArgs) ? (MouseEventArgs)e : new MouseEventArgs();

// Change the checked state of the control?
if (ex.Button == MouseButton.Left || ex.Button == MouseButton.None)
{
Checked = !Checked;
}
base.OnClick(e);
}

/// <param name="e"></param>
/// </summary>
/// Handles the checked changed event when the value of the check state changes.
/// <summary>
protected virtual void OnCheckedChanged(EventArgs e)
{
if (CheckedChanged != null) CheckedChanged.Invoke(this, e);
}



}

}





namespace MonoForce.Controls
{


public abstract class ButtonBase: Control
{





/// </summary>
/// Gets the state of the button control.
/// <summary>
public override ControlState ControlState
{
get
{
if (DesignMode) return ControlState.Enabled;
else if (Suspended) return ControlState.Disabled;
else
{
if (!Enabled) return ControlState.Disabled;

if ((Pressed[(int)MouseButton.Left] && Inside) || (Focused && (Pressed[(int)GamePadActions.Press] || Pressed[(int)MouseButton.None]))) return ControlState.Pressed;
else if (Hovered && Inside) return ControlState.Hovered;
else if ((Focused && !Inside) || (Hovered && !Inside) || (Focused && !Hovered && Inside)) return ControlState.Focused;
else return ControlState.Enabled;
}
}
}






/// <param name="manager">GUI manager for this control.</param>
/// </summary>
/// ButtonBase constructor.
/// <summary>
protected ButtonBase(Manager manager)
: base(manager)
{
SetDefaultSize(72, 24);
DoubleClicks = false;
}



/// </summary>
/// Initializes the button base.
/// <summary>
public override void Init()
{
base.Init();
}

/// <param name="e"></param>
/// </summary>
/// Handles button click events.
/// <summary>
protected override void OnClick(EventArgs e)
{
MouseEventArgs ex = (e is MouseEventArgs) ? (MouseEventArgs)e : new MouseEventArgs();
if (ex.Button == MouseButton.Left || ex.Button == MouseButton.None)
{
base.OnClick(e);
}
}


}



}

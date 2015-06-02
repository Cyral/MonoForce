


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MonoForce.Controls
{


/// </summary>
/// Specifies how an image is sized and positioned within a control.
/// <summary>
public enum SizeMode
{
/// </summary>
/// ???
/// <summary>
Normal,
/// </summary>
/// ???
/// <summary>
Auto,
/// </summary>
/// Image is centered within the control.
/// <summary>
Centered,
/// </summary>
/// Image is scaled to fit in the control.
/// <summary>
Stretched,
/// </summary>
/// Only Supported by ImageBox
/// <summary>
Tiled
}

/// </summary>
/// Specifies how a button reacts to clicks.
/// <summary>
public enum ButtonMode
{
/// </summary>
/// ???
/// <summary>
Normal,
/// </summary>
/// Toggle button.
/// <summary>
PushButton
}




/// </summary>
/// Represents an image on a button.
/// <summary>
public class Glyph
{
/// </summary>
/// The image asset.
/// <summary>
public Texture2D Image = null;
/// </summary>
/// Specifies how the image is sized on the button.
/// <summary>
public SizeMode SizeMode = SizeMode.Stretched;
/// </summary>
/// Color tint to apply to the button.
/// <summary>
public Color Color = Color.White;
/// </summary>
/// Offset from the button's position where the image will be drawn.
/// <summary>
public Point Offset = Point.Zero;
/// </summary>
/// Source region on the texture where the glyph appears.
/// <summary>
public Rectangle SourceRect = Rectangle.Empty;

/// <param name="image">Glyph image asset.</param>
/// </summary>
/// Creates a new glyph from the specified texture.
/// <summary>
public Glyph(Texture2D image)
{
Image = image;
}

public Glyph(Texture2D image, Rectangle sourceRect): this(image)
{
SourceRect = sourceRect;
}
}

public class Button: ButtonBase
{


/// </summary>
/// The name of the button element in the skin file.
/// <summary>
private const string skButton = "Button";
/// </summary>
/// The name of the layer that a button appears on.
/// <summary>
private const string lrButton = "Control";



/// </summary>
/// Image to display on the button.
/// <summary>
private Glyph glyph = null;
/// </summary>
/// should be returned when the button is clicked.
/// When the button appears on a dialog, this indicates what value
/// <summary>
private ModalResult modalResult = ModalResult.None;
/// </summary>
/// Specifies the type of button. Standard or Toggle.
/// <summary>
private ButtonMode mode = ButtonMode.Normal;
/// </summary>
/// Indicates whether the button is currently pressed or not.
/// <summary>
private bool pushed = false;



/// </summary>
/// Gets or sets the button's glyph. (image)
/// <summary>
public Glyph Glyph
{
get { return glyph; }
set
{
glyph = value;
if (!Suspended) OnGlyphChanged(new EventArgs());
}
}

/// </summary>
/// Gets or sets the value returned when the button of a dialog is clicked.
/// <summary>
public ModalResult ModalResult
{
get { return modalResult; }
set { modalResult = value; }
}

/// </summary>
/// Gets or sets the way the button operates, standard or toggle button.
/// <summary>
public ButtonMode Mode
{
get { return mode; }
set { mode = value; }
}

/// </summary>
/// Indicates whether the button is pressed or not.
/// <summary>
public bool Pushed
{
get { return pushed; }
set
{
pushed = value;
Invalidate();
}
}



/// </summary>
/// Occurs when the button's glyph is changed.
/// <summary>
public event EventHandler GlyphChanged;



public Button(Manager manager): base(manager)
{
SetDefaultSize(72, 24);
}



/// <param name="disposing"></param>
/// </summary>
/// Releases resources used by the button control.
/// <summary>
protected override void Dispose(bool disposing)
{
if (disposing)
{
}
base.Dispose(disposing);
}



/// </summary>
/// Initializes the button control.
/// <summary>
public override void Init()
{
base.Init();
}

/// </summary>
/// Initializes the skin for the button control.
/// <summary>
protected internal override void InitSkin()
{
base.InitSkin();
Skin = new SkinControl(Manager.Skin.Controls[skButton]);
}

protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
{

// Toggle button in pressed state?
if (mode == ButtonMode.PushButton && pushed)
{
SkinLayer l = Skin.Layers[lrButton];
renderer.DrawLayer(l, rect, l.States.Pressed.Color, l.States.Pressed.Index);
// Does the layer's pressed state have an overlay?
if (l.States.Pressed.Overlay)
{
// Draw the overlay on top of the button.
renderer.DrawLayer(l, rect, l.Overlays.Pressed.Color, l.Overlays.Pressed.Index);
}
}
else
{
// Standard button. ButtonBase can handle drawing.
base.DrawControl(renderer, rect, gameTime);
}

SkinLayer layer = Skin.Layers[lrButton];
SpriteFont font = (layer.Text != null && layer.Text.Font != null) ? layer.Text.Font.Resource : null;
Color col = Color.White;
int ox = 0; int oy = 0;

// Standard button pressed?
if (ControlState == ControlState.Pressed)
{
if (layer.Text != null) col = layer.Text.Colors.Pressed;
ox = 1; oy = 1;
}
// Button has an image to apply?
if (glyph != null)
{
// Draw the button image.
Margins cont = layer.ContentMargins;
Rectangle r = new Rectangle(rect.Left + cont.Left,
rect.Top + cont.Top,
rect.Width - cont.Horizontal,
rect.Height - cont.Vertical);
renderer.DrawGlyph(glyph, r);
}
else
{
renderer.DrawString(this, layer, Text, rect, true, ox, oy);
}
}

/// <param name="e"></param>
/// </summary>
/// Event handler for when the button's glyph is changed.
/// <summary>
private void OnGlyphChanged(EventArgs e)
{
if (GlyphChanged != null) GlyphChanged.Invoke(this, e);
}

/// <param name="e"></param>
/// </summary>
/// Button click event handler.
/// <summary>
protected override void OnClick(EventArgs e)
{
MouseEventArgs ex = (e is MouseEventArgs) ? (MouseEventArgs)e : new MouseEventArgs();

if (ex.Button == MouseButton.Left || ex.Button == MouseButton.None)
{
pushed = !pushed;
}

base.OnClick(e);

if ((ex.Button == MouseButton.Left || ex.Button == MouseButton.None) && Root != null)
{
// assume this button belongs to a dialog and close the dialog after the click.
// If the root control is a window and the button set a modal result value,
if (Root is Window)
{
Window wnd = (Window)Root;
if (ModalResult != ModalResult.None)
{
wnd.Close(ModalResult);
}
}
}
}


}


}

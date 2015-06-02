// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

namespace MonoForce.Controls
{
/// </summary>
/// Specifies the blend mode that will be used when the renderer draws objects.
/// <summary>
public enum BlendingMode
{
/// </summary>
/// The renderer will draw with the default blend settings.
/// <summary>
Default,
/// </summary>
/// The renderer will draw in overwrite mode.
/// <summary>
None,
Additive,
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

/// </summary>
/// Contains state information for the GraphicsDevice.
/// <summary>
public class DeviceStates
{
/// </summary>
/// Graphics device blend state information.
/// <summary>
public readonly BlendState BlendState;
/// </summary>
/// Indicates how the graphics device converts vector data into raster data.
/// <summary>
public readonly RasterizerState RasterizerState;
/// </summary>
/// Graphics device depth stencil state.
/// <summary>
public readonly DepthStencilState DepthStencilState;
/// </summary>
/// Indicates how the graphics device samples texture data.
/// <summary>
public readonly SamplerState SamplerState;
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

/// </summary>
///
/// <summary>
public DeviceStates()
{
// Default blend state settings.
BlendState = new BlendState();
BlendState.AlphaBlendFunction = BlendState.AlphaBlend.AlphaBlendFunction;
BlendState.AlphaDestinationBlend = BlendState.AlphaBlend.AlphaDestinationBlend;
BlendState.AlphaSourceBlend = BlendState.AlphaBlend.AlphaSourceBlend;
BlendState.BlendFactor = BlendState.AlphaBlend.BlendFactor;
BlendState.ColorBlendFunction = BlendState.AlphaBlend.ColorBlendFunction;
BlendState.ColorDestinationBlend = BlendState.AlphaBlend.ColorDestinationBlend;
BlendState.ColorSourceBlend = BlendState.AlphaBlend.ColorSourceBlend;
BlendState.ColorWriteChannels = BlendState.AlphaBlend.ColorWriteChannels;
BlendState.ColorWriteChannels1 = BlendState.AlphaBlend.ColorWriteChannels1;
BlendState.ColorWriteChannels2 = BlendState.AlphaBlend.ColorWriteChannels2;
BlendState.ColorWriteChannels3 = BlendState.AlphaBlend.ColorWriteChannels3;
BlendState.MultiSampleMask = BlendState.AlphaBlend.MultiSampleMask;
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// Default rasterizer state settings.
RasterizerState = new RasterizerState();
RasterizerState.CullMode = RasterizerState.CullNone.CullMode;
RasterizerState.DepthBias = RasterizerState.CullNone.DepthBias;
RasterizerState.FillMode = RasterizerState.CullNone.FillMode;
RasterizerState.MultiSampleAntiAlias = RasterizerState.CullNone.MultiSampleAntiAlias;
RasterizerState.ScissorTestEnable = RasterizerState.CullNone.ScissorTestEnable;
RasterizerState.SlopeScaleDepthBias = RasterizerState.CullNone.SlopeScaleDepthBias;
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

RasterizerState.ScissorTestEnable = true;
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// Default sampler state settings.
SamplerState = new SamplerState();
SamplerState.AddressU = SamplerState.AnisotropicClamp.AddressU;
SamplerState.AddressV = SamplerState.AnisotropicClamp.AddressV;
SamplerState.AddressW = SamplerState.AnisotropicClamp.AddressW;
SamplerState.Filter = SamplerState.AnisotropicClamp.Filter;
SamplerState.MaxAnisotropy = SamplerState.AnisotropicClamp.MaxAnisotropy;
SamplerState.MaxMipLevel = SamplerState.AnisotropicClamp.MaxMipLevel;
SamplerState.MipMapLevelOfDetailBias = SamplerState.AnisotropicClamp.MipMapLevelOfDetailBias;
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// Default depth stencil state settings.
DepthStencilState = new DepthStencilState();
DepthStencilState = DepthStencilState.None;
}
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

/// </summary>
/// Provides methods for drawing control layers and strings.
/// <summary>
public class Renderer : Component
{
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

/// </summary>
/// Sprite batch object used for drawing.
/// <summary>
private SpriteBatch sb = null;
/// </summary>
/// Various graphics device settings for the renderer.
/// <summary>
private DeviceStates states = new DeviceStates();
private BlendingMode bmode = BlendingMode.Default;
private Matrix? customMatrix;
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public Matrix? CustomMatrix
{
get { return customMatrix; }
set { customMatrix = value; }
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

/// </summary>
/// Accesses the renderer's sprite batch object.
/// <summary>
public virtual SpriteBatch SpriteBatch
{
get
{
return sb;
}
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

/// <param name="manager">GUI manager for creating the sprite batch.</param>
/// </summary>
/// Creates a new renderer.
/// <summary>
public Renderer(Manager manager)
: base(manager)
{
sb = new SpriteBatch(Manager.GraphicsDevice);
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

/// <param name="disposing"></param>
/// </summary>
/// Cleans up resources used by the renderer.
/// <summary>
protected override void Dispose(bool disposing)
{
if (disposing)
{
if (sb != null)
{
sb.Dispose();
sb = null;
}
}
base.Dispose(disposing);
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

/// </summary>
/// Initializes the renderer.
/// <summary>
public override void Init()
{
base.Init();
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

/// BlendingMode.Default will begin drawing using the default blend state settings.</param>
/// <param name="mode">Specify BlendingMode.None will begin drawing in overwrite mode.
/// </summary>
/// Begins drawing batched objects.
/// <summary>
public virtual void Begin(BlendingMode mode)
{
bmode = mode;
if (mode != BlendingMode.None)
{
BlendState state = states.BlendState;
if (mode == BlendingMode.Additive)
{
state = BlendState.Additive;
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

if (this.customMatrix.HasValue)
{
sb.Begin(SpriteSortMode.Immediate, state, states.SamplerState, states.DepthStencilState, states.RasterizerState, null, this.customMatrix.Value);
}
// Grab the text color of the Enabled state.
else
{
sb.Begin(SpriteSortMode.Immediate, state, states.SamplerState, states.DepthStencilState, states.RasterizerState);
}
}
// Grab the text color of the Enabled state.
else
{
if (this.customMatrix.HasValue)
{
sb.Begin(SpriteSortMode.Immediate, BlendState.Opaque, states.SamplerState, states.DepthStencilState, states.RasterizerState, null, this.customMatrix.Value);
}
// Grab the text color of the Enabled state.
else
{
sb.Begin(SpriteSortMode.Immediate, BlendState.Opaque, states.SamplerState, states.DepthStencilState, states.RasterizerState);
}
}
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

/// </summary>
/// Ends drawing of batched objects.
/// <summary>
public virtual void End()
{
sb.End();
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

/// <param name="color">Color tint to apply to the image.</param>
/// <param name="destination">Destination region where the image will be drawn.</param>
/// <param name="texture">Image to draw.</param>
/// </summary>
/// Draws the texture in the specified region.
/// <summary>
public virtual void Draw(Texture2D texture, Rectangle destination, Color color)
{
// Valid destination region?
if (destination.Width > 0 && destination.Height > 0)
{
sb.Draw(texture, destination, null, color, 0.0f, Vector2.Zero, SpriteEffects.None, Manager.GlobalDepth);
}
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public virtual void DrawTileTexture(Texture2D texture, Rectangle destination, Color color)
{
// Valid destination region?
if (destination.Width > 0 && destination.Height > 0)
{
End();
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

if (this.customMatrix.HasValue)
{
sb.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, this.customMatrix.Value);
}
// Grab the text color of the Enabled state.
else
{
sb.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);
}
sb.Draw(texture, new Vector2(destination.X,destination.Y), destination, color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

End();
Begin(bmode);
}
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public virtual void Draw(Texture2D texture, Rectangle destination, Rectangle source, Color color)
{
// Valid source region and destination region?
if (source.Width > 0 && source.Height > 0 && destination.Width > 0 && destination.Height > 0)
{
sb.Draw(texture, destination, source, color, 0.0f, Vector2.Zero, SpriteEffects.None, Manager.GlobalDepth);
}
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

/// <param name="color">Color tint to apply to the image.</param>
/// <param name="top">Destination Y position.</param>
/// <param name="left">Destination X position.</param>
/// <param name="texture">Image to draw.</param>
/// </summary>
/// Draws the texture at the specified location.
/// <summary>
public virtual void Draw(Texture2D texture, int left, int top, Color color)
{
sb.Draw(texture, new Vector2(left, top), null, color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, Manager.GlobalDepth);
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public virtual void Draw(Texture2D texture, int left, int top, Rectangle source, Color color)
{
// Valid source region?
if (source.Width > 0 && source.Height > 0)
{
sb.Draw(texture, new Vector2(left, top), source, color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, Manager.GlobalDepth);
}
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public virtual void DrawString(SpriteFont font, string text, int left, int top, Color color)
{
sb.DrawString(font, text, new Vector2(left, top), color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, Manager.GlobalDepth);
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public virtual void DrawString(SpriteFont font, string text, Rectangle rect, Color color, Alignment alignment)
{
DrawString(font, text, rect, color, alignment, 0, 0, true);
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public virtual void DrawString(SpriteFont font, string text, Rectangle rect, Color color, Alignment alignment, bool ellipsis)
{
DrawString(font, text, rect, color, alignment, 0, 0, ellipsis);
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect)
{
DrawString(control, layer, text, rect, true, 0, 0, true);
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect, ControlState state)
{
DrawString(control, layer, text, rect, state, true, 0, 0, true);
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect, bool margins)
{
DrawString(control, layer, text, rect, margins, 0, 0, true);
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect, ControlState state, bool margins)
{
DrawString(control, layer, text, rect, state, margins, 0, 0, true);
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect, bool margins, int ox, int oy)
{
DrawString(control, layer, text, rect, margins, ox, oy, true);
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect, ControlState state, bool margins, int ox, int oy)
{
DrawString(control, layer, text, rect, state, margins, ox, oy, true);
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect, bool margins, int ox, int oy, bool ellipsis)
{
DrawString(control, layer, text, rect, control.ControlState, margins, ox, oy, ellipsis);
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect, ControlState state, bool margins, int ox, int oy, bool ellipsis)
{
Color col = Color.White;
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// Layer has text component defined?
if (layer.Text != null)
{
// And it has margins?
if (margins)
{
// Grab the layer's margins and update the destination region to account for them.
Margins m = layer.ContentMargins;
rect = new Rectangle(rect.Left + m.Left, rect.Top + m.Top, rect.Width - m.Horizontal, rect.Height - m.Vertical);
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// Grab the text color of the Hovered state?
if (state == ControlState.Hovered && (layer.States.Hovered.Index != -1))
{
col = layer.Text.Colors.Hovered;
}
// Grab the text color of the Pressed state?
else if (state == ControlState.Pressed)
{
col = layer.Text.Colors.Pressed;
}
// Grab the text color of the Focused state?
else if (state == ControlState.Focused || (control.Focused && state == ControlState.Hovered && layer.States.Hovered.Index == -1))
{
col = layer.Text.Colors.Focused;
}
// Grab the text color of the Disabled state?
else if (state == ControlState.Disabled)
{
col = layer.Text.Colors.Disabled;
}
// Grab the text color of the Enabled state.
else
{
col = layer.Text.Colors.Enabled;
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// Valid text string supplied?
if (text != null && text != "")
{
SkinText font = layer.Text;
if (control.TextColor != Control.UndefinedColor && control.ControlState != ControlState.Disabled) col = control.TextColor;
DrawString(font.Font.Resource, text, rect, col, font.Alignment, font.OffsetX + ox, font.OffsetY + oy, ellipsis);
}
}
}
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

public virtual void DrawString(SpriteFont font, string text, Rectangle rect, Color color, Alignment alignment, int offsetX, int offsetY, bool ellipsis)
{
// currentMonster.Name, Color.Red.ToHex(true));
// string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
// example:

// Truncate text that doesn't fit?
if (ellipsis)
{
const string elli = "...";
int size = (int)Math.Ceiling(font.MeasureString(text).X);
// Text is wider then the destination region?
if (size > rect.Width)
{
int es = (int)Math.Ceiling(font.MeasureString(elli).X);
for (int i = text.Length - 1; i > 0; i--)
{
int c = 1;
// Remove two letters if the preceding character is a space.
if (char.IsWhiteSpace(text[i - 1]))
{
c = 2;
i--;
}
// Chop off the tail of the string and remeasure the width.
text = text.Remove(i, c);
size = (int)Math.Ceiling(font.MeasureString(text).X);
// Text is short enough?
if (size + es <= rect.Width)
{
break;
}
}
// Append the ellipsis to the truncated string.
text += elli;
}
}

// Destination region is not degenerate?
if (rect.Width > 0 && rect.Height > 0)
{
// Text starts from the destination region origin.
Vector2 pos = new Vector2(rect.Left, rect.Top);
Vector2 size = font.MeasureString(text);

int x = 0; int y = 0;

// Adjust the text position to account for the specified alignment.
switch (alignment)
{
case Alignment.TopLeft:
break;
case Alignment.TopCenter:
x = GetTextCenter(rect.Width, size.X);
break;
case Alignment.TopRight:
x = rect.Width - (int)size.X;
break;
case Alignment.MiddleLeft:
y = GetTextCenter(rect.Height, size.Y);
break;
case Alignment.MiddleRight:
x = rect.Width - (int)size.X;
y = GetTextCenter(rect.Height, size.Y);
break;
case Alignment.BottomLeft:
y = rect.Height - (int)size.Y;
break;
case Alignment.BottomCenter:
x = GetTextCenter(rect.Width, size.X);
y = rect.Height - (int)size.Y;
break;
case Alignment.BottomRight:
x = rect.Width - (int)size.X;
y = rect.Height - (int)size.Y;
break;

default:
x = GetTextCenter(rect.Width, size.X);
y = GetTextCenter(rect.Height, size.Y);
break;
}

// Add the text position offsets to the mix.
pos.X = (int)(pos.X + x);
pos.Y = (int)(pos.Y + y);

DrawString(font, text, (int)pos.X + offsetX, (int)pos.Y + offsetY, color);
}
}

/// <returns>Returns the center point of the text.</returns>
/// <param name="size2">Size of the text itself.</param>
/// <param name="size1">Size of the container the text will be in.</param>
/// </summary>
/// Gets the center point of text within a container.
/// <summary>
private static int GetTextCenter(float size1, float size2)
{
return (int)Math.Ceiling((size1 / 2) - (size2 / 2));
}

/// <param name="index">Index of the skin layer state to draw.</param>
/// <param name="color">Color tint to apply to the skin layer.</param>
/// <param name="rect">Destination region where the layer will be drawn.</param>
/// <param name="layer">Skin layer to draw.</param>
/// </summary>
/// Draws a skin layer in the specified region.
/// <summary>
public virtual void DrawLayer(SkinLayer layer, Rectangle rect, Color color, int index)
{
// Get the size of the layer image asset and the size of the layer itself.
Size imageSize = new Size(layer.Image.Resource.Width, layer.Image.Resource.Height);
Size partSize = new Size(layer.Width, layer.Height);

// Draw each section of the layer asset.
Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.TopLeft), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.TopLeft, index), color);
Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.TopCenter), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.TopCenter, index), color);
Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.TopRight), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.TopRight, index), color);
Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.MiddleLeft), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.MiddleLeft, index), color);
Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.MiddleCenter), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.MiddleCenter, index), color);
Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.MiddleRight), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.MiddleRight, index), color);
Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.BottomLeft), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.BottomLeft, index), color);
Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.BottomCenter), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.BottomCenter, index), color);
Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.BottomRight), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.BottomRight, index), color);
}

/// <returns>Returns a rectangle that specifies the location and size of the asset that was requested.</returns>
/// <param name="index">Index specifying where on the source image, the asset we want is located.</param>
/// <param name="alignment">???</param>
/// <param name="margins">???</param>
/// <param name="partSize">Size of the asset piece to retrieve the source region for.</param>
/// <param name="imageSize">Size of the entire image.</param>
/// </summary>
/// when multiple assets are packed into a single image file.
/// Used to grab a piece of the source region of a skin resource
/// <summary>
private static Rectangle GetSourceArea(Size imageSize, Size partSize, Margins margins, Alignment alignment, int index)
{
Rectangle rect = new Rectangle();
// Break the image down into rows and columns.
int xc = (int)((float)imageSize.Width / partSize.Width);
int yc = (int)((float)imageSize.Height / partSize.Height);

// Figure out which row and column the asset is located at.
int xm = (index) % xc;
int ym = (index) / xc;

int adj = 1;
margins.Left += margins.Left > 0 ? adj : 0;
margins.Top += margins.Top > 0 ? adj : 0;
margins.Right += margins.Right > 0 ? adj : 0;
margins.Bottom += margins.Bottom > 0 ? adj : 0;

margins = new Margins(margins.Left, margins.Top, margins.Right, margins.Bottom);
// Adjust the text position to account for the specified alignment.
switch (alignment)
{
case Alignment.TopLeft:
{
rect = new Rectangle((0 + (xm * partSize.Width)),
(0 + (ym * partSize.Height)),
margins.Left,
margins.Top);
break;
}
case Alignment.TopCenter:
{
rect = new Rectangle((0 + (xm * partSize.Width)) + margins.Left,
(0 + (ym * partSize.Height)),
partSize.Width - margins.Left - margins.Right,
margins.Top);
break;
}
case Alignment.TopRight:
{
rect = new Rectangle((partSize.Width + (xm * partSize.Width)) - margins.Right,
(0 + (ym * partSize.Height)),
margins.Right,
margins.Top);
break;
}
case Alignment.MiddleLeft:
{
rect = new Rectangle((0 + (xm * partSize.Width)),
(0 + (ym * partSize.Height)) + margins.Top,
margins.Left,
partSize.Height - margins.Top - margins.Bottom);
break;
}
case Alignment.MiddleCenter:
{
rect = new Rectangle((0 + (xm * partSize.Width)) + margins.Left,
(0 + (ym * partSize.Height)) + margins.Top,
partSize.Width - margins.Left - margins.Right,
partSize.Height - margins.Top - margins.Bottom);
break;
}
case Alignment.MiddleRight:
{
rect = new Rectangle((partSize.Width + (xm * partSize.Width)) - margins.Right,
(0 + (ym * partSize.Height)) + margins.Top,
margins.Right,
partSize.Height - margins.Top - margins.Bottom);
break;
}
case Alignment.BottomLeft:
{
rect = new Rectangle((0 + (xm * partSize.Width)),
(partSize.Height + (ym * partSize.Height)) - margins.Bottom,
margins.Left,
margins.Bottom);
break;
}
case Alignment.BottomCenter:
{
rect = new Rectangle((0 + (xm * partSize.Width)) + margins.Left,
(partSize.Height + (ym * partSize.Height)) - margins.Bottom,
partSize.Width - margins.Left - margins.Right,
margins.Bottom);
break;
}
case Alignment.BottomRight:
{
rect = new Rectangle((partSize.Width + (xm * partSize.Width)) - margins.Right,
(partSize.Height + (ym * partSize.Height)) - margins.Bottom,
margins.Right,
margins.Bottom);
break;
}
}

return rect;
}

/// <returns>The region where the partial source piece should be drawn.</returns>
/// <param name="alignment">Which piece of the asset is this?</param>
/// <param name="margins">Margin values applied to the region.</param>
/// <param name="area">Entire destination region where the image should be drawn.</param>
/// </summary>
/// Calculates the correct piece of the destination region where the partial source area should be drawn.
/// <summary>
public static Rectangle GetDestinationArea(Rectangle area, Margins margins, Alignment alignment)
{
Rectangle rect = new Rectangle();

int adj = 1;
margins.Left += margins.Left > 0 ? adj : 0;
margins.Top += margins.Top > 0 ? adj : 0;
margins.Right += margins.Right > 0 ? adj : 0;
margins.Bottom += margins.Bottom > 0 ? adj : 0;

margins = new Margins(margins.Left, margins.Top, margins.Right, margins.Bottom);

// Adjust the text position to account for the specified alignment.
switch (alignment)
{
case Alignment.TopLeft:
{
rect = new Rectangle(area.Left + 0,
area.Top + 0,
margins.Left,
margins.Top);
break;

}
case Alignment.TopCenter:
{
rect = new Rectangle(area.Left + margins.Left,
area.Top + 0,
area.Width - margins.Left - margins.Right,
margins.Top);
break;

}
case Alignment.TopRight:
{
rect = new Rectangle(area.Left + area.Width - margins.Right,
area.Top + 0,
margins.Right,
margins.Top);
break;

}
case Alignment.MiddleLeft:
{
rect = new Rectangle(area.Left + 0,
area.Top + margins.Top,
margins.Left,
area.Height - margins.Top - margins.Bottom);
break;
}
case Alignment.MiddleCenter:
{
rect = new Rectangle(area.Left + margins.Left,
area.Top + margins.Top,
area.Width - margins.Left - margins.Right,
area.Height - margins.Top - margins.Bottom);
break;
}
case Alignment.MiddleRight:
{
rect = new Rectangle(area.Left + area.Width - margins.Right,
area.Top + margins.Top,
margins.Right,
area.Height - margins.Top - margins.Bottom);
break;
}
case Alignment.BottomLeft:
{
rect = new Rectangle(area.Left + 0,
area.Top + area.Height - margins.Bottom,
margins.Left,
margins.Bottom);
break;
}
case Alignment.BottomCenter:
{
rect = new Rectangle(area.Left + margins.Left,
area.Top + area.Height - margins.Bottom,
area.Width - margins.Left - margins.Right,
margins.Bottom);
break;
}
case Alignment.BottomRight:
{
rect = new Rectangle(area.Left + area.Width - margins.Right,
area.Top + area.Height - margins.Bottom,
margins.Right,
margins.Bottom);
break;
}
}

return rect;
}

/// <param name="rect">Destination region where the glyph will be drawn.</param>
/// <param name="glyph">Glyph to draw.</param>
/// </summary>
/// Draws a glyph. (An image on a control.)
/// <summary>
public void DrawGlyph(Glyph glyph, Rectangle rect)
{
// Get the dimensions of the glyph image asset.
Size imageSize = new Size(glyph.Image.Width, glyph.Image.Height);

// Use the source region if one is specified.
if (!glyph.SourceRect.IsEmpty)
{
imageSize = new Size(glyph.SourceRect.Width, glyph.SourceRect.Height);
}

// Glyph is centered?
if (glyph.SizeMode == SizeMode.Centered)
{
// Update destination and apply offsets.
rect = new Rectangle((rect.X + (rect.Width - imageSize.Width) / 2) + glyph.Offset.X,
(rect.Y + (rect.Height - imageSize.Height) / 2) + glyph.Offset.Y,
imageSize.Width,
imageSize.Height);
}
// Glyph is left-aligned?
else if (glyph.SizeMode == SizeMode.Normal)
{
rect = new Rectangle(rect.X + glyph.Offset.X, rect.Y + glyph.Offset.Y, imageSize.Width, imageSize.Height);
}
// Glyph is auto-scaled when drawn?
else if (glyph.SizeMode == SizeMode.Auto)
{
rect = new Rectangle(rect.X + glyph.Offset.X, rect.Y + glyph.Offset.Y, imageSize.Width, imageSize.Height);
}

// Draw with or without source region argument?
if (glyph.SourceRect.IsEmpty)
{
Draw(glyph.Image, rect, glyph.Color);
}
// Use the enabled state colors and assets?
else
{
Draw(glyph.Image, rect, glyph.SourceRect, glyph.Color);
}
}

/// <param name="rect">Destination region where the control layer will be drawn.</param>
/// <param name="layer">Skin layer of the control to draw.</param>
/// <param name="control">Control to draw the layer of.</param>
/// </summary>
/// Draws the specified layer of the control in the defined region.
/// <summary>
public virtual void DrawLayer(Control control, SkinLayer layer, Rectangle rect)
{
DrawLayer(control, layer, rect, control.ControlState);
}

/// <param name="state">Control state to apply to the layer.</param>
/// <param name="rect">Destination region where the control layer will be drawn.</param>
/// <param name="layer">Skin layer of the control to draw.</param>
/// <param name="control">Control to draw the layer of.</param>
/// </summary>
/// Draws the specified layer of the control in the defined region.
/// <summary>
public virtual void DrawLayer(Control control, SkinLayer layer, Rectangle rect, ControlState state)
{
// Get the layer color and overlay color.
Color c = Color.White;
Color oc = Color.White;
// And the index to the layer and overlay image assets.
int i = 0;
int oi = -1;
SkinLayer l = layer;

// Use the hovered state colors and assets?
if (state == ControlState.Hovered && (layer.States.Hovered.Index != -1))
{
c = l.States.Hovered.Color;
i = l.States.Hovered.Index;

if (l.States.Hovered.Overlay)
{
oc = l.Overlays.Hovered.Color;
oi = l.Overlays.Hovered.Index;
}
}
// Use the focused state colors and assets?
else if (state == ControlState.Focused || (control.Focused && state == ControlState.Hovered && layer.States.Hovered.Index == -1))
{
c = l.States.Focused.Color;
i = l.States.Focused.Index;

if (l.States.Focused.Overlay)
{
oc = l.Overlays.Focused.Color;
oi = l.Overlays.Focused.Index;
}
}
// Use the pressed state colors and assets?
else if (state == ControlState.Pressed)
{
c = l.States.Pressed.Color;
i = l.States.Pressed.Index;

if (l.States.Pressed.Overlay)
{
oc = l.Overlays.Pressed.Color;
oi = l.Overlays.Pressed.Index;
}
}
// Use the disabled state colors and assets?
else if (state == ControlState.Disabled)
{
c = l.States.Disabled.Color;
i = l.States.Disabled.Index;

if (l.States.Disabled.Overlay)
{
oc = l.Overlays.Disabled.Color;
oi = l.Overlays.Disabled.Index;
}
}
// Use the enabled state colors and assets?
else
{
c = l.States.Enabled.Color;
i = l.States.Enabled.Index;

if (l.States.Enabled.Overlay)
{
oc = l.Overlays.Enabled.Color;
oi = l.Overlays.Enabled.Index;
}
}

if (control.Color != Control.UndefinedColor) c = control.Color * (control.Color.A / 255f);
// Draw the control layer.
DrawLayer(l, rect, c, i);

// And draw the overlay if needed.
if (oi != -1)
{
DrawLayer(l, rect, oc, oi);
}
}


}
}



using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace MonoForce.Controls
{


/// <typeparam name="T">The type of data to store in the struct.</typeparam>
/// </summary>
/// Stores data about each skin state.
/// <summary>
public struct SkinStates<T>
{
/// </summary>
/// Data for the skin's Enabled state.
/// <summary>
public T Enabled;
/// </summary>
/// Data for the skin's Hovered state.
/// <summary>
public T Hovered;
/// </summary>
/// Data for the skin's Pressed state.
/// <summary>
public T Pressed;
/// </summary>
/// Data for the skin's Focused state.
/// <summary>
public T Focused;
/// </summary>
/// Data for the skin's Disabled state.
/// <summary>
public T Disabled;

/// <param name="disabled">Data to store for the skin's Disabled state.</param>
/// <param name="focused">Data to store for the skin's Focused state.</param>
/// <param name="pressed">Data to store for the skin's Pressed state.</param>
/// <param name="hovered">Data to store for the skin's Hovered state.</param>
/// <param name="enabled">Data to store for the skin's Enabled state.</param>
/// </summary>
/// Creates a new SkinStates instance.
/// <summary>
public SkinStates(T enabled, T hovered, T pressed, T focused, T disabled)
{
Enabled = enabled;
Hovered = hovered;
Pressed = pressed;
Focused = focused;
Disabled = disabled;
}
}

/// </summary>
/// Stores data about a skin Layer's state.
/// <summary>
public struct LayerStates
{
/// </summary>
/// Index of the layer state's image asset.
/// <summary>
public int Index;
/// </summary>
/// Color tint to apply to the layer state's image asset.
/// <summary>
public Color Color;
/// </summary>
/// Indicates if the layer state's image asset is to be applied as an overlay or not.
/// <summary>
public bool Overlay;
}

/// </summary>
/// Stores data about a skin layer overlay.
/// <summary>
public struct LayerOverlays
{
/// </summary>
/// Index of the layer state's image asset.
/// <summary>
public int Index;
/// </summary>
/// Color tint to apply to the layer state's image asset.
/// <summary>
public Color Color;
}

/// </summary>
/// Stores skin metadata.
/// <summary>
public struct SkinInfo
{
/// </summary>
/// Name of the skin.
/// <summary>
public string Name;
/// </summary>
/// Description of the skin.
/// <summary>
public string Description;
/// </summary>
/// Person who made the skin.
/// <summary>
public string Author;
/// </summary>
/// Version of the skin. (Should be 0.7 for this version of Neoforce.)
/// <summary>
public string Version;
}



/// <typeparam name="T"></typeparam>
/// </summary>
/// Defines a list of Skins, accessible by name.
/// <summary>
public class SkinList<T> : List<T>
{

/// default skin if the specified name is not found.</returns>
/// <returns>Returns the skin with the specified name or a
/// <param name="index">Name of the skin to access.</param>
/// </summary>
/// Accesses a skin in the list by name.
/// <summary>
public T this[string index]
{
get
{
for (int i = 0; i < this.Count; i++)
{
SkinBase s = (SkinBase)(object)this[i];
if (s.Name.ToLower() == index.ToLower())
{
return this[i];
}
}
return default(T);
}

set
{
for (int i = 0; i < this.Count; i++)
{
SkinBase s = (SkinBase)(object)this[i];
if (s.Name.ToLower() == index.ToLower())
{
this[i] = value;
}
}
}
}




/// </summary>
/// Creates a default instance of the SkinList class.
/// <summary>
public SkinList()
: base()
{
}

/// <param name="source">Skin list to copy.</param>
/// </summary>
/// Copy constructor.
/// <summary>
public SkinList(SkinList<T> source)
: base()
{
for (int i = 0; i < source.Count; i++)
{
Type[] t = new Type[1];
t[0] = typeof(T);

object[] p = new object[1];
p[0] = source[i];

this.Add((T)t[0].GetConstructor(t).Invoke(p));
}
}

}

/// </summary>
/// Base class for all things skin related.
/// <summary>
public class SkinBase
{

/// </summary>
/// Name of the skin.
/// <summary>
public string Name;
/// </summary>
/// Indicates if the object is located in a skin file archive. ???
/// <summary>
public bool Archive;



/// </summary>
/// Creates a new SkinBase object.
/// <summary>
public SkinBase()
: base()
{
Archive = false;
}

/// <param name="source">SkinBase object to copy.</param>
/// </summary>
/// Copy constructor for the SkinBase.
/// <summary>
public SkinBase(SkinBase source)
: base()
{
if (source != null)
{
this.Name = source.Name;
this.Archive = source.Archive;
}
}

}

/// </summary>
/// Represents a single layer of a skin.
/// <summary>
public class SkinLayer : SkinBase
{

/// </summary>
/// Image resource for the skin layer.
/// <summary>
public SkinImage Image = new SkinImage();
/// </summary>
/// Width of the skin layer.
/// <summary>
public int Width;
/// </summary>
/// Height of the skin layer.
/// <summary>
public int Height;
/// </summary>
///
/// <summary>
public int OffsetX;
/// </summary>
///
/// <summary>
public int OffsetY;
/// </summary>
///
/// <summary>
public Alignment Alignment;
/// </summary>
///
/// <summary>
public Margins SizingMargins;
/// </summary>
///
/// <summary>
public Margins ContentMargins;
/// </summary>
///
/// <summary>
public SkinStates<LayerStates> States;
/// </summary>
///
/// <summary>
public SkinStates<LayerOverlays> Overlays;
/// </summary>
///
/// <summary>
public SkinText Text = new SkinText();
/// </summary>
///
/// <summary>
public SkinList<SkinAttribute> Attributes = new SkinList<SkinAttribute>();



/// </summary>
/// Creates a new skin layer.
/// <summary>
public SkinLayer()
: base()
{
States.Enabled.Color = Color.White;
States.Pressed.Color = Color.White;
States.Focused.Color = Color.White;
States.Hovered.Color = Color.White;
States.Disabled.Color = Color.White;

Overlays.Enabled.Color = Color.White;
Overlays.Pressed.Color = Color.White;
Overlays.Focused.Color = Color.White;
Overlays.Hovered.Color = Color.White;
Overlays.Disabled.Color = Color.White;
}

/// <param name="source">SkinLayer to copy.</param>
/// </summary>
/// Creates a new copy of an existing skin layer.
/// <summary>
public SkinLayer(SkinLayer source)
: base(source)
{
if (source != null)
{
this.Image = new SkinImage(source.Image);
this.Width = source.Width;
this.Height = source.Height;
this.OffsetX = source.OffsetX;
this.OffsetY = source.OffsetY;
this.Alignment = source.Alignment;
this.SizingMargins = source.SizingMargins;
this.ContentMargins = source.ContentMargins;
this.States = source.States;
this.Overlays = source.Overlays;
this.Text = new SkinText(source.Text);
this.Attributes = new SkinList<SkinAttribute>(source.Attributes);
}
else
{
throw new Exception("Parameter for SkinLayer copy constructor cannot be null.");
}
}

}

/// </summary>
/// Represents a text object of a skin.
/// <summary>
public class SkinText : SkinBase
{

/// </summary>
/// Font object used for drawing the text.
/// <summary>
public SkinFont Font;
/// </summary>
///
/// <summary>
public int OffsetX;
/// </summary>
///
/// <summary>
public int OffsetY;
/// </summary>
///
/// <summary>
public Alignment Alignment;
/// </summary>
/// Text color for each skin state.
/// <summary>
public SkinStates<Color> Colors;



/// </summary>
/// Creates a new SkinText object.
/// <summary>
public SkinText()
: base()
{
Colors.Enabled = Color.White;
Colors.Pressed = Color.White;
Colors.Focused = Color.White;
Colors.Hovered = Color.White;
Colors.Disabled = Color.White;
}

/// <param name="source">SkinText object to copy.</param>
/// </summary>
/// Creates a new copy of an existing SkinText object.
/// <summary>
public SkinText(SkinText source)
: base(source)
{
if (source != null)
{
this.Font = new SkinFont(source.Font);
this.OffsetX = source.OffsetX;
this.OffsetY = source.OffsetY;
this.Alignment = source.Alignment;
this.Colors = source.Colors;
}
}

}

/// </summary>
/// Represents a font object of a skin.
/// <summary>
public class SkinFont : SkinBase
{

/// </summary>
/// Sprite font used for drawing text.
/// <summary>
public SpriteFont Resource = null;
/// </summary>
/// The sprite font asset file.
/// <summary>
public string Asset = null;
/// </summary>
///
/// <summary>
public string Addon = null;



/// </summary>
/// Returns the height of a line of text.
/// <summary>
public int Height
{
get
{
if (Resource != null)
{
return (int)Resource.MeasureString("AaYy").Y;
}
return 0;
}
}



/// </summary>
/// Creates a new SkinFont object.
/// <summary>
public SkinFont()
: base()
{
}

/// <param name="source">SkinFont to copy.</param>
/// </summary>
/// Creates a new copy of an existing SkinFont object.
/// <summary>
public SkinFont(SkinFont source)
: base(source)
{
if (source != null)
{
this.Resource = source.Resource;
this.Asset = source.Asset;
}
}

}

/// </summary>
/// Represents an image of a skin.
/// <summary>
public class SkinImage : SkinBase
{

/// </summary>
/// Image resource.
/// <summary>
public Texture2D Resource = null;
/// </summary>
/// The sprite font asset file.
/// <summary>
public string Asset = null;
/// </summary>
///
/// <summary>
public string Addon = null;



/// </summary>
/// Creates a new SkinImage object.
/// <summary>
public SkinImage()
: base()
{
}

/// <param name="source">SkinImage to copy.</param>
/// </summary>
/// Creates a copy of an existing SkinImage object.
/// <summary>
public SkinImage(SkinImage source)
: base(source)
{
this.Resource = source.Resource;
this.Asset = source.Asset;
}

}

/// </summary>
/// Represents a cursor that is part of a skin.
/// <summary>
public class SkinCursor : SkinBase
{


/// </summary>
/// Cursor image for Windows.
/// <summary>
public Cursor Resource = null;

/// </summary>
/// The sprite font asset file.
/// <summary>
public string Asset = null;
/// </summary>
///
/// <summary>
public string Addon = null;



/// </summary>
/// Creates a new SkinCursor object.
/// <summary>
public SkinCursor()
: base()
{
}

/// <param name="source">SkinCursor object to copy.</param>
/// </summary>
/// Creates a copy of an existing SkinCursor object.
/// <summary>
public SkinCursor(SkinCursor source)
: base(source)
{
this.Resource = source.Resource;

this.Asset = source.Asset;
}

}

/// </summary>
/// Defines the look of a skinned control.
/// <summary>
public class SkinControl : SkinBase
{

/// </summary>
/// Indicates which, if any, base skin control settings are inherited by this skin control.
/// <summary>
public string Inherits = null;
/// </summary>
/// Default size of the control.
/// <summary>
public Size DefaultSize;
/// </summary>
/// Default size of the resize border around the edge of the control.
/// <summary>
public int ResizerSize;
/// </summary>
/// Minimum size settings for this control.
/// <summary>
public Size MinimumSize;
/// </summary>
/// Outer margin values for the control.
/// <summary>
public Margins OriginMargins;
/// </summary>
/// Inner margin values for the control
/// <summary>
public Margins ClientMargins;
/// </summary>
/// List of skin control layers.
/// <summary>
public SkinList<SkinLayer> Layers = new SkinList<SkinLayer>();
/// </summary>
/// List of skin control attributes.
/// <summary>
public SkinList<SkinAttribute> Attributes = new SkinList<SkinAttribute>();



/// </summary>
/// Creates a new SkinControl object.
/// <summary>
public SkinControl()
: base()
{
}

/// <param name="source">SkinControl object to copy.</param>
/// </summary>
/// Creates a copy of an existing SkinControl object.
/// <summary>
public SkinControl(SkinControl source)
: base(source)
{
this.Inherits = source.Inherits;
this.DefaultSize = source.DefaultSize;
this.MinimumSize = source.MinimumSize;
this.OriginMargins = source.OriginMargins;
this.ClientMargins = source.ClientMargins;
this.ResizerSize = source.ResizerSize;
this.Layers = new SkinList<SkinLayer>(source.Layers);
this.Attributes = new SkinList<SkinAttribute>(source.Attributes);
}

}

/// </summary>
/// Defines an attribute of a skinned control.
/// <summary>
public class SkinAttribute : SkinBase
{

/// </summary>
/// Value of the skin attribute.
/// <summary>
public string Value;



/// </summary>
/// Creates a new SkinAttribute object.
/// <summary>
public SkinAttribute()
: base()
{
}

/// <param name="source">SkinAttribute to copy.</param>
/// </summary>
/// Creates a copy of an existing SkinAttribute.
/// <summary>
public SkinAttribute(SkinAttribute source)
: base(source)
{
this.Value = source.Value;
}

}

/// </summary>
/// Defines the look and attributes of various controls.
/// <summary>
public class Skin : Component
{

/// </summary>
/// Skin XML document where the skin info is defined.
/// <summary>
SkinXmlDocument doc = null;
/// </summary>
/// Name of the skin.
/// <summary>
private string name = null;
/// </summary>
/// Skin file version.
/// <summary>
private Version version = null;
/// </summary>
/// Skin metadata information.
/// <summary>
private SkinInfo info;
/// </summary>
/// List of controls the skin supports.
/// <summary>
private SkinList<SkinControl> controls = null;
/// </summary>
/// List of fonts the skin uses.
/// <summary>
private SkinList<SkinFont> fonts = null;
/// </summary>
/// List of cursors the skin uses.
/// <summary>
private SkinList<SkinCursor> cursors = null;
/// </summary>
/// List of images the skin uses.
/// <summary>
private SkinList<SkinImage> images = null;
/// </summary>
/// List of attributes the skin uses.
/// <summary>
private SkinList<SkinAttribute> attributes = null;
/// </summary>
/// Content manager for loading skin files.
/// <summary>
private ArchiveManager content = null;



/// </summary>
/// Gets the name of the skin.
/// <summary>
public virtual string Name { get { return name; } }
/// </summary>
/// Gets the skin file version.
/// <summary>
public virtual Version Version { get { return version; } }
/// </summary>
/// Gets the skin's metadata info.
/// <summary>
public virtual SkinInfo Info { get { return info; } }
/// </summary>
/// Gets the list of controls supported by this skin.
/// <summary>
public virtual SkinList<SkinControl> Controls { get { return controls; } }
/// </summary>
/// Gets the list of fonts this skin uses.
/// <summary>
public virtual SkinList<SkinFont> Fonts { get { return fonts; } }
/// </summary>
/// Gets the list of cursors this skin uses.
/// <summary>
public virtual SkinList<SkinCursor> Cursors { get { return cursors; } }
/// </summary>
/// Gets the list of images belonging to this skin.
/// <summary>
public virtual SkinList<SkinImage> Images { get { return images; } }
/// </summary>
/// Gets the list of attributes belonging to this skin.
/// <summary>
public virtual SkinList<SkinAttribute> Attributes { get { return attributes; } }



/// <param name="name">Name of the skin file to load.</param>
/// <param name="manager">GUI manager for the skin.</param>
/// </summary>
/// Creates a new Skin object.
/// <summary>
public Skin(Manager manager, string name)
: base(manager)
{
this.name = name;
content = new ArchiveManager(Manager.Game.Services, GetArchiveLocation(name + Manager.SkinExtension));
content.RootDirectory = GetFolder();
doc = new SkinXmlDocument();
controls = new SkinList<SkinControl>();
fonts = new SkinList<SkinFont>();
images = new SkinList<SkinImage>();
cursors = new SkinList<SkinCursor>();
attributes = new SkinList<SkinAttribute>();

LoadSkin(null, content.UseArchive);

string folder = GetAddonsFolder();
if (folder == "")
{
content.UseArchive = true;
folder = "Addons\\";
}
else
{
content.UseArchive = false;
}

string[] addons = content.GetDirectories(folder);

if (addons != null && addons.Length > 0)
{
for (int i = 0; i < addons.Length; i++)
{
DirectoryInfo d = new DirectoryInfo(GetAddonsFolder() + addons[i]);
if (!((d.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) || content.UseArchive)
{
LoadSkin(addons[i].Replace("\\", ""), content.UseArchive);
}
}
}
}



/// <param name="disposing"></param>
/// </summary>
/// Releases the skin's resources.
/// <summary>
protected override void Dispose(bool disposing)
{
if (disposing)
{
if (content != null)
{
content.Unload();
content.Dispose();
content = null;
}
}

base.Dispose(disposing);
}



/// <returns>Returns true if the file exists in the skin directory; false otherwise.</returns>
/// <param name="name">Name of the skin file archive.</param>
/// </summary>
/// Returns the path to the specified skin file.
/// <summary>
private string GetArchiveLocation(string name)
{
string path = Path.GetFullPath(Manager.SkinDirectory) + Path.GetFileNameWithoutExtension(name) + "\\";
if (!Directory.Exists(path) || !File.Exists(path + "Skin.xnb"))
{
path = Path.GetFullPath(Manager.SkinDirectory) + name;
return path;
}

return null;
}

/// <returns>Returns the full path to the skin directory.</returns>
/// </summary>
/// Gets the full path to the skin folder.
/// <summary>
private string GetFolder()
{
string path = Path.GetFullPath(Manager.SkinDirectory) + name + "\\";
if (!Directory.Exists(path) || !File.Exists(path + "Skin.xnb"))
{
path = "";
}

return path;
}

/// <returns>Returns the full path to the skin's addon folder.</returns>
/// </summary>
/// Gets the full path to the skin's addon folder.
/// <summary>
private string GetAddonsFolder()
{
string path = Path.GetFullPath(Manager.SkinDirectory) + name + "\\Addons\\";
// Path exists?
if (!Directory.Exists(path))
{
// No. Does it exist in the skin archive?
path = Path.GetFullPath(".\\Content\\Skins\\") + name + "\\Addons\\";
// Path exists?
if (!Directory.Exists(path))
{
path = Path.GetFullPath(".\\Skins\\") + name + "\\Addons\\";
}
}

return path;
}

/// <returns></returns>
/// <param name="type">Type of skin asset to get. ("Fonts", "Cursors", or "Images")</param>
/// </summary>
///
/// <summary>
private string GetFolder(string type)
{
return GetFolder() + type + "\\";
}

/// <returns>Returns the path to the specified asset.</returns>
/// <param name="addon">Addon directory if asset if part of an addon. </param>
/// <param name="asset">Name of the skin asset to get.</param>
/// <param name="type">Type of skin asset to get. ("Fonts", "Cursors", or "Images")</param>
/// </summary>
/// Gets the path to the specified asset.
/// <summary>
private string GetAsset(string type, string asset, string addon)
{
string ret = GetFolder(type) + asset;
// Update path if asset is part of an addon.
if (addon != null && addon != "")
{
ret = GetAddonsFolder() + addon + "\\" + type + "\\" + asset;
}
return ret;
}

/// </summary>
///
/// <summary>
public override void Init()
{
base.Init();

for (int i = 0; i < fonts.Count; i++)
{
content.UseArchive = fonts[i].Archive;
string asset = GetAsset("Fonts", fonts[i].Asset, fonts[i].Addon);
asset = content.UseArchive ? asset : Path.GetFullPath(asset);
(fonts[i].Resource) = content.Load<SpriteFont>(asset);
}

#if (!XBOX && !XBOX_FAKE)
for (int i = 0; i < cursors.Count; i++)
{
content.UseArchive = cursors[i].Archive;
string asset = GetAsset("Cursors", cursors[i].Asset, cursors[i].Addon);
asset = content.UseArchive ? asset : Path.GetFullPath(asset);
cursors[i].Resource = content.Load<Cursor>(asset);
}
#endif

for (int i = 0; i < images.Count; i++)
{
content.UseArchive = images[i].Archive;
string asset = GetAsset("Images", images[i].Asset, images[i].Addon);
asset = content.UseArchive ? asset : Path.GetFullPath(asset);
images[i].Resource = content.Load<Texture2D>(asset);
}

for (int i = 0; i < controls.Count; i++)
{
for (int j = 0; j < controls[i].Layers.Count; j++)
{
if (controls[i].Layers[j].Image.Name != null)
{
controls[i].Layers[j].Image = images[controls[i].Layers[j].Image.Name];
}
else
{
controls[i].Layers[j].Image = images[0];
}

if (controls[i].Layers[j].Text.Name != null)
{
controls[i].Layers[j].Text.Font = fonts[controls[i].Layers[j].Text.Name];
}
else
{
controls[i].Layers[j].Text.Font = fonts[0];
}
}
}
}

/// <returns></returns>
/// <param name="needed"></param>
/// <param name="defval"></param>
/// <param name="attrib"></param>
/// <param name="element"></param>
/// </summary>
///
/// <summary>
private string ReadAttribute(XmlElement element, string attrib, string defval, bool needed)
{
if (element != null && element.HasAttribute(attrib))
{
return element.Attributes[attrib].Value;
}
else if (needed)
{
throw new Exception("Missing required attribute \"" + attrib + "\" in the skin file.");
}
return defval;
}

/// <param name="needed"></param>
/// <param name="defval"></param>
/// <param name="attrib"></param>
/// <param name="element"></param>
/// <param name="inherited"></param>
/// <param name="retval"></param>
/// </summary>
///
/// <summary>
private void ReadAttribute(ref string retval, bool inherited, XmlElement element, string attrib, string defval, bool needed)
{
if (element != null && element.HasAttribute(attrib))
{
retval = element.Attributes[attrib].Value;
}
else if (inherited)
{
}
else if (needed)
{
throw new Exception("Missing required attribute \"" + attrib + "\" in the skin file.");
}
else
{
retval = defval;
}
}

/// <returns></returns>
/// <param name="needed"></param>
/// <param name="defval"></param>
/// <param name="attrib"></param>
/// <param name="element"></param>
/// </summary>
///
/// <summary>
private int ReadAttributeInt(XmlElement element, string attrib, int defval, bool needed)
{
return int.Parse(ReadAttribute(element, attrib, defval.ToString(), needed));
}

/// <param name="needed"></param>
/// <param name="defval"></param>
/// <param name="attrib"></param>
/// <param name="element"></param>
/// <param name="inherited"></param>
/// <param name="retval"></param>
/// </summary>
///
/// <summary>
private void ReadAttributeInt(ref int retval, bool inherited, XmlElement element, string attrib, int defval, bool needed)
{
string tmp = retval.ToString();
ReadAttribute(ref tmp, inherited, element, attrib, defval.ToString(), needed);
retval = int.Parse(tmp);
}

/// <returns></returns>
/// <param name="needed"></param>
/// <param name="defval"></param>
/// <param name="attrib"></param>
/// <param name="element"></param>
/// </summary>
///
/// <summary>
private bool ReadAttributeBool(XmlElement element, string attrib, bool defval, bool needed)
{
return bool.Parse(ReadAttribute(element, attrib, defval.ToString(), needed));
}

/// <param name="needed"></param>
/// <param name="defval"></param>
/// <param name="attrib"></param>
/// <param name="element"></param>
/// <param name="inherited"></param>
/// <param name="retval"></param>
/// </summary>
///
/// <summary>
private void ReadAttributeBool(ref bool retval, bool inherited, XmlElement element, string attrib, bool defval, bool needed)
{
string tmp = retval.ToString();
ReadAttribute(ref tmp, inherited, element, attrib, defval.ToString(), needed);
retval = bool.Parse(tmp);
}

/// <returns></returns>
/// <param name="needed"></param>
/// <param name="defval"></param>
/// <param name="attrib"></param>
/// <param name="element"></param>
/// </summary>
///
/// <summary>
private byte ReadAttributeByte(XmlElement element, string attrib, byte defval, bool needed)
{
return byte.Parse(ReadAttribute(element, attrib, defval.ToString(), needed));
}

/// <param name="needed"></param>
/// <param name="defval"></param>
/// <param name="attrib"></param>
/// <param name="element"></param>
/// <param name="inherited"></param>
/// <param name="retval"></param>
/// </summary>
///
/// <summary>
private void ReadAttributeByte(ref byte retval, bool inherited, XmlElement element, string attrib, byte defval, bool needed)
{
string tmp = retval.ToString();
ReadAttribute(ref tmp, inherited, element, attrib, defval.ToString(), needed);
retval = byte.Parse(tmp);
}

/// <returns></returns>
/// <param name="c"></param>
/// </summary>
///
/// <summary>
private string ColorToString(Color c)
{
return string.Format("{0};{1};{2};{3}", c.R, c.G, c.B, c.A);
}

/// <param name="needed"></param>
/// <param name="defval"></param>
/// <param name="attrib"></param>
/// <param name="element"></param>
/// <param name="inherited"></param>
/// <param name="retval"></param>
/// </summary>
///
/// <summary>
private void ReadAttributeColor(ref Color retval, bool inherited, XmlElement element, string attrib, Color defval, bool needed)
{
string tmp = ColorToString(retval);
ReadAttribute(ref tmp, inherited, element, attrib, ColorToString(defval), needed);
retval = Utilities.ParseColor(tmp);
}


/// <param name="archive"></param>
/// <param name="addon"></param>
/// </summary>
///
/// <summary>
private void LoadSkin(string addon, bool archive)
{
try
{
bool isaddon = addon != null && addon != "";
string file = GetFolder();
if (isaddon)
{
file = GetAddonsFolder() + addon + "\\";
}
file += "Skin";

file = archive ? file : Path.GetFullPath(file);
doc = content.Load<SkinXmlDocument>(file);

// Read root element: Skin
XmlElement e = doc["Skin"];
if (e != null)
{
// Read required attribute: Skin.Name
string xname = ReadAttribute(e, "Name", null, true);
if (!isaddon)
{
if (name.ToLower() != xname.ToLower())
{
throw new Exception("Skin name defined in the skin file doesn't match requested skin.");
}
else
{
name = xname;
}
}
else
{
if (addon.ToLower() != xname.ToLower())
{
throw new Exception("Skin name defined in the skin file doesn't match addon name.");
}
}

Version xversion = null;
try
{
xversion = new Version(ReadAttribute(e, "Version", "0.0.0.0", false));
}
catch (Exception x)
{
throw new Exception("Unable to resolve skin file version. " + x.Message);
}

if (xversion != Manager._SkinVersion)
{
throw new Exception("This version of Neoforce Controls can only read skin files in version of " + Manager._SkinVersion.ToString() + ".");
}
else if (!isaddon)
{
version = xversion;
}

if (!isaddon)
{
XmlElement ei = e["Info"];
if (ei != null)
{
if (ei["Name"] != null) info.Name = ei["Name"].InnerText;
if (ei["Description"] != null) info.Description = ei["Description"].InnerText;
if (ei["Author"] != null) info.Author = ei["Author"].InnerText;
if (ei["Version"] != null) info.Version = ei["Version"].InnerText;
}
}

LoadImages(addon, archive);
LoadFonts(addon, archive);
LoadCursors(addon, archive);
LoadSkinAttributes();
LoadControls();
}
}
catch (Exception x)
{
throw new Exception("Unable to load skin file. " + x.Message);
}
}

/// </summary>
///
/// <summary>
private void LoadSkinAttributes()
{
if (doc["Skin"]["Attributes"] == null) return;

XmlNodeList l = doc["Skin"]["Attributes"].GetElementsByTagName("Attribute");

if (l != null && l.Count > 0)
{
foreach (XmlElement e in l)
{
SkinAttribute sa = new SkinAttribute();
sa.Name = ReadAttribute(e, "Name", null, true);
sa.Value = ReadAttribute(e, "Value", null, true);
attributes.Add(sa);
}
}
}

/// </summary>
///
/// <summary>
private void LoadControls()
{
if (doc["Skin"]["Controls"] == null) return;


XmlNodeList l = doc["Skin"]["Controls"].GetElementsByTagName("Control");

if (l != null && l.Count > 0)
{
foreach (XmlElement e in l)
{
SkinControl sc = null;
string parent = ReadAttribute(e, "Inherits", null, false);
bool inh = false;

if (parent != null)
{
sc = new SkinControl(controls[parent]);
sc.Inherits = parent;
inh = true;
}
else
{
sc = new SkinControl();
}

ReadAttribute(ref sc.Name, inh, e, "Name", null, true);

ReadAttributeInt(ref sc.DefaultSize.Width, inh, e["DefaultSize"], "Width", 0, false);
ReadAttributeInt(ref sc.DefaultSize.Height, inh, e["DefaultSize"], "Height", 0, false);

ReadAttributeInt(ref sc.MinimumSize.Width, inh, e["MinimumSize"], "Width", 0, false);
ReadAttributeInt(ref sc.MinimumSize.Height, inh, e["MinimumSize"], "Height", 0, false);

ReadAttributeInt(ref sc.OriginMargins.Left, inh, e["OriginMargins"], "Left", 0, false);
ReadAttributeInt(ref sc.OriginMargins.Top, inh, e["OriginMargins"], "Top", 0, false);
ReadAttributeInt(ref sc.OriginMargins.Right, inh, e["OriginMargins"], "Right", 0, false);
ReadAttributeInt(ref sc.OriginMargins.Bottom, inh, e["OriginMargins"], "Bottom", 0, false);

ReadAttributeInt(ref sc.ClientMargins.Left, inh, e["ClientMargins"], "Left", 0, false);
ReadAttributeInt(ref sc.ClientMargins.Top, inh, e["ClientMargins"], "Top", 0, false);
ReadAttributeInt(ref sc.ClientMargins.Right, inh, e["ClientMargins"], "Right", 0, false);
ReadAttributeInt(ref sc.ClientMargins.Bottom, inh, e["ClientMargins"], "Bottom", 0, false);

ReadAttributeInt(ref sc.ResizerSize, inh, e["ResizerSize"], "Value", 0, false);

if (e["Layers"] != null)
{
XmlNodeList l2 = e["Layers"].GetElementsByTagName("Layer");
if (l2 != null && l2.Count > 0)
{
LoadLayers(sc, l2);
}
}
if (e["Attributes"] != null)
{
XmlNodeList l3 = e["Attributes"].GetElementsByTagName("Attribute");
if (l3 != null && l3.Count > 0)
{
LoadControlAttributes(sc, l3);
}
}
controls.Add(sc);
}
}
}

/// <param name="archive"></param>
/// <param name="addon"></param>
/// </summary>
///
/// <summary>
private void LoadFonts(string addon, bool archive)
{
if (doc["Skin"]["Fonts"] == null) return;

XmlNodeList l = doc["Skin"]["Fonts"].GetElementsByTagName("Font");
if (l != null && l.Count > 0)
{
foreach (XmlElement e in l)
{
SkinFont sf = new SkinFont();
sf.Name = ReadAttribute(e, "Name", null, true);
sf.Archive = archive;
sf.Asset = ReadAttribute(e, "Asset", null, true);
sf.Addon = addon;
fonts.Add(sf);
}
}
}

/// <param name="archive"></param>
/// <param name="addon"></param>
/// </summary>
///
/// <summary>
private void LoadCursors(string addon, bool archive)
{
if (doc["Skin"]["Cursors"] == null) return;

XmlNodeList l = doc["Skin"]["Cursors"].GetElementsByTagName("Cursor");
if (l != null && l.Count > 0)
{
foreach (XmlElement e in l)
{
SkinCursor sc = new SkinCursor();
sc.Name = ReadAttribute(e, "Name", null, true);
sc.Archive = archive;
sc.Asset = ReadAttribute(e, "Asset", null, true);
sc.Addon = addon;
cursors.Add(sc);
}
}
}

/// <param name="archive"></param>
/// <param name="addon"></param>
/// </summary>
///
/// <summary>
private void LoadImages(string addon, bool archive)
{
if (doc["Skin"]["Images"] == null) return;
XmlNodeList l = doc["Skin"]["Images"].GetElementsByTagName("Image");
if (l != null && l.Count > 0)
{
foreach (XmlElement e in l)
{
SkinImage si = new SkinImage();
si.Name = ReadAttribute(e, "Name", null, true);
si.Archive = archive;
si.Asset = ReadAttribute(e, "Asset", null, true);
si.Addon = addon;
images.Add(si);
}
}
}

/// <param name="l"></param>
/// <param name="sc"></param>
/// </summary>
///
/// <summary>
private void LoadLayers(SkinControl sc, XmlNodeList l)
{
foreach (XmlElement e in l)
{
string name = ReadAttribute(e, "Name", null, true);
bool over = ReadAttributeBool(e, "Override", false, false);
SkinLayer sl = sc.Layers[name];
bool inh = true;

if (sl == null)
{
sl = new SkinLayer();
inh = false;
}

if (inh && over)
{
sl = new SkinLayer();
sc.Layers[name] = sl;
}

ReadAttribute(ref sl.Name, inh, e, "Name", null, true);
ReadAttribute(ref sl.Image.Name, inh, e, "Image", "Control", false);
ReadAttributeInt(ref sl.Width, inh, e, "Width", 0, false);
ReadAttributeInt(ref sl.Height, inh, e, "Height", 0, false);

string tmp = sl.Alignment.ToString();
ReadAttribute(ref tmp, inh, e, "Alignment", "MiddleCenter", false);
sl.Alignment = (Alignment)Enum.Parse(typeof(Alignment), tmp, true);

ReadAttributeInt(ref sl.OffsetX, inh, e, "OffsetX", 0, false);
ReadAttributeInt(ref sl.OffsetY, inh, e, "OffsetY", 0, false);

ReadAttributeInt(ref sl.SizingMargins.Left, inh, e["SizingMargins"], "Left", 0, false);
ReadAttributeInt(ref sl.SizingMargins.Top, inh, e["SizingMargins"], "Top", 0, false);
ReadAttributeInt(ref sl.SizingMargins.Right, inh, e["SizingMargins"], "Right", 0, false);
ReadAttributeInt(ref sl.SizingMargins.Bottom, inh, e["SizingMargins"], "Bottom", 0, false);

ReadAttributeInt(ref sl.ContentMargins.Left, inh, e["ContentMargins"], "Left", 0, false);
ReadAttributeInt(ref sl.ContentMargins.Top, inh, e["ContentMargins"], "Top", 0, false);
ReadAttributeInt(ref sl.ContentMargins.Right, inh, e["ContentMargins"], "Right", 0, false);
ReadAttributeInt(ref sl.ContentMargins.Bottom, inh, e["ContentMargins"], "Bottom", 0, false);

if (e["States"] != null)
{
ReadAttributeInt(ref sl.States.Enabled.Index, inh, e["States"]["Enabled"], "Index", 0, false);
int di = sl.States.Enabled.Index;
ReadAttributeInt(ref sl.States.Hovered.Index, inh, e["States"]["Hovered"], "Index", di, false);
ReadAttributeInt(ref sl.States.Pressed.Index, inh, e["States"]["Pressed"], "Index", di, false);
ReadAttributeInt(ref sl.States.Focused.Index, inh, e["States"]["Focused"], "Index", di, false);
ReadAttributeInt(ref sl.States.Disabled.Index, inh, e["States"]["Disabled"], "Index", di, false);

ReadAttributeColor(ref sl.States.Enabled.Color, inh, e["States"]["Enabled"], "Color", Color.White, false);
Color dc = sl.States.Enabled.Color;
ReadAttributeColor(ref sl.States.Hovered.Color, inh, e["States"]["Hovered"], "Color", dc, false);
ReadAttributeColor(ref sl.States.Pressed.Color, inh, e["States"]["Pressed"], "Color", dc, false);
ReadAttributeColor(ref sl.States.Focused.Color, inh, e["States"]["Focused"], "Color", dc, false);
ReadAttributeColor(ref sl.States.Disabled.Color, inh, e["States"]["Disabled"], "Color", dc, false);

ReadAttributeBool(ref sl.States.Enabled.Overlay, inh, e["States"]["Enabled"], "Overlay", false, false);
bool dv = sl.States.Enabled.Overlay;
ReadAttributeBool(ref sl.States.Hovered.Overlay, inh, e["States"]["Hovered"], "Overlay", dv, false);
ReadAttributeBool(ref sl.States.Pressed.Overlay, inh, e["States"]["Pressed"], "Overlay", dv, false);
ReadAttributeBool(ref sl.States.Focused.Overlay, inh, e["States"]["Focused"], "Overlay", dv, false);
ReadAttributeBool(ref sl.States.Disabled.Overlay, inh, e["States"]["Disabled"], "Overlay", dv, false);
}

if (e["Overlays"] != null)
{
ReadAttributeInt(ref sl.Overlays.Enabled.Index, inh, e["Overlays"]["Enabled"], "Index", 0, false);
int di = sl.Overlays.Enabled.Index;
ReadAttributeInt(ref sl.Overlays.Hovered.Index, inh, e["Overlays"]["Hovered"], "Index", di, false);
ReadAttributeInt(ref sl.Overlays.Pressed.Index, inh, e["Overlays"]["Pressed"], "Index", di, false);
ReadAttributeInt(ref sl.Overlays.Focused.Index, inh, e["Overlays"]["Focused"], "Index", di, false);
ReadAttributeInt(ref sl.Overlays.Disabled.Index, inh, e["Overlays"]["Disabled"], "Index", di, false);

ReadAttributeColor(ref sl.Overlays.Enabled.Color, inh, e["Overlays"]["Enabled"], "Color", Color.White, false);
Color dc = sl.Overlays.Enabled.Color;
ReadAttributeColor(ref sl.Overlays.Hovered.Color, inh, e["Overlays"]["Hovered"], "Color", dc, false);
ReadAttributeColor(ref sl.Overlays.Pressed.Color, inh, e["Overlays"]["Pressed"], "Color", dc, false);
ReadAttributeColor(ref sl.Overlays.Focused.Color, inh, e["Overlays"]["Focused"], "Color", dc, false);
ReadAttributeColor(ref sl.Overlays.Disabled.Color, inh, e["Overlays"]["Disabled"], "Color", dc, false);
}

if (e["Text"] != null)
{
ReadAttribute(ref sl.Text.Name, inh, e["Text"], "Font", null, true);
ReadAttributeInt(ref sl.Text.OffsetX, inh, e["Text"], "OffsetX", 0, false);
ReadAttributeInt(ref sl.Text.OffsetY, inh, e["Text"], "OffsetY", 0, false);

tmp = sl.Text.Alignment.ToString();
ReadAttribute(ref tmp, inh, e["Text"], "Alignment", "MiddleCenter", false);
sl.Text.Alignment = (Alignment)Enum.Parse(typeof(Alignment), tmp, true);

LoadColors(inh, e["Text"], ref sl.Text.Colors);
}
if (e["Attributes"] != null)
{
XmlNodeList l2 = e["Attributes"].GetElementsByTagName("Attribute");
if (l2 != null && l2.Count > 0)
{
LoadLayerAttributes(sl, l2);
}
}
if (!inh) sc.Layers.Add(sl);
}
}

/// <param name="colors"></param>
/// <param name="e"></param>
/// <param name="inherited"></param>
/// </summary>
///
/// <summary>
private void LoadColors(bool inherited, XmlElement e, ref SkinStates<Color> colors)
{
if (e != null)
{
ReadAttributeColor(ref colors.Enabled, inherited, e["Colors"]["Enabled"], "Color", Color.White, false);
ReadAttributeColor(ref colors.Hovered, inherited, e["Colors"]["Hovered"], "Color", colors.Enabled, false);
ReadAttributeColor(ref colors.Pressed, inherited, e["Colors"]["Pressed"], "Color", colors.Enabled, false);
ReadAttributeColor(ref colors.Focused, inherited, e["Colors"]["Focused"], "Color", colors.Enabled, false);
ReadAttributeColor(ref colors.Disabled, inherited, e["Colors"]["Disabled"], "Color", colors.Enabled, false);
}
}

/// <param name="l"></param>
/// <param name="sc"></param>
/// </summary>
///
/// <summary>
private void LoadControlAttributes(SkinControl sc, XmlNodeList l)
{
foreach (XmlElement e in l)
{
string name = ReadAttribute(e, "Name", null, true);
SkinAttribute sa = sc.Attributes[name];
bool inh = true;

if (sa == null)
{
sa = new SkinAttribute();
inh = false;
}

sa.Name = name;
ReadAttribute(ref sa.Value, inh, e, "Value", null, true);

if (!inh) sc.Attributes.Add(sa);
}
}

/// <param name="l"></param>
/// <param name="sl"></param>
/// </summary>
///
/// <summary>
private void LoadLayerAttributes(SkinLayer sl, XmlNodeList l)
{
foreach (XmlElement e in l)
{
string name = ReadAttribute(e, "Name", null, true);
SkinAttribute sa = sl.Attributes[name];
bool inh = true;

if (sa == null)
{
sa = new SkinAttribute();
inh = false;
}

sa.Name = name;
ReadAttribute(ref sa.Value, inh, e, "Value", null, true);

if (!inh) sl.Attributes.Add(sa);
}
}

}


}

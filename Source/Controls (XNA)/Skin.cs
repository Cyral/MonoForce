using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#if (!XBOX && !XBOX_FAKE)
using System.Windows.Forms;

#endif

namespace MonoForce.Controls
{
    /// <typeparam name="T">The type of data to store in the struct.</typeparam>
    /// </summary>
    /// Stores data about each skin state.
    /// <summary>
    public struct SkinStates<T>
    {
        /// </summary>
        /// Data for the skin's Disabled state.
        /// <summary>
        public T Disabled;

        /// </summary>
        /// Data for the skin's Enabled state.
        /// <summary>
        public T Enabled;

        /// </summary>
        /// Data for the skin's Focused state.
        /// <summary>
        public T Focused;

        /// </summary>
        /// Data for the skin's Hovered state.
        /// <summary>
        public T Hovered;

        /// </summary>
        /// Data for the skin's Pressed state.
        /// <summary>
        public T Pressed;

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
        /// Color tint to apply to the layer state's image asset.
        /// <summary>
        public Color Color;

        /// </summary>
        /// Index of the layer state's image asset.
        /// <summary>
        public int Index;

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
        /// Color tint to apply to the layer state's image asset.
        /// <summary>
        public Color Color;

        /// </summary>
        /// Index of the layer state's image asset.
        /// <summary>
        public int Index;
    }

    /// </summary>
    /// Stores skin metadata.
    /// <summary>
    public struct SkinInfo
    {
        /// </summary>
        /// Person who made the skin.
        /// <summary>
        public string Author;

        /// </summary>
        /// Description of the skin.
        /// <summary>
        public string Description;

        /// </summary>
        /// Name of the skin.
        /// <summary>
        public string Name;

        /// </summary>
        /// Version of the skin. (Should be 0.7 for this version of Neoforce.)
        /// <summary>
        public string Version;
    }


    public class SkinList<T> : List<T>
    {
        /// default skin if the specified name is not found.
        /// </returns>
        /// <returns>
        /// Returns the skin with the specified name or a
        /// <param name="index">Name of the skin to access.</param>
        /// </summary>
        /// Accesses a skin in the list by name.
        /// <summary>
        public T this[string index]
        {
            get
            {
                for (var i = 0; i < Count; i++)
                {
                    var s = (SkinBase)(object)this[i];
                    if (s.Name.ToLower() == index.ToLower())
                    {
                        return this[i];
                    }
                }
                return default(T);
            }

            set
            {
                for (var i = 0; i < Count; i++)
                {
                    var s = (SkinBase)(object)this[i];
                    if (s.Name.ToLower() == index.ToLower())
                    {
                        this[i] = value;
                    }
                }
            }
        }

        public SkinList()
        {
        }

        public SkinList(SkinList<T> source)
        {
            for (var i = 0; i < source.Count; i++)
            {
                var t = new Type[1];
                t[0] = typeof (T);

                var p = new object[1];
                p[0] = source[i];

                Add((T)t[0].GetConstructor(t).Invoke(p));
            }
        }
    }

    /// </summary>
    /// Base class for all things skin related.
    /// <summary>
    public class SkinBase
    {
        /// </summary>
        /// Indicates if the object is located in a skin file archive. ???
        /// <summary>
        public bool Archive;

        /// </summary>
        /// Name of the skin.
        /// <summary>
        public string Name;

        public SkinBase()
        {
            Archive = false;
        }

        public SkinBase(SkinBase source)
        {
            if (source != null)
            {
                Name = source.Name;
                Archive = source.Archive;
            }
        }
    }

    public class SkinLayer : SkinBase
    {
        /// </summary>
        /// <summary>
        public Alignment Alignment;

        /// </summary>
        /// <summary>
        public SkinList<SkinAttribute> Attributes = new SkinList<SkinAttribute>();

        /// </summary>
        /// <summary>
        public Margins ContentMargins;

        /// </summary>
        /// Height of the skin layer.
        /// <summary>
        public int Height;

        /// </summary>
        /// Image resource for the skin layer.
        /// <summary>
        public SkinImage Image = new SkinImage();

        /// </summary>
        /// <summary>
        public int OffsetX;

        /// </summary>
        /// <summary>
        public int OffsetY;

        /// </summary>
        /// <summary>
        public SkinStates<LayerOverlays> Overlays;

        /// </summary>
        /// <summary>
        public Margins SizingMargins;

        /// </summary>
        /// <summary>
        public SkinStates<LayerStates> States;

        /// </summary>
        /// <summary>
        public SkinText Text = new SkinText();

        /// </summary>
        /// Width of the skin layer.
        /// <summary>
        public int Width;

        public SkinLayer()
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

        public SkinLayer(SkinLayer source) : base(source)
        {
            if (source != null)
            {
                Image = new SkinImage(source.Image);
                Width = source.Width;
                Height = source.Height;
                OffsetX = source.OffsetX;
                OffsetY = source.OffsetY;
                Alignment = source.Alignment;
                SizingMargins = source.SizingMargins;
                ContentMargins = source.ContentMargins;
                States = source.States;
                Overlays = source.Overlays;
                Text = new SkinText(source.Text);
                Attributes = new SkinList<SkinAttribute>(source.Attributes);
            }
            else
            {
                throw new Exception("Parameter for SkinLayer copy constructor cannot be null.");
            }
        }
    }

    public class SkinText : SkinBase
    {
        /// </summary>
        /// <summary>
        public Alignment Alignment;

        /// </summary>
        /// Text color for each skin state.
        /// <summary>
        public SkinStates<Color> Colors;

        /// </summary>
        /// Font object used for drawing the text.
        /// <summary>
        public SkinFont Font;

        /// </summary>
        /// <summary>
        public int OffsetX;

        /// </summary>
        /// <summary>
        public int OffsetY;

        public SkinText()
        {
            Colors.Enabled = Color.White;
            Colors.Pressed = Color.White;
            Colors.Focused = Color.White;
            Colors.Hovered = Color.White;
            Colors.Disabled = Color.White;
        }

        public SkinText(SkinText source) : base(source)
        {
            if (source != null)
            {
                Font = new SkinFont(source.Font);
                OffsetX = source.OffsetX;
                OffsetY = source.OffsetY;
                Alignment = source.Alignment;
                Colors = source.Colors;
            }
        }
    }

    public class SkinFont : SkinBase
    {
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
        /// <summary>
        public string Addon;

        /// </summary>
        /// The sprite font asset file.
        /// <summary>
        public string Asset;

        /// </summary>
        /// Sprite font used for drawing text.
        /// <summary>
        public SpriteFont Resource;

        public SkinFont()
        {
        }

        public SkinFont(SkinFont source) : base(source)
        {
            if (source != null)
            {
                Resource = source.Resource;
                Asset = source.Asset;
            }
        }
    }

    public class SkinImage : SkinBase
    {
        /// </summary>
        /// <summary>
        public string Addon;

        /// </summary>
        /// The sprite font asset file.
        /// <summary>
        public string Asset;

        /// </summary>
        /// Image resource.
        /// <summary>
        public Texture2D Resource;

        public SkinImage()
        {
        }

        public SkinImage(SkinImage source) : base(source)
        {
            Resource = source.Resource;
            Asset = source.Asset;
        }
    }

    public class SkinCursor : SkinBase
    {
        /// </summary>
        /// <summary>
        public string Addon;

        /// </summary>
        /// The sprite font asset file.
        /// <summary>
        public string Asset;

#if (!XBOX && !XBOX_FAKE)
        /// </summary>
        /// Cursor image for Windows.
        /// <summary>
        public Cursor Resource;
#endif

        public SkinCursor()
        {
        }

        public SkinCursor(SkinCursor source) : base(source)
        {
#if (!XBOX && !XBOX_FAKE)
            Resource = source.Resource;
#endif

            Asset = source.Asset;
        }
    }

    public class SkinControl : SkinBase
    {
        /// </summary>
        /// List of skin control attributes.
        /// <summary>
        public SkinList<SkinAttribute> Attributes = new SkinList<SkinAttribute>();

        /// </summary>
        /// Inner margin values for the control
        /// <summary>
        public Margins ClientMargins;

        /// </summary>
        /// Default size of the control.
        /// <summary>
        public Size DefaultSize;

        /// </summary>
        /// Indicates which, if any, base skin control settings are inherited by this skin control.
        /// <summary>
        public string Inherits;

        /// </summary>
        /// List of skin control layers.
        /// <summary>
        public SkinList<SkinLayer> Layers = new SkinList<SkinLayer>();

        /// </summary>
        /// Minimum size settings for this control.
        /// <summary>
        public Size MinimumSize;

        /// </summary>
        /// Outer margin values for the control.
        /// <summary>
        public Margins OriginMargins;

        /// </summary>
        /// Default size of the resize border around the edge of the control.
        /// <summary>
        public int ResizerSize;

        public SkinControl()
        {
        }

        public SkinControl(SkinControl source) : base(source)
        {
            Inherits = source.Inherits;
            DefaultSize = source.DefaultSize;
            MinimumSize = source.MinimumSize;
            OriginMargins = source.OriginMargins;
            ClientMargins = source.ClientMargins;
            ResizerSize = source.ResizerSize;
            Layers = new SkinList<SkinLayer>(source.Layers);
            Attributes = new SkinList<SkinAttribute>(source.Attributes);
        }
    }

    public class SkinAttribute : SkinBase
    {
        /// </summary>
        /// Value of the skin attribute.
        /// <summary>
        public string Value;

        public SkinAttribute()
        {
        }

        public SkinAttribute(SkinAttribute source) : base(source)
        {
            Value = source.Value;
        }
    }

    public class Skin : Component
    {
        /// </summary>
        /// Gets the list of attributes belonging to this skin.
        /// <summary>
        public virtual SkinList<SkinAttribute> Attributes
        {
            get { return attributes; }
        }

        /// </summary>
        /// Gets the list of controls supported by this skin.
        /// <summary>
        public virtual SkinList<SkinControl> Controls
        {
            get { return controls; }
        }

        /// </summary>
        /// Gets the list of cursors this skin uses.
        /// <summary>
        public virtual SkinList<SkinCursor> Cursors
        {
            get { return cursors; }
        }

        /// </summary>
        /// Gets the list of fonts this skin uses.
        /// <summary>
        public virtual SkinList<SkinFont> Fonts
        {
            get { return fonts; }
        }

        /// </summary>
        /// Gets the list of images belonging to this skin.
        /// <summary>
        public virtual SkinList<SkinImage> Images
        {
            get { return images; }
        }

        /// </summary>
        /// Gets the skin's metadata info.
        /// <summary>
        public virtual SkinInfo Info
        {
            get { return info; }
        }

        /// </summary>
        /// Gets the name of the skin.
        /// <summary>
        public virtual string Name
        {
            get { return name; }
        }

        /// </summary>
        /// Gets the skin file version.
        /// <summary>
        public virtual Version Version
        {
            get { return version; }
        }

        /// </summary>
        /// List of attributes the skin uses.
        /// <summary>
        private readonly SkinList<SkinAttribute> attributes;

        /// </summary>
        /// List of controls the skin supports.
        /// <summary>
        private readonly SkinList<SkinControl> controls;

        /// </summary>
        /// List of cursors the skin uses.
        /// <summary>
        private readonly SkinList<SkinCursor> cursors;

        /// </summary>
        /// List of fonts the skin uses.
        /// <summary>
        private readonly SkinList<SkinFont> fonts;

        /// </summary>
        /// List of images the skin uses.
        /// <summary>
        private readonly SkinList<SkinImage> images;

        /// </summary>
        /// Content manager for loading skin files.
        /// <summary>
        private ArchiveManager content;

        /// </summary>
        /// Skin XML document where the skin info is defined.
        /// <summary>
        private SkinXmlDocument doc;

        /// </summary>
        /// Skin metadata information.
        /// <summary>
        private SkinInfo info;

        /// </summary>
        /// Name of the skin.
        /// <summary>
        private string name;

        /// </summary>
        /// Skin file version.
        /// <summary>
        private Version version;

        public Skin(Manager manager, string name) : base(manager)
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

            var folder = GetAddonsFolder();
            if (folder == "")
            {
                content.UseArchive = true;
                folder = "Addons\\";
            }
            else
            {
                content.UseArchive = false;
            }

            var addons = content.GetDirectories(folder);

            if (addons != null && addons.Length > 0)
            {
                for (var i = 0; i < addons.Length; i++)
                {
                    var d = new DirectoryInfo(GetAddonsFolder() + addons[i]);
                    if (!((d.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) || content.UseArchive)
                    {
                        LoadSkin(addons[i].Replace("\\", ""), content.UseArchive);
                    }
                }
            }
        }

        /// </summary>
        /// <summary>
        public override void Init()
        {
            base.Init();

            for (var i = 0; i < fonts.Count; i++)
            {
                content.UseArchive = fonts[i].Archive;
                var asset = GetAsset("Fonts", fonts[i].Asset, fonts[i].Addon);
                asset = content.UseArchive ? asset : Path.GetFullPath(asset);
                (fonts[i].Resource) = content.Load<SpriteFont>(asset);
            }

#if (!XBOX && !XBOX_FAKE)
            for (var i = 0; i < cursors.Count; i++)
            {
                content.UseArchive = cursors[i].Archive;
                var asset = GetAsset("Cursors", cursors[i].Asset, cursors[i].Addon);
                asset = content.UseArchive ? asset : Path.GetFullPath(asset);
                cursors[i].Resource = content.Load<Cursor>(asset);
            }
#endif

            for (var i = 0; i < images.Count; i++)
            {
                content.UseArchive = images[i].Archive;
                var asset = GetAsset("Images", images[i].Asset, images[i].Addon);
                asset = content.UseArchive ? asset : Path.GetFullPath(asset);
                images[i].Resource = content.Load<Texture2D>(asset);
            }

            for (var i = 0; i < controls.Count; i++)
            {
                for (var j = 0; j < controls[i].Layers.Count; j++)
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

        /// <returns></returns>
        /// <param name="c"></param>
        /// </summary>
        /// <summary>
        private string ColorToString(Color c)
        {
            return string.Format("{0};{1};{2};{3}", c.R, c.G, c.B, c.A);
        }

        /// <returns>Returns the full path to the skin's addon folder.</returns>
        /// </summary>
        /// Gets the full path to the skin's addon folder.
        /// <summary>
        private string GetAddonsFolder()
        {
            var path = Path.GetFullPath(Manager.SkinDirectory) + name + "\\Addons\\";
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

        /// <returns>Returns true if the file exists in the skin directory; false otherwise.</returns>
        /// <param name="name">Name of the skin file archive.</param>
        /// </summary>
        /// Returns the path to the specified skin file.
        /// <summary>
        private string GetArchiveLocation(string name)
        {
            var path = Path.GetFullPath(Manager.SkinDirectory) + Path.GetFileNameWithoutExtension(name) + "\\";
            if (!Directory.Exists(path) || !File.Exists(path + "Skin.xnb"))
            {
                path = Path.GetFullPath(Manager.SkinDirectory) + name;
                return path;
            }

            return null;
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
            var ret = GetFolder(type) + asset;
// Update path if asset is part of an addon.
            if (addon != null && addon != "")
            {
                ret = GetAddonsFolder() + addon + "\\" + type + "\\" + asset;
            }
            return ret;
        }

        /// <returns>Returns the full path to the skin directory.</returns>
        /// </summary>
        /// Gets the full path to the skin folder.
        /// <summary>
        private string GetFolder()
        {
            var path = Path.GetFullPath(Manager.SkinDirectory) + name + "\\";
            if (!Directory.Exists(path) || !File.Exists(path + "Skin.xnb"))
            {
                path = "";
            }

            return path;
        }

        /// <returns></returns>
        /// <param name="type">Type of skin asset to get. ("Fonts", "Cursors", or "Images")</param>
        /// </summary>
        /// <summary>
        private string GetFolder(string type)
        {
            return GetFolder() + type + "\\";
        }

        /// <param name="colors"></param>
        /// <param name="e"></param>
        /// <param name="inherited"></param>
        /// </summary>
        /// <summary>
        private void LoadColors(bool inherited, XmlElement e, ref SkinStates<Color> colors)
        {
            if (e != null)
            {
                ReadAttributeColor(ref colors.Enabled, inherited, e["Colors"]["Enabled"], "Color", Color.White, false);
                ReadAttributeColor(ref colors.Hovered, inherited, e["Colors"]["Hovered"], "Color", colors.Enabled, false);
                ReadAttributeColor(ref colors.Pressed, inherited, e["Colors"]["Pressed"], "Color", colors.Enabled, false);
                ReadAttributeColor(ref colors.Focused, inherited, e["Colors"]["Focused"], "Color", colors.Enabled, false);
                ReadAttributeColor(ref colors.Disabled, inherited, e["Colors"]["Disabled"], "Color", colors.Enabled,
                    false);
            }
        }

        /// <param name="l"></param>
        /// <param name="sc"></param>
        /// </summary>
        /// <summary>
        private void LoadControlAttributes(SkinControl sc, XmlNodeList l)
        {
            foreach (XmlElement e in l)
            {
                var name = ReadAttribute(e, "Name", null, true);
                var sa = sc.Attributes[name];
                var inh = true;

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

        /// </summary>
        /// <summary>
        private void LoadControls()
        {
            if (doc["Skin"]["Controls"] == null) return;


            var l = doc["Skin"]["Controls"].GetElementsByTagName("Control");

            if (l != null && l.Count > 0)
            {
                foreach (XmlElement e in l)
                {
                    SkinControl sc = null;
                    var parent = ReadAttribute(e, "Inherits", null, false);
                    var inh = false;

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
                        var l2 = e["Layers"].GetElementsByTagName("Layer");
                        if (l2 != null && l2.Count > 0)
                        {
                            LoadLayers(sc, l2);
                        }
                    }
                    if (e["Attributes"] != null)
                    {
                        var l3 = e["Attributes"].GetElementsByTagName("Attribute");
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
        /// <summary>
        private void LoadCursors(string addon, bool archive)
        {
            if (doc["Skin"]["Cursors"] == null) return;

            var l = doc["Skin"]["Cursors"].GetElementsByTagName("Cursor");
            if (l != null && l.Count > 0)
            {
                foreach (XmlElement e in l)
                {
                    var sc = new SkinCursor();
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
        /// <summary>
        private void LoadFonts(string addon, bool archive)
        {
            if (doc["Skin"]["Fonts"] == null) return;

            var l = doc["Skin"]["Fonts"].GetElementsByTagName("Font");
            if (l != null && l.Count > 0)
            {
                foreach (XmlElement e in l)
                {
                    var sf = new SkinFont();
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
        /// <summary>
        private void LoadImages(string addon, bool archive)
        {
            if (doc["Skin"]["Images"] == null) return;
            var l = doc["Skin"]["Images"].GetElementsByTagName("Image");
            if (l != null && l.Count > 0)
            {
                foreach (XmlElement e in l)
                {
                    var si = new SkinImage();
                    si.Name = ReadAttribute(e, "Name", null, true);
                    si.Archive = archive;
                    si.Asset = ReadAttribute(e, "Asset", null, true);
                    si.Addon = addon;
                    images.Add(si);
                }
            }
        }

        /// <param name="l"></param>
        /// <param name="sl"></param>
        /// </summary>
        /// <summary>
        private void LoadLayerAttributes(SkinLayer sl, XmlNodeList l)
        {
            foreach (XmlElement e in l)
            {
                var name = ReadAttribute(e, "Name", null, true);
                var sa = sl.Attributes[name];
                var inh = true;

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

        /// <param name="l"></param>
        /// <param name="sc"></param>
        /// </summary>
        /// <summary>
        private void LoadLayers(SkinControl sc, XmlNodeList l)
        {
            foreach (XmlElement e in l)
            {
                var name = ReadAttribute(e, "Name", null, true);
                var over = ReadAttributeBool(e, "Override", false, false);
                var sl = sc.Layers[name];
                var inh = true;

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

                var tmp = sl.Alignment.ToString();
                ReadAttribute(ref tmp, inh, e, "Alignment", "MiddleCenter", false);
                sl.Alignment = (Alignment)Enum.Parse(typeof (Alignment), tmp, true);

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
                    var di = sl.States.Enabled.Index;
                    ReadAttributeInt(ref sl.States.Hovered.Index, inh, e["States"]["Hovered"], "Index", di, false);
                    ReadAttributeInt(ref sl.States.Pressed.Index, inh, e["States"]["Pressed"], "Index", di, false);
                    ReadAttributeInt(ref sl.States.Focused.Index, inh, e["States"]["Focused"], "Index", di, false);
                    ReadAttributeInt(ref sl.States.Disabled.Index, inh, e["States"]["Disabled"], "Index", di, false);

                    ReadAttributeColor(ref sl.States.Enabled.Color, inh, e["States"]["Enabled"], "Color", Color.White,
                        false);
                    var dc = sl.States.Enabled.Color;
                    ReadAttributeColor(ref sl.States.Hovered.Color, inh, e["States"]["Hovered"], "Color", dc, false);
                    ReadAttributeColor(ref sl.States.Pressed.Color, inh, e["States"]["Pressed"], "Color", dc, false);
                    ReadAttributeColor(ref sl.States.Focused.Color, inh, e["States"]["Focused"], "Color", dc, false);
                    ReadAttributeColor(ref sl.States.Disabled.Color, inh, e["States"]["Disabled"], "Color", dc, false);

                    ReadAttributeBool(ref sl.States.Enabled.Overlay, inh, e["States"]["Enabled"], "Overlay", false,
                        false);
                    var dv = sl.States.Enabled.Overlay;
                    ReadAttributeBool(ref sl.States.Hovered.Overlay, inh, e["States"]["Hovered"], "Overlay", dv, false);
                    ReadAttributeBool(ref sl.States.Pressed.Overlay, inh, e["States"]["Pressed"], "Overlay", dv, false);
                    ReadAttributeBool(ref sl.States.Focused.Overlay, inh, e["States"]["Focused"], "Overlay", dv, false);
                    ReadAttributeBool(ref sl.States.Disabled.Overlay, inh, e["States"]["Disabled"], "Overlay", dv, false);
                }

                if (e["Overlays"] != null)
                {
                    ReadAttributeInt(ref sl.Overlays.Enabled.Index, inh, e["Overlays"]["Enabled"], "Index", 0, false);
                    var di = sl.Overlays.Enabled.Index;
                    ReadAttributeInt(ref sl.Overlays.Hovered.Index, inh, e["Overlays"]["Hovered"], "Index", di, false);
                    ReadAttributeInt(ref sl.Overlays.Pressed.Index, inh, e["Overlays"]["Pressed"], "Index", di, false);
                    ReadAttributeInt(ref sl.Overlays.Focused.Index, inh, e["Overlays"]["Focused"], "Index", di, false);
                    ReadAttributeInt(ref sl.Overlays.Disabled.Index, inh, e["Overlays"]["Disabled"], "Index", di, false);

                    ReadAttributeColor(ref sl.Overlays.Enabled.Color, inh, e["Overlays"]["Enabled"], "Color",
                        Color.White, false);
                    var dc = sl.Overlays.Enabled.Color;
                    ReadAttributeColor(ref sl.Overlays.Hovered.Color, inh, e["Overlays"]["Hovered"], "Color", dc, false);
                    ReadAttributeColor(ref sl.Overlays.Pressed.Color, inh, e["Overlays"]["Pressed"], "Color", dc, false);
                    ReadAttributeColor(ref sl.Overlays.Focused.Color, inh, e["Overlays"]["Focused"], "Color", dc, false);
                    ReadAttributeColor(ref sl.Overlays.Disabled.Color, inh, e["Overlays"]["Disabled"], "Color", dc,
                        false);
                }

                if (e["Text"] != null)
                {
                    ReadAttribute(ref sl.Text.Name, inh, e["Text"], "Font", null, true);
                    ReadAttributeInt(ref sl.Text.OffsetX, inh, e["Text"], "OffsetX", 0, false);
                    ReadAttributeInt(ref sl.Text.OffsetY, inh, e["Text"], "OffsetY", 0, false);

                    tmp = sl.Text.Alignment.ToString();
                    ReadAttribute(ref tmp, inh, e["Text"], "Alignment", "MiddleCenter", false);
                    sl.Text.Alignment = (Alignment)Enum.Parse(typeof (Alignment), tmp, true);

                    LoadColors(inh, e["Text"], ref sl.Text.Colors);
                }
                if (e["Attributes"] != null)
                {
                    var l2 = e["Attributes"].GetElementsByTagName("Attribute");
                    if (l2 != null && l2.Count > 0)
                    {
                        LoadLayerAttributes(sl, l2);
                    }
                }
                if (!inh) sc.Layers.Add(sl);
            }
        }

        /// <param name="archive"></param>
        /// <param name="addon"></param>
        /// </summary>
        /// <summary>
        private void LoadSkin(string addon, bool archive)
        {
            try
            {
                var isaddon = addon != null && addon != "";
                var file = GetFolder();
                if (isaddon)
                {
                    file = GetAddonsFolder() + addon + "\\";
                }
                file += "Skin";

                file = archive ? file : Path.GetFullPath(file);
                doc = content.Load<SkinXmlDocument>(file);

// Read root element: Skin
                var e = doc["Skin"];
                if (e != null)
                {
// Read required attribute: Skin.Name
                    var xname = ReadAttribute(e, "Name", null, true);
                    if (!isaddon)
                    {
                        if (name.ToLower() != xname.ToLower())
                        {
                            throw new Exception("Skin name defined in the skin file doesn't match requested skin.");
                        }
                        name = xname;
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
                        throw new Exception(
                            "This version of Neoforce Controls can only read skin files in version of " +
                            Manager._SkinVersion + ".");
                    }
                    if (!isaddon)
                    {
                        version = xversion;
                    }

                    if (!isaddon)
                    {
                        var ei = e["Info"];
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
        /// <summary>
        private void LoadSkinAttributes()
        {
            if (doc["Skin"]["Attributes"] == null) return;

            var l = doc["Skin"]["Attributes"].GetElementsByTagName("Attribute");

            if (l != null && l.Count > 0)
            {
                foreach (XmlElement e in l)
                {
                    var sa = new SkinAttribute();
                    sa.Name = ReadAttribute(e, "Name", null, true);
                    sa.Value = ReadAttribute(e, "Value", null, true);
                    attributes.Add(sa);
                }
            }
        }

        /// <returns></returns>
        /// <param name="needed"></param>
        /// <param name="defval"></param>
        /// <param name="attrib"></param>
        /// <param name="element"></param>
        /// </summary>
        /// <summary>
        private string ReadAttribute(XmlElement element, string attrib, string defval, bool needed)
        {
            if (element != null && element.HasAttribute(attrib))
            {
                return element.Attributes[attrib].Value;
            }
            if (needed)
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
        /// <summary>
        private void ReadAttribute(ref string retval, bool inherited, XmlElement element, string attrib, string defval,
            bool needed)
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
        /// <summary>
        private void ReadAttributeBool(ref bool retval, bool inherited, XmlElement element, string attrib, bool defval,
            bool needed)
        {
            var tmp = retval.ToString();
            ReadAttribute(ref tmp, inherited, element, attrib, defval.ToString(), needed);
            retval = bool.Parse(tmp);
        }

        /// <returns></returns>
        /// <param name="needed"></param>
        /// <param name="defval"></param>
        /// <param name="attrib"></param>
        /// <param name="element"></param>
        /// </summary>
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
        /// <summary>
        private void ReadAttributeByte(ref byte retval, bool inherited, XmlElement element, string attrib, byte defval,
            bool needed)
        {
            var tmp = retval.ToString();
            ReadAttribute(ref tmp, inherited, element, attrib, defval.ToString(), needed);
            retval = byte.Parse(tmp);
        }

        /// <param name="needed"></param>
        /// <param name="defval"></param>
        /// <param name="attrib"></param>
        /// <param name="element"></param>
        /// <param name="inherited"></param>
        /// <param name="retval"></param>
        /// </summary>
        /// <summary>
        private void ReadAttributeColor(ref Color retval, bool inherited, XmlElement element, string attrib,
            Color defval, bool needed)
        {
            var tmp = ColorToString(retval);
            ReadAttribute(ref tmp, inherited, element, attrib, ColorToString(defval), needed);
            retval = Utilities.ParseColor(tmp);
        }

        /// <returns></returns>
        /// <param name="needed"></param>
        /// <param name="defval"></param>
        /// <param name="attrib"></param>
        /// <param name="element"></param>
        /// </summary>
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
        /// <summary>
        private void ReadAttributeInt(ref int retval, bool inherited, XmlElement element, string attrib, int defval,
            bool needed)
        {
            var tmp = retval.ToString();
            ReadAttribute(ref tmp, inherited, element, attrib, defval.ToString(), needed);
            retval = int.Parse(tmp);
        }
    }
}
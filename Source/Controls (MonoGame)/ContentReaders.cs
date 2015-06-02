

using System;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework.Content;

#if (!XBOX && !XBOX_FAKE)
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
#endif


namespace MonoForce.Controls
{

/// </summary>
/// Represents a Neoforce Layout file.
/// <summary>
public class LayoutXmlDocument : XmlDocument { }
/// </summary>
/// Represents a Neoforce Skin file.
/// <summary>
public class SkinXmlDocument : XmlDocument { }


/// </summary>
/// Reads a Skin document from binary format. (.xml | .skin) ???
/// <summary>
public class SkinReader : ContentTypeReader<SkinXmlDocument>
{


/// <returns>Returns the loaded skin file.</returns>
/// <param name="existingInstance">Existing instance to read stream data into.</param>
/// <param name="input">Content reader used to read the skin file.</param>
/// </summary>
/// Reads a Skin file from binary format.
/// <summary>
protected override SkinXmlDocument Read(ContentReader input, SkinXmlDocument existingInstance)
{
if (existingInstance == null)
{
SkinXmlDocument doc = new SkinXmlDocument();
doc.LoadXml(input.ReadString());
return doc;
}
else
{
existingInstance.LoadXml(input.ReadString());
}

return existingInstance;
}


}

/// </summary>
/// Reads a Layout document from binary format.
/// <summary>
public class LayoutReader : ContentTypeReader<LayoutXmlDocument>
{


/// <returns>Returns the Layout document from the stream.</returns>
/// <param name="existingInstance">Existing instance to read into.</param>
/// <param name="input">Content reader used to read the Layout document.</param>
/// </summary>
/// Reads a Layout document from the current stream.
/// <summary>
protected override LayoutXmlDocument Read(ContentReader input, LayoutXmlDocument existingInstance)
{
if (existingInstance == null)
{
LayoutXmlDocument doc = new LayoutXmlDocument();
doc.LoadXml(input.ReadString());
return doc;
}
else
{
existingInstance.LoadXml(input.ReadString());
}

return existingInstance;
}


}

#if (!XBOX && !XBOX_FAKE)

/// </summary>
/// Reads a cursor file from binary format.
/// <summary>
public class CursorReader : ContentTypeReader<Cursor>
{


/// <returns>Returns the cursor object from the stream.</returns>
/// <param name="existingInstance">Existing cursor object to read into.</param>
/// <param name="input">Content reader used to read the cursor.</param>
/// </summary>
/// Reads a cursor type from the current stream.
/// <summary>
protected override Cursor Read(ContentReader input, Cursor existingInstance)
{
if (existingInstance == null)
{
int count = input.ReadInt32();
byte[] data = input.ReadBytes(count);

string path = Path.GetTempFileName();
File.WriteAllBytes(path, data);
string tPath = Path.GetTempFileName();
using(System.Drawing.Icon i = System.Drawing.Icon.ExtractAssociatedIcon(path))
{
using (System.Drawing.Bitmap b = i.ToBitmap())
{

b.Save(tPath, System.Drawing.Imaging.ImageFormat.Png);
b.Dispose();
}

i.Dispose();
}
IntPtr handle = NativeMethods.LoadCursor(path);
System.Windows.Forms.Cursor c = new System.Windows.Forms.Cursor(handle);
Vector2 hs = new Vector2(c.HotSpot.X, c.HotSpot.Y);
int w = c.Size.Width;
int h = c.Size.Height;
c.Dispose();
File.Delete(path);

return new Cursor(tPath, hs, w, h);
}
else
{
}

return existingInstance;
}


}

#endif

}


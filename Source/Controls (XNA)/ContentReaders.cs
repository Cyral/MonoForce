using System.IO;
using System.Xml;
using Microsoft.Xna.Framework.Content;
#if (!XBOX && !XBOX_FAKE)
using System.Windows.Forms;

#endif

namespace MonoForce.Controls
{
    /// <summary>
    /// Represents a Neoforce Layout file.
    /// </summary>
    public class LayoutXmlDocument : XmlDocument
    {
    }

    /// <summary>
    /// Represents a Neoforce Skin file.
    /// </summary>
    public class SkinXmlDocument : XmlDocument
    {
    }


    public class SkinReader : ContentTypeReader<SkinXmlDocument>
    {
        /// <returns>Returns the loaded skin file.</returns>
        /// <param name="existingInstance">Existing instance to read stream data into.</param>
        /// <param name="input">Content reader used to read the skin file.</param>
        /// <summary>
        /// Reads a Skin file from binary format.
        /// </summary>
        protected override SkinXmlDocument Read(ContentReader input, SkinXmlDocument existingInstance)
        {
            if (existingInstance == null)
            {
                var doc = new SkinXmlDocument();
                doc.LoadXml(input.ReadString());
                return doc;
            }
            existingInstance.LoadXml(input.ReadString());

            return existingInstance;
        }
    }

    /// <summary>
    /// Reads a Layout document from binary format.
    /// </summary>
    public class LayoutReader : ContentTypeReader<LayoutXmlDocument>
    {
        /// <returns>Returns the Layout document from the stream.</returns>
        /// <param name="existingInstance">Existing instance to read into.</param>
        /// <param name="input">Content reader used to read the Layout document.</param>
        /// <summary>
        /// Reads a Layout document from the current stream.
        /// </summary>
        protected override LayoutXmlDocument Read(ContentReader input, LayoutXmlDocument existingInstance)
        {
            if (existingInstance == null)
            {
                var doc = new LayoutXmlDocument();
                doc.LoadXml(input.ReadString());
                return doc;
            }
            existingInstance.LoadXml(input.ReadString());

            return existingInstance;
        }
    }

#if (!XBOX && !XBOX_FAKE)

    public class CursorReader : ContentTypeReader<Cursor>
    {
        /// <returns>Returns the cursor object from the stream.</returns>
        /// <param name="existingInstance">Existing cursor object to read into.</param>
        /// <param name="input">Content reader used to read the cursor.</param>
        /// <summary>
        /// Reads a cursor type from the current stream.
        /// </summary>
        protected override Cursor Read(ContentReader input, Cursor existingInstance)
        {
            if (existingInstance == null)
            {
                var count = input.ReadInt32();
                var data = input.ReadBytes(count);

                var path = Path.GetTempFileName();
                File.WriteAllBytes(path, data);

                var handle = NativeMethods.LoadCursor(path);
                var cur = new Cursor(handle);
                File.Delete(path);

                return cur;
            }

            return existingInstance;
        }
    }

#endif
}

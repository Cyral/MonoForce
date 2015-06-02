////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Importers                                        //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: LayoutImporter.cs                            //
//                                                            //
//      Version: 0.7                                          //
//                                                            //
//         Date: 15/02/2010                                   //
//                                                            //
//       Author: Tom Shane                                    //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//  Layout file importer.                                     //
//                                                            //
////////////////////////////////////////////////////////////////

#region //// Using /////////////

////////////////////////////////////////////////////////////////////////////
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

////////////////////////////////////////////////////////////////////////////

#endregion

namespace MonoForce.Importers
{

    #region //// Importer //////////

    ////////////////////////////////////////////////////////////////////////////
    public class LayoutXmlDocument : XmlDocument
    {
    }

    ////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////
    [ContentImporter(".xml", DisplayName = "Layout - Neoforce Controls")]
    internal class LayoutImporter : ContentImporter<LayoutXmlDocument>
    {
        #region //// Methods ///////////

        ////////////////////////////////////////////////////////////////////////////
        public override LayoutXmlDocument Import(string filename, ContentImporterContext context)
        {
            var doc = new LayoutXmlDocument();
            doc.Load(filename);

            return doc;
        }

        ////////////////////////////////////////////////////////////////////////////

        #endregion
    }

    ////////////////////////////////////////////////////////////////////////////

    #endregion

    #region //// Writer ////////////

    ////////////////////////////////////////////////////////////////////////////
    [ContentTypeWriter]
    internal class LayoutWriter : ContentTypeWriter<LayoutXmlDocument>
    {
        #region //// Methods ///////////

        ////////////////////////////////////////////////////////////////////////////
        protected override void Write(ContentWriter output, LayoutXmlDocument value)
        {
            output.Write(value.InnerXml);
        }

        ///////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof (LayoutXmlDocument).AssemblyQualifiedName;
        }

        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////    
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            if (targetPlatform == TargetPlatform.Xbox360)
            {
                return "MonoForce.Controls.LayoutReader, MonoForce.Controls.360";
            }
            return "MonoForce.Controls.LayoutReader, MonoForce.Controls";
        }

        ////////////////////////////////////////////////////////////////////////////

        #endregion
    }

    ////////////////////////////////////////////////////////////////////////////

    #endregion
}
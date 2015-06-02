////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Importers                                        //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: SkinImporter.cs                              //
//                                                            //
//      Version: 0.7                                          //
//                                                            //
//         Date: 15/02/2010                                   //
//                                                            //
//       Author: Tom Shane                                    //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//  Skin file importer.                                       //
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
    internal class SkinXmlDocument : XmlDocument
    {
    }

    ////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////
    [ContentImporter(".xml", DisplayName = "Skin - Neoforce Controls")]
    internal class SkinImporter : ContentImporter<SkinXmlDocument>
    {
        #region //// Methods ///////////

        ////////////////////////////////////////////////////////////////////////////
        public override SkinXmlDocument Import(string filename, ContentImporterContext context)
        {
            var doc = new SkinXmlDocument();
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
    internal class SkinWriter : ContentTypeWriter<SkinXmlDocument>
    {
        #region //// Methods ///////////

        ////////////////////////////////////////////////////////////////////////////
        protected override void Write(ContentWriter output, SkinXmlDocument value)
        {
            output.Write(value.InnerXml);
        }

        ///////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof (SkinXmlDocument).AssemblyQualifiedName;
        }

        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////    
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            if (targetPlatform == TargetPlatform.Xbox360)
            {
                return "MonoForce.Controls.SkinReader, MonoForce.Controls.360";
            }
            return "MonoForce.Controls.SkinReader, MonoForce.Controls";
        }

        ////////////////////////////////////////////////////////////////////////////

        #endregion
    }

    ////////////////////////////////////////////////////////////////////////////

    #endregion
}
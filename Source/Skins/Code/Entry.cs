////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Skins                                            //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: __Entry.cs                                   //
//                                                            //
//      Version: 0.7                                          //
//                                                            //
//         Date: 11/09/2010                                   //
//                                                            //
//       Author: Tom Shane                                    //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//  Copyright (c) by Tom Shane                                //
//                                                            //
////////////////////////////////////////////////////////////////

#region //// Using /////////////

////////////////////////////////////////////////////////////////////////////
using System.Diagnostics;

////////////////////////////////////////////////////////////////////////////

#endregion

namespace MonoForce.Skins
{
    internal static class Entry
    {
        #region //// Methods ///////////

        ////////////////////////////////////////////////////////////////////////////
        private static void Main(string[] args)
        {
#if (!XBOX && !XBOX_FAKE)
            try
            {
                var Proc = new Process();
                Proc.StartInfo.FileName = "..\\..\\BuildSkins.bat";
                Proc.Start();
            }
            catch
            {
            }
#else
        Console.WriteLine("No action for Xbox platform defined.");
      #endif
        }

        ////////////////////////////////////////////////////////////////////////////

        #endregion
    }
}
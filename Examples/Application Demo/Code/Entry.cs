////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Central                                          //
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

#region Using

using System;
using System.IO;

#endregion

namespace MonoForce.Examples.ApplicationDemo
{
    static class Entry
    {

        #region Methods

#if (!XBOX && !XBOX_FAKE)
        [STAThread]
#endif

        static void Main(string[] args)
        {
            using (ApplicationDemo central = new ApplicationDemo())
            {
                central.Run();
            }
        }

        #endregion

    }
}
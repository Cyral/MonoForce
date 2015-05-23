////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Controls                                         //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: NativeMethods.cs                             //
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
using System;
using System.Runtime.InteropServices;
////////////////////////////////////////////////////////////////////////////

#endregion

namespace TomShane.Neoforce.Controls
{

  internal static class NativeMethods
  {    

    #region //// Methods ///////////

    ////////////////////////////////////////////////////////////////////////////
    [DllImport("User32.dll", CharSet = CharSet.Unicode)]
    internal static extern IntPtr LoadImage(IntPtr instance, string fileName, uint type, int width, int height, uint load);
    ////////////////////////////////////////////////////////////////////////////    
    
    ////////////////////////////////////////////////////////////////////////////
    [DllImport("User32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool DestroyCursor(IntPtr cursor);
    ////////////////////////////////////////////////////////////////////////////
       
    ////////////////////////////////////////////////////////////////////////////
    internal static IntPtr LoadCursor(string fileName)
    {      
      return LoadImage(IntPtr.Zero, fileName, 2, 0, 0, 0x0010);
    }
    ////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////
    [DllImport("user32.dll")]
    internal static extern short GetKeyState(int key);
    ////////////////////////////////////////////////////////////////////////////
    
    #endregion    
    
  }
  
}
  
using System;
using System.Runtime.InteropServices;

namespace MonoForce.Controls
{
    [Obsolete("Native methods should be avoided at all times")]
    /// <summary>
    /// Imports a few native functions we need.
    /// </summary>
    internal static class NativeMethods
    {
        [Obsolete, DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyCursor(IntPtr cursor);

        [Obsolete, DllImport("user32.dll")]
        internal static extern short GetKeyState(int key);

        [Obsolete]
        internal static IntPtr LoadCursor(string fileName)
        {
            return LoadImage(IntPtr.Zero, fileName, 2, 0, 0, 0x0010);
        }

        [Obsolete, DllImport("User32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr LoadImage(IntPtr instance, string fileName, uint type, int width, int height,
            uint load);
    }
}

using System;
using Microsoft.Xna.Framework;

namespace MonoForce.Controls
{
    internal static class Utilities
    {
        /// <returns>Returns the unqualified name of the specified control.</returns>
        /// <param name="control">Control to get the name of.</param>
        /// <summary>
        /// Attempts to return the name of a control.
        /// </summary>
        public static string DeriveControlName(Control control)
        {
            if (control != null)
            {
                try
                {
                    var str = control.ToString();
                    var i = str.LastIndexOf(".");
                    return str.Remove(0, i + 1);
                }
                catch
                {
                    return control.ToString();
                }
            }
            return control.ToString();
        }

        /// <returns>Returns the string converted to a BevelStyle object.</returns>
        /// <param name="str">Name of the bevel style value.</param>
        /// <summary>
        /// Parses a BevelStyle enumeration defined in XML.
        /// </summary>
        public static BevelStyle ParseBevelStyle(string str)
        {
            return (BevelStyle)Enum.Parse(typeof (BevelStyle), str, true);
        }

        /// <returns>Returns the string converted to a Color object.</returns>
        /// <param name="str">";" delimited string of RGBA values.</param>
        /// <summary>
        /// Parses a color value defined in XML.
        /// </summary>
        public static Color ParseColor(string str)
        {
            var val = str.Split(';');
            byte r = 255, g = 255, b = 255, a = 255;

            if (val.Length >= 1) r = byte.Parse(val[0]);
            if (val.Length >= 2) g = byte.Parse(val[1]);
            if (val.Length >= 3) b = byte.Parse(val[2]);
            if (val.Length >= 4) a = byte.Parse(val[3]);

            return Color.FromNonPremultiplied(r, g, b, a);
        }
    }
}

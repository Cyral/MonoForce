using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForce.Controls
{
    public static class Extensions
    {
        private static string color = "Red";
        private static string[] msgs;
        private static readonly StringBuilder s = new StringBuilder();
        private static string[] splits;

        public static Vector2 MeasureRichString(this SpriteFont font, string text, Manager manager,
            bool DrawFormattedString = true)
        {
            if (DrawFormattedString)
            {
                // only bother if we have color commands involved
                if (text.Contains("[color:"))
                {
                    s.Clear();
                    // how far in x to offset from position
                    var currentOffset = 0;

                    // example:
                    // string.Format("You attempt to hit the [color:#FFFF0000]{0}[/color] but [color:{1}]MISS[/color]!",
                    // currentMonster.Name, Color.Red.ToHex(true));
                    splits = text.Split(Manager.StringColorStart, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var str in splits)
                    {
                        // if this section starts with a color
                        if (str.StartsWith(":"))
                        {
                            // #AARRGGBB
                            // #FFFFFFFFF
                            // #123456789
                            var end = str.IndexOf("]");

                            color = str.Substring(1, end - 1);
                            // any subsequent msgs after the [/color] tag are defaultColor
                            msgs = str.Substring(end + 1)
                                .Split(Manager.StringColorEnd, StringSplitOptions.RemoveEmptyEntries);
                            if (msgs.Length > 0)
                            {
                                // always draw [0] there should be at least one
                                s.Append(msgs[0]);
                                currentOffset += (int)font.MeasureString(msgs[0]).X;

                                // there should only ever be one other string or none
                                if (msgs.Length == 2)
                                {
                                    s.Append(msgs[1]);
                                }
                            }
                        }
                        else
                        {
                            s.Append(str);
                        }
                    }
                    return font.MeasureString(s);
                }
                return font.MeasureString(text);
            }
            return font.MeasureString(text);
        }

        /// Creates a
        /// <see cref="Color" />
        /// value from an ARGB or RGB hex string.  The string may
        /// begin with or without the hash mark (#) character.
        /// <summary>
        /// <param name="hexString">The ARGB hex string to parse.</param>
        /// <returns>
        /// A <see cref="Color" /> value as defined by the ARGB or RGB hex string.
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown if the string is not a valid ARGB or RGB hex value.</exception>
        public static Color ToColor(this string hexString)
        {
            var color = Color.White;
            if (hexString.StartsWith("#"))
                hexString = hexString.Substring(1);
            bool success;
            uint hex;
            success = uint.TryParse(hexString, NumberStyles.HexNumber, null, out hex);
            if (success)
            {
                if (hexString.Length == 8)
                {
                    color.A = (byte)(hex >> 24);
                    color.R = (byte)(hex >> 16);
                    color.G = (byte)(hex >> 8);
                    color.B = (byte)(hex);
                }
                else if (hexString.Length == 6)
                {
                    color.R = (byte)(hex >> 16);
                    color.G = (byte)(hex >> 8);
                    color.B = (byte)(hex);
                }
                else
                {
                    return color;
                }
            }
            return color;
        }

        //public static Color FromName(this string colorName)
        //{
        //    System.Drawing.Color systemColor = System.Drawing.Color.FromName(colorName);
        //    return new Color(systemColor.R, systemColor.G, systemColor.B, systemColor.A); //Here Color is Microsoft.Xna.Framework.Graphics.Color
        //}
        /// </summary>
        /// Creates an ARGB hex string representation of the <see cref="Color" /> value.
        /// <summary>
        /// <param name="color">The <see cref="Color" /> value to parse.</param>
        /// <param name="includeHash">Determines whether to include the hash mark (#) character in the string.</param>
        /// <returns>A hex string representation of the specified <see cref="Color" /> value.</returns>
        public static string ToHex(this Color color, bool includeHash)
        {
            string[] argb =
            {
                color.A.ToString("X2"),
                color.R.ToString("X2"),
                color.G.ToString("X2"),
                color.B.ToString("X2")
            };
            return (includeHash ? "#" : string.Empty) + string.Join(string.Empty, argb);
        }

        /// </summary>
        /// Lowercase string.
        /// <summary>
        public static string ToLowerFast(this string value)
        {
            var output = value.ToCharArray();
            for (var i = 0; i < output.Length; i++)
            {
                if (output[i] >= 'A' &&
                    output[i] <= 'Z')
                {
                    output[i] = (char)(output[i] + 32);
                }
            }
            return new string(output);
        }
    }

    public static class Colors
    {
        //Use reflection to get all of the colors contained in the Color class
        private static readonly Dictionary<string, Color> strDictionary =
            typeof (Color).GetProperties(BindingFlags.Public |
                                         BindingFlags.Static)
                .Where(prop => prop.PropertyType == typeof (Color))
                .ToDictionary(prop => prop.Name.ToLowerFast(),
                    prop => (Color)prop.GetValue(null, null));

        public static Color FromName(this string name)
        {
            //Lower the case of the name using a faster method
            name = name.ToLowerFast();
            //If the strDictionary contains the key, get the value, if it dosent return the default value
            if (strDictionary.ContainsKey(name))
                return strDictionary[name];
            return Color.White;
        }

        public static string ToColorString(this Color color)
        {
            var st = strDictionary.FirstOrDefault(x => x.Value == color).Key;
            if (!string.IsNullOrWhiteSpace(st))
                return st;
            return "White";
        }

        #region Color Extensions

        /// </summary>
        /// Convert HSV to RGB
        /// h is from 0-360
        /// s,v values are 0-1
        /// r,g,b values are 0-255
        /// Based upon http://ilab.usc.edu/wiki/index.php/HSV_And_H2SV_Color_Space#HSV_Transformation_C_.2F_C.2B.2B_Code_2
        /// <summary>
        public static Color ColorFromHSV(double h, double S, double V)
        {
            int r, g, b;

            var H = h;
            while (H < 0)
            {
                H += 360;
            }
            ;
            while (H >= 360)
            {
                H -= 360;
            }
            ;
            double R, G, B;
            if (V <= 0)
            {
                R = G = B = 0;
            }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                var hf = H / 60.0;
                var i = (int)Math.Floor(hf);
                var f = hf - i;
                var pv = V * (1 - S);
                var qv = V * (1 - S * f);
                var tv = V * (1 - S * (1 - f));
                switch (i)
                {
                    // Red is the dominant color
                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((int)(R * 255.0));
            g = Clamp((int)(G * 255.0));
            b = Clamp((int)(B * 255.0));
            return new Color(r, g, b);
        }

        /// </summary>
        /// Clamp a value to 0-255
        /// <summary>
        private static int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }

        #endregion
    }
}

using System;
using System.Drawing;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace MonoForce.Controls
{
    public static class Extensions
    {
        private static string color = "Red";
        private static string[] msgs;
        private static readonly StringBuilder s = new StringBuilder();
        private static string[] splits;

        public static Vector2 MeasureRichString(this SpriteFont font, string text, Manager manager,
            bool drawFormattedString = true)
        {
            if (drawFormattedString)
            {
                // only bother if we have color commands involved
                if (text.Contains("[color:"))
                {
                    s.Clear();
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
                                s.Append(msgs[0]);

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
    }

    internal static class Colors
    {
        public static Color FromName(this string name)
        {
            var color = ColorTranslator.FromHtml(name);
            return new Color(color.R, color.G, color.B, color.A);
        }

        public static string ToColorString(this Color color)
        {
            var col = ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B));
            return !string.IsNullOrWhiteSpace(col) ? col : "White";
        }

        /// <summary>
        /// Convert HSV to RGB
        /// h is from 0-360
        /// s,v values are 0-1
        /// r,g,b values are 0-255
        /// Based upon http://ilab.usc.edu/wiki/index.php/HSV_And_H2SV_Color_Space#HSV_Transformation_C_.2F_C.2B.2B_Code_2
        /// </summary>
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
                var hf = H/60.0;
                var i = (int) Math.Floor(hf);
                var f = hf - i;
                var pv = V*(1 - S);
                var qv = V*(1 - S*f);
                var tv = V*(1 - S*(1 - f));
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
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((int) (R*255.0));
            g = Clamp((int) (G*255.0));
            b = Clamp((int) (B*255.0));
            return new Color(r, g, b);
        }

        private static int Clamp(int i)
        {
            if (i < 0) return 0;
            return i > 255 ? 255 : i;
        }
    }
}
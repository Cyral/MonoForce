using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForce.Controls
{
    /// <summary>
    /// Provides a basic Software cursor
    /// </summary>
    public class Cursor
    {
        public Texture2D CursorTexture { get; set; }
        public int Height { get; set; }
        public Vector2 HotSpot { get; set; }
        public int Width { get; set; }
        internal string cursorPath;

        public Cursor(string path, Vector2 hotspot, int width, int height)
        {
            cursorPath = path;
            this.HotSpot = hotspot;
            this.Width = width;
            this.Height = height;
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForce.Controls
{
    public class ImageBox : Control
    {
        /// </summary>
        /// Gets or sets the image the control will display.
        /// <summary>
        public Texture2D Image
        {
            get { return image; }
            set
            {
                image = value;
                sourceRect = new Rectangle(0, 0, image.Width, image.Height);
                Invalidate();
                if (!Suspended) OnImageChanged(new EventArgs());
            }
        }

        /// </summary>
        /// image and control dimensions are different sizes.
        /// Indicates how the image will be positioned and scaled when the
        /// <summary>
        public SizeMode SizeMode
        {
            get { return sizeMode; }
            set
            {
                if (value == SizeMode.Auto && image != null)
                {
                    Width = image.Width;
                    Height = image.Height;
                }
                sizeMode = value;
                Invalidate();
                if (!Suspended) OnSizeModeChanged(new EventArgs());
            }
        }

        /// </summary>
        /// Defines the region of the texture that is displayed in the image box control.
        /// <summary>
        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set
            {
                if (value != null && image != null)
                {
                    var l = value.Left;
                    var t = value.Top;
                    var w = value.Width;
                    var h = value.Height;

                    if (l < 0) l = 0;
                    if (t < 0) t = 0;
                    if (w > image.Width) w = image.Width;
                    if (h > image.Height) h = image.Height;
                    if (l + w > image.Width) w = (image.Width - l);
                    if (t + h > image.Height) h = (image.Height - t);

                    sourceRect = new Rectangle(l, t, w, h);
                }
                else if (image != null)
                {
                    sourceRect = new Rectangle(0, 0, image.Width, image.Height);
                }
                else
                {
                    sourceRect = Rectangle.Empty;
                }
                Invalidate();
            }
        }

        /// </summary>
        /// Image the control will display.
        /// <summary>
        private Texture2D image;

        /// </summary>
        /// and control dimensions are not the same.
        /// Indicates how the image will be positioned/scaled when image
        /// <summary>
        private SizeMode sizeMode = SizeMode.Normal;

        /// </summary>
        /// Defines the region of the texture that is displayed in the control.
        /// <summary>
        private Rectangle sourceRect = Rectangle.Empty;

        public ImageBox(Manager manager) : base(manager)
        {
        }

        /// </summary>
        /// Occurs when the control's image is changed.
        /// <summary>
        public event EventHandler ImageChanged;

        /// </summary>
        /// Initializes the image box control.
        /// <summary>
        public override void Init()
        {
            base.Init();
            CanFocus = false;
            Color = Color.White;
        }

        /// </summary>
        /// Occurs when the control's size mode is changed.
        /// <summary>
        public event EventHandler SizeModeChanged;

        public override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            if (image != null)
            {
                if (sizeMode == SizeMode.Normal)
                {
                    renderer.Draw(image, rect.X, rect.Y, sourceRect, Color);
                }
                else if (sizeMode == SizeMode.Auto)
                {
                    renderer.Draw(image, rect.X, rect.Y, sourceRect, Color);
                }
                else if (sizeMode == SizeMode.Stretched)
                {
                    renderer.Draw(image, rect, sourceRect, Color);
                }
                else if (sizeMode == SizeMode.Centered)
                {
                    var x = (rect.Width / 2) - (image.Width / 2);
                    var y = (rect.Height / 2) - (image.Height / 2);

                    renderer.Draw(image, x, y, sourceRect, Color);
                }
                else if (sizeMode == SizeMode.Tiled)
                {
                    renderer.DrawTileTexture(image, rect, Color);
                }
            }
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles when the control's image is changed.
        /// <summary>
        protected virtual void OnImageChanged(EventArgs e)
        {
            if (ImageChanged != null) ImageChanged.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles when the control's size mode value is changed.
        /// <summary>
        protected virtual void OnSizeModeChanged(EventArgs e)
        {
            if (SizeModeChanged != null) SizeModeChanged.Invoke(this, e);
        }
    }
}
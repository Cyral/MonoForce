using System;
using Microsoft.Xna.Framework;

namespace MonoForce.Controls
{
    public class ScrollBar : Control
    {
        /// </summary>
        /// Gets or sets the increment the scroll bar value changes for large increments.
        /// <summary>
        public virtual int PageSize
        {
            get { return pageSize; }
            set
            {
                if (pageSize != value)
                {
                    pageSize = value;
                    if (pageSize > range) pageSize = range;
                    RecalcParams();
                    if (!Suspended) OnPageSizeChanged(new EventArgs());
                }
            }
        }

        /// </summary>
        /// Gets or sets the maximum value of the scroll bar.
        /// <summary>
        public virtual int Range
        {
            get { return range; }
            set
            {
                if (range != value)
                {
                    range = value;
                    if (pageSize > range) pageSize = range;
                    RecalcParams();
                    if (!Suspended) OnRangeChanged(new EventArgs());
                }
            }
        }

        /// </summary>
        /// Gets or sets the increment the scroll bar value changes for small increments.
        /// <summary>
        public virtual int StepSize
        {
            get { return stepSize; }
            set
            {
                if (stepSize != value)
                {
                    stepSize = value;
                    if (!Suspended) OnStepSizeChanged(new EventArgs());
                }
            }
        }

        /// </summary>
        /// Gets or sets the current value of the scroll bar.
        /// <summary>
        public virtual int Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    if (this.value < 0) this.value = 0;
                    if (this.value > range - pageSize) this.value = range - pageSize;
                    Invalidate();
                    if (!Suspended) OnValueChanged(new EventArgs());
                }
            }
        }

        /// </summary>
        /// Button used to decrease the value of the scroll bar.
        /// <summary>
        private readonly Button btnMinus;

        /// </summary>
        /// Button used to increase the value of the scroll bar.
        /// <summary>
        private readonly Button btnPlus;

        /// </summary>
        /// can drag up/down or left/right to scroll.
        /// Button indicating the current value of the scroll bar that the user
        /// <summary>
        private readonly Button btnSlider;

        /// </summary>
        /// Indicates if the control is a horizontal or vertical scroll bar.
        /// <summary>
        private readonly Orientation orientation = Orientation.Vertical;

        /// </summary>
        /// minus buttons.
        /// String for accessing the resource used to draw the scroll bar plus and
        /// <summary>
        private readonly string strButton = "ScrollBar.ButtonVert";

        /// </summary>
        /// String for accessing the glyph displayed on the scroll bar slider button.
        /// <summary>
        private readonly string strGlyph = "ScrollBar.GlyphVert";

        /// </summary>
        /// String for accessing the Arrow Up glyph for the scroll bar.
        /// <summary>
        private readonly string strMinus = "ScrollBar.ArrowUp";

        /// </summary>
        /// String for accessing the Arrow Down glyph for the scroll bar.
        /// <summary>
        private readonly string strPlus = "ScrollBar.ArrowDown";

        /// </summary>
        /// button moves on.
        /// String for accessing the resource used to draw the rail that the slider
        /// <summary>
        private readonly string strRail = "ScrollBar.RailVert";

        /// </summary>
        /// String for accessing the resource used to draw the scroll bar slider button.
        /// <summary>
        private readonly string strSlider = "ScrollBar.SliderVert";

        /// </summary>
        /// Increment used for large changes to the scroll bar value.
        /// <summary>
        private int pageSize = 50;

        /// </summary>
        /// Maximum value of the scroll bar control.
        /// <summary>
        private int range = 100;

        /// </summary>
        /// Increment used for small changes to the scroll bar value.
        /// <summary>
        private int stepSize = 1;

        /// </summary>
        /// Current value of the scroll bar control.
        /// <summary>
        private int value;

        public ScrollBar(Manager manager, Orientation orientation) : base(manager)
        {
            this.orientation = orientation;
            CanFocus = false;


// Set the skin accessor strings based on orientation.
            if (orientation == Orientation.Horizontal)
            {
                strButton = "ScrollBar.ButtonHorz";
                strRail = "ScrollBar.RailHorz";
                strSlider = "ScrollBar.SliderHorz";
                strGlyph = "ScrollBar.GlyphHorz";
                strMinus = "ScrollBar.ArrowLeft";
                strPlus = "ScrollBar.ArrowRight";

                MinimumHeight = 16;
                MinimumWidth = 46;
                Width = 64;
                Height = 16;
            }
// Vertical scroll bar.
            else
            {
                strButton = "ScrollBar.ButtonVert";
                strRail = "ScrollBar.RailVert";
                strSlider = "ScrollBar.SliderVert";
                strGlyph = "ScrollBar.GlyphVert";
                strMinus = "ScrollBar.ArrowUp";
                strPlus = "ScrollBar.ArrowDown";

                MinimumHeight = 46;
                MinimumWidth = 16;
                Width = 16;
                Height = 64;
            }

// Create the minus button.
            btnMinus = new Button(Manager);
            btnMinus.Init();
            btnMinus.Text = "";
            btnMinus.MousePress += ArrowPress;
            btnMinus.CanFocus = false;

// Create the slider button.
            btnSlider = new Button(Manager);
            btnSlider.Init();
            btnSlider.Text = "";
            btnSlider.CanFocus = false;
            btnSlider.MinimumHeight = 16;
            btnSlider.MinimumWidth = 16;

// Create the plus button.
            btnPlus = new Button(Manager);
            btnPlus.Init();
            btnPlus.Text = "";
            btnPlus.MousePress += ArrowPress;
            btnPlus.CanFocus = false;

            btnSlider.Move += btnSlider_Move;

            Add(btnMinus);
            Add(btnSlider);
            Add(btnPlus);
        }

        /// </summary>
        /// Initializes the scroll bar control.
        /// <summary>
        public override void Init()
        {
            base.Init();

            var sc = new SkinControl(btnPlus.Skin);
            sc.Layers["Control"] = new SkinLayer(Skin.Layers[strButton]);
            sc.Layers[strButton].Name = "Control";
            btnPlus.Skin = btnMinus.Skin = sc;

            var ss = new SkinControl(btnSlider.Skin);
            ss.Layers["Control"] = new SkinLayer(Skin.Layers[strSlider]);
            ss.Layers[strSlider].Name = "Control";
            btnSlider.Skin = ss;

            btnMinus.Glyph = new Glyph(Skin.Layers[strMinus].Image.Resource);
            btnMinus.Glyph.SizeMode = SizeMode.Centered;
            btnMinus.Glyph.Color = Manager.Skin.Controls["Button"].Layers["Control"].Text.Colors.Enabled;

            btnPlus.Glyph = new Glyph(Skin.Layers[strPlus].Image.Resource);
            btnPlus.Glyph.SizeMode = SizeMode.Centered;
            btnPlus.Glyph.Color = Manager.Skin.Controls["Button"].Layers["Control"].Text.Colors.Enabled;

            btnSlider.Glyph = new Glyph(Skin.Layers[strGlyph].Image.Resource);
            btnSlider.Glyph.SizeMode = SizeMode.Centered;
        }

        /// </summary>
        /// Occurs when the page size of the scroll bar changes.
        /// <summary>
        public event EventHandler PageSizeChanged;

        /// </summary>
        /// Occurs when the range of the scroll bar changes.
        /// <summary>
        public event EventHandler RangeChanged;

        public void ScrollDown()
        {
            Value += stepSize;
            if (Value > range - pageSize) Value = range - pageSize - 1;
        }

        public void ScrollDown(bool alot)
        {
            if (alot)
            {
                Value += pageSize;
                if (Value > range - pageSize) Value = range - pageSize - 1;
            }
// Vertical scroll bar.
            else
                ScrollDown();
        }

        public void ScrollUp()
        {
            Value -= stepSize;
            if (Value < 0) Value = 0;
        }

        public void ScrollUp(bool alot)
        {
            if (alot)
            {
                Value -= pageSize;
                if (Value < 0) Value = 0;
            }
// Vertical scroll bar.
            else
                ScrollUp();
        }

        /// </summary>
        /// Occurs when the step size of the scroll bar changes.
        /// <summary>
        public event EventHandler StepSizeChanged;

        /// </summary>
        /// Occurs when the value of the scroll bar changes.
        /// <summary>
        public event EventHandler ValueChanged;

        /// </summary>
        /// Initializes the skin for the scroll bar control.
        /// <summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls["ScrollBar"]);
        }

        public override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            RecalcParams();

            var bg = Skin.Layers[strRail];
            renderer.DrawLayer(bg, rect, Color.White, bg.States.Enabled.Index);
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles mouse button down events for the scroll bar.
        /// <summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            btnSlider.Passive = true;

            if (e.Button == MouseButton.Left)
            {
// Update the slider button's position.
                if (orientation == Orientation.Horizontal)
                {
                    var pos = e.Position.X;

                    if (pos < btnSlider.Left)
                    {
                        ScrollUp(true);
                    }
                    else if (pos >= btnSlider.Left + btnSlider.Width)
                    {
                        ScrollDown(true);
                    }
                }
// Vertical scroll bar.
                else
                {
                    var pos = e.Position.Y;

                    if (pos < btnSlider.Top)
                    {
                        ScrollUp(true);
                    }
                    else if (pos >= btnSlider.Top + btnSlider.Height)
                    {
                        ScrollDown(true);
                    }
                }
            }
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles mouse button up events for the scroll bar.
        /// <summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            btnSlider.Passive = false;
            base.OnMouseUp(e);
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles changes in the scroll bar's page size.
        /// <summary>
        protected virtual void OnPageSizeChanged(EventArgs e)
        {
            if (PageSizeChanged != null) PageSizeChanged.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles changes in the scroll bar's range.
        /// <summary>
        protected virtual void OnRangeChanged(EventArgs e)
        {
            if (RangeChanged != null) RangeChanged.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles resizing the scroll bar control.
        /// <summary>
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            RecalcParams();
            if (Value + PageSize > Range) Value = Range - PageSize;
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles changes in the scroll bar's step size.
        /// <summary>
        protected virtual void OnStepSizeChanged(EventArgs e)
        {
            if (StepSizeChanged != null) StepSizeChanged.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles scroll bar value changes.
        /// <summary>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null) ValueChanged.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles click events for plus and minus button controls.
        /// <summary>
        private void ArrowPress(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                if (sender == btnMinus)
                {
                    ScrollUp();
                }
                else if (sender == btnPlus)
                {
                    ScrollDown();
                }
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles movement of the slider button.
        /// <summary>
        private void btnSlider_Move(object sender, MoveEventArgs e)
        {
// Horizontal scroll bar?
            if (orientation == Orientation.Horizontal)
            {
// Slider moves horizontally. Set new position and clamp it to the rail.
                var size = btnMinus.Width + Skin.Layers[strSlider].OffsetX;
                btnSlider.SetPosition(e.Left, 0);
                if (btnSlider.Left < size) btnSlider.SetPosition(size, 0);
                if (btnSlider.Left + btnSlider.Width + size > Width)
                    btnSlider.SetPosition(Width - size - btnSlider.Width, 0);
            }
// Vertical scroll bar.
            else
            {
// Slider moves vertically. Set new position and clamp it to the rail.
                var size = btnMinus.Height + Skin.Layers[strSlider].OffsetY;
                btnSlider.SetPosition(0, e.Top);
                if (btnSlider.Top < size) btnSlider.SetPosition(0, size);
                if (btnSlider.Top + btnSlider.Height + size > Height)
                    btnSlider.SetPosition(0, Height - size - btnSlider.Height);
            }

// Horizontal scroll bar?
            if (orientation == Orientation.Horizontal)
            {
// Slider moves horizontally. Set new position and clamp it to the rail.
                var size = btnMinus.Width + Skin.Layers[strSlider].OffsetX;
                var w = (Width - 2 * size) - btnSlider.Width;
                var px = (Range - PageSize) / (float)w;
                Value = (int)(Math.Ceiling((btnSlider.Left - size) * px));
            }
// Vertical scroll bar.
            else
            {
// Slider moves vertically. Set new position and clamp it to the rail.
                var size = btnMinus.Height + Skin.Layers[strSlider].OffsetY;
                var h = (Height - 2 * size) - btnSlider.Height;
                var px = (Range - PageSize) / (float)h;
                Value = (int)(Math.Ceiling((btnSlider.Top - size) * px));
            }
        }

        /// </summary>
        /// Adjusts the sizes and positions of all buttons based on scroll bar orientation, dimensions, and value.
        /// <summary>
        private void RecalcParams()
        {
// Buttons exist?
            if (btnMinus != null && btnPlus != null && btnSlider != null)
            {
// Horizontal scroll bar?
                if (orientation == Orientation.Horizontal)
                {
// Plus/Minus buttons are square and the same height as the scroll bar control.
                    btnMinus.Width = Height;
                    btnMinus.Height = Height;

                    btnPlus.Width = Height;
                    btnPlus.Height = Height;
                    btnPlus.Left = Width - Height;
                    btnPlus.Top = 0;

                    btnSlider.Movable = true;
// Slider moves horizontally. Set new position and clamp it to the rail.
                    var size = btnMinus.Width + Skin.Layers[strSlider].OffsetX;

// Minimum size of the slider button is a square the same height as the scroll bar.
                    btnSlider.MinimumWidth = Height;
                    var w = (Width - 2 * size);
                    btnSlider.Width = (int)Math.Ceiling((pageSize * w) / (float)range);
                    btnSlider.Height = Height;


// Position the slider button based on its size and the scroll bar value.
                    var px = (Range - PageSize) / (float)(w - btnSlider.Width);
                    var pos = (int)(Math.Ceiling(Value / px));
                    btnSlider.SetPosition(size + pos, 0);
                    if (btnSlider.Left < size) btnSlider.SetPosition(size, 0);
                    if (btnSlider.Left + btnSlider.Width + size > Width)
                        btnSlider.SetPosition(Width - size - btnSlider.Width, 0);
                }
// Vertical scroll bar.
                else
                {
// Plus/Minus buttons are square and the same width as the scroll bar control.
                    btnMinus.Width = Width;
                    btnMinus.Height = Width;

                    btnPlus.Width = Width;
                    btnPlus.Height = Width;
                    btnPlus.Top = Height - Width;

                    btnSlider.Movable = true;
// Slider moves vertically. Set new position and clamp it to the rail.
                    var size = btnMinus.Height + Skin.Layers[strSlider].OffsetY;

// Minimum size of the slider button is a square the same width as the scroll bar.
                    btnSlider.MinimumHeight = Width;
                    var h = (Height - 2 * size);
                    btnSlider.Height = (int)Math.Ceiling((pageSize * h) / (float)range);
                    btnSlider.Width = Width;

// Position the slider button based on its size and the scroll bar value.
                    var px = (Range - PageSize) / (float)(h - btnSlider.Height);
                    var pos = (int)(Math.Ceiling(Value / px));
                    btnSlider.SetPosition(0, size + pos);
                    if (btnSlider.Top < size) btnSlider.SetPosition(0, size);
                    if (btnSlider.Top + btnSlider.Height + size > Height)
                        btnSlider.SetPosition(0, Height - size - btnSlider.Height);
                }
            }
        }
    }
}
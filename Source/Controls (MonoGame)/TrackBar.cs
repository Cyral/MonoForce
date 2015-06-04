using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoForce.Controls
{
    public class TrackBar : Control
    {
        /// <summary>
        /// Gets or sets the amount the track bar's value is altered for large increments.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the value range of the track bar control.
        /// </summary>
        public virtual int Range
        {
            get { return range; }
            set
            {
                if (range != value)
                {
                    range = value;
                    range = value;
                    if (pageSize > range) pageSize = range;
                    RecalcParams();
                    if (!Suspended) OnRangeChanged(new EventArgs());
                }
            }
        }

        /// <summary>
        /// Indicates if the scale for the track bar should be drawn.
        /// </summary>
        public virtual bool Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// Gets or sets the amount the track bar's value is altered for small increments.
        /// </summary>
        public virtual int StepSize
        {
            get { return stepSize; }
            set
            {
                if (stepSize != value)
                {
                    stepSize = value;
                    if (stepSize > range) stepSize = range;
                    if (!Suspended) OnStepSizeChanged(new EventArgs());
                }
            }
        }

        /// <summary>
        /// Gets or sets the current value of the track bar control.
        /// </summary>
        public virtual int Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    if (this.value < 0) this.value = 0;
                    if (this.value > range) this.value = range;
                    Invalidate();
                    if (!Suspended) OnValueChanged(new EventArgs());
                }
            }
        }

        /// <summary>
        /// Slider button that can be dragged to change the value of the track bar control.
        /// </summary>
        private readonly Button btnSlider;

        /// <summary>
        /// Large increment at which the track bar value changes.
        /// </summary>
        private int pageSize = 5;

        /// <summary>
        /// Range the track bar control's value can be within. [0, range]
        /// </summary>
        private int range = 100;

        /// <summary>
        /// Indicates if the scale for the track bar should be drawn.
        /// </summary>
        private bool scale = true;

        /// <summary>
        /// Small increment at which the track bar value changes.
        /// </summary>
        private int stepSize = 1;

        /// <summary>
        /// Current value of the track bar control.
        /// </summary>
        private int value;

        public TrackBar(Manager manager) : base(manager)
        {
            Width = 64;
            Height = 20;
            CanFocus = false;

            btnSlider = new Button(Manager);
            btnSlider.Init();
            btnSlider.Text = "";
            btnSlider.CanFocus = true;
            btnSlider.Parent = this;
            btnSlider.Anchor = Anchors.Left | Anchors.Top | Anchors.Bottom;
            btnSlider.Detached = true;
            btnSlider.Movable = true;
            btnSlider.Move += btnSlider_Move;
            btnSlider.KeyPress += btnSlider_KeyPress;
            btnSlider.GamePadPress += btnSlider_GamePadPress;
        }

        /// <summary>
        /// Initializes the track bar control.
        /// </summary>
        public override void Init()
        {
            base.Init();
            btnSlider.Skin = new SkinControl(Manager.Skin.Controls["TrackBar.Button"]);
        }

        /// <summary>
        /// Occurs when the page size of the track bar changes.
        /// </summary>
        public event EventHandler PageSizeChanged;

        /// <summary>
        /// Occurs when the range of the track bar changes.
        /// </summary>
        public event EventHandler RangeChanged;

        /// <summary>
        /// Occurs when the step size value of the track bar changes.
        /// </summary>
        public event EventHandler StepSizeChanged;

        /// <summary>
        /// Occurs when the value of the track bar changes.
        /// </summary>
        public event EventHandler ValueChanged;

        /// <summary>
        /// Initializes the skin of the track bar control.
        /// </summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls["TrackBar"]);
        }

        public override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            RecalcParams();

            var p = Skin.Layers["Control"];
            var l = Skin.Layers["Scale"];

// Scales down the height of the track bar to allow room for the scale to be drawn.
            var ratio = 0.66f;
            var h = (int)(ratio * rect.Height);
            var t = rect.Top + (Height - h) / 2;

            var px = (value / (float)range);
            var w = (int)Math.Ceiling(px * (rect.Width - p.ContentMargins.Horizontal - btnSlider.Width)) + 2;

            if (w < l.SizingMargins.Vertical) w = l.SizingMargins.Vertical;
            if (w > rect.Width - p.ContentMargins.Horizontal) w = rect.Width - p.ContentMargins.Horizontal;

// Create the region for the track bar scale.
            var r1 = new Rectangle(rect.Left + p.ContentMargins.Left, t + p.ContentMargins.Top, w,
                h - p.ContentMargins.Vertical);

            base.DrawControl(renderer, new Rectangle(rect.Left, t, rect.Width, h), gameTime);
            if (scale) renderer.DrawLayer(this, l, r1);
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles mouse press events for the track bar control.
        /// </summary>
        protected override void OnMousePress(MouseEventArgs e)
        {
            OnMouseDown(e);

            if (e.Button == MouseButton.Left)
            {
                var pos = e.Position.X;

// Step down track bar value.
                if (pos < btnSlider.Left)
                {
                    Value -= pageSize;
                }
// Step up track bar value.
                else if (pos >= btnSlider.Left + btnSlider.Width)
                {
                    Value += pageSize;
                }
            }
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles changes in the track bar's page size.
        /// </summary>
        protected virtual void OnPageSizeChanged(EventArgs e)
        {
            if (PageSizeChanged != null) PageSizeChanged.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles changes in the track bar's range.
        /// </summary>
        protected virtual void OnRangeChanged(EventArgs e)
        {
            if (RangeChanged != null) RangeChanged.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles resizing of the track bar control.
        /// </summary>
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            RecalcParams();
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles changes in the track bar's step size.
        /// </summary>
        protected virtual void OnStepSizeChanged(EventArgs e)
        {
            if (StepSizeChanged != null) StepSizeChanged.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles changes in the track bar's value.
        /// </summary>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null) ValueChanged.Invoke(this, e);
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <summary>
        /// Handles gamepad left/right button presses for the track bar control.
        /// </summary>
        private void btnSlider_GamePadPress(object sender, GamePadEventArgs e)
        {
// Step up/down the track bar's value as needed.
            if (e.Button == GamePadActions.Left || e.Button == GamePadActions.Down) Value -= stepSize;
            if (e.Button == GamePadActions.Right || e.Button == GamePadActions.Up) Value += stepSize;
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <summary>
        /// Handles key press events for the track bar control.
        /// </summary>
        private void btnSlider_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Keys.Left || e.Key == Keys.Down) Value -= stepSize;
            else if (e.Key == Keys.Right || e.Key == Keys.Up) Value += stepSize;
            else if (e.Key == Keys.PageDown) Value -= pageSize;
            else if (e.Key == Keys.PageUp) Value += pageSize;
            else if (e.Key == Keys.Home) Value = 0;
            else if (e.Key == Keys.End) Value = Range;
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <summary>
        /// Handles slider button move events.
        /// </summary>
        private void btnSlider_Move(object sender, MoveEventArgs e)
        {
            var p = Skin.Layers["Control"];
            var size = btnSlider.Width;
            var w = Width - p.ContentMargins.Horizontal - size;
            var pos = e.Left;

// Keep button on track by clamping position values..
            if (pos < p.ContentMargins.Left) pos = p.ContentMargins.Left;
            if (pos > w + p.ContentMargins.Left) pos = w + p.ContentMargins.Left;

// Update button position and value.
            btnSlider.SetPosition(pos, 0);

// Determine the position of the slider button.
            var px = range / (float)w;
            Value = (int)(Math.Ceiling((pos - p.ContentMargins.Left) * px));
        }

        /// <summary>
        /// Updates the position of the slider button.
        /// </summary>
        private void RecalcParams()
        {
// The slider button is created?
            if (btnSlider != null)
            {
// Width of the slider is large enough to fit a glyph on?
                if (btnSlider.Width > 12)
                {
// Reload the glyph and center it on the button.
                    btnSlider.Glyph = new Glyph(Manager.Skin.Images["Shared.Glyph"].Resource);
                    btnSlider.Glyph.SizeMode = SizeMode.Centered;
                }
                else
                {
                    btnSlider.Glyph = null;
                }

                var p = Skin.Layers["Control"];
                btnSlider.Width = (int)(Height * 0.8);
                btnSlider.Height = Height;
                var size = btnSlider.Width;
                var w = Width - p.ContentMargins.Horizontal - size;

// Determine the position of the slider button.
                var px = range / (float)w;
                var pos = p.ContentMargins.Left + (int)(Math.Ceiling(Value / px));

// Keep button on track by clamping position values..
                if (pos < p.ContentMargins.Left) pos = p.ContentMargins.Left;
                if (pos > w + p.ContentMargins.Left) pos = w + p.ContentMargins.Left;

// Update button position and value.
                btnSlider.SetPosition(pos, 0);
            }
        }
    }
}

using Microsoft.Xna.Framework;

namespace MonoForce.Controls
{
    /// <summary>
    /// Indicates how other radio buttons are updated when a radio button is clicked.
    /// </summary>
    public enum RadioButtonMode
    {
        /// <summary>
        /// Clicked radio button will update the checked state of other radio buttons.
        /// </summary>
        Auto,

        /// <summary>
        /// Updating the check state of other radio buttons is a task left to the user.
        /// </summary>
        Manual
    }


    public class RadioButton : CheckBox
    {
        /// <summary>
        /// String used to access the RadioButton's skin control.
        /// </summary>
        private const string skRadioButton = "RadioButton";

        /// <summary>
        /// Gets or sets the way the radio button handles updating other radio button control check states when it is clicked.
        /// </summary>
        public RadioButtonMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        /// <summary>
        /// Indicates if the control will update the check state of other radio button clicks when it's clicked.
        /// </summary>
        private RadioButtonMode mode = RadioButtonMode.Auto;

        public RadioButton(Manager manager) : base(manager)
        {
        }

        /// <summary>
        /// Initializes the radio button control.
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// Initializes the skin of the radio button control.
        /// </summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls[skRadioButton]);
        }

        public override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            base.DrawControl(renderer, rect, gameTime);
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles radio button mouse click events.
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            var ex = (e is MouseEventArgs) ? (MouseEventArgs)e : new MouseEventArgs();

            if (ex.Button == MouseButton.Left || ex.Button == MouseButton.None)
            {
// Should we handled updating other radio button siblings?
                if (mode == RadioButtonMode.Auto)
                {
// Radio button has parent?
                    if (Parent != null)
                    {
                        var lst = Parent.Controls as ControlsList;
// Radio button has siblings?
                        for (var i = 0; i < lst.Count; i++)
                        {
// grouping and uncheck the other radio buttons.
// Assume all radio buttons are part of a single global
                            if (lst[i] is RadioButton)
                            {
// Uncheck RB siblings.
                                (lst[i] as RadioButton).Checked = false;
                            }
                        }
                    }
                    else if (Parent == null && Manager != null)
                    {
                        var lst = Manager.Controls as ControlsList;

// Radio button has siblings?
                        for (var i = 0; i < lst.Count; i++)
                        {
// grouping and uncheck the other radio buttons.
// Assume all radio buttons are part of a single global
                            if (lst[i] is RadioButton)
                            {
// Uncheck RB siblings.
                                (lst[i] as RadioButton).Checked = false;
                            }
                        }
                    }
                }
            }
            base.OnClick(e);
        }
    }
}

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Keys = Microsoft.Xna.Framework.Input.Keys;

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

namespace MonoForce.Controls
{
//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

    /// </summary>
    /// Specifies the type of text box.
    /// <summary>
    public enum TextBoxMode
    {
        /// </summary>
        /// Standard text box control. Single line.
        /// <summary>
        Normal,

        /// </summary>
        /// Masked text box control. Input is replaced by the control's password character.
        /// <summary>
        Password,

        /// </summary>
        /// Multi-line text box control.
        /// <summary>
        Multiline
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
    }

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

    /// </summary>
    /// Represents a text box control.
    /// <summary>
    public class TextBox : ClipControl
    {
//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Not used?
        /// <summary>
        private const string crDefault = "Default";

        /// </summary>
        /// String indicating which cursor resource should be used for this control.
        /// <summary>
        private const string crText = "Text";

        /// </summary>
        /// String for accessing the text box cursor layer.
        /// <summary>
        private const string lrCursor = "Cursor";

        /// </summary>
        /// String for accessing the text box control layer.
        /// <summary>
        private const string lrTextBox = "Control";

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// String for accessing the text box control skin.
        /// <summary>
        private const string skTextBox = "TextBox";

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Indicates if all text should be selected automatically when the text box receives focus.
        /// <summary>
        public virtual bool AutoSelection
        {
            get { return autoSelection; }
            set { autoSelection = value; }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Indicates if the text insertion position is visible or not.
        /// <summary>
        public virtual bool CaretVisible
        {
            get { return caretVisible; }
            set { caretVisible = value; }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Gets or sets the current position of the caret in the text box.
        /// <summary>
        public virtual int CursorPosition
        {
            get { return Pos; }
            set
            {
                Pos = value;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Indicates if the borders of the text box control should be drawn or not.
        /// <summary>
        public virtual bool DrawBorders
        {
            get { return drawBorders; }
            set
            {
                drawBorders = value;
                if (ClientArea != null) ClientArea.Invalidate();
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Gets or sets the current mode of the text box control.
        /// <summary>
        public virtual TextBoxMode Mode
        {
            get { return mode; }
            set
            {
                if (value != TextBoxMode.Multiline)
                {
                    Text = Text.Replace(Separator, "");
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
                mode = value;
// Clear selection.
                selection.Clear();
//if (Manager.UseGuide && Guide.IsVisible) return;

                if (ClientArea != null) ClientArea.Invalidate();
                SetupBars();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Gets or sets the character used to mask input when the text box is in password mode.
        /// <summary>
        public virtual char PasswordChar
        {
            get { return passwordChar; }
            set
            {
                passwordChar = value;
                if (ClientArea != null) ClientArea.Invalidate();
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Indicates if the text box allows user input or not.
        /// <summary>
        public virtual bool ReadOnly
        {
            get { return readOnly; }
            set { readOnly = value; }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Gets or sets the scroll bars the text box should display.
        /// <summary>
        public virtual ScrollBars ScrollBars
        {
            get { return scrollBars; }
            set
            {
                scrollBars = value;
                SetupBars();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Gets all text within the current selection.
        /// <summary>
        public virtual string SelectedText
        {
            get
            {
// Insert text?
                if (selection.IsEmpty)
                {
                    return "";
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Replace selection?
                return Text.Substring(selection.Start, selection.Length);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Gets or sets (from the current value of SelectionStart) the length of the selection.
        /// <summary>
        public virtual int SelectionLength
        {
            get
            {
                return selection.Length;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
            set
            {
                if (value == 0)
                {
                    selection.End = selection.Start;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
                else if (selection.IsEmpty)
                {
                    selection.Start = 0;
                    selection.End = value;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
                else if (!selection.IsEmpty)
                {
                    selection.End = selection.Start + value;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
//if (Manager.UseGuide && Guide.IsVisible) return;

// Delete all selected text?
                if (!selection.IsEmpty)
                {
                    if (selection.Start < 0) selection.Start = 0;
                    if (selection.Start > Text.Length) selection.Start = Text.Length;
                    if (selection.End < 0) selection.End = 0;
                    if (selection.End > Text.Length) selection.End = Text.Length;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
                ClientArea.Invalidate();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Gets or sets the start position of the selection.
        /// <summary>
        public virtual int SelectionStart
        {
            get
            {
// Insert text?
                if (selection.IsEmpty)
                {
                    return Pos;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Replace selection?
                return selection.Start;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
            set
            {
                Pos = value;
                if (Pos < 0) Pos = 0;
                if (Pos > Text.Length) Pos = Text.Length;
                selection.Start = Pos;
                if (selection.End == -1) selection.End = Pos;
                ClientArea.Invalidate();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Gets or sets the contents of the text box control.
        /// <summary>
        public override string Text
        {
            get
            {
                return text;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
            set
            {
                if (wordWrap)
                    value = WrapWords(value, ClientWidth);
//if (Manager.UseGuide && Guide.IsVisible) return;

                if (mode != TextBoxMode.Multiline && value != null)
                {
                    value = value.Replace(Separator, "");
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
//if (Manager.UseGuide && Guide.IsVisible) return;

                text = value;
//if (Manager.UseGuide && Guide.IsVisible) return;

                if (!Suspended) OnTextChanged(new EventArgs());
//if (Manager.UseGuide && Guide.IsVisible) return;

                lines = SplitLines(text);
                if (ClientArea != null) ClientArea.Invalidate();
//if (Manager.UseGuide && Guide.IsVisible) return;

                SetupBars();
                ProcessScrolling();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Indicates if word wrap is enabled in multi-line text box controls.
        /// <summary>
        public virtual bool WordWrap
        {
            get { return wordWrap; }
            set
            {
                wordWrap = value;
                if (ClientArea != null) ClientArea.Invalidate();
                SetupBars();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Returns the text content as a Separator delimited list of strings.
        /// <summary>
        private List<string> Lines
        {
            get
            {
                return lines;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
            set
            {
                lines = value;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

        private int Pos
        {
            get
            {
                return GetPos(PosX, PosY);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
            set
            {
                PosY = GetPosY(value);
                PosX = GetPosX(value);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Gets or sets the X position of the caret on the current line.
        /// <summary>
        private int PosX
        {
            get
            {
                return posx;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
            set
            {
                posx = value;
//if (Manager.UseGuide && Guide.IsVisible) return;

                if (posx < 0) posx = 0;
                if (posx > Lines[PosY].Length) posx = Lines[PosY].Length;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Gets or sets the Y position of the caret in the text box.
        /// <summary>
        private int PosY
        {
            get
            {
                return posy;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
            set
            {
                posy = value;
//if (Manager.UseGuide && Guide.IsVisible) return;

                if (posy < 0) posy = 0;
                if (posy > Lines.Count - 1) posy = Lines.Count - 1;
//if (Manager.UseGuide && Guide.IsVisible) return;

                PosX = PosX;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

        /// </summary>
        /// Horizontal scroll bar of the text box.
        /// <summary>
        private readonly ScrollBar horz;

        /// </summary>
        /// Characted used as the line separator character.
        /// <summary>
        private readonly string Separator = "\n";

        /// </summary>
        /// Vertical scroll bar of the text box.
        /// <summary>
        private readonly ScrollBar vert;

        /// </summary>
        /// Indicates if all text should be selected automatically when the control gains focus.
        /// <summary>
        private bool autoSelection = true;

        /// </summary>
        /// Internal use during text splitting operations.
        /// <summary>
        private string buffer = "";

        /// </summary>
        /// Indicates if the caret is displayed or not.
        /// <summary>
        private bool caretVisible = true;

        /// </summary>
        /// Number of characters that can fit horizontally in the client area.
        /// <summary>
        private int charsDrawn;

        /// </summary>
        /// Indicates if the borders of the text box should be drawn.
        /// <summary>
        private bool drawBorders = true;

        private double flashTime;

        /// </summary>
        /// Font used to draw the control's text.
        /// <summary>
        private SpriteFont font;

        /// </summary>
        /// Text content broken into individual lines.
        /// <summary>
        private List<string> lines = new List<string>();

        /// </summary>
        /// Number of lines of text that can fit vertically in the client area.
        /// <summary>
        private int linesDrawn;

        /// </summary>
        /// Indicates if the text box is a single-line, multi-line, or password text box.
        /// <summary>
        private TextBoxMode mode = TextBoxMode.Normal;

        /// </summary>
        /// Specifies which character will be used to mask input when the text box is in Password mode.
        /// <summary>
        private char passwordChar = '�';

        /// </summary>
        /// X position of the text caret.
        /// <summary>
        private int posx;

        /// </summary>
        /// Y position of the text caret.
        /// <summary>
        private int posy;

        /// </summary>
        /// Indicates if the text box can accept user input or if it is read-only.
        /// <summary>
        private bool readOnly;

        /// </summary>
        /// Specifies which, if any, scroll bars should be displayed in the text box.
        /// <summary>
        private ScrollBars scrollBars = ScrollBars.Both;

        /// </summary>
        /// Currently selected text of the control, specified by starting and ending indexes.
        /// <summary>
        private Selection selection = new Selection(-1, -1);

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Indicates if the cursor should be displayed when hovered. ???
        /// <summary>
        private bool showCursor;

        /// </summary>
        /// Text that is currently visible in the client area.
        /// <summary>
        private string shownText = "";

        /// </summary>
        /// Current text content of the control.
        /// <summary>
        private string text = "";

        /// </summary>
        /// Indicates if word wrap is enabled on multi-line text boxes.
        /// <summary>
        private bool wordWrap;

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <param name="manager">GUI manager for the control.</param>
        /// </summary>
        /// Creates a new TextBox control.
        /// <summary>
        public TextBox(Manager manager)
            : base(manager)
        {
// Cursor layer defined?
            CheckLayer(Skin, lrCursor);
//if (Manager.UseGuide && Guide.IsVisible) return;

            SetDefaultSize(128, 20);
            Lines.Add("");
//if (Manager.UseGuide && Guide.IsVisible) return;

            ClientArea.Draw += ClientArea_Draw;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Create the scroll bars for the text box.
            vert = new ScrollBar(manager, Orientation.Vertical);
            horz = new ScrollBar(manager, Orientation.Horizontal);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Initializes the text box control.
        /// <summary>
        public override void Init()
        {
            base.Init();
//if (Manager.UseGuide && Guide.IsVisible) return;

// Set up the vertical scroll bar.
            vert.Init();
            vert.Range = 1;
            vert.PageSize = 1;
            vert.Value = 0;
            vert.Anchor = Anchors.Top | Anchors.Right | Anchors.Bottom;
            vert.ValueChanged += sb_ValueChanged;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Set up the horizontal scroll bar.
            horz.Init();
            horz.Range = ClientArea.Width;
            horz.PageSize = ClientArea.Width;
            horz.Value = 0;
            horz.Anchor = Anchors.Right | Anchors.Left | Anchors.Bottom;
            horz.ValueChanged += sb_ValueChanged;
//if (Manager.UseGuide && Guide.IsVisible) return;

            horz.Visible = false;
            vert.Visible = false;
//if (Manager.UseGuide && Guide.IsVisible) return;

            Add(vert, false);
            Add(horz, false);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        public virtual void SelectAll()
        {
            if (text.Length > 0)
            {
                selection.Start = 0;
                selection.End = Text.Length;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Initializes the skin of the text box control.
        /// <summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls[skTextBox]);
//if (Manager.UseGuide && Guide.IsVisible) return;

#if (!XBOX && !XBOX_FAKE)
            Cursor = Manager.Skin.Cursors[crText].Resource;
#endif
//if (Manager.UseGuide && Guide.IsVisible) return;

// Get the font used for drawing the text box contents.
            font = (Skin.Layers[lrTextBox].Text != null) ? Skin.Layers[lrTextBox].Text.Font.Resource : null;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// </summary>
        /// Updates the text box cursor state.
        /// <summary>
        protected internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
//if (Manager.UseGuide && Guide.IsVisible) return;

            var sc = showCursor;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Only show the cursor when the text box has focus.
            showCursor = Focused;
//if (Manager.UseGuide && Guide.IsVisible) return;

            if (Focused)
            {
// Update the cursor flash timer and display/hide the cursor every 0.5 seconds.
                flashTime += gameTime.ElapsedGameTime.TotalSeconds;
                showCursor = flashTime < 0.5;
                if (flashTime > 1) flashTime = 0;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Visibility of the cursor has changed? Redraw.
            if (sc != showCursor) ClientArea.Invalidate();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Update the text box margins based on the visibility of the scroll bars.
        /// <summary>
        protected override void AdjustMargins()
        {
// Horizontal scroll bar hidden?
            if (horz != null && !horz.Visible)
            {
                vert.Height = Height - 4;
                ClientMargins = new Margins(ClientMargins.Left, ClientMargins.Top, ClientMargins.Right,
                    Skin.ClientMargins.Bottom);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Replace selection?
            else
            {
                ClientMargins = new Margins(ClientMargins.Left, ClientMargins.Top, ClientMargins.Right,
                    18 + Skin.ClientMargins.Bottom);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
//if (Manager.UseGuide && Guide.IsVisible) return;

// Vertical scroll bar hidden?
            if (vert != null && !vert.Visible)
            {
                horz.Width = Width - 4;
                ClientMargins = new Margins(ClientMargins.Left, ClientMargins.Top, Skin.ClientMargins.Right,
                    ClientMargins.Bottom);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Replace selection?
            else
            {
                ClientMargins = new Margins(ClientMargins.Left, ClientMargins.Top, 18 + Skin.ClientMargins.Right,
                    ClientMargins.Bottom);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
            base.AdjustMargins();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
// Need to draw borders?
            if (drawBorders)
            {
                base.DrawControl(renderer, rect, gameTime);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <param name="e"></param>
        /// </summary>
        /// Handler for when the text box gains focus.
        /// <summary>
        protected override void OnFocusGained(EventArgs e)
        {
// Auto-select all text?
            if (!readOnly && autoSelection)
            {
                SelectAll();
                ClientArea.Invalidate();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
//if (Manager.UseGuide && Guide.IsVisible) return;

            base.OnFocusGained(e);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <param name="e"></param>
        /// </summary>
        /// Handler for when the text box loses focus.
        /// <summary>
        protected override void OnFocusLost(EventArgs e)
        {
// Clear selection.
            selection.Clear();
            ClientArea.Invalidate();
            base.OnFocusLost(e);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <param name="e"></param>
        /// </summary>
        /// Handles key press events for the text box.
        /// <summary>
        protected override void OnKeyPress(KeyEventArgs e)
        {
// Reset the timer used to flash the caret.
            flashTime = 0;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Key event handled already?
            if (!e.Handled)
            {
// Control + A = Select All Text.
                if (e.Key == Keys.A && e.Control && mode != TextBoxMode.Password)
                {
                    SelectAll();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Up arrow key press?
                if (e.Key == Keys.Up)
                {
// Display the on-screen keyboard.
                    e.Handled = true;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Begin selection on Shift + Up if a selection isn't already set.
                    if (e.Shift && selection.IsEmpty && mode != TextBoxMode.Password)
                    {
                        selection.Start = Pos;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        PosY -= 1;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Down arrow key press?
                else if (e.Key == Keys.Down)
                {
// Display the on-screen keyboard.
                    e.Handled = true;
// Begin selection on Shift + Up if a selection isn't already set.
                    if (e.Shift && selection.IsEmpty && mode != TextBoxMode.Password)
                    {
                        selection.Start = Pos;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        PosY += 1;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Delete text if Backspace pressed?
                else if (e.Key == Keys.Back && !readOnly)
                {
// Display the on-screen keyboard.
                    e.Handled = true;
// Delete all selected text?
                    if (!selection.IsEmpty)
                    {
                        Text = Text.Remove(selection.Start, selection.Length);
                        Pos = selection.Start;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Remove a single character?
                    else if (Text.Length > 0 && Pos > 0)
                    {
                        Pos -= 1;
                        Text = Text.Remove(Pos, 1);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Clear selection.
                    selection.Clear();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Delete text if Delete is pressed?
                else if (e.Key == Keys.Delete && !readOnly)
                {
// Display the on-screen keyboard.
                    e.Handled = true;
// Delete all selected text?
                    if (!selection.IsEmpty)
                    {
                        Text = Text.Remove(selection.Start, selection.Length);
                        Pos = selection.Start;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Remove the character after the caret?
                    else if (Pos < Text.Length)
                    {
                        Text = Text.Remove(Pos, 1);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Clear selection.
                    selection.Clear();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Left arrow key pressed?
                else if (e.Key == Keys.Left)
                {
// Display the on-screen keyboard.
                    e.Handled = true;
// Begin selection on Shift + Up if a selection isn't already set.
                    if (e.Shift && selection.IsEmpty && mode != TextBoxMode.Password)
                    {
                        selection.Start = Pos;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        Pos -= 1;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Move the caret to the start of the previous word on Control + Left.
                    if (e.Control)
                    {
                        Pos = FindPrevWord(shownText);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Right arrow key pressed?
                else if (e.Key == Keys.Right)
                {
// Display the on-screen keyboard.
                    e.Handled = true;
// Begin selection on Shift + Up if a selection isn't already set.
                    if (e.Shift && selection.IsEmpty && mode != TextBoxMode.Password)
                    {
                        selection.Start = Pos;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        Pos += 1;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Move the caret to the start of the previous word on Control + Left.
                    if (e.Control)
                    {
                        Pos = FindNextWord(shownText);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Home key pressed?
                else if (e.Key == Keys.Home)
                {
// Display the on-screen keyboard.
                    e.Handled = true;
// Begin selection on Shift + Up if a selection isn't already set.
                    if (e.Shift && selection.IsEmpty && mode != TextBoxMode.Password)
                    {
                        selection.Start = Pos;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        PosX = 0;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Move the caret to the start of the previous word on Control + Left.
                    if (e.Control)
                    {
                        Pos = 0;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// End key pressed?
                else if (e.Key == Keys.End)
                {
// Display the on-screen keyboard.
                    e.Handled = true;
// Begin selection on Shift + Up if a selection isn't already set.
                    if (e.Shift && selection.IsEmpty && mode != TextBoxMode.Password)
                    {
                        selection.Start = Pos;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        PosX = Lines[PosY].Length;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Move the caret to the start of the previous word on Control + Left.
                    if (e.Control)
                    {
                        Pos = Text.Length;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Page Up key pressed?
                else if (e.Key == Keys.PageUp)
                {
// Display the on-screen keyboard.
                    e.Handled = true;
// Begin selection on Shift + Up if a selection isn't already set.
                    if (e.Shift && selection.IsEmpty && mode != TextBoxMode.Password)
                    {
                        selection.Start = Pos;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        PosY -= linesDrawn;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Page Down key pressed?
                else if (e.Key == Keys.PageDown)
                {
// Display the on-screen keyboard.
                    e.Handled = true;
// Begin selection on Shift + Up if a selection isn't already set.
                    if (e.Shift && selection.IsEmpty && mode != TextBoxMode.Password)
                    {
                        selection.Start = Pos;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        PosY += linesDrawn;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Insert new line on Enter key press?
                else if (e.Key == Keys.Enter && mode == TextBoxMode.Multiline && !readOnly)
                {
// Display the on-screen keyboard.
                    e.Handled = true;
                    Text = Text.Insert(Pos, Separator);
                    PosX = 0;
                    PosY += 1;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Tab key pressed?
                else if (e.Key == Keys.Tab)
                {
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Handle all other key press events.
                else if (!readOnly && !e.Control)
                {
                    var c = Manager.KeyboardLayout.GetKey(e);
// Insert text?
                    if (selection.IsEmpty)
                    {
                        Text = Text.Insert(Pos, c);
                        if (c != "") PosX += 1;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Replace selection?
                    else
                    {
                        if (Text.Length > 0)
                        {
                            Text = Text.Remove(selection.Start, selection.Length);
                            Text = Text.Insert(selection.Start, c);
                            Pos = selection.Start + 1;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                        }
// Clear selection.
                        selection.Clear();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
//if (Manager.UseGuide && Guide.IsVisible) return;

// Update the end of selection?
                if (e.Shift && !selection.IsEmpty)
                {
                    selection.End = Pos;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
//if (Manager.UseGuide && Guide.IsVisible) return;

// Copy selected text to clipboard on Control + C pressed and running on Windows.
                if (e.Control && e.Key == Keys.C && mode != TextBoxMode.Password)
                {
#if (!XBOX && !XBOX_FAKE)
                    Clipboard.Clear();
                    if (mode != TextBoxMode.Password && !selection.IsEmpty)
                    {
                        Clipboard.SetText((Text.Substring(selection.Start, selection.Length)).Replace("\n",
                            Environment.NewLine));
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
#endif
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Paste from clipboard on Control + V if running on Windows.
                else if (e.Control && e.Key == Keys.V && !readOnly && mode != TextBoxMode.Password)
                {
#if (!XBOX && !XBOX_FAKE)
                    var t = Clipboard.GetText().Replace(Environment.NewLine, "\n");
// Insert text?
                    if (selection.IsEmpty)
                    {
                        Text = Text.Insert(Pos, t);
                        Pos = Pos + t.Length;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Replace selection?
                    else
                    {
                        Text = Text.Remove(selection.Start, selection.Length);
                        Text = Text.Insert(selection.Start, t);
                        PosX = selection.Start + t.Length;
// Clear selection.
                        selection.Clear();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
#endif
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
//if (Manager.UseGuide && Guide.IsVisible) return;

// Clear selection?
                if ((!e.Shift && !e.Control) || Text.Length <= 0)
                {
// Clear selection.
                    selection.Clear();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
//if (Manager.UseGuide && Guide.IsVisible) return;

// Show guide on Control + Down.
                if (e.Control && e.Key == Keys.Down)
                {
// Display the on-screen keyboard.
                    e.Handled = true;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Reset the timer used to flash the caret.
                flashTime = 0;
                if (ClientArea != null) ClientArea.Invalidate();
//if (Manager.UseGuide && Guide.IsVisible) return;

                DeterminePages();
                ProcessScrolling();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
            base.OnKeyPress(e);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <param name="e"></param>
        /// </summary>
        /// Handles mouse button down events for the text box.
        /// <summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
//if (Manager.UseGuide && Guide.IsVisible) return;

// Reset the timer used to flash the caret.
            flashTime = 0;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Reposition caret.
            Pos = CharAtPos(e.Position);
// Clear selection.
            selection.Clear();
//if (Manager.UseGuide && Guide.IsVisible) return;

// Update selection?
            if (e.Button == MouseButton.Left && caretVisible && mode != TextBoxMode.Password)
            {
                selection.Start = Pos;
                selection.End = Pos;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
            ClientArea.Invalidate();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <param name="e"></param>
        /// </summary>
        /// Handles mouse move events for the text box.
        /// <summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
//if (Manager.UseGuide && Guide.IsVisible) return;

// Mouse move + Left button down = Update selection.
            if (e.Button == MouseButton.Left && !selection.IsEmpty && mode != TextBoxMode.Password &&
                selection.Length < Text.Length)
            {
                var pos = CharAtPos(e.Position);
                selection.End = CharAtPos(e.Position);
                Pos = pos;
//if (Manager.UseGuide && Guide.IsVisible) return;

                ClientArea.Invalidate();
//if (Manager.UseGuide && Guide.IsVisible) return;

                ProcessScrolling();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        protected override void OnMouseScroll(MouseEventArgs e)
        {
            if (Mode != TextBoxMode.Multiline)
            {
                base.OnMouseScroll(e);
                return;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
//if (Manager.UseGuide && Guide.IsVisible) return;

            if (e.ScrollDirection == MouseScrollDirection.Down)
                vert.ScrollDown();
// Replace selection?
            else
                vert.ScrollUp();
//if (Manager.UseGuide && Guide.IsVisible) return;

            base.OnMouseScroll(e);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <param name="e"></param>
        /// </summary>
        /// Handles mouse up events for the text box.
        /// <summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
//if (Manager.UseGuide && Guide.IsVisible) return;

// Clear selection if the text box receives a left button click.
            if (e.Button == MouseButton.Left && !selection.IsEmpty && mode != TextBoxMode.Password)
            {
                if (selection.Length == 0) selection.Clear();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <param name="e"></param>
        /// </summary>
        /// Handles resize events for the text box.
        /// <summary>
        protected override void OnResize(ResizeEventArgs e)
        {
// Clear text selection and update scroll bars.
            base.OnResize(e);
// Clear selection.
            selection.Clear();
            SetupBars();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <returns>Returns the cursor position that corresponds to the point received.</returns>
        /// <param name="pos">Point to find the text position of.</param>
        /// </summary>
        /// box that is closest to the specified position.
        /// Given a point (such as mouse position), this determines the position in the text
        /// <summary>
        private int CharAtPos(Point pos)
        {
            var x = pos.X;
            var y = pos.Y;
            var px = 0;
            var py = 0;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Adjust rectangle to account for current vertical scroll bar value?
            if (mode == TextBoxMode.Multiline)
            {
// Get the line index under the specified point.
                py = vert.Value + (y - ClientTop) / font.LineSpacing;
                if (py < 0) py = 0;
                if (py >= Lines.Count) py = Lines.Count - 1;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Replace selection?
            else
            {
// Otherwise, line index is zero.
                py = 0;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
//if (Manager.UseGuide && Guide.IsVisible) return;

            var str = mode == TextBoxMode.Multiline ? Lines[py] : shownText;
//if (Manager.UseGuide && Guide.IsVisible) return;

            if (str != null && str != "")
            {
// Determine X position within the current line.
                for (var i = 1; i <= Lines[py].Length; i++)
                {
                    var v = font.MeasureString(str.Substring(0, i)) - (font.MeasureString(str[i - 1].ToString()) / 3);
                    if (x <= (ClientLeft + (int)v.X) - horz.Value)
                    {
                        px = i - 1;
                        break;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
                if (x >
                    ClientLeft + ((int)font.MeasureString(str).X) - horz.Value -
                    (font.MeasureString(str[str.Length - 1].ToString()).X / 3)) px = str.Length;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
//if (Manager.UseGuide && Guide.IsVisible) return;

            return GetPos(px, py);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles drawing the client area of the text box control.
        /// <summary>
        private void ClientArea_Draw(object sender, DrawEventArgs e)
        {
// Grab the text box control's skin information.
            var layer = Skin.Layers[lrTextBox];
            var col = Skin.Layers[lrTextBox].Text.Colors.Enabled;
            var cursor = Skin.Layers[lrCursor];
// Multi-line text boxes are aligned top-left; other types have their line centered vertically.
            var al = mode == TextBoxMode.Multiline ? Alignment.TopLeft : Alignment.MiddleLeft;
            var renderer = e.Renderer;
            var r = e.Rectangle;
// Text box has a selected text to consider?
            var drawsel = !selection.IsEmpty;
            var tmpText = "";
//if (Manager.UseGuide && Guide.IsVisible) return;

// Get the font used for drawing the text box contents.
            font = (Skin.Layers[lrTextBox].Text != null) ? Skin.Layers[lrTextBox].Text.Font.Resource : null;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Control has text to draw and we have a font to draw it with?
            if (Text != null && font != null)
            {
                DeterminePages();
//if (Manager.UseGuide && Guide.IsVisible) return;

// Adjust rectangle to account for current vertical scroll bar value?
                if (mode == TextBoxMode.Multiline)
                {
                    shownText = Text;
                    tmpText = Lines[PosY];
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
                else if (mode == TextBoxMode.Password)
                {
// Mask the text using the password character.
                    shownText = "";
                    for (var i = 0; i < Text.Length; i++)
                    {
                        shownText = shownText + passwordChar;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
                    tmpText = shownText;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Replace selection?
                else
                {
                    shownText = Text;
                    tmpText = Lines[PosY];
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
//if (Manager.UseGuide && Guide.IsVisible) return;

// Text color defined and control not disabled.
                if (TextColor != UndefinedColor && ControlState != ControlState.Disabled)
                {
// Use the control's text color value.
                    col = TextColor;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
//if (Manager.UseGuide && Guide.IsVisible) return;

                if (mode != TextBoxMode.Multiline)
                {
                    linesDrawn = 0;
                    vert.Value = 0;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
//if (Manager.UseGuide && Guide.IsVisible) return;

// Is there a selection to draw?
                if (drawsel)
                {
                    DrawSelection(e.Renderer, r);
/*
renderer.End();
renderer.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);
renderer.SpriteBatch.GraphicsDevice.RenderState.SeparateAlphaBlendEnabled = true;
renderer.SpriteBatch.GraphicsDevice.RenderState.SourceBlend = Blend.DestinationColor;
renderer.SpriteBatch.GraphicsDevice.RenderState.DestinationBlend = Blend.SourceColor;
renderer.SpriteBatch.GraphicsDevice.RenderState.BlendFunction = BlendFunction.Subtract;
//renderer.SpriteBatch.GraphicsDevice.RenderState.AlphaDestinationBlend = Blend.DestinationAlpha;
//renderer.SpriteBatch.GraphicsDevice.RenderState.AlphaSourceBlend = Blend.One;
//renderer.SpriteBatch.GraphicsDevice.RenderState.AlphaFunction = CompareFunction.Equal;
*/
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
//if (Manager.UseGuide && Guide.IsVisible) return;

// Get height of a single line of text.
                var sizey = font.LineSpacing;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Need to draw the caret?
                if (showCursor && caretVisible)
                {
                    var size = Vector2.Zero;
                    if (PosX > 0 && PosX <= tmpText.Length)
                    {
                        size = font.MeasureString(tmpText.Substring(0, PosX));
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
                    if (size.Y == 0)
                    {
                        size = font.MeasureString(" ");
                        size.X = 0;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
//if (Manager.UseGuide && Guide.IsVisible) return;

                    var m = r.Height - font.LineSpacing;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Create the rectangle where the cursor should be drawn.
                    var rc = new Rectangle(r.Left - horz.Value + (int)size.X, r.Top + m / 2, cursor.Width,
                        font.LineSpacing);
//if (Manager.UseGuide && Guide.IsVisible) return;

// Adjust rectangle to account for current vertical scroll bar value?
                    if (mode == TextBoxMode.Multiline)
                    {
                        rc = new Rectangle(r.Left + (int)size.X - horz.Value,
                            r.Top + (PosY - vert.Value) * font.LineSpacing, cursor.Width, font.LineSpacing);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Draw the cursor in the text box.
                    cursor.Alignment = al;
                    renderer.DrawLayer(cursor, rc, col, 0);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
//if (Manager.UseGuide && Guide.IsVisible) return;

// Draw all visible text.
                for (var i = 0; i < linesDrawn + 1; i++)
                {
                    var ii = i + vert.Value;
                    if (ii >= Lines.Count || ii < 0) break;
//if (Manager.UseGuide && Guide.IsVisible) return;

                    if (Lines[ii] != "")
                    {
// Adjust rectangle to account for current vertical scroll bar value?
                        if (mode == TextBoxMode.Multiline)
                        {
                            renderer.DrawString(font, Lines[ii], r.Left - horz.Value, r.Top + (i * sizey), col);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                        }
// Replace selection?
                        else
                        {
                            var rx = new Rectangle(r.Left - horz.Value, r.Top, r.Width, r.Height);
                            renderer.DrawString(font, shownText, rx, col, al, false);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                        }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
/*  if (drawsel)
{
renderer.End();
renderer.Begin(BlendingMode.Premultiplied);
}*/
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// client area of the text box.
        /// Updates the number of lines and characters drawn based on the current dimensions of the
        /// <summary>
        private void DeterminePages()
        {
            if (ClientArea != null)
            {
// Get height of a single line of text.
                var sizey = font.LineSpacing;
// Update the number of lines that can fit within the current height of the text area.
                linesDrawn = ClientArea.Height / sizey;
// Can't draw more lines than there actually is.
                if (linesDrawn > Lines.Count) linesDrawn = Lines.Count;
//if (Manager.UseGuide && Guide.IsVisible) return;

// NOTE: How exactly does this work out?
// Update the number of characters drawn.
                charsDrawn = ClientArea.Width - 1;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <param name="rect">Region where the selection overlay should be drawn.</param>
        /// <param name="renderer">Render management object.</param>
        /// </summary>
        /// Draws the text box's selection overlay to highlight selected text.
        /// <summary>
        private void DrawSelection(Renderer renderer, Rectangle rect)
        {
// Delete all selected text?
            if (!selection.IsEmpty)
            {
                var s = selection.Start;
                var e = selection.End;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Get selection's starting line index, ending line index, starting column index, and ending column index.
                var sl = GetPosY(s);
                var el = GetPosY(e);
                var sc = GetPosX(s);
                var ec = GetPosX(e);
//if (Manager.UseGuide && Guide.IsVisible) return;

// Selection height is the height of a single line of text.
                var hgt = font.LineSpacing;
//if (Manager.UseGuide && Guide.IsVisible) return;

                var start = sl;
                var end = el;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Adjust start and end positions to account for vertical scroll values.
                if (start < vert.Value) start = vert.Value;
                if (end > vert.Value + linesDrawn) end = vert.Value + linesDrawn;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Draw each line of the selection.
                for (var i = start; i <= end; i++)
                {
                    var r = Rectangle.Empty;
//if (Manager.UseGuide && Guide.IsVisible) return;

                    if (mode == TextBoxMode.Normal)
                    {
                        var m = ClientArea.Height - font.LineSpacing;
                        r = new Rectangle(
                            rect.Left - horz.Value + (int)font.MeasureString(Lines[i].Substring(0, sc)).X,
                            rect.Top + m / 2,
                            (int)font.MeasureString(Lines[i].Substring(0, ec + 0)).X -
                            (int)font.MeasureString(Lines[i].Substring(0, sc)).X, hgt);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
                    else if (sl == el)
                    {
                        r = new Rectangle(
                            rect.Left - horz.Value + (int)font.MeasureString(Lines[i].Substring(0, sc)).X,
                            rect.Top + (i - vert.Value) * hgt,
                            (int)font.MeasureString(Lines[i].Substring(0, ec + 0)).X -
                            (int)font.MeasureString(Lines[i].Substring(0, sc)).X, hgt);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
// Replace selection?
                    else
                    {
                        if (i == sl)
                            r =
                                new Rectangle(
                                    rect.Left - horz.Value + (int)font.MeasureString(Lines[i].Substring(0, sc)).X,
                                    rect.Top + (i - vert.Value) * hgt,
                                    (int)font.MeasureString(Lines[i]).X -
                                    (int)font.MeasureString(Lines[i].Substring(0, sc)).X, hgt);
                        else if (i == el)
                            r = new Rectangle(rect.Left - horz.Value, rect.Top + (i - vert.Value) * hgt,
                                (int)font.MeasureString(Lines[i].Substring(0, ec + 0)).X, hgt);
                        else
                            r = new Rectangle(rect.Left - horz.Value, rect.Top + (i - vert.Value) * hgt,
                                (int)font.MeasureString(Lines[i]).X, hgt);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                    }
//if (Manager.UseGuide && Guide.IsVisible) return;

                    renderer.Draw(Manager.Skin.Images["Control"].Resource, r,
                        Color.FromNonPremultiplied(160, 160, 160, 128));
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <returns>
        /// Returns the index of the start of the next word or the last valid index if the cursor has reached the end
        /// point.
        /// </returns>
        /// <param name="text">Text content to search.</param>
        /// </summary>
        /// From the current cursor position, this finds the index of the start of the word ahead of it.
        /// <summary>
        private int FindNextWord(string text)
        {
            var space = false;
//if (Manager.UseGuide && Guide.IsVisible) return;

            for (var i = Pos; i < text.Length - 1; i++)
            {
                if (!char.IsLetterOrDigit(text[i]))
                {
                    space = true;
                    continue;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// First non-whitespace character after the first encountered space is the start of the next word.
                if (space && char.IsLetterOrDigit(text[i]))
                {
                    return i;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
//if (Manager.UseGuide && Guide.IsVisible) return;

// Reached the end of the text.
            return text.Length;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <returns>Returns the index of the start of the previous word or zero if the cursor has reached the starting point.</returns>
        /// <param name="text">Text content to search.</param>
        /// </summary>
        /// word, or the start of the previous word if the cursor is at the start of a word..
        /// This will be the start of the current word, if the cursor is positioned in the middle of a
        /// From the current cursor position, this finds the index of the start of the word behind it.
        /// <summary>
        private int FindPrevWord(string text)
        {
            var letter = false;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Get current position of the cursor.
            var p = Pos - 1;
            if (p < 0) p = 0;
            if (p >= text.Length) p = text.Length - 1;
//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

// Search backwards from the current position
            for (var i = p; i >= 0; i--)
            {
// of the word we want to find the start of.
// First non whitespace character from current position indicates start
                if (char.IsLetterOrDigit(text[i]))
                {
                    letter = true;
                    continue;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// of the word behind the cursor's current position.
// First white space character indicates that we are at the beginning
                if (letter && !char.IsLetterOrDigit(text[i]))
                {
                    return i + 1;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
//if (Manager.UseGuide && Guide.IsVisible) return;

// Reached the beginning of the text string.
            return 0;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <returns>Returns the number of characters, from the start of the string, that will fit in the width specified.</returns>
        /// <param name="width">Width available for text placement.</param>
        /// <param name="text">Text string to fit.</param>
        /// </summary>
        /// Returns the number of characters of the specified text that will fit within the specified width.
        /// <summary>
        private int GetFitChars(string text, int width)
        {
// All characters will fit unless proven otherwise.
            var ret = text.Length;
            var size = 0;
//if (Manager.UseGuide && Guide.IsVisible) return;

            for (var i = 0; i < text.Length; i++)
            {
// Get the width of the current substring.
                size = (int)font.MeasureString(text.Substring(0, i)).X;
// Too large? Update character count and exit.
                if (size > width)
                {
                    ret = i;
                    break;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
//if (Manager.UseGuide && Guide.IsVisible) return;

            return ret;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <returns>The longest line in the text box.</returns>
        /// </summary>
        /// Gets the line of the text box with the greatest length.
        /// <summary>
        private string GetMaxLine()
        {
            var max = 0;
            var x = 0;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Determine which line the cursor is on.
            for (var i = 0; i < Lines.Count; i++)
            {
                if (Lines[i].Length > max)
                {
                    max = Lines[i].Length;
                    x = i;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
            return Lines.Count > 0 ? Lines[x] : "";
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <returns>Returns the cursor position for the specified location.</returns>
        /// <param name="y">Line index.</param>
        /// <param name="x">Column index.</param>
        /// </summary>
        /// that matches the specified location.
        /// Given the column (x) and line (y) indexes, this returns the cursor position
        /// <summary>
        private int GetPos(int x, int y)
        {
            var p = 0;
//if (Manager.UseGuide && Guide.IsVisible) return;

            for (var i = 0; i < y; i++)
            {
                p += Lines[i].Length + Separator.Length;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
            p += x;
//if (Manager.UseGuide && Guide.IsVisible) return;

            return p;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <returns>Returns the index of the column at the specified cursor position.</returns>
        /// <param name="pos">Position of the cursor in the text.</param>
        /// </summary>
        /// Gets the column index of the specified position.
        /// <summary>
        private int GetPosX(int pos)
        {
// Cursor is at the end of the text content?
            if (pos >= Text.Length) return Lines[Lines.Count - 1].Length;
//if (Manager.UseGuide && Guide.IsVisible) return;

            var p = pos;
// Determine which line the cursor is on.
            for (var i = 0; i < Lines.Count; i++)
            {
// in the current line. Return the current line index.
// If p - line length is less than zero, the cursor is located somewhere
                p -= Lines[i].Length + Separator.Length;
                if (p < 0)
                {
                    p = p + Lines[i].Length + Separator.Length;
                    return p;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Reached the beginning of the text string.
            return 0;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <returns>Returns the index of the line where the cursor is positioned.</returns>
        /// <param name="pos">Cursor position in text.</param>
        /// </summary>
        /// Gets the line index where the cursor is currently positioned.
        /// <summary>
        private int GetPosY(int pos)
        {
// Cursor is past the last line of text?
            if (pos >= Text.Length) return Lines.Count - 1;
//if (Manager.UseGuide && Guide.IsVisible) return;

            var p = pos;
// Determine which line the cursor is on.
            for (var i = 0; i < Lines.Count; i++)
            {
// in the current line. Return the current line index.
// If p - line length is less than zero, the cursor is located somewhere
                p -= Lines[i].Length + Separator.Length;
                if (p < 0)
                {
                    p = p + Lines[i].Length + Separator.Length;
                    return i;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Reached the beginning of the text string.
            return 0;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <returns>Returns the width of the specified number of characters in the supplied text.</returns>
        /// <param name="count">Number of characters from the start of the string to measure.</param>
        /// <param name="text">String to measure the width of.</param>
        /// </summary>
        /// Measures the width of the specified text or a sub-string of the text.
        /// <summary>
        private int GetStringWidth(string text, int count)
        {
            if (count > text.Length) count = text.Length;
            return (int)font.MeasureString(text.Substring(0, count)).X;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// of the text box and the current cursor position within the text box.
        /// Updates scroll bar values and page sizes based on the dimensions of the client area
        /// <summary>
        private void ProcessScrolling()
        {
            if (vert != null && horz != null)
            {
// Update page size values based on dimensions of client area.
                vert.PageSize = linesDrawn;
                horz.PageSize = charsDrawn;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Clamp horizontal page value in range.
                if (horz.PageSize > horz.Range) horz.PageSize = horz.Range;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Update vertical scroll bar value so the current insertion position is visible.
                if (PosY >= vert.Value + vert.PageSize)
                {
                    vert.Value = (PosY + 1) - vert.PageSize;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
                else if (PosY < vert.Value)
                {
                    vert.Value = PosY;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
//if (Manager.UseGuide && Guide.IsVisible) return;

// Update horizontal scroll bar value so the current insertion position is visible.
                if (GetStringWidth(Lines[PosY], PosX) >= horz.Value + horz.PageSize)
                {
                    horz.Value = (GetStringWidth(Lines[PosY], PosX) + 1) - horz.PageSize;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
                else if (GetStringWidth(Lines[PosY], PosX) < horz.Value)
                {
                    horz.Value = GetStringWidth(Lines[PosY], PosX) - horz.PageSize;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles scroll events for the text box.
        /// <summary>
        private void sb_ValueChanged(object sender, EventArgs e)
        {
            ClientArea.Invalidate();
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// Updates scroll bar settings based on dimensions of the client area and text content.
        /// <summary>
        private void SetupBars()
        {
            DeterminePages();
//if (Manager.UseGuide && Guide.IsVisible) return;

            if (vert != null) vert.Range = Lines.Count;
            if (horz != null)
            {
                horz.Range = (int)font.MeasureString(GetMaxLine()).X;
                if (horz.Range == 0) horz.Range = ClientArea.Width;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
//if (Manager.UseGuide && Guide.IsVisible) return;

            if (vert != null)
            {
                vert.Left = Width - 16 - 2;
                vert.Top = 2;
                vert.Height = Height - 4 - 16;
//if (Manager.UseGuide && Guide.IsVisible) return;

                if (Height < 50 || (scrollBars != ScrollBars.Both && scrollBars != ScrollBars.Vertical))
                    vert.Visible = false;
                else if ((scrollBars == ScrollBars.Vertical || scrollBars == ScrollBars.Both) &&
                         mode == TextBoxMode.Multiline) vert.Visible = true;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
            if (horz != null)
            {
                horz.Left = 2;
                horz.Top = Height - 16 - 2;
                horz.Width = Width - 4 - 16;
//if (Manager.UseGuide && Guide.IsVisible) return;

                if (Width < 50 || wordWrap || (scrollBars != ScrollBars.Both && scrollBars != ScrollBars.Horizontal))
                    horz.Visible = false;
                else if ((scrollBars == ScrollBars.Horizontal || scrollBars == ScrollBars.Both) &&
                         mode == TextBoxMode.Multiline && !wordWrap) horz.Visible = true;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
//if (Manager.UseGuide && Guide.IsVisible) return;

            AdjustMargins();
//if (Manager.UseGuide && Guide.IsVisible) return;

            if (vert != null) vert.PageSize = linesDrawn;
            if (horz != null) horz.PageSize = charsDrawn;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <returns>List of strings delimited by the text box separator character.</returns>
        /// <param name="text">Text to split.</param>
        /// </summary>
        /// Splits the specified text into a list of strings based on the text box separator character.
        /// <summary>
        private List<string> SplitLines(string text)
        {
            if (buffer != text)
            {
                buffer = text;
                var list = new List<string>();
                var s = text.Split(Separator[0]);
                list.Clear();
//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

                list.AddRange(s);
//if (Manager.UseGuide && Guide.IsVisible) return;

                if (posy < 0) posy = 0;
                if (posy > list.Count - 1) posy = list.Count - 1;
//if (Manager.UseGuide && Guide.IsVisible) return;

                if (posx < 0) posx = 0;
                if (posx > list[PosY].Length) posx = list[PosY].Length;
//if (Manager.UseGuide && Guide.IsVisible) return;

                return list;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
            return lines;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// <returns>Returns the word wrapped string.</returns>
        /// <param name="size">Width of the text box the text will be wrapped in.</param>
        /// <param name="text">Text content to word wrap.</param>
        /// </summary>
        /// Breaks up text content so that all lines fit within the width of the client area of the text box.
        /// <summary>
        private string WrapWords(string text, int size)
        {
            var ret = "";
            var line = "";
//if (Manager.UseGuide && Guide.IsVisible) return;

// Split text at each space and break into a word array.
            var words = text.Replace("\v", "").Split(" ".ToCharArray());
//if (Manager.UseGuide && Guide.IsVisible) return;

// the width of the text box client area.
// Concatenate words until it has been reformed into lines that fit
            for (var i = 0; i < words.Length; i++)
            {
                if (font.MeasureString(line + words[i]).X > size)
                {
                    ret += line + "\n";
                    line = words[i] + " ";
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Replace selection?
                else
                {
                    line += words[i] + " ";
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
//if (Manager.UseGuide && Guide.IsVisible) return;

// Append last line.
            ret += line;
//if (Manager.UseGuide && Guide.IsVisible) return;

// Remove last space and return the new formatted string.
            return ret.Remove(ret.Length - 1, 1);
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
        }

        #region Nested type: Struct

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;

        /// </summary>
        /// <summary>
        private struct Selection
        {
//if (Manager.UseGuide && Guide.IsVisible) return;

            public int End
            {
                get
                {
                    if (end < start && start != -1 && end != -1) return start;
                    return end;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
                set
                {
                    end = value;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }

//if (Manager.UseGuide && Guide.IsVisible) return;

            public bool IsEmpty
            {
                get { return Start == -1 && End == -1; }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }

//if (Manager.UseGuide && Guide.IsVisible) return;

            public int Length
            {
                get { return IsEmpty ? 0 : (End - Start); }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }

//if (Manager.UseGuide && Guide.IsVisible) return;

            public int Start
            {
                get
                {
                    if (start > end && start != -1 && end != -1) return end;
                    return start;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
                set
                {
                    start = value;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
                }
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }

            private int end;
            private int start;
//if (Manager.UseGuide && Guide.IsVisible) return;

            public Selection(int start, int end)
            {
                this.start = start;
                this.end = end;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }

//if (Manager.UseGuide && Guide.IsVisible) return;

            public void Clear()
            {
                Start = -1;
                End = -1;
// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
            }
        }

        #endregion
    }

//if (Manager.UseGuide && Guide.IsVisible) return;

//if (Manager.UseGuide && Guide.IsVisible) return;
// Guide visible?

// Guide.BeginShowKeyboardInput(pi, "Enter Text", "", Text, GetText, pi.ToString());
//if (Manager.UseGuide && !Guide.IsVisible)
}
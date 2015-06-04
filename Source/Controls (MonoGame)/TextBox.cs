using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MonoForce.Controls
{
    /// <summary>
    /// Specifies the type of text box.
    /// </summary>
    public enum TextBoxMode
    {
        /// <summary>
        /// Standard text box control. Single line.
        /// </summary>
        Normal,

        /// <summary>
        /// Masked text box control. Input is replaced by the control's password character.
        /// </summary>
        Password,

        /// <summary>
        /// Multi-line text box control.
        /// </summary>
        Multiline
    }


    /// <summary>
    /// Represents a text box control.
    /// </summary>
    public class TextBox : ClipControl
    {
        /// <summary>
        /// Not used?
        /// </summary>
        private const string crDefault = "Default";

        /// <summary>
        /// String indicating which cursor resource should be used for this control.
        /// </summary>
        private const string crText = "Text";

        /// <summary>
        /// String for accessing the text box cursor layer.
        /// </summary>
        private const string lrCursor = "Cursor";

        /// <summary>
        /// String for accessing the text box control layer.
        /// </summary>
        private const string lrTextBox = "Control";

        /// <summary>
        /// String for accessing the text box control skin.
        /// </summary>
        private const string skTextBox = "TextBox";

        /// <summary>
        /// Indicates if all text should be selected automatically when the text box receives focus.
        /// </summary>
        public virtual bool AutoSelection
        {
            get { return autoSelection; }
            set { autoSelection = value; }
        }

        /// <summary>
        /// Indicates if the text insertion position is visible or not.
        /// </summary>
        public virtual bool CaretVisible
        {
            get { return caretVisible; }
            set { caretVisible = value; }
        }

        /// <summary>
        /// Gets or sets the current position of the caret in the text box.
        /// </summary>
        public virtual int CursorPosition
        {
            get { return Pos; }
            set { Pos = value; }
        }

        /// <summary>
        /// Indicates if the borders of the text box control should be drawn or not.
        /// </summary>
        public virtual bool DrawBorders
        {
            get { return drawBorders; }
            set
            {
                drawBorders = value;
                if (ClientArea != null) ClientArea.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the current mode of the text box control.
        /// </summary>
        public virtual TextBoxMode Mode
        {
            get { return mode; }
            set
            {
                if (value != TextBoxMode.Multiline)
                {
                    Text = Text.Replace(Separator, "");
                }
                mode = value;
// Clear selection.
                selection.Clear();


                if (ClientArea != null) ClientArea.Invalidate();
                SetupBars();
            }
        }

        /// <summary>
        /// Gets or sets the character used to mask input when the text box is in password mode.
        /// </summary>
        public virtual char PasswordChar
        {
            get { return passwordChar; }
            set
            {
                passwordChar = value;
                if (ClientArea != null) ClientArea.Invalidate();
            }
        }

        public string Placeholder { get; set; } = "";

        public Color PlaceholderColor { get; set; } = Color.LightGray;

        /// <summary>
        /// Indicates if the text box allows user input or not.
        /// </summary>
        public virtual bool ReadOnly
        {
            get { return readOnly; }
            set { readOnly = value; }
        }

        /// <summary>
        /// Gets or sets the scroll bars the text box should display.
        /// </summary>
        public virtual ScrollBars ScrollBars
        {
            get { return scrollBars; }
            set
            {
                scrollBars = value;
                SetupBars();
            }
        }

        /// <summary>
        /// Gets all text within the current selection.
        /// </summary>
        public virtual string SelectedText
        {
            get
            {
// Insert text?
                if (selection.IsEmpty)
                {
                    return "";
                }
// Replace selection?
                return Text.Substring(selection.Start, selection.Length);
            }
        }

        /// <summary>
        /// Gets or sets (from the current value of SelectionStart) the length of the selection.
        /// </summary>
        public virtual int SelectionLength
        {
            get { return selection.Length; }
            set
            {
                if (value == 0)
                {
                    selection.End = selection.Start;
                }
                else if (selection.IsEmpty)
                {
                    selection.Start = 0;
                    selection.End = value;
                }
                else if (!selection.IsEmpty)
                {
                    selection.End = selection.Start + value;
                }


// Delete all selected text?
                if (!selection.IsEmpty)
                {
                    if (selection.Start < 0) selection.Start = 0;
                    if (selection.Start > Text.Length) selection.Start = Text.Length;
                    if (selection.End < 0) selection.End = 0;
                    if (selection.End > Text.Length) selection.End = Text.Length;
                }
                ClientArea.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the start position of the selection.
        /// </summary>
        public virtual int SelectionStart
        {
            get
            {
// Insert text?
                if (selection.IsEmpty)
                {
                    return Pos;
                }
// Replace selection?
                return selection.Start;
            }
            set
            {
                Pos = value;
                if (Pos < 0) Pos = 0;
                if (Pos > Text.Length) Pos = Text.Length;
                selection.Start = Pos;
                if (selection.End == -1) selection.End = Pos;
                ClientArea.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the contents of the text box control.
        /// </summary>
        public override string Text
        {
            get { return text; }
            set
            {
                if (wordWrap)
                    value = WrapWords(value, ClientWidth);


                if (mode != TextBoxMode.Multiline && value != null)
                {
                    value = value.Replace(Separator, "");
                }


                text = value;


                if (!Suspended) OnTextChanged(new EventArgs());


                lines = SplitLines(text);
                if (ClientArea != null) ClientArea.Invalidate();


                SetupBars();
                ProcessScrolling();
            }
        }

        /// <summary>
        /// Indicates if word wrap is enabled in multi-line text box controls.
        /// </summary>
        public virtual bool WordWrap
        {
            get { return wordWrap; }
            set
            {
                wordWrap = value;
                if (ClientArea != null) ClientArea.Invalidate();
                SetupBars();
            }
        }

        /// <summary>
        /// Returns the text content as a Separator delimited list of strings.
        /// </summary>
        private List<string> Lines
        {
            get { return lines; }
            set { lines = value; }
        }

        public int Pos
        {
            get { return GetPos(PosX, PosY); }
            set
            {
                PosY = GetPosY(value);
                PosX = GetPosX(value);
            }
        }

        /// <summary>
        /// Gets or sets the X position of the caret on the current line.
        /// </summary>
        private int PosX
        {
            get { return posx; }
            set
            {
                posx = value;


                if (posx < 0) posx = 0;
                if (posx > Lines[PosY].Length) posx = Lines[PosY].Length;
            }
        }

        /// <summary>
        /// Gets or sets the Y position of the caret in the text box.
        /// </summary>
        private int PosY
        {
            get { return posy; }
            set
            {
                posy = value;


                if (posy < 0) posy = 0;
                if (posy > Lines.Count - 1) posy = Lines.Count - 1;


                PosX = PosX;
            }
        }

        /// <summary>
        /// Horizontal scroll bar of the text box.
        /// </summary>
        private readonly ScrollBar horz;

        /// <summary>
        /// Characted used as the line separator character.
        /// </summary>
        private readonly string Separator = "\n";

        /// <summary>
        /// Vertical scroll bar of the text box.
        /// </summary>
        private readonly ScrollBar vert;

        /// <summary>
        /// Indicates if all text should be selected automatically when the control gains focus.
        /// </summary>
        private bool autoSelection = true;

        /// <summary>
        /// Internal use during text splitting operations.
        /// </summary>
        private string buffer = "";

        /// <summary>
        /// Indicates if the caret is displayed or not.
        /// </summary>
        private bool caretVisible = true;

        /// <summary>
        /// Number of characters that can fit horizontally in the client area.
        /// </summary>
        private int charsDrawn;

        /// <summary>
        /// Indicates if the borders of the text box should be drawn.
        /// </summary>
        private bool drawBorders = true;

        private double flashTime;

        /// <summary>
        /// Font used to draw the control's text.
        /// </summary>
        private SpriteFont font;

        /// <summary>
        /// Text content broken into individual lines.
        /// </summary>
        private List<string> lines = new List<string>();

        /// <summary>
        /// Number of lines of text that can fit vertically in the client area.
        /// </summary>
        private int linesDrawn;

        /// <summary>
        /// Indicates if the text box is a single-line, multi-line, or password text box.
        /// </summary>
        private TextBoxMode mode = TextBoxMode.Normal;

        /// <summary>
        /// Specifies which character will be used to mask input when the text box is in Password mode.
        /// </summary>
        private char passwordChar = '•';

        /// <summary>
        /// X position of the text caret.
        /// </summary>
        private int posx;

        /// <summary>
        /// Y position of the text caret.
        /// </summary>
        private int posy;

        /// <summary>
        /// Indicates if the text box can accept user input or if it is read-only.
        /// </summary>
        private bool readOnly;

        /// <summary>
        /// Specifies which, if any, scroll bars should be displayed in the text box.
        /// </summary>
        private ScrollBars scrollBars = ScrollBars.Both;

        /// <summary>
        /// Currently selected text of the control, specified by starting and ending indexes.
        /// </summary>
        private Selection selection = new Selection(-1, -1);

        /// <summary>
        /// Indicates if the cursor should be displayed when hovered. ???
        /// </summary>
        private bool showCursor;

        /// <summary>
        /// Text that is currently visible in the client area.
        /// </summary>
        private string shownText = "";

        /// <summary>
        /// Current text content of the control.
        /// </summary>
        private string text = "";

        /// <summary>
        /// Indicates if word wrap is enabled on multi-line text boxes.
        /// </summary>
        private bool wordWrap;

        /// <param name="manager">GUI manager for the control.</param>
        /// <summary>
        /// Creates a new TextBox control.
        /// </summary>
        public TextBox(Manager manager)
            : base(manager)
        {
// Cursor layer defined?
            CheckLayer(Skin, lrCursor);


            SetDefaultSize(128, 20);
            Lines.Add("");


            ClientArea.Draw += ClientArea_Draw;


// Create the scroll bars for the text box.
            vert = new ScrollBar(manager, Orientation.Vertical);
            horz = new ScrollBar(manager, Orientation.Horizontal);
        }

        /// <summary>
        /// Initializes the text box control.
        /// </summary>
        public override void Init()
        {
            base.Init();


// Set up the vertical scroll bar.
            vert.Init();
            vert.Range = 1;
            vert.PageSize = 1;
            vert.Value = 0;
            vert.Anchor = Anchors.Top | Anchors.Right | Anchors.Bottom;
            vert.ValueChanged += sb_ValueChanged;


// Set up the horizontal scroll bar.
            horz.Init();
            horz.Range = ClientArea.Width;
            horz.PageSize = ClientArea.Width;
            horz.Value = 0;
            horz.Anchor = Anchors.Right | Anchors.Left | Anchors.Bottom;
            horz.ValueChanged += sb_ValueChanged;


            horz.Visible = false;
            vert.Visible = false;


            Add(vert, false);
            Add(horz, false);
        }

        public virtual void SelectAll()
        {
            if (text.Length > 0)
            {
                selection.Start = 0;
                selection.End = Text.Length;
            }
        }

        /// <summary>
        /// Initializes the skin of the text box control.
        /// </summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls[skTextBox]);


#if (!XBOX && !XBOX_FAKE)
            Cursor = Manager.Skin.Cursors[crText].Resource;
#endif


// Get the font used for drawing the text box contents.
            font = (Skin.Layers[lrTextBox].Text != null) ? Skin.Layers[lrTextBox].Text.Font.Resource : null;
        }

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <summary>
        /// Updates the text box cursor state.
        /// </summary>
        protected internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            var sc = showCursor;


// Only show the cursor when the text box has focus.
            showCursor = Focused;


            if (Focused)
            {
// Update the cursor flash timer and display/hide the cursor every 0.5 seconds.
                flashTime += gameTime.ElapsedGameTime.TotalSeconds;
                showCursor = flashTime < 0.5;
                if (flashTime > 1) flashTime = 0;
            }
// Visibility of the cursor has changed? Redraw.
            if (sc != showCursor) ClientArea.Invalidate();
        }

        /// <summary>
        /// Update the text box margins based on the visibility of the scroll bars.
        /// </summary>
        protected override void AdjustMargins()
        {
// Horizontal scroll bar hidden?
            if (horz != null && !horz.Visible)
            {
                vert.Height = Height - 4;
                ClientMargins = new Margins(ClientMargins.Left, ClientMargins.Top, ClientMargins.Right,
                    Skin.ClientMargins.Bottom);
            }
// Replace selection?
            else
            {
                ClientMargins = new Margins(ClientMargins.Left, ClientMargins.Top, ClientMargins.Right,
                    18 + Skin.ClientMargins.Bottom);
            }


// Vertical scroll bar hidden?
            if (vert != null && !vert.Visible)
            {
                horz.Width = Width - 4;
                ClientMargins = new Margins(ClientMargins.Left, ClientMargins.Top, Skin.ClientMargins.Right,
                    ClientMargins.Bottom);
            }
// Replace selection?
            else
            {
                ClientMargins = new Margins(ClientMargins.Left, ClientMargins.Top, 18 + Skin.ClientMargins.Right,
                    ClientMargins.Bottom);
            }
            base.AdjustMargins();
        }

        public override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
// Need to draw borders?
            if (drawBorders)
            {
                base.DrawControl(renderer, rect, gameTime);
            }
        }

// Guide visible?

        /// <param name="e"></param>
        /// <summary>
        /// Handler for when the text box gains focus.
        /// </summary>
        protected override void OnFocusGained(EventArgs e)
        {
// Auto-select all text?
            if (!readOnly && autoSelection)
            {
                SelectAll();
                ClientArea.Invalidate();
            }

// Guide visible?

            base.OnFocusGained(e);
        }

// Guide visible?

        /// <param name="e"></param>
        /// <summary>
        /// Handler for when the text box loses focus.
        /// </summary>
        protected override void OnFocusLost(EventArgs e)
        {
// Clear selection.
            selection.Clear();
            ClientArea.Invalidate();
            base.OnFocusLost(e);
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles key press events for the text box.
        /// </summary>
        protected override void OnKeyPress(KeyEventArgs e)
        {
// Reset the timer used to flash the caret.
            flashTime = 0;


// Key event handled already?
            if (!e.Handled)
            {
// Control + A = Select All Text.
                if (e.Key == Keys.A && e.Control && mode != TextBoxMode.Password)
                {
                    SelectAll();
                }
// Up arrow key press?
                if (e.Key == Keys.Up)
                {
// Display the on-screen keyboard.
                    e.Handled = true;


// Begin selection on Shift + Up if a selection isn't already set.
                    if (e.Shift && selection.IsEmpty && mode != TextBoxMode.Password)
                    {
                        selection.Start = Pos;
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        PosY -= 1;
                    }
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
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        PosY += 1;
                    }
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
                    }
// Remove a single character?
                    else if (Text.Length > 0 && Pos > 0)
                    {
                        Pos -= 1;
                        Text = Text.Remove(Pos, 1);
                    }
// Clear selection.
                    selection.Clear();
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
                    }
// Remove the character after the caret?
                    else if (Pos < Text.Length)
                    {
                        Text = Text.Remove(Pos, 1);
                    }
// Clear selection.
                    selection.Clear();
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
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        Pos -= 1;
                    }
// Move the caret to the start of the previous word on Control + Left.
                    if (e.Control)
                    {
                        Pos = FindPrevWord(shownText);
                    }
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
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        Pos += 1;
                    }
// Move the caret to the start of the previous word on Control + Left.
                    if (e.Control)
                    {
                        Pos = FindNextWord(shownText);
                    }
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
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        PosX = 0;
                    }
// Move the caret to the start of the previous word on Control + Left.
                    if (e.Control)
                    {
                        Pos = 0;
                    }
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
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        PosX = Lines[PosY].Length;
                    }
// Move the caret to the start of the previous word on Control + Left.
                    if (e.Control)
                    {
                        Pos = Text.Length;
                    }
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
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        PosY -= linesDrawn;
                    }
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
                    }
// Move the caret up a line.
                    if (!e.Control)
                    {
                        PosY += linesDrawn;
                    }
                }
// Insert new line on Enter key press?
                else if (e.Key == Keys.Enter && mode == TextBoxMode.Multiline && !readOnly)
                {
// Display the on-screen keyboard.
                    e.Handled = true;
                    Text = Text.Insert(Pos, Separator);
                    PosX = 0;
                    PosY += 1;
                }
// Tab key pressed?
                else if (e.Key == Keys.Tab)
                {
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
                    }
// Replace selection?
                    else
                    {
                        if (Text.Length > 0)
                        {
                            Text = Text.Remove(selection.Start, selection.Length);
                            Text = Text.Insert(selection.Start, c);
                            Pos = selection.Start + 1;
                        }
// Clear selection.
                        selection.Clear();
                    }
                }


// Update the end of selection?
                if (e.Shift && !selection.IsEmpty)
                {
                    selection.End = Pos;
                }


/*
* TODO: Fix
* MONOTODO: Fix
*/
// Copy selected text to clipboard on Control + C pressed and running on Windows.
                if (e.Control && e.Key == Keys.C && mode != TextBoxMode.Password)
                {
#if (!XBOX && !XBOX_FAKE)
                    Clipboard.Clear();
                    if (mode != TextBoxMode.Password && !selection.IsEmpty)
                    {
                        Clipboard.SetText((Text.Substring(selection.Start, selection.Length)).Replace("\n",
                            Environment.NewLine));
                    }
#endif
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
                    }
// Replace selection?
                    else if (!string.IsNullOrEmpty(Text))
                    {
                        Text = Text.Remove(selection.Start, selection.Length);
                        Text = Text.Insert(selection.Start, t);
                        PosX = selection.Start + t.Length;
// Clear selection.
                        selection.Clear();
                    }
#endif
                }
/* END TODO */
// Clear selection?
                if ((!e.Shift && !e.Control) || Text.Length <= 0)
                {
// Clear selection.
                    selection.Clear();
                }


// Show guide on Control + Down.
                if (e.Control && e.Key == Keys.Down)
                {
// Display the on-screen keyboard.
                    e.Handled = true;
                }
// Reset the timer used to flash the caret.
                flashTime = 0;
                if (ClientArea != null) ClientArea.Invalidate();


                DeterminePages();
                ProcessScrolling();
            }
            base.OnKeyPress(e);
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles mouse button down events for the text box.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);


// Reset the timer used to flash the caret.
            flashTime = 0;


// Reposition caret.
            Pos = CharAtPos(e.Position);
// Clear selection.
            selection.Clear();


// Update selection?
            if (e.Button == MouseButton.Left && caretVisible && mode != TextBoxMode.Password)
            {
                selection.Start = Pos;
                selection.End = Pos;
            }
            ClientArea.Invalidate();
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles mouse move events for the text box.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);


// Mouse move + Left button down = Update selection.
            if (e.Button == MouseButton.Left && !selection.IsEmpty && mode != TextBoxMode.Password &&
                selection.Length < Text.Length)
            {
                var pos = CharAtPos(e.Position);
                selection.End = CharAtPos(e.Position);
                Pos = pos;


                ClientArea.Invalidate();


                ProcessScrolling();
            }
        }

        protected override void OnMouseScroll(MouseEventArgs e)
        {
            if (Mode != TextBoxMode.Multiline)
            {
                base.OnMouseScroll(e);
                return;
            }


            if (e.ScrollDirection == MouseScrollDirection.Down)
                vert.ScrollDown();
// Replace selection?
            else
                vert.ScrollUp();


            base.OnMouseScroll(e);
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles mouse up events for the text box.
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);


// Clear selection if the text box receives a left button click.
            if (e.Button == MouseButton.Left && !selection.IsEmpty && mode != TextBoxMode.Password)
            {
                if (selection.Length == 0) selection.Clear();
            }
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles resize events for the text box.
        /// </summary>
        protected override void OnResize(ResizeEventArgs e)
        {
// Clear text selection and update scroll bars.
            base.OnResize(e);
// Clear selection.
            selection.Clear();
            SetupBars();
        }

        /// <returns>Returns the cursor position that corresponds to the point received.</returns>
        /// <param name="pos">Point to find the text position of.</param>
        /// <summary>
        /// box that is closest to the specified position.
        /// Given a point (such as mouse position), this determines the position in the text
        /// </summary>
        private int CharAtPos(Point pos)
        {
            var x = pos.X;
            var y = pos.Y;
            var px = 0;
            var py = 0;


// Is there more than one line of text to consider?
            if (mode == TextBoxMode.Multiline)
            {
// Get the line index under the specified point.
                py = vert.Value + (y - ClientTop) / font.LineSpacing;
                if (py < 0) py = 0;
                if (py >= Lines.Count) py = Lines.Count - 1;
            }
// Replace selection?
            else
            {
// Otherwise, line index is zero.
                py = 0;
            }


            var str = mode == TextBoxMode.Multiline ? Lines[py] : shownText;


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
                    }
                }
                if (x >
                    ClientLeft + ((int)font.MeasureString(str).X) - horz.Value -
                    (font.MeasureString(str[str.Length - 1].ToString()).X / 3)) px = str.Length;
            }


            return GetPos(px, py);
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <summary>
        /// Handles drawing the client area of the text box control.
        /// </summary>
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


// Get the font used for drawing the text box contents.
            font = (Skin.Layers[lrTextBox].Text != null) ? Skin.Layers[lrTextBox].Text.Font.Resource : null;


// Control has text to draw and we have a font to draw it with?
            if (Text != null && font != null)
            {
                DeterminePages();


// Adjust rectangle to account for current vertical scroll bar value?
                if (mode == TextBoxMode.Multiline)
                {
                    shownText = Text;
                    tmpText = Lines[PosY];
                }
                else if (mode == TextBoxMode.Password)
                {
// Mask the text using the password character.
                    shownText = "";
                    for (var i = 0; i < Text.Length; i++)
                    {
                        shownText = shownText + passwordChar;
                    }
                    tmpText = shownText;
                }
// Replace selection?
                else
                {
                    shownText = Text;
                    tmpText = Lines[PosY];
                }


// Text color defined and control not disabled.
                if (TextColor != UndefinedColor && ControlState != ControlState.Disabled)
                {
// Use the control's text color value.
                    col = TextColor;
                }


                if (mode != TextBoxMode.Multiline)
                {
                    linesDrawn = 0;
                    vert.Value = 0;
                }


                if (string.IsNullOrEmpty(text))
                {
                    var rx = new Rectangle(r.Left - horz.Value, r.Top, r.Width, r.Height);
                    renderer.DrawString(font, Placeholder, rx, PlaceholderColor, al, false, DrawFormattedText);
                }


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
                }


// Get height of a single line of text.
                var sizey = font.LineSpacing;


// Need to draw the caret?
                if (showCursor && caretVisible)
                {
                    var size = Vector2.Zero;
                    if (PosX > 0 && PosX <= tmpText.Length)
                    {
                        size = font.MeasureString(tmpText.Substring(0, PosX));
                    }
                    if (size.Y == 0)
                    {
                        size = font.MeasureString(" ");
                        size.X = 0;
                    }


                    var m = r.Height - font.LineSpacing;


// Create the rectangle where the cursor should be drawn.
                    var rc = new Rectangle(r.Left - horz.Value + (int)size.X, r.Top + m / 2, cursor.Width,
                        font.LineSpacing);


// Adjust rectangle to account for current vertical scroll bar value?
                    if (mode == TextBoxMode.Multiline)
                    {
                        rc = new Rectangle(r.Left + (int)size.X - horz.Value,
                            r.Top + (PosY - vert.Value) * font.LineSpacing, cursor.Width, font.LineSpacing);
                    }
// Draw the cursor in the text box.
                    cursor.Alignment = al;
                    renderer.DrawLayer(cursor, rc, col, 0);
                }


// Draw all visible text.
                for (var i = 0; i < linesDrawn + 1; i++)
                {
                    var ii = i + vert.Value;
                    if (ii >= Lines.Count || ii < 0) break;


                    if (Lines[ii] != "")
                    {
// Adjust rectangle to account for current vertical scroll bar value?
                        if (mode == TextBoxMode.Multiline)
                        {
                            renderer.DrawString(font, Lines[ii], r.Left - horz.Value, r.Top + (i * sizey), col, DrawFormattedText);
                        }
// Replace selection?
                        else
                        {
                            var rx = new Rectangle(r.Left - horz.Value, r.Top, r.Width, r.Height);
                            renderer.DrawString(font, shownText, rx, col, al, false, DrawFormattedText);
                        }
                    }
                }
/*  if (drawsel)
{
renderer.End();
renderer.Begin(BlendingMode.Premultiplied);
}*/
            }
        }

        /// <summary>
        /// client area of the text box.
        /// Updates the number of lines and characters drawn based on the current dimensions of the
        /// </summary>
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


// NOTE: How exactly does this work out?
// Update the number of characters drawn.
                charsDrawn = ClientArea.Width - 1;
            }
        }

        /// <param name="rect">Region where the selection overlay should be drawn.</param>
        /// <param name="renderer">Render management object.</param>
        /// <summary>
        /// Draws the text box's selection overlay to highlight selected text.
        /// </summary>
        private void DrawSelection(Renderer renderer, Rectangle rect)
        {
// Delete all selected text?
            if (!selection.IsEmpty)
            {
                var s = selection.Start;
                var e = selection.End;


// Get selection's starting line index, ending line index, starting column index, and ending column index.
                var sl = GetPosY(s);
                var el = GetPosY(e);
                var sc = GetPosX(s);
                var ec = GetPosX(e);


// Selection height is the height of a single line of text.
                var hgt = font.LineSpacing;


                var start = sl;
                var end = el;


// Adjust start and end positions to account for vertical scroll values.
                if (start < vert.Value) start = vert.Value;
                if (end > vert.Value + linesDrawn) end = vert.Value + linesDrawn;


// Draw each line of the selection.
                for (var i = start; i <= end; i++)
                {
                    var r = Rectangle.Empty;


                    if (mode == TextBoxMode.Normal)
                    {
                        var m = ClientArea.Height - font.LineSpacing;
                        r = new Rectangle(
                            rect.Left - horz.Value + (int)font.MeasureString(Lines[i].Substring(0, sc)).X,
                            rect.Top + m / 2,
                            (int)font.MeasureString(Lines[i].Substring(0, ec + 0)).X -
                            (int)font.MeasureString(Lines[i].Substring(0, sc)).X, hgt);
                    }
                    else if (sl == el)
                    {
                        r = new Rectangle(
                            rect.Left - horz.Value + (int)font.MeasureString(Lines[i].Substring(0, sc)).X,
                            rect.Top + (i - vert.Value) * hgt,
                            (int)font.MeasureString(Lines[i].Substring(0, ec + 0)).X -
                            (int)font.MeasureString(Lines[i].Substring(0, sc)).X, hgt);
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
                    }


                    renderer.Draw(Manager.Skin.Images["Control"].Resource, r,
                        Color.FromNonPremultiplied(160, 160, 160, 128));
                }
            }
        }

        /// <returns>
        /// Returns the index of the start of the next word or the last valid index if the cursor has reached the end
        /// point.
        /// </returns>
        /// <param name="text">Text content to search.</param>
        /// <summary>
        /// From the current cursor position, this finds the index of the start of the word ahead of it.
        /// </summary>
        private int FindNextWord(string text)
        {
            var space = false;


            for (var i = Pos; i < text.Length - 1; i++)
            {
                if (!char.IsLetterOrDigit(text[i]))
                {
                    space = true;
                    continue;
                }
// First non-whitespace character after the first encountered space is the start of the next word.
                if (space && char.IsLetterOrDigit(text[i]))
                {
                    return i;
                }
            }


// Reached the end of the text.
            return text.Length;
        }

        /// <returns>Returns the index of the start of the previous word or zero if the cursor has reached the starting point.</returns>
        /// <param name="text">Text content to search.</param>
        /// <summary>
        /// word, or the start of the previous word if the cursor is at the start of a word..
        /// This will be the start of the current word, if the cursor is positioned in the middle of a
        /// From the current cursor position, this finds the index of the start of the word behind it.
        /// </summary>
        private int FindPrevWord(string text)
        {
            var letter = false;


// Get current position of the cursor.
            var p = Pos - 1;
            if (p < 0) p = 0;
            if (p >= text.Length) p = text.Length - 1;


// Search backwards from the current position
            for (var i = p; i >= 0; i--)
            {
// of the word we want to find the start of.
// First non whitespace character from current position indicates start
                if (char.IsLetterOrDigit(text[i]))
                {
                    letter = true;
                    continue;
                }
// of the word behind the cursor's current position.
// First white space character indicates that we are at the beginning
                if (letter && !char.IsLetterOrDigit(text[i]))
                {
                    return i + 1;
                }
            }


// Reached the beginning of the text string.
            return 0;
        }

        /// <returns>Returns the number of characters, from the start of the string, that will fit in the width specified.</returns>
        /// <param name="width">Width available for text placement.</param>
        /// <param name="text">Text string to fit.</param>
        /// <summary>
        /// Returns the number of characters of the specified text that will fit within the specified width.
        /// </summary>
        private int GetFitChars(string text, int width)
        {
// All characters will fit unless proven otherwise.
            var ret = text.Length;
            var size = 0;


            for (var i = 0; i < text.Length; i++)
            {
// Get the width of the current substring.
                size = (int)font.MeasureString(text.Substring(0, i)).X;
// Too large? Update character count and exit.
                if (size > width)
                {
                    ret = i;
                    break;
                }
            }


            return ret;
        }

        /// <returns>The longest line in the text box.</returns>
        /// <summary>
        /// Gets the line of the text box with the greatest length.
        /// </summary>
        private string GetMaxLine()
        {
            var max = 0;
            var x = 0;


// Determine which line the cursor is on.
            for (var i = 0; i < Lines.Count; i++)
            {
                if (Lines[i].Length > max)
                {
                    max = Lines[i].Length;
                    x = i;
                }
            }
            return Lines.Count > 0 ? Lines[x] : "";
        }

        /// <returns>Returns the cursor position for the specified location.</returns>
        /// <param name="y">Line index.</param>
        /// <param name="x">Column index.</param>
        /// <summary>
        /// that matches the specified location.
        /// Given the column (x) and line (y) indexes, this returns the cursor position
        /// </summary>
        private int GetPos(int x, int y)
        {
            var p = 0;


            for (var i = 0; i < y; i++)
            {
                p += Lines[i].Length + Separator.Length;
            }
            p += x;


            return p;
        }

        /// <returns>Returns the index of the column at the specified cursor position.</returns>
        /// <param name="pos">Position of the cursor in the text.</param>
        /// <summary>
        /// Gets the column index of the specified position.
        /// </summary>
        private int GetPosX(int pos)
        {
// Cursor is at the end of the text content?
            if (pos >= Text.Length) return Lines[Lines.Count - 1].Length;


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
                }
            }
// Reached the beginning of the text string.
            return 0;
        }

        /// <returns>Returns the index of the line where the cursor is positioned.</returns>
        /// <param name="pos">Cursor position in text.</param>
        /// <summary>
        /// Gets the line index where the cursor is currently positioned.
        /// </summary>
        private int GetPosY(int pos)
        {
// Cursor is past the last line of text?
            if (pos >= Text.Length) return Lines.Count - 1;


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
                }
            }
// Reached the beginning of the text string.
            return 0;
        }

        /// <returns>Returns the width of the specified number of characters in the supplied text.</returns>
        /// <param name="count">Number of characters from the start of the string to measure.</param>
        /// <param name="text">String to measure the width of.</param>
        /// <summary>
        /// Measures the width of the specified text or a sub-string of the text.
        /// </summary>
        private int GetStringWidth(string text, int count)
        {
            if (count > text.Length) count = text.Length;
            return (int)font.MeasureString(text.Substring(0, count)).X;
        }

        /// <summary>
        /// of the text box and the current cursor position within the text box.
        /// Updates scroll bar values and page sizes based on the dimensions of the client area
        /// </summary>
        private void ProcessScrolling()
        {
            if (vert != null && horz != null)
            {
// Update page size values based on dimensions of client area.
                vert.PageSize = linesDrawn;
                horz.PageSize = charsDrawn;


// Clamp horizontal page value in range.
                if (horz.PageSize > horz.Range) horz.PageSize = horz.Range;


// Update vertical scroll bar value so the current insertion position is visible.
                if (PosY >= vert.Value + vert.PageSize)
                {
                    vert.Value = (PosY + 1) - vert.PageSize;
                }
                else if (PosY < vert.Value)
                {
                    vert.Value = PosY;
                }


// Update horizontal scroll bar value so the current insertion position is visible.
                if (GetStringWidth(Lines[PosY], PosX) >= horz.Value + horz.PageSize)
                {
                    horz.Value = (GetStringWidth(Lines[PosY], PosX) + 1) - horz.PageSize;
                }
                else if (GetStringWidth(Lines[PosY], PosX) < horz.Value)
                {
                    horz.Value = GetStringWidth(Lines[PosY], PosX) - horz.PageSize;
                }
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <summary>
        /// Handles scroll events for the text box.
        /// </summary>
        private void sb_ValueChanged(object sender, EventArgs e)
        {
            ClientArea.Invalidate();
        }

        /// <summary>
        /// Updates scroll bar settings based on dimensions of the client area and text content.
        /// </summary>
        private void SetupBars()
        {
            DeterminePages();


            if (vert != null) vert.Range = Lines.Count;
            if (horz != null)
            {
                horz.Range = (int)font.MeasureString(GetMaxLine()).X;
                if (horz.Range == 0) horz.Range = ClientArea.Width;
            }


            if (vert != null)
            {
                vert.Left = Width - 16 - 2;
                vert.Top = 2;
                vert.Height = Height - 4 - 16;


                if (Height < 50 || (scrollBars != ScrollBars.Both && scrollBars != ScrollBars.Vertical))
                    vert.Visible = false;
                else if ((scrollBars == ScrollBars.Vertical || scrollBars == ScrollBars.Both) &&
                         mode == TextBoxMode.Multiline) vert.Visible = true;
            }
            if (horz != null)
            {
                horz.Left = 2;
                horz.Top = Height - 16 - 2;
                horz.Width = Width - 4 - 16;


                if (Width < 50 || wordWrap || (scrollBars != ScrollBars.Both && scrollBars != ScrollBars.Horizontal))
                    horz.Visible = false;
                else if ((scrollBars == ScrollBars.Horizontal || scrollBars == ScrollBars.Both) &&
                         mode == TextBoxMode.Multiline && !wordWrap) horz.Visible = true;
            }


            AdjustMargins();


            if (vert != null) vert.PageSize = linesDrawn;
            if (horz != null) horz.PageSize = charsDrawn;
        }

        /// <returns>List of strings delimited by the text box separator character.</returns>
        /// <param name="text">Text to split.</param>
        /// <summary>
        /// Splits the specified text into a list of strings based on the text box separator character.
        /// </summary>
        private List<string> SplitLines(string text)
        {
            if (buffer != text)
            {
                buffer = text;
                var list = new List<string>();
                var s = text.Split(Separator[0]);
                list.Clear();


                list.AddRange(s);


                if (posy < 0) posy = 0;
                if (posy > list.Count - 1) posy = list.Count - 1;


                if (posx < 0) posx = 0;
                if (posx > list[PosY].Length) posx = list[PosY].Length;


                return list;
            }
            return lines;
        }

        /// <returns>Returns the word wrapped string.</returns>
        /// <param name="size">Width of the text box the text will be wrapped in.</param>
        /// <param name="text">Text content to word wrap.</param>
        /// <summary>
        /// Breaks up text content so that all lines fit within the width of the client area of the text box.
        /// </summary>
        private string WrapWords(string text, int size)
        {
            var ret = "";
            var line = "";


// Split text at each space and break into a word array.
            var words = text.Replace("\v", "").Split(" ".ToCharArray());


// the width of the text box client area.
// Concatenate words until it has been reformed into lines that fit
            for (var i = 0; i < words.Length; i++)
            {
                if (font.MeasureString(line + words[i]).X > size)
                {
                    ret += line + "\n";
                    line = words[i] + " ";
                }
// Replace selection?
                else
                {
                    line += words[i] + " ";
                }
            }


// Append last line.
            ret += line;


// Remove last space and return the new formatted string.
            return ret.Remove(ret.Length - 1, 1);
        }

        #region Nested type: Struct

        /// <summary>
        /// </summary>
        private struct Selection
        {
            public int End
            {
                get
                {
                    if (end < start && start != -1 && end != -1) return start;
                    return end;
                }
                set { end = value; }
            }

            public bool IsEmpty
            {
                get { return Start == -1 && End == -1; }
            }

            public int Length
            {
                get { return IsEmpty ? 0 : (End - Start); }
            }

            public int Start
            {
                get
                {
                    if (start > end && start != -1 && end != -1) return end;
                    return start;
                }
                set { start = value; }
            }

            private int end;
            private int start;

            public Selection(int start, int end)
            {
                this.start = start;
                this.end = end;
            }

            public void Clear()
            {
                Start = -1;
                End = -1;
            }
        }

        #endregion
    }


// Guide visible?


// Guide visible?
}

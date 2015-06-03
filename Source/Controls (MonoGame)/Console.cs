using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoForce.Controls
{
    /// <summary>
    /// Represents a single message sent to a console.
    /// </summary>
    public struct ConsoleMessage
    {
        /// <summary>
        /// Message text.
        /// </summary>
        public string Text;
        /// <summary>
        /// Console channel index.
        /// </summary>
        public byte Channel;
        /// <summary>
        /// Message time stamp.
        /// </summary>
        public DateTime Time;
        /// <summary>
        /// Color of the message, If different than channel color.
        /// </summary>
        public Color Color;
        /// <summary>
        /// Defines if the date/time is shown, or plain text
        /// </summary>
        public bool NoShow;

        /// <summary>
        /// Describes how many lines the text has.
        /// </summary>
        public int Lines;

        /// <summary>
        /// Creates a new console message with color.
        /// </summary>
        /// <param name="text">Message text.</param>
        /// <param name="channel">Console channel index.</param>
        /// <param name="color">Color of the message</param>
        public ConsoleMessage(string text, byte channel, Color color)
        {
            this.Text = text;
            this.Channel = channel;
            this.Time = DateTime.Now;
            this.Color = color;
            this.Lines = 1;
            this.NoShow = false;
            Lines = GetLineCount(text);
        }


        /// <summary>
        /// Creates a new console message.
        /// </summary>
        /// <param name="text">Message text.</param>
        /// <param name="channel">Console channel index.</param>
        public ConsoleMessage(string text, byte channel)
        {
            this.Text = text;
            this.Channel = channel;
            this.Time = DateTime.Now;
            this.Color = Color.Transparent;
            this.Lines = 1;
            this.NoShow = false;
            Lines = GetLineCount(text);

        }

        private int GetLineCount(string text)
        {
            //Get how many /n's there are to incriment line count
            return System.Text.RegularExpressions.Regex.Matches(text, Manager.StringNewline).Count + 1;
        }
    }

    /// <summary>
    /// Represents a list of console channels.
    /// </summary>
    public class ChannelList : EventedList<ConsoleChannel>
    {
  
        /// <summary>
        /// Gets or sets a console channel by channel name.
        /// </summary>
        /// <param name="name">Console channel name.</param>
        public ConsoleChannel this[string name]
        {
            get
            {
                for (int i = 0; i < this.Count; i++)
                {
                    ConsoleChannel s = (ConsoleChannel)this[i];
                    if (s.Name.ToLower() == name.ToLower())
                    {
                        return s;
                    }
                }
                return default(ConsoleChannel);
            }

            set
            {
                for (int i = 0; i < this.Count; i++)
                {
                    ConsoleChannel s = (ConsoleChannel)this[i];
                    if (s.Name.ToLower() == name.ToLower())
                    {
                        this[i] = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a console channel by the channel's index.
        /// </summary>
        /// <param name="index">Console channel index.</param>
        public ConsoleChannel this[byte index]
        {
            get
            {
                for (int i = 0; i < this.Count; i++)
                {
                    ConsoleChannel s = (ConsoleChannel)this[i];
                    if (s.Index == index)
                    {
                        return s;
                    }
                }
                return default(ConsoleChannel);
            }

            set
            {
                for (int i = 0; i < this.Count; i++)
                {
                    ConsoleChannel s = (ConsoleChannel)this[i];
                    if (s.Index == index)
                    {
                        this[i] = value;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Represents a single channel of the console.
    /// </summary>
    public class ConsoleChannel
    {
        /// <summary>
        /// Name of the console channel.
        /// </summary>
        private string name;
        /// <summary>
        /// Index of the console channel.
        /// </summary>
        private byte index;
        /// <summary>
        /// Color of the console channel's message text.
        /// </summary>
        private Color color;

        /// <summary>
        /// Creates a new console channel.
        /// </summary>
        /// <param name="index">Index of the console channel.</param>
        /// <param name="name">Name of the console channel.</param>
        /// <param name="color">Color of the console channel message text.</param>
        public ConsoleChannel(byte index, string name, Color color)
        {
            this.name = name;
            this.index = index;
            this.color = color;
        }

        /// <summary>
        /// Gets or sets the index of the console channel.
        /// </summary>
        public virtual byte Index
        {
            get { return index; }
            set { index = value; }
        }

        /// <summary>
        /// Gets or sets the text color of the console channel's messages.
        /// </summary>
        public virtual Color Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// Gets or sets the name of the console channel.
        /// </summary>
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

    }

    /// <summary>
    /// Describes the format of a console message.
    /// </summary>
    [Flags]
    public enum ConsoleMessageFormats
    {
        /// <summary>
        /// Messages only display the body text.
        /// </summary>
        None = 0x00,
        /// <summary>
        /// Messages are prefixed with the channel name.
        /// </summary>
        ChannelName = 0x01,
        /// <summary>
        /// Messages are prefixed with the time they were sent.
        /// </summary>
        TimeStamp = 0x02,
        /// <summary>
        /// Messages are prefixed with the channel name and timestamp.
        /// </summary>
        All = ChannelName | TimeStamp
    }

    /// <summary>
    /// Multi-channel console control that also allows user text input.
    /// </summary>
    public class Console : Container
    {
 
        /// <summary>
        /// Text box control where console messages can be input.
        /// </summary>
        public TextBox txtMain = null;
        /// <summary>
        /// Console channel selection box.
        /// </summary>
        public ComboBox cmbMain;
        /// <summary>
        /// Console text box's vertical scroll bar control.
        /// </summary>
        private ScrollBar sbVert;
        /// <summary>
        /// Console message list.
        /// </summary>
        private EventedList<ConsoleMessage> buffer = new EventedList<ConsoleMessage>();
        /// <summary>
        /// Users sent message list
        /// </summary>
        private EventedList<ConsoleMessage> sentMessages = new EventedList<ConsoleMessage>();
        /// <summary>
        /// List of console channels for this console.
        /// </summary>
        private ChannelList channels = new ChannelList();
        /// <summary>
        /// Console channel filter list.
        /// </summary>
        private List<byte> filter = new List<byte>();
        /// <summary>
        /// Console message format.
        /// </summary>
        private ConsoleMessageFormats messageFormat = ConsoleMessageFormats.None;
        /// <summary>
        /// Indicates if the channel selection combo box is visible.
        /// </summary>
        private bool channelsVisible = true;
        /// <summary>
        /// Indicates if the user input text box is visible.
        /// </summary>
        private bool textBoxVisible = true;
        /// <summary>
        /// Selected history by pressing "Up"
        /// </summary>
        private int historyIndex = 0;
        /// <summary>
        /// Gets or sets the console's message buffer.
        /// </summary>
        public virtual EventedList<ConsoleMessage> MessageBuffer
        {
            get { return buffer; }
            set
            {
                buffer.ItemAdded -= new EventHandler(buffer_ItemAdded);
                buffer = value;
                buffer.ItemAdded += new EventHandler(buffer_ItemAdded);
            }
        }

        /// <summary>
        /// Gets or sets the console's channel list.
        /// </summary>
        public virtual ChannelList Channels
        {
            get { return channels; }
            set
            {
                channels.ItemAdded -= new EventHandler(channels_ItemAdded);
                channels = value;
                channels.ItemAdded += new EventHandler(channels_ItemAdded);
                channels_ItemAdded(null, null);
            }
        }

        /// <summary>
        /// Gets or sets the console's channel filter.
        /// </summary>
        public virtual List<byte> ChannelFilter
        {
            get { return filter; }
            set { filter = value; }
        }

        /// <summary>
        /// Gets or sets the console's current channel.
        /// </summary>
        public virtual byte SelectedChannel
        {
            set { cmbMain.Text = channels[value].Name; }
            get { return channels[cmbMain.Text].Index; }
        }

        /// <summary>
        /// Gets or sets the console's message format.
        /// </summary>
        public virtual ConsoleMessageFormats MessageFormat
        {
            get { return messageFormat; }
            set { messageFormat = value; }
        }

        /// <summary>
        /// Indicates whether the console is displaying the console channels or not. ???
        /// </summary>
        public virtual bool ChannelsVisible
        {
            get { return channelsVisible; }
            set
            {
                cmbMain.Visible = channelsVisible = value;
                if (value && !textBoxVisible) TextBoxVisible = false;
                PositionControls();
            }
        }

        /// <summary>
        /// Indicates if the console's text box is visible or not.
        /// </summary>
        public virtual bool TextBoxVisible
        {
            get { return textBoxVisible; }
            set
            {
                txtMain.Visible = textBoxVisible = value;
                if (!value && channelsVisible) ChannelsVisible = false;
                PositionControls();
            }
        }
        /// <summary>
        /// The textbox input for the console
        /// </summary>
        public TextBox TextBox { get { return txtMain; } set { txtMain = value; } }
  
        /// <summary>
        /// Occurs when a console message is sent.
        /// </summary>
        public event ConsoleMessageEventHandler MessageSent;
   
        /// <summary>
        /// Creates a new console control.
        /// </summary>
        /// <param name="manager">GUI manager for the console.</param>
        public Console(Manager manager)
            : base(manager)
        {
            Width = 320;
            Height = 160;
            MinimumHeight = 64;
            MinimumWidth = 64;
            CanFocus = false;

            Resizable = false;
            Movable = false;

            cmbMain = new ComboBox(manager);
            cmbMain.Init();
            cmbMain.Top = Height - cmbMain.Height;
            cmbMain.Left = 0;
            cmbMain.Width = 128;
            cmbMain.Anchor = Anchors.Left | Anchors.Bottom;
            cmbMain.Detached = false;
            cmbMain.DrawSelection = false;
            cmbMain.Visible = channelsVisible;
            Add(cmbMain, false);

            txtMain = new TextBox(manager);
            txtMain.Init();
            txtMain.Top = Height - txtMain.Height;
            txtMain.Left = cmbMain.Width + 1;
            txtMain.Anchor = Anchors.Left | Anchors.Bottom | Anchors.Right;
            txtMain.Detached = false;
            txtMain.Visible = textBoxVisible;
            txtMain.KeyDown += new KeyEventHandler(txtMain_KeyDown);
            txtMain.GamePadDown += new GamePadEventHandler(txtMain_GamePadDown);
            txtMain.FocusGained += new EventHandler(txtMain_FocusGained);
            Add(txtMain, false);

            sbVert = new ScrollBar(manager, Orientation.Vertical);
            sbVert.Init();
            sbVert.Top = 2;
            sbVert.Left = Width - 18;
            sbVert.Anchor = Anchors.Right | Anchors.Top | Anchors.Bottom;
            sbVert.Range = 1;
            sbVert.PageSize = 1;
            sbVert.Value = 0;
            sbVert.ValueChanged += new EventHandler(sbVert_ValueChanged);
            Add(sbVert, false);

            ClientArea.Draw += new DrawEventHandler(ClientArea_Draw);

            buffer.ItemAdded += new EventHandler(buffer_ItemAdded);
            channels.ItemAdded += new EventHandler(channels_ItemAdded);
            channels.ItemRemoved += new EventHandler(channels_ItemRemoved);

            PositionControls();
        }

        /// <summary>
        /// Helper to position controls based on the visibility of the input text box control.
        /// </summary>
        private void PositionControls()
        {
            // Is the user input text box initialized?
            if (txtMain != null)
            {
                // Position the input text box based on the visibility of the channel selection box.
                txtMain.Left = channelsVisible ? cmbMain.Width + 1 : 0;
                txtMain.Width = channelsVisible ? Width - cmbMain.Width - 1 : Width;

                if (textBoxVisible)
                {
                    ClientMargins = new Margins(Skin.ClientMargins.Left, Skin.ClientMargins.Top + 4, sbVert.Width + 6, txtMain.Height + 4);
                    sbVert.Height = Height - txtMain.Height - 5;
                }

                else
                {
                    ClientMargins = new Margins(Skin.ClientMargins.Left, Skin.ClientMargins.Top + 4, sbVert.Width + 6, 2);
                    sbVert.Height = Height - 4;
                }

                Invalidate();
            }
        }
   
        /// <summary>
        /// Initializes the console control.
        /// </summary>
        public override void Init()
        {
            base.Init();
        }
    

        /// <summary>
        /// Initializes the skin of the console control.
        /// </summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls["Console"]);

            PositionControls();
        }
    
        /// <summary>
        /// Updates the console control.
        /// </summary>
        /// <param name="gameTime"></param>
        private MouseState LastMouseState;
        private MouseState MouseState;
        /// <summary>
        /// Updates the state of the list box and watches for changes in list size.
        /// </summary>
        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        protected internal override void Update(GameTime gameTime)
        {
            LastMouseState = MouseState;
            MouseState = Mouse.GetState();
            base.Update(gameTime);

            if (LastMouseState.ScrollWheelValue != MouseState.ScrollWheelValue)
                sbVert.Value += (LastMouseState.ScrollWheelValue - MouseState.ScrollWheelValue) / 20;
        }
    
        /// <summary>
        /// Draws the client area of the console.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ClientArea_Draw(object sender, DrawEventArgs e)
        {
            SpriteFont font = null;
            if (!SmallFont)
                font = Skin.Layers[0].Text.Font.Resource;
            else
                font = Manager.Skin.Fonts["Default6"].Resource;
            Rectangle r = new Rectangle(e.Rectangle.Left, e.Rectangle.Top, e.Rectangle.Width, e.Rectangle.Height);
            int pos = 0;

            // Are there messages to display?
            if (buffer.Count > 0)
            {
                // Get messages based on channel index filter.
                EventedList<ConsoleMessage> b = GetFilteredBuffer(filter);
                int c = b.Count;
                int s = (sbVert.Value + sbVert.PageSize);
                int f = s - sbVert.PageSize;

                // Still messages to display?
                if (b.Count > 0)
                {
                    // Display visible messages based on the scroll bar values.for (int i = s - 1; i >= f; i--)
                    for (int i = s - 1; i >= f; i--)
                    {
                        {
                            pos += 1;
                            int x = 4;
                            int y = r.Bottom - (pos) * ((int)font.LineSpacing + 0);

                            string msg = ((ConsoleMessage)b[i]).Text;
                            string pre = "";
                            ConsoleChannel ch = (channels[((ConsoleMessage)b[i]).Channel] as ConsoleChannel);

                            if (!((ConsoleMessage)b[i]).NoShow)
                            {
                                if (messageFormat != ConsoleMessageFormats.None)
                                {
                                    pre += "[color:" + ch.Color.ToColorString() + "]";

                                    // Prefix message with message timestamp?
                                    if ((messageFormat & ConsoleMessageFormats.TimeStamp) == ConsoleMessageFormats.TimeStamp)
                                    {
                                        pre += string.Format("[{0}]", ((ConsoleMessage)b[i]).Time.ToShortTimeString());
                                    }

                                    // Prefix message with console channel name?
                                    if ((messageFormat & ConsoleMessageFormats.ChannelName) == ConsoleMessageFormats.ChannelName)
                                    {
                                        pre += string.Format("[{0}]", channels[((ConsoleMessage)b[i]).Channel].Name);
                                    }

                                    if (pre != "")
                                        msg = pre + ":[/color] " + msg; //Add the pre text
                                }
                            }


                            string[] msgs = msg.Split(new string[1] { Manager.StringNewline }, StringSplitOptions.RemoveEmptyEntries);

                            if (b[i].Color != Color.Transparent)
                            {

                                for (int st = 0; st < msgs.Length; st++)
                                {
                                    int yTemp = st * font.LineSpacing;
                                    e.Renderer.DrawString(font,
                                                  msgs[st],
                                                  x, y + yTemp,
                                                  b[i].Color, base.DrawFormattedText);
                                }


                            }
                            else
                            {

                                for (int st = 0; st < msgs.Length; st++)
                                {
                                    int yTemp = st * font.LineSpacing;
                                    e.Renderer.DrawString(font,
                                                  msgs[st],
                                                  x, y + yTemp,
                                                  ch.Color, base.DrawFormattedText);
                                }

                            }

                        }
                    }
                }
            }
        }
      
        /// <summary>
        /// Draws the console control.
        /// </summary>
        /// <param name="renderer">Render management object.</param>
        /// <param name="rect">Destination rectangle where the control should be drawn.</param>
        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        public override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            // Adjusts the message area size based on whether or not the input area is visible.
            int h = txtMain.Visible ? (txtMain.Height + 1) : 0;
            Rectangle r = new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height - h);
            base.DrawControl(renderer, r, gameTime);
        }
     
        /// <summary>
        /// Updates the active console channel and the text color when 
        /// the text box control receives focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtMain_FocusGained(object sender, System.EventArgs e)
        {
            // Input textbox has focus, set channel and text color appropriately
            // based on the channel selected in the combo box control.
            ConsoleChannel ch = channels[cmbMain.Text];
            if (ch != null) txtMain.TextColor = ch.Color;
        }

        /// <summary>
        /// Handles key down events for the text box. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtMain_KeyDown(object sender, KeyEventArgs e)
        {
            SendMessage(e);
        }

        /// <summary>
        /// Handles gamepad button down events for the text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtMain_GamePadDown(object sender, GamePadEventArgs e)
        {
            SendMessage(e);
        }
    
        /// <summary>
        /// Handles key and button press events the console input text box receives.
        /// </summary>
        /// <param name="x"></param>
        private void SendMessage(EventArgs x)
        {
            // Respect the guide.
            

            KeyEventArgs k = new KeyEventArgs();
            GamePadEventArgs g = new GamePadEventArgs(PlayerIndex.One);

            // Cast to Key/GamePad event arguments as needed.
            if (x is KeyEventArgs) k = x as KeyEventArgs;
            else if (x is GamePadEventArgs) g = x as GamePadEventArgs;

            // Get the selected console channel.
            ConsoleChannel ch = channels[cmbMain.Text];
            if (ch != null)
            {
                // Set the text colors according to channel.
                txtMain.TextColor = ch.Color;

                // Get the message text from the input textbox.
                string message = txtMain.Text;

                // Send the message to the console if the Enter key or the Y button was pressed.
                if ((k.Key == Microsoft.Xna.Framework.Input.Keys.Enter || g.Button == GamePadActions.Press) && message != null && message != "")
                {
                    x.Handled = true;

                    // Send console message event.
                    ConsoleMessageEventArgs me = new ConsoleMessageEventArgs(new ConsoleMessage(message, ch.Index, ch.Color));
                    OnMessageSent(me);
                    sentMessages.Add(me.Message);
                    // Clear the text.
                    txtMain.Text = "";
                    ClientArea.Invalidate();

                    // Update scroll bar value.
                    CalcScrolling();
                    historyIndex = 0;
                }
                //Scroll though history
                else if (k.Key == Microsoft.Xna.Framework.Input.Keys.Down)
                {
                    if (historyIndex > 0)
                        historyIndex--;
                    if (sentMessages.Count - historyIndex >= 0 && historyIndex > 0)
                    {
                        txtMain.Text = sentMessages[sentMessages.Count - historyIndex].Text;
                        txtMain.Pos = txtMain.Text.Length;
                    }
                    else if (txtMain.Text == sentMessages[sentMessages.Count - 1].Text)
                        txtMain.Text = "";
                }
                else if (k.Key == Microsoft.Xna.Framework.Input.Keys.Up && sentMessages.Count > 0 && sentMessages.Count - historyIndex > 0)
                {
                    txtMain.Text = sentMessages[sentMessages.Count - 1 - historyIndex].Text;
                    txtMain.Pos = txtMain.Text.Length;
                    historyIndex++;
                }
            }
        }

        /// <summary>
        /// Handles console message sent events.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMessageSent(ConsoleMessageEventArgs e)
        {
            if (MessageSent != null)
            {
                MessageSent.Invoke(this, e);
            }
        }

        /// <summary>
        /// Handles repopulating the channel list when an item is added.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void channels_ItemAdded(object sender, System.EventArgs e)
        {
            // Clear the channels list.
            cmbMain.Items.Clear();
            for (int i = 0; i < channels.Count; i++)
            {
                // Repopulate the channels list with fresh content.
                cmbMain.Items.Add((channels[i] as ConsoleChannel).Name);
            }
        }

        /// <summary>
        /// Handles repopulating the channels list when items are removed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void channels_ItemRemoved(object sender, System.EventArgs e)
        {
            // Clear the channels list.
            cmbMain.Items.Clear();
            for (int i = 0; i < channels.Count; i++)
            {
                // Repopulate the channels list with fresh content.
                cmbMain.Items.Add((channels[i] as ConsoleChannel).Name);
            }
        }

        /// <summary>
        /// Handles adding new messages to the console message area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buffer_ItemAdded(object sender, System.EventArgs e)
        {
            CalcScrolling();
            ClientArea.Invalidate();
        }

        /// <summary>
        /// Updates the scroll bar values based on the font size, console dimensions, and number of messages.
        /// </summary>
        private void CalcScrolling()
        {
            // Adjust the scroll bar values if it exists.
            if (sbVert != null)
            {
                // Get the line height of the text, the number of lines displayed, and the number of lines that can be displayed at once.
                int line = Skin.Layers[0].Text.Font.Resource.LineSpacing;
                int c = GetFilteredBuffer(filter).Count;
                int p = (int)Math.Ceiling(ClientArea.ClientHeight / (float)line);

                // Update the scroll bar values.
                sbVert.Range = c == 0 ? 1 : c;
                sbVert.PageSize = c == 0 ? 1 : p;
                sbVert.Value = sbVert.Range;
            }
        }

        /// <summary>
        /// Redraws the console's message area after a scrolling event occurs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sbVert_ValueChanged(object sender, System.EventArgs e)
        {
            ClientArea.Invalidate();
        }

        /// <summary>
        /// Updates scroll bar values after the console window has been resized.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(ResizeEventArgs e)
        {
            CalcScrolling();
            base.OnResize(e);
        }

        /// <summary>
        /// Gets all console messages from channels with matching indexes specified in the filter list.
        /// </summary>
        /// <param name="filter">List of channel indexes to retrieve messages for.</param>
        /// <returns>Returns all messages from channels whose index is specified in the filter list.</returns>
        private EventedList<ConsoleMessage> GetFilteredBuffer(List<byte> filter)
        {
            EventedList<ConsoleMessage> ret = new EventedList<ConsoleMessage>();

            if (filter.Count > 0)
            {
                // Only return messages sent by the channels listed in the filter list.
                for (int i = 0; i < buffer.Count; i++)
                {
                    if (filter.Contains(((ConsoleMessage)buffer[i]).Channel))
                    {
                        ret.Add(buffer[i]);
                    }
                }
                return ret;
            }

            // No filter? Return full message buffer.
            else return buffer;
        }

        public bool SmallFont { get; set; }
        public SpriteFont GetFont()
        {
            SpriteFont font = null;
            if (!SmallFont)
                font = Skin.Layers[0].Text.Font.Resource;
            else
                font = Manager.Skin.Fonts["Default6"].Resource;
            return font;
        }
    }
}
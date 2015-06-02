//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

namespace MonoForce.Controls
{
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

    /// </summary>
    /// Represents a single message sent to a console.
    /// <summary>
    public struct ConsoleMessage
    {
        /// </summary>
        /// Console channel index.
        /// <summary>
        public byte Channel;

        public string Sender;

        /// </summary>
        /// Message text.
        /// <summary>
        public string Text;

        /// </summary>
        /// Message time stamp.
        /// <summary>
        public DateTime Time;

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        public ConsoleMessage(string sender, string text, byte channel)
        {
            Text = text;
            Channel = channel;
            Time = DateTime.Now;
            Sender = sender;
        }
    }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

    /// </summary>
    /// Represents a list of console channels.
    /// <summary>
    public class ChannelList : EventedList<ConsoleChannel>
    {
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <param name="name">Console channel name.</param>
        /// </summary>
        /// Gets or sets a console channel by channel name.
        /// <summary>
        public ConsoleChannel this[string name]
        {
            get
            {
                for (var i = 0; i < Count; i++)
                {
                    var s = this[i];
                    if (s.Name.ToLower() == name.ToLower())
                    {
                        return s;
                    }
                }
                return default(ConsoleChannel);
            }
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.
            set
            {
                for (var i = 0; i < Count; i++)
                {
                    var s = this[i];
                    if (s.Name.ToLower() == name.ToLower())
                    {
                        this[i] = value;
                    }
                }
            }
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <param name="index">Console channel index.</param>
        /// </summary>
        /// Gets or sets a console channel by the channel's index.
        /// <summary>
        public ConsoleChannel this[byte index]
        {
            get
            {
                for (var i = 0; i < Count; i++)
                {
                    var s = this[i];
                    if (s.Index == index)
                    {
                        return s;
                    }
                }
                return default(ConsoleChannel);
            }
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.
            set
            {
                for (var i = 0; i < Count; i++)
                {
                    var s = this[i];
                    if (s.Index == index)
                    {
                        this[i] = value;
                    }
                }
            }
        }
    }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

    /// </summary>
    /// Represents a single channel of the console.
    /// <summary>
    public class ConsoleChannel
    {
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// </summary>
        /// Gets or sets the text color of the console channel's messages.
        /// <summary>
        public virtual Color Color
        {
            get { return color; }
            set { color = value; }
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// </summary>
        /// Gets or sets the index of the console channel.
        /// <summary>
        public virtual byte Index
        {
            get { return index; }
            set { index = value; }
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// </summary>
        /// Gets or sets the name of the console channel.
        /// <summary>
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// </summary>
        /// Color of the console channel's message text.
        /// <summary>
        private Color color;

        /// </summary>
        /// Index of the console channel.
        /// <summary>
        private byte index;

        /// </summary>
        /// Name of the console channel.
        /// <summary>
        private string name;

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <param name="color">Color of the console channel message text.</param>
        /// <param name="name">Name of the console channel.</param>
        /// <param name="index">Index of the console channel.</param>
        /// </summary>
        /// Creates a new console channel.
        /// <summary>
        public ConsoleChannel(byte index, string name, Color color)
        {
            this.name = name;
            this.index = index;
            this.color = color;
        }
    }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

    /// </summary>
    /// Describes the format of a console message.
    /// <summary>
    [Flags]
    public enum ConsoleMessageFormats
    {
        /// </summary>
        /// Messages only display the body text.
        /// <summary>
        None = 0x00,

        /// </summary>
        /// Messages are prefixed with the channel name.
        /// <summary>
        ChannelName = 0x01,

        /// </summary>
        /// Messages are prefixed with the time they were sent.
        /// <summary>
        TimeStamp = 0x02,
        Sender = 0x03,
        All = Sender | ChannelName | TimeStamp
    }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

    /// </summary>
    /// Multi-channel console control that also allows user text input.
    /// <summary>
    public class Console : Container
    {
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// </summary>
        /// Gets or sets the console's channel filter.
        /// <summary>
        public virtual List<byte> ChannelFilter
        {
            get { return filter; }
            set { filter = value; }
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// </summary>
        /// Gets or sets the console's channel list.
        /// <summary>
        public virtual ChannelList Channels
        {
            get { return channels; }
            set
            {
                channels.ItemAdded -= channels_ItemAdded;
                channels = value;
                channels.ItemAdded += channels_ItemAdded;
                channels_ItemAdded(null, null);
            }
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// </summary>
        /// Indicates whether the console is displaying the console channels or not. ???
        /// <summary>
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

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// </summary>
        /// Gets or sets the console's message buffer.
        /// <summary>
        public virtual EventedList<ConsoleMessage> MessageBuffer
        {
            get { return buffer; }
            set
            {
                buffer.ItemAdded -= buffer_ItemAdded;
                buffer = value;
                buffer.ItemAdded += buffer_ItemAdded;
            }
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// </summary>
        /// Gets or sets the console's message format.
        /// <summary>
        public virtual ConsoleMessageFormats MessageFormat
        {
            get { return messageFormat; }
            set { messageFormat = value; }
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// </summary>
        /// Gets or sets the console's current channel.
        /// <summary>
        public virtual byte SelectedChannel
        {
            set { cmbMain.Text = channels[value].Name; }
            get { return channels[cmbMain.Text].Index; }
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        public string Sender { get; set; }
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// </summary>
        /// Indicates if the console's text box is visible or not.
        /// <summary>
        public virtual bool TextBoxVisible
        {
            get { return textBoxVisible; }
            set
            {
                txtMain.Visible = textBoxVisible = value;
                txtMain.Focused = true;
                if (!value && channelsVisible) ChannelsVisible = false;
                PositionControls();
            }
        }

        private readonly ComboBox cmbMain;
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        private readonly TextBox txtMain;

        /// </summary>
        /// Console message list.
        /// <summary>
        private EventedList<ConsoleMessage> buffer = new EventedList<ConsoleMessage>();

        /// </summary>
        /// List of console channels for this console.
        /// <summary>
        private ChannelList channels = new ChannelList();

        /// </summary>
        /// Indicates if the channel selection combo box is visible.
        /// <summary>
        private bool channelsVisible = true;

        /// </summary>
        /// Console channel filter list.
        /// <summary>
        private List<byte> filter = new List<byte>();

        /// </summary>
        /// Console message format.
        /// <summary>
        private ConsoleMessageFormats messageFormat = ConsoleMessageFormats.None;

        /// </summary>
        /// Indicates if the user input text box is visible.
        /// <summary>
        private bool textBoxVisible = true;

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <param name="manager">GUI manager for the console.</param>
        /// </summary>
        /// Creates a new console control.
        /// <summary>
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
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

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
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

            txtMain = new TextBox(manager);
            txtMain.Init();
            txtMain.Top = Height - txtMain.Height;
            txtMain.Left = cmbMain.Width + 1;
            txtMain.Anchor = Anchors.Left | Anchors.Bottom | Anchors.Right;
            txtMain.Detached = false;
            txtMain.Visible = textBoxVisible;
            txtMain.KeyDown += txtMain_KeyDown;
            txtMain.GamePadDown += txtMain_GamePadDown;
            txtMain.FocusGained += txtMain_FocusGained;
            Add(txtMain, false);
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

            VerticalScrollBar.Top = 2;
            VerticalScrollBar.Left = Width - 18;
            VerticalScrollBar.Range = 1;
            VerticalScrollBar.PageSize = 1;
            VerticalScrollBar.ValueChanged += VerticalScrollBar_ValueChanged;
            VerticalScrollBar.Visible = true;
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

            ClientArea.Draw += ClientArea_Draw;
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

            buffer.ItemAdded += buffer_ItemAdded;
            channels.ItemAdded += channels_ItemAdded;
            channels.ItemRemoved += channels_ItemRemoved;
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

            PositionControls();
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// </summary>
        /// Initializes the console control.
        /// <summary>
        public override void Init()
        {
            base.Init();
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// </summary>
        /// Occurs when a console message is sent.
        /// <summary>
        public event ConsoleMessageEventHandler MessageSent;

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// </summary>
        /// Initializes the skin of the console control.
        /// <summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls["Console"]);
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

            PositionControls();
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// </summary>
        /// Updates the state of the list box and watches for changes in list size.
        /// <summary>
        protected internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
// Adjusts the message area size based on whether or not the input area is visible.
            var h = txtMain.Visible ? (txtMain.Height + 1) : 0;
            var r = new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height - h);
            base.DrawControl(renderer, r, gameTime);
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <param name="e"></param>
        /// </summary>
        /// Handles console message sent events.
        /// <summary>
        protected virtual void OnMessageSent(ConsoleMessageEventArgs e)
        {
            if (MessageSent != null) MessageSent.Invoke(this, e);
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <param name="e"></param>
        /// </summary>
        /// Updates scroll bar values after the console window has been resized.
        /// <summary>
        protected override void OnResize(ResizeEventArgs e)
        {
// Update scroll bar value.
            CalcScrolling();
            base.OnResize(e);
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles adding new messages to the console message area.
        /// <summary>
        private void buffer_ItemAdded(object sender, EventArgs e)
        {
// Update scroll bar value.
            CalcScrolling();
            ClientArea.Invalidate();
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// </summary>
        /// Updates the scroll bar values based on the font size, console dimensions, and number of messages.
        /// <summary>
        private void CalcScrolling()
        {
            if (VerticalScrollBar != null)
            {
// Get the line height of the text, the number of lines displayed, and the number of lines that can be displayed at once.
                var line = Skin.Layers[0].Text.Font.Resource.LineSpacing;
                var c = GetFilteredBuffer(filter).Count;
                var p = (int)Math.Ceiling(ClientArea.ClientHeight / (float)line);
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

                VerticalScrollBar.Range = c == 0 ? 1 : c;
                VerticalScrollBar.PageSize = c == 0 ? 1 : p;
                VerticalScrollBar.Value = VerticalScrollBar.Range;
            }
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles repopulating the channel list when an item is added.
        /// <summary>
        private void channels_ItemAdded(object sender, EventArgs e)
        {
// Clear the channels list.
            cmbMain.Items.Clear();
            for (var i = 0; i < channels.Count; i++)
            {
// Repopulate the channels list with fresh content.
                cmbMain.Items.Add(channels[i].Name);
            }
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles repopulating the channels list when items are removed.
        /// <summary>
        private void channels_ItemRemoved(object sender, EventArgs e)
        {
// Clear the channels list.
            cmbMain.Items.Clear();
            for (var i = 0; i < channels.Count; i++)
            {
// Repopulate the channels list with fresh content.
                cmbMain.Items.Add(channels[i].Name);
            }
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Draws the client area of the console.
        /// <summary>
        private void ClientArea_Draw(object sender, DrawEventArgs e)
        {
            var font = Skin.Layers[0].Text.Font.Resource;
            var r = new Rectangle(e.Rectangle.Left, e.Rectangle.Top, e.Rectangle.Width, e.Rectangle.Height);
            var pos = 0;
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

// Are there messages to display?
            if (buffer.Count > 0)
            {
// Get messages based on channel index filter.
                var b = GetFilteredBuffer(filter);
                var c = b.Count;
                var s = (VerticalScrollBar.Value + VerticalScrollBar.PageSize);
                var f = s - VerticalScrollBar.PageSize;
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

// Still messages to display?
                if (b.Count > 0)
                {
// Display visible messages based on the scroll bar values.for (int i = s - 1; i >= f; i--)
                    for (var i = s - 1; i >= f; i--)
                    {
                        {
                            var x = 4;
                            var y = r.Bottom - (pos + 1) * (font.LineSpacing + 0);
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

                            var msg = b[i].Text;
                            var pre = "";
                            var ch = channels[b[i].Channel];
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

// Prefix message with console channel name?
                            if ((messageFormat & ConsoleMessageFormats.ChannelName) == ConsoleMessageFormats.ChannelName)
                            {
                                pre += string.Format("[{0}]", channels[b[i].Channel].Name);
                            }
                            if ((messageFormat & ConsoleMessageFormats.Sender) == ConsoleMessageFormats.Sender)
                            {
                                pre += string.Format("[{0}]", b[i].Sender);
                            }
// Prefix message with message timestamp?
                            if ((messageFormat & ConsoleMessageFormats.TimeStamp) == ConsoleMessageFormats.TimeStamp)
                            {
                                pre = string.Format("[{0}]", b[i].Time.ToLongTimeString()) + pre;
                            }
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

                            if (pre != "") msg = pre + ": " + msg;
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

                            e.Renderer.DrawString(font,
                                msg,
                                x, y,
                                ch.Color);
                            pos += 1;
                        }
                    }
                }
            }
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <returns>Returns all messages from channels whose index is specified in the filter list.</returns>
        /// <param name="filter">List of channel indexes to retrieve messages for.</param>
        /// </summary>
        /// Gets all console messages from channels with matching indexes specified in the filter list.
        /// <summary>
        private EventedList<ConsoleMessage> GetFilteredBuffer(List<byte> filter)
        {
            var ret = new EventedList<ConsoleMessage>();
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

            if (filter.Count > 0)
            {
// Only return messages sent by the channels listed in the filter list.
                for (var i = 0; i < buffer.Count; i++)
                {
                    if (filter.Contains(buffer[i].Channel))
                    {
                        ret.Add(buffer[i]);
                    }
                }
                return ret;
            }
// No filter? Return full message buffer.
            return buffer;
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// </summary>
        /// Helper to position controls based on the visibility of the input text box control.
        /// <summary>
        private void PositionControls()
        {
// Is the user input text box initialized?
            if (txtMain != null)
            {
// Position the input text box based on the visibility of the channel selection box.
                txtMain.Left = channelsVisible ? cmbMain.Width + 1 : 0;
                txtMain.Width = channelsVisible ? Width - cmbMain.Width - 1 : Width;
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

                if (textBoxVisible)
                {
                    ClientMargins = new Margins(Skin.ClientMargins.Left, Skin.ClientMargins.Top + 4,
                        VerticalScrollBar.Width + 6, txtMain.Height + 4);
                    VerticalScrollBar.Height = Height - txtMain.Height - 5;
                }
                else
                {
                    ClientMargins = new Margins(Skin.ClientMargins.Left, Skin.ClientMargins.Top + 4,
                        VerticalScrollBar.Width + 6, 2);
                    VerticalScrollBar.Height = Height - 4;
                }
                Invalidate();
            }
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <param name="x"></param>
        /// </summary>
        /// Handles key and button press events the console input text box receives.
        /// <summary>
        private void SendMessage(EventArgs x)
        {
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

            var k = new KeyEventArgs();
            var g = new GamePadEventArgs(PlayerIndex.One);
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

// Cast to Key/GamePad event arguments as needed.
            if (x is KeyEventArgs) k = x as KeyEventArgs;
            else if (x is GamePadEventArgs) g = x as GamePadEventArgs;
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

// based on the channel selected in the combo box control.
// Input textbox has focus, set channel and text color appropriately
            var ch = channels[cmbMain.Text];
            if (ch != null)
            {
// Set the text colors according to channel.
                txtMain.TextColor = ch.Color;
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

// Get the message text from the input textbox.
                var message = txtMain.Text;
// Send the message to the console if the Enter key or the Y button was pressed.
                if ((k.Key == Keys.Enter || g.Button == GamePadActions.Press) && message != null && message != "")
                {
                    x.Handled = true;
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

                    var me = new ConsoleMessageEventArgs(new ConsoleMessage(Sender, message, ch.Index));
                    OnMessageSent(me);
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

                    buffer.Add(new ConsoleMessage(Sender, me.Message.Text, me.Message.Channel));
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

// Clear the text.
                    txtMain.Text = "";
                    ClientArea.Invalidate();
//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

// Update scroll bar value.
                    CalcScrolling();
                }
            }
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// the text box control receives focus.
        /// Updates the active console channel and the text color when
        /// <summary>
        private void txtMain_FocusGained(object sender, EventArgs e)
        {
// based on the channel selected in the combo box control.
// Input textbox has focus, set channel and text color appropriately
            var ch = channels[cmbMain.Text];
            if (ch != null) txtMain.TextColor = ch.Color;
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles gamepad button down events for the text box.
        /// <summary>
        private void txtMain_GamePadDown(object sender, GamePadEventArgs e)
        {
            SendMessage(e);
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles key down events for the text box.
        /// <summary>
        private void txtMain_KeyDown(object sender, KeyEventArgs e)
        {
            SendMessage(e);
        }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.

        private void VerticalScrollBar_ValueChanged(object sender, EventArgs e)
        {
            ClientArea.Invalidate();
        }
    }

//if (Manager.UseGuide && Guide.IsVisible) return;
// Respect the guide.
}
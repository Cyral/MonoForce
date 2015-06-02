using Microsoft.Xna.Framework;
using MonoForce.Controls;

namespace MonoForce.Demo
{
    public partial class MainWindow : Window
    {
        public Label lblAvgFps;
        public Label lblFps;
        public Label lblObjects;
        private Button btnApply;
        private Button btnClose;
        private Button btnExit;
        private Button btnRandom;
        private Button[] btnTasks;
        private CheckBox chkResFull;
        private SideBarPanel pnlRes;
        private SideBarPanel pnlStats;
        private SideBarPanel pnlTasks;
        private RadioButton rdbRes1024;
        private RadioButton rdbRes1280;
        private RadioButton rdbRes1680;
        private SideBar sidebar;

        public MainWindow(Manager manager) : base(manager)
        {
            InitControls();
        }

        private void con1_MessageSent(object sender, ConsoleMessageEventArgs e)
        {
            if (e.Message.Channel == 0)
            {
                //e.Message.Text = "(!) " + e.Message.Text;
            }
        }

        private void InitConsole()
        {
            var tbc = new TabControl(Manager);
            var con1 = new Console(Manager);
            var con2 = new Console(Manager);

            // Setup of TabControl, which will be holding both consoles
            tbc.Init();
            tbc.AddPage("Global");
            tbc.AddPage("Private");

            tbc.Alpha = 220;
            tbc.Left = 220;
            tbc.Height = 220;
            tbc.Width = 400;
            tbc.Top = Manager.TargetHeight - tbc.Height - 32;

            tbc.Movable = true;
            tbc.Resizable = true;
            tbc.MinimumHeight = 96;
            tbc.MinimumWidth = 160;

            tbc.TabPages[0].Add(con1);
            tbc.TabPages[1].Add(con2);

            con1.Init();
            con1.Sender = "Console1";
            con2.Init();
            con2.Sender = "Console2";

            con2.Width = con1.Width = tbc.TabPages[0].ClientWidth;
            con2.Height = con1.Height = tbc.TabPages[0].ClientHeight;
            con2.Anchor = con1.Anchor = Anchors.All;

            con1.Channels.Add(new ConsoleChannel(0, "General", Color.Orange));
            con1.Channels.Add(new ConsoleChannel(1, "Private", Color.White));
            con1.Channels.Add(new ConsoleChannel(2, "System", Color.Yellow));

            // We want to share channels and message buffer in both consoles
            con2.Channels = con1.Channels;
            con2.MessageBuffer = con1.MessageBuffer;

            // In the second console we display only "Private" messages
            con2.ChannelFilter.Add(1);

            // Select default channels for each tab
            con1.SelectedChannel = 0;
            con2.SelectedChannel = 1;

            // Do we want to add timestamp or channel name at the start of every message?
            con1.MessageFormat = ConsoleMessageFormats.All;
            con2.MessageFormat = ConsoleMessageFormats.TimeStamp;

            // Handler for altering incoming message
            con1.MessageSent += con1_MessageSent;

            // We send initial welcome message to System channel
            con1.MessageBuffer.Add(new ConsoleMessage("Application", "Welcome to Neoforce!", 2));

            Manager.Add(tbc);
        }

        private void InitControls()
        {
            sidebar = new SideBar(Manager);
            sidebar.Init();
            sidebar.StayOnBack = true;
            sidebar.Passive = true;
            sidebar.Width = 200;
            sidebar.Height = ClientHeight;
            sidebar.Anchor = Anchors.Left | Anchors.Top | Anchors.Bottom;

            Add(sidebar);

            InitRes();
            InitTasks();
            InitStats();
            InitConsole();
        }

        private void InitRes()
        {
            pnlRes = new SideBarPanel(Manager);
            pnlRes.Init();
            pnlRes.Passive = true;
            pnlRes.Parent = sidebar;
            pnlRes.Left = 16;
            pnlRes.Top = 16;
            pnlRes.Width = sidebar.Width - pnlRes.Left;
            pnlRes.Height = 86;
            pnlRes.CanFocus = false;

            rdbRes1024 = new RadioButton(Manager);
            rdbRes1024.Init();
            rdbRes1024.Parent = pnlRes;
            rdbRes1024.Left = 8;
            rdbRes1024.Width = pnlRes.Width - rdbRes1024.Left * 2;
            rdbRes1024.Height = 16;
            rdbRes1024.Text = "Resolution 1024x768";
            rdbRes1024.Top = 8;
            rdbRes1024.Checked = true;

            rdbRes1280 = new RadioButton(Manager);
            rdbRes1280.Init();
            rdbRes1280.Parent = pnlRes;
            rdbRes1280.Left = rdbRes1024.Left;
            rdbRes1280.Width = rdbRes1024.Width;
            rdbRes1280.Height = rdbRes1024.Height;
            rdbRes1280.Text = "Resolution 1280x1024";
            rdbRes1280.Top = 24;

            rdbRes1680 = new RadioButton(Manager);
            rdbRes1680.Init();
            rdbRes1680.Parent = pnlRes;
            rdbRes1680.Left = rdbRes1024.Left;
            rdbRes1680.Width = rdbRes1024.Width;
            rdbRes1680.Height = rdbRes1024.Height;
            rdbRes1680.Text = "Resolution 1680x1050";
            rdbRes1680.Top = 40;

            chkResFull = new CheckBox(Manager);
            chkResFull.Parent = pnlRes;
            chkResFull.Init();
            chkResFull.Left = rdbRes1024.Left;
            chkResFull.Width = rdbRes1024.Width;
            chkResFull.Height = rdbRes1024.Height;
            chkResFull.Text = "Fullscreen Mode";
            chkResFull.Top = 64;

            btnApply = new Button(Manager);
            btnApply.Init();
            btnApply.Width = 80;
            btnApply.Parent = sidebar;
            btnApply.Left = pnlRes.Left;
            btnApply.Top = pnlRes.Top + pnlRes.Height + 8;
            btnApply.Text = "Apply";
            btnApply.Click += btnApply_Click;

            btnExit = new Button(Manager);
            btnExit.Init();
            btnExit.Width = 80;
            btnExit.Parent = sidebar;
            btnExit.Left = btnApply.Left + btnApply.Width + 8;
            btnExit.Top = pnlRes.Top + pnlRes.Height + 8;
            btnExit.Text = "Exit";
            btnExit.Click += btnExit_Click;
        }

        private void InitStats()
        {
            pnlStats = new SideBarPanel(Manager);
            pnlStats.Init();
            pnlStats.Passive = true;
            pnlStats.Parent = sidebar;
            pnlStats.Left = 16;
            pnlStats.Width = sidebar.Width - pnlStats.Left;
            pnlStats.Height = 64;
            pnlStats.Top = ClientHeight - 16 - pnlStats.Height;
            pnlStats.Anchor = Anchors.Left | Anchors.Bottom;
            pnlStats.CanFocus = false;

            lblObjects = new Label(Manager);
            lblObjects.Init();
            lblObjects.Parent = pnlStats;
            lblObjects.Left = 8;
            lblObjects.Top = 8;
            lblObjects.Height = 16;
            lblObjects.Width = pnlStats.Width - lblObjects.Left * 2;
            ;
            lblObjects.Alignment = Alignment.MiddleLeft;

            lblAvgFps = new Label(Manager);
            lblAvgFps.Init();
            lblAvgFps.Parent = pnlStats;
            lblAvgFps.Left = 8;
            lblAvgFps.Top = 24;
            lblAvgFps.Height = 16;
            lblAvgFps.Width = pnlStats.Width - lblObjects.Left * 2;
            lblAvgFps.Alignment = Alignment.MiddleLeft;

            lblFps = new Label(Manager);
            lblFps.Init();
            lblFps.Parent = pnlStats;
            lblFps.Left = 8;
            lblFps.Top = 40;
            lblFps.Height = 16;
            lblFps.Width = pnlStats.Width - lblObjects.Left * 2;
            lblFps.Alignment = Alignment.MiddleLeft;
        }

        private void InitTasks()
        {
            pnlTasks = new SideBarPanel(Manager);
            pnlTasks.Init();
            pnlTasks.Passive = true;
            pnlTasks.Parent = sidebar;
            pnlTasks.Left = 16;
            pnlTasks.Width = sidebar.Width - pnlRes.Left;
            pnlTasks.Height = (TasksCount * 25) + 16;
            pnlTasks.Top = btnApply.Top + btnApply.Height + 16;
            pnlTasks.CanFocus = false;

            btnTasks = new Button[TasksCount];
            for (var i = 0; i < TasksCount; i++)
            {
                btnTasks[i] = new Button(Manager);
                btnTasks[i].Init();
                btnTasks[i].Parent = pnlTasks;
                btnTasks[i].Left = 8;
                btnTasks[i].Top = 8 + i * (btnTasks[i].Height + 1);
                btnTasks[i].Width = -8 + btnApply.Width * 2;
                btnTasks[i].Click += btnTask_Click;
                btnTasks[i].Text = "Task [" + i + "]";
                if (Tasks.Length >= i - 1 && Tasks[i] != "") btnTasks[i].Text = Tasks[i];
            }

            btnRandom = new Button(Manager);
            btnRandom.Init();
            btnRandom.Parent = sidebar;
            btnRandom.Width = 80;
            btnRandom.Left = 16;
            btnRandom.Top = pnlTasks.Top + pnlTasks.Height + 8;
            btnRandom.Text = "Random";
            btnRandom.Click += btnRandom_Click;

            btnClose = new Button(Manager);
            btnClose.Init();
            btnClose.Width = 80;
            btnClose.Parent = sidebar;
            btnClose.Left = btnRandom.Left + btnRandom.Width + 8;
            btnClose.Top = pnlTasks.Top + pnlTasks.Height + 8;
            ;
            btnClose.Text = "Close";
            btnClose.Click += btnClose_Click;
        }
    }
}
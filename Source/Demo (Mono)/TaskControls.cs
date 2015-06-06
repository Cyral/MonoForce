using System;
using Microsoft.Xna.Framework;
using MonoForce.Controls;
using EventArgs = MonoForce.Controls.EventArgs;

namespace MonoForce.Demo
{
    public class TaskControls : Dialog
    {
        private readonly Button btnDisable;
        private readonly Button btnProgress;
        private readonly CheckBox chkBorders;
        private readonly CheckBox chkReadOnly;
        private readonly ComboBox cmbMain;
        private readonly GroupPanel grpEdit;
        private readonly Label lblEdit;
        private readonly Label lblTrack;
        private readonly ListBox lstMain;
        private readonly ContextMenu mnuListBox;
        private readonly MainMenu mnuMain;
        private readonly Panel pnlControls;
        private readonly ProgressBar prgMain;
        private readonly RadioButton rdbNormal;
        private readonly RadioButton rdbPassword;
        private readonly SpinBox spnMain;
        private readonly TrackBar trkMain;
        private readonly TextBox txtEdit;

        public TaskControls(Manager manager) : base(manager)
        {
            MinimumWidth = 340;
            MinimumHeight = 140;
            Height = 480;
            Center();
            Text = "Controls Test";

            TopPanel.Visible = true;
            Caption.Text = "Information";
            Description.Text = "Demonstration of various controls available in Neoforce Controls library.";
            Caption.TextColor = Description.TextColor = new Color(96, 96, 96);

            grpEdit = new GroupPanel(Manager);
            grpEdit.Init();
            grpEdit.Parent = this;
            grpEdit.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;
            grpEdit.Width = ClientWidth - 200;
            grpEdit.Height = 160;
            grpEdit.Left = 8;
            grpEdit.Top = TopPanel.Height + 8;
            grpEdit.Text = "EditBox";

            pnlControls = new Panel(Manager);
            pnlControls.Init();
            pnlControls.Passive = true;
            pnlControls.Parent = this;
            pnlControls.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;
            pnlControls.Left = 8;
            pnlControls.Top = grpEdit.Top + grpEdit.Height + 8;
            pnlControls.Width = ClientWidth - 200;
            pnlControls.Height = BottomPanel.Top - 32 - pnlControls.Top;
            pnlControls.BevelBorder = BevelBorder.All;
            pnlControls.BevelMargin = 1;
            pnlControls.BevelStyle = BevelStyle.Etched;
            pnlControls.Color = Color.Transparent;

            lblEdit = new Label(manager);
            lblEdit.Init();
            lblEdit.Parent = grpEdit;
            lblEdit.Left = 16;
            lblEdit.Top = 8;
            lblEdit.Text = "Testing field:";
            lblEdit.Width = 128;
            lblEdit.Height = 16;

            txtEdit = new TextBox(manager);
            txtEdit.Init();
            txtEdit.Parent = grpEdit;
            txtEdit.Left = 16;
            txtEdit.Top = 24;
            txtEdit.Width = grpEdit.ClientWidth - 32;
            txtEdit.Height = 20;
            txtEdit.Anchor = Anchors.Left | Anchors.Top | Anchors.Right | Anchors.Bottom;
            txtEdit.Placeholder = "Text";

            rdbNormal = new RadioButton(manager);
            rdbNormal.Init();
            rdbNormal.Parent = grpEdit;
            rdbNormal.Left = 16;
            rdbNormal.Top = 52;
            rdbNormal.Width = grpEdit.ClientWidth - 32;
            rdbNormal.Anchor = Anchors.Left | Anchors.Bottom | Anchors.Right;
            rdbNormal.Checked = true;
            rdbNormal.Text = "Normal mode";
            rdbNormal.ToolTip.Text = "Enables normal mode for TextBox control.";
            rdbNormal.CheckedChanged += ModeChanged;

            rdbPassword = new RadioButton(manager);
            rdbPassword.Init();
            rdbPassword.Parent = grpEdit;
            rdbPassword.Left = 16;
            rdbPassword.Top = 68;
            rdbPassword.Width = grpEdit.ClientWidth - 32;
            rdbPassword.Anchor = Anchors.Left | Anchors.Bottom | Anchors.Right;
            rdbPassword.Checked = false;
            rdbPassword.Text = "Password mode";
            rdbPassword.ToolTip.Text = "Enables password mode for TextBox control.";
            rdbPassword.CheckedChanged += ModeChanged;

            chkBorders = new CheckBox(manager);
            chkBorders.Init();
            chkBorders.Parent = grpEdit;
            chkBorders.Left = 16;
            chkBorders.Top = 96;
            chkBorders.Width = grpEdit.ClientWidth - 32;
            chkBorders.Anchor = Anchors.Left | Anchors.Bottom | Anchors.Right;
            chkBorders.Checked = false;
            chkBorders.Text = "Borderless mode";
            chkBorders.ToolTip.Text = "Enables or disables borderless mode for TextBox control.";
            chkBorders.CheckedChanged += chkBorders_CheckedChanged;

            chkReadOnly = new CheckBox(manager);
            chkReadOnly.Init();
            chkReadOnly.Parent = grpEdit;
            chkReadOnly.Left = 16;
            chkReadOnly.Top = 110;
            chkReadOnly.Width = grpEdit.ClientWidth - 32;
            chkReadOnly.Anchor = Anchors.Left | Anchors.Bottom | Anchors.Right;
            chkReadOnly.Checked = false;
            chkReadOnly.Text = "Read only mode";
            chkReadOnly.ToolTip.Text =
                "Enables or disables read only mode for TextBox control.\nThis mode is necessary to enable explicitly.";
            chkReadOnly.CheckedChanged += chkReadOnly_CheckedChanged;

            string[] colors =
            {
                "Red", "Green", "Blue", "Yellow", "Orange", "Purple", "White", "Black", "Magenta", "Cyan",
                "Brown", "Aqua", "Beige", "Coral", "Crimson", "Gray", "Azure", "Ivory", "Indigo", "Khaki",
                "Orchid", "Plum", "Salmon", "Silver", "Gold", "Pink", "Linen", "Lime", "Olive", "Slate"
            };

            spnMain = new SpinBox(manager, SpinBoxMode.List);
            spnMain.Init();
            spnMain.Parent = pnlControls;
            spnMain.Left = 16;
            spnMain.Top = 16;
            spnMain.Width = pnlControls.Width - 32;
            spnMain.Height = 20;
            spnMain.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;
            spnMain.Items.AddRange(colors);
            spnMain.Mode = SpinBoxMode.Range;

            spnMain.ItemIndex = 0;

            cmbMain = new ComboBox(manager);
            cmbMain.Init();
            cmbMain.Parent = pnlControls;
            cmbMain.Left = 16;
            cmbMain.Top = 44;
            cmbMain.Width = pnlControls.Width - 32;
            cmbMain.Height = 20;
            cmbMain.ReadOnly = true;
            cmbMain.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;
            cmbMain.Items.AddRange(colors);
            cmbMain.ItemIndex = 0;
            cmbMain.MaxItems = 5;
            cmbMain.ToolTip.Color = Color.Yellow;
            cmbMain.Movable = cmbMain.Resizable = true;
            cmbMain.OutlineMoving = cmbMain.OutlineResizing = true;

            trkMain = new TrackBar(manager);
            trkMain.Init();
            trkMain.Parent = pnlControls;
            trkMain.Left = 16;
            trkMain.Top = 72;
            trkMain.Width = pnlControls.Width - 32;
            trkMain.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;
            trkMain.Range = 64;
            trkMain.Value = 16;
            trkMain.ValueChanged += trkMain_ValueChanged;

            lblTrack = new Label(manager);
            lblTrack.Init();
            lblTrack.Parent = pnlControls;
            lblTrack.Left = 16;
            lblTrack.Top = 96;
            lblTrack.Width = pnlControls.Width - 32;
            lblTrack.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;
            lblTrack.Alignment = Alignment.TopRight;
            lblTrack.TextColor = new Color(32, 32, 32);
            trkMain_ValueChanged(this, null); // forcing label redraw with init values

            mnuListBox = new ContextMenu(manager);

            var i1 = new MenuItem("This is very long text");
            var i2 = new MenuItem("Menu", true);
            var i3 = new MenuItem("Item", false);
            //i3.Enabled = false;
            var i4 = new MenuItem("Separated", true);

            var i11 = new MenuItem();
            var i12 = new MenuItem();
            var i13 = new MenuItem();
            var i14 = new MenuItem();

            var i111 = new MenuItem();
            var i112 = new MenuItem();
            var i113 = new MenuItem();

            mnuListBox.Items.AddRange(new[] { i1, i2, i3, i4 });
            i2.Items.AddRange(new[] { i11, i12, i13, i14 });
            i13.Items.AddRange(new[] { i111, i112, i113 });


            lstMain = new ListBox(manager);
            lstMain.Init();
            lstMain.Parent = this;
            lstMain.Top = TopPanel.Height + 8;
            lstMain.Left = grpEdit.Left + grpEdit.Width + 8;
            lstMain.Width = ClientWidth - lstMain.Left - 8;
            lstMain.Height = ClientHeight - 16 - BottomPanel.Height - TopPanel.Height;
            lstMain.Anchor = Anchors.Top | Anchors.Right | Anchors.Bottom;
            lstMain.HideSelection = false;
            lstMain.Items.AddRange(colors);
            lstMain.ContextMenu = mnuListBox;

            prgMain = new ProgressBar(manager);
            prgMain.Init();
            prgMain.Parent = BottomPanel;
            prgMain.Left = lstMain.Left;
            prgMain.Top = 10;
            prgMain.Width = lstMain.Width;
            prgMain.Height = 16;
            prgMain.Anchor = Anchors.Top | Anchors.Right;
            prgMain.Mode = ProgressBarMode.Infinite;
            prgMain.Passive = false;

            btnDisable = new Button(manager);
            btnDisable.Init();
            btnDisable.Parent = BottomPanel;
            btnDisable.Left = 8;
            btnDisable.Top = 8;
            btnDisable.Text = "Disable";
            btnDisable.Click += btnDisable_Click;
            btnDisable.TextColor = Color.FromNonPremultiplied(255, 64, 32, 200);

            btnProgress = new Button(manager);
            btnProgress.Init();
            btnProgress.Parent = BottomPanel;
            btnProgress.Left = prgMain.Left - 16;
            btnProgress.Top = prgMain.Top;
            btnProgress.Height = 16;
            btnProgress.Width = 16;
            btnProgress.Text = "!";
            btnProgress.Anchor = Anchors.Top | Anchors.Right;
            btnProgress.Click += btnProgress_Click;

            mnuMain = new MainMenu(manager);

            mnuMain.Items.Add(i2);
            mnuMain.Items.Add(i13);
            mnuMain.Items.Add(i3);
            mnuMain.Items.Add(i4);

            MainMenu = mnuMain;

            var tlp = new ToolBarPanel(manager);
            ToolBarPanel = tlp;

            var tlb = new ToolBar(manager);
            var tlbx = new ToolBar(manager);
            tlb.FullRow = true;
            tlbx.Row = 1;
            tlbx.FullRow = false;

            tlp.Add(tlb);
            tlp.Add(tlbx);

            /*
      tlb.Init();         
      tlb.Width = 256;
      tlb.Parent = ToolBarPanel;*/


            //tlbx.Init();
            /*
      tlbx.Width = 512;
      tlbx.Top = 25;      
      tlbx.Parent = ToolBarPanel;*/

            /* 
      ToolBarButton tb1 = new ToolBarButton(manager);
      tb1.Init();
      tb1.Parent = tlb;
      tb1.Left = 10;
      tb1.Top = 1;
      tb1.Glyph = new Glyph(Manager.Skin.Images["Icon.Warning"].Resource);      
      tb1.Glyph.SizeMode = SizeMode.Stretched;  */

            var stb = new StatusBar(Manager);
            StatusBar = stb;

            DefaultControl = txtEdit;

            OutlineMoving = true;
            OutlineResizing = true;

            BottomPanel.BringToFront();

            SkinChanged += TaskControls_SkinChanged;
            TaskControls_SkinChanged(null, null);
        }

        private void TaskControls_SkinChanged(object sender, EventArgs e)
        {
#if (!XBOX && !XBOX_FAKE)
            prgMain.Cursor = Manager.Skin.Cursors["Busy"].Resource;
#endif
        }

        private void ModeChanged(object sender, EventArgs e)
        {
            if (sender == rdbNormal)
            {
                txtEdit.Mode = TextBoxMode.Normal;
            }
            else if (sender == rdbPassword)
            {
                txtEdit.Mode = TextBoxMode.Password;
            }
        }

        private void chkReadOnly_CheckedChanged(object sender, EventArgs e)
        {
            txtEdit.ReadOnly = chkReadOnly.Checked;
        }

        private void chkBorders_CheckedChanged(object sender, EventArgs e)
        {
            txtEdit.DrawBorders = !chkBorders.Checked;
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            if (txtEdit.Enabled)
            {
                btnDisable.Text = "Enable";
                btnDisable.TextColor = Color.FromNonPremultiplied(64, 255, 32, 200);
            }
            else
            {
                btnDisable.Text = "Disable";
                btnDisable.TextColor = Color.FromNonPremultiplied(255, 64, 32, 200);
            }
            ClientArea.Enabled = !ClientArea.Enabled;

            BottomPanel.Enabled = true;

            prgMain.Enabled = ClientArea.Enabled;
        }

        private void btnProgress_Click(object sender, EventArgs e)
        {
            if (prgMain.Mode == ProgressBarMode.Default) prgMain.Mode = ProgressBarMode.Infinite;
            else prgMain.Mode = ProgressBarMode.Default;

            lstMain.Items.Add(new Random().Next().ToString());
            lstMain.ItemIndex = lstMain.Items.Count - 1;
            cmbMain.Text = "!!!";
        }

        private void trkMain_ValueChanged(object sender, EventArgs e)
        {
            if (lblTrack != null)
            {
                lblTrack.Text = trkMain.Value + "/" + trkMain.Range;
            }
        }
    }
}
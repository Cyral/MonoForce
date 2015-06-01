using System;
using System.Threading;
using MonoForce.Controls;
using EventArgs = MonoForce.Controls.EventArgs;

namespace MonoForce.Demo
{
    public partial class MainWindow
    {
        private const int TasksCount = 5;

        private readonly string[] Tasks = new string[TasksCount]
        {"Dialog Template", "Controls Test", "Auto Scrolling", "Layout Window", "Events Test"};

        private void btnClose_Click(object sender, EventArgs e)
        {
            var list = new ControlsList();
            list.AddRange(Manager.Controls);

            for (var i = 0; i < list.Count; i++)
            {
                if (list[i] is Window)
                {
                    if (((Window)list[i]).Text.Substring(0, 6) == "Window")
                    {
                        (list[i] as Window).Dispose();
                    }
                }
            }
            list.Clear();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            var win = new Window(Manager);
            var btn = new Button(Manager);
            var txt = new TextBox(Manager);

            win.Init();
            btn.Init();
            txt.Init();

            win.ClientWidth = 320;
            win.ClientHeight = 160;

            win.MinimumWidth = 128;
            win.MinimumHeight = 128;

            var r = new Random((int)Central.Frames);
            win.ClientWidth += r.Next(-100, +100);
            win.ClientHeight += r.Next(-100, +100);

            win.Left = r.Next(200, Manager.ScreenWidth - win.ClientWidth / 2);
            win.Top = r.Next(0, Manager.ScreenHeight - win.ClientHeight / 2);
            win.Closed += win_Closed;

            /*
      win.Width = 1024;
      win.Height = 768;
      win.Left = 220;
      win.Top = 0;
      win.StayOnBack = true;
      win.SendToBack();
*/
            btn.Anchor = Anchors.Bottom;
            btn.Left = (win.ClientWidth / 2) - (btn.Width / 2);
            btn.Top = win.ClientHeight - btn.Height - 8;
            btn.Text = "OK";

            win.Text = "Window (" + win.Width + "x" + win.Height + ")";

            txt.Parent = win;
            txt.Left = 8;
            txt.Top = 8;
            txt.Width = win.ClientArea.Width - 16;
            txt.Height = win.ClientArea.Height - 48;
            txt.Anchor = Anchors.All;
            txt.Mode = TextBoxMode.Multiline;
            txt.Text = "This is a Multiline TextBox.\n" +
                       "Allows to edit large texts,\n" +
                       "copy text to and from clipboard,\n" +
                       "select text with mouse or keyboard\n" +
                       "and much more...";

            txt.SelectAll();
            txt.Focused = true;
            //txt.ReadOnly = true;

            txt.ScrollBars = ScrollBars.Both;

            win.Add(btn, true);
            win.Show();
            Manager.Add(win);
        }

        private void win_Closed(object sender, WindowClosedEventArgs e)
        {
            e.Dispose = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Manager.Graphics.IsFullScreen = chkResFull.Checked;

            var w = 1024;
            var h = 768;

            if (rdbRes1024.Checked)
            {
                w = 1024;
                h = 768;
            }
            else if (rdbRes1280.Checked)
            {
                w = 1280;
                h = 1024;
            }
            else if (rdbRes1680.Checked)
            {
                w = 1680;
                h = 1050;
            }

            Manager.Graphics.PreferredBackBufferWidth = w;
            Manager.Graphics.PreferredBackBufferHeight = h;

            Manager.Graphics.ApplyChanges();
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            if (sender == btnTasks[0])
            {
#if (!XBOX && !XBOX_FAKE)
                Manager.Cursor = Manager.Skin.Cursors["Busy"].Resource;
#endif

                btnTasks[0].Enabled = false;
                var tmp = new TaskDialog(Manager);
                tmp.Closing += WindowClosing;
                tmp.Closed += WindowClosed;
                tmp.Init();
                Manager.Add(tmp);

                tmp.Show();

#if (!XBOX && !XBOX_FAKE)
                Manager.Cursor = Manager.Skin.Cursors["Default"].Resource;
#endif
            }
            else if (sender == btnTasks[1])
            {
                btnTasks[1].Enabled = false;
                var tmp = new TaskControls(Manager);
                tmp.Closing += WindowClosing;
                tmp.Closed += WindowClosed;
                tmp.Init();
                Manager.Add(tmp);
                tmp.ShowModal();
            }
            else if (sender == btnTasks[2])
            {
                btnTasks[2].Enabled = false;
                var tmp = new TaskAutoScroll(Manager);
                tmp.Closing += WindowClosing;
                tmp.Closed += WindowClosed;
                tmp.Init();
                Manager.Add(tmp);
                tmp.Show();
            }
            else if (sender == btnTasks[3])
            {
                btnTasks[3].Enabled = false;

                var tmp = (Window)Layout.Load(Manager, "Window");
                tmp.Closing += WindowClosing;
                tmp.Closed += WindowClosed;
                tmp.Init();
                tmp.GetControl("btnOk").Click += Central_Click;
                Manager.Add(tmp);
                tmp.Show();
            }
            else if (sender == btnTasks[4])
            {
                btnTasks[4].Enabled = false;

                var tmp = new TaskEvents(Manager);
                tmp.Closing += WindowClosing;
                tmp.Closed += WindowClosed;
                tmp.Init();
                Manager.Add(tmp);
                tmp.Show();
            }
        }

        private void Central_Click(object sender, EventArgs e)
        {
            ((sender as Button).Root as Window).Close();
        }

        private void WindowClosing(object sender, WindowClosingEventArgs e)
        {
            //e.Cancel = true; 
        }

        private void WindowClosed(object sender, WindowClosedEventArgs e)
        {
            if (sender is TaskDialog)
            {
                btnTasks[0].Enabled = true;
                btnTasks[0].Focused = true;
            }
            else if (sender is TaskControls)
            {
                btnTasks[1].Enabled = true;
                btnTasks[1].Focused = true;
            }
            else if (sender is TaskAutoScroll)
            {
                btnTasks[2].Enabled = true;
                btnTasks[2].Focused = true;
            }
            else if (sender is Window && (sender as Window).Name == "frmMain")
            {
                btnTasks[3].Enabled = true;
                btnTasks[3].Focused = true;
            }
            else if (sender is TaskEvents)
            {
                btnTasks[4].Enabled = true;
                btnTasks[4].Focused = true;
            }
            e.Dispose = true;
        }
    }
}
//using System;

using MonoForce.Controls;

namespace MonoForce.Demo
{
    public class TaskEvents : Window
    {
        private readonly Button btn;
        private readonly ListBox lst;
        private readonly ListBox txt;

        public TaskEvents(Manager manager) : base(manager)
        {
            Height = 360;
            MinimumHeight = 99;
            MinimumWidth = 78;
            Text = "Events Test";
            Center();

            btn = new Button(manager);
            btn.Init();
            btn.Parent = this;
            btn.Left = 20;
            btn.Top = 20;
            btn.MouseMove += btn_MouseMove;
            btn.MouseDown += btn_MouseDown;
            btn.MouseUp += btn_MouseUp;
            btn.MouseOver += btn_MouseOver;
            btn.MouseOut += btn_MouseOut;
            btn.MousePress += btn_MousePress;
            btn.Click += btn_Click;

            lst = new ListBox(manager);
            lst.Init();
            lst.Parent = this;
            lst.Left = 20;
            lst.Top = 60;
            lst.Width = 128;
            lst.Height = 128;
            lst.MouseMove += btn_MouseMove;
            lst.MouseDown += btn_MouseDown;
            lst.MouseUp += btn_MouseUp;
            lst.MouseOver += btn_MouseOver;
            lst.MouseOut += btn_MouseOut;
            lst.MousePress += btn_MousePress;
            lst.MouseScroll += lst_MouseScroll;
            lst.Click += btn_Click;

            txt = new ListBox(manager);
            txt.Init();
            txt.Parent = this;
            txt.Left = 200;
            txt.Top = 8;
            txt.Width = 160;
            txt.Height = 300;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            var ex = (e is MouseEventArgs) ? (MouseEventArgs)e : new MouseEventArgs();
            txt.Items.Add((sender == btn ? "Button" : "List") + ": Click " + ex.Button);
            txt.ItemIndex = txt.Items.Count - 1;
        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            txt.Items.Add((sender == btn ? "Button" : "List") + ": Down " + e.Button);
            txt.ItemIndex = txt.Items.Count - 1;
        }

        private void btn_MouseMove(object sender, MouseEventArgs e)
        {
            txt.Items.Add((sender == btn ? "Button" : "List") + ": Move");
            txt.ItemIndex = txt.Items.Count - 1;
        }

        private void btn_MouseOut(object sender, MouseEventArgs e)
        {
            txt.Items.Add((sender == btn ? "Button" : "List") + ": Out");
            txt.ItemIndex = txt.Items.Count - 1;
        }

        private void btn_MouseOver(object sender, MouseEventArgs e)
        {
            txt.Items.Add((sender == btn ? "Button" : "List") + ": Over");
            txt.ItemIndex = txt.Items.Count - 1;
        }

        private void btn_MousePress(object sender, MouseEventArgs e)
        {
            //  txt.Items.Add((sender == btn ? "Button" : "List") + ": Press");
            //  txt.ItemIndex = txt.Items.Count - 1;
        }

        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            txt.Items.Add((sender == btn ? "Button" : "List") + ": Up " + e.Button);
            txt.ItemIndex = txt.Items.Count - 1;
        }

        private void lst_MouseScroll(object sender, MouseEventArgs e)
        {
            txt.Items.Add((sender == btn ? "Button" : "List") + ": Scroll");
            txt.ItemIndex = txt.Items.Count - 1;
        }
    }
}
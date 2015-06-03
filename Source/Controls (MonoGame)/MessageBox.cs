using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoForce.Controls
{
    public enum MessageBoxType
    {
        Okay,
        Cancel,
        YesNo,
        Error,
        YesNoCancel,
        Warning,
    }

    /// <summary>
    /// A message box with different button styles.
    /// </summary>
    public class MessageBox : Dialog
    {

        public List<Button> buttons { get; set; }
        public Label lblMessage { get; set; }
        public ImageBox imgIcon { get; set; }
        public string Message
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; }
        }
        public string Title
        {
            get { return Text; }
            set { Text = value; }
        }

        public MessageBox(Manager manager, MessageBoxType Type, string message, string title)
            : base(manager)
        {

            ClientWidth = (int)Manager.Skin.Controls["Label"].Layers[0].Text.Font.Resource.MeasureString(message).X + 48 + 16 + 16 + 16;
            ClientHeight = 120;
            TopPanel.Visible = false;
            IconVisible = true;
            Resizable = false;
            if (title == string.Empty)

                Text = Manager.Game.Window.Title;
            else
                Title = title;
            Center();

            imgIcon = new ImageBox(Manager);
            imgIcon.Init();
            imgIcon.Image = Manager.Skin.Images["Icon.Question"].Resource;
            imgIcon.Left = 16;
            imgIcon.Top = 16;
            imgIcon.Width = 48;
            imgIcon.Height = 48;
            imgIcon.SizeMode = SizeMode.Stretched;

            lblMessage = new Label(Manager);
            lblMessage.Init();

            lblMessage.Left = 80;
            lblMessage.Top = 16;
            lblMessage.Width = ClientWidth - lblMessage.Left;
            lblMessage.Height = (int)Manager.Skin.Fonts["Default8"].Resource.MeasureRichString(message, Manager).Y;
            lblMessage.Alignment = Alignment.MiddleCenter;
            lblMessage.Text = message;

            BottomPanel.Show();
            BottomPanel.Visible = true;
            buttons = new List<Button>();
            if (Type == MessageBoxType.Okay || Type == MessageBoxType.Cancel)
            {
                buttons.Add(new Button(Manager));
                buttons[0].Init();

                buttons[0].Top = 8;
                buttons[0].Left = (BottomPanel.ClientWidth / 2) - (buttons[0].Width / 2);
                buttons[0].Text = (Type == MessageBoxType.Okay ? "Okay" : "Cancel");

                buttons[0].ModalResult = (Type == MessageBoxType.Okay ? ModalResult.Ok : ModalResult.Cancel);


                imgIcon.Image = Manager.Skin.Images["Icon.Information"].Resource;
                BottomPanel.Add(buttons[0]);
            }
            else if (Type == MessageBoxType.Error)
            {
                buttons.Add(new Button(Manager));
                buttons[0].Init();

                buttons[0].Top = 8;
                buttons[0].Left = (BottomPanel.ClientWidth / 2) - (buttons[0].Width / 2);
                buttons[0].Text = "Okay";

                buttons[0].ModalResult = ModalResult.Ok;
                imgIcon.Image = Manager.Skin.Images["Icon.Error"].Resource;
                BottomPanel.Add(buttons[0]);
            }
            else if (Type == MessageBoxType.Warning)
            {
                buttons.Add(new Button(Manager));
                buttons[0].Init();
                buttons[0].Top = 8;
                buttons[0].Left = (BottomPanel.ClientWidth / 2) - (buttons[0].Width / 2);
                buttons[0].Text = "Okay";

                buttons[0].ModalResult = ModalResult.Ok;
                imgIcon.Image = Manager.Skin.Images["Icon.Warning"].Resource;
                BottomPanel.Add(buttons[0]);
            }
            else if (Type == MessageBoxType.YesNo)
            {
                buttons.Add(new Button(Manager));
                buttons[0].Init();

                buttons[0].Top = 8;
                buttons[0].Left = (BottomPanel.ClientWidth / 2) - buttons[0].Width - 4;
                buttons[0].Text = "Yes";

                buttons[0].ModalResult = ModalResult.Yes;

                buttons.Add(new Button(Manager));
                buttons[1].Init();

                buttons[1].Top = buttons[0].Top;
                buttons[1].Left = (BottomPanel.ClientWidth / 2) + 4;
                buttons[1].Text = "No";


                buttons[1].ModalResult = ModalResult.No;
                imgIcon.Image = Manager.Skin.Images["Icon.Question"].Resource;
                BottomPanel.Add(buttons[0]);
                BottomPanel.Add(buttons[1]);
            }
            else if (Type == MessageBoxType.YesNoCancel)
            {
                buttons.Add(new Button(Manager));
                buttons[0].Init();
                buttons[0].Text = "Yes";
                buttons[0].Top = 8;
                buttons[0].Left = (BottomPanel.ClientWidth / 2) - (int)((buttons[0].Width) * 1.5f) - 4;

                buttons[0].ModalResult = ModalResult.Yes;

                buttons.Add(new Button(Manager));
                buttons[1].Init();
                buttons[1].Text = "No";
                buttons[1].Top = buttons[0].Top;
                buttons[1].Left = (BottomPanel.ClientWidth / 2) - (buttons[0].Width / 2);

                buttons[1].ModalResult = ModalResult.No;

                buttons.Add(new Button(Manager));
                buttons[2].Init();
                buttons[2].Text = "Cancel";
                buttons[2].Top = buttons[0].Top;
                buttons[2].Left = (BottomPanel.ClientWidth / 2) + (int)((buttons[0].Width) * .5f) + 4;
                buttons[2].ModalResult = ModalResult.Cancel;



                imgIcon.Image = Manager.Skin.Images["Icon.Question"].Resource;
                BottomPanel.Add(buttons[0]);
                BottomPanel.Add(buttons[1]);
                BottomPanel.Add(buttons[2]);
            }

            Add(imgIcon);
            Add(lblMessage);


            DefaultControl = buttons[0];
        }
        public override void Init()
        {
            base.Init();
        }

    }
}

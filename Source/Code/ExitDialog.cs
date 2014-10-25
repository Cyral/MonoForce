namespace MonoForce.Controls
{
    /// <summary>
    /// Dialog window asking users to confirm or cancel an exit operation.
    /// </summary>
    public class ExitDialog : Dialog
    {

        #region Fields

        /// <summary>
        /// Yes button.
        /// </summary>
        private Button btnYes;
        /// <summary>
        /// No button.
        /// </summary>
        private Button btnNo;
        /// <summary>
        /// Do you want to exit message.
        /// </summary>
        private Label lblMessage;
        /// <summary>
        /// Dialog window icon image.
        /// </summary>
        private ImageBox imgIcon;

        #endregion

        #region Construstors

        /// <summary>
        /// Creates a new instance of the exit dialog window.
        /// </summary>
        /// <param name="manager">GUI manager for the dialog window.</param>
        public ExitDialog(Manager manager, string customMessage = "")
            : base(manager)
        {
            string msg = customMessage;
            if (customMessage == string.Empty)
                msg = "Do you really want to exit " + Manager.Game.Window.Title + "?";

            ClientWidth = (int)Manager.Skin.Controls["Label"].Layers[0].Text.Font.Resource.MeasureString(msg).X + 48 + 16 + 16 + 16;
            ClientHeight = 120;
            TopPanel.Visible = false;
            IconVisible = true;
            Resizable = false;
            Text = Manager.Game.Window.Title;
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
            lblMessage.Height = 48;
            lblMessage.Alignment = Alignment.TopLeft;
            lblMessage.Text = msg;

            btnYes = new Button(Manager);
            btnYes.Init();
            btnYes.Left = (BottomPanel.ClientWidth / 2) - btnYes.Width - 4;
            btnYes.Top = 8;
            btnYes.Text = "Yes";
            btnYes.ModalResult = ModalResult.Yes;

            btnNo = new Button(Manager);
            btnNo.Init();
            btnNo.Left = (BottomPanel.ClientWidth / 2) + 4;
            btnNo.Top = 8;
            btnNo.Text = "No";
            btnNo.ModalResult = ModalResult.No;

            Add(imgIcon);
            Add(lblMessage);
            BottomPanel.Add(btnYes);
            BottomPanel.Add(btnNo);

            DefaultControl = btnNo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the exit dialog window.
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        #endregion

    }
}

using Microsoft.Xna.Framework.Input;

namespace MonoForce.Controls
{
    public class ModalContainer : Container
    {
        /// <summary>
        /// Indicates if the container is modal or not.
        /// </summary>
        public virtual bool IsModal
        {
            get { return Manager.ModalWindow == this; }

        }

        /// <summary>
        /// which button of the dialog was pressed.)
        /// Gets or sets the result of the modal dialog. (Usually indicating
        /// </summary>
        public virtual ModalResult ModalResult
        {
            get
            {
                return modalResult;

            }
            set
            {
                modalResult = value;

            }

        }

        /// <summary>
        /// Indicates if the modal container is visible or not.
        /// </summary>
        public override bool Visible
        {
            get
            {
                return base.Visible;

            }
            set
            {
                if (value) Focused = true;
                base.Visible = value;

            }

        }

        /// <summary>
        /// Parent modal control, if there is any.
        /// </summary>
        private ModalContainer lastModal;

        /// <summary>
        /// Indicates the result of the modal dialog.
        /// </summary>
        private ModalResult modalResult = ModalResult.None;

        public ModalContainer(Manager manager) : base(manager)
        {
        }

        /// <summary>
        /// Closes the modal dialog.
        /// </summary>
        public virtual void Close()
        {
            var ex = new WindowClosingEventArgs();
            OnClosing(ex);
            if (!ex.Cancel)
            {
                Manager.Input.KeyDown -= Input_KeyDown;
                Manager.ModalWindow = lastModal;
                if (lastModal != null) lastModal.Focused = true;
                Hide();
                var ev = new WindowClosedEventArgs();
                OnClosed(ev);

                if (ev.Dispose)
                {
                    Dispose();

                }

            }

        }

        /// <param name="modalResult">Dialog result to close the window with.</param>
        /// <summary>
        /// Closes the modal dialog with the specified result.
        /// </summary>
        public virtual void Close(ModalResult modalResult)
        {
            ModalResult = modalResult;
            Close();

        }

        /// <summary>
        /// Occurs when the dialog has finished closing.
        /// </summary>
        public event WindowClosedEventHandler Closed;

        /// <summary>
        /// Occurs when the dialog is beginning to close.
        /// </summary>
        public event WindowClosingEventHandler Closing;

        /// <summary>
        /// Shows the control as a modal dialog.
        /// </summary>
        public virtual void ShowModal()
        {
            lastModal = Manager.ModalWindow;
            Manager.ModalWindow = this;
            Manager.Input.KeyDown += Input_KeyDown;
        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles the closed event of the modal container.
        /// </summary>
        protected virtual void OnClosed(WindowClosedEventArgs e)
        {
            if (Closed != null) Closed.Invoke(this, e);

        }

        /// <param name="e"></param>
        /// <summary>
        /// Handles the closing of the modal container control.
        /// </summary>
        protected virtual void OnClosing(WindowClosingEventArgs e)
        {
            if (Closing != null) Closing.Invoke(this, e);

        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <summary>
        /// Handles key press events for the modal container.
        /// </summary>
        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (Visible && (Manager.FocusedControl != null && Manager.FocusedControl.Root == this) &&
                e.Key == Keys.Escape)
            {

            }
        }
    }
}

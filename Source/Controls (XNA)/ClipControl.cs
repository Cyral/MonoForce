using Microsoft.Xna.Framework;

namespace MonoForce.Controls
{
    public class ClipControl : Control
    {
        /// </summary>
        /// Gets or sets the client area of the clip control.
        /// </summary>
        public virtual ClipBox ClientArea
        {
            get { return clientArea; }
            set { clientArea = value; }
        }

        /// </summary>
        /// Gets or sets the client area margins of the clip control.
        /// </summary>
        public override Margins ClientMargins
        {
            get { return base.ClientMargins; }
            set
            {
                base.ClientMargins = value;
// Update client area dimensions.
                if (clientArea != null)
                {
                    clientArea.Left = ClientLeft;
                    clientArea.Top = ClientTop;
                    clientArea.Width = ClientWidth;
                    clientArea.Height = ClientHeight;
                }
            }
        }

        /// </summary>
        /// Client area of the clip control.
        /// </summary>
        private ClipBox clientArea;

        public ClipControl(Manager manager) : base(manager)
        {
            clientArea = new ClipBox(manager);

            clientArea.Init();
            clientArea.MinimumWidth = 0;
            clientArea.MinimumHeight = 0;
            clientArea.Left = ClientLeft;
            clientArea.Top = ClientTop;
            clientArea.Width = ClientWidth;
            clientArea.Height = ClientHeight;

            base.Add(clientArea);
        }

        /// clip box (true) or the a direct child of the clip control itself (false)
        /// </param>
        /// <param name="client">
        /// Indicates if the control to add will be a child of the client area
        /// <param name="control">Child control to add to the clip control.</param>
        /// </summary>
        /// Adds a child control to the clip control.
        /// </summary>
        public virtual void Add(Control control, bool client)
        {
            if (client)
            {
                clientArea.Add(control);
            }
            else
            {
                base.Add(control);
            }
        }

        /// <param name="control">Child control to add to the clip control.</param>
        /// </summary>
        /// Adds a child control to the clip box.
        /// </summary>
        public override void Add(Control control)
        {
            Add(control, true);
        }

        /// </summary>
        /// Initializes the clip control.
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        public override void Remove(Control control)
        {
            base.Remove(control);
            clientArea.Remove(control);
        }

        /// </summary>
        /// Initializes the skin for the clip control.
        /// </summary>
        protected internal override void InitSkin()
        {
            base.InitSkin();
        }

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// </summary>
        /// Updates the clip control and its child controls.
        /// </summary>
        protected internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// </summary>
        /// Adjusts the margins of the clip control.
        /// </summary>
        protected virtual void AdjustMargins()
        {
        }

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            base.DrawControl(renderer, rect, gameTime);
        }

        /// <param name="e"></param>
        /// </summary>
        /// Handles resize events for the clip control.
        /// </summary>
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

// Update client area dimensions.
            if (clientArea != null)
            {
                clientArea.Left = ClientLeft;
                clientArea.Top = ClientTop;
                clientArea.Width = ClientWidth;
                clientArea.Height = ClientHeight;
            }
        }
    }
}

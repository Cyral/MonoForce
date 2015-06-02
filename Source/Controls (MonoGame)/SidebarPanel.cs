namespace MonoForce.Controls
{
    public class SideBarPanel : Container
    {
        public SideBarPanel(Manager manager) : base(manager)
        {
            CanFocus = false;
            Passive = true;
            Width = 64;
            Height = 64;
        }

        /// </summary>
        /// Initializes the side bar panel control.
        /// <summary>
        public override void Init()
        {
            base.Init();
        }
    }
}
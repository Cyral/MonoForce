using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;
#if (!XBOX && !XBOX_FAKE)
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Media;

#endif


[assembly: CLSCompliant(false)]

namespace MonoForce.Controls
{
    public class Manager : DrawableGameComponent
    {
        /// </summary>
        /// Name of the default skin file asset.
        /// </summary>
        internal const string _DefaultSkin = "Default";

        /// </summary>
        /// will be interpreted as a double click event.
        /// Maximum delay between two click events. Two clicks occuring within this limit
        /// </summary>
        internal const int _DoubleClickTime = 500;

        /// </summary>
        /// Directory in the content project where the manager expects to find layout files.
        /// </summary>
        internal const string _LayoutDirectory = ".\\Content\\Layout\\";

        /// </summary>
        /// Amount of milliseconds to delay the initial display of menus.
        /// </summary>
        internal const int _MenuDelay = 500;

        /// </summary>
        /// Indicates how the manager's render target data is used when a new render target is set.
        /// </summary>
        internal const RenderTargetUsage _RenderTargetUsage = RenderTargetUsage.DiscardContents;

        /// </summary>
        /// Directory in the content project where the manager expects to find skin files.
        /// </summary>
        internal const string _SkinDirectory = ".\\Content\\Skins\\";

        /// </summary>
        /// Extension of valid skin file archives.
        /// </summary>
        internal const string _SkinExtension = ".skin";

        /// </summary>
        /// Increment at which a texture can be resized.
        /// </summary>
        internal const int _TextureResizeIncrement = 32;

        /// </summary>
        /// tip is displayed.
        /// Amount of milliseconds the mouse cursor has to hover a control before its tool
        /// </summary>
        internal const int _ToolTipDelay = 500;

        /// </summary>
        /// Gets or sets a value indicating whether the Manager should create render target automatically.
        /// </summary>
        public virtual bool AutoCreateRenderTarget
        {
            get { return autoCreateRenderTarget; }
            set { autoCreateRenderTarget = value; }
        }

        /// </summary>
        /// Gets or sets a value indicating if a control should unfocus if you click outside on the screen.
        /// </summary>
        public virtual bool AutoUnfocus
        {
            get { return autoUnfocus; }
            set { autoUnfocus = value; }
        }

        /// </summary>
        /// Returns list of components added to the manager.
        /// </summary>
        public virtual IEnumerable<Component> Components
        {
            get { return components; }
        }

        /// </summary>
        /// Returns
        /// <see cref="ArchiveManager" />
        /// used for loading assets.
        /// </summary>
        public virtual ArchiveManager Content
        {
            get { return content; }
        }

        /// </summary>
        /// Returns list of controls added to the manager.
        /// </summary>
        public virtual IEnumerable<Control> Controls
        {
            get { return controls; }
        }

        /// </summary>
        /// Gets a value indicating whether Manager is in the process of disposing.
        /// </summary>
        public virtual bool Disposing
        {
            get { return disposing; }
        }

        /// </summary>
        /// Gets or sets the maximum number of milliseconds that can elapse between a first click and a second click to consider the mouse action a double-click.
        /// </summary>
        public virtual int DoubleClickTime
        {
            get { return doubleClickTime; }
            set { doubleClickTime = value; }
        }

        /// </summary>
        /// Gets or sets the currently focused control.
        /// </summary>
        public virtual Control FocusedControl
        {
            get { return focusedControl; }
            internal set
            {
                if (value != null && value.Visible && value.Enabled)
                {
                    if (value != null && value.CanFocus)
                    {
                        if (focusedControl == null || (focusedControl != null && value.Root != focusedControl.Root) ||
                            !value.IsRoot)
                        {
                            if (focusedControl != null && focusedControl != value)
                            {
                                focusedControl.Focused = false;
                            }
                            focusedControl = value;
                        }
                    }
                    else if (value != null && !value.CanFocus)
                    {
                        if (focusedControl != null && value.Root != focusedControl.Root)
                        {
                            if (focusedControl != value.Root)
                            {
                                focusedControl.Focused = false;
                            }
                            focusedControl = value.Root;
                        }
                        else if (focusedControl == null)
                        {
                            focusedControl = value.Root;
                        }
                    }
                    BringToFront(value.Root);
                }
                else if (value == null)
                {
                    focusedControl = value;
                }
            }
        }

        /// </summary>
        /// Returns associated
        /// <see cref="Game" />
        /// component.
        /// </summary>
        public new virtual Game Game
        {
            get { return base.Game; }
        }

        /// </summary>
        /// Gets or sets the depth value used for rendering sprites.
        /// </summary>
        public virtual float GlobalDepth
        {
            get { return globalDepth; }
            set { globalDepth = value; }
        }

        /// </summary>
        /// Returns associated
        /// <see cref="GraphicsDeviceManager" />
        /// .
        /// </summary>
        public virtual GraphicsDeviceManager Graphics
        {
            get { return graphics; }
        }

        /// </summary>
        /// Returns associated
        /// <see cref="GraphicsDevice" />
        /// .
        /// </summary>
        public new virtual GraphicsDevice GraphicsDevice
        {
            get { return base.GraphicsDevice; }
        }

        /// </summary>
        /// Returns
        /// <see cref="InputSystem" />
        /// instance responsible for managing user input.
        /// </summary>
        public virtual InputSystem Input
        {
            get { return input; }
        }

        /// </summary>
        /// Enables or disables input processing.
        /// </summary>
        public virtual bool InputEnabled
        {
            get { return inputEnabled; }
            set { inputEnabled = value; }
        }

        /// </summary>
        /// Gets or sets current keyboard layout for proper text inputs.
        /// </summary>
        public virtual KeyboardLayout KeyboardLayout
        {
            get
            {
                if (keyboardLayout == null)
                {
#if (!XBOX && !XBOX_FAKE)
                    var id = InputLanguage.CurrentInputLanguage.Culture.KeyboardLayoutId;
                    for (var i = 0; i < keyboardLayouts.Count; i++)
                    {
                        if (keyboardLayouts[i].LayoutList.Contains(id))
                        {
                            return keyboardLayouts[i];
                        }
                    }
#endif
                    keyboardLayout = new KeyboardLayout();
                }
                return keyboardLayout;
            }
            set { keyboardLayout = value; }
        }

        /// </summary>
        /// Gets or sets collection of active keyboard layouts.
        /// </summary>
        public virtual List<KeyboardLayout> KeyboardLayouts
        {
            get { return keyboardLayouts; }
            set { keyboardLayouts = value; }
        }

        /// </summary>
        /// Gets or sets the directory where layout files are located.
        /// </summary>
        public virtual string LayoutDirectory
        {
            get
            {
                if (!layoutDirectory.EndsWith("\\"))
                {
                    layoutDirectory += "\\";
                }
                return layoutDirectory;
            }
            set
            {
                layoutDirectory = value;
                if (!layoutDirectory.EndsWith("\\"))
                {
                    layoutDirectory += "\\";
                }
            }
        }

        /// </summary>
        /// Enables or disables logging of unhandled exceptions.
        /// </summary>
        public virtual bool LogUnhandledExceptions
        {
            get { return logUnhandledExceptions; }
            set { logUnhandledExceptions = value; }
        }

        /// </summary>
        /// Gets or sets the time that passes before a submenu appears when hovered over menu item.
        /// </summary>
        public virtual int MenuDelay
        {
            get { return menuDelay; }
            set { menuDelay = value; }
        }

        /// </summary>
        /// Gets or sets the currently active modal window.
        /// </summary>
        public virtual ModalContainer ModalWindow
        {
            get { return modalWindow; }
            internal set
            {
                modalWindow = value;

                if (value != null)
                {
                    value.ModalResult = ModalResult.None;

                    value.Visible = true;
                    value.Focused = true;
                }
            }
        }

        /// </summary>
        /// Returns
        /// <see cref="Renderer" />
        /// used for rendering controls.
        /// </summary>
        public virtual Renderer Renderer
        {
            get { return renderer; }
        }

        public virtual RenderTarget2D RenderTarget
        {
            get { return renderTarget; }
            set { renderTarget = value; }
        }

        /// </summary>
        /// Gets current height of the screen in pixels.
        /// </summary>
        public virtual int ScreenHeight
        {
            get
            {
                if (GraphicsDevice != null)
                {
                    return GraphicsDevice.PresentationParameters.BackBufferHeight;
                }
                return 0;
            }
        }

        /// </summary>
        /// Gets current width of the screen in pixels.
        /// </summary>
        public virtual int ScreenWidth
        {
            get
            {
                if (GraphicsDevice != null)
                {
                    return GraphicsDevice.PresentationParameters.BackBufferWidth;
                }
                return 0;
            }
        }

        /// </summary>
        /// Gets or sets the skin applied to all controls.
        /// </summary>
        public virtual Skin Skin
        {
            get { return skin; }
            set { SetSkin(value); }
        }

        /// </summary>
        /// Gets or sets the directory where skin files are located.
        /// </summary>
        public virtual string SkinDirectory
        {
            get
            {
                if (!skinDirectory.EndsWith("\\"))
                {
                    skinDirectory += "\\";
                }
                return skinDirectory;
            }
            set
            {
                skinDirectory = value;
                if (!skinDirectory.EndsWith("\\"))
                {
                    skinDirectory += "\\";
                }
            }
        }

        /// </summary>
        /// Gets or sets file extension for archived skin files.
        /// </summary>
        public string SkinExtension
        {
            get
            {
                if (!skinExtension.StartsWith("."))
                {
                    skinExtension = "." + skinExtension;
                }
                return skinExtension;
            }
            set
            {
                skinExtension = value;
                if (!skinExtension.StartsWith("."))
                {
                    skinExtension = "." + skinExtension;
                }
            }
        }

        /// </summary>
        /// Gets or sets update interval for drawing, logic and input.
        /// </summary>
        public virtual int TargetFrames
        {
            get { return targetFrames; }
            set { targetFrames = value; }
        }

        /// </summary>
        /// Gets height of the selected render target in pixels.
        /// </summary>
        public virtual int TargetHeight
        {
            get
            {
                if (renderTarget != null)
                {
                    return renderTarget.Height;
                }
                return ScreenHeight;
            }
        }

        /// </summary>
        /// Gets width of the selected render target in pixels.
        /// </summary>
        public virtual int TargetWidth
        {
            get
            {
                if (renderTarget != null)
                {
                    return renderTarget.Width;
                }
                return ScreenWidth;
            }
        }

        /// </summary>
        /// Gets or sets texture size increment in pixel while performing controls resizing.
        /// </summary>
        public virtual int TextureResizeIncrement
        {
            get { return textureResizeIncrement; }
            set { textureResizeIncrement = value; }
        }

        /// </summary>
        /// Gets or sets the time that passes before the
        /// <see cref="ToolTip" />
        /// appears.
        /// </summary>
        public virtual int ToolTipDelay
        {
            get { return toolTipDelay; }
            set { toolTipDelay = value; }
        }

        /// </summary>
        /// Enables or disables showing of tooltips globally.
        /// </summary>
        public virtual bool ToolTipsEnabled
        {
            get { return toolTipsEnabled; }
            set { toolTipsEnabled = value; }
        }

        /// </summary>
        /// Gets or sets a value indicating if Guide component can be used
        /// </summary>
        public bool UseGuide
        {
            get { return useGuide; }
            set { useGuide = value; }
        }

        /// </summary>
        /// Gets the list of visible controls being managed.
        /// </summary>
        internal virtual ControlsList OrderList
        {
            get { return orderList; }
        }

        /// </summary>
        /// List of all components being managed.
        /// </summary>
        private readonly List<Component> components;

        /// </summary>
        /// List of all controls being managed.
        /// </summary>
        private readonly ControlsList controls;

        /// </summary>
        /// Graphics device manager for the application.
        /// </summary>
        private readonly GraphicsDeviceManager graphics;

        /// </summary>
        /// List of visible controls in the controls list.
        /// </summary>
        private readonly ControlsList orderList;

        /// </summary>
        /// Layout file version the manager expects to work with.
        /// </summary>
        internal Version _LayoutVersion = new Version(0, 7);

        /// </summary>
        /// Skin version the manager expects to work with.
        /// </summary>
        internal Version _SkinVersion = new Version(0, 7);

        /// </summary>
        /// Indicates if the manager's render target should be created automatically when initialized.
        /// </summary>
        private bool autoCreateRenderTarget = true;

        /// </summary>
        /// Indicates if controls automatically unfocus when a new control is navigated to. ???
        /// </summary>
        private bool autoUnfocus = true;

        /// </summary>
        /// Archive content manager for loading skin files.
        /// </summary>
        private ArchiveManager content;

        /// </summary>
        /// Indicates if the graphics device has been reset.
        /// </summary>
        private bool deviceReset;

        /// </summary>
        /// Indicates if the manager is being disposed.
        /// </summary>
        private bool disposing;

        /// </summary>
        /// Indicates how fast two click events must occur to trigger a double click event.
        /// </summary>
        private int doubleClickTime = _DoubleClickTime;

        /// </summary>
        /// Tracks elapsed time to control drawing to match target FPS.
        /// </summary>
        private long drawTime;

        /// </summary>
        /// Application control that currently has input focus.
        /// </summary>
        private Control focusedControl;

        /// </summary>
        /// Global depth value used when drawing controls.
        /// </summary>
        private float globalDepth;

        /// </summary>
        /// Handles input device updates for the application.
        /// </summary>
        private InputSystem input;

        /// </summary>
        /// Indicates if the manager is responding to user input.
        /// </summary>
        private bool inputEnabled = true;

        /// </summary>
        /// Active keyboard layout.
        /// </summary>
        private KeyboardLayout keyboardLayout;

        /// </summary>
        /// List of keyboard layouts available for the application.
        /// </summary>
        private List<KeyboardLayout> keyboardLayouts = new List<KeyboardLayout>();

        /// </summary>
        /// Directory where layout files are located.
        /// </summary>
        private string layoutDirectory = _LayoutDirectory;

        /// </summary>
        /// Indicates if unhandled exceptions should be logged to file.
        /// </summary>
        private bool logUnhandledExceptions = true;

        /// </summary>
        /// Indicates how long the display of a menu is delayed.
        /// </summary>
        private int menuDelay = _MenuDelay;

        /// </summary>
        /// Current modal window displayed, if any.
        /// </summary>
        private ModalContainer modalWindow;

        /// </summary>
        /// Render manager for the application.
        /// </summary>
        private Renderer renderer;

        /// </summary>
        /// Render target where control drawing takes place before being displayed.
        /// </summary>
        private RenderTarget2D renderTarget;

        private bool renderTargetValid;

        /// </summary>
        /// Current skin being applied to the application controls.
        /// </summary>
        private Skin skin;

        /// </summary>
        /// Directory where skin files are located.
        /// </summary>
        private string skinDirectory = _SkinDirectory;

        /// </summary>
        /// Extension of skin files.
        /// </summary>
        private string skinExtension = _SkinExtension;

        /// </summary>
        /// Name of the skin file being used.
        /// </summary>
        private string skinName = _DefaultSkin;

        /// </summary>
        /// ???
        /// </summary>
        private ControlStates states;

        /// </summary>
        /// Application's target FPS.
        /// </summary>
        private int targetFrames = 60;

        /// </summary>
        /// Increment which textures can be resized at.
        /// </summary>
        private int textureResizeIncrement = _TextureResizeIncrement;

        /// </summary>
        /// Indicates how long a mouse has to hover a control in order to activate its tool tip.
        /// </summary>
        private int toolTipDelay = _ToolTipDelay;

        /// </summary>
        /// Indicates if the application can use tool tips.
        /// </summary>
        private bool toolTipsEnabled = true;

        /// </summary>
        /// Tracks elapsed time to control frequency of updates.
        /// </summary>
        private long updateTime;

        /// </summary>
        /// Indicates if the guide component can be used.
        /// </summary>
        private bool useGuide;

        public Manager(Game game, GraphicsDeviceManager graphics, string skin) : base(game)
        {
            disposing = false;

            AppDomain.CurrentDomain.UnhandledException += HandleUnhadledExceptions;

#if (!XBOX && !XBOX_FAKE)
            menuDelay = SystemInformation.MenuShowDelay;
            doubleClickTime = SystemInformation.DoubleClickTime;
#endif

#if (!XBOX && !XBOX_FAKE)
            window = (Form)System.Windows.Forms.Control.FromHandle(Game.Window.Handle);
            window.FormClosing += Window_FormClosing;
#endif

            content = new ArchiveManager(Game.Services);
            input = new InputSystem(this, new InputOffset(0, 0, 1f, 1f));
            components = new List<Component>();
            controls = new ControlsList();
            orderList = new ControlsList();

            this.graphics = graphics;
            graphics.PreparingDeviceSettings += PrepareGraphicsDevice;

            skinName = skin;

#if (XBOX_FAKE)
game.Window.Title += " (XBOX_FAKE)";
#endif

            states.Buttons = new Control[32];
            states.Click = -1;
            states.Over = null;

// Hook up the input system mouse events.
            input.MouseDown += MouseDownProcess;
            input.MouseUp += MouseUpProcess;
            input.MousePress += MousePressProcess;
            input.MouseMove += MouseMoveProcess;
            input.MouseScroll += MouseScrollProcess;

// Hook up the input system gamepad events.
            input.GamePadDown += GamePadDownProcess;
            input.GamePadUp += GamePadUpProcess;
            input.GamePadPress += GamePadPressProcess;

// Hook up the input system key events.
            input.KeyDown += KeyDownProcess;
            input.KeyUp += KeyUpProcess;
            input.KeyPress += KeyPressProcess;

// Create the English (US), Czech, and German keyboard layouts.
            keyboardLayouts.Add(new KeyboardLayout());
            keyboardLayouts.Add(new CzechKeyboardLayout());
            keyboardLayouts.Add(new GermanKeyboardLayout());
        }

        public Manager(Game game, string skin)
            : this(game, game.Services.GetService(typeof (IGraphicsDeviceManager)) as GraphicsDeviceManager, skin)
        {
        }

        public Manager(Game game, GraphicsDeviceManager graphics) : this(game, graphics, _DefaultSkin)
        {
        }

        public Manager(Game game)
            : this(
                game, game.Services.GetService(typeof (IGraphicsDeviceManager)) as GraphicsDeviceManager, _DefaultSkin)
        {
        }

        /// </param>
        /// The component or control being added.
        /// <param name="component">
        /// </summary>
        /// Adds a component or a control to the manager.
        /// </summary>
        public virtual void Add(Component component)
        {
// Component exists?
            if (component != null)
            {
// Component is a Control not already in the list?
                if (component is Control && !controls.Contains(component as Control))
                {
                    var c = (Control)component;

// Remove control from parent list because it's under new management now.
                    if (c.Parent != null) c.Parent.Remove(c);

                    controls.Add(c);
                    c.Manager = this;
                    c.Parent = null;
// New control gains focus unless another control already has it.
                    if (focusedControl == null) c.Focused = true;

                    DeviceSettingsChanged += (component as Control).OnDeviceSettingsChanged;
                    SkinChanging += (component as Control).OnSkinChanging;
                    SkinChanged += (component as Control).OnSkinChanged;
                }
// Component is not a control and doesn't already exist in the component list?
                else if (!(component is Control) && !components.Contains(component))
                {
                    components.Add(component);
                    component.Manager = this;
                }
            }
        }

        /// </param>
        /// Time passed since the last call to Draw.
        /// <param name="gameTime">
        /// </summary>
        /// Renders all controls added to the manager.
        /// </summary>
        public virtual void BeginDraw(GameTime gameTime)
        {
            if (!renderTargetValid && AutoCreateRenderTarget)
            {
                if (renderTarget != null) RenderTarget.Dispose();
                RenderTarget = CreateRenderTarget();
                renderer = new Renderer(this);
            }
            Draw(gameTime);
        }

        /// </param>
        /// The control being brought to the front.
        /// <param name="control">
        /// </summary>
        /// Brings the control to the front of the z-order.
        /// </summary>
        public virtual void BringToFront(Control control)
        {
// Valid control and allowed to be in front?
            if (control != null && !control.StayOnBack)
            {
                var cs = (control.Parent == null) ? controls : control.Parent.Controls as ControlsList;
                if (cs.Contains(control))
                {
                    cs.Remove(control);
                    if (!control.StayOnTop)
                    {
                        var pos = cs.Count;
                        for (var i = cs.Count - 1; i >= 0; i--)
                        {
                            if (!cs[i].StayOnTop)
                            {
                                break;
                            }
                            pos = i;
                        }
                        cs.Insert(pos, control);
                    }
// Remove from components list.
                    else
                    {
                        cs.Add(control);
                    }
                }
            }
        }

        /// <returns>Returns the created render target.</returns>
        /// </summary>
        /// Creates a render target.
        /// </summary>
        public virtual RenderTarget2D CreateRenderTarget()
        {
            return CreateRenderTarget(ScreenWidth, ScreenHeight);
        }

        /// <returns>Returns the created render target.</returns>
        /// <param name="height">Height of the new render target.</param>
        /// <param name="width">Width of the new render target.</param>
        /// </summary>
        /// Creates a render target with the specified dimensions.
        /// </summary>
        public virtual RenderTarget2D CreateRenderTarget(int width, int height)
        {
            Input.InputOffset = new InputOffset(0, 0, ScreenWidth / (float)width, ScreenHeight / (float)height);
            return new RenderTarget2D(GraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None,
                GraphicsDevice.PresentationParameters.MultiSampleCount, _RenderTargetUsage);
        }

        /// </summary>
        /// Occurs when the GraphicsDevice settings are changed.
        /// </summary>
        public event DeviceEventHandler DeviceSettingsChanged;

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// </summary>
        /// Draws the manager's controls.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            if (renderTarget != null)
            {
// Update the draw timer.
                drawTime += gameTime.ElapsedGameTime.Ticks;
                var ms = TimeSpan.FromTicks(drawTime).TotalMilliseconds;

// Time to draw a new frame yet?
                if (targetFrames == 0 || (ms == 0 || ms >= (1000f / targetFrames)))
                {
                    var span = TimeSpan.FromTicks(drawTime);
                    gameTime = new GameTime(gameTime.TotalGameTime, span);
                    drawTime = 0;

// Are there controls to draw?
                    if ((controls != null))
                    {
                        var list = new ControlsList();
                        list.AddRange(controls);

                        foreach (var c in list)
                        {
// Draw each control to its render target.
                            c.PrepareTexture(renderer, gameTime);
                        }

// Draw all controls on the manager's render target and display them.
                        GraphicsDevice.SetRenderTarget(renderTarget);
                        GraphicsDevice.Clear(Color.Transparent);

                        if (renderer != null)
                        {
                            foreach (var c in list)
                            {
                                c.Render(renderer, gameTime);
                            }
                        }
                    }

                    GraphicsDevice.SetRenderTarget(null);
                }
            }
// Remove from components list.
            else
            {
                throw new Exception(
                    "Manager.RenderTarget has to be specified. Assign a render target or set Manager.AutoCreateRenderTarget property to true.");
            }
        }

        /// </summary>
        /// Draws texture resolved from RenderTarget used for rendering.
        /// </summary>
        public virtual void EndDraw()
        {
            EndDraw(new Rectangle(0, 0, ScreenWidth, ScreenHeight));
        }

        /// </summary>
        /// Draws texture resolved from RenderTarget to specified rectangle.
        /// </summary>
        public virtual void EndDraw(Rectangle rect)
        {
            if (renderTarget != null && !deviceReset)
            {
                renderer.Begin(BlendingMode.Default);
                renderer.Draw(RenderTarget, rect, Color.White);
                renderer.End();
            }
            else if (deviceReset)
            {
                deviceReset = false;
            }
        }

        /// <returns>Returns the control with the specified name.</returns>
        /// <param name="name">Name of the control to retrieve.</param>
        /// </summary>
        /// Gets the control with the specified name.
        /// </summary>
        public virtual Control GetControl(string name)
        {
// Make sure no control can be larger than the back buffer.
            foreach (var c in Controls)
            {
                if (c.Name.ToLower() == name.ToLower())
                {
                    return c;
                }
            }
            return null;
        }

        /// </summary>
        /// Initializes the control manager.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

// Should we create the render target or let someone else handle it?
            if (autoCreateRenderTarget)
            {
                if (renderTarget != null)
                {
                    renderTarget.Dispose();
                }
                renderTarget = CreateRenderTarget();
            }

            GraphicsDevice.DeviceReset += GraphicsDevice_DeviceReset;

            input.Initialize();
            renderer = new Renderer(this);
            SetSkin(skinName);
        }

        /// <param name="e">The exception to log.</param>
        /// </summary>
        /// Logs exceptions to files on Windows.
        /// </summary>
        public virtual void LogException(Exception e)
        {
#if (!XBOX && !XBOX_FAKE)
            var an = Assembly.GetEntryAssembly().Location;
            var asm = Assembly.GetAssembly(typeof (Manager));
            var path = Path.GetDirectoryName(an);
            var fn = path + "\\" + Path.GetFileNameWithoutExtension(asm.Location) + ".log";

            File.AppendAllText(fn, "////////////////////////////////////////////////////////////////\n" +
                                   "    Date: " + DateTime.Now + "\n" +
                                   "Assembly: " + Path.GetFileName(asm.Location) + "\n" +
                                   " Version: " + asm.GetName().Version + "\n" +
                                   " Message: " + e.Message + "\n" +
                                   "////////////////////////////////////////////////////////////////\n" +
                                   e.StackTrace + "\n" +
                                   "////////////////////////////////////////////////////////////////\n\n",
                Encoding.Default);
#endif
        }

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// </summary>
        /// Draws the controls on their respective render targets.
        /// </summary>
        public virtual void Prepare(GameTime gameTime)
        {
        }

        /// </param>
        /// The component or control being removed.
        /// <param name="component">
        /// </summary>
        /// Removes a component or a control from the manager.
        /// </summary>
        public virtual void Remove(Component component)
        {
// Component exists?
            if (component != null)
            {
// Remove from the control list?
                if (component is Control)
                {
                    var c = component as Control;
                    SkinChanging -= c.OnSkinChanging;
                    SkinChanged -= c.OnSkinChanged;
                    DeviceSettingsChanged -= c.OnDeviceSettingsChanged;

                    if (c.Focused) c.Focused = false;
                    controls.Remove(c);
                }
// Remove from components list.
                else
                {
                    components.Remove(component);
                }
            }
        }

        /// </param>
        /// The control being sent back.
        /// <param name="control">
        /// </summary>
        /// Sends the control to the back of the z-order.
        /// </summary>
        public virtual void SendToBack(Control control)
        {
            if (control != null && !control.StayOnTop)
            {
                var cs = (control.Parent == null) ? controls : control.Parent.Controls as ControlsList;
                if (cs.Contains(control))
                {
                    cs.Remove(control);
                    if (!control.StayOnBack)
                    {
                        var pos = 0;
                        for (var i = 0; i < cs.Count; i++)
                        {
                            if (!cs[i].StayOnBack)
                            {
                                break;
                            }
                            pos = i;
                        }
                        cs.Insert(pos, control);
                    }
// Remove from components list.
                    else
                    {
                        cs.Insert(0, control);
                    }
                }
            }
        }

        /// </param>
        /// The name of the skin being loaded.
        /// <param name="name">
        /// </summary>
        /// Sets and loads the new skin.
        /// </summary>
        public virtual void SetSkin(string name)
        {
            var skin = new Skin(this, name);
            SetSkin(skin);
        }

        /// </param>
        /// The skin being set.
        /// <param name="skin">
        /// </summary>
        /// Sets the new skin.
        /// </summary>
        public virtual void SetSkin(Skin skin)
        {
// Raise the skin changing event.
            if (SkinChanging != null) SkinChanging.Invoke(new EventArgs());

// Dispose old skin first?
            if (this.skin != null)
            {
                Remove(this.skin);
                this.skin.Dispose();
                this.skin = null;
                GC.Collect();
            }
// Set new skin.
            this.skin = skin;
            this.skin.Init();
            Add(this.skin);
            skinName = this.skin.Name;

#if (!XBOX && !XBOX_FAKE)
            if (this.skin.Cursors["Default"] != null)
            {
                SetCursor(this.skin.Cursors["Default"].Resource);
            }
#endif

// Update control skins.
            InitSkins();
// Raise the skin changed event.
            if (SkinChanged != null) SkinChanged.Invoke(new EventArgs());

// Reinitialize all controls.
            InitControls();
        }

        /// </summary>
        /// Occurs when the skin changes.
        /// </summary>
        public event SkinEventHandler SkinChanged;

        /// </summary>
        /// Occurs when the skin is about to change.
        /// </summary>
        public event SkinEventHandler SkinChanging;

        /// </param>
        /// Time elapsed since the last call to Update.
        /// <param name="gameTime">
        /// </summary>
        /// Called when the manager needs to be updated.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            updateTime += gameTime.ElapsedGameTime.Ticks;
            var ms = TimeSpan.FromTicks(updateTime).TotalMilliseconds;

// Is it time to update yet?
            if (targetFrames == 0 || ms == 0 || ms >= (1000f / targetFrames))
            {
                var span = TimeSpan.FromTicks(updateTime);
                gameTime = new GameTime(gameTime.TotalGameTime, span);
                updateTime = 0;

// Update the state of the input devices?
                if (inputEnabled)
                {
                    input.Update(gameTime);
                }

// Disposing all components added to manager.
                if (components != null)
                {
                    foreach (var c in components)
                    {
                        c.Update(gameTime);
                    }
                }

                var list = new ControlsList(controls);

// Controls to update?
                if (list != null)
                {
                    foreach (var c in list)
                    {
                        c.Update(gameTime);
                    }
                }

                OrderList.Clear();
                SortLevel(controls);
            }
        }

        /// </summary>
        /// Occurs when game window is about to close.
        /// </summary>
        public event WindowClosingEventHandler WindowClosing;

        /// <param name="disposing"></param>
        /// </summary>
        /// Releases resources used by the manager.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.disposing = true;

// Recursively disposing all controls added to the manager and its child controls.
                if (controls != null)
                {
                    var c = controls.Count;
                    for (var i = 0; i < c; i++)
                    {
                        if (controls.Count > 0) controls[0].Dispose();
                    }
                }

// Disposing all components added to manager.
                if (components != null)
                {
                    var c = components.Count;
                    for (var i = 0; i < c; i++)
                    {
                        if (components.Count > 0) components[0].Dispose();
                    }
                }

// Dispose the content manager.
                if (content != null)
                {
                    content.Unload();
                    content.Dispose();
                    content = null;
                }

// And the renderer.
                if (renderer != null)
                {
                    renderer.Dispose();
                    renderer = null;
                }
// And the input system.
                if (input != null)
                {
                    input.Dispose();
                    input = null;
                }
            }
            if (GraphicsDevice != null)
                GraphicsDevice.DeviceReset -= GraphicsDevice_DeviceReset;
            base.Dispose(disposing);
        }

        /// </summary>
        /// Method used as an event handler for the GraphicsDeviceManager.PreparingDeviceSettings event.
        /// </summary>
        protected virtual void PrepareGraphicsDevice(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = _RenderTargetUsage;
// Get the dimensions of the back buffer.
            var w = e.GraphicsDeviceInformation.PresentationParameters.BackBufferWidth;
            var h = e.GraphicsDeviceInformation.PresentationParameters.BackBufferHeight;

// Make sure no control can be larger than the back buffer.
            foreach (var c in Controls)
            {
                SetMaxSize(c, w, h);
            }


            if (DeviceSettingsChanged != null) DeviceSettingsChanged.Invoke(new DeviceEventArgs(e));
        }

#if (!XBOX && !XBOX_FAKE)
        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Windows form closing event handler when running on windows.
        /// </summary>
        internal void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            var ret = false;

// Fire the window closing event.
            var ex = new WindowClosingEventArgs();
            if (WindowClosing != null)
            {
                WindowClosing.Invoke(this, ex);
                ret = ex.Cancel;
            }

// Cancel closing if necessary.
            e.Cancel = ret;
        }
#endif

        /// <returns></returns>
        /// <param name="index"></param>
        /// </summary>
        /// </summary>
        private bool CheckButtons(int index)
        {
            for (var i = 0; i < states.Buttons.Length; i++)
            {
                if (i == index) continue;
                if (states.Buttons[i] != null) return false;
            }

            return true;
        }

        /// <returns></returns>
        /// <param name="control"></param>
        /// </summary>
        /// </summary>
        private bool CheckDetached(Control control)
        {
            var ret = control.Detached;
            if (control.Parent != null)
            {
                if (CheckDetached(control.Parent)) ret = true;
            }
            return ret;
        }

        /// <returns></returns>
        /// <param name="pos"></param>
        /// <param name="control"></param>
        /// </summary>
        /// </summary>
        private bool CheckOrder(Control control, Point pos)
        {
            if (!CheckPosition(control, pos)) return false;

            for (var i = OrderList.Count - 1; i > OrderList.IndexOf(control); i--)
            {
                var c = OrderList[i];

                if (!c.Passive && CheckPosition(c, pos) && CheckParent(c, pos))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckParent(Control control, Point pos)
        {
            if (control.Parent != null && !CheckDetached(control))
            {
                var parent = control.Parent;
                var root = control.Root;

                var pr = new Rectangle(parent.AbsoluteLeft,
                    parent.AbsoluteTop,
                    parent.Width,
                    parent.Height);

                var margins = root.Skin.ClientMargins;
                var rr = new Rectangle(root.AbsoluteLeft + margins.Left,
                    root.AbsoluteTop + margins.Top,
                    root.OriginWidth - margins.Horizontal,
                    root.OriginHeight - margins.Vertical);


                return (rr.Contains(pos) && pr.Contains(pos));
            }

            return true;
        }

        private bool CheckPosition(Control control, Point pos)
        {
            return (control.AbsoluteLeft <= pos.X &&
                    control.AbsoluteTop <= pos.Y &&
                    control.AbsoluteLeft + control.Width >= pos.X &&
                    control.AbsoluteTop + control.Height >= pos.Y &&
                    CheckParent(control, pos));
        }


        /// <returns></returns>
        /// <param name="control"></param>
        /// </summary>
        /// </summary>
        private bool CheckState(Control control)
        {
            var modal = (ModalWindow == null) ? true : (ModalWindow == control.Root);

            return (control != null && !control.Passive && control.Visible && control.Enabled && modal);
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles gamepad button down events for the manager.
        /// </summary>
        private void GamePadDownProcess(object sender, GamePadEventArgs e)
        {
            var c = FocusedControl;

// Is there a focused control?
            if (c != null && CheckState(c))
            {
// Update the control's click state?
                if (states.Click == -1)
                {
                    states.Click = (int)e.Button;
                }
// Send the gamepad down message to the control.
                states.Buttons[(int)e.Button] = c;
                c.SendMessage(Message.GamePadDown, e);

// Need to send a click event message if the click button was pressed.
                if (e.Button == c.GamePadActions.Click)
                {
                    c.SendMessage(Message.Click, new MouseEventArgs(new MouseState(), MouseButton.None, Point.Zero));
                }
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles gamepad button presses for the manager.
        /// </summary>
        private void GamePadPressProcess(object sender, GamePadEventArgs e)
        {
            var c = states.Buttons[(int)e.Button];
            if (c != null)
            {
// Send the gamepad button press message to the control.
                c.SendMessage(Message.GamePadPress, e);

// Convert DPad buttons?
                if ((e.Button == c.GamePadActions.Right ||
                     e.Button == c.GamePadActions.Left ||
                     e.Button == c.GamePadActions.Up ||
                     e.Button == c.GamePadActions.Down) && !e.Handled && CheckButtons((int)e.Button))
                {
                    ProcessArrows(c, new KeyEventArgs(), e);
                    GamePadDownProcess(sender, e);
                }
// Switch to next control if RightTrigger is pressed.
                else if (e.Button == c.GamePadActions.NextControl && !e.Handled && CheckButtons((int)e.Button))
                {
                    TabNextControl(c);
                    GamePadDownProcess(sender, e);
                }
// Switch to previous control if LeftTrigger is pressed.
                else if (e.Button == c.GamePadActions.PrevControl && !e.Handled && CheckButtons((int)e.Button))
                {
                    TabPrevControl(c);
                    GamePadDownProcess(sender, e);
                }
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles gamepad button up events for the manager.
        /// </summary>
        private void GamePadUpProcess(object sender, GamePadEventArgs e)
        {
            var c = states.Buttons[(int)e.Button];

            if (c != null)
            {
// Send click event message to the control if the gamepad X button pressed?
                if (e.Button == c.GamePadActions.Press)
                {
                    c.SendMessage(Message.Click, new MouseEventArgs(new MouseState(), MouseButton.None, Point.Zero));
                }
                states.Click = -1;
                states.Buttons[(int)e.Button] = null;
// Send gamepad button up message to the control.
                c.SendMessage(Message.GamePadUp, e);
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Recreates the render target when the graphics device is reset.
        /// </summary>
        private void GraphicsDevice_DeviceReset(object sender, System.EventArgs e)
        {
            deviceReset = true;
            InvalidateRenderTarget();
/*if (AutoCreateRenderTarget)
{
if (renderTarget != null) RenderTarget.Dispose();
RenderTarget = CreateRenderTarget();
}
}*/
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles unhandled exceptions.
        /// </summary>
        private void HandleUnhadledExceptions(object sender, UnhandledExceptionEventArgs e)
        {
            if (LogUnhandledExceptions)
            {
                LogException(e.ExceptionObject as Exception);
            }
        }

        /// </summary>
        /// Initializes every single control created.
        /// </summary>
        private void InitControls()
        {
// not added to the manager or another parent.
// Initializing skins for every control created, even not visible or
            foreach (var c in Control.Stack)
            {
                c.Init();
            }
        }

        /// </summary>
        /// Initializes the skin of every single control created.
        /// </summary>
        private void InitSkins()
        {
// not added to the manager or another parent.
// Initializing skins for every control created, even not visible or
            foreach (var c in Control.Stack)
            {
                c.InitSkin();
            }
        }

        private void InvalidateRenderTarget()
        {
            renderTargetValid = false;
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles key down events for the manager.
        /// </summary>
        private void KeyDownProcess(object sender, KeyEventArgs e)
        {
            var c = FocusedControl;

// Is there a focused control?
            if (c != null && CheckState(c))
            {
// Update the control's click state?
                if (states.Click == -1)
                {
                    states.Click = (int)MouseButton.None;
                }
// Send the key down event message to the control.
                states.Buttons[(int)MouseButton.None] = c;
                c.SendMessage(Message.KeyDown, e);

// Send click event message to the control when the Enter key pressed.
                if (e.Key == Keys.Enter)
                {
                    c.SendMessage(Message.Click, new MouseEventArgs(new MouseState(), MouseButton.None, Point.Zero));
                }
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles key press events for the manager.
        /// </summary>
        private void KeyPressProcess(object sender, KeyEventArgs e)
        {
            var c = states.Buttons[(int)MouseButton.None];
            if (c != null)
            {
// Resend the key press message to the control.
                c.SendMessage(Message.KeyPress, e);

// Convert arrow keys to gamepad DPad buttons?
                if ((e.Key == Keys.Right ||
                     e.Key == Keys.Left ||
                     e.Key == Keys.Up ||
                     e.Key == Keys.Down) && !e.Handled && CheckButtons((int)MouseButton.None))
                {
                    ProcessArrows(c, e, new GamePadEventArgs(PlayerIndex.One));
                    KeyDownProcess(sender, e);
                }
// Tab key pressed? Switch to next control.
                else if (e.Key == Keys.Tab && !e.Shift && !e.Handled && CheckButtons((int)MouseButton.None))
                {
                    TabNextControl(c);
                    KeyDownProcess(sender, e);
                }
// Shift + Tab pressed? Switch to previous control.
                else if (e.Key == Keys.Tab && e.Shift && !e.Handled && CheckButtons((int)MouseButton.None))
                {
                    TabPrevControl(c);
                    KeyDownProcess(sender, e);
                }
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Handles key up events for the manager.
        /// </summary>
        private void KeyUpProcess(object sender, KeyEventArgs e)
        {
            var c = states.Buttons[(int)MouseButton.None];

            if (c != null)
            {
// Send mouse click event to the control on Space key released?
                if (e.Key == Keys.Space)
                {
                    c.SendMessage(Message.Click, new MouseEventArgs(new MouseState(), MouseButton.None, Point.Zero));
                }
                states.Click = -1;
                states.Buttons[(int)MouseButton.None] = null;
// Send key up event to the control.
                c.SendMessage(Message.KeyUp, e);
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Processes mouse button down events for the manager.
        /// </summary>
        private void MouseDownProcess(object sender, MouseEventArgs e)
        {
// Get the visible controls.
            var c = new ControlsList();
            c.AddRange(OrderList);

            if (autoUnfocus && focusedControl != null && focusedControl.Root != modalWindow)
            {
                var hit = false;

// Check managed controls for a hit.
                foreach (var cx in Controls)
                {
                    if (cx.AbsoluteRect.Contains(e.Position))
                    {
                        hit = true;
                        break;
                    }
                }
// No hit detected?
                if (!hit)
                {
// Check ALL controls for a hit.
                    for (var i = 0; i < Control.Stack.Count; i++)
                    {
                        if (Control.Stack[i].Visible && Control.Stack[i].Detached &&
                            Control.Stack[i].AbsoluteRect.Contains(e.Position))
                        {
                            hit = true;
                            break;
                        }
                    }
                }
// No other control hit? Unfocus focused control.
                if (!hit) focusedControl.Focused = false;
            }

// ???
            for (var i = c.Count - 1; i >= 0; i--)
            {
                if (CheckState(c[i]) && CheckPosition(c[i], e.Position))
                {
                    states.Buttons[(int)e.Button] = c[i];
                    c[i].SendMessage(Message.MouseDown, e);

// Update the control's click state?
                    if (states.Click == -1)
                    {
                        states.Click = (int)e.Button;

                        if (FocusedControl != null)
                        {
                            FocusedControl.Invalidate();
                        }
                        c[i].Focused = true;
                    }
                    return;
                }
            }

// Play system beep on Windows if the mouse click occurred outside the bounds of a modal window.
            if (ModalWindow != null)
            {
#if (!XBOX && !XBOX_FAKE)
                SystemSounds.Beep.Play();
#endif
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Processes mouse move events for the manager.
        /// </summary>
        private void MouseMoveProcess(object sender, MouseEventArgs e)
        {
// Get the visible controls.
            var c = new ControlsList();
            c.AddRange(OrderList);

// ???
            for (var i = c.Count - 1; i >= 0; i--)
            {
                var chpos = CheckPosition(c[i], e.Position);
                var chsta = CheckState(c[i]);

                if (chsta && ((chpos && states.Over == c[i]) || (states.Buttons[(int)e.Button] == c[i])))
                {
                    c[i].SendMessage(Message.MouseMove, e);
                    break;
                }
            }

// ???
            for (var i = c.Count - 1; i >= 0; i--)
            {
                var chpos = CheckPosition(c[i], e.Position);
                var chsta = CheckState(c[i]) || (c[i].ToolTip.Text != "" && c[i].ToolTip.Text != null && c[i].Visible);

                if (chsta && !chpos && states.Over == c[i] && states.Buttons[(int)e.Button] == null)
                {
                    states.Over = null;
                    c[i].SendMessage(Message.MouseOut, e);
                    break;
                }
            }

// ???
            for (var i = c.Count - 1; i >= 0; i--)
            {
                var chpos = CheckPosition(c[i], e.Position);
                var chsta = CheckState(c[i]) || (c[i].ToolTip.Text != "" && c[i].ToolTip.Text != null && c[i].Visible);

                if (chsta && chpos && states.Over != c[i] && states.Buttons[(int)e.Button] == null)
                {
                    if (states.Over != null)
                    {
                        states.Over.SendMessage(Message.MouseOut, e);
                    }
                    states.Over = c[i];
                    c[i].SendMessage(Message.MouseOver, e);
                    break;
                }
                if (states.Over == c[i]) break;
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Processes mouse button press events for the manager.
        /// </summary>
        private void MousePressProcess(object sender, MouseEventArgs e)
        {
            var c = states.Buttons[(int)e.Button];
            if (c != null)
            {
                if (CheckPosition(c, e.Position))
                {
                    c.SendMessage(Message.MousePress, e);
                }
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Processes mouse scroll events for the manager.
        /// </summary>
        private void MouseScrollProcess(object sender, MouseEventArgs e)
        {
// Get the visible controls.
            var c = new ControlsList();
            c.AddRange(OrderList);

// ???
            for (var i = c.Count - 1; i >= 0; i--)
            {
                var chpos = CheckPosition(c[i], e.Position);
                var chsta = CheckState(c[i]);

                if (chsta && chpos && states.Over == c[i])
                {
                    c[i].SendMessage(Message.MouseScroll, e);
                    break;
                }
            }
        }

        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// </summary>
        /// Processes mouse button up events for the manager.
        /// </summary>
        private void MouseUpProcess(object sender, MouseEventArgs e)
        {
            var c = states.Buttons[(int)e.Button];
            if (c != null)
            {
                if (CheckPosition(c, e.Position) && CheckOrder(c, e.Position) && states.Click == (int)e.Button &&
                    CheckButtons((int)e.Button))
                {
                    c.SendMessage(Message.Click, e);
                }
                states.Click = -1;
                c.SendMessage(Message.MouseUp, e);
                states.Buttons[(int)e.Button] = null;
                MouseMoveProcess(sender, e);
            }
        }

        /// <param name="gpe">Gamepad event arguments.</param>
        /// <param name="kbe">Key event arguments.</param>
        /// <param name="control">Control to process arrow keys for.</param>
        /// </summary>
        /// Processes up/down/left/right inputs for the manager.
        /// </summary>
        private void ProcessArrows(Control control, KeyEventArgs kbe, GamePadEventArgs gpe)
        {
            var c = control;
// Control has siblings?
            if (c.Parent != null && c.Parent.Controls != null)
            {
                var index = -1;

// Unhandled left arrow key or DPad left button press received?
                if ((kbe.Key == Keys.Left && !kbe.Handled) ||
                    (gpe.Button == c.GamePadActions.Left && !gpe.Handled))
                {
                    var miny = int.MaxValue;
                    var minx = int.MinValue;
// Check sibling controls to find the closest control to this one.
                    for (var i = 0; i < (c.Parent.Controls as ControlsList).Count; i++)
                    {
                        var cx = (c.Parent.Controls as ControlsList)[i];
// Skip if this control is the same control, not visible, disabled, or cannot receive focus.
                        if (cx == c || !cx.Visible || !cx.Enabled || cx.Passive || !cx.CanFocus) continue;

// Control vertical center.
                        var cay = c.Top + (c.Height / 2);
// Child control vertical center.
                        var cby = cx.Top + (cx.Height / 2);

// Difference between center points is the new minimum value and is the closest control to the left of the control?
                        if (Math.Abs(cay - cby) <= miny && (cx.Left + cx.Width) >= minx &&
                            (cx.Left + cx.Width) <= c.Left)
                        {
// Update minimum values and update index to the new closest control.
                            miny = Math.Abs(cay - cby);
                            minx = cx.Left + cx.Width;
                            index = i;
                        }
                    }
                }
// Unhandled right arrow key or DPad right button press received?
                else if ((kbe.Key == Keys.Right && !kbe.Handled) ||
                         (gpe.Button == c.GamePadActions.Right && !gpe.Handled))
                {
                    var miny = int.MaxValue;
                    var minx = int.MaxValue;
// Check sibling controls to find the closest control to this one.
                    for (var i = 0; i < (c.Parent.Controls as ControlsList).Count; i++)
                    {
                        var cx = (c.Parent.Controls as ControlsList)[i];
// Skip if this control is the same control, not visible, disabled, or cannot receive focus.
                        if (cx == c || !cx.Visible || !cx.Enabled || cx.Passive || !cx.CanFocus) continue;

// Control vertical center.
                        var cay = c.Top + (c.Height / 2);
// Child control vertical center.
                        var cby = cx.Top + (cx.Height / 2);

// Difference between center points is the new minimum value and is the closest control to the right of the control?
                        if (Math.Abs(cay - cby) <= miny && cx.Left <= minx && cx.Left >= (c.Left + c.Width))
                        {
// Update minimum values and update index to the new closest control.
                            miny = Math.Abs(cay - cby);
                            minx = cx.Left;
                            index = i;
                        }
                    }
                }
// Unhandled up arrow key or DPad up button press received?
                else if ((kbe.Key == Keys.Up && !kbe.Handled) ||
                         (gpe.Button == c.GamePadActions.Up && !gpe.Handled))
                {
                    var miny = int.MinValue;
                    var minx = int.MaxValue;
// Check sibling controls to find the closest control to this one.
                    for (var i = 0; i < (c.Parent.Controls as ControlsList).Count; i++)
                    {
                        var cx = (c.Parent.Controls as ControlsList)[i];
// Skip if this control is the same control, not visible, disabled, or cannot receive focus.
                        if (cx == c || !cx.Visible || !cx.Enabled || cx.Passive || !cx.CanFocus) continue;

                        var cax = c.Left + (c.Width / 2);
                        var cbx = cx.Left + (cx.Width / 2);

                        if (Math.Abs(cax - cbx) <= minx && (cx.Top + cx.Height) >= miny && (cx.Top + cx.Height) <= c.Top)
                        {
                            minx = Math.Abs(cax - cbx);
                            miny = cx.Top + cx.Height;
                            index = i;
                        }
                    }
                }
// Unhandled down arrow key or DPad down button press received?
                else if ((kbe.Key == Keys.Down && !kbe.Handled) ||
                         (gpe.Button == c.GamePadActions.Down && !gpe.Handled))
                {
                    var miny = int.MaxValue;
                    var minx = int.MaxValue;
// Check sibling controls to find the closest control to this one.
                    for (var i = 0; i < (c.Parent.Controls as ControlsList).Count; i++)
                    {
                        var cx = (c.Parent.Controls as ControlsList)[i];
// Skip if this control is the same control, not visible, disabled, or cannot receive focus.
                        if (cx == c || !cx.Visible || !cx.Enabled || cx.Passive || !cx.CanFocus) continue;

                        var cax = c.Left + (c.Width / 2);
                        var cbx = cx.Left + (cx.Width / 2);

                        if (Math.Abs(cax - cbx) <= minx && cx.Top <= miny && cx.Top >= (c.Top + c.Height))
                        {
                            minx = Math.Abs(cax - cbx);
                            miny = cx.Top;
                            index = i;
                        }
                    }
                }

// Index changed?
                if (index != -1)
                {
// Focus the new control and handle the input events.
                    (c.Parent.Controls as ControlsList)[index].Focused = true;
                    kbe.Handled = true;
                    gpe.Handled = true;
                }
            }
        }

#if (!XBOX && !XBOX_FAKE)
        /// <param name="cursor">New cursor that the window should use.</param>
        /// </summary>
        /// Changes the window cursor to the specified cursor.
        /// </summary>
        private void SetCursor(Cursor cursor)
        {
            window.Cursor = cursor;
        }
#endif

        /// <param name="h">Maximum height of the control.</param>
        /// <param name="w">Maximum width of the control.</param>
        /// <param name="c">Control to check the dimensions of.</param>
        /// </summary>
        /// dimensions and resizes it if necessary.
        /// Makes sure the specified control does not exceed the specified
        /// </summary>
        private void SetMaxSize(Control c, int w, int h)
        {
// Control to wide?
            if (c.Width > w)
            {
                w -= (c.Skin != null) ? c.Skin.OriginMargins.Horizontal : 0;
                c.Width = w;
            }
// Control to tall?
            if (c.Height > h)
            {
                h -= (c.Skin != null) ? c.Skin.OriginMargins.Vertical : 0;
                c.Height = h;
            }

// Control has children to notify?
            foreach (var cx in c.Controls)
            {
                SetMaxSize(cx, w, h);
            }
        }

        /// <param name="cs"></param>
        /// </summary>
        /// </summary>
        private void SortLevel(ControlsList cs)
        {
            if (cs != null)
            {
                foreach (var c in cs)
                {
                    if (c.Visible)
                    {
                        OrderList.Add(c);
                        SortLevel(c.Controls as ControlsList);
                    }
                }
            }
        }

        /// <param name="control">Current control we are tabbing from.</param>
        /// </summary>
        /// Tabs to the next control in the list.
        /// </summary>
        private void TabNextControl(Control control)
        {
            var start = OrderList.IndexOf(control);
            var i = start;

            do
            {
                if (i < OrderList.Count - 1) i += 1;
                else i = 0;
            } while ((OrderList[i].Root != control.Root || !OrderList[i].CanFocus || OrderList[i].IsRoot ||
                      !OrderList[i].Enabled) && i != start);

            OrderList[i].Focused = true;
        }

        /// <param name="control">Current control we are tabbing from.</param>
        /// </summary>
        /// Tabs to the previous control in the list.
        /// </summary>
        private void TabPrevControl(Control control)
        {
            var start = OrderList.IndexOf(control);
            var i = start;

            do
            {
                if (i > 0) i -= 1;
                else i = OrderList.Count - 1;
            } while ((OrderList[i].Root != control.Root || !OrderList[i].CanFocus || OrderList[i].IsRoot ||
                      !OrderList[i].Enabled) && i != start);
            OrderList[i].Focused = true;
        }

        #region Nested type: Struct

        /// </summary>
        /// </summary>
        private struct ControlStates
        {
            /// </summary>
            /// </summary>
            public Control[] Buttons;

            /// </summary>
            /// </summary>
            public int Click;

            /// </summary>
            /// </summary>
            public Control Over;
        }

        #endregion

#if (!XBOX && !XBOX_FAKE)
        /// </summary>
        /// Application's windows form when running on Windows.
        /// </summary>
        private readonly Form window;

        /// </summary>
        /// Application's cursor used when running on Windows.
        /// </summary>
        private Cursor cursor;
#endif
#if (!XBOX && !XBOX_FAKE)
        /// </summary>
        /// Returns the
        /// <see cref="Form" />
        /// the game runs in.
        /// </summary>
        public virtual Form Window
        {
            get { return window; }
        }

        /// </summary>
        /// Gets or sets an application cursor.
        /// </summary>
        public virtual Cursor Cursor
        {
            get { return cursor; }
            set
            {
                cursor = value;
                SetCursor(cursor);
            }
        }
#endif
    }
}

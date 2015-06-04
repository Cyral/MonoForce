using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoForce.Controls
{
    /// <summary>
    /// Defines the gamepad button to action mappings.
    /// </summary>
    public class GamePadActions
    {
        /// <summary>
        /// Button mapped to a control click event.
        /// </summary>
        public GamePadButton Click = GamePadButton.A;

        /// <summary>
        /// Button mapped to show the context menu of a control.
        /// </summary>
        public GamePadButton ContextMenu = GamePadButton.X;

        /// <summary>
        /// Button mapped to the Down action.
        /// </summary>
        public GamePadButton Down = GamePadButton.LeftStickDown;

        /// <summary>
        /// Button mapped to the Left action.
        /// </summary>
        public GamePadButton Left = GamePadButton.LeftStickLeft;

        /// <summary>
        /// Button mapped to control navigation.
        /// </summary>
        public GamePadButton NextControl = GamePadButton.RightShoulder;

        /// <summary>
        /// Button mapped to a control press event.
        /// </summary>
        public GamePadButton Press = GamePadButton.Y;

        /// <summary>
        /// Button mapped to control navigation.
        /// </summary>
        public GamePadButton PrevControl = GamePadButton.LeftShoulder;

        /// <summary>
        /// Button mapped to the Right action.
        /// </summary>
        public GamePadButton Right = GamePadButton.LeftStickRight;

        /// <summary>
        /// Button mapped to the Up action.
        /// </summary>
        public GamePadButton Up = GamePadButton.LeftStickUp;
    }

    /// <summary>
    /// Defines type used as a controls collection.
    /// </summary>
    public class ControlsList : EventedList<Control>
    {
        /// <summary>
        /// Creates a new ControlsList.
        /// </summary>
        public ControlsList()
        {
        }

        /// <param name="capacity"></param>
        /// <summary>
        /// Creates a new ControlsList with the specified initial capacity.
        /// </summary>
        public ControlsList(int capacity) : base(capacity)
        {
        }

        /// <param name="collection"></param>
        /// <summary>
        /// Creates a new ControlsList and populates it with the items from the source collection.
        /// </summary>
        public ControlsList(IEnumerable<Control> collection) : base(collection)
        {
        }
    }

    /// </summary>
    /// Defines the base class for all controls.
    /// <summary>    
    public class Control : Component
    {
        #region Consts
        /// </summary>
        /// Default color used when a color is not defined.
        /// <summary>
        public static readonly Color UndefinedColor = new Color(255, 255, 255, 0);
        #endregion

        #region Fields
        /// </summary>
        /// An internal list of all controls in memory managed by the GUI manager.
        /// <summary>
        internal static ControlsList Stack = new ControlsList();
#if (!XBOX && !XBOX_FAKE)
        /// </summary>
        /// Cursor displayed when the mouse is over the control.
        /// <summary>
        private Cursor cursor = null;
#endif
        /// </summary>
        /// Color of the control.
        /// <summary>
        private Color color = UndefinedColor;
        /// </summary>
        /// Color of the control's text.
        /// <summary>
        private Color textColor = UndefinedColor;
        /// </summary>
        /// Color of the control's background.
        /// <summary>
        private Color backColor = Color.Transparent;
        /// </summary>
        /// Transparency of the control.
        /// <summary>
        private float alpha = 255;
        /// </summary>
        /// Transparency of the control.
        /// <summary>
        internal float Hoveralpha = 0;
        /// </summary>
        /// Control's anchor values that define which edge of a container a control is 
        /// bound and how the control is resized in relation to its parent control.
        /// <summary>
        private Anchors anchor = Anchors.Left | Anchors.Top;
        /// </summary>
        /// Indicates which edge of the control will show the resize cursor and support resizing.
        /// <summary>
        private Anchors resizeEdge = Anchors.All;
        /// </summary>
        /// Text of the control.
        /// <summary>
        private string text = "Control";
        /// </summary>
        /// Indicates if the control is visible or not.
        /// <summary>
        private bool visible = true;
        /// </summary>
        /// Indicates if the control is enabled or not.
        /// <summary>
        private bool enabled = true;
        /// </summary>
        /// Skin used for rendering the control.
        /// <summary>
        protected SkinControl skin = null;
        /// </summary>
        /// Control's parent control.
        /// <summary>
        protected Control parent = null;
        /// </summary>
        /// Root container control. ???
        /// <summary>
        protected Control root = null;
        /// </summary>
        /// X position of the control as an offset from the left edge of its parent control
        /// <summary>
        private int left = 0;
        /// </summary>
        /// Y position of the control as an offset from the top edge of its parent control.
        /// <summary>
        private int top = 0;
        /// </summary>
        /// Width of the control.
        /// <summary>
        private int width = 64;
        /// </summary>
        /// Height of the control.
        /// <summary>
        private int height = 64;
        /// </summary>
        /// Indicates if the control should receive any events.
        /// <summary>
        private bool suspended = false;
        /// </summary>
        /// Context menu for this control.
        /// <summary>
        private ContextMenu contextMenu = null;
        /// </summary>
        /// Tracks elapsed time for delaying the display of the control's tool tip. 
        /// <summary>
        protected long tooltipTimer = 0;
        /// </summary>
        /// Tracks elapsed time for delaying the display of the control's tool tip. 
        /// <summary>
        internal long hoverTimer = 0;
        /// </summary>
        /// Tracks the time of the last doubleClickButton click to help detect double click events.
        /// <summary>
        private long doubleClickTimer = 0;
        /// </summary>
        /// Specifies which mouse button should be detected for double click events.
        /// <summary>
        private MouseButton doubleClickButton = MouseButton.None;
        /// </summary>
        /// Type argument for the tool tip.
        /// <summary>
        private Type toolTipType = typeof(ToolTip);
        /// </summary>
        /// Tool tip for the control.
        /// <summary>
        protected ToolTip toolTip = null;
        /// </summary>
        /// Indicates if the control can receive double clicks.
        /// <summary>
        private bool doubleClicks = true;
        /// </summary>
        /// Indicates if the control should use outline resizing.
        /// <summary>
        private bool outlineResizing = false;
        /// </summary>
        /// Indicates if the control should use outline moving.
        /// <summary>
        private bool outlineMoving = false;
        /// </summary>
        /// Name of the control.
        /// <summary>
        private string name = "Control";
        /// </summary>
        /// Custom user-defined data associated with the control.
        /// <summary>
        private object tag = null;
        /// </summary>
        /// Gamepad button to control action mappings.
        /// <summary>
        private GamePadActions gamePadActions = new GamePadActions();
        public bool toolTipisClosing;
        /// </summary>
        /// Indicates if the control is in design mode.
        /// <summary>
        private bool designMode = false;
        /// </summary>
        /// Indicates if the control outline is only displayed for certain edges.
        /// <summary>
        private bool partialOutline = true;
        /// </summary>
        /// Defines the area where the control should be drawn.
        /// <summary>
        private Rectangle drawingRect = Rectangle.Empty;
        /// </summary>
        /// List of child controls belonging to the control.
        /// <summary>
        private ControlsList controls = new ControlsList();
        /// </summary>
        /// Defines the region of the control where the mouse can click to start a move operation.
        /// <summary>
        private Rectangle movableArea = Rectangle.Empty;
        /// </summary>
        /// Indicates if the control can receive user input events or not.
        /// <summary>
        private bool passive = false;
        /// </summary>
        /// Indicates if the control is rendered off the parents texture.
        /// <summary>
        private bool detached = false;
        /// </summary>
        /// Indicates if the control can be moved by a drag operation.
        /// <summary>
        private bool movable = false;
        /// </summary>
        /// Indicates if the control can be resized by a drag operation.
        /// <summary>
        private bool resizable = false;
        /// </summary>
        /// Indicates if the control needs to be redrawn.
        /// <summary>
        private bool invalidated = true;
        /// </summary>
        /// Indicates if the control can receive input focus or not.
        /// <summary>
        private bool canFocus = true;
        /// </summary>
        /// The size of the borders around the control used for resizing by the mouse.
        /// <summary>
        private int resizerSize = 4;
        /// </summary>
        /// Minimum width of the control.
        /// <summary>
        private int minimumWidth = 0;
        /// </summary>
        /// Maximum width of the control.
        /// <summary>
        private int maximumWidth = 4096;
        /// </summary>
        /// Minimum height of the control.
        /// <summary>
        private int minimumHeight = 0;
        /// </summary>
        /// Maximum height of the control.
        /// <summary>
        private int maximumHeight = 4096;
        /// </summary>
        /// Y position offset to apply to the control.
        /// <summary>
        private int topModifier = 0;
        /// </summary>
        /// X position offset to apply to the control.
        /// <summary>
        private int leftModifier = 0;
        /// </summary>
        /// ???
        /// <summary>
        private int virtualHeight = 64;
        /// </summary>
        /// ???
        /// <summary>
        private int virtualWidth = 64;
        /// </summary>
        /// Indicates if the control should stay under other controls.
        /// <summary>
        private bool stayOnBack = false;
        /// </summary>
        /// Indicates if the control should stay on top of other controls.
        /// <summary>
        private bool stayOnTop = false;
        /// </summary>
        /// Texture the control pre-draws to before being rendered on screen.
        /// <summary>
        private RenderTarget2D target;
        /// </summary>
        /// Indicates where the mouse cursor is pressing on the control. Used to help 
        /// detect move and resize events on the control.
        /// <summary>
        private Point pressSpot = Point.Zero;
        /// </summary>
        /// ???
        /// <summary>
        private int[] pressDiff = new int[4];
        /// </summary>
        /// ???
        /// <summary>
        private Alignment resizeArea = Alignment.None;
        /// </summary>
        /// Indicates if the mouse cursor is hovering the control.
        /// <summary>
        private bool hovered = false;
        /// </summary>
        /// ???
        /// <summary>
        private bool inside = false;
        /// </summary>
        /// Tracks the pressed state of various input buttons.
        /// <summary>
        private bool[] pressed = new bool[32];
        /// </summary>
        /// Indicates if the control is currently involved in a move event.
        /// <summary>
        private bool isMoving = false;
        /// </summary>
        /// Indicates if the control is currently involved in a resizing event.
        /// <summary>
        private bool isResizing = false;
        /// </summary>
        /// Margins between this control and another control. Usable with the StackPanel control.
        /// <summary>
        private Margins margins = new Margins(4, 4, 4, 4);
        /// </summary>
        /// ???
        /// <summary>
        private Margins anchorMargins = new Margins();
        /// </summary>
        /// Margins defining how much offset to define the client area of the control.
        /// <summary>
        private Margins clientMargins = new Margins();
        /// </summary>
        /// Rectangle drawn when the control is being resized to indicate the new size of the control.
        /// <summary>
        private Rectangle outlineRect = Rectangle.Empty;
        /// </summary>
        /// Tracks the position of the mouse scroll wheel
        /// <summary>
        private int scrollWheel = 0;
        /// </summary>
        /// Indicates if text that has color ([color:Color][/color] are renderered
        /// <summary>
        protected bool drawFormattedText = true;
        protected bool toolTipFadingOut;
        #endregion

        #region Properties
#if (!XBOX && !XBOX_FAKE)
        /// </summary>
        /// Gets or sets the cursor displaying over the control.
        /// <summary>
        public Cursor Cursor
        {
            get { return cursor; }
            set { cursor = value; }
        }
#endif

        /// </summary>
        /// Indicates if text that has color is renderered
        /// <summary>
        public bool DrawFormattedText { get { return drawFormattedText; } set { drawFormattedText = value; } }

        /// </summary>
        /// Gets a list of all child controls.
        /// <summary>
        public virtual IEnumerable<Control> Controls { get { return controls; } }

        /// </summary>
        /// Gets or sets a rectangular area that reacts on moving the control with the mouse.
        /// <summary>
        public virtual Rectangle MovableArea { get { return movableArea; } set { movableArea = value; } }

        /// </summary>
        /// Gets a value indicating whether this control is a child control.
        /// <summary>
        public virtual bool IsChild { get { return (parent != null); } }

        /// </summary>
        /// Gets a value indicating whether this control is a parent control.
        /// <summary>
        public virtual bool IsParent { get { return (controls != null && controls.Count > 0); } }

        /// </summary>
        /// Gets a value indicating whether this control is a root control.
        /// <summary>
        public virtual bool IsRoot { get { return (root == this); } }

        /// </summary>
        /// Gets or sets a value indicating whether this control can receive focus. 
        /// <summary>
        public virtual bool CanFocus { get { return canFocus; } set { canFocus = value; } }

        /// </summary>
        /// Gets or sets a value indicating whether this control is rendered off the parents texture.
        /// <summary>
        public virtual bool Detached { get { return detached; } set { detached = value; } }

        /// </summary>
        /// Gets or sets a value indicating whether this controls can receive user input events.
        /// <summary>
        public virtual bool Passive { get { return passive; } set { passive = value; } }

        /// </summary>
        /// Gets or sets a value indicating whether this control can be moved by the mouse.
        /// <summary>
        public virtual bool Movable { get { return movable; } set { movable = value; } }

        /// </summary>
        /// Gets or sets a value indicating whether this control can be resized by the mouse.
        /// <summary>
        public virtual bool Resizable { get { return resizable; } set { resizable = value; } }

        /// </summary>
        /// Gets or sets the size of the rectangular borders around the control used for resizing by the mouse.
        /// <summary>
        public virtual int ResizerSize { get { return resizerSize; } set { resizerSize = value; } }

        /// </summary>
        /// Gets or sets the ContextMenu associated with this control.
        /// <summary>
        public virtual ContextMenu ContextMenu { get { return contextMenu; } set { contextMenu = value; } }

        /// </summary>
        /// Gets or sets a value indicating whether this control should process mouse double-clicks.
        /// <summary>
        public virtual bool DoubleClicks { get { return doubleClicks; } set { doubleClicks = value; } }

        /// </summary>
        /// Gets or sets a value indicating whether this control should use ouline resizing.
        /// <summary>
        public virtual bool OutlineResizing { get { return outlineResizing; } set { outlineResizing = value; } }

        /// </summary>
        /// Gets or sets a value indicating whether this control should use outline moving.
        /// <summary>
        public virtual bool OutlineMoving { get { return outlineMoving; } set { outlineMoving = value; } }

        /// </summary>
        /// Gets or sets the object that contains data about the control.
        /// <summary>
        public virtual object Tag { get { return tag; } set { tag = value; } }

        /// </summary>
        /// Gets or sets the value indicating the distance from another control. Usable with StackPanel control.
        /// <summary>
        public virtual Margins Margins { get { return margins; } set { margins = value; } }

        /// </summary>
        /// Gets or sets the value indicating wheter control is in design mode.
        /// <summary>
        public virtual bool DesignMode { get { return designMode; } set { designMode = value; } }

        /// </summary>
        /// Gets or sets gamepad actions for the control.
        /// <summary>
        public virtual GamePadActions GamePadActions
        {
            get { return gamePadActions; }
            set { gamePadActions = value; }
        }

        /// </summary>
        /// Gets or sets the value indicating whether the control outline is displayed only for certain edges. 
        /// <summary>   
        public virtual bool PartialOutline
        {
            get { return partialOutline; }
            set { partialOutline = value; }
        }

        /// </summary>
        /// Gets or sets the value indicating whether the control is allowed to be brought in the front.
        /// <summary>
        public virtual bool StayOnBack
        {
            get { return stayOnBack; }
            set
            {
                if (value && stayOnTop) stayOnTop = false;
                stayOnBack = value;
            }
        }

        /// </summary>
        /// Gets or sets the value indicating that the control should stay on top of other controls.
        /// <summary>
        public virtual bool StayOnTop
        {
            get { return stayOnTop; }
            set
            {
                if (value && stayOnBack) stayOnBack = false;
                stayOnTop = value;
            }
        }

        /// </summary>
        /// Gets or sets a name of the control.
        /// <summary>
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// </summary>
        /// Gets or sets a value indicating whether this control has input focus.
        /// <summary>
        public virtual bool Focused
        {
            get
            {
                return (Manager.FocusedControl == this);
            }
            set
            {
                this.Invalidate();
                if (value)
                {
                    bool f = Focused;
                    Manager.FocusedControl = this;
                    if (!Suspended && value && !f) OnFocusGained(new EventArgs());
                    if (Focused && Root != null && Root is Container)
                    {
                        (Root as Container).ScrollTo(this);
                    }
                }
                else
                {
                    bool f = Focused;
                    if (Manager.FocusedControl == this) Manager.FocusedControl = null;
                    if (!Suspended && !value && f) OnFocusLost(new EventArgs());
                }
            }
        }

        /// </summary>
        /// Gets a value indicating current state of the control.
        /// <summary>
        public virtual ControlState ControlState
        {
            get
            {
                if (DesignMode) return ControlState.Enabled;
                else if (Suspended) return ControlState.Disabled;
                else
                {
                    if (!enabled) return ControlState.Disabled;

                    if ((IsPressed && inside) || (Focused && IsPressed)) return ControlState.Pressed;
                    else if (hovered && !IsPressed) return ControlState.Hovered;
                    else if ((Focused && !inside) || (hovered && IsPressed && !inside) || (Focused && !hovered && inside)) return ControlState.Focused;
                    else return ControlState.Enabled;
                }
            }
        }

        /// </summary>
        /// Gets or sets the type of the control's ToolTip.
        /// <summary>
        public virtual Type ToolTipType
        {
            get { return toolTipType; }
            set
            {
                toolTipType = value;
                if (toolTip != null)
                {
                    toolTip.Dispose();
                    toolTip = null;
                }
            }
        }

        /// </summary>
        /// Gets or sets the control's tool tip that will display when hovered.
        /// <summary>
        public virtual ToolTip ToolTip
        {
            get
            {
                if (toolTip == null)
                {
                    Type[] t = new Type[1] { typeof(Manager) };
                    object[] p = new object[1] { Manager };

                    toolTip = (ToolTip)toolTipType.GetConstructor(t).Invoke(p);
                    toolTip.Init();
                    toolTip.Visible = false;
                }
                return toolTip;
            }
            set
            {
                toolTip = value;
            }
        }

        /// </summary>
        /// Indicates if the control is in the pressed state.
        /// <summary>
        internal protected virtual bool IsPressed
        {
            get
            {
                for (int i = 0; i < pressed.Length - 1; i++)
                {
                    if (pressed[i]) return true;
                }
                return false;
            }
        }

        /// </summary>
        /// Gets or sets the Y position offset applied to the control. (Used to
        /// adjust the control's position when a vertical scroll bar is present.)
        /// <summary>
        internal virtual int TopModifier
        {
            get { return topModifier; }
            set { topModifier = value; }
        }

        /// </summary>
        /// Gets or sets the X position offset applied to the control. (Used to 
        /// adjust the control's position when a horizontal scroll bar is present.)
        /// <summary>
        internal virtual int LeftModifier
        {
            get { return leftModifier; }
            set { leftModifier = value; }
        }

        /// </summary>
        /// ???
        /// <summary>
        internal virtual int VirtualHeight
        {
            get { return GetVirtualHeight(); }
            //set { virtualHeight = value; }
        }

        /// </summary>
        /// ???
        /// <summary>
        internal virtual int VirtualWidth
        {
            get { return GetVirtualWidth(); }
            //set { virtualWidth = value; }
        }

        /// </summary>
        /// Gets an area where is the control supposed to be drawn.
        /// <summary>
        public Rectangle DrawingRect
        {
            get { return drawingRect; }
            private set { drawingRect = value; }
        }

        /// </summary>
        /// Gets or sets a value indicating whether this control should receive any events.
        /// <summary>
        public virtual bool Suspended
        {
            get { return suspended; }
            set { suspended = value; }
        }

        /// </summary>
        /// Indicates if the control is hovered by the mouse cursor.
        /// <summary>
        internal protected virtual bool Hovered
        {
            get { return hovered; }
        }

        /// </summary>
        /// Indicates if the mouse cursor is within the bounds of the control. ???
        /// <summary>
        internal protected virtual bool Inside
        {
            get { return inside; }
        }

        /// </summary>
        /// Tracks the pressed state of various input buttons.
        /// <summary>
        internal protected virtual bool[] Pressed
        {
            get { return pressed; }
        }

        /// </summary>
        /// Gets or sets a value indicating whether this controls is currently being moved.
        /// <summary>
        protected virtual bool IsMoving
        {
            get
            {
                return isMoving;
            }
            set
            {
                isMoving = value;
            }
        }

        /// </summary>
        /// Gets or sets a value indicating whether this controls is currently being resized.
        /// <summary>
        protected virtual bool IsResizing
        {
            get
            {
                return isResizing;
            }
            set
            {
                isResizing = value;
            }
        }

        /// </summary>
        /// Gets or sets the edges of the container to which a control is bound and determines how a control is resized with its parent.
        /// <summary>
        public virtual Anchors Anchor
        {
            get
            {
                return anchor;
            }
            set
            {
                anchor = value;
                SetAnchorMargins();
                if (!Suspended) OnAnchorChanged(new EventArgs());
            }
        }

        /// </summary>
        /// Gets or sets the edges of the contol which are allowed for resizing.
        /// <summary>
        public virtual Anchors ResizeEdge
        {
            get
            {
                return resizeEdge;
            }
            set
            {
                resizeEdge = value;
            }
        }

        /// </summary>
        /// Gets or sets the skin used for rendering the control.
        /// <summary>
        public virtual SkinControl Skin
        {
            get
            {
                return skin;
            }
            set
            {
                skin = value;
                ClientMargins = skin.ClientMargins;
            }
        }

        /// </summary>
        /// Gets or sets the text associated with this control.
        /// <summary>
        public virtual string Text
        {
            get { return text; }
            set
            {
                text = value;
                Invalidate();
                if (!Suspended) OnTextChanged(new EventArgs());
            }
        }

        /// </summary>
        /// Gets or sets the alpha value for this control.
        /// <summary>
        public virtual float Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = value;
                if (!Suspended) OnAlphaChanged(new EventArgs());
            }
        }

        /// </summary>
        /// Gets or sets the background color for the control.
        /// <summary>
        public virtual Color BackColor
        {
            get
            {
                return backColor;
            }
            set
            {
                backColor = value;
                Invalidate();
                if (!Suspended) OnBackColorChanged(new EventArgs());
            }
        }

        /// </summary>
        /// Gets or sets the color for the control.
        /// <summary>
        public virtual Color Color
        {
            get
            {
                return color;
            }
            set
            {
                if (value != color)
                {
                    color = value;
                    Invalidate();
                    if (!Suspended) OnColorChanged(new EventArgs());
                }
            }
        }

        /// </summary>
        /// Gets or sets the text color for the control.
        /// <summary>
        public virtual Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                if (value != textColor)
                {
                    textColor = value;
                    Invalidate();
                    if (!Suspended) OnTextColorChanged(new EventArgs());
                }
            }
        }

        /// </summary>
        /// Gets or sets a value indicating whether the control can respond to user interaction.
        /// <summary>
        public virtual bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (Root != null && Root != this && !Root.Enabled && value) return;

                enabled = value;
                Invalidate();

                foreach (Control c in controls)
                {
                    c.Enabled = value;
                }

                if (!Suspended) OnEnabledChanged(new EventArgs());
            }
        }

        /// </summary>
        /// Gets or sets a value that indicates whether the control is rendered.
        /// <summary>
        public virtual bool Visible
        {
            get
            {
                return (visible && (parent == null || parent.Visible));
            }
            set
            {
                visible = value;
                Invalidate();

                if (!Suspended) OnVisibleChanged(new EventArgs());
            }
        }

        /// </summary>
        /// Gets or sets the parent for the control.
        /// <summary>
        public virtual Control Parent
        {
            get
            {
                return parent;
            }
            set
            {
                if (parent != value)
                {
                    if (value != null) value.Add(this);
                    else Manager.Add(this);
                }
            }
        }

        /// </summary>
        /// Gets or sets the root for the control.
        /// <summary>
        public virtual Control Root
        {
            get
            {
                return root;
            }
            private set
            {
                if (root != value)
                {
                    root = value;

                    foreach (Control c in controls)
                    {
                        c.Root = root;
                    }

                    if (!Suspended) OnRootChanged(new EventArgs());
                }
            }
        }
        /// </summary>
        /// Gets or sets the distance, in pixels, between the right edge of the control and the left edge of its parent.
        /// <summary>
        public virtual int Right
        {
            get
            {
                return left + Width;
            }
            set
            {
                value -= Width;
                if (left != value)
                {
                    int old = left;
                    left = value;

                    SetAnchorMargins();

                    if (!Suspended) OnMove(new MoveEventArgs(left, top, old, top));
                }
            }
        }
        /// </summary>
        /// Gets or sets the distance, in pixels, between the left edge of the control and the left edge of its parent.
        /// <summary>
        public virtual int Left
        {
            get
            {
                return left;
            }
            set
            {
                if (left != value)
                {
                    int old = left;
                    left = value;

                    SetAnchorMargins();

                    if (!Suspended) OnMove(new MoveEventArgs(left, top, old, top));
                }
            }
        }
        /// </summary>
        /// Gets or sets the distance, in pixels, between the bottom edge of the control and the top edge of its parent.
        /// <summary>
        public virtual int Bottom
        {
            get
            {
                return top + Height;
            }
            set
            {
                value -= Height;
                if (top != value)
                {
                    int old = top;
                    top = value;

                    SetAnchorMargins();

                    if (!Suspended) OnMove(new MoveEventArgs(left, top, left, old));
                }
            }
        }
        /// </summary>
        /// Gets or sets the distance, in pixels, between the top edge of the control and the top edge of its parent.
        /// <summary>
        public virtual int Top
        {
            get
            {
                return top;
            }
            set
            {
                if (top != value)
                {
                    int old = top;
                    top = value;

                    SetAnchorMargins();

                    if (!Suspended) OnMove(new MoveEventArgs(left, top, left, old));
                }
            }
        }

        /// </summary>
        /// Gets or sets the width of the control.
        /// <summary>
        public virtual int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (width != value)
                {
                    int old = width;
                    width = value;

                    if (skin != null)
                    {
                        if (width + skin.OriginMargins.Horizontal > MaximumWidth) width = MaximumWidth - skin.OriginMargins.Horizontal;
                    }
                    else
                    {
                        if (width > MaximumWidth) width = MaximumWidth;
                    }
                    if (width < MinimumWidth) width = MinimumWidth;

                    if (width > 0) SetAnchorMargins();

                    if (!Suspended) OnResize(new ResizeEventArgs(width, height, old, height));
                }
            }
        }

        /// </summary>
        /// Gets or sets the height of the control.
        /// <summary>
        public virtual int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height != value)
                {
                    int old = height;

                    height = value;

                    if (skin != null)
                    {
                        if (height + skin.OriginMargins.Vertical > MaximumHeight)
                            height = MaximumHeight - skin.OriginMargins.Vertical;
                    }
                    else
                    {
                        if (height > MaximumHeight) height = MaximumHeight;
                    }
                    if (height < MinimumHeight) height = MinimumHeight;

                    if (height > 0) SetAnchorMargins();

                    if (!Suspended) OnResize(new ResizeEventArgs(width, height, width, old));
                }

            }

        }

        /// </summary>
        /// Gets or sets the minimum width in pixels the control can be sized to.
        /// <summary>
        public virtual int MinimumWidth
        {
            get
            {
                return minimumWidth;
            }
            set
            {
                minimumWidth = value;
                if (minimumWidth < 0) minimumWidth = 0;
                if (minimumWidth > maximumWidth) minimumWidth = maximumWidth;
                if (width < MinimumWidth) Width = MinimumWidth;
            }
        }

        /// </summary>
        /// Gets or sets the minimum height in pixels the control can be sized to.
        /// <summary>
        public virtual int MinimumHeight
        {
            get
            {
                return minimumHeight;
            }
            set
            {
                minimumHeight = value;
                if (minimumHeight < 0) minimumHeight = 0;
                if (minimumHeight > maximumHeight) minimumHeight = maximumHeight;
                if (height < MinimumHeight) Height = MinimumHeight;
            }
        }

        /// </summary>
        /// Gets or sets the maximum width in pixels the control can be sized to.
        /// <summary>
        public virtual int MaximumWidth
        {
            get
            {
                int max = maximumWidth;
                return max;
            }
            set
            {
                maximumWidth = value;
                if (maximumWidth < minimumWidth) maximumWidth = minimumWidth;
                if (width > MaximumWidth) Width = MaximumWidth;
            }
        }

        /// </summary>
        /// Gets or sets the maximum height in pixels the control can be sized to.
        /// <summary>
        public virtual int MaximumHeight
        {
            get
            {
                int max = maximumHeight;
                return max;
            }
            set
            {
                maximumHeight = value;
                if (maximumHeight < minimumHeight) maximumHeight = minimumHeight;
                if (height > MaximumHeight) Height = MaximumHeight;
            }
        }

        /// </summary>
        /// Gets the absolute screen X position of the control.
        /// <summary>
        public virtual int AbsoluteLeft
        {
            get
            {
                if (parent == null) return left + LeftModifier;
                else if (parent.Skin == null) return parent.AbsoluteLeft + left + LeftModifier;
                else return parent.AbsoluteLeft + left - parent.Skin.OriginMargins.Left + LeftModifier;
            }
        }

        /// </summary>
        /// Gets the absolute screen Y position of the control.
        /// <summary>
        public virtual int AbsoluteTop
        {
            get
            {
                if (parent == null) return top + TopModifier;
                else if (parent.Skin == null) return parent.AbsoluteTop + top + TopModifier;
                else return parent.AbsoluteTop + top - parent.Skin.OriginMargins.Top + TopModifier;
            }
        }

        /// </summary>
        /// Gets the absolute screen X position of the control. (Control origin margins are factored in.)
        /// <summary>
        public virtual int OriginLeft
        {
            get
            {
                if (skin == null) return AbsoluteLeft;
                return AbsoluteLeft - skin.OriginMargins.Left;
            }
        }

        /// </summary>
        /// Gets the absolute screen Y position of the control. (Control origin margins are factored in.
        /// <summary>
        public virtual int OriginTop
        {
            get
            {
                if (skin == null) return AbsoluteTop;
                return AbsoluteTop - skin.OriginMargins.Top;
            }
        }

        /// </summary>
        /// Gets the width of the control, including margin amounts.
        /// <summary>
        public virtual int OriginWidth
        {
            get
            {
                if (skin == null) return width;
                return width + skin.OriginMargins.Left + skin.OriginMargins.Right;
            }
        }

        /// </summary>
        /// Gets the height of the control, including margin amounts.
        /// <summary>
        public virtual int OriginHeight
        {
            get
            {
                if (skin == null) return height;
                return height + skin.OriginMargins.Top + skin.OriginMargins.Bottom;
            }
        }

        /// </summary>
        /// Gets the client (inner) margins for the control.
        /// <summary>
        public virtual Margins ClientMargins
        {
            get { return clientMargins; }
            set
            {
                clientMargins = value;
            }
        }

        /// </summary>
        /// Gets the X position of the control's client area. (Relative offset from control origin.)
        /// <summary>
        public virtual int ClientLeft
        {
            get
            {
                //if (skin == null) return Left;
                return ClientMargins.Left;
            }
        }

        /// </summary>
        /// Gets the Y position of the control's client area. (Relative offset from control origin.)
        /// <summary>
        public virtual int ClientTop
        {
            get
            {
                //if (skin == null) return Top;
                return ClientMargins.Top;
            }
        }

        /// </summary>
        /// Gets or sets the width of the control's client area.
        /// <summary>
        public virtual int ClientWidth
        {
            get
            {
                //if (skin == null) return Width;
                return OriginWidth - ClientMargins.Left - ClientMargins.Right;
            }
            set
            {
                Width = value + ClientMargins.Horizontal - skin.OriginMargins.Horizontal;
            }
        }

        /// </summary>
        /// Gets or sets the height of the control's client area.
        /// <summary>
        public virtual int ClientHeight
        {
            get
            {
                //if (skin == null) return Height;
                return OriginHeight - ClientMargins.Top - ClientMargins.Bottom;
            }
            set
            {
                Height = value + ClientMargins.Vertical - skin.OriginMargins.Vertical;
            }
        }

        /// </summary>
        /// Gets a rectangle that contains the control and its margin amounts. (Absolute X and Y positions are specified.)
        /// <summary>
        public virtual Rectangle AbsoluteRect
        {
            get
            {
                return new Rectangle(AbsoluteLeft, AbsoluteTop, OriginWidth, OriginHeight);
            }
        }

        /// </summary>
        /// Gets a rectangle that contains the control and its margin amounts. (Relative X and Y offsets from the control origin are specified.)
        /// <summary>
        public virtual Rectangle OriginRect
        {
            get
            {
                return new Rectangle(OriginLeft, OriginTop, OriginWidth, OriginHeight);
            }
        }

        /// </summary>
        /// Gets a rectangle that contains the control's client area. (Relative X and Y offsets from the control origin are specified.
        /// <summary>
        public virtual Rectangle ClientRect
        {
            get
            {
                return new Rectangle(ClientLeft, ClientTop, ClientWidth, ClientHeight);
            }
        }

        /// </summary>
        /// Gets or sets the rectangle that contains the entire control and all margins. 
        /// <summary>
        public virtual Rectangle ControlRect
        {
            get
            {
                return new Rectangle(Left, Top, Width, Height);
            }
            set
            {
                Left = value.Left;
                Top = value.Top;
                Width = value.Width;
                Height = value.Height;
            }
        }

        /// </summary>
        /// Gets or sets the size of the outline rectangle drawn during a resize operation.
        /// <summary>
        private Rectangle OutlineRect
        {
            get { return outlineRect; }
            set
            {
                outlineRect = value;
                if (value != Rectangle.Empty)
                {
                    if (outlineRect.Width > MaximumWidth) outlineRect.Width = MaximumWidth;
                    if (outlineRect.Height > MaximumHeight) outlineRect.Height = MaximumHeight;
                    if (outlineRect.Width < MinimumWidth) outlineRect.Width = MinimumWidth;
                    if (outlineRect.Height < MinimumHeight) outlineRect.Height = MinimumHeight;
                }
            }
        }
        #endregion

        #region Events
        /// </summary>
        /// Occurs when the control is clicked.
        /// <summary>
        public event EventHandler Click;
        /// </summary>
        /// Occurs when the control is double clicked.
        /// <summary>
        public event EventHandler DoubleClick;
        /// </summary>
        /// Occurs when the control receives a mouse button down event.
        /// <summary>
        public event MouseEventHandler MouseDown;
        /// </summary>
        /// Occurs right after a MouseDown event and fires repeatedly with a delay.
        /// <summary>
        public event MouseEventHandler MousePress;
        /// </summary>
        /// Occurs when the control receives a mouse button up event.
        /// <summary>
        public event MouseEventHandler MouseUp;
        /// </summary>
        /// Occurs when the mouse position changes.
        /// <summary>
        public event MouseEventHandler MouseMove;
        /// </summary>
        /// Occurs when the mouse cursor is hovering over the controls.
        /// <summary>
        public event MouseEventHandler MouseOver;
        /// </summary>
        /// Occurs when the mouse cursor leaves the boundaries of the control.
        /// <summary>
        public event MouseEventHandler MouseOut;
        /// </summary>
        /// Occurs when the mouse scroll wheel position changes
        /// <summary>
        public event MouseEventHandler MouseScroll;
        /// </summary>
        /// Occurs when a key is initially pressed down.
        /// <summary>
        public event KeyEventHandler KeyDown;
        /// </summary>
        /// Occurs just after a KeyDown event.
        /// <summary>
        public event KeyEventHandler KeyPress;
        /// </summary>
        /// Occurs when a key is released from the pressed state.
        /// <summary>
        public event KeyEventHandler KeyUp;
        /// </summary>
        /// Occurs when a gamepad button is initially pressed.
        /// <summary>
        public event GamePadEventHandler GamePadDown;
        /// </summary>
        /// Occurs when a gamepad button is released from the pressed state.
        /// <summary>
        public event GamePadEventHandler GamePadUp;
        /// </summary>
        /// Occurs after a GamePadDown event and fires repeated press events when a button is held down.
        /// <summary>
        public event GamePadEventHandler GamePadPress;
        /// </summary>
        /// Occurs when the control is moving.
        /// <summary>
        public event MoveEventHandler Move;
        /// </summary>
        /// Occurs just before a move event is finalized.
        /// <summary>
        public event MoveEventHandler ValidateMove;
        /// </summary>
        /// Occurs when the control is resized.
        /// <summary>
        public event ResizeEventHandler Resize;
        /// </summary>
        /// Occurs just before a resize event is finalized.
        /// <summary>
        public event ResizeEventHandler ValidateResize;
        /// </summary>
        /// Occurs when the control needs to draw.
        /// <summary>
        public event DrawEventHandler Draw;
        /// </summary>
        /// Occurs at the start of a move event.
        /// <summary>
        public event EventHandler MoveBegin;
        /// </summary>
        /// Occurs at the end of a move event.
        /// <summary>
        public event EventHandler MoveEnd;
        /// </summary>
        /// Occurs at the start of a resize event.
        /// <summary>
        public event EventHandler ResizeBegin;
        /// </summary>
        /// Occurs at the end of a resize event.
        /// <summary>
        public event EventHandler ResizeEnd;
        /// </summary>
        /// Occurs when the color of the control changes.
        /// <summary>
        public event EventHandler ColorChanged;
        /// </summary>
        /// Occurs when the color of the control's text changes.
        /// <summary>
        public event EventHandler TextColorChanged;
        /// </summary>
        /// Occurs when the color of the control's background changes.
        /// <summary>
        public event EventHandler BackColorChanged;
        /// </summary>
        /// Occurs when the text of the control changes.
        /// <summary>
        public event EventHandler TextChanged;
        /// </summary>
        /// Occurs when the control's anchor values change.
        /// <summary>
        public event EventHandler AnchorChanged;
        /// </summary>
        /// Occurs when the control's skin is changing.
        /// <summary>
        public event EventHandler SkinChanging;
        /// </summary>
        /// Occurs when the control's skin has changed.
        /// <summary>
        public event EventHandler SkinChanged;
        /// </summary>
        /// Occurs when the value of the control's parent changes.
        /// <summary>
        public event EventHandler ParentChanged;
        /// </summary>
        /// Occurs when the value of the control's root changes.
        /// <summary>
        public event EventHandler RootChanged;
        /// </summary>
        /// Occurs when the visibility of the control changes.
        /// <summary>
        public event EventHandler VisibleChanged;
        /// </summary>
        /// Occurs when the control's enabled value changes.
        /// <summary>
        public event EventHandler EnabledChanged;
        /// </summary>
        /// Occurs when the transparency of the control changes.
        /// <summary>
        public event EventHandler AlphaChanged;
        /// </summary>
        /// Occurs when the control loses input focus.
        /// <summary>
        public event EventHandler FocusLost;
        /// </summary>
        /// Occurs when the control gains input focus.
        /// <summary>
        public event EventHandler FocusGained;
        /// </summary>
        /// Occurs when the control needs to draw itself.
        /// <summary>
        public event DrawEventHandler DrawTexture;
        #endregion

        #region Constructors 
        /// </summary>
        /// Creates a new Control.
        /// <summary>
        /// <param name="manager">GUI manager for the control.</param>
        public Control(Manager manager)
            : base(manager)
        {
            // Control needs a GUI manager and a loaded skin.
            if (Manager == null)
            {
                throw new Exception("Control cannot be created. Manager instance is needed.");
            }

            else if (Manager.Skin == null)
            {
                throw new Exception("Control cannot be created. No skin loaded.");
            }

            // Default text of the control is the name of the control.
            text = Utilities.DeriveControlName(this);
            root = this;

            // Initialize the control's skin.
            InitSkin();

            CheckLayer(skin, "Control");

            if (Skin != null)
            {
                SetDefaultSize(width, height);
                SetMinimumSize(MinimumWidth, MinimumHeight);
                ResizerSize = skin.ResizerSize;
            }

            // Add the control to the stack.
            Stack.Add(this);
        }
        #endregion
        /// </summary>
        /// Create a copy of the control
        /// <summary>
        /// <returns></returns>
        public object Clone()
        {
            return base.MemberwiseClone();
        }
        #region Destructors
        /// </summary>
        /// Releases resources used by the control and removes itself from parent and manager control lists.
        /// <summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (parent != null) parent.Remove(this);
                else if (Manager != null) Manager.Remove(this);
                if (Manager.OrderList != null) Manager.OrderList.Remove(this);

                // Possibly we added the menu to another parent than this control, 
                // so we dispose it manually, beacause in logic it belongs to this control.        
                if (contextMenu != null)
                {
                    contextMenu.Dispose();
                    contextMenu = null;
                }

                // Recursively disposing all controls. The collection might change from its children, 
                // so we check it on count greater than zero.
                if (controls != null)
                {
                    int c = controls.Count;
                    for (int i = 0; i < c; i++)
                    {
                        if (controls.Count > 0)
                        {
                            controls[0].Dispose();
                        }
                    }
                }

                // Disposes tooltip owned by Manager        
                if (toolTip != null && !Manager.Disposing)
                {
                    toolTip.Dispose();
                    toolTip = null;
                }

                // Removing this control from the global stack.
                Stack.Remove(this);

                // Release the render target used by the control.
                if (target != null)
                {
                    target.Dispose();
                    target = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Get Virtual Height
        /// </summary>
        /// Gets the virtual height of the control. (Total scrollable height of the client area, not just the region being displayed.)
        /// <summary>
        /// <returns></returns>
        private int GetVirtualHeight()
        {
            // Control may have scroll bars?
            if (this.Parent is Container && (this.Parent as Container).AutoScroll)
            {
                int maxy = 0;

                // Check the position and height of all child controls.
                foreach (Control c in Controls)
                {
                    if ((c.Anchor & Anchors.Bottom) != Anchors.Bottom && c.Visible)
                    {
                        // Virtual height is the distance to the bottom of the furthest child control.
                        if (c.Top + c.Height > maxy) maxy = c.Top + c.Height;
                    }
                }

                // Less than the actual size of the control?
                if (maxy < Height) maxy = Height;

                // Return virtual height.
                return maxy;
            }
            else
            {
                return Height;
            }
        }
        #endregion

        #region Get Virtual Width
        /// </summary>
        /// Gets the virtual width of the control. (Total scrollable width of the client area, not just the region being displayed.)
        /// <summary>
        /// <returns></returns>
        private int GetVirtualWidth()
        {
            // Control may have a scroll bar?
            if (this.Parent is Container && (this.Parent as Container).AutoScroll)
            {
                int maxx = 0;

                // Check the position and width of all child controls.
                foreach (Control c in Controls)
                {
                    if ((c.Anchor & Anchors.Right) != Anchors.Right && c.Visible)
                    {
                        // Virtual width is the distance to the right side of the furthest child control.
                        if (c.Left + c.Width > maxx) maxx = c.Left + c.Width;
                    }
                }

                // Less than actual height of the control? 
                if (maxx < Width) maxx = Width;

                return maxx;
            }
            else
            {
                return Width;
            }
        }
        #endregion

        #region Get Clipping Rect
        /// </summary>
        /// Gets the controls clipping region.
        /// <summary>
        /// <param name="c">Control to get the clip rectangle of.</param>
        /// <returns>Returns the specified control's clip rectangle.</returns>
        private Rectangle GetClippingRect(Control c)
        {
            Rectangle r = Rectangle.Empty;

            r = new Rectangle(c.OriginLeft - root.AbsoluteLeft,
                              c.OriginTop - root.AbsoluteTop,
                              c.OriginWidth,
                              c.OriginHeight);

            int x1 = r.Left;
            int x2 = r.Right;
            int y1 = r.Top;
            int y2 = r.Bottom;

            Control ctrl = c.Parent;
            while (ctrl != null)
            {
                int cx1 = ctrl.OriginLeft - root.AbsoluteLeft;
                int cy1 = ctrl.OriginTop - root.AbsoluteTop;
                int cx2 = cx1 + ctrl.OriginWidth;
                int cy2 = cy1 + ctrl.OriginHeight;

                if (x1 < cx1) x1 = cx1;
                if (y1 < cy1) y1 = cy1;
                if (x2 > cx2) x2 = cx2;
                if (y2 > cy2) y2 = cy2;

                ctrl = ctrl.Parent;
            }

            int fx2 = x2 - x1;
            int fy2 = y2 - y1;

            if (x1 < 0) x1 = 0;
            if (y1 < 0) y1 = 0;
            if (fx2 < 0) fx2 = 0;
            if (fy2 < 0) fy2 = 0;
            if (x1 > root.Width) { x1 = root.Width; }
            if (y1 > root.Height) { y1 = root.Height; }
            if (fx2 > root.Width) fx2 = root.Width;
            if (fy2 > root.Height) fy2 = root.Height;

            Rectangle ret = new Rectangle(x1, y1, fx2, fy2);

            return ret;
        }
        #endregion

        #region Create Render Target
        /// </summary>
        /// Creates the render target for the control.
        /// <summary>
        /// <param name="width">Width of the render target.</param>
        /// <param name="height">Height of the render target.</param>
        /// <returns>Returns the render target of the specified dimensions.</returns>
        private RenderTarget2D CreateRenderTarget(int width, int height)
        {
            if (width > 0 && height > 0)
            {
                return new RenderTarget2D(Manager.GraphicsDevice,
                                          width,
                                          height,
                                          false,
                                          SurfaceFormat.Color,
                                          DepthFormat.None,
                                          Manager.GraphicsDevice.PresentationParameters.MultiSampleCount,
                                          Manager._RenderTargetUsage);

            }

            return null;
        }
        #endregion

        #region Prepare Texture
        /// </summary>
        /// Draws the control on the render target.
        /// <summary>
        /// <param name="renderer">Render management object.</param>
        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        internal virtual void PrepareTexture(Renderer renderer, GameTime gameTime)
        {
            if (visible)
            {
                if (invalidated)
                {
                    OnDrawTexture(new DrawEventArgs(renderer, new Rectangle(0, 0, OriginWidth, OriginHeight), gameTime));

                    // Need to create or resize the render target?
                    if (target == null || target.Width < OriginWidth || target.Height < OriginHeight)
                    {
                        if (target != null)
                        {
                            target.Dispose();
                            target = null;
                        }

                        // Calculate the new dimensions of the render target. 
                        int w = OriginWidth + (Manager.TextureResizeIncrement - (OriginWidth % Manager.TextureResizeIncrement));
                        int h = OriginHeight + (Manager.TextureResizeIncrement - (OriginHeight % Manager.TextureResizeIncrement));

                        // But don't allow the control render target to be larger than the manager's render target.
                        if (h > Manager.TargetHeight) h = Manager.TargetHeight;
                        if (w > Manager.TargetWidth) w = Manager.TargetWidth;

                        target = CreateRenderTarget(w, h);
                    }

                    if (target != null && Manager != null && target.GraphicsDevice != null)
                    {
                        // Have the graphics device draw to the control's render target.
                        Manager.GraphicsDevice.SetRenderTarget(target);
                        target.GraphicsDevice.Clear(backColor);

                        // Draw the control.
                        Rectangle rect = new Rectangle(0, 0, OriginWidth, OriginHeight);
                        DrawControls(renderer, rect, gameTime, false);

                        // Unset the render target.
                        Manager.GraphicsDevice.SetRenderTarget(null);
                    }
                    invalidated = false;
                }
            }
        }
        #endregion

        #region Check Detached
        /// </summary>
        /// Determines if the control is detached.
        /// <summary>
        /// <param name="c">Control to check.</param>
        /// <returns>Returns true if the control or one of its parent controls are detached; false otherwise.</returns>
        private bool CheckDetached(Control c)
        {
            Control parent = c.Parent;

            // Are any of the parent controls detached?
            while (parent != null)
            {
                if (parent.Detached)
                {
                    return true;
                }
                parent = parent.Parent;
            }

            // Is this control detached.
            return c.Detached;
        }
        #endregion

        #region Draw Child Controls
        /// </summary>
        /// Draws the child controls on the render target.
        /// <summary>
        /// <param name="renderer">Render management object.</param>
        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// <param name="firstDetachedLevel"></param>
        public void DrawChildControls(Renderer renderer, GameTime gameTime, bool firstDetachedLevel)
        {
            // Has child controls?
            if (controls != null)
            {
                foreach (Control c in controls)
                {
                    // We skip detached controls for first level after root (they are rendered separately in Draw() method)
                    if (((c.Root == c.Parent && !c.Detached) || c.Root != c.Parent) && AbsoluteRect.Intersects(c.AbsoluteRect) && c.visible)
                    {
                        Rectangle oldRect = Manager.GraphicsDevice.ScissorRectangle;
                        Manager.GraphicsDevice.ScissorRectangle = GetClippingRect(c);

                        Rectangle rect = new Rectangle(c.OriginLeft - root.AbsoluteLeft, c.OriginTop - root.AbsoluteTop, c.OriginWidth, c.OriginHeight);

                        if (c.Root != c.Parent && ((!c.Detached && CheckDetached(c)) || firstDetachedLevel))
                        {
                            rect = new Rectangle(c.OriginLeft, c.OriginTop, c.OriginWidth, c.OriginHeight);
                            Manager.GraphicsDevice.ScissorRectangle = rect;
                        }

                        renderer.Begin(BlendingMode.Default);
                        c.DrawingRect = rect;

                        c.DrawControl(renderer, rect, gameTime);

                        DrawEventArgs args = new DrawEventArgs();
                        args.Rectangle = rect;
                        args.Renderer = renderer;
                        args.GameTime = gameTime;
                        c.OnDraw(args);
                        renderer.End();

                        c.DrawChildControls(renderer, gameTime, firstDetachedLevel);

                        c.DrawOutline(renderer, true);

                        Manager.GraphicsDevice.ScissorRectangle = oldRect;
                    }
                }
            }
        }
        #endregion

        #region Draw Controls
        /// </summary>
        /// 
        /// <summary>
        /// <param name="renderer"></param>
        /// <param name="rect"></param>
        /// <param name="gameTime"></param>
        /// <param name="firstDetach"></param>
        private void DrawControls(Renderer renderer, Rectangle rect, GameTime gameTime, bool firstDetach)
        {
            renderer.Begin(BlendingMode.Default);

            DrawingRect = rect;
            DrawControl(renderer, rect, gameTime);

            DrawEventArgs args = new DrawEventArgs();
            args.Rectangle = rect;
            args.Renderer = renderer;
            args.GameTime = gameTime;
            OnDraw(args);

            renderer.End();

            DrawChildControls(renderer, gameTime, firstDetach);
        }
        #endregion

        #region Draw Detached
        /// </summary>
        /// 
        /// <summary>
        /// <param name="control"></param>
        /// <param name="renderer"></param>
        /// <param name="gameTime"></param>
        private void DrawDetached(Control control, Renderer renderer, GameTime gameTime)
        {
            if (control.Controls != null)
            {
                foreach (Control c in control.Controls)
                {
                    if (c.Detached && c.Visible)
                    {
                        c.DrawControls(renderer, new Rectangle(c.OriginLeft, c.OriginTop, c.OriginWidth, c.OriginHeight), gameTime, true);
                    }
                }
            }
        }
        #endregion

        #region Render
        /// </summary>
        /// 
        /// <summary>
        /// <param name="renderer"></param>
        /// <param name="gameTime"></param>
        internal virtual void Render(Renderer renderer, GameTime gameTime)
        {
            if (visible && target != null)
            {
                bool draw = true;

                if (draw)
                {
                    renderer.Begin(BlendingMode.Default);
                    renderer.Draw(target, OriginLeft, OriginTop, new Rectangle(0, 0, OriginWidth, OriginHeight), Color.FromNonPremultiplied(255, 255, 255, Alpha > 255 ? 255 : (byte)Alpha));
                    renderer.End();

                    DrawDetached(this, renderer, gameTime);

                    DrawOutline(renderer, false);
                }
            }
        }
        #endregion

        #region Draw Outline
        /// </summary>
        /// 
        /// <summary>
        /// <param name="renderer"></param>
        /// <param name="child"></param>
        private void DrawOutline(Renderer renderer, bool child)
        {
            if (!OutlineRect.IsEmpty)
            {
                Rectangle r = OutlineRect;
                if (child)
                {
                    r = new Rectangle(OutlineRect.Left + (parent.AbsoluteLeft - root.AbsoluteLeft), OutlineRect.Top + (parent.AbsoluteTop - root.AbsoluteTop), OutlineRect.Width, OutlineRect.Height);
                }

                Texture2D t = Manager.Skin.Controls["Control.Outline"].Layers[0].Image.Resource;

                int s = resizerSize;
                Rectangle r1 = new Rectangle(r.Left + leftModifier, r.Top + topModifier, r.Width, s);
                Rectangle r2 = new Rectangle(r.Left + leftModifier, r.Top + s + topModifier, resizerSize, r.Height - (2 * s));
                Rectangle r3 = new Rectangle(r.Right - s + leftModifier, r.Top + s + topModifier, s, r.Height - (2 * s));
                Rectangle r4 = new Rectangle(r.Left + leftModifier, r.Bottom - s + topModifier, r.Width, s);

                Color c = Manager.Skin.Controls["Control.Outline"].Layers[0].States.Enabled.Color;

                renderer.Begin(BlendingMode.Default);
                if ((ResizeEdge & Anchors.Top) == Anchors.Top || !partialOutline) renderer.Draw(t, r1, c);
                if ((ResizeEdge & Anchors.Left) == Anchors.Left || !partialOutline) renderer.Draw(t, r2, c);
                if ((ResizeEdge & Anchors.Right) == Anchors.Right || !partialOutline) renderer.Draw(t, r3, c);
                if ((ResizeEdge & Anchors.Bottom) == Anchors.Bottom || !partialOutline) renderer.Draw(t, r4, c);
                renderer.End();
            }

            else if (DesignMode && Focused)
            {
                Rectangle r = ControlRect;
                if (child)
                {
                    r = new Rectangle(r.Left + (parent.AbsoluteLeft - root.AbsoluteLeft), r.Top + (parent.AbsoluteTop - root.AbsoluteTop), r.Width, r.Height);
                }

                Texture2D t = Manager.Skin.Controls["Control.Outline"].Layers[0].Image.Resource;

                int s = resizerSize;
                Rectangle r1 = new Rectangle(r.Left + leftModifier, r.Top + topModifier, r.Width, s);
                Rectangle r2 = new Rectangle(r.Left + leftModifier, r.Top + s + topModifier, resizerSize, r.Height - (2 * s));
                Rectangle r3 = new Rectangle(r.Right - s + leftModifier, r.Top + s + topModifier, s, r.Height - (2 * s));
                Rectangle r4 = new Rectangle(r.Left + leftModifier, r.Bottom - s + topModifier, r.Width, s);

                Color c = Manager.Skin.Controls["Control.Outline"].Layers[0].States.Enabled.Color;

                renderer.Begin(BlendingMode.Default);
                renderer.Draw(t, r1, c);
                renderer.Draw(t, r2, c);
                renderer.Draw(t, r3, c);
                renderer.Draw(t, r4, c);
                renderer.End();
            }
        }
        #endregion

        #region Set Position
        /// </summary>
        /// Sets the position of the control to the specified values.
        /// <summary>
        /// <param name="left">X position of the control.</param>
        /// <param name="top">Y position of the control.</param>
        public virtual void SetPosition(int left, int top)
        {
            this.left = left;
            this.top = top;
        }
        #endregion

        #region Set Size
        /// </summary>
        /// Sets the size of the control to the specified dimensions.
        /// <summary>
        /// <param name="width">Width of the control.</param>
        /// <param name="height">Height of the control.</param>
        public virtual void SetSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
        #endregion

        #region Set Anchor Margins
        /// </summary>
        /// 
        /// <summary>
        internal void SetAnchorMargins()
        {
            if (Parent != null)
            {
                anchorMargins.Left = Left;
                anchorMargins.Top = Top;
                anchorMargins.Right = Parent.VirtualWidth - Width - Left;
                anchorMargins.Bottom = Parent.VirtualHeight - Height - Top;
            }

            else
            {
                anchorMargins = new Margins();
            }
        }
        #endregion

        #region Process Anchor
        /// </summary>
        /// 
        /// <summary>
        /// <param name="e"></param>
        private void ProcessAnchor(ResizeEventArgs e)
        {
            if (((Anchor & Anchors.Right) == Anchors.Right) && ((Anchor & Anchors.Left) != Anchors.Left))
            {
                Left = Parent.VirtualWidth - Width - anchorMargins.Right;
            }

            else if (((Anchor & Anchors.Right) == Anchors.Right) && ((Anchor & Anchors.Left) == Anchors.Left))
            {
                Width = Parent.VirtualWidth - Left - anchorMargins.Right;
            }

            else if (((Anchor & Anchors.Right) != Anchors.Right) && ((Anchor & Anchors.Left) != Anchors.Left))
            {
                int diff = (e.Width - e.OldWidth);
                if (e.Width % 2 != 0 && diff != 0)
                {
                    diff += (diff / Math.Abs(diff));
                }
                Left += (diff / 2);
            }

            if (((Anchor & Anchors.Bottom) == Anchors.Bottom) && ((Anchor & Anchors.Top) != Anchors.Top))
            {
                Top = Parent.VirtualHeight - Height - anchorMargins.Bottom;
            }

            else if (((Anchor & Anchors.Bottom) == Anchors.Bottom) && ((Anchor & Anchors.Top) == Anchors.Top))
            {
                Height = Parent.VirtualHeight - Top - anchorMargins.Bottom;
            }

            else if (((Anchor & Anchors.Bottom) != Anchors.Bottom) && ((Anchor & Anchors.Top) != Anchors.Top))
            {
                int diff = (e.Height - e.OldHeight);
                if (e.Height % 2 != 0 && diff != 0)
                {
                    diff += (diff / Math.Abs(diff));
                }
                Top += (diff / 2);
            }
        }
        #endregion

        #region Init
        /// </summary>
        /// Initializes the control.
        /// <summary>
        public override void Init()
        {
            base.Init();

            OnMove(new MoveEventArgs());
            OnResize(new ResizeEventArgs());
        }
        #endregion

        #region Init Skin
        /// </summary>
        /// Initializes the control's skin.
        /// <summary>
        protected internal virtual void InitSkin()
        {
            if (Manager != null && Manager.Skin != null && Manager.Skin.Controls != null)
            {
                SkinControl s = Manager.Skin.Controls[Utilities.DeriveControlName(this)];
                if (s != null) Skin = new SkinControl(s);
                else Skin = new SkinControl(Manager.Skin.Controls["Control"]);
            }
            else
            {
                throw new Exception("Control skin cannot be initialized. No skin loaded.");
            }
        }
        #endregion

        #region Set Default Size
        /// </summary>
        /// Sets the default size of the control.
        /// <summary>
        /// <param name="width">Default width of the control.</param>
        /// <param name="height">Default height of the control.</param>
        /// <remarks>
        /// This method will only use the values specified if the control's 
        /// skin does not have default size values set.
        /// </remarks>
        protected virtual void SetDefaultSize(int width, int height)
        {
            if (skin.DefaultSize.Width > 0)
            {
                Width = skin.DefaultSize.Width;
            }

            else
            {
                Width = width;
            }

            if (skin.DefaultSize.Height > 0)
            {
                Height = skin.DefaultSize.Height;
            }

            else
            {
                Height = height;
            }
        }
        #endregion

        #region Set Minimum Size
        /// </summary>
        /// Sets the minimum size of the control.
        /// <summary>
        /// <param name="minimumWidth">Minimum width of the control.</param>
        /// <param name="minimumHeight">Minimum height of the control.</param>
        /// <remarks>
        /// This method will only use the values specified if the control's 
        /// skin does not have minimum size values set.
        /// </remarks>
        protected virtual void SetMinimumSize(int minimumWidth, int minimumHeight)
        {
            if (skin.MinimumSize.Width > 0)
            {
                MinimumWidth = skin.MinimumSize.Width;
            }

            else
            {
                MinimumWidth = minimumWidth;
            }

            if (skin.MinimumSize.Height > 0)
            {
                MinimumHeight = skin.MinimumSize.Height;
            }

            else
            {
                MinimumHeight = minimumHeight;
            }
        }
        #endregion

        #region On Device Settings Changed Event Handler
        /// </summary>
        /// Handler for when the graphics device's settings change.
        /// <summary>
        /// <param name="e"></param>
        protected internal void OnDeviceSettingsChanged(DeviceEventArgs e)
        {
            if (!e.Handled)
            {
                Invalidate();
            }
        }
        #endregion

        #region Draw Control
        /// </summary>
        /// Draws the control.
        /// <summary>
        /// <param name="renderer">Render management object.</param>
        /// <param name="rect">Destination rectangle where the control will be drawn.</param>
        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        public virtual void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            // Valid color specified for the control background?
            if (backColor != UndefinedColor && backColor != Color.Transparent)
            {
                // Draw the control's skin.
                renderer.Draw(Manager.Skin.Images["Control"].Resource, rect, backColor);
            }

            // Draw the control's layer.
            renderer.DrawLayer(this, skin.Layers[0], rect);
        }
        #endregion

        public virtual bool CheckPositionMouse(Point pos)
        {
            return (this.AbsoluteLeft <= pos.X &&
                    this.AbsoluteTop <= pos.Y &&
                    this.AbsoluteLeft + this.Width >= pos.X &&
                    this.AbsoluteTop + this.Height >= pos.Y &&
                    Manager.CheckParent(this, pos));
        }
        #region Update
        /// </summary>
        /// Updates the control.
        /// <summary>
        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        ControlsList list = new ControlsList();
        protected internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Update the control's tool tip.
            ToolTipUpdate(gameTime);
            // Update all child controls.
            if (controls != null)
            {
                list.Clear();
                list.AddRange(controls);
                foreach (Control c in list)
                {
                    c.Update(gameTime);
                }
            }
        }
        #endregion

        #region Check Layer
        /// </summary>
        /// Makes sure the specified skin and layer are actually defined.
        /// <summary>
        /// <param name="skin">Skin to check the layers of.</param>
        /// <param name="layer">Layer of the skin to check the existance of.</param>
        protected internal virtual void CheckLayer(SkinControl skin, string layer)
        {
            if (!(skin != null && skin.Layers != null && skin.Layers.Count > 0 && skin.Layers[layer] != null))
            {
                throw new Exception("Unable to read skin layer \"" + layer + "\" for control \"" + Utilities.DeriveControlName(this) + "\".");
            }
        }

        /// </summary>
        /// Makes sure the specified skin and layer are actually defined.
        /// <summary>
        /// <param name="skin">Skin to check the layers of.</param>
        /// <param name="layer">Index of the skin layer to check the existance of.</param>
        protected internal virtual void CheckLayer(SkinControl skin, int layer)
        {
            if (!(skin != null && skin.Layers != null && skin.Layers.Count > 0 && skin.Layers[layer] != null))
            {
                throw new Exception("Unable to read skin layer with index \"" + layer.ToString() + "\" for control \"" + Utilities.DeriveControlName(this) + "\".");
            }
        }
        #endregion

        #region Get Control
        /// </summary>
        /// Gets a child control by name.
        /// <summary>
        /// <param name="name">Name of the control to search for.</param>
        /// <returns>Returns the control with the specified name or null if not found.</returns>
        public virtual Control GetControl(string name)
        {
            Control ret = null;

            // Check all child controls.
            foreach (Control c in Controls)
            {
                // Matching name here?
                if (c.Name.ToLower() == name.ToLower())
                {
                    ret = c;
                    break;
                }

                // Not a match.
                else
                {
                    // Check this control's children.
                    ret = c.GetControl(name);

                    // Done if the control was found, otherwise search next sibling.
                    if (ret != null)
                    {
                        break;
                    }
                }
            }
            return ret;
        }
        #endregion

        #region Add
        /// </summary>
        /// Adds the specified control as a child to this control.
        /// <summary>
        /// <param name="control">Child control to add to the list.</param>
        public virtual void Add(Control control)
        {
            // Valid control received?
            if (control != null)
            {
                // Not already in the list?
                if (!controls.Contains(control))
                {
                    // Remove control from previous parent first?
                    if (control.Parent != null)
                    {
                        control.Parent.Remove(control);
                    }

                    // Notify manager that the control is under new management. 
                    else
                    {
                        Manager.Remove(control);
                    }

                    // Update control members.
                    control.Manager = Manager;
                    control.parent = this;
                    control.Root = root;
                    control.Enabled = (Enabled ? control.Enabled : Enabled);
                    controls.Add(control);

                    // Update this control's virtual dimensions to account for the new child.
                    virtualHeight = GetVirtualHeight();
                    virtualWidth = GetVirtualWidth();

                    Manager.DeviceSettingsChanged += new DeviceEventHandler(control.OnDeviceSettingsChanged);
                    Manager.SkinChanging += new SkinEventHandler(control.OnSkinChanging);
                    Manager.SkinChanged += new SkinEventHandler(control.OnSkinChanged);
                    Resize += new ResizeEventHandler(control.OnParentResize);

                    control.SetAnchorMargins();

                    if (!Suspended) OnParentChanged(new EventArgs());
                }
            }
        }
        #endregion

        #region Remove
        /// </summary>
        /// Removes the specified control from the child control list.
        /// <summary>
        /// <param name="control">Control to remove from the child control list.</param>
        public virtual void Remove(Control control)
        {
            // Valid control received?
            if (control != null)
            {
                // Control has focus and a root control?
                if (control.Focused && control.Root != null)
                {
                    // Pass focus to the root control.
                    control.Root.Focused = true;
                }

                // Otherwise just unfocus the control before removing it.
                else if (control.Focused)
                {
                    control.Focused = false;
                }

                controls.Remove(control);

                // Detach from this control and make it its own root.
                control.parent = null;
                control.Root = control;

                // Unhook control event handlers.
                Resize -= control.OnParentResize;
                Manager.DeviceSettingsChanged -= control.OnDeviceSettingsChanged;
                Manager.SkinChanging -= control.OnSkinChanging;
                Manager.SkinChanged -= control.OnSkinChanged;

                if (!Suspended) OnParentChanged(new EventArgs());
            }
        }
        #endregion

        #region Contains
        /// </summary>
        /// Determines if the specified control is a child or descendant of this control.
        /// <summary>
        /// <param name="control">Control to search for in the child control collection.</param>
        /// <param name="recursively">Recursively check the children of children when searching.
        /// If false is specified, only first-level children are checked.</param>
        /// <returns>Returns true if the specified control is a child of this control.</returns>
        public virtual bool Contains(Control control, bool recursively)
        {
            if (Controls != null)
            {
                foreach (Control c in Controls)
                {
                    if (c == control) return true;
                    if (recursively && c.Contains(control, true)) return true;
                }
            }
            return false;
        }
        #endregion

        #region Invalidate
        /// </summary>
        /// Invalidates the control and parent controls forcing a redraw.
        /// <summary>
        public virtual void Invalidate()
        {
            invalidated = true;

            if (parent != null)
            {
                parent.Invalidate();
            }
        }
        #endregion

        #region Bring To Front
        /// </summary>
        /// Brings the control to the front-most Z-order.
        /// <summary>
        public virtual void BringToFront()
        {
            if (Manager != null) Manager.BringToFront(this);
        }
        #endregion

        #region Send To Back
        /// </summary>
        /// Sends the control to the back of the Z-order.
        /// <summary>
        public virtual void SendToBack()
        {
            if (Manager != null) Manager.SendToBack(this);
        }
        #endregion

        #region Show
        /// </summary>
        /// Makes the control visible.
        /// <summary>
        public virtual void Show()
        {
            Visible = true;
        }
        #endregion

        #region Hide
        /// </summary>
        /// Hides the control from rendering.
        /// <summary>
        public virtual void Hide()
        {
            Visible = false;
        }
        #endregion

        #region Refresh
        /// </summary>
        /// Refreshes the control.
        /// <summary>
        public virtual void Refresh()
        {
            OnMove(new MoveEventArgs(left, top, left, top));
            OnResize(new ResizeEventArgs(width, height, width, height));
        }
        #endregion

        #region Send Message
        /// </summary>
        /// Sends an event message to the control. 
        /// <summary>
        /// <param name="message">Message to send to the control.</param>
        /// <param name="e">Event arguments for the message.</param>
        public virtual void SendMessage(Message message, EventArgs e)
        {
            MessageProcess(message, e);
        }
        #endregion

        #region Message Process
        /// </summary>
        /// Processes message events for the control.
        /// <summary>
        /// <param name="message">Event message to process.</param>
        /// <param name="e">Event arguments for the message.</param>
        protected virtual void MessageProcess(Message message, EventArgs e)
        {
            switch (message)
            {
                case Message.Click:
                    {
                        ClickProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.MouseDown:
                    {
                        MouseDownProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.MouseUp:
                    {
                        MouseUpProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.MousePress:
                    {
                        MousePressProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.MouseScroll:
                    {
                        MouseScrollProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.MouseMove:
                    {
                        MouseMoveProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.MouseOver:
                    {
                        MouseOverProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.MouseOut:
                    {
                        MouseOutProcess(e as MouseEventArgs);
                        break;
                    }
                case Message.GamePadDown:
                    {
                        GamePadDownProcess(e as GamePadEventArgs);
                        break;
                    }
                case Message.GamePadUp:
                    {
                        GamePadUpProcess(e as GamePadEventArgs);
                        break;
                    }
                case Message.GamePadPress:
                    {
                        GamePadPressProcess(e as GamePadEventArgs);
                        break;
                    }
                case Message.KeyDown:
                    {
                        KeyDownProcess(e as KeyEventArgs);
                        break;
                    }
                case Message.KeyUp:
                    {
                        KeyUpProcess(e as KeyEventArgs);
                        break;
                    }
                case Message.KeyPress:
                    {
                        KeyPressProcess(e as KeyEventArgs);
                        break;
                    }
            }
        }
        #endregion

        #region Game Pad Press Process
        /// </summary>
        /// Processes gamepad button press events for the control.
        /// <summary>
        /// <param name="e"></param>
        private void GamePadPressProcess(GamePadEventArgs e)
        {
            Invalidate();
            if (!Suspended) OnGamePadPress(e);
        }
        #endregion

        #region Game Pad Up Process
        /// </summary>
        /// Processes gamepad button up events for the control.
        /// <summary>
        /// <param name="e"></param>
        private void GamePadUpProcess(GamePadEventArgs e)
        {
            Invalidate();

            // Unpress the control? 
            if (e.Button == GamePadActions.Press && pressed[(int)e.Button])
            {
                pressed[(int)e.Button] = false;
            }

            // Fire gamepad up event if needed.
            if (!Suspended) OnGamePadUp(e);

            // Display the control's context menu?
            if (e.Button == GamePadActions.ContextMenu && !e.Handled)
            {
                if (contextMenu != null)
                {
                    contextMenu.Show(this, AbsoluteLeft + 8, AbsoluteTop + 8);
                }
            }
        }
        #endregion

        #region Game Pad Down Process
        /// </summary>
        /// Processes gamepad button down events for the control.
        /// <summary>
        /// <param name="e"></param>
        private void GamePadDownProcess(GamePadEventArgs e)
        {
            Invalidate();
            ToolTipOut();

            // Gamepad button press received?
            if (e.Button == GamePadActions.Press && !IsPressed)
            {
                pressed[(int)e.Button] = true;
            }

            if (!Suspended) OnGamePadDown(e);
        }
        #endregion

        #region Key Press Process
        /// </summary>
        /// Processes key press events for the control.
        /// <summary>
        /// <param name="e"></param>
        private void KeyPressProcess(KeyEventArgs e)
        {
            Invalidate();
            if (!Suspended) OnKeyPress(e);
        }
        #endregion

        #region Key Down Process
        /// </summary>
        /// Handles key down events for the control.
        /// <summary>
        /// <param name="e"></param>
        private void KeyDownProcess(KeyEventArgs e)
        {
            Invalidate();
            ToolTipOut();

            // Key press received?
            if (e.Key == Microsoft.Xna.Framework.Input.Keys.Space && !IsPressed)
            {
                pressed[(int)MouseButton.None] = true;
            }

            if (!Suspended) OnKeyDown(e);
        }
        #endregion

        #region Key Up Process
        /// </summary>
        /// Handles key up events for the control.
        /// <summary>
        /// <param name="e"></param>
        private void KeyUpProcess(KeyEventArgs e)
        {
            Invalidate();

            // Unpress button?
            if (e.Key == Microsoft.Xna.Framework.Input.Keys.Space && pressed[(int)MouseButton.None])
            {
                pressed[(int)MouseButton.None] = false;
            }

            if (!Suspended) OnKeyUp(e);

            if (e.Key == Microsoft.Xna.Framework.Input.Keys.Apps && !e.Handled)
            {
                if (contextMenu != null)
                {
                    contextMenu.Show(this, AbsoluteLeft + 8, AbsoluteTop + 8);
                }
            }
        }
        #endregion

        #region Mouse Down Process
        /// </summary>
        /// Processes mouse button down events for the control.
        /// <summary>
        /// <param name="e"></param>
        private void MouseDownProcess(MouseEventArgs e)
        {
            Invalidate();
            pressed[(int)e.Button] = true;

            if (e.Button == MouseButton.Left)
            {
                pressSpot = new Point(TransformPosition(e).Position.X, TransformPosition(e).Position.Y);

                if (CheckResizableArea(e.Position))
                {
                    pressDiff[0] = pressSpot.X;
                    pressDiff[1] = pressSpot.Y;
                    pressDiff[2] = Width - pressSpot.X;
                    pressDiff[3] = Height - pressSpot.Y;

                    IsResizing = true;
                    if (outlineResizing) OutlineRect = ControlRect;
                    if (!Suspended) OnResizeBegin(e);
                }
                else if (CheckMovableArea(e.Position))
                {
                    IsMoving = true;
                    if (outlineMoving) OutlineRect = ControlRect;
                    if (!Suspended) OnMoveBegin(e);
                }
            }

            ToolTipOut();

            if (!Suspended) OnMouseDown(TransformPosition(e));
        }
        #endregion

        #region Mouse Up Process
        /// </summary>
        /// Processes mouse button up events for the control.
        /// <summary>
        /// <param name="e"></param>
        private void MouseUpProcess(MouseEventArgs e)
        {
            Invalidate();
            if (pressed[(int)e.Button] || isMoving || isResizing)
            {
                pressed[(int)e.Button] = false;

                if (e.Button == MouseButton.Left)
                {
                    if (IsResizing)
                    {
                        IsResizing = false;
                        if (outlineResizing)
                        {
                            Left = OutlineRect.Left;
                            Top = OutlineRect.Top;
                            Width = OutlineRect.Width;
                            Height = OutlineRect.Height;
                            OutlineRect = Rectangle.Empty;
                        }
                        if (!Suspended) OnResizeEnd(e);
                    }
                    else if (IsMoving)
                    {
                        IsMoving = false;
                        if (outlineMoving)
                        {
                            Left = OutlineRect.Left;
                            Top = OutlineRect.Top;
                            OutlineRect = Rectangle.Empty;
                        }
                        if (!Suspended) OnMoveEnd(e);
                    }
                }
                if (!Suspended) OnMouseUp(TransformPosition(e));
            }
        }
        #endregion

        #region Mouse Press Process
        /// </summary>
        /// Processes mouse button press events for the control.
        /// <summary>
        /// <param name="e"></param>
        void MousePressProcess(MouseEventArgs e)
        {
            if (pressed[(int)e.Button] && !IsMoving && !IsResizing)
            {
                if (!Suspended) OnMousePress(TransformPosition(e));
            }
        }
        void MouseScrollProcess(MouseEventArgs e)
        {
            if (!IsMoving && !IsResizing && !Suspended && Manager.FocusedControl == this)
            {
                OnMouseScroll(e);
            }
        }
        #endregion

        #region Mouse Over Process
        /// </summary>
        /// Processes mouse over events for the control.
        /// <summary>
        /// <param name="e"></param>
        private void MouseOverProcess(MouseEventArgs e)
        {
            Invalidate();
            hovered = true;
            ToolTipOver();

#if (!XBOX && !XBOX_FAKE)
            if (cursor != null && Manager.Cursor != cursor) Manager.Cursor = cursor;
#endif

            if (!Suspended) OnMouseOver(e);
        }
        #endregion

        #region Mouse Out Process
        /// </summary>
        /// Processes mouse out events for the control.
        /// <summary>
        /// <param name="e"></param>
        private void MouseOutProcess(MouseEventArgs e)
        {
            Invalidate();
            hovered = false;
            ToolTipOut();

#if (!XBOX && !XBOX_FAKE)
            Manager.Cursor = Manager.Skin.Cursors["Default"].Resource;
#endif

            if (!Suspended) OnMouseOut(e);
        }
        #endregion

        #region Mouse Move Process
        /// </summary>
        /// Processes mouse move events for the control.
        /// <summary>
        /// <param name="e"></param>
        private void MouseMoveProcess(MouseEventArgs e)
        {
            if (CheckPosition(e.Position) && !inside)
            {
                inside = true;
                Invalidate();
            }
            else if (!CheckPosition(e.Position) && inside)
            {
                inside = false;
                Invalidate();
            }

            PerformResize(e);

            if (!IsResizing && IsMoving)
            {
                int x = (parent != null) ? parent.AbsoluteLeft : 0;
                int y = (parent != null) ? parent.AbsoluteTop : 0;

                int l = e.Position.X - x - pressSpot.X - leftModifier;
                int t = e.Position.Y - y - pressSpot.Y - topModifier;

                if (!Suspended)
                {
                    MoveEventArgs v = new MoveEventArgs(l, t, Left, Top);
                    OnValidateMove(v);

                    l = v.Left;
                    t = v.Top;
                }

                if (outlineMoving)
                {
                    OutlineRect = new Rectangle(l, t, OutlineRect.Width, OutlineRect.Height);
                    if (parent != null) parent.Invalidate();
                }
                else
                {
                    Left = l;
                    Top = t;
                }
            }

            if (!Suspended)
            {
                OnMouseMove(TransformPosition(e));
            }
        }
        #endregion

        #region Click Process
        /// </summary>
        /// Processes mouse button click events for the control.
        /// <summary>
        /// <param name="e"></param>
        private void ClickProcess(EventArgs e)
        {
            long timer = (long)TimeSpan.FromTicks(DateTime.Now.Ticks).TotalMilliseconds;

            MouseEventArgs ex = (e is MouseEventArgs) ? (MouseEventArgs)e : new MouseEventArgs();

            if ((doubleClickTimer == 0 || (timer - doubleClickTimer > Manager.DoubleClickTime)) ||
                !doubleClicks)
            {
                TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
                doubleClickTimer = (long)ts.TotalMilliseconds;
                doubleClickButton = ex.Button;

                if (!Suspended) OnClick(e);


            }
            else if (timer - doubleClickTimer <= Manager.DoubleClickTime && (ex.Button == doubleClickButton && ex.Button != MouseButton.None))
            {
                doubleClickTimer = 0;
                if (!Suspended) OnDoubleClick(e);
            }
            else
            {
                doubleClickButton = MouseButton.None;
            }

            if (ex.Button == MouseButton.Right && contextMenu != null && !e.Handled)
            {
                contextMenu.Show(this, ex.Position.X, ex.Position.Y);
            }
        }
        #endregion

        #region Tool Tip Update
        /// </summary>
        /// Updates the control's tool tip.
        /// <summary>
        private void ToolTipUpdate(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Tool tips enabled, control had one, and sufficient delay time has passed?
            if (Manager.ToolTipsEnabled && toolTip != null && toolTip.Visible == false && tooltipTimer > 0 && (TimeSpan.FromTicks(DateTime.Now.Ticks).TotalMilliseconds - tooltipTimer) >= Manager.ToolTipDelay)
            {
                toolTip.Visible = true;
                Manager.Add(toolTip);
                tooltipTimer = 0;
                return;
            }
            //Fade in
            if (!toolTipFadingOut && Manager.ToolTipsEnabled && toolTip != null && toolTip.Visible == true && toolTip.Alpha <= 255)
            {
                toolTip.Alpha += elapsed * 600;
                tooltipTimer = 0;
            }
            //Fade out
            else if (toolTipFadingOut && Manager.ToolTipsEnabled && toolTip != null && toolTip.Visible == true && toolTip.Alpha >= 0)
            {
                tooltipTimer = 0;
                toolTip.Alpha -= elapsed * 600;
                if (toolTip.Alpha <= 0)
                {
                    toolTipFadingOut = false;
                    toolTip.Visible = false;
                    tooltipTimer = 0;
                    Manager.Remove(toolTip);
                }
            }
        }
        #endregion

        #region Hover Update

        #endregion

        #region Tool Tip Over
        /// </summary>
        /// Updates the tool tip delay timer when the control is hovered.
        /// <summary>
        protected virtual void ToolTipOver()
        {
            // Update the tool tip delay timer when hovered.
            bool i = Manager.CheckParent(this, new Point(Mouse.GetState().X, Mouse.GetState().Y));
            if (i && Manager.ToolTipsEnabled && toolTip != null && tooltipTimer == 0)
            {
                toolTip.Alpha = 0;
                toolTipFadingOut = false;
                TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
                tooltipTimer = (long)ts.TotalMilliseconds;
            }
        }
        #endregion

        #region Tool Tip Out
        /// </summary>
        /// Resets the tool tip delay timer when the mouse position leaves the control boundaries.
        /// <summary>
        public virtual void ToolTipOut()
        {
            if (Manager.ToolTipsEnabled && toolTip != null)
            {
                toolTipFadingOut = true;
            }
        }
        #endregion

        #region Check Position
        /// </summary>
        /// Determines if the specified point is within the bounds of the control.
        /// <summary>
        /// <param name="pos">Position to test.</param>
        /// <returns>Returns true if "pos" if within the bounds of the control; false otherwise.</returns>
        private bool CheckPosition(Point pos)
        {
            if ((pos.X >= AbsoluteLeft) && (pos.X < AbsoluteLeft + Width))
            {
                if ((pos.Y >= AbsoluteTop) && (pos.Y < AbsoluteTop + Height))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Check Movable Area
        /// </summary>
        /// 
        /// <summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool CheckMovableArea(Point pos)
        {
            if (movable)
            {
                Rectangle rect = movableArea;

                if (rect == Rectangle.Empty)
                {
                    rect = new Rectangle(0, 0, width, height);
                }

                pos.X -= AbsoluteLeft;
                pos.Y -= AbsoluteTop;

                if ((pos.X >= rect.X) && (pos.X < rect.X + rect.Width))
                {
                    if ((pos.Y >= rect.Y) && (pos.Y < rect.Y + rect.Height))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region Check Resizable Area
        /// </summary>
        /// 
        /// <summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool CheckResizableArea(Point pos)
        {
            if (resizable)
            {
                pos.X -= AbsoluteLeft;
                pos.Y -= AbsoluteTop;

                if ((pos.X >= 0 && pos.X < resizerSize && pos.Y >= 0 && pos.Y < Height) ||
                    (pos.X >= Width - resizerSize && pos.X < Width && pos.Y >= 0 && pos.Y < Height) ||
                    (pos.Y >= 0 && pos.Y < resizerSize && pos.X >= 0 && pos.X < Width) ||
                    (pos.Y >= Height - resizerSize && pos.Y < Height && pos.X >= 0 && pos.X < Width))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Transform Position
        protected MouseEventArgs TransformPosition(MouseEventArgs e)
        {
            MouseEventArgs ee = new MouseEventArgs(e.State, e.Button, e.Position);
            ee.Difference = e.Difference;

            ee.Position.X = ee.State.X - AbsoluteLeft;
            ee.Position.Y = ee.State.Y - AbsoluteTop;
            return ee;
        }
        #endregion

        #region Check Width
        /// </summary>
        /// 
        /// <summary>
        /// <param name="w"></param>
        /// <returns></returns>
        private int CheckWidth(ref int w)
        {
            int diff = 0;

            if (w > MaximumWidth)
            {
                diff = MaximumWidth - w;
                w = MaximumWidth;
            }
            if (w < MinimumWidth)
            {
                diff = MinimumWidth - w;
                w = MinimumWidth;
            }

            return diff;
        }
        #endregion

        #region Check Height
        /// </summary>
        /// 
        /// <summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private int CheckHeight(ref int h)
        {
            int diff = 0;

            if (h > MaximumHeight)
            {
                diff = MaximumHeight - h;
                h = MaximumHeight;
            }
            if (h < MinimumHeight)
            {
                diff = MinimumHeight - h;
                h = MinimumHeight;
            }

            return diff;
        }
        #endregion

        #region Perform Resize
        /// </summary>
        /// 
        /// <summary>
        /// <param name="e"></param>
        private void PerformResize(MouseEventArgs e)
        {
            if (resizable && !IsMoving)
            {
                if (!IsResizing)
                {
#if (!XBOX && !XBOX_FAKE)
                    GetResizePosition(e);
                    Manager.Cursor = Cursor = GetResizeCursor();
#endif
                }

                if (IsResizing)
                {
                    invalidated = true;

                    bool top = false;
                    bool bottom = false;
                    bool left = false;
                    bool right = false;

                    if ((resizeArea == Alignment.TopCenter ||
                        resizeArea == Alignment.TopLeft ||
                        resizeArea == Alignment.TopRight) && (resizeEdge & Anchors.Top) == Anchors.Top)
                        top = true;

                    else if ((resizeArea == Alignment.BottomCenter ||
                             resizeArea == Alignment.BottomLeft ||
                             resizeArea == Alignment.BottomRight) && (resizeEdge & Anchors.Bottom) == Anchors.Bottom)
                        bottom = true;

                    if ((resizeArea == Alignment.MiddleLeft ||
                        resizeArea == Alignment.BottomLeft ||
                        resizeArea == Alignment.TopLeft) && (resizeEdge & Anchors.Left) == Anchors.Left)
                        left = true;

                    else if ((resizeArea == Alignment.MiddleRight ||
                             resizeArea == Alignment.BottomRight ||
                             resizeArea == Alignment.TopRight) && (resizeEdge & Anchors.Right) == Anchors.Right)
                        right = true;

                    int w = Width;
                    int h = Height;
                    int l = Left;
                    int t = Top;

                    if (outlineResizing && !OutlineRect.IsEmpty)
                    {
                        l = OutlineRect.Left;
                        t = OutlineRect.Top;
                        w = OutlineRect.Width;
                        h = OutlineRect.Height;
                    }

                    int px = e.Position.X - (parent != null ? parent.AbsoluteLeft : 0);
                    int py = e.Position.Y - (parent != null ? parent.AbsoluteTop : 0);

                    if (left)
                    {
                        w = w + (l - px) + leftModifier + pressDiff[0];
                        l = px - leftModifier - pressDiff[0] - CheckWidth(ref w);

                    }
                    else if (right)
                    {
                        w = px - l - leftModifier + pressDiff[2];
                        CheckWidth(ref w);
                    }

                    if (top)
                    {
                        h = h + (t - py) + topModifier + pressDiff[1];
                        t = py - topModifier - pressDiff[1] - CheckHeight(ref h);
                    }
                    else if (bottom)
                    {
                        h = py - t - topModifier + pressDiff[3];
                        CheckHeight(ref h);
                    }

                    if (!Suspended)
                    {
                        ResizeEventArgs v = new ResizeEventArgs(w, h, Width, Height);
                        OnValidateResize(v);

                        if (top)
                        {
                            // Compensate for a possible height change from Validate event
                            t += (h - v.Height);
                        }
                        if (left)
                        {
                            // Compensate for a possible width change from Validate event
                            l += (w - v.Width);
                        }
                        w = v.Width;
                        h = v.Height;
                    }

                    if (outlineResizing)
                    {
                        OutlineRect = new Rectangle(l, t, w, h);
                        if (parent != null) parent.Invalidate();
                    }
                    else
                    {
                        Width = w;
                        Height = h;
                        Top = t;
                        Left = l;
                    }
                }
            }
        }
        #endregion

        #region Get Resize Cursor
#if (!XBOX && !XBOX_FAKE)
        /// </summary>
        /// 
        /// <summary>
        /// <returns></returns>
        private Cursor GetResizeCursor()
        {
            Cursor cur = Cursor;
            switch (resizeArea)
            {
                case Alignment.TopCenter:
                    {
                        return ((resizeEdge & Anchors.Top) == Anchors.Top) ? Manager.Skin.Cursors["Vertical"].Resource : Cursor;
                    }
                case Alignment.BottomCenter:
                    {
                        return ((resizeEdge & Anchors.Bottom) == Anchors.Bottom) ? Manager.Skin.Cursors["Vertical"].Resource : Cursor;
                    }
                case Alignment.MiddleLeft:
                    {
                        return ((resizeEdge & Anchors.Left) == Anchors.Left) ? Manager.Skin.Cursors["Horizontal"].Resource : Cursor;
                    }
                case Alignment.MiddleRight:
                    {
                        return ((resizeEdge & Anchors.Right) == Anchors.Right) ? Manager.Skin.Cursors["Horizontal"].Resource : Cursor;
                    }
                case Alignment.TopLeft:
                    {
                        return ((resizeEdge & Anchors.Left) == Anchors.Left && (resizeEdge & Anchors.Top) == Anchors.Top) ? Manager.Skin.Cursors["DiagonalLeft"].Resource : Cursor;
                    }
                case Alignment.BottomRight:
                    {
                        return ((resizeEdge & Anchors.Bottom) == Anchors.Bottom && (resizeEdge & Anchors.Right) == Anchors.Right) ? Manager.Skin.Cursors["DiagonalLeft"].Resource : Cursor;
                    }
                case Alignment.TopRight:
                    {
                        return ((resizeEdge & Anchors.Top) == Anchors.Top && (resizeEdge & Anchors.Right) == Anchors.Right) ? Manager.Skin.Cursors["DiagonalRight"].Resource : Cursor;
                    }
                case Alignment.BottomLeft:
                    {
                        return ((resizeEdge & Anchors.Bottom) == Anchors.Bottom && (resizeEdge & Anchors.Left) == Anchors.Left) ? Manager.Skin.Cursors["DiagonalRight"].Resource : Cursor;
                    }
            }
            return Manager.Skin.Cursors["Default"].Resource;
        }
#endif
        #endregion

        #region Get Resize Position
        /// </summary>
        /// 
        /// <summary>
        /// <param name="e"></param>
        private void GetResizePosition(MouseEventArgs e)
        {
            int x = e.Position.X - AbsoluteLeft;
            int y = e.Position.Y - AbsoluteTop;
            bool l = false, t = false, r = false, b = false;

            resizeArea = Alignment.None;

            if (CheckResizableArea(e.Position))
            {
                if (x < resizerSize) l = true;
                if (x >= Width - resizerSize) r = true;
                if (y < resizerSize) t = true;
                if (y >= Height - resizerSize) b = true;

                if (l && t) resizeArea = Alignment.TopLeft;
                else if (l && b) resizeArea = Alignment.BottomLeft;
                else if (r && t) resizeArea = Alignment.TopRight;
                else if (r && b) resizeArea = Alignment.BottomRight;
                else if (l) resizeArea = Alignment.MiddleLeft;
                else if (t) resizeArea = Alignment.TopCenter;
                else if (r) resizeArea = Alignment.MiddleRight;
                else if (b) resizeArea = Alignment.BottomCenter;
            }
            else
            {
                resizeArea = Alignment.None;
            }
        }
        #endregion

        #region On Mouse Up Event Handler
        /// </summary>
        /// Handles mouse button up events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            if (MouseUp != null) MouseUp.Invoke(this, e);
        }
        protected virtual void OnMouseScroll(MouseEventArgs e)
        {
            if (MouseScroll != null) MouseScroll.Invoke(this, e);
        }
        #endregion

        #region On Mouse Down Event Handler
        /// </summary>
        /// Handles mouse button down events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            if (MouseDown != null) MouseDown.Invoke(this, e);
        }
        #endregion

        #region On Mouse Move Event Handler
        /// </summary>
        /// Handles mouse move events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            if (MouseMove != null) MouseMove.Invoke(this, e);
        }
        #endregion

        #region On Mouse Over Event Handler
        /// </summary>
        /// Handles mouse over events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnMouseOver(MouseEventArgs e)
        {
            if (MouseOver != null) MouseOver.Invoke(this, e);
        }
        #endregion

        #region On Mouse Out Event Handler
        /// </summary>
        /// Handle mouse out events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnMouseOut(MouseEventArgs e)
        {
            if (MouseOut != null) MouseOut.Invoke(this, e);
        }
        #endregion

        #region On Click Event Handler
        /// </summary>
        /// Handles mouse click events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnClick(EventArgs e)
        {
            if (Click != null) Click.Invoke(this, e);
        }
        #endregion

        #region On Double Click Event Handler
        /// </summary>
        /// Handles double click events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnDoubleClick(EventArgs e)
        {
            if (DoubleClick != null) DoubleClick.Invoke(this, e);
        }
        #endregion

        #region On Move Event Handler
        /// </summary>
        /// Handles move events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnMove(MoveEventArgs e)
        {
            if (parent != null) parent.Invalidate();
            if (Move != null) Move.Invoke(this, e);
        }
        #endregion

        #region On Resize Event Handler
        /// </summary>
        /// Handles resize events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnResize(ResizeEventArgs e)
        {
            Invalidate();
            if (Resize != null) Resize.Invoke(this, e);
        }
        #endregion

        #region On Validate Resize Event Handler
        /// </summary>
        /// Handles validation of new sizes after a move event takes place.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnValidateResize(ResizeEventArgs e)
        {
            if (ValidateResize != null) ValidateResize.Invoke(this, e);
        }
        #endregion

        #region On Validate Move Event Handler
        /// </summary>
        /// Handles validation of new positions after a move event takes place.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnValidateMove(MoveEventArgs e)
        {
            if (ValidateMove != null) ValidateMove.Invoke(this, e);
        }
        #endregion

        #region On Move Begin Event Handler
        /// </summary>
        /// Handler for when a move operation begins.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnMoveBegin(EventArgs e)
        {
            if (MoveBegin != null) MoveBegin.Invoke(this, e);
        }
        #endregion

        #region On Move End Event Handler
        /// </summary>
        /// Handler for when a move operation has finished.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnMoveEnd(EventArgs e)
        {
            if (MoveEnd != null) MoveEnd.Invoke(this, e);
        }
        #endregion

        #region On Resize Begin Event Handler
        /// </summary>
        /// Handler used when a resize operation has begun.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnResizeBegin(EventArgs e)
        {
            if (ResizeBegin != null) ResizeBegin.Invoke(this, e);
        }
        #endregion

        #region On Resize End Event Handler
        /// </summary>
        /// Handler used when a resize operation has completed.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnResizeEnd(EventArgs e)
        {
            if (ResizeEnd != null) ResizeEnd.Invoke(this, e);
        }
        #endregion

        #region On Parent Resize Event Handler
        /// </summary>
        /// Handles changes in the parent control's size for the control.
        /// <summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnParentResize(object sender, ResizeEventArgs e)
        {
            ProcessAnchor(e);
        }
        #endregion

        #region On Key Up Event Handler
        /// </summary>
        /// Handles key up events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnKeyUp(KeyEventArgs e)
        {
            if (KeyUp != null) KeyUp.Invoke(this, e);
        }
        #endregion

        #region On Key Down Event Handler
        /// </summary>
        /// Handles key down events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnKeyDown(KeyEventArgs e)
        {
            if (KeyDown != null) KeyDown.Invoke(this, e);
        }
        #endregion

        #region On Key Press Event Handler
        /// </summary>
        /// Handles key press events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnKeyPress(KeyEventArgs e)
        {
            if (KeyPress != null) KeyPress.Invoke(this, e);
        }
        #endregion

        #region On Gamepad Up Event Handler
        /// </summary>
        /// Handles gamepad button up events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnGamePadUp(GamePadEventArgs e)
        {
            if (GamePadUp != null) GamePadUp.Invoke(this, e);
        }
        #endregion

        #region On Gamepad Down Event Handler
        /// </summary>
        /// Handles gamepad button down events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnGamePadDown(GamePadEventArgs e)
        {
            if (GamePadDown != null) GamePadDown.Invoke(this, e);
        }
        #endregion

        #region On Gamepad Press Event Handler
        /// </summary>
        /// Handle gamepad button press events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnGamePadPress(GamePadEventArgs e)
        {
            if (GamePadPress != null) GamePadPress.Invoke(this, e);
        }
        #endregion

        #region On Draw Event Handler
        /// </summary>
        /// Handles draw events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected internal void OnDraw(DrawEventArgs e)
        {
            if (Draw != null) Draw.Invoke(this, e);
        }
        #endregion

        #region On Draw Texture Event Handler
        /// </summary>
        /// Handles draw texture events for the control. (Drawing to the control's render target.)
        /// <summary>
        /// <param name="e"></param>
        protected void OnDrawTexture(DrawEventArgs e)
        {
            if (DrawTexture != null) DrawTexture.Invoke(this, e);
        }
        #endregion

        #region On Color Changed Event Handler
        /// </summary>
        /// Handles color change events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnColorChanged(EventArgs e)
        {
            if (ColorChanged != null) ColorChanged.Invoke(this, e);
        }
        #endregion

        #region On Text Color Changed Event Handler
        /// </summary>
        /// Handles changes of the control's text color.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnTextColorChanged(EventArgs e)
        {
            if (TextColorChanged != null) TextColorChanged.Invoke(this, e);
        }
        #endregion

        #region On Back Color Changed Event Handler
        /// </summary>
        /// Handles changes of the control's background color.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnBackColorChanged(EventArgs e)
        {
            if (BackColorChanged != null) BackColorChanged.Invoke(this, e);
        }
        #endregion

        #region On Text Changed Event Handler
        /// </summary>
        /// Handles changes of the control's text.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnTextChanged(EventArgs e)
        {
            if (TextChanged != null) TextChanged.Invoke(this, e);
        }
        #endregion

        #region On Anchor Changed Event Handler
        /// </summary>
        /// Handles changes of the control's anchor points.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnAnchorChanged(EventArgs e)
        {
            if (AnchorChanged != null) AnchorChanged.Invoke(this, e);
        }
        #endregion

        #region On Skin Changed Event Handler
        /// </summary>
        /// Handles skin changed events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected internal virtual void OnSkinChanged(EventArgs e)
        {
            if (SkinChanged != null) SkinChanged.Invoke(this, e);
        }
        #endregion

        #region On Skin Changing Event Handler
        /// </summary>
        /// Handles skin changing events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected internal virtual void OnSkinChanging(EventArgs e)
        {
            if (SkinChanging != null) SkinChanging.Invoke(this, e);
        }
        #endregion

        #region On Parent Changed Event Handler
        /// </summary>
        /// Handles changes of the control's parent.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnParentChanged(EventArgs e)
        {
            if (ParentChanged != null) ParentChanged.Invoke(this, e);
        }
        #endregion

        #region On Root Changed Event Handler
        /// </summary>
        /// Handles changes of the root control of the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnRootChanged(EventArgs e)
        {
            if (RootChanged != null) RootChanged.Invoke(this, e);
        }
        #endregion

        #region On Visible Changed Event Handler
        /// </summary>
        /// Handles changes in the visibility of the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnVisibleChanged(EventArgs e)
        {
            if (VisibleChanged != null) VisibleChanged.Invoke(this, e);
        }
        #endregion

        #region On Enabled Changed Event Handler
        /// </summary>
        /// Handles changes in the enabled value of the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnEnabledChanged(EventArgs e)
        {
            if (EnabledChanged != null) EnabledChanged.Invoke(this, e);
        }
        #endregion

        #region On Alpha Changed Event Handler
        /// </summary>
        /// Handles changes in the alpha value of the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnAlphaChanged(EventArgs e)
        {
            if (AlphaChanged != null) AlphaChanged.Invoke(this, e);
        }
        #endregion

        #region On Focus Lost Event Handler
        /// </summary>
        /// Handles focus lost events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnFocusLost(EventArgs e)
        {
            if (FocusLost != null) FocusLost.Invoke(this, e);
        }
        #endregion

        #region On Focus Gained Event Handler
        /// </summary>
        /// Handles focus gained events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnFocusGained(EventArgs e)
        {
            if (FocusGained != null) FocusGained.Invoke(this, e);
        }
        #endregion

        #region On Mouse Press Event Handler
        /// </summary>
        /// Handles mouse press events for the control.
        /// <summary>
        /// <param name="e"></param>
        protected virtual void OnMousePress(MouseEventArgs e)
        {
            if (MousePress != null) MousePress.Invoke(this, e);
        }
        #endregion
    }
}

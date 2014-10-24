////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Controls                                         //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: Label.cs                                     //
//                                                            //
//      Version: 0.7                                          //
//                                                            //
//         Date: 11/09/2010                                   //
//                                                            //
//       Author: Tom Shane                                    //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//  Copyright (c) by Tom Shane                                //
//                                                            //
////////////////////////////////////////////////////////////////

#region Using

using Microsoft.Xna.Framework;

#endregion

namespace MonoForce.Controls
{
    public class Label : Control
    {

        #region Fields

        private Alignment alignment = Alignment.MiddleLeft;
        private bool ellipsis = true;

        #endregion

        #region Properties

        public virtual Alignment Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }

        public virtual bool Ellipsis
        {
            get { return ellipsis; }
            set { ellipsis = value; }
        }

        #endregion

        #region Construstors

        public Label(Manager manager)
            : base(manager)
        {
            CanFocus = false;
            Passive = true;

            //Default size
            Width = 64;
            Height = 16;
        }

        #endregion

        #region Methods

        public override void Init()
        {
            base.Init();
        }

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            //Don't draw the base control, instead, implement our own drawing
            //base.DrawControl(renderer, rect, gameTime);

            SkinLayer s = new SkinLayer(Skin.Layers[0]);
            s.Text.Alignment = alignment;
            renderer.DrawString(this, s, Text, rect, true, 0, 0, ellipsis);
        }

        #endregion

    }
}

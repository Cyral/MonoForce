////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Controls                                         //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: SideBar.cs                                   //
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
    public class SideBar : Panel
    {

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Construstors

        public SideBar(Manager manager)
            : base(manager)
        {
            // CanFocus = true;
        }

        #endregion

        #region Methods

        public override void Init()
        {
            base.Init();
        }

        protected internal override void InitSkin()
        {
            base.InitSkin();
            Skin = new SkinControl(Manager.Skin.Controls["SideBar"]);
        }

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            base.DrawControl(renderer, rect, gameTime);
        }

        #endregion

    }
}

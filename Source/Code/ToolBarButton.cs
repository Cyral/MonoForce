﻿////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Controls                                         //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: ToolBarButton.cs                             //
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
    public class ToolBarButton : Button
    {

        #region Construstors

        public ToolBarButton(Manager manager)
            : base(manager)
        {
            CanFocus = false;
            Text = "";
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
            Skin = new SkinControl(Manager.Skin.Controls["ToolBarButton"]);
        }

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            base.DrawControl(renderer, rect, gameTime);
        }

        #endregion

    }
}

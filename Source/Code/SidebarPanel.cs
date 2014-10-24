////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Controls                                         //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: SideBarPanel.cs                              //
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

using Microsoft.Xna.Framework.Graphics;

#endregion

namespace MonoForce.Controls
{
    public class SideBarPanel : Container
    {

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Construstors

        public SideBarPanel(Manager manager)
            : base(manager)
        {
            CanFocus = false;
            Passive = true;
            Width = 64;
            Height = 64;
        }

        #endregion

        #region Methods

        public override void Init()
        {
            base.Init();
        }

        #endregion

    }
}

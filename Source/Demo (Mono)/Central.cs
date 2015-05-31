////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Central                                          //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: Central.cs                                   //
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

#region //// Using /////////////

////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoForce.Controls;
using System.Runtime.InteropServices;
////////////////////////////////////////////////////////////////////////////

#endregion

namespace MonoForce.Demo
{
    
  public class Central: Game
  {

    #region //// Fields ////////////

    ////////////////////////////////////////////////////////////////////////////               
    private int afps = 0;
    private int fps = 0;
    private double et = 0;
    public static long Frames = 0;             
    ////////////////////////////////////////////////////////////////////////////

    #endregion   

    #region //// Constructors //////
    
    ////////////////////////////////////////////////////////////////////////////    
    public Central()
    {                                   
               
    }
    ////////////////////////////////////////////////////////////////////////////        
    
    #endregion  

		#region //// Methods ///////////
                
    ////////////////////////////////////////////////////////////////////////////
    protected override void Initialize()
    {      
      base.Initialize();                                
    }
    ////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////
    protected override void LoadContent()
    {
      base.LoadContent();
    }
    ////////////////////////////////////////////////////////////////////////////    
    

    ////////////////////////////////////////////////////////////////////////////
    protected override void Draw(GameTime gameTime)
    {
      Frames += 1;
      base.Draw(gameTime);            
    }
		////////////////////////////////////////////////////////////////////////////
  	#endregion 
  	
	}
}
////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Controls                                         //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: Disposable.cs                                //
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

using System;

#endregion

namespace MonoForce.Controls
{

    public abstract class Disposable : Unknown, IDisposable
    {

        #region Fields

        private static int count = 0;

        #endregion

        #region Properties

        public static int Count { get { return count; } }

        #endregion

        #region Constructors

        protected Disposable()
        {
            count += 1;
        }

        #endregion

        #region Destructors

        //////////////////////////////////////////////////////////////////////////
        ~Disposable()
        {
            Dispose(false);
        }
        //////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////	  	  
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        //////////////////////////////////////////////////////////////////////////	  	  	  

        //////////////////////////////////////////////////////////////////////////	  	  	  
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                count -= 1;
            }
        }
        //////////////////////////////////////////////////////////////////////////	  	  	  	  	 

        #endregion

    }

}

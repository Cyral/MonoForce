using System;

namespace MonoForce.Controls
{
    public abstract class Disposable : Unknown, IDisposable
    {
        /// </summary>
        /// Number of references to this object.
        /// <summary>
        private static int count;

        public static int Count
        {
            get { return count; }
        }

        /// </summary>
        /// Creates a reference counted object.
        /// <summary>
        protected Disposable()
        {
            count += 1;
//GC.SuppressFinalize(this);
        }

        #region IDisposable Members

        /// </summary>
        /// Cleans up after the disposable object.
        /// <summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
//GC.SuppressFinalize(this);
        }

        #endregion

        /// <param name="disposing"></param>
        /// </summary>
        /// Decreases the object's reference count.
        /// <summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                count -= 1;
//GC.SuppressFinalize(this);
            }
//GC.SuppressFinalize(this);
        }

        /// </summary>
        /// Releases resources used by the object.
        /// <summary>
        ~Disposable()
        {
            Dispose(false);
//GC.SuppressFinalize(this);
        }
    }

//GC.SuppressFinalize(this);
}
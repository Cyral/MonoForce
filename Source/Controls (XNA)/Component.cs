using System;
using Microsoft.Xna.Framework;

namespace MonoForce.Controls
{
    public class Component : Disposable
    {
        public virtual bool Initialized
        {
            get { return initialized; }
        }

        public virtual Manager Manager
        {
            get { return manager; }
            set { manager = value; }
        }

        /// </summary>
        /// Indicates if the component has been initialized or not.
        /// <summary>
        private bool initialized;

        /// </summary>
        /// GUI manager for the component.
        /// <summary>
        private Manager manager;

        /// <param name="manager">GUI manager for the component.</param>
        /// </summary>
        /// Creates a new Component.
        /// <summary>
        public Component(Manager manager)
        {
            if (manager != null)
            {
                this.manager = manager;
            }
            else
            {
                throw new Exception("Component cannot be created. Manager instance is needed.");
            }
        }

        /// </summary>
        /// Initializes the component.
        /// <summary>
        public virtual void Init()
        {
            initialized = true;
        }

        /// <param name="gameTime">Snapshot of the application's timing values.</param>
        /// </summary>
        /// Updates the component.
        /// <summary>
        protected internal virtual void Update(GameTime gameTime)
        {
        }

        /// <param name="disposing"></param>
        /// </summary>
        /// Releases resources used by the component.
        /// <summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
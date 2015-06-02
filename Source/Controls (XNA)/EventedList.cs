using System;
using System.Collections.Generic;

namespace MonoForce.Controls
{
    public class EventedList<T> : List<T>
    {
        public EventedList()
        {
        }

        public EventedList(int capacity) : base(capacity)
        {
        }

        public EventedList(IEnumerable<T> collection) : base(collection)
        {
        }

        /// <param name="item">Item to add to the collection.</param>
        /// </summary>
        /// Adds a new item to the collection.
        /// <summary>
        public new void Add(T item)
        {
            var c = Count;
            base.Add(item);
            if (ItemAdded != null && c != Count) ItemAdded.Invoke(this, new EventArgs());
        }

        /// <param name="collection">Collection of items to add to the collection.</param>
        /// </summary>
        /// Adds a collection of items to the collection.
        /// <summary>
        public new void AddRange(IEnumerable<T> collection)
        {
            var c = Count;
            base.AddRange(collection);
            if (ItemAdded != null && c != Count) ItemAdded.Invoke(this, new EventArgs());
        }

        /// </summary>
        /// Removes all the items from the collection.
        /// <summary>
        public new void Clear()
        {
            var c = Count;
            base.Clear();
            if (ItemRemoved != null && c != Count) ItemRemoved.Invoke(this, new EventArgs());
        }

        /// <param name="item">Item to be inserted into the collection.</param>
        /// <param name="index">Zero-based index defining the insertion position.</param>
        /// </summary>
        /// Inserts a new item into the collection at the specified index.
        /// <summary>
        public new void Insert(int index, T item)
        {
            var c = Count;
            base.Insert(index, item);
            if (ItemAdded != null && c != Count) ItemAdded.Invoke(this, new EventArgs());
        }

        /// <param name="collection">Collection of items to add to the collection at the specified index.</param>
        /// <param name="index">Zero-based index where the collection will be inserted.</param>
        /// </summary>
        /// Inserts a collection of items into the collection at the specified position.
        /// <summary>
        public new void InsertRange(int index, IEnumerable<T> collection)
        {
            var c = Count;
            base.InsertRange(index, collection);
            if (ItemAdded != null && c != Count) ItemAdded.Invoke(this, new EventArgs());
        }

        /// </summary>
        /// Occurs when an item is added to the list.
        /// <summary>
        public event EventHandler ItemAdded;

        /// </summary>
        /// Occurs when an item is removed from the list.
        /// <summary>
        public event EventHandler ItemRemoved;

        /// <param name="obj">Item to remove from the collection.</param>
        /// </summary>
        /// Removes the specified item from the collection.
        /// <summary>
        public new void Remove(T obj)
        {
            var c = Count;
            base.Remove(obj);
            if (ItemRemoved != null && c != Count) ItemRemoved.Invoke(this, new EventArgs());
        }

        /// <returns>Returns the number of items removed from the collection.</returns>
        /// <param name="match">Predicate method used to evaluate collection items.</param>
        /// </summary>
        /// Removes all items in the collection that match the specified condition.
        /// <summary>
        public new int RemoveAll(Predicate<T> match)
        {
            var c = Count;
            var ret = base.RemoveAll(match);
            if (ItemRemoved != null && c != Count) ItemRemoved.Invoke(this, new EventArgs());
            return ret;
        }

        /// <param name="index">Zero-based index specifying which item to remove.</param>
        /// </summary>
        /// Removes an item from the collection at the specified index.
        /// <summary>
        public new void RemoveAt(int index)
        {
            var c = Count;
            base.RemoveAt(index);
            if (ItemRemoved != null && c != Count) ItemRemoved.Invoke(this, new EventArgs());
        }

        /// <param name="count">Number of items to remove from the collection.</param>
        /// <param name="index">Zero-based index to begin removal.</param>
        /// </summary>
        /// Removes a range of items from the collection beginning at the specified index.
        /// <summary>
        public new void RemoveRange(int index, int count)
        {
            var c = Count;
            base.RemoveRange(index, count);
            if (ItemRemoved != null && c != Count) ItemRemoved.Invoke(this, new EventArgs());
        }
    }
}
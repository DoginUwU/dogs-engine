using System.Collections;
using System.Diagnostics;

namespace engine.Framework.Lists
{
    class SortedList<T> : ReadOnlyList<T>, IEnumerable<T>
    {
        private IComparer<T> comparer;

        public SortedList(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public new int Capacity => InternalList.Capacity;
        public new int Count => InternalList.Count;
        public bool IsFixedSize => ((IList)InternalList).IsFixedSize;
        public bool IsReadOnly => ((IList)InternalList).IsReadOnly;
        public bool IsSynchronized => ((IList)InternalList).IsSynchronized;
        public object SyncRoot => ((IList)InternalList).SyncRoot;

        public virtual int Add(T value)
        {
            Debug.Assert(value != null);
            Debug.Assert(value is T);

            int index = getAdditionIndex((T)value);
            InternalList.Insert(index, (T)value);

            return index;
        }

        private int getAdditionIndex(T value)
        {
            int index = BinarySearch(value, comparer);
            if (index < 0)
                index = ~index;
                
            for (; index < Count; index++)
            {
                if (comparer.Compare(this[index], value) != 0)
                    break;
            }

            return index;
        }

        public virtual void Clear()
        {
            InternalList.Clear();
        }

        public virtual bool Remove(T item)
        {
            return InternalList.Remove(item);
        }

        public virtual void RemoveAt(int index)
        {
            InternalList.RemoveAt(index);
        }

        public new IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

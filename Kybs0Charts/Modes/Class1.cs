using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kybs0Charts
{
    public sealed class DataModelCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
    {
        private readonly List<T> _headContents = new List<T>();
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _headContents.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _headContents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _headContents.Add(item);
        }

        public int Add(object value)
        {
            if (value is T T)
            {
                _headContents.Add(T);
            }
            else if (value is DataModelCollection<T> headerContentCollection)
            {
                _headContents.AddRange(headerContentCollection._headContents);
            }
            return _headContents.Count;
        }

        public bool Contains(object value)
        {
            return _headContents.Contains((T)value);
        }

        public void Clear()
        {
            _headContents.Clear();
        }

        public int IndexOf(object value)
        {
            return _headContents.IndexOf((T)value);
        }

        public void Insert(int index, object value)
        {
            _headContents.Insert(index, (T)value);
        }

        public void Remove(object value)
        {
            _headContents.Remove((T)value);
        }

        void IList.RemoveAt(int index)
        {
            _headContents.RemoveAt(index);
        }

        object IList.this[int index]
        {
            get => _headContents[index];
            set => _headContents[index] = (T)value;
        }

        public bool Contains(T item)
        {
            return _headContents.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _headContents.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _headContents.Remove(item);
        }

        public void CopyTo(Array array, int index)
        {
            _headContents.CopyTo((T[])array, index);
        }

        public int Count => _headContents.Count;

        public object SyncRoot { get; }

        public bool IsSynchronized { get; }

        public bool IsReadOnly { get; }

        public bool IsFixedSize { get; }

        public int IndexOf(T item)
        {
            return _headContents.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _headContents.Insert(index, item);
        }

        void IList<T>.RemoveAt(int index)
        {
            _headContents.RemoveAt(index);
        }

        public T this[int index]
        {
            get => _headContents[index];
            set => _headContents[index] = value;
        }

    }
}

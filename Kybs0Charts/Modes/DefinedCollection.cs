using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kybs0Charts
{
    public sealed class DefinedCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
    {
        private readonly List<T> _contents = new List<T>();
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _contents.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _contents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _contents.Add(item);
        }

        public int Add(object value)
        {
            if (value is T T)
            {
                _contents.Add(T);
            }
            else if (value is DefinedCollection<T> headerContentCollection)
            {
                _contents.AddRange(headerContentCollection._contents);
            }
            return _contents.Count;
        }

        public bool Contains(object value)
        {
            return _contents.Contains((T)value);
        }

        public void Clear()
        {
            _contents.Clear();
        }

        public int IndexOf(object value)
        {
            return _contents.IndexOf((T)value);
        }

        public void Insert(int index, object value)
        {
            _contents.Insert(index, (T)value);
        }

        public void Remove(object value)
        {
            _contents.Remove((T)value);
        }

        void IList.RemoveAt(int index)
        {
            _contents.RemoveAt(index);
        }

        object IList.this[int index]
        {
            get => _contents[index];
            set => _contents[index] = (T)value;
        }

        public bool Contains(T item)
        {
            return _contents.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _contents.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _contents.Remove(item);
        }

        public void CopyTo(Array array, int index)
        {
            _contents.CopyTo((T[])array, index);
        }

        public int Count => _contents.Count;

        public object SyncRoot { get; }

        public bool IsSynchronized { get; }

        public bool IsReadOnly { get; }

        public bool IsFixedSize { get; }

        public int IndexOf(T item)
        {
            return _contents.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _contents.Insert(index, item);
        }

        void IList<T>.RemoveAt(int index)
        {
            _contents.RemoveAt(index);
        }

        public T this[int index]
        {
            get => _contents[index];
            set => _contents[index] = value;
        }

    }
}

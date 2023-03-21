using System;
using System.Collections;
using System.Linq;

namespace Utils
{
    /// <summary>An array indexed by an Enum</summary>
    /// <typeparam name="T">Type stored in array</typeparam>
    /// <typeparam name="U">Indexer Enum type</typeparam>
    /// https://stackoverflow.com/a/50969107/14813577
    [Serializable]
    public class ArrayByEnum<U, T> : IEnumerable where U : Enum // requires C# 7.3 or later
    {
        private readonly T[] _array;
        private readonly int _lower;

        public ArrayByEnum()
        {
            _lower = Convert.ToInt32(Enum.GetValues(typeof(U)).Cast<U>().Min());
            int upper = Convert.ToInt32(Enum.GetValues(typeof(U)).Cast<U>().Max());
            _array = new T[1 + upper - _lower];
        }

        public T this[U key]
        {
            get { return _array[Convert.ToInt32(key) - _lower]; }
            set { _array[Convert.ToInt32(key) - _lower] = value; }
        }

        public IEnumerator GetEnumerator()
        {
            return Enum.GetValues(typeof(U)).Cast<U>().Select(i => this[i]).GetEnumerator();
        }
    }
}

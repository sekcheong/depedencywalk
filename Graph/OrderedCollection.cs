using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace depedencywalk.Graph
{
	public class OrderedCollection<K, T> : IEnumerable<T>
	{

		private Dictionary<K, T> _hash;
		private List<T> _list;

		public OrderedCollection(IEqualityComparer<K> comparer)
		{
			_hash = new Dictionary<K, T>(comparer);
			_list = new List<T>();
		}

		public OrderedCollection()
		{
			_hash = new Dictionary<K, T>();
			_list = new List<T>();
		}

		public T this[int index]
		{
			get { return _list[index]; }
		}

		public bool Contains(K key)
		{
			return _hash.ContainsKey(key);
		}

		public T this[K key]
		{
			get
			{
				if (_hash.ContainsKey(key)) { return _hash[key]; }
				return default(T);
			}
		}

		public void Add(K key, T item)
		{
			if (_hash.ContainsKey(key)) {
				throw new Exception("Duplicated item: " + item.ToString());
			}
			_hash.Add(key, item);
			_list.Add(item);
		}

		public void Remove(K key)
		{
			if (!_hash.ContainsKey(key)) {
				throw new Exception("Item doesn't exist: " + key.ToString());
			}
			T item = _hash[key];
			_hash.Remove(key);
			_list.Remove(item);
		}

		public int Count { get { return _list.Count; } }

		public IEnumerator<T> GetEnumerator() { return _list.GetEnumerator(); }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}



	public class OrderedCollection<T> : IEnumerable<T>
	{
		private OrderedCollection<T, T> _collection;

		public OrderedCollection()
		{
			_collection = new OrderedCollection<T, T>();
		}

		public OrderedCollection(IEqualityComparer<T> comparer)
		{
			_collection = new OrderedCollection<T, T>(comparer);
		}

		public T this[int index]
		{
			get { return _collection[index]; }
		}

		public void Add(T item)
		{
			_collection.Add(item, item);
		}

		public void Remove(T item)
		{
			_collection.Remove(item);
		}

		public bool Contains(T item)
		{
			return _collection.Contains(item);
		}

		public int Count
		{
			get { return _collection.Count; }
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _collection.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}


}

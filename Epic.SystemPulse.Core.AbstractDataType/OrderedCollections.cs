using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.SystemPulse.AbstractDataType
{
	public class OrderedCollection<TKey, TValue> : IEnumerable<TValue>
	{
		private Dictionary<TKey, TValue> _hash;
		private List<TValue> _list;

		public OrderedCollection(IEqualityComparer<TKey> comparer)
		{
			_hash = new Dictionary<TKey, TValue>(comparer);
			_list = new List<TValue>();
		}

		public OrderedCollection()
		{
			_hash = new Dictionary<TKey, TValue>();
			_list = new List<TValue>();
		}

		public TValue this[int index]
		{
			get { return _list[index]; }
		}

		public bool Contains(TKey key)
		{
			return _hash.ContainsKey(key);
		}

		public TValue this[TKey key]
		{
			get
			{
				if (_hash.ContainsKey(key)) { return _hash[key]; }
				return default(TValue);
			}
		}

		public void Add(TKey key, TValue item)
		{
			if (_hash.ContainsKey(key)) {
				throw new Exception("Duplicated item: " + item.ToString());
			}
			_hash.Add(key, item);
			_list.Add(item);
		}

		public void Remove(TKey key)
		{
			if (!_hash.ContainsKey(key)) {
				throw new Exception("Item doesn't exist: " + key.ToString());
			}
			TValue item = _hash[key];
			_hash.Remove(key);
			_list.Remove(item);
		}

		public int Count { get { return _list.Count; } }

		public IEnumerator<TValue> GetEnumerator() { return _list.GetEnumerator(); }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}


	public class OrderedCollection<TValue> : IEnumerable<TValue>
	{
		private OrderedCollection<TValue, TValue> _collection;

		public OrderedCollection()
		{
			_collection = new OrderedCollection<TValue, TValue>();
		}

		public OrderedCollection(IEqualityComparer<TValue> comparer)
		{
			_collection = new OrderedCollection<TValue, TValue>(comparer);
		}

		public TValue this[int index]
		{
			get { return _collection[index]; }
		}

		public void Add(TValue item)
		{
			_collection.Add(item, item);
		}

		public void Remove(TValue item)
		{
			_collection.Remove(item);
		}

		public bool Contains(TValue item)
		{
			return _collection.Contains(item);
		}

		public int Count
		{
			get { return _collection.Count; }
		}

		public IEnumerator<TValue> GetEnumerator()
		{
			return _collection.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}


}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.SystemPulse.AbstractDataType
{
	public class OrderedMap<TKey, TValue> : IEnumerable<TValue>
	{
		private Dictionary<TKey, TValue> _map;
		private List<TValue> _list;

		public OrderedMap(IEqualityComparer<TKey> comparer)
		{
			_map = new Dictionary<TKey, TValue>(comparer);
			_list = new List<TValue>();
		}

		public OrderedMap()
		{
			_map = new Dictionary<TKey, TValue>();
			_list = new List<TValue>();
		}

		public TValue this[int index]
		{
			get { return _list[index]; }
		}		

		public bool Contains(TKey key)
		{
			return _map.ContainsKey(key);
		}

		public TValue this[TKey key]
		{
			get
			{
				if (_map.ContainsKey(key)) { return _map[key]; }
				return default(TValue);
			}
		}

		public void Add(TKey key, TValue item)
		{
			if (_map.ContainsKey(key)) {
				throw new Exception("Duplicated item: " + item.ToString());
			}
			_map.Add(key, item);
			_list.Add(item);
		}

		public void Remove(TKey key)
		{
			if (!_map.ContainsKey(key)) {
				throw new Exception("Item doesn't exist: " + key.ToString());
			}
			TValue item = _map[key];
			_map.Remove(key);
			_list.Remove(item);
		}

		public int Count { get { return _list.Count; } }
		
		public IEnumerable<TKey> Keys
		{
			get { return _map.Keys.ToList<TKey>(); }
		}

		public IEnumerable<TValue> Values
		{
			get { return _list.ToArray<TValue>(); }
		}

		public IEnumerator<TValue> GetEnumerator() { return _list.GetEnumerator(); }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

	}
}
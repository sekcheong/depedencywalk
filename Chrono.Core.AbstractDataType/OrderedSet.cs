using System;
using System.Collections.Generic;
using System.Text;

namespace Chrono.Core.AbstractDataType
{
	public class OrderedSet<T> : Set<T>,IEnumerable<T>
	{
		private List<T> _items = new List<T>();
		private bool _itemRemoved = false;

		
		public OrderedSet() { }

		
		public OrderedSet(bool readOnly) : this(readOnly, null) { }

		
		public OrderedSet(params T[] items) : this(false, items) { }


		public OrderedSet(bool readOnly, params T[] items)
			: base(readOnly, items)
		{
			if (items != null) {
				foreach (var item in items) {
					_items.Add(item);
				}
			}
		}


		public override bool Add(T item)
		{
			if (!base.Add(item)) return false;
			_items.Add(item);
			return true;
		}

		public override bool Remove(T item)
		{
			if (!base.Remove(item)) return false;
			_itemRemoved = true;
			return true;
		}

		public override void Clear()
		{
			base.Clear();
			_items.Clear();
		}

		public new T this[int index]
		{
			get
			{
				return _items[index];
			}
		}

		public override bool EquivalentTo(Set<T> other)
		{
			OrderedSet<T> b = other as OrderedSet<T>;
			if (b == null) return false;
			if (b.Count != this.Count) return false;
			for (int i = 0; i < _items.Count; i++) {
				if (!_items[i].Equals(b[i])) return false;
			}
			return true;
		}

		private List<T> GetItems()
		{			
			if (!_itemRemoved) return this._items;
			List<T> newItems = new List<T>();
			foreach (var n in _items) {
				if (base._set.Contains(n)) {
					newItems.Add(n);
				}
			}
			_items = newItems;			
			return _items;
		}


		public new System.Collections.IEnumerator GetEnumerator()
		{
			return this.GetItems().GetEnumerator();
		}
	}		
}
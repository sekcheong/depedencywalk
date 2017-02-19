using System;
using System.Collections.Generic;
using System.Text;

namespace Chrono.Core.AbstractDataType
{
	public class Set<T> : IEnumerable<T> 
	{
		protected bool _readOnly = false;
		protected HashSet<T> _set = new HashSet<T>();

		public Set() { }

		public Set(bool readOnly) : this(readOnly, null) { }
		
		public Set(params T[] items) : this(false, items) { }
		
		public Set(bool readOnly, params T[] items)			
		{
			this._readOnly = readOnly;
			if (items != null) {
				foreach (var item in items) {
					this.Add(item);
				}
			}
		}

		
		public virtual void Clear()
		{
			_set.Clear();
		}

		
		public virtual int Count
		{
			get { return _set.Count; }
		}

		
		public virtual bool Add(T item)
		{
			return _set.Add(item);
		}

		
		public virtual bool Remove(T item)
		{
			return _set.Remove(item);
		}

		
		public virtual bool Contains(T item)
		{
			return _set.Contains(item);
		}

		
		public virtual Set<T> Union(Set<T> other)
		{
			var a = this.Clone();
			a._readOnly = false;
			a._set.UnionWith(other._set);
			return a;
		}

		
		public virtual Set<T> Intersec(Set<T> other)
		{
			var a = this.Clone();
			a._readOnly = false;
			a._set.IntersectWith(other._set);
			return a;
		}

		
		public virtual Set<T> Except(Set<T> other)
		{
			var a = this.Clone();
			a._readOnly = false;
			a._set.ExceptWith(other._set);
			return a;
		}

		public T this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}


		public virtual bool EquivalentTo(Set<T> other)
		{
			return false;
		}

		
		public virtual Set<T> Clone()
		{
			Set<T> clone = new Set<T>();
			return clone;
		}

		
		public virtual T[] ToArray()
		{
			var ret = new T[_set.Count];
			var i = 0;
			foreach (var n in _set) {
				ret[i] = n;
			}
			return ret;
		}

	
		public static Set<T> operator + (Set<T> x, Set<T> y)
		{
			return x.Union(y);
		}


		public static Set<T> operator -(Set<T> x, Set<T> y)
		{
			return x.Except(y);
		}


		public static Set<T> operator ^ (Set<T> x, Set<T> y)
		{
			return x.Intersec(y);
		}


		public IEnumerator<T> GetEnumerator()
		{
			return _set.GetEnumerator();
		}


		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
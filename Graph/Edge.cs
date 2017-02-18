using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace depedencywalk.Graph
{
	public class Edge<T, E>
	{
		private Vertex<T> _v1;
		private Vertex<T> _v2;
		private E _value;

		public Edge(Vertex<T> v1, Vertex<T> v2) : this(v1, v2, default(E)) { }

		public Edge(Vertex<T> v1, Vertex<T> v2, E value)
		{
			this._v1 = v1;
			this._v2 = v2;
			this._value = value;
		}

		public Vertex<T> this[int vertex]
		{
			get
			{
				if (vertex == 0) return this._v1;
				if (vertex == 1) return this._v2;
				return null;
			}
		}


		public E Value
		{
			get { return this._value; }
			set { this._value = value; }
		}


		public override bool Equals(object obj)
		{
			Edge<T, E> e = obj as Edge<T, E>;
			if (e == null) {
				return false;
			}
			else {
				return this.GetHashCode() == obj.GetHashCode();
			}
		}


		public override int GetHashCode()
		{
			string edge = _v1.Name + "-" + _v2.Name;
			return edge.GetHashCode();
		}


		public override string ToString()
		{
			return "(" + _v1.ToString() + "," + _v2.ToString() + ")";
		}
	}
}

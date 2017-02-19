using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.SystemPulse.AbstractDataType.Graph
{
	public class Edge<TVertex, TEdge>
	{
		private Vertex<TVertex> _u;
		private Vertex<TVertex> _v;
		private TEdge _value;

		public Edge(Vertex<TVertex> u, Vertex<TVertex> v) : this(u, v, default(TEdge)) { }

		public Edge(Vertex<TVertex> u, Vertex<TVertex> v, TEdge value)
		{
			this._u = u;
			this._v = v;
			this._value = value;
		}

		public Vertex<TVertex> this[int vertex]
		{
			get
			{
				if (vertex == 0) return this.U;
				if (vertex == 1) return this.V;
				return null;
			}
		}

		public TEdge Value
		{
			get { return this._value; }
			set { this._value = value; }
		}
		 
		public virtual bool EquivalentTo(Edge<TVertex, TEdge> other)
		{
			return false;
		}

		public Vertex<TVertex> U { get { return _u; } }
		public Vertex<TVertex> V { get { return _v; } }

		public override string ToString()
		{
			return "(" + this.U.ToString() + "," + this.V.ToString() + ")";
		}
	}
}
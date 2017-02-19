using System;
using System.Collections.Generic;
using System.Text;
using Epic.SystemPulse.AbstractDataType;
namespace Epic.SystemPulse.Core.AbstractDataType.Graph
{
	public class Graph<TVertex, TEdge>
	{
		private Vertices<TVertex> _verticers = new Vertices<TVertex>();
		private Edges<TVertex, TEdge> _edges;

		private bool _directed;

		public Graph() : this(true) { }

		public Graph(bool directed)
		{
			_directed = directed;
			_edges = new Edges<TVertex, TEdge>();
		}

		public Vertices<TVertex> Vertices { get { return _verticers; } }

		public Edges<TVertex, TEdge> Edges { get { return _edges; } }

		public bool IsDirected
		{
			get { return _directed; }
		}
	}
}
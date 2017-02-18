using System;
using System.Collections.Generic;
using System.Text;


namespace Epic.SystemPulse.AbstractDataType.Graph
{

	public class DirectedGraph<TNode, TEdge>
	{
		private OrderedCollection<string, Vertex<TNode>> _vertices;
		private OrderedCollection<Edge<TNode, TEdge>> _edges;
		private OrderedCollection<Vertex<TNode>, OrderedCollection<Vertex<TNode>>> _adjacent;


		public enum Selection
		{
			SINGLETONS,
			NON_SINGLETONS,
			DFS,
			BFS
		}


		public DirectedGraph()
		{
			_vertices = new OrderedCollection<string, Vertex<TNode>>(StringComparer.InvariantCultureIgnoreCase);
			_edges = new OrderedCollection<Edge<TNode, TEdge>>();
			_adjacent = new OrderedCollection<Vertex<TNode>, OrderedCollection<Vertex<TNode>>>();
		}


		public Vertex<TNode> AddVertex(string name)
		{
			Vertex<TNode> v = new Vertex<TNode>(name, default(TNode));
			return AddVertex(v);
		}


		public Vertex<TNode> AddVertex(string name, TNode value)
		{
			Vertex<TNode> v = new Vertex<TNode>(name, value);
			return AddVertex(v);
		}


		public Vertex<TNode> AddVertex(Vertex<TNode> v)
		{
			if (string.IsNullOrEmpty(v.Name)) {
				throw new Exception("DirectedGraph.AddVertex(): Vertex name cannot be null.");
			}

			if (_vertices.Contains(v.Name)) {
				throw new Exception("DirectedGraph.AddVertex(): Duplicated vertex: " + v.Name);
			}
			_vertices.Add(v.Name, v);
			return v;
		}


		public Edge<TNode, TEdge> AddEdge(Vertex<TNode> v1, Vertex<TNode> v2)
		{
			return this.AddEdge(v1, v2, default(TEdge));
		}


		public Edge<TNode, TEdge> AddEdge(Vertex<TNode> v1, Vertex<TNode> v2, TEdge edgeValue)
		{
			var adj = this.GetAdjacentVertices(v1);
			if (adj.Contains(v2)) throw new Exception("DirectedGraph.AddEdge(): Edge already exist for (" + v1 + "," + v2 + ")");
			adj.Add(v2);
			Edge<TNode, TEdge> newEdge = new Edge<TNode, TEdge>(v1, v2, edgeValue);
			this._edges.Add(newEdge);
			return newEdge;
		}


		private OrderedCollection<Vertex<TNode>> GetAdjacentVertices(Vertex<TNode> v)
		{
			if (_adjacent.Contains(v)) {
				return _adjacent[v];
			}
			else {
				OrderedCollection<Vertex<TNode>> adj = new OrderedCollection<Vertex<TNode>>();
				_adjacent.Add(v, adj);
				return adj;
			}
		}


		public Vertex<TNode> GetVertex(string name, bool createNew)
		{
			if (_vertices.Contains(name)) {
				return _vertices[name];
			}

			if (createNew) {
				return this.AddVertex(name);
			}
			else {
				return null;
			}
		}


		public Vertex<TNode> GetVertex(string name)
		{
			return this.GetVertex(name, false);
		}


		public List<Vertex<TNode>> Select(Vertex<TNode> root, Selection selection)
		{
			switch (selection) {

				case Selection.SINGLETONS:
					return FindSingletons();

				case Selection.NON_SINGLETONS:
					return FindNonSingletons();

				case Selection.DFS:
					return DepthFirstSearch(root);

				case Selection.BFS:
					return BreadFirstSearch(root);

				default:
					return new List<Vertex<TNode>>();
			}
		}


		private List<Vertex<TNode>> FindNonSingletons()
		{
			List<Vertex<TNode>> list = new List<Vertex<TNode>>();
			foreach (var v in _vertices) {
				var adj = this.GetAdjacentVertices(v);
				if (adj.Count >= 0) {
					list.Add(v);
				}
			}
			return list;
		}


		private List<Vertex<TNode>> FindSingletons()
		{
			List<Vertex<TNode>> list = new List<Vertex<TNode>>();
			foreach (var v in _vertices) {
				var adj = this.GetAdjacentVertices(v);
				if (adj.Count == 0) {
					list.Add(v);
				}
			}
			return list;
		}


		private List<Vertex<TNode>> DepthFirstSearch(Vertex<TNode> root)
		{
			return new List<Vertex<TNode>>();
		}


		private List<Vertex<TNode>> BreadFirstSearch(Vertex<TNode> root)
		{
			return new List<Vertex<TNode>>();
		}

	}
}
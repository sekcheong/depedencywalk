using System;
using System.Collections.Generic;
using System.Text;


namespace Epic.SystemPulse.AbstractDataType.Graph
{

	public class DirectedGraph<T, E>
	{
		private OrderedCollection<string, Vertex<T>> _vertices;
		private OrderedCollection<Edge<T, E>> _edges;
		private OrderedCollection<Vertex<T>,OrderedCollection<Vertex<T>>> _adjacent;

		
		public enum Selection
		{
			SINGLETONS,
			NON_SINGLETONS,
			DFS,
			BFS
		}


		public DirectedGraph()
		{
			_vertices = new OrderedCollection<string, Vertex<T>>(StringComparer.InvariantCultureIgnoreCase);
			_edges = new OrderedCollection<Edge<T, E>>();
			_adjacent = new OrderedCollection<Vertex<T>, OrderedCollection<Vertex<T>>>();
		}


		public Vertex<T> AddVertex(string name)
		{
			Vertex<T> v = new Vertex<T>(name, default(T));
			return AddVertex(v);
		}

	
		public Vertex<T> AddVertex(string name, T value)
		{
			Vertex<T> v = new Vertex<T>(name, value);
			return AddVertex(v);
		}


		public Vertex<T> AddVertex(Vertex<T> v)
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

		
		public Edge<T, E> AddEdge(Vertex<T> v1, Vertex<T> v2)
		{
			return this.AddEdge(v1, v2, default(E));
		}


		public Edge<T,E> AddEdge(Vertex<T> v1, Vertex<T> v2, E edgeValue)
		{
			var adj = this.GetAdjacent(v1);
			if (adj.Contains(v2)) throw new Exception("DirectedGraph.AddEdge(): Edge already exist for (" + v1 + "," + v2 + ")");
			adj.Add(v2);
			Edge<T, E> newEdge = new Edge<T, E>(v1, v2, edgeValue);
			this._edges.Add(newEdge);
			return newEdge;
		}


		private OrderedCollection<Vertex<T>> GetAdjacent(Vertex<T> v)
		{
			if (_adjacent.Contains(v)) {
				return _adjacent[v];
			}
			else {
				OrderedCollection<Vertex<T>> adj = new OrderedCollection<Vertex<T>>();
				_adjacent.Add(v, adj);
				return adj;
			}
		}


		public Vertex<T> GetVertex(string name, bool createNew)
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


		public Vertex<T> GetVertex(string name)
		{
			return this.GetVertex(name, false);
		}

		
		public List<Vertex<T>> Select(Vertex<T> root, Selection selection)
		{
			switch (selection) {

				case Selection.SINGLETONS:
					return FindSingletons(root);

				case Selection.NON_SINGLETONS:
					return FindNonSingletons(root);

				case Selection.DFS:
					return DepthFirstSearch(root);

				case Selection.BFS:
					return BreadFirstSearch(root);

				default:
					return new List<Vertex<T>>();
			}			
		}


		private List<Vertex<T>> FindNonSingletons(Vertex<T> root)
		{
			throw new NotImplementedException();
		}


		private List<Vertex<T>> FindSingletons(Vertex<T> root)
		{
			throw new NotImplementedException();
		}


		private List<Vertex<T>> DepthFirstSearch(Vertex<T> root)
		{
			return new List<Vertex<T>>();
		}


		private List<Vertex<T>> BreadFirstSearch(Vertex<T> root)
		{
			return new List<Vertex<T>>();
		}

	}
}
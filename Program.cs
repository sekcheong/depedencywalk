using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace depedencywalk
{
	class Vertex
	{
		private string _name = "";

		public Vertex(string name, object data)
		{
			if (string.IsNullOrEmpty(name)) {
				throw new Exception("Vertex name must be a non empty string.");
			}
			
			this._name = name;
			this.Data = data;
		}

		public string Name { 
			get {return _name;} 			
		}

		public object Data { get; set; }

		public override bool Equals(object obj)
		{
			var v = obj as Vertex;
			if (v == null) return false;
			return (v.Name.ToLower() == this.Name.ToLower());
		}

		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		public override string ToString()
		{
			return this.Name;
		}

	}


	class DirectedGraph
	{		
		private List<Vertex> _vertices;
		private Dictionary<string, Vertex> _verticesMap;
		private Dictionary<Vertex, Dictionary<Vertex, bool>> _edges;


		public DirectedGraph()
		{
			this._verticesMap = new Dictionary<string, Vertex>(StringComparer.InvariantCultureIgnoreCase);
			this._vertices = new List<Vertex>();
			this._edges = new Dictionary<Vertex, Dictionary<Vertex, bool>>();
		}

		public static Dictionary<Vertex, bool> CreateMap()
		{
			return new Dictionary<Vertex, bool>();
		}

		public Vertex Vertex(string name)
		{
			if (_verticesMap.ContainsKey(name)) {
				return _verticesMap[name];
			}
			else {
				Vertex v = new Vertex(name, null);
				_verticesMap.Add(v.Name, v);
				_vertices.Add(v);
				return v;
			}
		}

		public bool AddEdge(Vertex v1, Vertex v2)
		{
			//if (v1.Equals(v2)) {
			//	throw new Exception("Vertex cannot have edge to itself: (" + v1 + ")");
			//}

			if (_edges.ContainsKey(v1)) {
				var adj = _edges[v1];
				if (!adj.ContainsKey(v2)) {
					adj.Add(v2, true);
					return true;
				}
			}
			else {
				var adj = DirectedGraph.CreateMap();
				adj.Add(v2, true);
				_edges.Add(v1, adj);
				return true;
			}

			return false;
		}


		public List<Vertex> Adjacent(Vertex v)
		{
			if (!_edges.ContainsKey(v)) return new List<Vertex>();
			var e = _edges[v];
			var adjacent = new List<Vertex>(e.Keys);
			return adjacent;
		}


		public List<Vertex> Vertices()
		{
			return this._vertices;
		}


		public bool IsCyclic()
		{
			foreach (Vertex v in this.Vertices()) {
				if (IsCyclic(v)) return true;
			}
			return false;
		}


		public bool IsCyclic(Vertex v)
		{
			var visited = DirectedGraph.CreateMap();
			var stack = new Stack<Vertex>();
			return IsCyclicPrivate(v, visited, stack,"");
		}


		private bool IsCyclicPrivate(Vertex v, Dictionary<Vertex, bool> visited, Stack<Vertex> stack, string level)
		{			
			stack.Push(v);
			//Trace.WriteLine(level + v.Name);
			foreach (var q in this.Adjacent(v)) {
				if (stack.Contains(q)) return true;				
				if (IsCyclicPrivate(q, visited, stack, level + "  ")) return true;
			}
			stack.Pop();			
			return false;
		}

	}


	class Bundler
	{

		private static List<string> GetUsingDeclaration(string filePath)
		{
			var usings = new List<string>();
			var declRegx = new Regex(@"^\s*//\s*\@using\s+", RegexOptions.Compiled);
			string tsFilePath = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) + ".ts");

			int lineNo = 0;
			string line;

			using (StreamReader sr = new StreamReader(File.Open(filePath, FileMode.Open))) {
				while (true) {

					line = sr.ReadLine();
					if (line == null) break;
					lineNo++;

					if (declRegx.IsMatch(line)) {
						line = line.Replace(@"//", "").Trim();

						//split the string by one or more spaces
						string[] pieces = Regex.Split(line, @"\s+");

						if (pieces.Length != 2) {
							throw new Exception("@using statement syntax error: " + Path.GetFileName(filePath) + "(" + "):" + line);
						}
						usings.Add(pieces[1]);
					}
				}
			}
			return usings;
		}


		private static void TraverseDirectoryTree(DirectoryInfo root, string baseName, DirectedGraph graph)
		{
			System.IO.FileInfo[] files = null;
			System.IO.DirectoryInfo[] subDirs = null;

			try {
				files = root.GetFiles("*.*");
			}
			catch (UnauthorizedAccessException) {
				Trace.TraceError("Unable to access the files in " + root.Name);
			}
			catch (DirectoryNotFoundException) {
				Trace.TraceError("Directory not found:" + root.Name);
			}

			if (files == null) return;

			foreach (System.IO.FileInfo fileInfo in files) {

				//skip all non TypeScript files
				if (!fileInfo.Name.ToLower().EndsWith(".ts")) continue;

				//search the TypeScript file for using statements
				List<string> usingFiles = GetUsingDeclaration(fileInfo.FullName);

				string qualifiedName = baseName + "." + Path.GetFileNameWithoutExtension(fileInfo.FullName);

				//get the vertex of the current TypeScript file
				Vertex currFileVertex = graph.Vertex(qualifiedName);
				currFileVertex.Data = fileInfo.FullName;

				//create an edge for each dependency of the current TypeScript file
				foreach (string u in usingFiles) {
					Vertex useFileVertex = graph.Vertex(u);
					graph.AddEdge(currFileVertex, useFileVertex);
					if (graph.IsCyclic(currFileVertex)) {
						throw new Exception("The file '" + currFileVertex.Data + "' introduced at least one cycle.");
					}
				}
			}

			//recursively traverse each sub directory 
			subDirs = root.GetDirectories();
			foreach (DirectoryInfo dirInfo in subDirs) {
				TraverseDirectoryTree(dirInfo, baseName + "." + dirInfo.Name, graph);
			}
		}


		public static string[] SearchScriptFiles(string basePath, string baseNamespace)
		{
			var graph = new DirectedGraph();
			//map to mark the visited vertex during traversal 
			var visited = DirectedGraph.CreateMap();
			//the list of file sorted in topological order
			var shouldVisit = new List<Vertex>();
			var dirInfo = new DirectoryInfo(basePath);

			Bundler.TraverseDirectoryTree(dirInfo, baseNamespace, graph);

			//Run DFS on the graph and produce a list of files in topological order
			List<Vertex> vertices = graph.Vertices();
			foreach (Vertex v in vertices) {
				DepthFirstSearch(graph, v, visited, shouldVisit);
			}

			var files = new List<string>();
			foreach (Vertex v in shouldVisit) {
				Trace.WriteLine(v.Name);
			}

			return files.ToArray();			
		}


		private static void DepthFirstSearch(DirectedGraph graph, Vertex start, Dictionary<Vertex, bool> visited, List<Vertex> shouldVisit)
		{			
			if (visited.ContainsKey(start)) return;
			foreach (var v in graph.Adjacent(start)) {
				DepthFirstSearch(graph, v, visited, shouldVisit);
			}
			visited.Add(start, true);
			shouldVisit.Add(start);
		}

	}


	class Program
	{
		static void Main(string[] args)
		{			
			string dir = @"C:\EpicSource\8.4\DLG-454545\SystemPulse\SystemPulse\Web\Scripts\SystemPulse";
			try {
				string[] files = Bundler.SearchScriptFiles(dir, "Chrono");
			}
			catch (Exception ex) {
				Trace.TraceError("ERROR:" + ex.Message);
			}
		}

	}
}
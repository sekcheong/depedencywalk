using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Epic.SystemPulse.AbstractDataType;
using Epic.SystemPulse.AbstractDataType.Graph;
using System.Diagnostics;
namespace Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			DirectedGraph<string, int> g = new DirectedGraph<string, int>();
			g.AddVertex("1");
			g.AddVertex("2");
			g.AddEdge("1", "2");

			foreach (var v in g.Vertices) {
				Trace.WriteLine(v);
			}

			Trace.WriteLine(" ");

			foreach (var e in g.Edges) {
				Trace.WriteLine(e);
				Trace.WriteLine("v1:" + e[0]);
				Trace.WriteLine("v2:" + e[1]);
			}

		}
	}
}

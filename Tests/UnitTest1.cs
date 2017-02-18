using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Epic.SystemPulse.AbstractDataType;
using Epic.SystemPulse.AbstractDataType.Graph;
namespace Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			DirectedGraph<string, int> g = new DirectedGraph<string, int>();
		}
	}
}

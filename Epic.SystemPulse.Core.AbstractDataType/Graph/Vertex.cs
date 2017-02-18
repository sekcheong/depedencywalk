using System;
using System.Collections.Generic;
using System.Text;


namespace Epic.SystemPulse.AbstractDataType.Graph
{
	public class Vertex<N>
	{
		private string _name;
		private N _value;

		public Vertex(string name, N value)
		{
			this._name = name;
			this._value = value;
		}


		public N Value
		{
			get { return this._value; }
			set { this._value = value; }
		}


		public string Name
		{
			get { return this._name; }
		}


		public override bool Equals(object obj)
		{
			Vertex<N> v = obj as Vertex<N>;
			if (v == null) return false;
			return v.GetHashCode() == this.GetHashCode();
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
}

using System;
using System.Collections.Generic;
using System.Text;


namespace Epic.SystemPulse.AbstractDataType.Graph
{
	public class Vertex<N>
	{
		private string _label;
		private N _value;

		public Vertex(string label, N value)
		{
			this._label = label;
			this._value = value;
		}


		public N Value
		{
			get { return this._value; }
			set { this._value = value; }
		}


		public string Label
		{
			get { return this._label; }
		}


		public override bool Equals(object obj)
		{
			Vertex<N> v = obj as Vertex<N>;
			if (v == null) return false;
			return v.GetHashCode() == this.GetHashCode();
		}


		public override int GetHashCode()
		{
			return this.Label.GetHashCode();
		}


		public override string ToString()
		{
			return this.Label;
		}
	}
}

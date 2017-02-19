using System;
using System.Collections.Generic;
using System.Text;


namespace Chrono.Core.AbstractDataType.Graph
{
	public class Vertex<T>
	{
		private string _label;
		private T _value;

		public Vertex(string label, T value)
		{
			this._label = label;
			this._value = value;
		}


		public T Value
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
			Vertex<T> v = obj as Vertex<T>;
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

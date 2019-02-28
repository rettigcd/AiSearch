using System;

namespace AiSearch.Adversary {

	/// <summary>
	/// Appears to be primarily used with Negamax.
	/// Thrown when trouble generating children or evaluating heuristic.
	/// </summary>
	public class SearchException : Exception {
	
		public SearchException(string msg, Exception inner, Node node )
			:base( msg, inner )
		{
			this.Node = node;
		}
		
		public Node Node{ get; private set; }

	}

}

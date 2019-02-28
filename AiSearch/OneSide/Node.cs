using System.Collections.Generic;

namespace AiSearch.OneSide {

	// Wraps around a simple state and provides links to ancestors so we can generate a trail later.
	// used by depth first
	public class Node<GameState> { // could add this: where T : IGameState<T> but that would require adding it in other places too.

		public Node( GameState state, Move<GameState> move, Node<GameState> prev=null ){
			State = state;
			Move = move;
			PreviousNode = prev;
			Depth = ( prev == null ) ? 0 : prev.Depth + 1;
		}

		public GameState State { get; private set; }

		/// <summary> Move that brought us to this state. Null for root node.</summary>
		public Move<GameState> Move { get; private set; }

		public Node<GameState> PreviousNode { get; private set; }

		/// <summary>0 for root.</summary>
		public int Depth{ get; private set; }

		public Node<GameState>[] GetNodePath() {
			var stack = new Stack<Node<GameState>>();
			Node<GameState> node = this;
			while( node != null ){
				stack.Push( node );
				node = node.PreviousNode;
			}
			return stack.ToArray();
		}

	}

}

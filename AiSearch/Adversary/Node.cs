using System.Collections.Generic;
using System.Linq;

namespace AiSearch.Adversary {

	// Wraps around a simple state and provides links to ancestors so we can generate a trail later.
	// used by depth first
	public class Node { // could add this: where T : IGameState<T> but that would require adding it in other places too.

		public Node( IAdversaryGs state, Node prev=null ){
			this.State = state;
			this.PreviousNode = prev;
			this.Depth = ( prev == null ) ? 0 : prev.Depth + 1;
		}

		public IAdversaryGs State { get; private set; }
		
		public IEnumerable<Node> GenerateChildren( IEnumerable<IAdversaryGs> childStates ){
		
			// This bit where we filter out the ancestors may not go here.
			// There may be some game-states where it is not posible to generate ancestors so we wouldn't need to check that.
			// Also, the current implementation of the BFS checks ALL visited nodes so checking ancestors is double work.

			return childStates
				.Select( child => new Node( child, this ) );
		}

		public Node PreviousNode { get; private set; }

		public int Depth{ get; private set; }

		public IAdversaryGs[] GetPath(){
			List<IAdversaryGs> states = new List<IAdversaryGs>();
			Node node = this;
			while( node != null ){
				states.Add( node.State );
				node = node.PreviousNode;
			}
			states.Reverse();
			return states.ToArray();
		}

		public Node[] GetNodePath() {
			List<Node> states = new List<Node>();
			Node node = this;
			while( node != null ){
				states.Add( node );
				node = node.PreviousNode;
			}
			states.Reverse();
			return states.ToArray();

		}

		public string MoveToGetHere => State.MoveToGetHere;

	}

}

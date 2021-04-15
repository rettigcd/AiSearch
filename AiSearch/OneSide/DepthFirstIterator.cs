using System.Collections.Generic;
using System.Linq;

namespace AiSearch.OneSide {

	// Typically used to traverse entire graph.
	// If graph to be traversed is too large or infinite, a depth limit is added.
	// DFS also lends itself much better to heuristic methods for choosing a likely-looking branch.

	// Beware of bi-directional legs that lead to an ancestor!
	// Wikipedia pseudo code shows labeling nodes as discovered.  However, this seems to require same memory as BFS.
	// Minimum labeling would be ancestors. 

	// Package: http://quickgraph.codeplex.com/ 

	// Typically used to traverse entire graph.
	// If graph to be traversed is too large or infinite, a depth limit is added.
	// DFS also lends itself much better to heuristic methods for choosing a likely-looking branch.

	/// <summary>
	/// .Iterate is not thread safe.
	/// </summary>
	public class DepthFirstIterator<T> {

		#region private fields

		// Instead of putting Node2(IGameState) items on the stack which force us to generate all the siblings, even the ones we aren't going to explore
		// we put a special 'FutureNode' on the stack that will generate the Child node when and if we want to explore it.
		readonly Stack<IDeferredNode> _stack;
		readonly int _maxDepth;
		readonly INodeMoveGenerator<T> _nodeMoveGenerator;

		#endregion

		/// <summary> Set to true to prevent repeating ancestor nodes. </summary>
		public bool DontRepeat { get; set; }

		public DepthFirstIterator( INodeMoveGenerator<T> nodeMoveGenerator, int maxDepth ) {
			_stack = new Stack<IDeferredNode>();
			_nodeMoveGenerator = nodeMoveGenerator;
			_maxDepth = maxDepth;
		}

		public DepthFirstIterator( IMoveGenerator<T> moveGenerator, int maxDepth )
			:this(new MoveGeneratorWrapper<T>(moveGenerator),maxDepth) {}


		public IEnumerable<Node<T>> Iterate( T startState ) {

			if( _maxDepth >= 0 )
				_stack.Push( new StaticDeferredNode(new Node<T>( startState, null )) );

			while( _stack.Count > 0 ) {
				Node<T> curNode = _stack.Pop().Value;
				if( DontRepeat && MatchesAncestor( curNode ) ) continue;
				yield return curNode;

				PushChildrenOntoStack( curNode );
			}

		}

		void PushChildrenOntoStack( Node<T> curNode ) {
			if( curNode.Depth < _maxDepth )
				foreach(var move in _nodeMoveGenerator.GetMoves( curNode ).Reverse() ) // reverse so first generated is first popped off of stack
					_stack.Push(new DeferredChild(curNode,move));
		}

		static bool MatchesAncestor( Node<T> stateToTest ){ 

			// start testing with parent
			Node<T> cur = stateToTest.PreviousNode;

			while( cur != null ){
			
				if( stateToTest.State.Equals( cur.State ) )
					return true; // match! 

				cur = cur.PreviousNode;
			}
			
			return false; // no match;
		}

		#region DeferredNode interface & classes

		interface IDeferredNode {
			Node<T> Value { get; }
		}

		// for root node
		class StaticDeferredNode : IDeferredNode {
			public StaticDeferredNode(Node<T> node) { _node = node; }
			public Node<T> Value => _node;
			readonly Node<T> _node;
		}

		/// <summary>
		/// Binds the Parent node to the child-generating move without actually generating the child.
		/// </summary>
		/// <remarks>Enables deferred child generation</remarks>
		class DeferredChild : IDeferredNode {
			public DeferredChild( Node<T> parent, IMove<T> move ) {
				_parent = parent;
				_move = move;
			}
			public Node<T> Value => new Node<T>( _move.GenerateChild(_parent.State), _move, _parent);

			readonly Node<T> _parent;
			readonly IMove<T> _move;
		}

		#endregion

	}

}
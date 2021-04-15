using System.Collections.Generic;
using System.Linq;

namespace AiSearch.OneSide {

	/// <summary>
	/// Iterates through all level-0 nodes of a tree 
	/// followed by all level-1 nodes
	/// followed by all level-2 nodes
	/// etc.
	/// </summary>
	/// <remarks>
	/// Memory intensive if high branching factor.
	/// Iterates top level down in order
	/// http://en.wikipedia.org/wiki/Breadth-first_search
	/// </remarks>
	public class BreadthFirstIterator<T> {

		#region private fields

		readonly Queue<Node<T>> _queue;
		readonly int _maxDepth;
		readonly INodeMoveGenerator<T> _nodeMoveGenerator;

		#endregion

		/// <summary> Set to true to prevent repeating ancestor nodes. </summary>
		public bool DontRepeat { get; set; }

		public BreadthFirstIterator( IMoveGenerator<T> moveGenerator, int maxDepth )
			:this(new MoveGeneratorWrapper<T>(moveGenerator),maxDepth) {}

		public BreadthFirstIterator( INodeMoveGenerator<T> nodeMoveGenerator, int maxDepth ) {
			_queue = new Queue<Node<T>>();
			_maxDepth = maxDepth;
			_nodeMoveGenerator = nodeMoveGenerator;
		}


		/// <returns>
		/// all nodes in a breadth-first order EXCEPT the duplicate nodes that were visited previously (at the same or shallower depth)
		/// </returns>
		public IEnumerable<Node<T>> Iterate( T startState ) {

			if( _maxDepth >= 0 )
				_queue.Enqueue( new Node<T>( startState, null ) );

			HashSet<T> visited = new HashSet<T>();

			while( _queue.Any() ){
				Node<T> cur = _queue.Dequeue();

				if( DontRepeat ) {
					if( visited.Contains( cur.State ) ) continue;
					visited.Add( cur.State );
				}

				yield return cur;
				QueueChildren( cur );

			}
		}

		void QueueChildren( Node<T> cur ) {

			if( cur.Depth < _maxDepth )
				foreach(IMove<T> move in _nodeMoveGenerator.GetMoves(cur))
					_queue.Enqueue( new Node<T>( move.GenerateChild(cur.State), move, cur ) );
		}

	}



}

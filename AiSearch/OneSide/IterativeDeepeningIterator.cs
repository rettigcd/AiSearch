using System.Collections.Generic;

namespace AiSearch.OneSide {

	// Combines Depth-First-Search(DFS)'s space efficiency 
	// and BFS's completeness (when branching factor is finite)
	// Optimal when path cost is non-decreasing function of the depth of the node.

	// Not as wastefull as it may seem beacuse most of the nodes are in the bottom level, 
	// so it doesn't matter much if upper leverls are visited multiple times.


	/// <summary>
	/// Iterates through nodes at level-0. 
	/// Then repeats with all nodes level 0-1.
	/// Then repeats with all nodes level 0-2.
	/// etc.
	/// </summary>
	public class IterativeDeepeningIterator<T> {

		NodeMoveGenerator<T> _nodeMoveGenerator;

		readonly int _maxDepth;
		readonly int _stepSize = 1;
		readonly int _startDepth = 0;

		public bool DontRepeat { get; set; }

		public IterativeDeepeningIterator( MoveGenerator<T> moveGenerator, int maxDepth, int stepSize = 1, int startDepth = 0 )
			:this(new MoveGeneratorWrapper<T>(moveGenerator),maxDepth,stepSize,startDepth)
		{}

		public IterativeDeepeningIterator( NodeMoveGenerator<T> nodeMoveGenerator, int maxDepth, int stepSize = 1, int startDepth = 0 ) {
			_nodeMoveGenerator = nodeMoveGenerator;
			_maxDepth = maxDepth;
			_stepSize = stepSize;
			_startDepth = startDepth;
		}

		/// <summary>
		/// Iterate over nodes in depth bands using depth-first memory.
		/// If stepSize is 1 level deep, then order is same as breadth-first search.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Node<T>> Iterate( T startState ) {

			int lastDepthLimit = -1;

			for( int currentDepthLimit = _startDepth; currentDepthLimit <= _maxDepth; currentDepthLimit += _stepSize ) {

				if( currentDepthLimit > _maxDepth )
					currentDepthLimit = _maxDepth;

				bool yieldedOne = false;
				var simpleDepthFirstIterator = new DepthFirstIterator<T>( _nodeMoveGenerator, currentDepthLimit ) { DontRepeat = this.DontRepeat };
				var dfsItems = simpleDepthFirstIterator.Iterate( startState );
				foreach( var n in dfsItems ) {
					// only return nodes in the current band - 
					// i.e. prevent returning repeated nodes with every iteration
					if( n.Depth > lastDepthLimit ) {
						yieldedOne = true;
						yield return n;
					}
				}

				if( !yieldedOne ) {
					break; // no more levels, no need to look deeper
				}

				lastDepthLimit = currentDepthLimit;
			}

		}

	}

}

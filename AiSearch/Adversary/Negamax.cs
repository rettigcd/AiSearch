using System;

namespace AiSearch.Adversary {

	// http://en.wikipedia.org/wiki/Negamax

	// Simplification of the minimax algorythm.
	// Alpha-beta pruning is technique to decrease # of nodes evaluated by minimax
	// Minimax is an adversarial search.


	public class Negamax<T> where T:IAdversaryGs {

		#region constructor

		/// <summary>
		/// Creates a Negamax algorythm that uses alpha-beta pruning to find best node.
		/// </summary>
		/// <param name="depth"></param>
		/// <param name="heuristic"></param>
		public Negamax(int depth, Func<T,int> heuristic){
			_depth = depth;
			_heuristic = heuristic;
		}

		#endregion

		public NodeWithScore<IAdversaryGs> Eval( IAdversaryGs gameState ){
			int color = this.GetColor( gameState );
			return color == 1
				? this.EvalWithNonAlternating( new Node( gameState ), -int.MaxValue, int.MaxValue, 1 )
				: -this.EvalWithNonAlternating( new Node( gameState ), int.MaxValue, -int.MaxValue, -1 );
		}

		public int NodeEvaluationCount { get; private set; }

		#region private methods
		
		int GetColor(IAdversaryGs t){
			return t.PositivePlayerToMove ? 1 : -1;
		}

		int ApplyHeuristic( Node node ){
			try{
				return _heuristic( (T)(IAdversaryGs)node.State ); // ??? is this safe?
			}
			catch(Exception ex){
				throw new SearchException( "Unable to evaluate heuristic for " + node.State.ToString(), ex, node );
			}
		
		}

		NodeWithScore<IAdversaryGs> EvalWithNonAlternating( Node node, int alpha, int beta, int color ){

			this.NodeEvaluationCount++;

			var cache = new ChildCacher( node );
			
			if( node.Depth == _depth
			   || cache.LazyChildren.Length == 0    // deferred generation!
			){

				return new NodeWithScore<IAdversaryGs>{
					Node = node,
					Score = color * this.ApplyHeuristic( node ),
				};
			}
			
		    NodeWithScore<IAdversaryGs> bestValue = NodeWithScore<IAdversaryGs>.MinValue;

			var childNodes = cache.LazyChildren;
			// !!! Order Moves

			foreach( var child in childNodes ) {

				int childColor = this.GetColor( (IAdversaryGs)child.State );
				NodeWithScore<IAdversaryGs> val = ( childColor != color )
					? -EvalWithNonAlternating(child, -beta, -alpha, childColor)
					:  EvalWithNonAlternating(child, alpha, beta, childColor);

				bestValue = NodeWithScore<IAdversaryGs>.Max( bestValue, val );
				alpha = Math.Max( alpha, val.Score );
				if( alpha >= beta ){
					break;
				}
			}

			return bestValue;

		}

		#endregion

		#region private fields

		readonly int _depth;
		readonly Func<T,int> _heuristic;

		#endregion

	}


}

using System;

namespace AiSearch.Adversary {

	// http://en.wikipedia.org/wiki/Negamax

	// Simplification of the minimax algorythm.
	// Alpha-beta pruning is technique to decrease # of nodes evaluated by minimax
	// Minimax is an adversarial search.


	public class Negamax<T> where T:AdversaryGs {

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

		public NodeWithScore<AdversaryGs> Eval( AdversaryGs gameState ){
			int color = this.GetColor( gameState );
			if( color == 1 )
				return this.EvalWithNonAlternating( new Node( gameState ), -int.MaxValue, int.MaxValue, 1 );
			else
				return -this.EvalWithNonAlternating( new Node( gameState ), int.MaxValue, -int.MaxValue, -1 );
		}

		public int NodeEvaluationCount { get; private set; }

		#region private methods
		
		int GetColor(AdversaryGs t){
			return t.PositivePlayerToMove ? 1 : -1;
		}

		int ApplyHeuristic( Node node ){
			try{
				return _heuristic( (T)(AdversaryGs)node.State ); // ??? is this safe?
			}
			catch(Exception ex){
				throw new SearchException( "Unable to evaluate heuristic for " + node.State.ToString(), ex, node );
			}
		
		}

		NodeWithScore<AdversaryGs> EvalWithNonAlternating( Node node, int alpha, int beta, int color ){

			this.NodeEvaluationCount++;

			var cache = new ChildCacher( node );
			
			if( node.Depth == _depth
			   || cache.LazyChildren.Length == 0    // deferred generation!
			){

				return new NodeWithScore<AdversaryGs>{
					Node = node,
					Score = color * this.ApplyHeuristic( node ),
				};
			}
			
		    NodeWithScore<AdversaryGs> bestValue = NodeWithScore<AdversaryGs>.MinValue;

			var childNodes = cache.LazyChildren;
			// !!! Order Moves

			foreach( var child in childNodes ) {

				int childColor = this.GetColor( (AdversaryGs)child.State );
				NodeWithScore<AdversaryGs> val = ( childColor != color )
					? -EvalWithNonAlternating(child, -beta, -alpha, childColor)
					:  EvalWithNonAlternating(child, alpha, beta, childColor);

				bestValue = NodeWithScore<AdversaryGs>.Max( bestValue, val );
				alpha = Math.Max( alpha, val.Score );
				if( alpha >= beta ){
					break;
				}
			}

			return bestValue;

		}

		#endregion

		#region private fields

		int _depth;
		
		Func<T,int> _heuristic;

		#endregion

	}


}

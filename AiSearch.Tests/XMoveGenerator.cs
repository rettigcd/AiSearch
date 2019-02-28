using System.Collections.Generic;
using System.Linq;
using AiSearch.OneSide;


namespace AiSearch.Tests
{
	class XMoveGenerator : MoveGenerator<BreadthTestState> {

		public IEnumerable<Move<BreadthTestState>> GetMoves( BreadthTestState s ) {
			BreadthTestState gameState = (BreadthTestState)s;
			return gameState.Children
				.Select(child => new BreadthTestMove( child.Name ) );
		}
	}



}

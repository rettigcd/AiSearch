using System.Collections.Generic;

namespace AiSearch.OneSide {

	// wraps move-generator so implementers of MoveGenerator<GameState> don't have to deal with the Node object if they don't want too.
	// but can if they need to
	internal class MoveGeneratorWrapper<GameState> : NodeMoveGenerator<GameState> {

		MoveGenerator<GameState> _moveGenerator;

		public MoveGeneratorWrapper(MoveGenerator<GameState> moveGenerator ) {
			_moveGenerator = moveGenerator;
		}

		public IEnumerable<Move<GameState>> GetMoves(Node<GameState> node ) {
			return _moveGenerator.GetMoves( node.State );
		}
	}

}

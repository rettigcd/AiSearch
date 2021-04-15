using System.Collections.Generic;

namespace AiSearch.OneSide {

	// wraps move-generator so implementers of MoveGenerator<GameState> don't have to deal with the Node object if they don't want too.
	// but can if they need to
	internal class MoveGeneratorWrapper<GameState> : INodeMoveGenerator<GameState> {
		readonly IMoveGenerator<GameState> _moveGenerator;

		public MoveGeneratorWrapper(IMoveGenerator<GameState> moveGenerator ) {
			_moveGenerator = moveGenerator;
		}

		public IEnumerable<IMove<GameState>> GetMoves(Node<GameState> node ) {
			return _moveGenerator.GetMoves( node.State );
		}
	}

}

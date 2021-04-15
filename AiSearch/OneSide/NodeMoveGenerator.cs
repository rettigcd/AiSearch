using System.Collections.Generic;

namespace AiSearch.OneSide {

	/// <summary>
	/// For move generators that need access to the move history via the linked list node.
	/// </summary>
	public interface INodeMoveGenerator<GameState> {
		IEnumerable<IMove<GameState>> GetMoves( Node<GameState> node );
	}

}
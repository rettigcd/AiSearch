﻿using System.Collections.Generic;

namespace AiSearch.OneSide {

	/// <summary>
	/// Picks children to generate but doesn't generate them.  Instead, it generates the instructions / command / move to generate them.
	/// </summary>
	/// <typeparam name="GameState"></typeparam>
	public interface IMoveGenerator<GameState> {

		// Enumerable so it doesn't have to generate all the moves if they are not needed.
		IEnumerable<IMove<GameState>> GetMoves(GameState s);

	}

}

using System.Collections.Generic;

namespace AiSearch.Adversary {

	public interface AdversaryGs {

		// Enumerable so it doesn't have to generate all children if they are not needed.
		IEnumerable<AdversaryGs> GenerateChildren();

		/// <summary>
		/// Description move that changed from the previous state to the current state.
		/// </summary>
		string MoveToGetHere { get; }

		/// <summary> Identifies who's turn it is. Used by Negamax. </summary>
		bool PositivePlayerToMove { get; }
		
	}


}

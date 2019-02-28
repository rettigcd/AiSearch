namespace AiSearch.OneSide {

	/// <summary>
	/// Represents a command to generate a child-state from some parent-state.
	/// </summary>
	public interface Move<GameState> {

		/// <summary>
		/// Like .ToString() but exlicitly meant to be read by people.
		/// </summary>
		string HumanReadable { get; }

		GameState GenerateChild( GameState state );

	}


}

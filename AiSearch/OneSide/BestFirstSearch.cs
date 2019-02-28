namespace AiSearch.OneSide {

	// Implement with a priority queue
	// Might encounter an already-visited node by a shorter path and give it a better score

	// A* is an example 
	// B* is an example


	//	OPEN = [initial state]
	//	CLOSED = []
	//	while OPEN is not empty
	//	do
	//	 1. Remove the best node from OPEN, call it n, add it to CLOSED.
	//	 2. If n is the goal state, backtrace path to n (through recorded parents) and return path.
	//	 3. Create n's successors.
	//	 4. For each successor do:
	//	       a. If it is not in CLOSED and it is not in OPEN: evaluate it, add it to OPEN, and record its parent.
	//	       b. Otherwise, if this new path is better than previous one, change its recorded parent. 
	//	          i.  If it is not in OPEN add it to OPEN. 
	//	          ii. Otherwise, adjust its priority in OPEN using this new evaluation. 
	//	done


	/// <summary>
	/// Description of BestFirstSearch.
	/// </summary>
	public class BestFirstSearch {

		//public BestFirstSearch(){}

	}

}

// Best first search.
// Generalization of Dijkstra's Algorithm.  http://en.wikipedia.org/wiki/Dijkstra%27s_algorithm

// Read more on:
// http://en.wikipedia.org/wiki/A*_search_algorithm
// http://en.wikipedia.org/wiki/Best-first_search
// http://en.wikipedia.org/wiki/Beam_search

namespace AiSearch.OneSide {

	// Cost = knowlege + heuristic or f(x) = g(x) + h(x)
	// h(x)	must be an admissible heuristic, 
	//		- it must not overestimate the distance to the goal
	//		- it calculates a value indicating the goal is no closer than
	//		- the goal is at least this far away.

	/// <summary>
	/// Description of AStar.
	/// </summary>
	public class AStar {
		// public AStar(){}

/*
		
		public void AStarX<T>(T start,goal) {
			HashSet<T> closedset = new HashSet<T>(); //  the empty set    // The set of nodes already evaluated.
			
			HashSet<T> openset = new HashSet<T>{ start };    // The set of tentative nodes to be evaluated, initially containing the start node
    		// !!!came_from := the empty map    // The map of navigated nodes.
 
    		g_score[start] := 0    // Cost from start along best known path.
    		// Estimated total cost from start to goal through y.
    		f_score[start] := g_score[start] + heuristic_cost_estimate(start, goal)
 
    		while openset is not empty
        		current := the node in openset having the lowest f_score[] value
        		if current = goal
            		return reconstruct_path(came_from, goal)
 
        		remove current from openset
        		add current to closedset
        		for each neighbor in neighbor_nodes(current)
            		if neighbor in closedset
                		continue
            		tentative_g_score := g_score[current] + dist_between(current,neighbor)
		 
            		if neighbor not in openset or tentative_g_score < g_score[neighbor] 
                		came_from[neighbor] := current
                		g_score[neighbor] := tentative_g_score
                		f_score[neighbor] := g_score[neighbor] + heuristic_cost_estimate(neighbor, goal)
                		if neighbor not in openset
                    		add neighbor to openset
		 
    		return failure
 
		function reconstruct_path(came_from,current)
    		total_path := [current]
    		while current in came_from:
        		current := came_from[current]
        		total_path.append(current)
    		return total_path
		
*/		
		
	}
}

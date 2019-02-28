/*

using System;
using System.Collections.Generic;

namespace AiSearch.OneSide {

	/// <summary>
	/// Dijkstra algo. finds shortest distance between 2 nodes, 
	/// NOT shortest distance to traverse all nodes.
	/// </summary>
	public class Dijkstra {

		static public void FindShortestPathToAllNodes( IEnumerable<object> allNodes, object startNode, NeighborProvider<Vertext> neighborhood ){

			PriorityQueue<Vertext> queue = null;

			Dictionary<Vertex,Vertext> prev = new Dictionary<Vertex,Vertext>();
			Dictionary<Vertext,double> minDistance = new Dictionary<Vertext,double>();
			HashSet<Vertext> visited = new HashSet<Vertext>();

			// Fill queue with all states.
			// initial state is 0, other is max
			// (alternative version of this only inserts into queue after neighbor is visited)
			foreach(var o in allNodes ){
				double distance = (o==startNode) ? 0 : double.MaxValue; 
				minDistance[o] = distance;
				queue.Add( o, distance );
			}
			
			while( !queue.IsEmpty ){
				Vertex current = queue.PopSmallest();
				visited.Add( current );
				foreach(var neighbor in neighborhood.GetNeighbors( current ) ){
				
					if( visited.Contains( neighbor ) ){ continue; }
					
					double alt = minDistance[current] + neighborhood.DistanceBetween( neighbor, current );
					if( alt < minDistance[ neighbor ] ){
						minDistance[ neighbor ] = alt;
						prev[ neighbor ] = current;
						queue.Adjust( neighbor, alt );
					}
					
				}// foreach neighbor
				
			}// while queue has items
		
		}
		
	}

	class Vertext{}

	interface NeighborProvider<T> {
		T[] GetNeighbors(T current);
		double DistanceBetween(T neighbor0, T neighbor1 );
	}


	interface PriorityQueue<T> {
		void Add(T t, double priority);
		void Adjust(T t, double priority);
		bool IsEmpty{ get; }
		T PopSmallest();
	}

}

*/

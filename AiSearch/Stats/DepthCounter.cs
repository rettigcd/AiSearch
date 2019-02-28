using System.Collections.Generic;
using System.Linq;
using AiSearch.OneSide;

namespace AiSearch {

	/// <summary>
	/// Tracks the depth of nodes returned by a search iterator.  
	/// </summary>
	public class DepthCounter<T> {
		
		public DepthCounter(){
			_depthCounts = new Dictionary<int, int>();
			// !! seems like it would be easier to switch to a List<int>
		}

		/// <summary>
		/// Logs the result nodes depth.
		/// </summary>
		/// <example>
		/// called via .ForEach( counter.Track ) to log a new result node dept.
		/// </example>
		public void Track( Node<T> node ){
			_totalCount++; 
			int depth = node.Depth;
			if( _depthCounts.ContainsKey(depth) ){ 
				_depthCounts[depth]++; 
			} else { 
				_depthCounts.Add(depth,1); 
				Deepest = node;
			}
		}

		/// <summary>
		/// Gets the # of nodes returned at each level.
		/// </summary>
		public int[] LevelCounts(){
			return _depthCounts
				.OrderBy(p=>p.Key)
				.Select(p=>p.Value)
				.ToArray();
		}
		
		public void Write( System.IO.TextWriter writer ){
			int[] levelCounts = this.LevelCounts();
			for(int i=0;i<levelCounts.Length;++i)
				writer.WriteLine(string.Format("{0}: {1}",i,levelCounts[i]));
			writer.WriteLine("Total Count: "+_totalCount);
		}
		
		readonly Dictionary<int,int> _depthCounts;
		int _totalCount;
		
		/// <summary> Gets the first node to reach the deepest level </summary>
		public Node<T> Deepest{ get; private set; }

		/// <summary> Shortcut to Deepest.Depth </summary>
		public int MaxDepth{ get{ return this.Deepest.Depth; } }

		/// <summary> Gets the first node to reach the deepest level </summary>
		public Node<T> Deepest2{ get; private set; }

	}
	
}

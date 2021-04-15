using System;
using System.Linq;

namespace AiSearch.Adversary {

	/// <summary>
	/// Associates an integer score with a GameState Node.  
	/// Used primarily by Negamax.
	/// </summary>
	public class NodeWithScore<T> where T : IAdversaryGs {
	
		public Node[] GetNodePath() => Node.GetNodePath();

		public Node Node { get; set; }
		public int Score { get; set; }

		public void ShowGame(){
			Console.WriteLine( "=======================" );
			Console.WriteLine( GetGame( this.Node ) );
			Console.WriteLine( this.Score );
		}

		static string GetGame( Node node ) {
			return node.GetNodePath()
				.Select( n => n.MoveToGetHere + " : " +n.State.ToString() )
				.Join("\r\n");
		}	

		#region static NegaMax stuff
		
		static NodeWithScore(){
			MinValue = new NodeWithScore<T>{ Score = int.MinValue };
			MaxValue = new NodeWithScore<T>{ Score = int.MaxValue };
		}
		/// <summary> Lower bounding default-result used by NegaMax </summary>
		public static readonly NodeWithScore<T> MinValue;
		/// <summary> Upper bounding default-result used by NegaMax </summary>
		public static readonly NodeWithScore<T> MaxValue;

		#endregion

		#region static operators

		static public NodeWithScore<T> operator-( NodeWithScore<T> n ){
			return new NodeWithScore<T>{ Node = n.Node, Score = -n.Score };
		}
		
		static public NodeWithScore<T> Max(NodeWithScore<T> n1, NodeWithScore<T> n2){
			return n1.Score >= n2.Score ? n1 : n2;
		}

		#endregion
		
	}


}

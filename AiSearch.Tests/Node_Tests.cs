using AiSearch.OneSide;
using System;
using System.Linq;
using Xunit;

namespace AiSearch.Tests {

	public class Node_Tests {

		class StringMove : IMove<string>{
			public StringMove(string h) { HumanReadable = h; }
			public string HumanReadable { get; set; }
			public string GenerateChild( string state ) { throw new NotImplementedException(); }
		}

		[Fact]
		public void GetNodePath_OrdersNodesFromRootToLeaf() {
			var n1 = new Node<string>("A",new StringMove("A"));
			var n2 = new Node<string>("B",new StringMove("B"),n1);
			var n3 = new Node<string>("C",new StringMove("C"),n2);
			Assert.Equal( "ABC", n3.GetNodePath().Select(n=>n.State).Join("") );
		}
	}

}

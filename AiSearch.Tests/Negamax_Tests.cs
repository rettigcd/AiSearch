using AiSearch.Adversary;
using Xunit;


namespace AiSearch.Tests {

	public class Negamax_Tests 	{
		readonly TestNode _root;

		public Negamax_Tests(){
			// http://en.wikipedia.org/wiki/Alpha%E2%80%93beta_pruning
			_root = new TestNode("6a1",true
				,new TestNode("3b1"
					,new TestNode("5c1"
						,new TestNode("5d0"
							,new TestNode("5e1")
							,new TestNode("6e2")
						)
						,new TestNode("4d1"
							,new TestNode("7e3")
							,new TestNode("4e4")
							,new TestNode("5e5")
						)
					)
					,new TestNode("3c2"
						,new TestNode("3d2"
							,new TestNode("3e6")
						)
					)
				)
				,new TestNode("6b2"
					,new TestNode("6c3"
						,new TestNode("6d3"
							,new TestNode("6e7")
						)
						,new TestNode("6d4"
							,new TestNode("6e8")
							,new TestNode("9e9")
						)
					)
					,new TestNode("7c4"
						,new TestNode("7d5"
							,new TestNode("7e0")
						)
					)
				)
				,new TestNode("5b3"
					,new TestNode("5c5"
						,new TestNode("5d6"
							,new TestNode("5eA")
						)
					)
					,new TestNode("8c6"
						,new TestNode("8d7"
							,new TestNode("9eB")
							,new TestNode("8eC")
						)
						,new TestNode("6d8"
							,new TestNode("6eD")
						)
					)
				)
			);
		}

		[Fact]
		public void CanEval(){

			// http://en.wikipedia.org/wiki/Alpha%E2%80%93beta_pruning		

			var negamax = new Negamax<TestNode>(5, TestNodeHeuristic);
			int result = negamax.Eval( _root ).Score;
			Assert.Equal( 6, result );
			Assert.Equal( 25, negamax.NodeEvaluationCount );
		}

		int TestNodeHeuristic(TestNode s){
			var testNode = (TestNode)s;
			return int.Parse( testNode.Name.Substring(0,1) );
		}

	}

}

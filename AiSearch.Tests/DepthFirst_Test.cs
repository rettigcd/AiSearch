using System.Linq;
using AiSearch.OneSide;
using Xunit;

namespace AiSearch.Tests {

	public class DepthFirst_Test {

		public DepthFirst_Test() {

			_startState = new BreadthTestState( "A"
				,new BreadthTestState("B"
					,new BreadthTestState("C")
					,new BreadthTestState("D"
						,new BreadthTestState("E"
							,new BreadthTestState("F")
							,new BreadthTestState("G")
							,new BreadthTestState("H")
						)
						,new BreadthTestState("I"
							,new BreadthTestState("J")
						)
					)
				)
				,new BreadthTestState("K"
					,new BreadthTestState("L"
						,new BreadthTestState("M")
						,new BreadthTestState("N")
					)
					,new BreadthTestState("O"
						,new BreadthTestState("P")
						,new BreadthTestState("Q") 
					)
				)
			);

		}

		readonly BreadthTestState _startState;
		
		[Theory]
		[InlineData(0,"A")]
		[InlineData(1,"ABK")]
		[InlineData(2,"ABCDKLO")]
		[InlineData(3,"ABCDEIKLMNOPQ")]
		[InlineData(4,"ABCDEFGHIJKLMNOPQ")]
		[InlineData(10000,"ABCDEFGHIJKLMNOPQ")]
		public void DepthLimitsNodesReturned(int depth,string expected){
			this.Depth( _startState, depth, expected );
		}

		[Fact]
		public void GuardAncestors(){
			
			// Scenario: Make sure depth first can filter out ancestors to reduce search space
			
			// Given: tree where generate children creates an ancestor node
			var startState = new BreadthTestState( "A"
				,new BreadthTestState("B"
					,new BreadthTestState("A"		 // ancestor NODE! matches root node
						,new BreadthTestState("E")	// with children (but different so we can see if it does something funny
						,new BreadthTestState("F")  // neither 'A' nor its new children should appear.
					)
					,new BreadthTestState("C")
				)
				,new BreadthTestState("D")
			);
			
			// When: we iterate over it
			// Then: the deeper ancestor node is ignored
			this.Depth( startState, int.MaxValue,"ABCD" );
		}
		
		#region private

		void Depth( BreadthTestState startState, int depth, string expected ){

			DepthFirstIterator<BreadthTestState> iterator = new DepthFirstIterator<BreadthTestState>( new XMoveGenerator(), depth ) { DontRepeat = true };

			// When: we search
			var moves = iterator
				.Iterate( startState )
				.Select(n=>n.State.Name)
				.Join("");
			
			Assert.Equal( expected, moves );
		}
	

		#endregion
		
	}
	
}

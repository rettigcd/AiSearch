using System.Linq;
using AiSearch.OneSide;
using Xunit;

namespace AiSearch.Tests {

	public class BreadthFirst_Test {

		public BreadthFirst_Test() {
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
		
		
		[Fact]public void Depth0(){ this.Depth(0,"A"); }
		[Fact]public void Depth1(){ this.Depth(1,"ABK"); }
		[Fact]public void Depth2(){ this.Depth(2,"ABKCDLO"); }
		[Fact]public void Depth3(){ this.Depth(3,"ABKCDLOEIMNPQ"); }
		[Fact]public void Depth4(){ this.Depth(4,"ABKCDLOEIMNPQFGHJ"); }
		[Fact]public void Depth_Beyond(){ this.Depth(int.MaxValue,"ABKCDLOEIMNPQFGHJ"); }

		[Fact]
		public void GuardPreviousDepths(){
			
			// Scenario: Make sure breadth first can filter out nodes from previous depths to reduce search space
			// even if it is not an ancesstor
			
			// Given: tree where generate children creates an ancestor node
			var startState = new BreadthTestState( "A"
				,new BreadthTestState("B"
					,new BreadthTestState("J")
					,new BreadthTestState("D"		 // ancestor NODE! matches root node
						,new BreadthTestState("E")	// with children (but different so we can see if it does something funny
						,new BreadthTestState("F")  // neither 'A' nor its new children should appear.
					)
					,new BreadthTestState("C")
				)
				,new BreadthTestState("D")
			);
			
			// When: we iterate over it
			// Then: the deeper ancestor node is ignored
			this.Depth( startState, int.MaxValue,"ABDJC" );
		}
		
		#region private
		
		void Depth( BreadthTestState startState, int depth, string expected ){
			BreadthFirstIterator<BreadthTestState> iterator = new BreadthFirstIterator<BreadthTestState>( new XMoveGenerator(), depth ) { DontRepeat = true };
			string moves = iterator.Iterate( startState )
				.Select(n=>n.State.Name)
				.Join("");
			
			Assert.Equal( expected, moves );
		}
		
		void Depth(int depth, string expected){
			this.Depth( _startState, depth, expected );
		}

		#endregion
		
	}
	
}

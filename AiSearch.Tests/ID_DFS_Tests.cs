//using System.Linq;
//using NUnit.Framework;

//namespace AiSearch.Tests {

//	[TestFixture]
//	public class ID_DFS_Test {

//		[TestFixtureSetUp]
//		public void Setup(){
			
//			_startState = new BreadthTestState( "A"
//				,new BreadthTestState("B"
//					,new BreadthTestState("C")
//					,new BreadthTestState("D"
//						,new BreadthTestState("E"
//							,new BreadthTestState("F")
//							,new BreadthTestState("G")
//							,new BreadthTestState("H")
//						)
//						,new BreadthTestState("I"
//							,new BreadthTestState("J")
//						)
//					)
//				)
//				,new BreadthTestState("K"
//					,new BreadthTestState("L"
//						,new BreadthTestState("M")
//						,new BreadthTestState("N")
//					)
//					,new BreadthTestState("O"
//						,new BreadthTestState("P")
//						,new BreadthTestState("Q") 
//					)
//				)
//			);
			
//		}
		
//		BreadthTestState _startState;
		
		
//		[Test]public void Depth0(){ this.Depth(0,"A"); }
//		[Test]public void Depth1(){ this.Depth(1,"ABK"); }
//		[Test]public void Depth2(){ this.Depth(2,"ABKCDLO"); }
//		[Test]public void Depth3(){ this.Depth(3,"ABKCDLOEIMNPQ"); }
//		[Test]public void Depth4(){ this.Depth(4,"ABKCDLOEIMNPQFGHJ"); }
//		[Test]public void Depth_Beyond(){ this.Depth(int.MaxValue,"ABKCDLOEIMNPQFGHJ"); }

//		[Test]
//		public void DoesNotGuardPreviousDepths(){
			
//			// Scenario: this isn't really a requirement but highlighting the face that iterative depening will suffer
//			// the same problems as DFS.
			
//			// Note, we might want to be able to turn this off for gamestates that can't generate ancestors.
			
//			// Given: tree where generate children creates an ancestor node
//			var startState = new BreadthTestState( "A"
//				,new BreadthTestState("B"
//					,new BreadthTestState("J")
//					,new BreadthTestState("D"		 // ancestor NODE! matches root node
//						,new BreadthTestState("E")	// with children (but different so we can see if it does something funny
//						,new BreadthTestState("F")  // neither 'A' nor its new children should appear.
//					)
//					,new BreadthTestState("C")
//				)
//				,new BreadthTestState("D")
//			);
			
//			// When: we iterate over it
//			// Then: the deeper ancestor node is ignored
//			this.Depth( startState, int.MaxValue,"ABDJDCEF" );
//		}
		
//		#region private		
		
//		void Depth( BreadthTestState startState, int depth, string expected ){
//			// When: we search
//			string moves = IterativeDepening.Iterator( new Node2( startState, new BreadthTestMove("A") ), startState.MoveGenerator, depth )
//				.Select(n=>n.MoveToGetHere)
//				.Join("");
			
//			Assert.That( moves, Is.EqualTo(expected) );
//		}
		
//		void Depth(int depth, string expected){
//			this.Depth( _startState, depth, expected );
//		}

//		#endregion
		
//	}
	
//}

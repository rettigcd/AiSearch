using System;
using System.Collections.Generic;
using System.Linq;
//using AiSearch.OneSide;
using AiSearch.Adversary;
using AiSearch.OneSide;

namespace AiSearch.Tests {





	public class BreadthTestMove : IMove<BreadthTestState> {
		public BreadthTestMove( string move ) { HumanReadable = move; }
		public string HumanReadable { get; set; }

		public BreadthTestState GenerateChild( BreadthTestState state ) {
			return state.GenerateChild(this);
		}
	}



}

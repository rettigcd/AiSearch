﻿using System;
using System.Collections.Generic;
using System.Linq;
using AiSearch.OneSide;


namespace AiSearch.Tests {

	class XMoveGenerator : MoveGenerator<BreadthTestState> {

		public IEnumerable<Move<BreadthTestState>> GetMoves( BreadthTestState s ) {
			BreadthTestState gameState = (BreadthTestState)s;
			return gameState.Children
				.Select(child => new BreadthTestMove( child.Name ) );
		}
	}

	public class BreadthTestState {

		// Sets the player to move and alternates every child move until a child is set
		public BreadthTestState(string name, params BreadthTestState[] children){
			this.Name = name;
			this.Children = children;
		}

		public string Name{ get; set; }
		public BreadthTestState[] Children{ get; set; }

		#region Equals and GetHashCode implementation

		public override int GetHashCode()	{
			return this.Name.GetHashCode();
		}
		
		public override bool Equals(object obj) {
			BreadthTestState other = obj as BreadthTestState;
			return !Object.ReferenceEquals(other,null)
				&& this.Name == other.Name;
		}

		public BreadthTestState GenerateChild( Move<BreadthTestState> move ) {
			return this.Children.First(child=>child.Name == move.HumanReadable );
		}
		
		#endregion

	}



}
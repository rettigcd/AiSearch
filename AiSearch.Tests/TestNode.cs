using System;
using System.Collections.Generic;
using AiSearch.Adversary;

namespace AiSearch.Tests {

	class TestNode : AdversaryGs {

		// Sets the player to move and alternates every child move until a child is set
		public TestNode(string name, bool positivePlayerToMove, params TestNode[] children){
			this.Name = name;
			this.MoveToGetHere = name;
			this.Children = children;
			SetAlternatingPlayerToMove( positivePlayerToMove );
		}

		void SetAlternatingPlayerToMove(bool playerToMove){
			// if node was already set, stop iterating down thru tree
			if( _posPlayerToMove.HasValue ){ return; }

			// set self
			_posPlayerToMove = playerToMove;
			
			// set children
			foreach(var child in this.Children){
				child.SetAlternatingPlayerToMove( !playerToMove );
			}
		}

		public TestNode(string name, params TestNode[] children){
			this.Name = name;
			this.MoveToGetHere = name;
			this.Children = children;
		}

		public string Name{ get; set; }
		public TestNode[] Children{ get; set; }
		
		public string MoveToGetHere{ get; set; }
		
		public IEnumerable<AdversaryGs> GenerateChildren(){
			return this.Children;
		}

		#region Equals and GetHashCode implementation
		public override int GetHashCode()	{ return this.Name.GetHashCode(); }
		
		public override bool Equals(object obj) {
			TestNode other = obj as TestNode;
			if (other == null) { return false; }
			return this.MoveToGetHere == other.MoveToGetHere;
		}
		
		public static bool operator ==(TestNode lhs, TestNode rhs) {
			if (ReferenceEquals(lhs, rhs))
				return true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return false;
			return lhs.Equals(rhs);
		}
		
		public static bool operator !=(TestNode lhs, TestNode rhs){
			return !(lhs == rhs);
		}
		
		#endregion

		
		public bool PositivePlayerToMove { 
			get{
				if( _posPlayerToMove.HasValue ){ 
					return _posPlayerToMove.Value; 
				}
				throw new Exception("Player to move has not been set for this record: " + this.Name ); 
			}
		}
		bool? _posPlayerToMove;
	}



}

using System;
using System.Linq;

namespace AiSearch.Adversary {

	/// <summary>
	/// Allows testing for children without having to generate them twice.
	/// Defers generating them until we need them so we don't generate unnessarily.
	/// </summary>
	/// <remarks>
	/// Used by NegaMax
	/// </remarks>
	public class ChildCacher {

		public ChildCacher(Node node){
			_node = node;
		}

		public Node[] LazyChildren{
			get{
				if( _cachedChildren == null ){
					try{
						_cachedChildren = this._node.GenerateChildren( _node.State.GenerateChildren() )
							.ToArray();
					}
					catch(Exception ex){
						throw new SearchException("Unable to generate children for " + this._node.State.ToString(), ex, this._node );
					}
				}
				return _cachedChildren;
			}
		}
		
		Node[] _cachedChildren;
		Node _node;
	}
	
}

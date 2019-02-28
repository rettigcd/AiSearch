using System;
using System.Collections.Generic;

namespace AiSearch {
	
	/// <summary>
	/// Extends IEnumerable to iterate through items for only limited time.
	/// </summary>
	public static class TimeLimitExtension {
		
		static public IEnumerable<T> TimeLimit<T>(this IEnumerable<T> items, TimeSpan maxDuration) {
			DateTime timeout = DateTime.Now + maxDuration;
			foreach(var item in items){
				yield return item;
				if( DateTime.Now > timeout ){ break; }
			}
		}
		
	}
}

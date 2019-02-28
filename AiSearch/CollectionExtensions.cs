using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AiSearch {


	public static class ExtendCollections {
	
		// handy for tracking statistics
		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> src, Action<T> watch){
			foreach(var t in src){
				watch(t);
				yield return t;
			}
		}
		
		public static string Join(this IEnumerable items, string glue){
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			bool first = true;
			foreach(object o in items){
				if( first ){ first = false; } else { builder.Append(glue); }
				builder.Append(o.ToString());
			}
			return builder.ToString();
		}

		public static T[] Shuffle<T>(this IEnumerable<T> src){
			T[] arr = src.ToArray();
			var rand = new Random();
			for(int i=arr.Length-1; i>1; --i){
				int other = rand.Next(i-1);
				T temp = arr[i];
				arr[i] = arr[other];
				arr[other] = temp;
			}
			return arr;
		}

	}


}

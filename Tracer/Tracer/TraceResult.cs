using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
	public class TraceResult
	{
		public ConcurrentDictionary<int, object> dictionary;

		internal TraceResult()
		{
			dictionary = new ConcurrentDictionary<int, object>();
		}

		internal void Start(int id,object method)
		{

		}
	}
}

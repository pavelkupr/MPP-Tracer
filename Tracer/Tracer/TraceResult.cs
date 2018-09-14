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
		internal ConcurrentDictionary<int, ThreadTracer> dictionary;

		internal TraceResult()
		{
			dictionary = new ConcurrentDictionary<int, ThreadTracer>();
		}

		internal void Start(int id,object method)
		{
			ThreadTracer threadTracer = dictionary.GetOrAdd(id, new ThreadTracer());
		}
	}
}

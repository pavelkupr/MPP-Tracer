using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Trace
{
	public class TraceResult
	{
		private readonly ConcurrentDictionary<int, ThreadTracer> dictionary;
		internal IEnumerable<KeyValuePair<int, ThreadTracer>> Dictionary { get { return dictionary; } }

		internal TraceResult(ConcurrentDictionary<int, ThreadTracer> curr_dictionary)
		{
			dictionary = curr_dictionary;
		}

		internal void Start(int id,MethodBase method)
		{
			ThreadTracer threadTracer = dictionary.GetOrAdd(id, new ThreadTracer());
			threadTracer.AddMethod(method);
		}

		internal void Stop(int id)
		{
			ThreadTracer threadTracer;
			if(!dictionary.TryGetValue(id,out threadTracer))
			{
				throw new Exception("Can't get value");
			}
			threadTracer.Stop();
		}
	}
}

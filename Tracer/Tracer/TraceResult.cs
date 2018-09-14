using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Tracer
{
	public class TraceResult
	{
		internal ConcurrentDictionary<int, ThreadTracer> dictionary;

		internal TraceResult()
		{
			dictionary = new ConcurrentDictionary<int, ThreadTracer>();
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

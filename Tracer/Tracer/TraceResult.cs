using System.Collections.Concurrent;
using System.Reflection;
using System.Diagnostics;

namespace Tracer
{
	public class TraceResult
	{
		internal ConcurrentDictionary<int, ThreadTracer> dictionary;

		private ConcurrentDictionary<MethodInfo, Stopwatch> timerDictionary;

		internal TraceResult(ConcurrentDictionary<MethodInfo, Stopwatch> tDictionary)
		{
			dictionary = new ConcurrentDictionary<int, ThreadTracer>();
			timerDictionary = tDictionary;
		}

		internal void Start(int id,MethodBase method)
		{
			ThreadTracer threadTracer = dictionary.GetOrAdd(id, new ThreadTracer(timerDictionary));
			threadTracer.AddMethod(method);
		}
	}
}

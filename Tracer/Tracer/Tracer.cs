using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace Tracer
{
	public class Tracer : ITracer
	{

		private StackTrace stackTrace;
		private StackFrame stackFrame;
		private TraceResult trResult;
		private ConcurrentDictionary<MethodInfo, Stopwatch> timerDictionary;

		public Tracer()
		{
			timerDictionary = new ConcurrentDictionary<MethodInfo, Stopwatch>();
			trResult = new TraceResult(timerDictionary);
		}

		public void StartTrace()
		{
			stackTrace = new StackTrace();
			stackFrame = stackTrace.GetFrame(1);
			trResult.Start(Thread.CurrentThread.ManagedThreadId, stackFrame.GetMethod());
		}

		public void Stoptrace()
		{
			throw new NotImplementedException();
		}

		public TraceResult GetTraceResult()
		{
			return trResult;
		}
	}
}

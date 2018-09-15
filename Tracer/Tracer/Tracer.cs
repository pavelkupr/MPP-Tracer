using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace Trace
{
	public class Tracer : ITracer
	{
		private readonly ConcurrentDictionary<int, ThreadTracer> dictionary;
		private StackTrace stackTrace;
		private StackFrame stackFrame;
		private TraceResult trResult;

		public Tracer()
		{
			dictionary = new ConcurrentDictionary<int, ThreadTracer>();
			trResult = new TraceResult(dictionary);
		}

		public void StartTrace()
		{
			StopThreadTimers();
			stackTrace = new StackTrace();
			stackFrame = stackTrace.GetFrame(1);
			trResult.Start(Thread.CurrentThread.ManagedThreadId, stackFrame.GetMethod());
			StartThreadTimers();
		}

		public void StopTrace()
		{
			trResult.Stop(Thread.CurrentThread.ManagedThreadId);
		}

		public TraceResult GetTraceResult()
		{
			return trResult;
		}

		private void StartThreadTimers()
		{
			int id = Thread.CurrentThread.ManagedThreadId;
			if (dictionary.ContainsKey(id))
			{
				if (!dictionary.TryGetValue(id, out ThreadTracer threadTracer))
					throw new Exception("Can't get value");

				foreach (MethodInfo methodInfo in threadTracer.MethodList)
					if(methodInfo.IsTracing)
						methodInfo.Start();
			}
		}

		private void StopThreadTimers()
		{
			int id = Thread.CurrentThread.ManagedThreadId;
			if (dictionary.ContainsKey(id))
			{
				if (!dictionary.TryGetValue(id, out ThreadTracer threadTracer))
					throw new Exception("Can't get value");

				foreach (MethodInfo methodInfo in threadTracer.MethodList)
					if (methodInfo.IsTracing)
						methodInfo.Stop();
			}
		}
	}
}

using System;
using System.Diagnostics;
using System.Threading;

namespace Tracer
{
	public class Tracer : ITracer
	{

		private StackTrace stackTrace;
		private StackFrame stackFrame;
		private TraceResult trResult;

		public Tracer()
		{
			trResult = new TraceResult();
		}

		public void StartTrace()
		{
			StopThreadTimers();
			stackTrace = new StackTrace();
			stackFrame = stackTrace.GetFrame(1);
			trResult.Start(Thread.CurrentThread.ManagedThreadId, stackFrame.GetMethod());
			StartThreadTimers();
		}

		public void Stoptrace()
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
			if (trResult.dictionary.ContainsKey(id))
			{
				if (!trResult.dictionary.TryGetValue(id, out ThreadTracer threadTracer))
					throw new Exception("Can't get value");

				foreach (MethodInfo methodInfo in threadTracer.methodList)
					if(methodInfo.IsTracing)
						methodInfo.Start();
			}
		}

		private void StopThreadTimers()
		{
			int id = Thread.CurrentThread.ManagedThreadId;
			if (trResult.dictionary.ContainsKey(id))
			{
				if (!trResult.dictionary.TryGetValue(id, out ThreadTracer threadTracer))
					throw new Exception("Can't get value");

				foreach (MethodInfo methodInfo in threadTracer.methodList)
					if (methodInfo.IsTracing)
						methodInfo.Stop();
			}
		}
	}
}

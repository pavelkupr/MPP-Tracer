using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

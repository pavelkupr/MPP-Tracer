using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trace;
using System.Diagnostics;
using System.Threading;

namespace TraceTests
{
	[TestClass]
	public class TracerTests
	{
		private static Stopwatch watcher;
		private static ITracer tracer;

		[ClassInitialize]
		public static void ClassInitilize(TestContext context)
		{
			watcher = new Stopwatch();
		}

		[TestInitialize]
		public void TestInitilaze()
		{
			tracer = new Tracer();
			watcher.Reset();
		}

		[TestMethod]
		public void StartTrace_In_Less_3_ms()
		{
			const double expectedMax = 3;
			
			watcher.Start();
			tracer.StartTrace();
			watcher.Stop();
			Debug.WriteLine("Time: " + watcher.Elapsed.TotalMilliseconds);

			if (watcher.Elapsed.TotalMilliseconds > expectedMax)
				Assert.Fail("The method runs in more 3 ms");
		}

		[TestMethod]
		public void StopTrace_In_Less_1_ms()
		{
			const double expectedMax = 1;
			tracer.StartTrace();

			watcher.Start();
			tracer.StopTrace();
			watcher.Stop();
			Debug.WriteLine("Time: " + watcher.Elapsed.TotalMilliseconds);

			if (watcher.Elapsed.TotalMilliseconds > expectedMax)
				Assert.Fail("The method runs in more 1 ms");
		}

	}
}

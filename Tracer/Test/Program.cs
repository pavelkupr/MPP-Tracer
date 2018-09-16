using System;
using System.Threading;
using Trace;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Tracer tracer = new Tracer();
			Test(tracer);
			Test2(tracer);
			ResultWriter resultWriter = new ResultWriter();
			resultWriter.ConsolePrint(tracer.GetTraceResult());
			resultWriter.FilePrint(tracer.GetTraceResult(), "test");
			Console.ReadLine();
		}

		static void Test(Tracer tracer)
		{
			tracer.StartTrace();
			Test2(tracer);
			Thread.Sleep(20);
			tracer.StopTrace();
		}
		static void Test2(Tracer tracer)
		{
			tracer.StartTrace();
			Test3(tracer);
			Thread.Sleep(20);
			tracer.StopTrace();
		}
		static void Test3(Tracer tracer)
		{
			tracer.StartTrace();
			Thread.Sleep(40);
			tracer.StopTrace();
		}
	}
}

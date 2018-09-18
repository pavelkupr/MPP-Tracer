using System;
using System.Threading;
using Trace;
using System.Diagnostics;
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
			XmlSerializer xmlSerializer = new XmlSerializer();
			JsonSerializer jsonSerializer = new JsonSerializer();
			resultWriter.ConsolePrint(tracer.GetTraceResult());
			resultWriter.FilePrint(tracer.GetTraceResult(), "test");
			xmlSerializer.ResultInFile(tracer.GetTraceResult(),"result");
			jsonSerializer.ResultInFile(tracer.GetTraceResult(), "result");
			jsonSerializer.ResultInStream(tracer.GetTraceResult(), Console.OpenStandardOutput());
			Console.ReadLine();
			Stopwatch watcher = new Stopwatch();
			watcher.Start();
			tracer.StartTrace();
			//Thread.Sleep(100);
			watcher.Stop();
			Console.WriteLine(watcher.Elapsed.TotalMilliseconds);
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

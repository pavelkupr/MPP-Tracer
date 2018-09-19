using System;
using System.Threading;
using System.Collections.Generic;
using Trace;

namespace Test
{
	class Program
	{
		private static ITracer tracer = new Tracer();

		static void Main(string[] args)
		{
			//tracer.StartTrace();
			List<Thread> threads = new List<Thread>();
			TestClass1 testClass1 = new TestClass1(tracer);
			testClass1.Test3();
			testClass1.Test2();
			testClass1.Test1();
			for (int i = 0; i < 3; i++)
			{
				Thread thread = null;
				switch (i)
				{
					case 0:
						thread = new Thread(testClass1.Test1);
						break;

					case 1:
						thread = new Thread(testClass1.Test2);
						break;

					case 2:
						thread = new Thread(testClass1.Test3);
						break;
				}
				threads.Add(thread);

				thread.Start();
			}
			
			foreach (Thread thread in threads)
			{
				thread.Join();
			}

			//tracer.StopTrace();

			WriteResult();
			Console.ReadLine();
		}

		static void WriteResult()
		{
			ResultWriter resultWriter = new ResultWriter();
			XmlSerializer xmlSerializer = new XmlSerializer();
			JsonSerializer jsonSerializer = new JsonSerializer();
			TxtWriter txtWriter = new TxtWriter();
			TraceResult result = tracer.GetTraceResult();

			resultWriter.ResultInConsole(txtWriter, result);
			resultWriter.ResultInFile(txtWriter, result, "result");
			resultWriter.ResultInFile(jsonSerializer, result, "result");
			resultWriter.ResultInFile(xmlSerializer, result, "result");
		}

	}
}

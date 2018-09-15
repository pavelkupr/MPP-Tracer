using System;
using System.Threading;
using Trace;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Tracer tracer = new Tracer();
			Test(tracer);
			Console.ReadLine();
		}

		static void Test(Tracer tracer)
		{
			tracer.StartTrace();
			Thread.Sleep(20);
			tracer.StopTrace();
		}
	}
}

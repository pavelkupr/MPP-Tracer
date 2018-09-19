using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Trace;

namespace Test
{
	public class TestClass1
	{
		private TestClass2 testClass2;
		ITracer tracer;

		internal TestClass1(ITracer currTracer)
		{
			tracer = currTracer;
			testClass2 = new TestClass2(tracer);
		}

		internal void Test1()
		{
			tracer.StartTrace();
			for (int i = 0; i < 5; i++)
			{
				testClass2.TestEx1(i);
			}
			Thread.Sleep(20);
			tracer.StopTrace();
		}

		internal void Test2()
		{
			tracer.StartTrace();
			Thread thread = new Thread(testClass2.TestEx2);
			thread.Start();
			thread.Join();
			tracer.StopTrace();
		}

		internal void Test3()
		{
			tracer.StartTrace();
			Thread.Sleep(230);
			tracer.StopTrace();
		}
	}

	public class TestClass2
	{
		private ITracer tracer;

		internal TestClass2(ITracer currTracer)
		{
			tracer = currTracer;
		}

		internal void TestEx1(int i)
		{
			tracer.StartTrace();
			if (i != 0)
				TestEx1(--i);
			Thread.Sleep(40);
			tracer.StopTrace();
		}

		internal void TestEx2()
		{
			tracer.StartTrace();
			TestEx1(2);
			Thread.Sleep(40);
			tracer.StopTrace();
		}
	}
}
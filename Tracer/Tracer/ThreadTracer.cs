using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;

namespace Tracer
{
	internal class ThreadTracer
	{
		internal List<MethodInfo> methodList;
		private ConcurrentDictionary<MethodInfo, Stopwatch> timerDictionary;
		private Stack<MethodInfo> methodStack;

		internal ThreadTracer(ConcurrentDictionary<MethodInfo, Stopwatch> tDictionary)
		{
			methodList = new List<MethodInfo>();
			methodStack = new Stack<MethodInfo>();
			timerDictionary = tDictionary;
		}

		internal void AddMethod(MethodBase method)
		{
			MethodInfo currMethodInfo = new MethodInfo(method);
			Stopwatch stopwatch = currMethodInfo.GetTimer();
			methodList.Add(currMethodInfo);
			timerDictionary.GetOrAdd(currMethodInfo, stopwatch);
			methodStack.Push(currMethodInfo);
		}

		internal void Stop()
		{
			MethodInfo currMethodInfo = methodStack.Pop();
			currMethodInfo.WriteResult();
			while(!timerDictionary.TryRemove(currMethodInfo, out Stopwatch value))
			{
				Console.WriteLine("Can't remove timer");
			}
		}
	}
}

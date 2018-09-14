using System.Collections.Generic;
using System.Reflection;

namespace Tracer
{
	internal class ThreadTracer
	{
		internal List<MethodInfo> methodList;
		private Stack<MethodInfo> methodStack;

		internal ThreadTracer()
		{
			methodList = new List<MethodInfo>();
			methodStack = new Stack<MethodInfo>();
		}

		internal void AddMethod(MethodBase method)
		{
			MethodInfo currMethodInfo = new MethodInfo(method);
			methodList.Add(currMethodInfo);
			methodStack.Push(currMethodInfo);
		}

		internal void Stop()
		{
			MethodInfo currMethodInfo = methodStack.Pop();
			currMethodInfo.Stop();
		}
	}
}

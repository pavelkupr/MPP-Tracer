using System.Collections.Generic;
using System.Reflection;

namespace Trace
{
	internal class ThreadTracer
	{
		private readonly List<MethodInfo> methodList;
		private readonly Stack<MethodInfo> methodStack;
		internal IEnumerable<MethodInfo> MethodList { get { return methodList; } }

		internal ThreadTracer()
		{
			methodList = new List<MethodInfo>();
			methodStack = new Stack<MethodInfo>();
		}

		internal void AddMethod(MethodBase method)
		{
			MethodInfo currMethodInfo = new MethodInfo(method);
			methodList.Add(currMethodInfo);
			if (methodStack.Count != 0)
				methodStack.Peek().AddChild(currMethodInfo);
			methodStack.Push(currMethodInfo);
		}

		internal void Stop()
		{
			MethodInfo currMethodInfo = methodStack.Pop();
			currMethodInfo.EndTrace();
		}
	}
}

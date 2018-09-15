using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;

namespace Trace
{
	internal class MethodInfo
	{
		private readonly List<MethodInfo> children;
		private Stopwatch timer;
		private bool isTracing;
		internal IEnumerable<MethodInfo> Children { get { return children; } }
		internal string Name { get; }
		internal string ClassName { get; }
		internal long Time { get { return timer.ElapsedMilliseconds; } }
		internal bool IsTracing { get { return isTracing; } }

		internal MethodInfo(MethodBase method)
		{
			timer = new Stopwatch();
			children = new List<MethodInfo>();
			isTracing = true;
			Name = method.Name;
			ClassName = method.DeclaringType.ToString();
		}

		internal void EndTrace()
		{
			timer.Stop();
			isTracing = false;
		}

		internal void Start()
		{
			timer.Start();
		}

		internal void Stop()
		{
			timer.Stop();
		}

		internal void AddChild(MethodInfo methodInfo)
		{
			children.Add(methodInfo);
		}
	}
}

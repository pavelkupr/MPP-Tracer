using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
	internal class MethodInfo
	{
		internal string Name { get; }
		internal string ClassName { get; }
		internal long Time { get { return timer.ElapsedMilliseconds; } }
		internal bool IsTracing { get { return isTracing; } }
		private Stopwatch timer;
		private bool isTracing;

		internal MethodInfo(MethodBase method)
		{
			timer = new Stopwatch();
			isTracing = true;
			Name = method.Name;
			ClassName = method.DeclaringType.ToString();
		}

		internal void EndTrace()
		{
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
	}
}

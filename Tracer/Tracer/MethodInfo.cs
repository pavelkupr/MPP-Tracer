using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
	internal class MethodInfo
	{
		internal string name;
		internal string className;
		internal long time;

		private Stopwatch timer;

		internal MethodInfo(MethodBase method)
		{
			timer = new Stopwatch();
			name = method.Name;
			className = method.DeclaringType.ToString();
		}

		internal Stopwatch GetTimer()
		{
			return timer;
		}

		internal void WriteResult()
		{
			time = timer.ElapsedMilliseconds;
		}
	}
}

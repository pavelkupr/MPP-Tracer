using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
	internal class ThreadTracer
	{
		internal List<MethodInfo> methodList;
		private int inNum;
		
		internal ThreadTracer()
		{
			methodList = new List<MethodInfo>();
			inNum = 0;
		}

		internal void Start(object method)
		{

		}
	}
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Trace
{
	public class ResultWriter
	{
		public void ResultInFile(ISerializer serializer, TraceResult result, string fileName)
		{
			serializer.ResultInFile(result, fileName);
		}

		public void ResultInConsole(ISerializer serializer, TraceResult result)
		{
			serializer.ResultInStream(result, Console.OpenStandardOutput());
		}
	}
}

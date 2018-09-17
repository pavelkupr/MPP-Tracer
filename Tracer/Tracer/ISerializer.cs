using System.IO;

namespace Trace
{
	interface ISerializer
	{
		void ResultInFile(TraceResult traceResult, string fileName);

		void ResultInStream(TraceResult traceResult, Stream stream);
	}
}

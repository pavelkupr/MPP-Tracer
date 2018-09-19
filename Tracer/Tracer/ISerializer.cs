using System.IO;

namespace Trace
{
	public interface ISerializer
	{
		void ResultInFile(TraceResult traceResult, string fileName);

		void ResultInStream(TraceResult traceResult, Stream stream);
	}
}

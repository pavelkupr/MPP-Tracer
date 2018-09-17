
namespace Trace
{
	interface ITracer
	{
		void StartTrace();

		void StopTrace();

		TraceResult GetTraceResult();
	}
}

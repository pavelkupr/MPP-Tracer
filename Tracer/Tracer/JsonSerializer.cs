using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Trace
{
	public class JsonSerializer : ISerializer
	{
		public void ResultInFile(TraceResult traceResult, string fileName)
		{
			Stream stream = File.Create(fileName + ".json");
			byte[] text = Encoding.ASCII.GetBytes(SerializeResult(traceResult).ToString());
			stream.Write(text, 0, text.Length);
			stream.Close();
		}

		public void ResultInStream(TraceResult traceResult, Stream stream)
		{
			byte[] text = Encoding.ASCII.GetBytes(SerializeResult(traceResult).ToString());
			stream.Write(text, 0, text.Length);
		}

		private StringBuilder SerializeResult(TraceResult traceResult)
		{
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);
			JsonTextWriter jsonWriter = new JsonTextWriter(sw);

			jsonWriter.Formatting = Formatting.Indented;

			jsonWriter.WriteStartObject();
			jsonWriter.WritePropertyName("Threads");
			jsonWriter.WriteStartArray();
			foreach (KeyValuePair<int, ThreadTracer> thread in traceResult.Dictionary)
			{
				jsonWriter.WriteStartObject();
				jsonWriter.WritePropertyName("Id");
				jsonWriter.WriteValue(thread.Key);
				jsonWriter.WritePropertyName("Time");
				jsonWriter.WriteValue(thread.Value.ThreadTime);
				jsonWriter.WritePropertyName("Methods");
				jsonWriter.WriteStartArray();
				foreach (MethodInfo method in thread.Value.MethodList)
				{
					SerializeMethod(method, jsonWriter);
				}
				jsonWriter.WriteEnd();
				jsonWriter.WriteEndObject();
			}
			jsonWriter.WriteEnd();
			jsonWriter.WriteEndObject();
			return sb;
		}
		private void SerializeMethod(MethodInfo method, JsonTextWriter jsonWriter)
		{
			jsonWriter.WriteStartObject();
			jsonWriter.WritePropertyName("Class name");
			jsonWriter.WriteValue(method.ClassName);
			jsonWriter.WritePropertyName("Name");
			jsonWriter.WriteValue(method.Name);
			jsonWriter.WritePropertyName("Time");
			jsonWriter.WriteValue(method.Time);
			jsonWriter.WritePropertyName("Methods");
			jsonWriter.WriteStartArray();
			foreach (MethodInfo child in method.Children)
			{
				SerializeMethod(child, jsonWriter);
			}
			jsonWriter.WriteEnd();
			jsonWriter.WriteEndObject();
		}
	}
}

using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Trace
{
	public class XmlSerializer : ISerializer
	{
		public void ResultInFile(TraceResult traceResult, string fileName)
		{
			SerializeResult(traceResult).Save(fileName+".xml");
		}

		public void ResultInStream(TraceResult traceResult, Stream stream)
		{
			SerializeResult(traceResult).Save(stream);
		}

		private XDocument SerializeResult(TraceResult traceResult)
		{
			XDocument resultXML = new XDocument();
			XElement rootXML = new XElement("root");

			foreach (KeyValuePair<int, ThreadTracer> thread in traceResult.Dictionary)
			{
				XElement threadXML = new XElement("thread");
				threadXML.Add(new XAttribute("id", thread.Key));
				threadXML.Add(new XAttribute("time", thread.Value.ThreadTime));
				foreach (MethodInfo method in thread.Value.MethodList)
				{
					threadXML.Add(SerializeMethod(method));	
				}
				rootXML.Add(threadXML);
			}
			resultXML.Add(rootXML);
			return resultXML;
		}

		private XElement SerializeMethod(MethodInfo method)
		{
			XElement methodXML = new XElement("method");
			methodXML.Add(new XAttribute("class", method.ClassName));
			methodXML.Add(new XAttribute("name", method.Name));
			methodXML.Add(new XAttribute("time", method.Time));
			foreach (MethodInfo child in method.Children)
			{
				methodXML.Add(SerializeMethod(child));
			}
			return methodXML;
		}
	}
}

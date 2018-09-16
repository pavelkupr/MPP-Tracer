using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Trace
{
	public class ResultWriter
	{
		public void ConsolePrint(TraceResult traceRes)
		{
			WriteResultInStream(traceRes,Console.OpenStandardOutput());
		}

		public void FilePrint(TraceResult traceRes, string fileName)
		{
			Stream stream = File.Create(fileName + ".txt");
			WriteResultInStream(traceRes, stream);
			stream.Close();
		}

		private void WriteResultInStream(TraceResult traceResult, Stream stream)
		{
			foreach(KeyValuePair<int, ThreadTracer> thread in traceResult.Dictionary)
			{
				WriteLineInStream("Thread number: "+thread.Key.ToString(), stream);
				WriteLineInStream("Time: "+thread.Value.ThreadTime.ToString(), stream);
				foreach(MethodInfo method in thread.Value.MethodList)
				{
					WriteLineInStream("   Class name: " + method.ClassName.ToString(), stream);
					WriteLineInStream("   Name: " + method.Name.ToString(), stream);
					WriteLineInStream("   Time: " + method.Time.ToString(), stream);
					ChildrenResult(method, stream, 2);
				}
			}
		}

		private void ChildrenResult(MethodInfo method, Stream stream,int inCount)
		{
			string shift = "";
			inCount++;
			for (int i = 1; i < inCount; i++)
				shift += "   ";
			foreach (MethodInfo child in method.Children)
			{
				WriteLineInStream(shift + "Class name: " + child.ClassName.ToString(), stream);
				WriteLineInStream(shift + "Name: " + child.Name.ToString(), stream);
				WriteLineInStream(shift + "Time: " + child.Time.ToString(), stream);
				ChildrenResult(child, stream, inCount);
			}
		}

		private void WriteLineInStream(string str, Stream stream)
		{
			byte[] text = Encoding.ASCII.GetBytes(str + "\r\n");
			stream.Write(text, 0, text.Length);
		}
	}
}

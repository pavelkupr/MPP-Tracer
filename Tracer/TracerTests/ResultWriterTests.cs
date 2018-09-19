using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trace;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace TracerTests
{
	[TestClass]
	public class ResultWriterTests
	{
		private static ITracer tracer;
		private static TraceResult traceResult;
		private static ResultWriter resultWriter;

		[ClassInitialize]
		public static void ClassInitilize(TestContext context)
		{
			tracer = new Tracer();
			TestMethod_1();
			traceResult = tracer.GetTraceResult();
			resultWriter = new ResultWriter();

		}

		private static void TestMethod_1()
		{
			tracer.StartTrace();
			TestMethod_2();
			tracer.StopTrace();
		}

		private static void TestMethod_2()
		{
			tracer.StartTrace();
			tracer.StopTrace();
		}

		[TestMethod]
		public void ResultInFile_With_Txt_Create_Right_File()
		{
			byte[] factHash, expectedHash;
			TxtWriter txtWriter = new TxtWriter();
			MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider();
			Stream stream = new FileStream("test.txt", FileMode.Open, FileAccess.Read);
			expectedHash = mD5.ComputeHash(stream);
			stream.Close();

			resultWriter.ResultInFile(txtWriter, traceResult, "factres");
			stream = new FileStream("factres.txt", FileMode.Open, FileAccess.Read);
			factHash = mD5.ComputeHash(stream);
			stream.Close();
			File.Delete("factres.txt");

			if (!expectedHash.SequenceEqual(factHash))
				Assert.Fail("Created file is wrong");
		}

		[TestMethod]
		public void ResultInFile_With_Xml_Create_Right_File()
		{
			byte[] factHash, expectedHash;
			XmlSerializer xmlSerializer = new XmlSerializer();
			MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider();
			Stream stream = new FileStream("test.xml", FileMode.Open, FileAccess.Read);
			expectedHash = mD5.ComputeHash(stream);
			stream.Close();

			resultWriter.ResultInFile(xmlSerializer, traceResult, "factres");
			stream = new FileStream("factres.xml", FileMode.Open, FileAccess.Read);
			factHash = mD5.ComputeHash(stream);
			stream.Close();
			File.Delete("factres.xml");

			if (!expectedHash.SequenceEqual(factHash))
				Assert.Fail("Created file is wrong");
		}

		[TestMethod]
		public void ResultInFile_With_Json_Create_Right_File()
		{
			byte[] factHash, expectedHash;
			JsonSerializer jsonSerializer = new JsonSerializer();
			MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider();
			Stream stream = new FileStream("test.json", FileMode.Open, FileAccess.Read);
			expectedHash = mD5.ComputeHash(stream);
			stream.Close();

			resultWriter.ResultInFile(jsonSerializer, traceResult, "factres");
			stream = new FileStream("factres.json", FileMode.Open, FileAccess.Read);
			factHash = mD5.ComputeHash(stream);
			stream.Close();
			File.Delete("factres.json");

			if (!expectedHash.SequenceEqual(factHash))
				Assert.Fail("Created file is wrong");
		}
	}
}

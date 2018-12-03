using System;
using System.Xml;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HaFT.XmlComparator;

namespace Tests
{
	[TestClass]
	public class Complex
	{
		#region Static Members
		private const string
			ROOT = "Files\\Complex\\",
			FILENAME_1 = "Doc1.xml",
			FILENAME_2 = "Doc2.xml";

		private static XmlDocument LoadXmlDocument(string Filename, [CallerMemberName] string MethodName = null)
		{
			var Doc = new XmlDocument();
			Doc.Load($@"{ROOT}{MethodName}\{Filename}");
			return Doc;
		}

		private static void LoadXmlDocuments(out XmlDocument Doc1, out XmlDocument Doc2, [CallerMemberName] string MethodName = null)
		{
			Doc1 = LoadXmlDocument(FILENAME_1, MethodName);
			Doc2 = LoadXmlDocument(FILENAME_2, MethodName);
		}

		private static void Compare(bool ExpectedResult, [CallerMemberName] string MethodName = null)
		{
			XmlDocument Doc1, Doc2;
			LoadXmlDocuments(out Doc1, out Doc2, MethodName);

			var C = new Comparator();

			Assert.AreEqual(ExpectedResult, C.AreSame(Doc1, Doc2));
		}
		#endregion

		[TestMethod]
		public void Sibling_Same_DifferentOrder() => Compare(true);

		[TestMethod]
		public void Sibling_DifferentAttributes() => Compare(false);

		[TestMethod]
		public void Case1() => Compare(true);
	}
}

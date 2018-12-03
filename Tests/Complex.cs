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
		private const string ROOT = "Files\\Complex\\";

		private static void LoadXmlDocuments(out XmlDocument Doc1, out XmlDocument Doc2, [CallerMemberName] string MethodName = null)
		{
			Doc1 = new XmlDocument();
			Doc2 = new XmlDocument();

			Doc1.Load($"{ROOT}{MethodName}\\Doc1.xml");
			Doc2.Load($"{ROOT}{MethodName}\\Doc2.xml");
		}

		[TestMethod]
		public void Sibling_Same_DifferentOrder()
		{
			XmlDocument Doc1, Doc2;
			LoadXmlDocuments(out Doc1, out Doc2);

			var C = new Comparator();

			Assert.IsTrue(C.AreSame(Doc1, Doc2));
		}
	}
}

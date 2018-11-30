using System;
using System.Xml;
using HaFT.XmlComparator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
	[TestClass]
	public class RootOnly
	{
		[TestMethod]
		public void Same()
		{
			XmlDocument
				Doc1 = new XmlDocument(),
				Doc2 = new XmlDocument();

			Doc1.LoadXml("<Config />");
			Doc2.LoadXml("<Config />");

			var C = new Comparator();

			Assert.IsTrue(C.AreSame(Doc1, Doc2));
		}

		[TestMethod]
		public void DifferentName()
		{
			XmlDocument
				Doc1 = new XmlDocument(),
				Doc2 = new XmlDocument();

			Doc1.LoadXml("<Config />");
			Doc2.LoadXml("<Settings />");

			var C = new Comparator();

			Assert.IsFalse(C.AreSame(Doc1, Doc2));
		}

		[TestMethod]
		public void SameAttributes()
		{
			XmlDocument
				Doc1 = new XmlDocument(),
				Doc2 = new XmlDocument();

			Doc1.LoadXml("<Config a='4' b='Hello' />");
			Doc2.LoadXml("<Config b='Hello' a='4' />");

			var C = new Comparator();

			Assert.IsTrue(C.AreSame(Doc1, Doc2));
		}

		[TestMethod]
		public void DifferentNumberOfAttributes()
		{
			XmlDocument
				Doc1 = new XmlDocument(),
				Doc2 = new XmlDocument();

			Doc1.LoadXml("<Config a='4' b='Hello' c='12' />");
			Doc2.LoadXml("<Config b='Hello' a='4' />");

			var C = new Comparator();

			Assert.IsFalse(C.AreSame(Doc1, Doc2));
		}

		[TestMethod]
		public void AttributesWithDifferentValue()
		{
			XmlDocument
				Doc1 = new XmlDocument(),
				Doc2 = new XmlDocument();

			Doc1.LoadXml("<Config a='5' b='Hello' />");
			Doc2.LoadXml("<Config b='Hello' a='4' />");

			var C = new Comparator();

			Assert.IsFalse(C.AreSame(Doc1, Doc2));
		}
	}
}

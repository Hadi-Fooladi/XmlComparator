using System;
using System.Xml;
using System.Collections.Generic;

namespace HaFT.XmlComparator
{
	public class Comparator
	{
		public StringComparer
			NameStringComparer = StringComparer.Ordinal,
			ValueStringComparer = StringComparer.Ordinal;

		public bool AreSame(XmlDocument Doc1, XmlDocument Doc2) => AreEqual(Doc1.DocumentElement, Doc2.DocumentElement);

		private bool AreEqual(XmlNode E1, XmlNode E2)
		{
			// Check Names
			if (!NameStringComparer.Equals(E1.Name, E2.Name)) return false;

			// Check Attributes Count
			if (E1.Attributes.Count != E2.Attributes.Count) return false;

			XmlAttributeCollection
				A1 = E1.Attributes,
				A2 = E2.Attributes;

			// Check Attributes
			foreach (XmlAttribute A in A1)
			{
				var B = A2[A.Name];

				if (B == null) return false; // No Match

				if (!ValueStringComparer.Equals(A.Value, B.Value)) return false;
			}

			// Check Nested Nodes
			return Match(IterateChildNodes(E1), IterateChildNodes(E2));
		}

		private IEnumerable<XmlNode> FindMatches(XmlNode Match, IEnumerable<XmlNode> Nodes)
		{
			foreach (var Node in Nodes)
				if (AreEqual(Node, Match))
					yield return Node;
		}

		private bool Match(IEnumerable<XmlNode> Nodes1, IEnumerable<XmlNode> Nodes2)
		{
			List<XmlNode>
				L1 = new List<XmlNode>(Nodes1),
				L2 = new List<XmlNode>(Nodes2);

			if (L1.Count != L2.Count) return false;

			if (L1.Count == 0) return true;

			var Node = L1[0];
			L1.RemoveAt(0);

			foreach (var MatchNode in FindMatches(Node, L2))
			{
				L2.Remove(MatchNode);

				if (Match(L1, L2)) return true;

				L2.Add(MatchNode);
			}

			return false;
		}

		private static IEnumerable<XmlNode> IterateChildNodes(XmlNode Node)
		{
			foreach (XmlNode X in Node.ChildNodes)
				if (X.NodeType == XmlNodeType.Element)
					yield return X;
		}
	}
}

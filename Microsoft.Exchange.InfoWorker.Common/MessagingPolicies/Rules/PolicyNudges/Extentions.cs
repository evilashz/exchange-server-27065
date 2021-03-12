using System;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules.PolicyNudges
{
	// Token: 0x02000178 RID: 376
	internal static class Extentions
	{
		// Token: 0x06000A27 RID: 2599 RVA: 0x0002BB04 File Offset: 0x00029D04
		internal static XElement FirstElement(this XElement element)
		{
			if (element == null)
			{
				return null;
			}
			XNode xnode = element.FirstNode;
			while (xnode != null && xnode.NodeType != XmlNodeType.Element)
			{
				xnode = xnode.NextNode;
			}
			return xnode as XElement;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002BB38 File Offset: 0x00029D38
		internal static XElement NextElement(this XElement element)
		{
			if (element == null)
			{
				return null;
			}
			XNode nextNode = element.NextNode;
			while (nextNode != null && nextNode.NodeType != XmlNodeType.Element)
			{
				nextNode = nextNode.NextNode;
			}
			return nextNode as XElement;
		}
	}
}

using System;
using System.Xml;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B77 RID: 2935
	internal sealed class XmlAttributeDefinition : XmlNodeDefinition
	{
		// Token: 0x06003F05 RID: 16133 RVA: 0x000A620A File Offset: 0x000A440A
		internal XmlAttributeDefinition(string localName, XmlNamespaceDefinition xmlNamespaceDefinition) : base(localName, xmlNamespaceDefinition)
		{
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x000A6214 File Offset: 0x000A4414
		public XmlAttribute CreateAttribute(XmlDocument xmlDocument, string value)
		{
			XmlAttribute xmlAttribute = this.CreateAttribute(xmlDocument);
			xmlAttribute.Value = value;
			return xmlAttribute;
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x000A6231 File Offset: 0x000A4431
		public string GetAttributeValue(XmlElement xmlElement)
		{
			return xmlElement.GetAttribute(base.LocalName, base.XmlNamespaceDefinition.NamespaceURI);
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x000A624A File Offset: 0x000A444A
		private XmlAttribute CreateAttribute(XmlDocument xmlDocument)
		{
			return xmlDocument.CreateAttribute(base.XmlNamespaceDefinition.Prefix, base.LocalName, base.XmlNamespaceDefinition.NamespaceURI);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B78 RID: 2936
	internal sealed class XmlElementDefinition : XmlNodeDefinition
	{
		// Token: 0x06003F09 RID: 16137 RVA: 0x000A626E File Offset: 0x000A446E
		internal XmlElementDefinition(string localName, XmlNamespaceDefinition xmlNamespaceDefinition) : base(localName, xmlNamespaceDefinition)
		{
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x000A6278 File Offset: 0x000A4478
		public XmlElement GetSingleElementByName(XmlElement parent)
		{
			XmlElement xmlElement = null;
			foreach (object obj in parent.ChildNodes)
			{
				XmlElement xmlElement2 = (XmlElement)obj;
				if (base.IsMatch(xmlElement2))
				{
					if (xmlElement != null)
					{
						return null;
					}
					xmlElement = xmlElement2;
				}
			}
			return xmlElement;
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x000A62E4 File Offset: 0x000A44E4
		public XmlElement CreateElement(XmlDocument xmlDocument, IEnumerable<XmlAttribute> attributes)
		{
			return this.CreateElement(xmlDocument, attributes, null, null);
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x000A62F0 File Offset: 0x000A44F0
		public XmlElement CreateElement(XmlDocument xmlDocument, IEnumerable<XmlAttribute> attributes, string innerText)
		{
			return this.CreateElement(xmlDocument, attributes, innerText, null);
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x000A62FC File Offset: 0x000A44FC
		public XmlElement CreateElement(XmlDocument xmlDocument, IEnumerable<XmlAttribute> attributes, IEnumerable<XmlElement> childELements)
		{
			return this.CreateElement(xmlDocument, attributes, null, childELements);
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x000A6308 File Offset: 0x000A4508
		public XmlElement CreateElement(XmlDocument xmlDocument, string innerText)
		{
			return this.CreateElement(xmlDocument, null, innerText, null);
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x000A6314 File Offset: 0x000A4514
		public XmlElement CreateElement(XmlDocument xmlDocument, IEnumerable<XmlElement> childELements)
		{
			return this.CreateElement(xmlDocument, null, null, childELements);
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x000A6320 File Offset: 0x000A4520
		private XmlElement CreateElement(XmlDocument xmlDocument, IEnumerable<XmlAttribute> attributes, string innerText, IEnumerable<XmlElement> childELements)
		{
			XmlElement xmlElement = xmlDocument.CreateElement(base.XmlNamespaceDefinition.Prefix, base.LocalName, base.XmlNamespaceDefinition.NamespaceURI);
			xmlElement.InnerText = innerText;
			if (childELements != null)
			{
				foreach (XmlElement newChild in childELements)
				{
					xmlElement.AppendChild(newChild);
				}
			}
			if (attributes != null)
			{
				foreach (XmlAttribute node in attributes)
				{
					xmlElement.Attributes.Append(node);
				}
			}
			return xmlElement;
		}
	}
}

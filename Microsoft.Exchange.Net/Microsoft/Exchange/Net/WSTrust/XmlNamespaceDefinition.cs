using System;
using System.Xml;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B7A RID: 2938
	internal class XmlNamespaceDefinition
	{
		// Token: 0x06003F1C RID: 16156 RVA: 0x000A6937 File Offset: 0x000A4B37
		internal XmlNamespaceDefinition(string prefix, string namespaceURI)
		{
			this.prefix = prefix;
			this.namespaceURI = namespaceURI;
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06003F1D RID: 16157 RVA: 0x000A694D File Offset: 0x000A4B4D
		internal string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06003F1E RID: 16158 RVA: 0x000A6955 File Offset: 0x000A4B55
		internal string NamespaceURI
		{
			get
			{
				return this.namespaceURI;
			}
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x000A6960 File Offset: 0x000A4B60
		internal static void AddPrefixes(XmlDocument xmlDocument, XmlElement xmlElement, params XmlNamespaceDefinition[] xmlNamespaceDefinitions)
		{
			foreach (XmlNamespaceDefinition xmlNamespaceDefinition in xmlNamespaceDefinitions)
			{
				if (xmlNamespaceDefinition.NamespaceURI != null && xmlNamespaceDefinition.Prefix != null)
				{
					XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("xmlns", xmlNamespaceDefinition.Prefix, "http://www.w3.org/2000/xmlns/");
					xmlAttribute.Value = xmlNamespaceDefinition.NamespaceURI;
					xmlElement.Attributes.Append(xmlAttribute);
				}
			}
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x000A69C1 File Offset: 0x000A4BC1
		internal void WriteAttribute(XmlWriter writer)
		{
			writer.WriteAttributeString("xmlns", this.prefix, null, this.namespaceURI);
		}

		// Token: 0x040036C6 RID: 14022
		private const string XmlNamespacePrefix = "xmlns";

		// Token: 0x040036C7 RID: 14023
		private const string XmlNamespaceURI = "http://www.w3.org/2000/xmlns/";

		// Token: 0x040036C8 RID: 14024
		private string prefix;

		// Token: 0x040036C9 RID: 14025
		private string namespaceURI;
	}
}

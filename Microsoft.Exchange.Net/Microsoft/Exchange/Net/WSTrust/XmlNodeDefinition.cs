using System;
using System.Xml;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B76 RID: 2934
	internal abstract class XmlNodeDefinition
	{
		// Token: 0x06003F00 RID: 16128 RVA: 0x000A612B File Offset: 0x000A432B
		protected XmlNodeDefinition(string localName, XmlNamespaceDefinition xmlNamespaceDefinition)
		{
			this.localName = localName;
			this.xmlNamespaceDefinition = xmlNamespaceDefinition;
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x000A6141 File Offset: 0x000A4341
		public bool IsMatch(XmlNode node)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(node.LocalName, this.localName) && StringComparer.OrdinalIgnoreCase.Equals(node.NamespaceURI, this.xmlNamespaceDefinition.NamespaceURI);
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06003F02 RID: 16130 RVA: 0x000A6178 File Offset: 0x000A4378
		protected string LocalName
		{
			get
			{
				return this.localName;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06003F03 RID: 16131 RVA: 0x000A6180 File Offset: 0x000A4380
		protected XmlNamespaceDefinition XmlNamespaceDefinition
		{
			get
			{
				return this.xmlNamespaceDefinition;
			}
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x000A6188 File Offset: 0x000A4388
		public override string ToString()
		{
			if (this.xmlNamespaceDefinition.NamespaceURI == null)
			{
				return "<" + this.localName + ">";
			}
			return string.Concat(new string[]
			{
				"<",
				this.localName,
				" ",
				this.xmlNamespaceDefinition.Prefix,
				":xmlns=\"",
				this.xmlNamespaceDefinition.NamespaceURI,
				"\">"
			});
		}

		// Token: 0x040036C1 RID: 14017
		private string localName;

		// Token: 0x040036C2 RID: 14018
		private XmlNamespaceDefinition xmlNamespaceDefinition;
	}
}

using System;
using System.Security;
using System.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.WBXml
{
	// Token: 0x02000079 RID: 121
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class WBXmlSecureStringNode : XmlElement
	{
		// Token: 0x06000241 RID: 577 RVA: 0x000077DF File Offset: 0x000059DF
		internal WBXmlSecureStringNode(string prefix, string localName, string namespaceUri, XmlDocument doc) : base(prefix, localName, namespaceUri, doc)
		{
		}

		// Token: 0x170000BD RID: 189
		// (set) Token: 0x06000242 RID: 578 RVA: 0x000077EC File Offset: 0x000059EC
		internal SecureString SecureData
		{
			set
			{
				this.secureData = value;
				this.InnerText = "******";
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00007800 File Offset: 0x00005A00
		internal SecureString DetachSecureData()
		{
			SecureString result = this.secureData;
			this.secureData = null;
			return result;
		}

		// Token: 0x040003E6 RID: 998
		private SecureString secureData;
	}
}

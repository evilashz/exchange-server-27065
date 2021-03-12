using System;
using System.Security;
using System.Xml;

namespace Microsoft.Exchange.AirSync.Wbxml
{
	// Token: 0x0200029E RID: 670
	internal class AirSyncSecureStringXmlNode : XmlElement
	{
		// Token: 0x06001877 RID: 6263 RVA: 0x0008F9C3 File Offset: 0x0008DBC3
		public AirSyncSecureStringXmlNode(string prefix, string localName, string namespaceURI, XmlDocument doc) : base(prefix, localName, namespaceURI, doc)
		{
		}

		// Token: 0x17000839 RID: 2105
		// (set) Token: 0x06001878 RID: 6264 RVA: 0x0008F9D0 File Offset: 0x0008DBD0
		public SecureString SecureData
		{
			set
			{
				this.secureData = value;
				this.InnerText = "******";
			}
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0008F9E4 File Offset: 0x0008DBE4
		public SecureString DetachSecureData()
		{
			SecureString result = this.secureData;
			this.secureData = null;
			return result;
		}

		// Token: 0x04000F26 RID: 3878
		private SecureString secureData;
	}
}

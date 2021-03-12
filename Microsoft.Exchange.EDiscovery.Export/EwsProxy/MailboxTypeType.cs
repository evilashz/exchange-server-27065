using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000EE RID: 238
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum MailboxTypeType
	{
		// Token: 0x040007F5 RID: 2037
		Unknown,
		// Token: 0x040007F6 RID: 2038
		OneOff,
		// Token: 0x040007F7 RID: 2039
		Mailbox,
		// Token: 0x040007F8 RID: 2040
		PublicDL,
		// Token: 0x040007F9 RID: 2041
		PrivateDL,
		// Token: 0x040007FA RID: 2042
		Contact,
		// Token: 0x040007FB RID: 2043
		PublicFolder,
		// Token: 0x040007FC RID: 2044
		GroupMailbox
	}
}

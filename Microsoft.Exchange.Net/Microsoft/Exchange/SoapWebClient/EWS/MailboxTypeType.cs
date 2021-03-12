using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001CF RID: 463
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum MailboxTypeType
	{
		// Token: 0x04000C47 RID: 3143
		Unknown,
		// Token: 0x04000C48 RID: 3144
		OneOff,
		// Token: 0x04000C49 RID: 3145
		Mailbox,
		// Token: 0x04000C4A RID: 3146
		PublicDL,
		// Token: 0x04000C4B RID: 3147
		PrivateDL,
		// Token: 0x04000C4C RID: 3148
		Contact,
		// Token: 0x04000C4D RID: 3149
		PublicFolder,
		// Token: 0x04000C4E RID: 3150
		GroupMailbox
	}
}

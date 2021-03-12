using System;

namespace Microsoft.Exchange.Transport.LoggingCommon
{
	// Token: 0x0200000F RID: 15
	internal enum ThrottlingScope
	{
		// Token: 0x0400008C RID: 140
		All,
		// Token: 0x0400008D RID: 141
		Tenant,
		// Token: 0x0400008E RID: 142
		Sender,
		// Token: 0x0400008F RID: 143
		Recipient,
		// Token: 0x04000090 RID: 144
		SenderRecipient,
		// Token: 0x04000091 RID: 145
		SenderRecipientSubject,
		// Token: 0x04000092 RID: 146
		SenderRecipientSubjectPart,
		// Token: 0x04000093 RID: 147
		MBServer,
		// Token: 0x04000094 RID: 148
		MDB,
		// Token: 0x04000095 RID: 149
		AccountForest
	}
}

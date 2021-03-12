using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200004D RID: 77
	[Flags]
	public enum MessageConfigureFlags
	{
		// Token: 0x0400012D RID: 301
		None = 0,
		// Token: 0x0400012E RID: 302
		CreateNewMessage = 1,
		// Token: 0x0400012F RID: 303
		IsAssociated = 2,
		// Token: 0x04000130 RID: 304
		IsContentAggregation = 4,
		// Token: 0x04000131 RID: 305
		RequestReadOnly = 8,
		// Token: 0x04000132 RID: 306
		RequestWrite = 16,
		// Token: 0x04000133 RID: 307
		IsReportMessage = 32,
		// Token: 0x04000134 RID: 308
		SkipQuotaCheck = 64
	}
}

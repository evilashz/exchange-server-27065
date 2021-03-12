using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200017D RID: 381
	[Flags]
	internal enum ProcessTransportRoleFlags
	{
		// Token: 0x040008E9 RID: 2281
		None = 0,
		// Token: 0x040008EA RID: 2282
		Hub = 1,
		// Token: 0x040008EB RID: 2283
		Edge = 2,
		// Token: 0x040008EC RID: 2284
		FrontEnd = 4,
		// Token: 0x040008ED RID: 2285
		MailboxSubmission = 8,
		// Token: 0x040008EE RID: 2286
		MailboxDelivery = 16,
		// Token: 0x040008EF RID: 2287
		All = 31
	}
}

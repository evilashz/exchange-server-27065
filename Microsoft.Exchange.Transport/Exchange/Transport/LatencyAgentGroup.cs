using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200017A RID: 378
	internal enum LatencyAgentGroup
	{
		// Token: 0x040008D7 RID: 2263
		Categorizer,
		// Token: 0x040008D8 RID: 2264
		SmtpReceive,
		// Token: 0x040008D9 RID: 2265
		StoreDriver,
		// Token: 0x040008DA RID: 2266
		Delivery,
		// Token: 0x040008DB RID: 2267
		MailboxTransportSubmissionStoreDriverSubmission,
		// Token: 0x040008DC RID: 2268
		Storage,
		// Token: 0x040008DD RID: 2269
		UnassignedAgentGroup = 2147483647,
		// Token: 0x040008DE RID: 2270
		AgentGroupCount = 6
	}
}

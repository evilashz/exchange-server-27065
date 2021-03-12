using System;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x0200021F RID: 543
	internal enum MailboxState
	{
		// Token: 0x04000A12 RID: 2578
		NotStarted,
		// Token: 0x04000A13 RID: 2579
		NormalCrawlInProgress,
		// Token: 0x04000A14 RID: 2580
		Done = 4,
		// Token: 0x04000A15 RID: 2581
		DeletionPending,
		// Token: 0x04000A16 RID: 2582
		InTransit,
		// Token: 0x04000A17 RID: 2583
		Failed = 100
	}
}

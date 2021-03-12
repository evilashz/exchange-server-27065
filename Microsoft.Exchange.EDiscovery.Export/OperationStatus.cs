using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000056 RID: 86
	public enum OperationStatus : uint
	{
		// Token: 0x0400024E RID: 590
		None,
		// Token: 0x0400024F RID: 591
		Pending,
		// Token: 0x04000250 RID: 592
		Searching,
		// Token: 0x04000251 RID: 593
		RetrySearching,
		// Token: 0x04000252 RID: 594
		SearchCompleted,
		// Token: 0x04000253 RID: 595
		Stopping,
		// Token: 0x04000254 RID: 596
		Processing,
		// Token: 0x04000255 RID: 597
		PartiallyProcessed,
		// Token: 0x04000256 RID: 598
		Processed,
		// Token: 0x04000257 RID: 599
		Rollbacking
	}
}

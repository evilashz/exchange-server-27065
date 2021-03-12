using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000A5 RID: 165
	internal enum DeferReason
	{
		// Token: 0x04000267 RID: 615
		None,
		// Token: 0x04000268 RID: 616
		ADTransientFailureDuringResolve,
		// Token: 0x04000269 RID: 617
		ADTransientFailureDuringContentConversion,
		// Token: 0x0400026A RID: 618
		Agent,
		// Token: 0x0400026B RID: 619
		LoopDetected,
		// Token: 0x0400026C RID: 620
		ReroutedByStoreDriver,
		// Token: 0x0400026D RID: 621
		StorageTransientFailureDuringContentConversion,
		// Token: 0x0400026E RID: 622
		MarkedAsRetryDeliveryIfRejected,
		// Token: 0x0400026F RID: 623
		TransientFailure,
		// Token: 0x04000270 RID: 624
		AmbiguousRecipient,
		// Token: 0x04000271 RID: 625
		ConcurrencyLimitInStoreDriver,
		// Token: 0x04000272 RID: 626
		TargetSiteInboundMailDisabled,
		// Token: 0x04000273 RID: 627
		RecipientThreadLimitExceeded,
		// Token: 0x04000274 RID: 628
		TransientAttributionFailure,
		// Token: 0x04000275 RID: 629
		TransientAcceptedDomainsLoadFailure,
		// Token: 0x04000276 RID: 630
		RecipientHasNoMdb,
		// Token: 0x04000277 RID: 631
		ConfigUpdate
	}
}

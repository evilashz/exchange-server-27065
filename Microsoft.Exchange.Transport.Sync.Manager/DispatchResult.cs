using System;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000049 RID: 73
	internal enum DispatchResult
	{
		// Token: 0x040001F7 RID: 503
		Success,
		// Token: 0x040001F8 RID: 504
		TransientFailure = 4096,
		// Token: 0x040001F9 RID: 505
		PermanentFailure = 8192,
		// Token: 0x040001FA RID: 506
		InvalidSubscription = 16384,
		// Token: 0x040001FB RID: 507
		SubscriptionLoseItsTurnAtTransientFailure = 36864,
		// Token: 0x040001FC RID: 508
		DatabaseLosesItsTurnAtTransientFailure = 4352,
		// Token: 0x040001FD RID: 509
		WorkerSlotsFull = 4097,
		// Token: 0x040001FE RID: 510
		UnableToContactWorker,
		// Token: 0x040001FF RID: 511
		NoHubsToDispatchTo,
		// Token: 0x04000200 RID: 512
		SubscriptionAlreadyDispatched = 36868,
		// Token: 0x04000201 RID: 513
		SubscriptionCacheMessageDoesNotExist = 16389,
		// Token: 0x04000202 RID: 514
		TransientFailureReadingCache = 4358,
		// Token: 0x04000203 RID: 515
		SubscriptionDisabled = 16391,
		// Token: 0x04000204 RID: 516
		MaxSyncsPerDatabase = 4360,
		// Token: 0x04000205 RID: 517
		DatabaseHealthUnknown,
		// Token: 0x04000206 RID: 518
		DatabaseRpcLatencyUnhealthy,
		// Token: 0x04000207 RID: 519
		MailboxServerHAUnhealthy,
		// Token: 0x04000208 RID: 520
		MailboxServerCpuUnknown = 4108,
		// Token: 0x04000209 RID: 521
		MailboxServerCpuOverloaded,
		// Token: 0x0400020A RID: 522
		TransportQueueHealthUnknown,
		// Token: 0x0400020B RID: 523
		ServerTransportQueueUnhealthy,
		// Token: 0x0400020C RID: 524
		UserTransportQueueUnhealthy = 37136,
		// Token: 0x0400020D RID: 525
		PolicyInducedDeletion = 17,
		// Token: 0x0400020E RID: 526
		DatabaseOverloaded = 4370,
		// Token: 0x0400020F RID: 527
		ServerNotAvailable = 4115,
		// Token: 0x04000210 RID: 528
		EdgeTransportStopped,
		// Token: 0x04000211 RID: 529
		SubscriptionTypeDisabled,
		// Token: 0x04000212 RID: 530
		TransportSyncDisabled,
		// Token: 0x04000213 RID: 531
		MaxConcurrentMailboxSubmissions
	}
}

using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Rpc.Submission
{
	// Token: 0x020000A3 RID: 163
	internal enum SubscriptionSubmissionResult : uint
	{
		// Token: 0x04000265 RID: 613
		Success,
		// Token: 0x04000266 RID: 614
		SubscriptionAlreadyOnHub,
		// Token: 0x04000267 RID: 615
		UnknownRetryableError,
		// Token: 0x04000268 RID: 616
		SchedulerQueueFull,
		// Token: 0x04000269 RID: 617
		MaxConcurrentMailboxSubmissions,
		// Token: 0x0400026A RID: 618
		RpcServerTooBusy,
		// Token: 0x0400026B RID: 619
		RetryableRpcError,
		// Token: 0x0400026C RID: 620
		DatabaseRpcLatencyUnhealthy,
		// Token: 0x0400026D RID: 621
		DatabaseHealthUnknown,
		// Token: 0x0400026E RID: 622
		DatabaseOverloaded,
		// Token: 0x0400026F RID: 623
		ServerNotAvailable,
		// Token: 0x04000270 RID: 624
		EdgeTransportStopped,
		// Token: 0x04000271 RID: 625
		SubscriptionTypeDisabled,
		// Token: 0x04000272 RID: 626
		TransportSyncDisabled,
		// Token: 0x04000273 RID: 627
		MailboxServerCpuUnhealthy,
		// Token: 0x04000274 RID: 628
		MailboxServerCpuUnknown,
		// Token: 0x04000275 RID: 629
		MailboxServerHAUnhealthy,
		// Token: 0x04000276 RID: 630
		ServerTransportQueueUnhealthy,
		// Token: 0x04000277 RID: 631
		UserTransportQueueUnhealthy,
		// Token: 0x04000278 RID: 632
		TransportQueueHealthUnknown
	}
}

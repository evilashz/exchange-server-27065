using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002E3 RID: 739
	internal interface IQueueQuotaConfig
	{
		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060020C2 RID: 8386
		bool EnforceQuota { get; }

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060020C3 RID: 8387
		double WarningRatio { get; }

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060020C4 RID: 8388
		double LowWatermarkRatio { get; }

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060020C5 RID: 8389
		int SubmissionQueueCapacity { get; }

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060020C6 RID: 8390
		int TotalQueueCapacity { get; }

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060020C7 RID: 8391
		int OrganizationQueueQuota { get; }

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060020C8 RID: 8392
		int SafeTenantOrganizationQueueQuota { get; }

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060020C9 RID: 8393
		int OutlookTenantOrganizationQueueQuota { get; }

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060020CA RID: 8394
		int OutlookTenantSenderQueueQuota { get; }

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060020CB RID: 8395
		int SenderQueueQuota { get; }

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060020CC RID: 8396
		int NullSenderQueueQuota { get; }

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060020CD RID: 8397
		int SenderTrackingThreshold { get; }

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060020CE RID: 8398
		int NumberOfOrganizationsLoggedInSummary { get; }

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060020CF RID: 8399
		int NumberOfSendersLoggedInSummary { get; }

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x060020D0 RID: 8400
		TimeSpan TrackerEntryLifeTime { get; }

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x060020D1 RID: 8401
		TimeSpan TrackSummaryLoggingInterval { get; }

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x060020D2 RID: 8402
		TimeSpan TrackSummaryBucketLength { get; }

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x060020D3 RID: 8403
		int MaxSummaryLinesLogged { get; }

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060020D4 RID: 8404
		// (set) Token: 0x060020D5 RID: 8405
		bool AccountForestEnabled { get; set; }

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060020D6 RID: 8406
		// (set) Token: 0x060020D7 RID: 8407
		int AccountForestQueueQuota { get; set; }
	}
}

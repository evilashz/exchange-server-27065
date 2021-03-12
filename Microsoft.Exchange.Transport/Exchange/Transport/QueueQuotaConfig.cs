using System;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002E4 RID: 740
	internal class QueueQuotaConfig : IQueueQuotaConfig
	{
		// Token: 0x060020D8 RID: 8408 RVA: 0x0007CE58 File Offset: 0x0007B058
		public QueueQuotaConfig(TransportAppConfig.FlowControlLogConfig flowControlLogConfig, TransportAppConfig.QueueConfig queueConfig)
		{
			this.EnforceQuota = TransportAppConfig.GetConfigBool("EnforceQueueQuota", VariantConfiguration.InvariantNoFlightingSnapshot.Transport.EnforceQueueQuota.Enabled);
			this.WarningRatio = TransportAppConfig.GetConfigDouble("QueueQuotaWarningRatio", 0.0, 1.0, 0.4);
			this.LowWatermarkRatio = TransportAppConfig.GetConfigDouble("QueueQuotaLowWatermarkRatio", 0.0, 1.0, 0.66);
			this.SubmissionQueueCapacity = TransportAppConfig.GetConfigInt("SubmissionQueueCapacity", 0, int.MaxValue, 10000);
			this.TotalQueueCapacity = TransportAppConfig.GetConfigInt("TotalQueueCapacity", 0, int.MaxValue, 100000);
			this.OrganizationQueueQuota = TransportAppConfig.GetConfigInt("OrganizationQueueQuota", 0, 100, 50);
			this.SafeTenantOrganizationQueueQuota = TransportAppConfig.GetConfigInt("SafeTenantOrganizationQueueQuota", 0, 100, 100);
			this.OutlookTenantOrganizationQueueQuota = TransportAppConfig.GetConfigInt("OutlookTenantOrganizationQueueQuota", 0, 500, 150);
			this.OutlookTenantSenderQueueQuota = TransportAppConfig.GetConfigInt("OutlookTenantSenderQueueQuota", 0, 100, 1);
			this.SenderQueueQuota = TransportAppConfig.GetConfigInt("SenderQueueQuota", 0, 100, 10);
			this.NullSenderQueueQuota = TransportAppConfig.GetConfigInt("NullSenderQueueQuota", 0, 100, 50);
			this.SenderTrackingThreshold = TransportAppConfig.GetConfigInt("QueueQuotaSenderTrackingThreshold", 0, int.MaxValue, 100);
			this.NumberOfOrganizationsLoggedInSummary = TransportAppConfig.GetConfigInt("NumberOfOrganizationsLoggedInQueueQuotaSummary", 0, int.MaxValue, 50);
			this.NumberOfSendersLoggedInSummary = TransportAppConfig.GetConfigInt("NumberOfSendersLoggedInQueueQuotaSummary", 0, int.MaxValue, 3);
			this.TrackerEntryLifeTime = TransportAppConfig.GetConfigTimeSpan("QueueQuotaTrackerEntryLifeTime", TimeSpan.Zero, TimeSpan.FromHours(1.0), TimeSpan.FromMinutes(15.0));
			this.AccountForestEnabled = TransportAppConfig.GetConfigBool("QueueQuotaAccountForestEnabled", false);
			this.AccountForestQueueQuota = TransportAppConfig.GetConfigInt("QueueQuotaAccountForestQuota", 0, 100, 75);
			this.TrackSummaryLoggingInterval = flowControlLogConfig.SummaryLoggingInterval;
			this.TrackSummaryBucketLength = flowControlLogConfig.SummaryBucketLength;
			this.MaxSummaryLinesLogged = flowControlLogConfig.MaxSummaryLinesLogged;
			this.RecentPerfCounterTrackingInterval = queueConfig.RecentPerfCounterTrackingInterval;
			this.RecentPerfCounterTrackingBucketSize = queueConfig.RecentPerfCounterTrackingBucketSize;
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x0007D07B File Offset: 0x0007B27B
		// (set) Token: 0x060020DA RID: 8410 RVA: 0x0007D083 File Offset: 0x0007B283
		public bool EnforceQuota { get; private set; }

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x0007D08C File Offset: 0x0007B28C
		// (set) Token: 0x060020DC RID: 8412 RVA: 0x0007D094 File Offset: 0x0007B294
		public double WarningRatio { get; private set; }

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x0007D09D File Offset: 0x0007B29D
		// (set) Token: 0x060020DE RID: 8414 RVA: 0x0007D0A5 File Offset: 0x0007B2A5
		public double LowWatermarkRatio { get; private set; }

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x0007D0AE File Offset: 0x0007B2AE
		// (set) Token: 0x060020E0 RID: 8416 RVA: 0x0007D0B6 File Offset: 0x0007B2B6
		public int SubmissionQueueCapacity { get; private set; }

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x0007D0BF File Offset: 0x0007B2BF
		// (set) Token: 0x060020E2 RID: 8418 RVA: 0x0007D0C7 File Offset: 0x0007B2C7
		public int TotalQueueCapacity { get; private set; }

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x060020E3 RID: 8419 RVA: 0x0007D0D0 File Offset: 0x0007B2D0
		// (set) Token: 0x060020E4 RID: 8420 RVA: 0x0007D0D8 File Offset: 0x0007B2D8
		public int OrganizationQueueQuota { get; private set; }

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x0007D0E1 File Offset: 0x0007B2E1
		// (set) Token: 0x060020E6 RID: 8422 RVA: 0x0007D0E9 File Offset: 0x0007B2E9
		public int SafeTenantOrganizationQueueQuota { get; private set; }

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x060020E7 RID: 8423 RVA: 0x0007D0F2 File Offset: 0x0007B2F2
		// (set) Token: 0x060020E8 RID: 8424 RVA: 0x0007D0FA File Offset: 0x0007B2FA
		public int OutlookTenantOrganizationQueueQuota { get; private set; }

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x060020E9 RID: 8425 RVA: 0x0007D103 File Offset: 0x0007B303
		// (set) Token: 0x060020EA RID: 8426 RVA: 0x0007D10B File Offset: 0x0007B30B
		public int OutlookTenantSenderQueueQuota { get; private set; }

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x060020EB RID: 8427 RVA: 0x0007D114 File Offset: 0x0007B314
		// (set) Token: 0x060020EC RID: 8428 RVA: 0x0007D11C File Offset: 0x0007B31C
		public int SenderQueueQuota { get; private set; }

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x0007D125 File Offset: 0x0007B325
		// (set) Token: 0x060020EE RID: 8430 RVA: 0x0007D12D File Offset: 0x0007B32D
		public int NullSenderQueueQuota { get; private set; }

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x0007D136 File Offset: 0x0007B336
		// (set) Token: 0x060020F0 RID: 8432 RVA: 0x0007D13E File Offset: 0x0007B33E
		public int SenderTrackingThreshold { get; private set; }

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x0007D147 File Offset: 0x0007B347
		// (set) Token: 0x060020F2 RID: 8434 RVA: 0x0007D14F File Offset: 0x0007B34F
		public int NumberOfOrganizationsLoggedInSummary { get; private set; }

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x0007D158 File Offset: 0x0007B358
		// (set) Token: 0x060020F4 RID: 8436 RVA: 0x0007D160 File Offset: 0x0007B360
		public int NumberOfSendersLoggedInSummary { get; private set; }

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x0007D169 File Offset: 0x0007B369
		// (set) Token: 0x060020F6 RID: 8438 RVA: 0x0007D171 File Offset: 0x0007B371
		public TimeSpan TrackerEntryLifeTime { get; private set; }

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x0007D17A File Offset: 0x0007B37A
		// (set) Token: 0x060020F8 RID: 8440 RVA: 0x0007D182 File Offset: 0x0007B382
		public TimeSpan TrackSummaryLoggingInterval { get; private set; }

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x0007D18B File Offset: 0x0007B38B
		// (set) Token: 0x060020FA RID: 8442 RVA: 0x0007D193 File Offset: 0x0007B393
		public TimeSpan TrackSummaryBucketLength { get; private set; }

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x0007D19C File Offset: 0x0007B39C
		// (set) Token: 0x060020FC RID: 8444 RVA: 0x0007D1A4 File Offset: 0x0007B3A4
		public int MaxSummaryLinesLogged { get; private set; }

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x060020FD RID: 8445 RVA: 0x0007D1AD File Offset: 0x0007B3AD
		// (set) Token: 0x060020FE RID: 8446 RVA: 0x0007D1B5 File Offset: 0x0007B3B5
		public bool AccountForestEnabled { get; set; }

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x060020FF RID: 8447 RVA: 0x0007D1BE File Offset: 0x0007B3BE
		// (set) Token: 0x06002100 RID: 8448 RVA: 0x0007D1C6 File Offset: 0x0007B3C6
		public int AccountForestQueueQuota { get; set; }

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06002101 RID: 8449 RVA: 0x0007D1CF File Offset: 0x0007B3CF
		// (set) Token: 0x06002102 RID: 8450 RVA: 0x0007D1D7 File Offset: 0x0007B3D7
		public TimeSpan RecentPerfCounterTrackingInterval { get; private set; }

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x0007D1E0 File Offset: 0x0007B3E0
		// (set) Token: 0x06002104 RID: 8452 RVA: 0x0007D1E8 File Offset: 0x0007B3E8
		public TimeSpan RecentPerfCounterTrackingBucketSize { get; private set; }

		// Token: 0x06002105 RID: 8453 RVA: 0x0007D1F1 File Offset: 0x0007B3F1
		public static bool IsQueueQuotaEnabled()
		{
			return TransportAppConfig.GetConfigBool("QueueQuotaEnabled", false);
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x0007D1FE File Offset: 0x0007B3FE
		public static bool IsQueueQuotaWithMeteringEnabled()
		{
			return TransportAppConfig.GetConfigBool("QueueQuotaMeteringEnabled", true);
		}
	}
}

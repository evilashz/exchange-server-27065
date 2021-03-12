using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;

namespace Microsoft.Exchange.MailboxLoadBalance.Config
{
	// Token: 0x02000033 RID: 51
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ILoadBalanceSettings
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600019A RID: 410
		bool AutomaticDatabaseDrainEnabled { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600019B RID: 411
		int AutomaticDrainStartFileSizePercent { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600019C RID: 412
		bool AutomaticLoadBalancingEnabled { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600019D RID: 413
		BatchSizeReducerType BatchBatchSizeReducer { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600019E RID: 414
		bool BuildLocalCacheOnStartup { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600019F RID: 415
		int CapacityGrowthPeriods { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001A0 RID: 416
		TimeSpan ClientCacheTimeToLive { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001A1 RID: 417
		int ConsumerGrowthRate { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001A2 RID: 418
		long DefaultDatabaseMaxSizeGb { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001A3 RID: 419
		int DefaultDatabaseRelativeLoadCapacity { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001A4 RID: 420
		bool DisableWlm { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001A5 RID: 421
		bool DontCreateMoveRequests { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001A6 RID: 422
		bool DontRemoveSoftDeletedMailboxes { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001A7 RID: 423
		string ExcludedMailboxProcessors { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A8 RID: 424
		TimeSpan IdleRunDelay { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001A9 RID: 425
		int InjectionBatchSize { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001AA RID: 426
		bool IsEnabled { get; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AB RID: 427
		bool LoadBalanceBlocked { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001AC RID: 428
		TimeSpan LocalCacheRefreshPeriod { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001AD RID: 429
		string LogFilePath { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001AE RID: 430
		long LogMaxDirectorySize { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001AF RID: 431
		long LogMaxFileSize { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B0 RID: 432
		bool MailboxProcessorsEnabled { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001B1 RID: 433
		int MaxDatabaseDiskUtilizationPercent { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001B2 RID: 434
		long MaximumAmountOfDataPerRoundGb { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001B3 RID: 435
		long MaximumBatchSizeGb { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001B4 RID: 436
		int MaximumConsumerMailboxSizePercent { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001B5 RID: 437
		int MaximumNumberOfRunspaces { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001B6 RID: 438
		long MaximumPendingMoveCount { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001B7 RID: 439
		TimeSpan MinimumSoftDeletedMailboxCleanupAge { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001B8 RID: 440
		string NonMovableOrganizationIds { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001B9 RID: 441
		int OrganizationGrowthRate { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001BA RID: 442
		int QueryBufferPeriodDays { get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001BB RID: 443
		int ReservedCapacityInGb { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001BC RID: 444
		bool SoftDeletedCleanupEnabled { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001BD RID: 445
		int SoftDeletedCleanupThreshold { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001BE RID: 446
		int TransientFailureMaxRetryCount { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001BF RID: 447
		TimeSpan TransientFailureRetryDelay { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001C0 RID: 448
		bool UseCachingActiveManager { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001C1 RID: 449
		bool UseDatabaseSelectorForMoveInjection { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001C2 RID: 450
		bool UseHeatMapProvisioning { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001C3 RID: 451
		bool UseParallelDiscovery { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001C4 RID: 452
		int WeightDeviationPercent { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001C5 RID: 453
		TimeSpan MinimumTimeInDatabaseForItemUpgrade { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001C6 RID: 454
		// (set) Token: 0x060001C7 RID: 455
		string DisabledMailboxPolicies { get; set; }
	}
}

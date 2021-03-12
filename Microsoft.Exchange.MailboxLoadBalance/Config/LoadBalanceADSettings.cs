using System;
using System.Configuration;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxLoadBalance.Config
{
	// Token: 0x02000034 RID: 52
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceADSettings : AnchorConfig, ILoadBalanceSettings
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00007444 File Offset: 0x00005644
		public LoadBalanceADSettings() : base("MailboxLoadBalance")
		{
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00007451 File Offset: 0x00005651
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000745E File Offset: 0x0000565E
		[ConfigurationProperty("AutomaticDatabaseDrainEnabled", DefaultValue = true)]
		public bool AutomaticDatabaseDrainEnabled
		{
			get
			{
				return base.GetConfig<bool>("AutomaticDatabaseDrainEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "AutomaticDatabaseDrainEnabled");
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000746C File Offset: 0x0000566C
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00007479 File Offset: 0x00005679
		[IntegerValidator(ExcludeRange = false, MinValue = 0, MaxValue = 100)]
		[ConfigurationProperty("AutomaticDrainStartFileSizePercent", DefaultValue = 20)]
		public int AutomaticDrainStartFileSizePercent
		{
			get
			{
				return base.GetConfig<int>("AutomaticDrainStartFileSizePercent");
			}
			set
			{
				this.InternalSetConfig<int>(value, "AutomaticDrainStartFileSizePercent");
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00007487 File Offset: 0x00005687
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00007494 File Offset: 0x00005694
		[ConfigurationProperty("AutomaticLoadBalancingEnabled", DefaultValue = true)]
		public bool AutomaticLoadBalancingEnabled
		{
			get
			{
				return base.GetConfig<bool>("AutomaticLoadBalancingEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "AutomaticLoadBalancingEnabled");
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000074A2 File Offset: 0x000056A2
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x000074AF File Offset: 0x000056AF
		[ConfigurationProperty("BatchBatchSizeReducer", DefaultValue = "FactorBased")]
		public BatchSizeReducerType BatchBatchSizeReducer
		{
			get
			{
				return base.GetConfig<BatchSizeReducerType>("BatchBatchSizeReducer");
			}
			set
			{
				this.InternalSetConfig<BatchSizeReducerType>(value, "BatchBatchSizeReducer");
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000074BD File Offset: 0x000056BD
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000074CA File Offset: 0x000056CA
		[ConfigurationProperty("BuildLocalCacheOnStartup", DefaultValue = true)]
		public bool BuildLocalCacheOnStartup
		{
			get
			{
				return base.GetConfig<bool>("BuildLocalCacheOnStartup");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "BuildLocalCacheOnStartup");
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000074D8 File Offset: 0x000056D8
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x000074E5 File Offset: 0x000056E5
		[IntegerValidator(ExcludeRange = false, MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("CapacityGrowthPeriods", DefaultValue = 6)]
		public int CapacityGrowthPeriods
		{
			get
			{
				return base.GetConfig<int>("CapacityGrowthPeriods");
			}
			set
			{
				this.InternalSetConfig<int>(value, "CapacityGrowthPeriods");
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000074F3 File Offset: 0x000056F3
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00007500 File Offset: 0x00005700
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "00:30:00", ExcludeRange = false)]
		[ConfigurationProperty("ClientCacheTimeToLive", DefaultValue = "00:00:00")]
		public TimeSpan ClientCacheTimeToLive
		{
			get
			{
				return base.GetConfig<TimeSpan>("ClientCacheTimeToLive");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ClientCacheTimeToLive");
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000750E File Offset: 0x0000570E
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000751B File Offset: 0x0000571B
		[ConfigurationProperty("ConsumerGrowthRate", DefaultValue = 3)]
		[IntegerValidator(ExcludeRange = false, MinValue = 0, MaxValue = 100)]
		public int ConsumerGrowthRate
		{
			get
			{
				return base.GetConfig<int>("ConsumerGrowthRate");
			}
			set
			{
				this.InternalSetConfig<int>(value, "ConsumerGrowthRate");
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00007529 File Offset: 0x00005729
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00007536 File Offset: 0x00005736
		[LongValidator(ExcludeRange = false, MinValue = 0L, MaxValue = 2147483647L)]
		[ConfigurationProperty("DefaultDatabaseMaxSizeGb", DefaultValue = 500L)]
		public long DefaultDatabaseMaxSizeGb
		{
			get
			{
				return base.GetConfig<long>("DefaultDatabaseMaxSizeGb");
			}
			set
			{
				this.InternalSetConfig<long>(value, "DefaultDatabaseMaxSizeGb");
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00007544 File Offset: 0x00005744
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00007551 File Offset: 0x00005751
		[IntegerValidator(ExcludeRange = false, MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("DefaultDatabaseRelativeLoadCapacity", DefaultValue = 180)]
		public int DefaultDatabaseRelativeLoadCapacity
		{
			get
			{
				return base.GetConfig<int>("DefaultDatabaseRelativeLoadCapacity");
			}
			set
			{
				this.InternalSetConfig<int>(value, "DefaultDatabaseRelativeLoadCapacity");
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000755F File Offset: 0x0000575F
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000756C File Offset: 0x0000576C
		[ConfigurationProperty("DisableWlm", DefaultValue = false)]
		public bool DisableWlm
		{
			get
			{
				return base.GetConfig<bool>("DisableWlm");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "DisableWlm");
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000757A File Offset: 0x0000577A
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00007587 File Offset: 0x00005787
		[ConfigurationProperty("DontCreateMoveRequests", DefaultValue = false)]
		public bool DontCreateMoveRequests
		{
			get
			{
				return base.GetConfig<bool>("DontCreateMoveRequests");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "DontCreateMoveRequests");
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00007595 File Offset: 0x00005795
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000075A2 File Offset: 0x000057A2
		[ConfigurationProperty("DontRemoveSoftDeletedMailboxes", DefaultValue = false)]
		public bool DontRemoveSoftDeletedMailboxes
		{
			get
			{
				return base.GetConfig<bool>("DontRemoveSoftDeletedMailboxes");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "DontRemoveSoftDeletedMailboxes");
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000075B0 File Offset: 0x000057B0
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000075BD File Offset: 0x000057BD
		[ConfigurationProperty("ExcludedMailboxProcessors", DefaultValue = "")]
		public string ExcludedMailboxProcessors
		{
			get
			{
				return base.GetConfig<string>("ExcludedMailboxProcessors");
			}
			set
			{
				this.InternalSetConfig<string>(value, "ExcludedMailboxProcessors");
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000075CB File Offset: 0x000057CB
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000075D8 File Offset: 0x000057D8
		[IntegerValidator(ExcludeRange = false, MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("InjectionBatchSize", DefaultValue = 500)]
		public int InjectionBatchSize
		{
			get
			{
				return base.GetConfig<int>("InjectionBatchSize");
			}
			set
			{
				this.InternalSetConfig<int>(value, "InjectionBatchSize");
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000075E6 File Offset: 0x000057E6
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00007600 File Offset: 0x00005800
		[ConfigurationProperty("IsEnabled", DefaultValue = false)]
		public new bool IsEnabled
		{
			get
			{
				return base.GetConfig<bool>("IsEnabled") && !this.LoadBalanceBlocked;
			}
			set
			{
				this.InternalSetConfig<bool>(value, "IsEnabled");
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00007610 File Offset: 0x00005810
		public bool LoadBalanceBlocked
		{
			get
			{
				return !VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Mrs.AutomaticMailboxLoadBalancing.Enabled;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000763E File Offset: 0x0000583E
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000764B File Offset: 0x0000584B
		[TimeSpanValidator]
		[ConfigurationProperty("LocalCacheRefreshPeriod", DefaultValue = "06:00:00")]
		public TimeSpan LocalCacheRefreshPeriod
		{
			get
			{
				return base.GetConfig<TimeSpan>("LocalCacheRefreshPeriod");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "LocalCacheRefreshPeriod");
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00007659 File Offset: 0x00005859
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00007666 File Offset: 0x00005866
		[ConfigurationProperty("MailboxProcessorsEnabled", DefaultValue = true)]
		public bool MailboxProcessorsEnabled
		{
			get
			{
				return base.GetConfig<bool>("MailboxProcessorsEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "MailboxProcessorsEnabled");
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00007674 File Offset: 0x00005874
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00007681 File Offset: 0x00005881
		[ConfigurationProperty("MaxDatabaseDiskUtilizationPercent", DefaultValue = 15)]
		[IntegerValidator(ExcludeRange = false, MinValue = 0, MaxValue = 100)]
		public int MaxDatabaseDiskUtilizationPercent
		{
			get
			{
				return base.GetConfig<int>("MaxDatabaseDiskUtilizationPercent");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxDatabaseDiskUtilizationPercent");
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000768F File Offset: 0x0000588F
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000769C File Offset: 0x0000589C
		[LongValidator(ExcludeRange = false, MinValue = 0L, MaxValue = 2147483647L)]
		[ConfigurationProperty("MaximumAmountOfDataPerRoundGb", DefaultValue = 450L)]
		public long MaximumAmountOfDataPerRoundGb
		{
			get
			{
				return base.GetConfig<long>("MaximumAmountOfDataPerRoundGb");
			}
			set
			{
				this.InternalSetConfig<long>(value, "MaximumAmountOfDataPerRoundGb");
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000076AA File Offset: 0x000058AA
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x000076B7 File Offset: 0x000058B7
		[ConfigurationProperty("MaximumBatchSizeGb", DefaultValue = 5120L)]
		[LongValidator(ExcludeRange = false, MinValue = 0L, MaxValue = 2147483647L)]
		public long MaximumBatchSizeGb
		{
			get
			{
				return base.GetConfig<long>("MaximumBatchSizeGb");
			}
			set
			{
				this.InternalSetConfig<long>(value, "MaximumBatchSizeGb");
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000076C5 File Offset: 0x000058C5
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x000076D2 File Offset: 0x000058D2
		[ConfigurationProperty("MaximumConsumerMailboxSizePercent", DefaultValue = 21)]
		[IntegerValidator(ExcludeRange = false, MinValue = 0, MaxValue = 100)]
		public int MaximumConsumerMailboxSizePercent
		{
			get
			{
				return base.GetConfig<int>("MaximumConsumerMailboxSizePercent");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaximumConsumerMailboxSizePercent");
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x000076E0 File Offset: 0x000058E0
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x000076ED File Offset: 0x000058ED
		[ConfigurationProperty("MaximumNumberOfRunspaces", DefaultValue = 5)]
		[IntegerValidator(ExcludeRange = false, MinValue = 0, MaxValue = 100)]
		public int MaximumNumberOfRunspaces
		{
			get
			{
				return base.GetConfig<int>("MaximumNumberOfRunspaces");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaximumNumberOfRunspaces");
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x000076FB File Offset: 0x000058FB
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00007709 File Offset: 0x00005909
		[LongValidator(ExcludeRange = false, MinValue = 0L, MaxValue = 2147483647L)]
		[ConfigurationProperty("MaximumPendingMoveCount", DefaultValue = 1000L)]
		public long MaximumPendingMoveCount
		{
			get
			{
				return (long)base.GetConfig<int>("MaximumPendingMoveCount");
			}
			set
			{
				this.InternalSetConfig<long>(value, "MaximumPendingMoveCount");
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00007717 File Offset: 0x00005917
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00007724 File Offset: 0x00005924
		[ConfigurationProperty("MinimumSoftDeletedMailboxCleanupAge", DefaultValue = "7.00:00:00")]
		[TimeSpanValidator(MinValueString = "7.00:00:00", MaxValueString = "365.00:00:00", ExcludeRange = false)]
		public TimeSpan MinimumSoftDeletedMailboxCleanupAge
		{
			get
			{
				return base.GetConfig<TimeSpan>("MinimumSoftDeletedMailboxCleanupAge");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MinimumSoftDeletedMailboxCleanupAge");
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00007732 File Offset: 0x00005932
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00007748 File Offset: 0x00005948
		[ConfigurationProperty("NonMovableOrganizationIds")]
		public string NonMovableOrganizationIds
		{
			get
			{
				return base.GetConfig<string>("NonMovableOrganizationIds") ?? string.Empty;
			}
			set
			{
				this.InternalSetConfig<string>(value, "NonMovableOrganizationIds");
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00007756 File Offset: 0x00005956
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00007763 File Offset: 0x00005963
		[ConfigurationProperty("OrganizationGrowthRate", DefaultValue = 9)]
		[IntegerValidator(ExcludeRange = false, MinValue = 0, MaxValue = 100)]
		public int OrganizationGrowthRate
		{
			get
			{
				return base.GetConfig<int>("OrganizationGrowthRate");
			}
			set
			{
				this.InternalSetConfig<int>(value, "OrganizationGrowthRate");
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00007771 File Offset: 0x00005971
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000777E File Offset: 0x0000597E
		[ConfigurationProperty("QueryBufferPeriodDays", DefaultValue = 10)]
		[IntegerValidator(ExcludeRange = false, MinValue = 1, MaxValue = 2147483647)]
		public int QueryBufferPeriodDays
		{
			get
			{
				return base.GetConfig<int>("QueryBufferPeriodDays");
			}
			set
			{
				this.InternalSetConfig<int>(value, "QueryBufferPeriodDays");
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000778C File Offset: 0x0000598C
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00007799 File Offset: 0x00005999
		[ConfigurationProperty("ReservedCapacityInGb", DefaultValue = 1024)]
		[IntegerValidator(ExcludeRange = false, MinValue = 0, MaxValue = 2147483647)]
		public int ReservedCapacityInGb
		{
			get
			{
				return base.GetConfig<int>("ReservedCapacityInGb");
			}
			set
			{
				this.InternalSetConfig<int>(value, "ReservedCapacityInGb");
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000077A7 File Offset: 0x000059A7
		// (set) Token: 0x06000205 RID: 517 RVA: 0x000077B4 File Offset: 0x000059B4
		[ConfigurationProperty("SoftDeletedCleanupEnabled", DefaultValue = false)]
		public bool SoftDeletedCleanupEnabled
		{
			get
			{
				return base.GetConfig<bool>("SoftDeletedCleanupEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "SoftDeletedCleanupEnabled");
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000077C2 File Offset: 0x000059C2
		// (set) Token: 0x06000207 RID: 519 RVA: 0x000077CF File Offset: 0x000059CF
		[ConfigurationProperty("SoftDeletedCleanupThreshold", DefaultValue = 80)]
		[IntegerValidator(ExcludeRange = false, MinValue = 0, MaxValue = 100)]
		public int SoftDeletedCleanupThreshold
		{
			get
			{
				return base.GetConfig<int>("SoftDeletedCleanupThreshold");
			}
			set
			{
				this.InternalSetConfig<int>(value, "SoftDeletedCleanupThreshold");
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000077DD File Offset: 0x000059DD
		// (set) Token: 0x06000209 RID: 521 RVA: 0x000077EA File Offset: 0x000059EA
		[ConfigurationProperty("TransientFailureMaxRetryCount", DefaultValue = 5)]
		[IntegerValidator(ExcludeRange = false, MinValue = 0, MaxValue = 2147483647)]
		public int TransientFailureMaxRetryCount
		{
			get
			{
				return base.GetConfig<int>("TransientFailureMaxRetryCount");
			}
			set
			{
				this.InternalSetConfig<int>(value, "TransientFailureMaxRetryCount");
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000077F8 File Offset: 0x000059F8
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00007805 File Offset: 0x00005A05
		[ConfigurationProperty("TransientFailureRetryDelay", DefaultValue = "00:00:30")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "00:30:00", ExcludeRange = false)]
		public TimeSpan TransientFailureRetryDelay
		{
			get
			{
				return base.GetConfig<TimeSpan>("TransientFailureRetryDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "TransientFailureRetryDelay");
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00007813 File Offset: 0x00005A13
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00007820 File Offset: 0x00005A20
		[ConfigurationProperty("UseCachingActiveManager", DefaultValue = true)]
		public bool UseCachingActiveManager
		{
			get
			{
				return base.GetConfig<bool>("UseCachingActiveManager");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "UseCachingActiveManager");
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000782E File Offset: 0x00005A2E
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000783B File Offset: 0x00005A3B
		[ConfigurationProperty("UseDatabaseSelectorForMoveInjection", DefaultValue = true)]
		public bool UseDatabaseSelectorForMoveInjection
		{
			get
			{
				return base.GetConfig<bool>("UseDatabaseSelectorForMoveInjection");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "UseDatabaseSelectorForMoveInjection");
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00007849 File Offset: 0x00005A49
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00007856 File Offset: 0x00005A56
		[ConfigurationProperty("UseHeatMapProvisioning", DefaultValue = false)]
		public bool UseHeatMapProvisioning
		{
			get
			{
				return base.GetConfig<bool>("UseHeatMapProvisioning");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "UseHeatMapProvisioning");
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00007864 File Offset: 0x00005A64
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00007871 File Offset: 0x00005A71
		[ConfigurationProperty("UseParallelDiscovery", DefaultValue = true)]
		public bool UseParallelDiscovery
		{
			get
			{
				return base.GetConfig<bool>("UseParallelDiscovery");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "UseParallelDiscovery");
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000787F File Offset: 0x00005A7F
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000788C File Offset: 0x00005A8C
		[IntegerValidator(ExcludeRange = false, MinValue = 0, MaxValue = 100)]
		[ConfigurationProperty("WeightDeviationPercent", DefaultValue = 2)]
		public int WeightDeviationPercent
		{
			get
			{
				return base.GetConfig<int>("WeightDeviationPercent");
			}
			set
			{
				this.InternalSetConfig<int>(value, "WeightDeviationPercent");
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000789A File Offset: 0x00005A9A
		// (set) Token: 0x06000217 RID: 535 RVA: 0x000078A7 File Offset: 0x00005AA7
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "365.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("MinimumTimeInDatabaseForItemUpgrade", DefaultValue = "14.00:00:00")]
		public TimeSpan MinimumTimeInDatabaseForItemUpgrade
		{
			get
			{
				return base.GetConfig<TimeSpan>("MinimumTimeInDatabaseForItemUpgrade");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MinimumTimeInDatabaseForItemUpgrade");
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000078B5 File Offset: 0x00005AB5
		// (set) Token: 0x06000219 RID: 537 RVA: 0x000078C2 File Offset: 0x00005AC2
		[ConfigurationProperty("DisabledMailboxPolicies", DefaultValue = "")]
		public string DisabledMailboxPolicies
		{
			get
			{
				return base.GetConfig<string>("DisabledMailboxPolicies");
			}
			set
			{
				this.InternalSetConfig<string>(value, "DisabledMailboxPolicies");
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000078EC File Offset: 0x00005AEC
		TimeSpan ILoadBalanceSettings.get_IdleRunDelay()
		{
			return base.IdleRunDelay;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000078F4 File Offset: 0x00005AF4
		string ILoadBalanceSettings.get_LogFilePath()
		{
			return base.LogFilePath;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000078FC File Offset: 0x00005AFC
		long ILoadBalanceSettings.get_LogMaxDirectorySize()
		{
			return base.LogMaxDirectorySize;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007904 File Offset: 0x00005B04
		long ILoadBalanceSettings.get_LogMaxFileSize()
		{
			return base.LogMaxFileSize;
		}

		// Token: 0x04000099 RID: 153
		public static readonly LoadBalanceADSettings DefaultContext = new LoadBalanceADSettings();

		// Token: 0x0400009A RID: 154
		public static readonly Hookable<ILoadBalanceSettings> Instance = Hookable<ILoadBalanceSettings>.Create(true, LoadBalanceADSettings.DefaultContext);
	}
}

using System;
using System.Configuration;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x02000048 RID: 72
	internal class CacheConfiguration : ConfigurationSection
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000907E File Offset: 0x0000727E
		public static string SectionName
		{
			get
			{
				return "CacheConfiguration";
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00009085 File Offset: 0x00007285
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000908C File Offset: 0x0000728C
		public static CacheConfiguration Instance
		{
			get
			{
				return CacheConfiguration.instance;
			}
			internal set
			{
				CacheConfiguration.instance = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00009094 File Offset: 0x00007294
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x000090A6 File Offset: 0x000072A6
		[ConfigurationProperty("MaxRetryCount", IsRequired = false, DefaultValue = 10)]
		public int MaxRetryCount
		{
			get
			{
				return (int)base["MaxRetryCount"];
			}
			set
			{
				base["MaxRetryCount"] = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000090B9 File Offset: 0x000072B9
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x000090CB File Offset: 0x000072CB
		[ConfigurationProperty("RetrySleepInterval", IsRequired = false, DefaultValue = "00:00:00.250")]
		public TimeSpan RetrySleepInterval
		{
			get
			{
				return (TimeSpan)base["RetrySleepInterval"];
			}
			set
			{
				base["RetrySleepInterval"] = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x000090DE File Offset: 0x000072DE
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x000090F0 File Offset: 0x000072F0
		[ConfigurationProperty("SlowResponseTime", IsRequired = false, DefaultValue = "00:00:03")]
		public TimeSpan SlowResponseTime
		{
			get
			{
				return (TimeSpan)base["SlowResponseTime"];
			}
			set
			{
				base["SlowResponseTime"] = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00009103 File Offset: 0x00007303
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x00009115 File Offset: 0x00007315
		[ConfigurationProperty("WatchdogTimeout", IsRequired = false, DefaultValue = "00:00:05")]
		public TimeSpan WatchdogTimeout
		{
			get
			{
				return (TimeSpan)base["WatchdogTimeout"];
			}
			set
			{
				base["WatchdogTimeout"] = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00009128 File Offset: 0x00007328
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000913A File Offset: 0x0000733A
		[ConfigurationProperty("HealthyToStaleThreshold", IsRequired = false, DefaultValue = "00:05:00")]
		public TimeSpan HealthyToStaleThreshold
		{
			get
			{
				return (TimeSpan)base["HealthyToStaleThreshold"];
			}
			set
			{
				base["HealthyToStaleThreshold"] = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000914D File Offset: 0x0000734D
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000915F File Offset: 0x0000735F
		[ConfigurationProperty("StaleToUnhealthyThreshold", IsRequired = false, DefaultValue = "00:20:00")]
		public TimeSpan StaleToUnhealthyThreshold
		{
			get
			{
				return (TimeSpan)base["StaleToUnhealthyThreshold"];
			}
			set
			{
				base["StaleToUnhealthyThreshold"] = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00009172 File Offset: 0x00007372
		// (set) Token: 0x060002BD RID: 701 RVA: 0x00009184 File Offset: 0x00007384
		[ConfigurationProperty("FailoverModeForPrimingState", IsRequired = false, DefaultValue = CacheFailoverMode.CacheThenDatabase)]
		public CacheFailoverMode FailoverModeForPrimingState
		{
			get
			{
				return (CacheFailoverMode)base["FailoverModeForPrimingState"];
			}
			set
			{
				base["FailoverModeForPrimingState"] = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00009197 File Offset: 0x00007397
		// (set) Token: 0x060002BF RID: 703 RVA: 0x000091A9 File Offset: 0x000073A9
		[ConfigurationProperty("FailoverModeForStaleState", IsRequired = false, DefaultValue = CacheFailoverMode.CacheThenDatabase)]
		public CacheFailoverMode FailoverModeForStaleState
		{
			get
			{
				return (CacheFailoverMode)base["FailoverModeForStaleState"];
			}
			set
			{
				base["FailoverModeForStaleState"] = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x000091BC File Offset: 0x000073BC
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x000091CE File Offset: 0x000073CE
		[ConfigurationProperty("FailoverModeForUnhealthyState", IsRequired = false, DefaultValue = CacheFailoverMode.CacheThenDatabase)]
		public CacheFailoverMode FailoverModeForUnhealthyState
		{
			get
			{
				return (CacheFailoverMode)base["FailoverModeForUnhealthyState"];
			}
			set
			{
				base["FailoverModeForUnhealthyState"] = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x000091E1 File Offset: 0x000073E1
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x000091F3 File Offset: 0x000073F3
		[ConfigurationProperty("FailoverModeForHealthyState", IsRequired = false, DefaultValue = CacheFailoverMode.CacheThenDatabase)]
		public CacheFailoverMode FailoverModeForHealthyState
		{
			get
			{
				return (CacheFailoverMode)base["FailoverModeForHealthyState"];
			}
			set
			{
				base["FailoverModeForHealthyState"] = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00009206 File Offset: 0x00007406
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00009218 File Offset: 0x00007418
		[ConfigurationProperty("FailoverModeForPrimingWithFileState", IsRequired = false, DefaultValue = CacheFailoverMode.DatabaseOnly)]
		public CacheFailoverMode FailoverModeForPrimingWithFileState
		{
			get
			{
				return (CacheFailoverMode)base["FailoverModeForPrimingWithFileState"];
			}
			set
			{
				base["FailoverModeForPrimingWithFileState"] = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000922B File Offset: 0x0000742B
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000923D File Offset: 0x0000743D
		[ConfigurationProperty("FailoverToCacheOnPermanentDALException", IsRequired = false, DefaultValue = true)]
		public bool FailoverToCacheOnPermanentDALException
		{
			get
			{
				return (bool)base["FailoverToCacheOnPermanentDALException"];
			}
			set
			{
				base["FailoverToCacheOnPermanentDALException"] = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00009250 File Offset: 0x00007450
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00009262 File Offset: 0x00007462
		[ConfigurationProperty("FailoverToCacheOnTransientDALException", IsRequired = false, DefaultValue = true)]
		public bool FailoverToCacheOnTransientDALException
		{
			get
			{
				return (bool)base["FailoverToCacheOnTransientDALException"];
			}
			set
			{
				base["FailoverToCacheOnTransientDALException"] = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00009275 File Offset: 0x00007475
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00009287 File Offset: 0x00007487
		[ConfigurationProperty("CachePrimingInfoThreadInterval", IsRequired = false, DefaultValue = "00:00:01")]
		public TimeSpan CachePrimingInfoThreadInterval
		{
			get
			{
				return (TimeSpan)base["CachePrimingInfoThreadInterval"];
			}
			set
			{
				base["CachePrimingInfoThreadInterval"] = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000929A File Offset: 0x0000749A
		// (set) Token: 0x060002CD RID: 717 RVA: 0x000092AC File Offset: 0x000074AC
		[ConfigurationProperty("CachePrimingInfoThreadExpiration", IsRequired = false, DefaultValue = "00:00:10")]
		public TimeSpan CachePrimingInfoThreadExpiration
		{
			get
			{
				return (TimeSpan)base["CachePrimingInfoThreadExpiration"];
			}
			set
			{
				base["CachePrimingInfoThreadExpiration"] = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000092BF File Offset: 0x000074BF
		// (set) Token: 0x060002CF RID: 719 RVA: 0x000092D1 File Offset: 0x000074D1
		[ConfigurationProperty("TracerTokensEnabled", IsRequired = false, DefaultValue = false)]
		public bool TracerTokensEnabled
		{
			get
			{
				return (bool)base["TracerTokensEnabled"];
			}
			set
			{
				base["TracerTokensEnabled"] = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x000092E4 File Offset: 0x000074E4
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x000092F6 File Offset: 0x000074F6
		[ConfigurationProperty("TracerTokenAgeThreshold", IsRequired = false, DefaultValue = "00:02:00")]
		public TimeSpan TracerTokenAgeThreshold
		{
			get
			{
				return (TimeSpan)base["TracerTokenAgeThreshold"];
			}
			set
			{
				base["TracerTokenAgeThreshold"] = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00009309 File Offset: 0x00007509
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000931B File Offset: 0x0000751B
		[ConfigurationProperty("NamedRegionEnabled", IsRequired = false, DefaultValue = false)]
		public bool NamedRegionEnabled
		{
			get
			{
				return (bool)base["NamedRegionEnabled"];
			}
			set
			{
				base["NamedRegionEnabled"] = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000932E File Offset: 0x0000752E
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00009340 File Offset: 0x00007540
		[ConfigurationProperty("StatisticBasedFailoverEnabled", IsRequired = false, DefaultValue = false)]
		public bool StatisticBasedFailoverEnabled
		{
			get
			{
				return (bool)base["StatisticBasedFailoverEnabled"];
			}
			set
			{
				base["StatisticBasedFailoverEnabled"] = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00009353 File Offset: 0x00007553
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x00009365 File Offset: 0x00007565
		[LongValidator(MinValue = 0L)]
		[ConfigurationProperty("MinimumLogicalOperationThreshold", IsRequired = false, DefaultValue = 100L)]
		public long MinimumLogicalOperationThreshold
		{
			get
			{
				return (long)base["MinimumLogicalOperationThreshold"];
			}
			set
			{
				base["MinimumLogicalOperationThreshold"] = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00009378 File Offset: 0x00007578
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000938A File Offset: 0x0000758A
		[ConfigurationProperty("StatisticsSlidingWindowDuration", IsRequired = false, DefaultValue = "00:15:00")]
		public TimeSpan StatisticsSlidingWindowDuration
		{
			get
			{
				return (TimeSpan)base["StatisticsSlidingWindowDuration"];
			}
			set
			{
				base["StatisticsSlidingWindowDuration"] = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000939D File Offset: 0x0000759D
		// (set) Token: 0x060002DB RID: 731 RVA: 0x000093AF File Offset: 0x000075AF
		[ConfigurationProperty("StatisticsBucketDuration", IsRequired = false, DefaultValue = "00:01:00")]
		public TimeSpan StatisticsBucketDuration
		{
			get
			{
				return (TimeSpan)base["StatisticsBucketDuration"];
			}
			set
			{
				base["StatisticsBucketDuration"] = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060002DC RID: 732 RVA: 0x000093C2 File Offset: 0x000075C2
		// (set) Token: 0x060002DD RID: 733 RVA: 0x000093D4 File Offset: 0x000075D4
		[ConfigurationProperty("StatisticsRefreshInterval", IsRequired = false, DefaultValue = "00:00:30")]
		public TimeSpan StatisticsRefreshInterval
		{
			get
			{
				return (TimeSpan)base["StatisticsRefreshInterval"];
			}
			set
			{
				base["StatisticsRefreshInterval"] = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060002DE RID: 734 RVA: 0x000093E7 File Offset: 0x000075E7
		// (set) Token: 0x060002DF RID: 735 RVA: 0x000093F9 File Offset: 0x000075F9
		[LongValidator(MinValue = 0L, MaxValue = 100L)]
		[ConfigurationProperty("SlowResponsePercentageFailoverThreshold", IsRequired = false, DefaultValue = 50L)]
		public long SlowResponsePercentageFailoverThreshold
		{
			get
			{
				return (long)base["SlowResponsePercentageFailoverThreshold"];
			}
			set
			{
				base["SlowResponsePercentageFailoverThreshold"] = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000940C File Offset: 0x0000760C
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0000941E File Offset: 0x0000761E
		[LongValidator(MinValue = 0L, MaxValue = 100L)]
		[ConfigurationProperty("LogicalOperationFailurePercentageFailoverThreshold", IsRequired = false, DefaultValue = 5L)]
		public long LogicalOperationFailurePercentageFailoverThreshold
		{
			get
			{
				return (long)base["LogicalOperationFailurePercentageFailoverThreshold"];
			}
			set
			{
				base["LogicalOperationFailurePercentageFailoverThreshold"] = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00009431 File Offset: 0x00007631
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00009443 File Offset: 0x00007643
		[LongValidator(MinValue = 0L, MaxValue = 100L)]
		[ConfigurationProperty("PhysicalOperationFailurePercentageFailoverThreshold", IsRequired = false, DefaultValue = 25L)]
		public long PhysicalOperationFailurePercentageFailoverThreshold
		{
			get
			{
				return (long)base["PhysicalOperationFailurePercentageFailoverThreshold"];
			}
			set
			{
				base["PhysicalOperationFailurePercentageFailoverThreshold"] = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00009456 File Offset: 0x00007656
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00009468 File Offset: 0x00007668
		[ConfigurationProperty("NotificationsEnabled", IsRequired = false, DefaultValue = false)]
		public bool NotificationsEnabled
		{
			get
			{
				return (bool)base["NotificationsEnabled"];
			}
			set
			{
				base["NotificationsEnabled"] = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000947B File Offset: 0x0000767B
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000948D File Offset: 0x0000768D
		[ConfigurationProperty("BloomFilterMode", IsRequired = false, DefaultValue = BloomFilterMode.Disabled)]
		public BloomFilterMode BloomFilterMode
		{
			get
			{
				return (BloomFilterMode)base["BloomFilterMode"];
			}
			set
			{
				base["BloomFilterMode"] = value;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x000094A0 File Offset: 0x000076A0
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x000094B2 File Offset: 0x000076B2
		[ConfigurationProperty("BloomFilterUpdateFrequency", IsRequired = false, DefaultValue = "00:05:00")]
		public TimeSpan BloomFilterUpdateFrequency
		{
			get
			{
				return (TimeSpan)base["BloomFilterUpdateFrequency"];
			}
			set
			{
				base["BloomFilterUpdateFrequency"] = value;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060002EA RID: 746 RVA: 0x000094C5 File Offset: 0x000076C5
		// (set) Token: 0x060002EB RID: 747 RVA: 0x000094D7 File Offset: 0x000076D7
		[ConfigurationProperty("TenantConfigurationCacheMode", IsRequired = false, DefaultValue = TenantConfigurationCacheMode.Disabled)]
		public TenantConfigurationCacheMode TenantConfigurationCacheMode
		{
			get
			{
				return (TenantConfigurationCacheMode)base["TenantConfigurationCacheMode"];
			}
			set
			{
				base["TenantConfigurationCacheMode"] = value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000094EA File Offset: 0x000076EA
		// (set) Token: 0x060002ED RID: 749 RVA: 0x000094FC File Offset: 0x000076FC
		[ConfigurationProperty("EntityCacheConfiguration", IsRequired = false)]
		public EntityCacheConfigurationCollection EntityCacheConfigurations
		{
			get
			{
				return (EntityCacheConfigurationCollection)base["EntityCacheConfiguration"];
			}
			set
			{
				base["EntityCacheConfiguration"] = value;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000950A File Offset: 0x0000770A
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000951C File Offset: 0x0000771C
		[ConfigurationProperty("VelocityDisabled", IsRequired = false, DefaultValue = false)]
		public bool VelocityDisabled
		{
			get
			{
				return (bool)base["VelocityDisabled"];
			}
			set
			{
				base["VelocityDisabled"] = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000952F File Offset: 0x0000772F
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x00009541 File Offset: 0x00007741
		[ConfigurationProperty("BloomFilterTracerTokenGranularity", IsRequired = false, DefaultValue = 15)]
		public int BloomFilterTracerTokenGranularity
		{
			get
			{
				return (int)base["BloomFilterTracerTokenGranularity"];
			}
			set
			{
				base["BloomFilterTracerTokenGranularity"] = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00009554 File Offset: 0x00007754
		public bool BloomFilterTracerTokensEnabled
		{
			get
			{
				return (int)base["BloomFilterTracerTokenGranularity"] > 0;
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000956C File Offset: 0x0000776C
		public string GetBloomFilterTracerToken(string entityName, DateTime referenceDateTime)
		{
			int bloomFilterTracerTokenGranularity = this.BloomFilterTracerTokenGranularity;
			return string.Format("{0}-{1:yyyyMMddHH}{2:D2}", entityName, referenceDateTime, (int)(Math.Floor((double)referenceDateTime.Minute / (double)bloomFilterTracerTokenGranularity) * (double)bloomFilterTracerTokenGranularity));
		}

		// Token: 0x040001D2 RID: 466
		private const string MaxRetryCountKey = "MaxRetryCount";

		// Token: 0x040001D3 RID: 467
		private const string RetrySleepIntervalKey = "RetrySleepInterval";

		// Token: 0x040001D4 RID: 468
		private const string SlowResponseTimeKey = "SlowResponseTime";

		// Token: 0x040001D5 RID: 469
		private const string WatchdogTimeoutKey = "WatchdogTimeout";

		// Token: 0x040001D6 RID: 470
		private const string HealthyToStaleThresholdKey = "HealthyToStaleThreshold";

		// Token: 0x040001D7 RID: 471
		private const string StaleToUnhealthyThresholdKey = "StaleToUnhealthyThreshold";

		// Token: 0x040001D8 RID: 472
		private const string FailoverModeForPrimingStateKey = "FailoverModeForPrimingState";

		// Token: 0x040001D9 RID: 473
		private const string FailoverModeForStaleStateKey = "FailoverModeForStaleState";

		// Token: 0x040001DA RID: 474
		private const string FailoverModeForUnhealthyStateKey = "FailoverModeForUnhealthyState";

		// Token: 0x040001DB RID: 475
		private const string FailoverModeForHealthyStateKey = "FailoverModeForHealthyState";

		// Token: 0x040001DC RID: 476
		private const string FailoverModeForPrimingWithFileStateKey = "FailoverModeForPrimingWithFileState";

		// Token: 0x040001DD RID: 477
		private const string FailoverToCacheOnPermanentDALExceptionKey = "FailoverToCacheOnPermanentDALException";

		// Token: 0x040001DE RID: 478
		private const string FailoverToCacheOnTransientDALExceptionKey = "FailoverToCacheOnTransientDALException";

		// Token: 0x040001DF RID: 479
		private const string CachePrimingInfoThreadIntervalKey = "CachePrimingInfoThreadInterval";

		// Token: 0x040001E0 RID: 480
		private const string CachePrimingInfoThreadExpirationKey = "CachePrimingInfoThreadExpiration";

		// Token: 0x040001E1 RID: 481
		private const string TracerTokensEnabledKey = "TracerTokensEnabled";

		// Token: 0x040001E2 RID: 482
		private const string TracerTokenAgeThresholdKey = "TracerTokenAgeThreshold";

		// Token: 0x040001E3 RID: 483
		private const string NamedRegionEnabledKey = "NamedRegionEnabled";

		// Token: 0x040001E4 RID: 484
		private const string StatisticBasedFailoverEnabledKey = "StatisticBasedFailoverEnabled";

		// Token: 0x040001E5 RID: 485
		private const string MinimumLogicalOperationThresholdKey = "MinimumLogicalOperationThreshold";

		// Token: 0x040001E6 RID: 486
		private const string StatisticsSlidingWindowDurationKey = "StatisticsSlidingWindowDuration";

		// Token: 0x040001E7 RID: 487
		private const string StatisticsBucketDurationKey = "StatisticsBucketDuration";

		// Token: 0x040001E8 RID: 488
		private const string StatisticsRefreshIntervalKey = "StatisticsRefreshInterval";

		// Token: 0x040001E9 RID: 489
		private const string SlowResponsePercentageFailoverThresholdKey = "SlowResponsePercentageFailoverThreshold";

		// Token: 0x040001EA RID: 490
		private const string LogicalOperationFailurePercentageFailoverThresholdKey = "LogicalOperationFailurePercentageFailoverThreshold";

		// Token: 0x040001EB RID: 491
		private const string PhysicalOperationFailurePercentageFailoverThresholdKey = "PhysicalOperationFailurePercentageFailoverThreshold";

		// Token: 0x040001EC RID: 492
		private const string NotificationsEnabledKey = "NotificationsEnabled";

		// Token: 0x040001ED RID: 493
		private const string BloomFilterModeKey = "BloomFilterMode";

		// Token: 0x040001EE RID: 494
		private const string BloomFilterUpdateFrequencyKey = "BloomFilterUpdateFrequency";

		// Token: 0x040001EF RID: 495
		private const string TenantConfigurationCacheModeKey = "TenantConfigurationCacheMode";

		// Token: 0x040001F0 RID: 496
		private const string EntityCacheConfigurationCollectionKey = "EntityCacheConfiguration";

		// Token: 0x040001F1 RID: 497
		private const string VelocityDisabledKey = "VelocityDisabled";

		// Token: 0x040001F2 RID: 498
		private const string BloomFilterTracerTokenGranularityKey = "BloomFilterTracerTokenGranularity";

		// Token: 0x040001F3 RID: 499
		private static CacheConfiguration instance = ((CacheConfiguration)ConfigurationManager.GetSection(CacheConfiguration.SectionName)) ?? new CacheConfiguration();
	}
}

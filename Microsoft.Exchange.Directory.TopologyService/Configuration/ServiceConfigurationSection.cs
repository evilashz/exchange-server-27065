using System;
using System.Configuration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Directory.TopologyService.Configuration
{
	// Token: 0x0200002C RID: 44
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ServiceConfigurationSection : ConfigurationSection
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000BAB2 File Offset: 0x00009CB2
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		[ConfigurationProperty("MinimumPrefixMatch", IsRequired = false, DefaultValue = 2)]
		public int MinimumPrefixMatch
		{
			get
			{
				return (int)base["MinimumPrefixMatch"];
			}
			set
			{
				base["MinimumPrefixMatch"] = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000BAD7 File Offset: 0x00009CD7
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000BAE9 File Offset: 0x00009CE9
		[ConfigurationProperty("FullTopologyDiscoveryTimeout", IsRequired = false, DefaultValue = "00:05:00")]
		public TimeSpan FullTopologyDiscoveryTimeout
		{
			get
			{
				return (TimeSpan)base["FullTopologyDiscoveryTimeout"];
			}
			set
			{
				base["FullTopologyDiscoveryTimeout"] = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000BAFC File Offset: 0x00009CFC
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000BB0E File Offset: 0x00009D0E
		[ConfigurationProperty("UrgentOrInitialTopologyTimeout", IsRequired = false, DefaultValue = "00:05:00")]
		public TimeSpan UrgentOrInitialTopologyTimeout
		{
			get
			{
				return (TimeSpan)base["UrgentOrInitialTopologyTimeout"];
			}
			set
			{
				base["UrgentOrInitialTopologyTimeout"] = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000BB21 File Offset: 0x00009D21
		// (set) Token: 0x060001AC RID: 428 RVA: 0x0000BB33 File Offset: 0x00009D33
		[ConfigurationProperty("DiscoveryFrequency", IsRequired = false, DefaultValue = "00:15:00")]
		public TimeSpan DiscoveryFrequency
		{
			get
			{
				return (TimeSpan)base["DiscoveryFrequency"];
			}
			set
			{
				base["DiscoveryFrequency"] = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000BB46 File Offset: 0x00009D46
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000BB58 File Offset: 0x00009D58
		[ConfigurationProperty("DiscoveryFrequencyOnFailure", IsRequired = false, DefaultValue = "00:05:00")]
		public TimeSpan DiscoveryFrequencyOnFailure
		{
			get
			{
				return (TimeSpan)base["DiscoveryFrequencyOnFailure"];
			}
			set
			{
				base["DiscoveryFrequencyOnFailure"] = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000BB6B File Offset: 0x00009D6B
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x0000BB7D File Offset: 0x00009D7D
		[ConfigurationProperty("DiscoveryFrequencyOnNoTopology", IsRequired = false, DefaultValue = "00:00:30")]
		public TimeSpan DiscoveryFrequencyOnNoTopology
		{
			get
			{
				return (TimeSpan)base["DiscoveryFrequencyOnNoTopology"];
			}
			set
			{
				base["DiscoveryFrequencyOnNoTopology"] = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000BB90 File Offset: 0x00009D90
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000BBA2 File Offset: 0x00009DA2
		[ConfigurationProperty("DiscoveryFrequencyOnFailover", IsRequired = false, DefaultValue = "00:05:00")]
		[Obsolete]
		public TimeSpan DiscoveryFrequencyOnFailover
		{
			get
			{
				return (TimeSpan)base["DiscoveryFrequencyOnFailover"];
			}
			set
			{
				base["DiscoveryFrequencyOnFailover"] = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000BBB5 File Offset: 0x00009DB5
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x0000BBC7 File Offset: 0x00009DC7
		[ConfigurationProperty("DiscoveryFrequencyOnMinPercentageDC", IsRequired = false, DefaultValue = "00:05:00")]
		public TimeSpan DiscoveryFrequencyOnMinPercentageDC
		{
			get
			{
				return (TimeSpan)base["DiscoveryFrequencyOnMinPercentageDC"];
			}
			set
			{
				base["DiscoveryFrequencyOnMinPercentageDC"] = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000BBDA File Offset: 0x00009DDA
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x0000BBEC File Offset: 0x00009DEC
		[ConfigurationProperty("WaitTimeBetweenInitialAndFullDiscovery", IsRequired = false, DefaultValue = "00:00:30")]
		public TimeSpan WaitTimeBetweenInitialAndFullDiscovery
		{
			get
			{
				return (TimeSpan)base["WaitTimeBetweenInitialAndFullDiscovery"];
			}
			set
			{
				base["WaitTimeBetweenInitialAndFullDiscovery"] = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000BBFF File Offset: 0x00009DFF
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000BC11 File Offset: 0x00009E11
		[ConfigurationProperty("ForestScanFrequency", IsRequired = false, DefaultValue = "04:00:00")]
		public TimeSpan ForestScanFrequency
		{
			get
			{
				return (TimeSpan)base["ForestScanFrequency"];
			}
			set
			{
				base["ForestScanFrequency"] = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000BC24 File Offset: 0x00009E24
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000BC36 File Offset: 0x00009E36
		[ConfigurationProperty("ForestScanFrequencyOnFailure", IsRequired = false, DefaultValue = "00:30:00")]
		public TimeSpan ForestScanFrequencyOnFailure
		{
			get
			{
				return (TimeSpan)base["ForestScanFrequencyOnFailure"];
			}
			set
			{
				base["ForestScanFrequencyOnFailure"] = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000BC49 File Offset: 0x00009E49
		// (set) Token: 0x060001BC RID: 444 RVA: 0x0000BC5B File Offset: 0x00009E5B
		[ConfigurationProperty("ForestScanTimeout", IsRequired = false, DefaultValue = "00:05:00")]
		public TimeSpan ForestScanTimeout
		{
			get
			{
				return (TimeSpan)base["ForestScanTimeout"];
			}
			set
			{
				base["ForestScanTimeout"] = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000BC6E File Offset: 0x00009E6E
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000BC80 File Offset: 0x00009E80
		[ConfigurationProperty("SiteMonitorFrequency", IsRequired = false, DefaultValue = "00:15:00")]
		public TimeSpan SiteMonitorFrequency
		{
			get
			{
				return (TimeSpan)base["SiteMonitorFrequency"];
			}
			set
			{
				base["SiteMonitorFrequency"] = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000BC93 File Offset: 0x00009E93
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000BCA5 File Offset: 0x00009EA5
		[ConfigurationProperty("SiteMonitorFrequencyOnFailure", IsRequired = false, DefaultValue = "00:05:00")]
		public TimeSpan SiteMonitorFrequencyOnFailure
		{
			get
			{
				return (TimeSpan)base["SiteMonitorFrequencyOnFailure"];
			}
			set
			{
				base["SiteMonitorFrequencyOnFailure"] = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000BCB8 File Offset: 0x00009EB8
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000BCCA File Offset: 0x00009ECA
		[ConfigurationProperty("SiteMonitorTimeout", IsRequired = false, DefaultValue = "00:05:00")]
		public TimeSpan SiteMonitorTimeout
		{
			get
			{
				return (TimeSpan)base["SiteMonitorTimeout"];
			}
			set
			{
				base["SiteMonitorTimeout"] = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000BCDD File Offset: 0x00009EDD
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000BCEF File Offset: 0x00009EEF
		[ConfigurationProperty("MaxRemoteForestDiscoveryErrorsPerHour", IsRequired = false, DefaultValue = 25)]
		public int MaxRemoteForestDiscoveryErrorsPerHour
		{
			get
			{
				return (int)base["MaxRemoteForestDiscoveryErrorsPerHour"];
			}
			set
			{
				base["MaxRemoteForestDiscoveryErrorsPerHour"] = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000BD02 File Offset: 0x00009F02
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000BD14 File Offset: 0x00009F14
		[ConfigurationProperty("RemoteDomainSingleServerDiscoveryTimeout", IsRequired = false, DefaultValue = "00:05:00")]
		public TimeSpan RemoteDomainSingleServerDiscoveryTimeout
		{
			get
			{
				return (TimeSpan)base["RemoteDomainSingleServerDiscoveryTimeout"];
			}
			set
			{
				base["RemoteDomainSingleServerDiscoveryTimeout"] = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000BD27 File Offset: 0x00009F27
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000BD39 File Offset: 0x00009F39
		[ConfigurationProperty("ServiceStartupLoggingEnabled", IsRequired = false, DefaultValue = false)]
		public bool ServiceStartupLoggingEnabled
		{
			get
			{
				return (bool)base["ServiceStartupLoggingEnabled"];
			}
			set
			{
				base["ServiceStartupLoggingEnabled"] = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000BD4C File Offset: 0x00009F4C
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000BD5E File Offset: 0x00009F5E
		[ConfigurationProperty("LocalIPAddressesCacheRefreshInterval", IsRequired = false, DefaultValue = "1.00:00:00")]
		public TimeSpan LocalIPAddressesCacheRefreshInterval
		{
			get
			{
				return (TimeSpan)base["LocalIPAddressesCacheRefreshInterval"];
			}
			set
			{
				base["LocalIPAddressesCacheRefreshInterval"] = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000BD71 File Offset: 0x00009F71
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000BD83 File Offset: 0x00009F83
		[ConfigurationProperty("MaxAttemptsResetDCCache", IsRequired = false, DefaultValue = "10")]
		public int MaxAttemptsResetDCCache
		{
			get
			{
				return (int)base["MaxAttemptsResetDCCache"];
			}
			set
			{
				base["MaxAttemptsResetDCCache"] = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000BD96 File Offset: 0x00009F96
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000BDA8 File Offset: 0x00009FA8
		[ConfigurationProperty("MaintenanceModeDiscoveryTimeout", IsRequired = false, DefaultValue = "00:30:00")]
		public TimeSpan MaintenanceModeDiscoveryTimeout
		{
			get
			{
				return (TimeSpan)base["MaintenanceModeDiscoveryTimeout"];
			}
			set
			{
				base["MaintenanceModeDiscoveryTimeout"] = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000BDBB File Offset: 0x00009FBB
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000BDCD File Offset: 0x00009FCD
		[ConfigurationProperty("MaintenanceModeDiscoveryFrequency", IsRequired = false, DefaultValue = "00:03:00")]
		public TimeSpan MaintenanceModeDiscoveryFrequency
		{
			get
			{
				return (TimeSpan)base["MaintenanceModeDiscoveryFrequency"];
			}
			set
			{
				base["MaintenanceModeDiscoveryFrequency"] = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000BDE0 File Offset: 0x00009FE0
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000BDF2 File Offset: 0x00009FF2
		[ConfigurationProperty("ThrottleOnEmptyQueue", IsRequired = false, DefaultValue = "00:01:00")]
		public TimeSpan ThrottleOnEmptyQueue
		{
			get
			{
				return (TimeSpan)base["ThrottleOnEmptyQueue"];
			}
			set
			{
				base["ThrottleOnEmptyQueue"] = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000BE05 File Offset: 0x0000A005
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000BE17 File Offset: 0x0000A017
		[ConfigurationProperty("ThrottleOnFullQueue", IsRequired = false, DefaultValue = "00:00:30")]
		public TimeSpan ThrottleOnFullQueue
		{
			get
			{
				return (TimeSpan)base["ThrottleOnFullQueue"];
			}
			set
			{
				base["ThrottleOnFullQueue"] = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000BE2A File Offset: 0x0000A02A
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x0000BE3C File Offset: 0x0000A03C
		[ConfigurationProperty("MaxRunningTasks", IsRequired = false, DefaultValue = 40)]
		public int MaxRunningTasks
		{
			get
			{
				return (int)base["MaxRunningTasks"];
			}
			set
			{
				base["MaxRunningTasks"] = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000BE4F File Offset: 0x0000A04F
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000BE61 File Offset: 0x0000A061
		[ConfigurationProperty("WaitAmountBeforeRestartRequest", IsRequired = false, DefaultValue = "00:00:15")]
		public TimeSpan WaitAmountBeforeRestartRequest
		{
			get
			{
				return (TimeSpan)base["WaitAmountBeforeRestartRequest"];
			}
			set
			{
				base["WaitAmountBeforeRestartRequest"] = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000BE74 File Offset: 0x0000A074
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000BE86 File Offset: 0x0000A086
		[ConfigurationProperty("ExchangeTopologyCacheLifetime", IsRequired = false, DefaultValue = "04:00:00,04:00:00")]
		public string ExchangeTopologyCacheLifetime
		{
			get
			{
				return (string)base["ExchangeTopologyCacheLifetime"];
			}
			set
			{
				base["ExchangeTopologyCacheLifetime"] = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000BE94 File Offset: 0x0000A094
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000BEA6 File Offset: 0x0000A0A6
		[ConfigurationProperty("ExchangeTopologyCacheFrequency", IsRequired = false, DefaultValue = "01:00:00,01:00:00")]
		public string ExchangeTopologyCacheFrequency
		{
			get
			{
				return (string)base["ExchangeTopologyCacheFrequency"];
			}
			set
			{
				base["ExchangeTopologyCacheFrequency"] = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000BEC6 File Offset: 0x0000A0C6
		[ConfigurationProperty("MinPercentageOfHealthyDC", IsRequired = false, DefaultValue = "50")]
		public int MinPercentageOfHealthyDC
		{
			get
			{
				return (int)base["MinPercentageOfHealthyDC"];
			}
			set
			{
				base["MinPercentageOfHealthyDC"] = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000BED9 File Offset: 0x0000A0D9
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000BEEB File Offset: 0x0000A0EB
		[ConfigurationProperty("MinSuitableServer", IsRequired = false, DefaultValue = "3")]
		public int MinSuitableServer
		{
			get
			{
				return (int)base["MinSuitableServer"];
			}
			set
			{
				base["MinSuitableServer"] = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000BF10 File Offset: 0x0000A110
		[ConfigurationProperty("EnableWholeForestDiscovery", IsRequired = false, DefaultValue = false)]
		public bool EnableWholeForestDiscovery
		{
			get
			{
				return (bool)base["EnableWholeForestDiscovery"];
			}
			set
			{
				base["EnableWholeForestDiscovery"] = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000BF23 File Offset: 0x0000A123
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000BF35 File Offset: 0x0000A135
		[ConfigurationProperty("ForestWideAffinityRequested", IsRequired = false, DefaultValue = true)]
		public bool ForestWideAffinityRequested
		{
			get
			{
				return (bool)base["ForestWideAffinityRequested"];
			}
			set
			{
				base["ForestWideAffinityRequested"] = value;
			}
		}

		// Token: 0x040000D6 RID: 214
		private const string MinimumPrefixMatchKey = "MinimumPrefixMatch";

		// Token: 0x040000D7 RID: 215
		private const string ConfigDCSwitchKey = "ConfigDCSwitch";

		// Token: 0x040000D8 RID: 216
		private const string FullTopologyDiscoveryTimeoutKey = "FullTopologyDiscoveryTimeout";

		// Token: 0x040000D9 RID: 217
		private const string UrgentOrInitialTopologyTimeoutKey = "UrgentOrInitialTopologyTimeout";

		// Token: 0x040000DA RID: 218
		private const string DiscoveryFrequencyKey = "DiscoveryFrequency";

		// Token: 0x040000DB RID: 219
		private const string DiscoveryFrequencyOnFailureKey = "DiscoveryFrequencyOnFailure";

		// Token: 0x040000DC RID: 220
		private const string DiscoveryFrequencyOnNoTopologyKey = "DiscoveryFrequencyOnNoTopology";

		// Token: 0x040000DD RID: 221
		private const string DiscoveryFrequencyOnMinPercentageDCKey = "DiscoveryFrequencyOnMinPercentageDC";

		// Token: 0x040000DE RID: 222
		private const string WaitTimeBetweenInitialAndFullDiscoveryKey = "WaitTimeBetweenInitialAndFullDiscovery";

		// Token: 0x040000DF RID: 223
		private const string RemoteDiscoveryFrequencyKey = "RemoteDiscoveryFrequency";

		// Token: 0x040000E0 RID: 224
		private const string ForestScanFrequencyKey = "ForestScanFrequency";

		// Token: 0x040000E1 RID: 225
		private const string ForestScanFrequencyOnFailureKey = "ForestScanFrequencyOnFailure";

		// Token: 0x040000E2 RID: 226
		private const string SiteMonitorFrequencyKey = "SiteMonitorFrequency";

		// Token: 0x040000E3 RID: 227
		private const string SiteMonitorFrequencyOnFailureKey = "SiteMonitorFrequencyOnFailure";

		// Token: 0x040000E4 RID: 228
		private const string SiteMonitorTimeoutKey = "SiteMonitorTimeout";

		// Token: 0x040000E5 RID: 229
		private const string MaxRemoteForestDiscoveryErrorsPerHourKey = "MaxRemoteForestDiscoveryErrorsPerHour";

		// Token: 0x040000E6 RID: 230
		private const string RemoteDomainSingleServerDiscoveryTimeoutKey = "RemoteDomainSingleServerDiscoveryTimeout";

		// Token: 0x040000E7 RID: 231
		private const string ForestScanTimeoutKey = "ForestScanTimeout";

		// Token: 0x040000E8 RID: 232
		private const string ThrottleOnFullQueueKey = "ThrottleOnFullQueue";

		// Token: 0x040000E9 RID: 233
		private const string ThrottleOnEmptyQueueKey = "ThrottleOnEmptyQueue";

		// Token: 0x040000EA RID: 234
		private const string MaxRunningTasksKey = "MaxRunningTasks";

		// Token: 0x040000EB RID: 235
		private const string WaitAmountBeforeRestartRequestKey = "WaitAmountBeforeRestartRequest";

		// Token: 0x040000EC RID: 236
		private const string ServiceStartupLoggingEnabledKey = "ServiceStartupLoggingEnabled";

		// Token: 0x040000ED RID: 237
		private const string LocalIPAddressesCacheRefreshIntervalKey = "LocalIPAddressesCacheRefreshInterval";

		// Token: 0x040000EE RID: 238
		private const string MaxAttemptsResetDCCacheKey = "MaxAttemptsResetDCCache";

		// Token: 0x040000EF RID: 239
		private const string MaintenanceModeDiscoveryTimeoutKey = "MaintenanceModeDiscoveryTimeout";

		// Token: 0x040000F0 RID: 240
		private const string MaintenanceModeDiscoveryFrequencyKey = "MaintenanceModeDiscoveryFrequency";

		// Token: 0x040000F1 RID: 241
		private const string ExchangeTopologyCacheLifetimeKey = "ExchangeTopologyCacheLifetime";

		// Token: 0x040000F2 RID: 242
		private const string ExchangeTopologyCacheFrequencyKey = "ExchangeTopologyCacheFrequency";

		// Token: 0x040000F3 RID: 243
		private const string MinPercentageOfHealthyDCKey = "MinPercentageOfHealthyDC";

		// Token: 0x040000F4 RID: 244
		private const string MinSuitableServerKey = "MinSuitableServer";

		// Token: 0x040000F5 RID: 245
		private const string DiscoveryFrequencyOnFailoverKey = "DiscoveryFrequencyOnFailover";

		// Token: 0x040000F6 RID: 246
		private const string EnableWholeForestDiscoveryKey = "EnableWholeForestDiscovery";

		// Token: 0x040000F7 RID: 247
		private const string ForestWideAffinityRequestedKey = "ForestWideAffinityRequested";
	}
}

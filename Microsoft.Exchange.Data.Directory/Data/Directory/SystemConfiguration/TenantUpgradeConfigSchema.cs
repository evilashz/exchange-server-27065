using System;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005CB RID: 1483
	internal class TenantUpgradeConfigSchema : ConfigSchemaBase
	{
		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x0600444F RID: 17487 RVA: 0x00100122 File Offset: 0x000FE322
		public override string Name
		{
			get
			{
				return "TenantUpgrade";
			}
		}

		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x06004450 RID: 17488 RVA: 0x00100129 File Offset: 0x000FE329
		public override string SectionName
		{
			get
			{
				return "TenantUpgradeConfiguration";
			}
		}

		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x06004451 RID: 17489 RVA: 0x00100130 File Offset: 0x000FE330
		// (set) Token: 0x06004452 RID: 17490 RVA: 0x00100142 File Offset: 0x000FE342
		[ConfigurationProperty("IsUpgradingBrokerEnabled", DefaultValue = false)]
		public bool IsUpgradingBrokerEnabled
		{
			get
			{
				return (bool)base["IsUpgradingBrokerEnabled"];
			}
			set
			{
				base["IsUpgradingBrokerEnabled"] = value;
			}
		}

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06004453 RID: 17491 RVA: 0x00100155 File Offset: 0x000FE355
		// (set) Token: 0x06004454 RID: 17492 RVA: 0x00100167 File Offset: 0x000FE367
		[ConfigurationProperty("MaxConcurrentUpgradingThreadsPerServer", DefaultValue = "4")]
		public uint MaxConcurrentUpgradingThreadsPerServer
		{
			get
			{
				return (uint)base["MaxConcurrentUpgradingThreadsPerServer"];
			}
			set
			{
				base["MaxConcurrentUpgradingThreadsPerServer"] = value;
			}
		}

		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x06004455 RID: 17493 RVA: 0x0010017A File Offset: 0x000FE37A
		// (set) Token: 0x06004456 RID: 17494 RVA: 0x0010018C File Offset: 0x000FE38C
		[ConfigurationProperty("UpgradingBrokerPollIntervalInMinutes", DefaultValue = "1440")]
		public uint UpgradingBrokerPollIntervalInMinutes
		{
			get
			{
				return (uint)base["UpgradingBrokerPollIntervalInMinutes"];
			}
			set
			{
				base["UpgradingBrokerPollIntervalInMinutes"] = value;
			}
		}

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x06004457 RID: 17495 RVA: 0x0010019F File Offset: 0x000FE39F
		// (set) Token: 0x06004458 RID: 17496 RVA: 0x001001B1 File Offset: 0x000FE3B1
		[ConfigurationProperty("GetWorkItemsRetryIntervalInMinutes", DefaultValue = "2")]
		public uint GetWorkItemsRetryIntervalInMinutes
		{
			get
			{
				return (uint)base["GetWorkItemsRetryIntervalInMinutes"];
			}
			set
			{
				base["GetWorkItemsRetryIntervalInMinutes"] = value;
			}
		}

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x06004459 RID: 17497 RVA: 0x001001C4 File Offset: 0x000FE3C4
		// (set) Token: 0x0600445A RID: 17498 RVA: 0x001001D6 File Offset: 0x000FE3D6
		[ConfigurationProperty("StartUpgradeRetries", DefaultValue = "2")]
		public uint StartUpgradeRetries
		{
			get
			{
				return (uint)base["StartUpgradeRetries"];
			}
			set
			{
				base["StartUpgradeRetries"] = value;
			}
		}

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x0600445B RID: 17499 RVA: 0x001001E9 File Offset: 0x000FE3E9
		// (set) Token: 0x0600445C RID: 17500 RVA: 0x001001FB File Offset: 0x000FE3FB
		[ConfigurationProperty("StartUpgradeThresholdInSeconds", DefaultValue = "120")]
		public uint StartUpgradeThresholdInSeconds
		{
			get
			{
				return (uint)base["StartUpgradeThresholdInSeconds"];
			}
			set
			{
				base["StartUpgradeThresholdInSeconds"] = value;
			}
		}

		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x0600445D RID: 17501 RVA: 0x0010020E File Offset: 0x000FE40E
		// (set) Token: 0x0600445E RID: 17502 RVA: 0x00100220 File Offset: 0x000FE420
		[ConfigurationProperty("DelayBeforeCompletionInSeconds", DefaultValue = "30")]
		public uint DelayBeforeCompletionInSeconds
		{
			get
			{
				return (uint)base["DelayBeforeCompletionInSeconds"];
			}
			set
			{
				base["DelayBeforeCompletionInSeconds"] = value;
			}
		}

		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x0600445F RID: 17503 RVA: 0x00100233 File Offset: 0x000FE433
		// (set) Token: 0x06004460 RID: 17504 RVA: 0x00100245 File Offset: 0x000FE445
		[ConfigurationProperty("ExpectedMajorVersion", DefaultValue = "15")]
		public uint ExpectedMajorVersion
		{
			get
			{
				return (uint)base["ExpectedMajorVersion"];
			}
			set
			{
				base["ExpectedMajorVersion"] = value;
			}
		}

		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x06004461 RID: 17505 RVA: 0x00100258 File Offset: 0x000FE458
		// (set) Token: 0x06004462 RID: 17506 RVA: 0x0010026A File Offset: 0x000FE46A
		[ConfigurationProperty("ExpectedMinorVersion", DefaultValue = "0")]
		public uint ExpectedMinorVersion
		{
			get
			{
				return (uint)base["ExpectedMinorVersion"];
			}
			set
			{
				base["ExpectedMinorVersion"] = value;
			}
		}

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x06004463 RID: 17507 RVA: 0x0010027D File Offset: 0x000FE47D
		// (set) Token: 0x06004464 RID: 17508 RVA: 0x0010028F File Offset: 0x000FE48F
		[ConfigurationProperty("SkipIfExpectedVersionDoesntMatchAssembly", DefaultValue = true)]
		public bool SkipIfExpectedVersionDoesntMatchAssembly
		{
			get
			{
				return (bool)base["SkipIfExpectedVersionDoesntMatchAssembly"];
			}
			set
			{
				base["SkipIfExpectedVersionDoesntMatchAssembly"] = value;
			}
		}

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x06004465 RID: 17509 RVA: 0x001002A2 File Offset: 0x000FE4A2
		// (set) Token: 0x06004466 RID: 17510 RVA: 0x001002B4 File Offset: 0x000FE4B4
		[ConfigurationProperty("UpgradeSchedule", DefaultValue = "Never")]
		public string UpgradeSchedule
		{
			get
			{
				return (string)base["UpgradeSchedule"];
			}
			set
			{
				base["UpgradeSchedule"] = value;
			}
		}

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06004467 RID: 17511 RVA: 0x001002C2 File Offset: 0x000FE4C2
		// (set) Token: 0x06004468 RID: 17512 RVA: 0x001002D4 File Offset: 0x000FE4D4
		[ConfigurationProperty("PreventServiceStopIfUpgradeInProgress", DefaultValue = false)]
		public bool PreventServiceStopIfUpgradeInProgress
		{
			get
			{
				return (bool)base["PreventServiceStopIfUpgradeInProgress"];
			}
			set
			{
				base["PreventServiceStopIfUpgradeInProgress"] = value;
			}
		}

		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06004469 RID: 17513 RVA: 0x001002E7 File Offset: 0x000FE4E7
		// (set) Token: 0x0600446A RID: 17514 RVA: 0x001002F9 File Offset: 0x000FE4F9
		[ConfigurationProperty("StuckTenantDetectionThresholdInMinutes", DefaultValue = "60")]
		public uint StuckTenantDetectionThresholdInMinutes
		{
			get
			{
				return (uint)base["StuckTenantDetectionThresholdInMinutes"];
			}
			set
			{
				base["StuckTenantDetectionThresholdInMinutes"] = value;
			}
		}

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x0600446B RID: 17515 RVA: 0x0010030C File Offset: 0x000FE50C
		// (set) Token: 0x0600446C RID: 17516 RVA: 0x0010031E File Offset: 0x000FE51E
		[ConfigurationProperty("NumberOfFailuresBeforeBlackListing", DefaultValue = "5")]
		public uint NumberOfFailuresBeforeBlackListing
		{
			get
			{
				return (uint)base["NumberOfFailuresBeforeBlackListing"];
			}
			set
			{
				base["NumberOfFailuresBeforeBlackListing"] = value;
			}
		}

		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x0600446D RID: 17517 RVA: 0x00100331 File Offset: 0x000FE531
		// (set) Token: 0x0600446E RID: 17518 RVA: 0x00100343 File Offset: 0x000FE543
		[ConfigurationProperty("EnableFileLogging", DefaultValue = false)]
		public bool EnableFileLogging
		{
			get
			{
				return (bool)base["EnableFileLogging"];
			}
			set
			{
				base["EnableFileLogging"] = value;
			}
		}

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x0600446F RID: 17519 RVA: 0x00100356 File Offset: 0x000FE556
		// (set) Token: 0x06004470 RID: 17520 RVA: 0x00100368 File Offset: 0x000FE568
		[ConfigurationProperty("EnableADHealthMonitoring", DefaultValue = true)]
		public bool EnableADHealthMonitoring
		{
			get
			{
				return (bool)base["EnableADHealthMonitoring"];
			}
			set
			{
				base["EnableADHealthMonitoring"] = value;
			}
		}

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x06004471 RID: 17521 RVA: 0x0010037B File Offset: 0x000FE57B
		// (set) Token: 0x06004472 RID: 17522 RVA: 0x0010038D File Offset: 0x000FE58D
		[ConfigurationProperty("EnableUpdateThrottling", DefaultValue = false)]
		public bool EnableUpdateThrottling
		{
			get
			{
				return (bool)base["EnableUpdateThrottling"];
			}
			set
			{
				base["EnableUpdateThrottling"] = value;
			}
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x001003A0 File Offset: 0x000FE5A0
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			ExTraceGlobals.TenantUpgradeServiceletTracer.TraceDebug<string, string>(0L, "Unrecognized configuration attribute {0}={1}", name, value);
			return base.OnDeserializeUnrecognizedAttribute(name, value);
		}

		// Token: 0x020005CC RID: 1484
		public static class Setting
		{
			// Token: 0x04002EAE RID: 11950
			public const string IsUpgradingBrokerEnabled = "IsUpgradingBrokerEnabled";

			// Token: 0x04002EAF RID: 11951
			public const string MaxConcurrentUpgradingThreadsPerServer = "MaxConcurrentUpgradingThreadsPerServer";

			// Token: 0x04002EB0 RID: 11952
			public const string UpgradingBrokerPollIntervalInMinutes = "UpgradingBrokerPollIntervalInMinutes";

			// Token: 0x04002EB1 RID: 11953
			public const string GetWorkItemsRetryIntervalInMinutes = "GetWorkItemsRetryIntervalInMinutes";

			// Token: 0x04002EB2 RID: 11954
			public const string StartUpgradeRetries = "StartUpgradeRetries";

			// Token: 0x04002EB3 RID: 11955
			public const string StartUpgradeThresholdInSeconds = "StartUpgradeThresholdInSeconds";

			// Token: 0x04002EB4 RID: 11956
			public const string DelayBeforeCompletionInSeconds = "DelayBeforeCompletionInSeconds";

			// Token: 0x04002EB5 RID: 11957
			public const string ExpectedMajorVersion = "ExpectedMajorVersion";

			// Token: 0x04002EB6 RID: 11958
			public const string ExpectedMinorVersion = "ExpectedMinorVersion";

			// Token: 0x04002EB7 RID: 11959
			public const string SkipIfExpectedVersionDoesntMatchAssembly = "SkipIfExpectedVersionDoesntMatchAssembly";

			// Token: 0x04002EB8 RID: 11960
			public const string UpgradeSchedule = "UpgradeSchedule";

			// Token: 0x04002EB9 RID: 11961
			public const string PreventServiceStopIfUpgradeInProgress = "PreventServiceStopIfUpgradeInProgress";

			// Token: 0x04002EBA RID: 11962
			public const string StuckTenantDetectionThresholdInMinutes = "StuckTenantDetectionThresholdInMinutes";

			// Token: 0x04002EBB RID: 11963
			public const string NumberOfFailuresBeforeBlackListing = "NumberOfFailuresBeforeBlackListing";

			// Token: 0x04002EBC RID: 11964
			public const string EnableFileLogging = "EnableFileLogging";

			// Token: 0x04002EBD RID: 11965
			public const string EnableADHealthMonitoring = "EnableADHealthMonitoring";

			// Token: 0x04002EBE RID: 11966
			public const string EnableUpdateThrottling = "EnableUpdateThrottling";
		}
	}
}

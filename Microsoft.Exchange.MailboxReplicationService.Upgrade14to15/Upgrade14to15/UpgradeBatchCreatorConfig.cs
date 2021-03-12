using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000BD RID: 189
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpgradeBatchCreatorConfig : AnchorConfig
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x0000AE00 File Offset: 0x00009000
		internal UpgradeBatchCreatorConfig() : base("UpgradeBatchCreator")
		{
			base.UpdateConfig<string>("BatchFileDirectoryPath", UpgradeBatchCreatorConfig.GetDefaultBatchFileDirectoryPath());
			base.UpdateConfig<TimeSpan>("IdleRunDelay", TimeSpan.FromMinutes(15.0));
			base.UpdateConfig<TimeSpan>("ActiveRunDelay", TimeSpan.FromSeconds(10.0));
			base.UpdateConfig<string>("MonitoringComponentName", ExchangeComponent.MailboxMigration.Name);
			base.UpdateConfig<string>("CacheEntryPoisonNotificationReason", "UpgradeBatchCreatorCacheEntryIsPoisonedNotification");
			base.UpdateConfig<long>("LogMaxFileSize", 52428800L);
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000AE90 File Offset: 0x00009090
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0000AE9D File Offset: 0x0000909D
		[ConfigurationProperty("BatchFileDirectoryPath")]
		public string BatchFileDirectoryPath
		{
			get
			{
				return this.InternalGetConfig<string>("BatchFileDirectoryPath");
			}
			set
			{
				this.InternalSetConfig<string>(value, "BatchFileDirectoryPath");
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000AEAB File Offset: 0x000090AB
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x0000AEB8 File Offset: 0x000090B8
		[ConfigurationProperty("UpgradeBatchFilenamePrefix", DefaultValue = "E15UpgradeMSEXCH")]
		public string UpgradeBatchFilenamePrefix
		{
			get
			{
				return this.InternalGetConfig<string>("UpgradeBatchFilenamePrefix");
			}
			set
			{
				this.InternalSetConfig<string>(value, "UpgradeBatchFilenamePrefix");
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000AEC6 File Offset: 0x000090C6
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x0000AED3 File Offset: 0x000090D3
		[ConfigurationProperty("DryRunBatchFilenamePrefix", DefaultValue = "E15UpgradeMSEXCHDryRun")]
		public string DryRunBatchFilenamePrefix
		{
			get
			{
				return this.InternalGetConfig<string>("DryRunBatchFilenamePrefix");
			}
			set
			{
				this.InternalSetConfig<string>(value, "DryRunBatchFilenamePrefix");
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000AEE1 File Offset: 0x000090E1
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0000AEEE File Offset: 0x000090EE
		[ConfigurationProperty("MaxBatchSize", DefaultValue = 500)]
		[IntegerValidator(MinValue = 1, MaxValue = 5000, ExcludeRange = false)]
		public int MaxBatchSize
		{
			get
			{
				return this.InternalGetConfig<int>("MaxBatchSize");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxBatchSize");
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000AEFC File Offset: 0x000090FC
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x0000AF09 File Offset: 0x00009109
		[ConfigurationProperty("E14CountUpdateInterval", DefaultValue = "03:00:00")]
		[TimeSpanValidator(MinValueString = "00:15:00", MaxValueString = "1.00:00:00", ExcludeRange = false)]
		public TimeSpan E14CountUpdateIntervalName
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("E14CountUpdateIntervalName");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "E14CountUpdateIntervalName");
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000AF17 File Offset: 0x00009117
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x0000AF24 File Offset: 0x00009124
		[ConfigurationProperty("DelayUntilCreateNewBatches", DefaultValue = "02:00:00")]
		[TimeSpanValidator(MinValueString = "00:15:00", MaxValueString = "1.00:00:00", ExcludeRange = false)]
		public TimeSpan DelayUntilCreateNewBatches
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("DelayUntilCreateNewBatches");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "DelayUntilCreateNewBatches");
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000AF32 File Offset: 0x00009132
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x0000AF3F File Offset: 0x0000913F
		[ConfigurationProperty("RemoveNonUpgradeMoveRequests", DefaultValue = true)]
		public bool RemoveNonUpgradeMoveRequests
		{
			get
			{
				return this.InternalGetConfig<bool>("RemoveNonUpgradeMoveRequests");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "RemoveNonUpgradeMoveRequests");
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000AF4D File Offset: 0x0000914D
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0000AF5A File Offset: 0x0000915A
		[ConfigurationProperty("ConfigOnly", DefaultValue = true)]
		public bool ConfigOnly
		{
			get
			{
				return this.InternalGetConfig<bool>("ConfigOnly");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "ConfigOnly");
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000AF68 File Offset: 0x00009168
		private static string GetDefaultBatchFileDirectoryPath()
		{
			string text = ExchangeSetupContext.InstallPath;
			if (text == null)
			{
				text = Assembly.GetExecutingAssembly().Location;
				text = Path.GetDirectoryName(text);
			}
			return Path.Combine(text, "UpgradeBatches");
		}

		// Token: 0x040002C1 RID: 705
		private const string UpgradeBatchCreatorCacheEntryIsPoisonedNotification = "UpgradeBatchCreatorCacheEntryIsPoisonedNotification";
	}
}

using System;
using System.Configuration;
using System.IO;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationServiceConfigSchema : ConfigSchemaBase
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00006C50 File Offset: 0x00004E50
		public MigrationServiceConfigSchema()
		{
			string text = Path.GetDirectoryName(MigrationLog.DefaultLogPath);
			text += "\\MigrationReports";
			base.SetDefaultConfigValue<string>("MigrationReportingLoggingFolder", text);
			base.SetDefaultConfigValue<string>("SyncMigrationEnabledMigrationTypes", (MigrationType.IMAP | MigrationType.ExchangeOutlookAnywhere | MigrationType.ExchangeRemoteMove | MigrationType.ExchangeLocalMove | MigrationType.PublicFolder).ToString());
			if (CommonUtils.IsMultiTenantEnabled())
			{
				base.SetDefaultConfigValue<bool>("MigrationReportingLoggingEnabled", true);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00006CB3 File Offset: 0x00004EB3
		public override string Name
		{
			get
			{
				return "MigrationService";
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00006CBA File Offset: 0x00004EBA
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00006CC7 File Offset: 0x00004EC7
		[ConfigurationProperty("SyncMigrationIsEnabled", DefaultValue = true)]
		public bool IsServiceletEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("IsServiceletEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "IsServiceletEnabled");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00006CD5 File Offset: 0x00004ED5
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00006CE2 File Offset: 0x00004EE2
		[ConfigurationProperty("SyncMigrationPollingBatchSize", DefaultValue = 10)]
		[IntegerValidator(MinValue = 0, MaxValue = 256, ExcludeRange = false)]
		public int PollingBatchSize
		{
			get
			{
				return this.InternalGetConfig<int>("PollingBatchSize");
			}
			set
			{
				this.InternalSetConfig<int>(value, "PollingBatchSize");
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00006CF0 File Offset: 0x00004EF0
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00006CFD File Offset: 0x00004EFD
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "1.0:00:00", ExcludeRange = false)]
		[ConfigurationProperty("SyncMigrationInitialSyncStartPollingTimeout", DefaultValue = "00:05:00")]
		public TimeSpan InitialSyncPollingInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("InitialSyncPollingInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "InitialSyncPollingInterval");
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00006D0B File Offset: 0x00004F0B
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00006D18 File Offset: 0x00004F18
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "7.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("SyncMigrationPollingTimeout", DefaultValue = "1.0:00:00")]
		public TimeSpan IncrementalSyncPollingInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("IncrementalSyncPollingInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "IncrementalSyncPollingInterval");
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00006D26 File Offset: 0x00004F26
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00006D33 File Offset: 0x00004F33
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "365.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("ReportInterval", DefaultValue = "1.0:00:00")]
		public TimeSpan ReportInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ReportInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ReportInterval");
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00006D41 File Offset: 0x00004F41
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00006D4E File Offset: 0x00004F4E
		[ConfigurationProperty("ReportMaxAttachmentSize", DefaultValue = 1073741824)]
		[IntegerValidator(MinValue = 0, ExcludeRange = false)]
		public int ReportMaxAttachmentSize
		{
			get
			{
				return this.InternalGetConfig<int>("ReportMaxAttachmentSize");
			}
			set
			{
				this.InternalSetConfig<int>(value, "ReportMaxAttachmentSize");
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00006D5C File Offset: 0x00004F5C
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00006D69 File Offset: 0x00004F69
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "7.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("SyncMigrationInitialSyncTimeOutForFailingSubscriptions", DefaultValue = "01:20:00")]
		public TimeSpan InitialSyncSubscriptionTimeout
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("InitialSyncSubscriptionTimeout");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "InitialSyncSubscriptionTimeout");
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00006D77 File Offset: 0x00004F77
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00006D84 File Offset: 0x00004F84
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "7.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("MRSInitialSyncSubscriptionTimeout", DefaultValue = "05:00:00")]
		public TimeSpan MRSInitialSyncSubscriptionTimeout
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MRSInitialSyncSubscriptionTimeout");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MRSInitialSyncSubscriptionTimeout");
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00006D92 File Offset: 0x00004F92
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00006D9F File Offset: 0x00004F9F
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "365.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("SyncMigrationTimeOutForFailingSubscriptions", DefaultValue = "365.00:00:00")]
		public TimeSpan IncrementalSyncSubscriptionTimeout
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("IncrementalSyncSubscriptionTimeout");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "IncrementalSyncSubscriptionTimeout");
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00006DAD File Offset: 0x00004FAD
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00006DBA File Offset: 0x00004FBA
		[TimeSpanValidator(MinValueString = "-00:00:01", MaxValueString = "1.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("SyncMigrationLazyCountRescanPollingTimeout", DefaultValue = "00:10:00")]
		public TimeSpan LazyCountRescanPollingInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("LazyCountRescanPollingInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "LazyCountRescanPollingInterval");
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00006DC8 File Offset: 0x00004FC8
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00006DD5 File Offset: 0x00004FD5
		[ConfigurationProperty("SyncMigrationCancellationBatchSize", DefaultValue = 100)]
		[IntegerValidator(MinValue = 1, MaxValue = 100000, ExcludeRange = false)]
		public int CancellationBatchSize
		{
			get
			{
				return this.InternalGetConfig<int>("CancellationBatchSize");
			}
			set
			{
				this.InternalSetConfig<int>(value, "CancellationBatchSize");
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00006DE3 File Offset: 0x00004FE3
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00006DF0 File Offset: 0x00004FF0
		[ConfigurationProperty("TransitionBatchSize", DefaultValue = 75)]
		[IntegerValidator(MinValue = 1, MaxValue = 100000, ExcludeRange = false)]
		public int TransitionBatchSize
		{
			get
			{
				return this.InternalGetConfig<int>("TransitionBatchSize");
			}
			set
			{
				this.InternalSetConfig<int>(value, "TransitionBatchSize");
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00006DFE File Offset: 0x00004FFE
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00006E0B File Offset: 0x0000500B
		[ConfigurationProperty("ProcessingBatchSize", DefaultValue = 100)]
		[IntegerValidator(MinValue = 1, MaxValue = 100000, ExcludeRange = false)]
		public int ProcessingBatchSize
		{
			get
			{
				return this.InternalGetConfig<int>("ProcessingBatchSize");
			}
			set
			{
				this.InternalSetConfig<int>(value, "ProcessingBatchSize");
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00006E19 File Offset: 0x00005019
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00006E26 File Offset: 0x00005026
		[ConfigurationProperty("ProcessingSessionSize", DefaultValue = 5)]
		[IntegerValidator(MinValue = 1, MaxValue = 100000, ExcludeRange = false)]
		public int ProcessingSessionSize
		{
			get
			{
				return this.InternalGetConfig<int>("ProcessingSessionSize");
			}
			set
			{
				this.InternalSetConfig<int>(value, "ProcessingSessionSize");
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00006E34 File Offset: 0x00005034
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00006E41 File Offset: 0x00005041
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "7.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("SyncMigrationProcessorIdleRunDelay", DefaultValue = "00:00:30")]
		public TimeSpan ProcessorIdleRunDelay
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ProcessorIdleRunDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ProcessorIdleRunDelay");
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00006E4F File Offset: 0x0000504F
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00006E5C File Offset: 0x0000505C
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "7.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("SyncMigrationProcessorActiveRunDelay", DefaultValue = "00:00:30")]
		public TimeSpan ProcessorActiveRunDelay
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ProcessorActiveRunDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ProcessorActiveRunDelay");
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00006E6A File Offset: 0x0000506A
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00006E77 File Offset: 0x00005077
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "7.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("SyncMigrationProcessorTransientErrorRunDelay", DefaultValue = "00:00:30")]
		public TimeSpan ProcessorTransientErrorRunDelay
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ProcessorTransientErrorRunDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ProcessorTransientErrorRunDelay");
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00006E85 File Offset: 0x00005085
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00006E92 File Offset: 0x00005092
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "12:00:00", ExcludeRange = false)]
		[ConfigurationProperty("SyncMigrationProcessorMaxWaitingJobDelay", DefaultValue = "00:30:00")]
		public TimeSpan ProcessorMaxWaitingJobDelay
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ProcessorMaxWaitingJobDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ProcessorMaxWaitingJobDelay");
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00006EA0 File Offset: 0x000050A0
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00006EAD File Offset: 0x000050AD
		[ConfigurationProperty("SyncMigrationProcessorAverageWaitingJobDelay", DefaultValue = "00:02:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "12:00:00", ExcludeRange = false)]
		public TimeSpan ProcessorAverageWaitingJobDelay
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ProcessorAverageWaitingJobDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ProcessorAverageWaitingJobDelay");
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00006EBB File Offset: 0x000050BB
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00006EC8 File Offset: 0x000050C8
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "12:00:00", ExcludeRange = false)]
		[ConfigurationProperty("SyncMigrationProcessorSyncedJobItemDelay", DefaultValue = "00:15:00")]
		public TimeSpan ProcessorSyncedJobItemDelay
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ProcessorSyncedJobItemDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ProcessorSyncedJobItemDelay");
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00006ED6 File Offset: 0x000050D6
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00006EE3 File Offset: 0x000050E3
		[IntegerValidator(MinValue = 1, MaxValue = 512, ExcludeRange = false)]
		[ConfigurationProperty("SyncMigrationServiceRpcSkeletonMaxThreads", DefaultValue = 8)]
		public int MigrationServiceRpcSkeletonMaxThreads
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationServiceRpcSkeletonMaxThreads");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationServiceRpcSkeletonMaxThreads");
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00006EF1 File Offset: 0x000050F1
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00006EFE File Offset: 0x000050FE
		[ConfigurationProperty("SyncMigrationNotificationRpcSkeletonMaxThreads", DefaultValue = 8)]
		[IntegerValidator(MinValue = 1, MaxValue = 512, ExcludeRange = false)]
		public int MigrationNotificationRpcSkeletonMaxThreads
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationNotificationRpcSkeletonMaxThreads");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationNotificationRpcSkeletonMaxThreads");
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00006F0C File Offset: 0x0000510C
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00006F19 File Offset: 0x00005119
		[ConfigurationProperty("ProvisioningMaxNumThreads", DefaultValue = 5)]
		[IntegerValidator(MinValue = 1, MaxValue = 50, ExcludeRange = false)]
		public int ProvisioningMaxNumThreads
		{
			get
			{
				return this.InternalGetConfig<int>("ProvisioningMaxNumThreads");
			}
			set
			{
				this.InternalSetConfig<int>(value, "ProvisioningMaxNumThreads");
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00006F27 File Offset: 0x00005127
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00006F34 File Offset: 0x00005134
		[ConfigurationProperty("MigrationSourceMailboxLegacyExchangeDNStampingEnabled", DefaultValue = true)]
		public bool IsMigrationSourceMailboxLegacyExchangeDNStampingEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("IsMigrationSourceMailboxLegacyExchangeDNStampingEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "IsMigrationSourceMailboxLegacyExchangeDNStampingEnabled");
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00006F42 File Offset: 0x00005142
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00006F4F File Offset: 0x0000514F
		[TimeSpanValidator(MinValueString = "00:00:01", MaxValueString = "365.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("MigrationDelayedSubscriptionThreshold", DefaultValue = "04:00:00")]
		public TimeSpan MigrationDelayedSubscriptionThreshold
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MigrationDelayedSubscriptionThreshold");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MigrationDelayedSubscriptionThreshold");
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00006F5D File Offset: 0x0000515D
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00006F6A File Offset: 0x0000516A
		[ConfigurationProperty("MaxConcurrentMigrations", DefaultValue = 10)]
		[IntegerValidator(MinValue = 0, MaxValue = 256, ExcludeRange = false)]
		public int MaxConcurrentMigrations
		{
			get
			{
				return this.InternalGetConfig<int>("MaxConcurrentMigrations");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxConcurrentMigrations");
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006F78 File Offset: 0x00005178
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00006F85 File Offset: 0x00005185
		[IntegerValidator(MinValue = 0, MaxValue = 1000, ExcludeRange = false)]
		[ConfigurationProperty("MigrationProxyRpcEndpointMaxConcurrentRpcCount", DefaultValue = 100)]
		public int MigrationProxyRpcEndpointMaxConcurrentRpcCount
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationProxyRpcEndpointMaxConcurrentRpcCount");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationProxyRpcEndpointMaxConcurrentRpcCount");
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006F93 File Offset: 0x00005193
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00006FA0 File Offset: 0x000051A0
		[ConfigurationProperty("MaxRowsToProcessInOnePass", DefaultValue = 500)]
		[IntegerValidator(MinValue = 0, MaxValue = 100000, ExcludeRange = false)]
		public int MaxRowsToProcessInOnePass
		{
			get
			{
				return this.InternalGetConfig<int>("MaxRowsToProcessInOnePass");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxRowsToProcessInOnePass");
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006FAE File Offset: 0x000051AE
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00006FBB File Offset: 0x000051BB
		[TimeSpanValidator(MinValueString = "00:00:05", MaxValueString = "1.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("MaxTimeToProcessInOnePass", DefaultValue = "00:08:00")]
		public TimeSpan MaxTimeToProcessInOnePass
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MaxTimeToProcessInOnePass");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MaxTimeToProcessInOnePass");
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00006FC9 File Offset: 0x000051C9
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00006FD6 File Offset: 0x000051D6
		[ConfigurationProperty("SyncMigrationMaxJobItemsToProcessForReportGeneration", DefaultValue = 5000)]
		[IntegerValidator(MinValue = 1, MaxValue = 100000, ExcludeRange = false)]
		public int MaxJobItemsToProcessForReportGeneration
		{
			get
			{
				return this.InternalGetConfig<int>("MaxJobItemsToProcessForReportGeneration");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxJobItemsToProcessForReportGeneration");
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00006FE4 File Offset: 0x000051E4
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00006FF1 File Offset: 0x000051F1
		[IntegerValidator(MinValue = 0, MaxValue = 2, ExcludeRange = false)]
		[ConfigurationProperty("MigrationUseDKMForEncryption", DefaultValue = 0)]
		public int MigrationUseDKMForEncryption
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationUseDKMForEncryption");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationUseDKMForEncryption");
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00006FFF File Offset: 0x000051FF
		// (set) Token: 0x06000100 RID: 256 RVA: 0x0000700C File Offset: 0x0000520C
		[ConfigurationProperty("MaxItemsToProvisionInOnePass", DefaultValue = 10)]
		[IntegerValidator(MinValue = 0, MaxValue = 100, ExcludeRange = false)]
		public int MaxItemsToProvisionInOnePass
		{
			get
			{
				return this.InternalGetConfig<int>("MaxItemsToProvisionInOnePass");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxItemsToProvisionInOnePass");
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000701A File Offset: 0x0000521A
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00007027 File Offset: 0x00005227
		[ConfigurationProperty("MigrationSourceExchangeMailboxMaximumCount", DefaultValue = 2000)]
		[IntegerValidator(MinValue = 0, MaxValue = 100000, ExcludeRange = false)]
		public int MigrationSourceExchangeMailboxMaximumCount
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationSourceExchangeMailboxMaximumCount");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationSourceExchangeMailboxMaximumCount");
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00007035 File Offset: 0x00005235
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00007042 File Offset: 0x00005242
		[ConfigurationProperty("MigrationSourceExchangeRecipientMaximumCount", DefaultValue = 50000)]
		[IntegerValidator(MinValue = 0, MaxValue = 100000, ExcludeRange = false)]
		public int MigrationSourceExchangeRecipientMaximumCount
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationSourceExchangeRecipientMaximumCount");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationSourceExchangeRecipientMaximumCount");
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00007050 File Offset: 0x00005250
		// (set) Token: 0x06000106 RID: 262 RVA: 0x0000705D File Offset: 0x0000525D
		[IntegerValidator(MinValue = 0, MaxValue = 100000, ExcludeRange = false)]
		[ConfigurationProperty("MigrationSourceStagedExchangeCSVMailboxMaximumCount", DefaultValue = 10000)]
		public int MigrationSourceStagedExchangeCSVMailboxMaximumCount
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationSourceStagedExchangeCSVMailboxMaximumCount");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationSourceStagedExchangeCSVMailboxMaximumCount");
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000706B File Offset: 0x0000526B
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00007078 File Offset: 0x00005278
		[IntegerValidator(MinValue = 1, MaxValue = 100000, ExcludeRange = false)]
		[ConfigurationProperty("MigrationPoisonedCountThreshold", DefaultValue = 5)]
		public int MigrationPoisonedCountThreshold
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationPoisonedCountThreshold");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationPoisonedCountThreshold");
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00007086 File Offset: 0x00005286
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00007093 File Offset: 0x00005293
		[ConfigurationProperty("MigrationTransientErrorCountThreshold", DefaultValue = 10)]
		[IntegerValidator(MinValue = 0, MaxValue = 100000, ExcludeRange = false)]
		public int MigrationTransientErrorCountThreshold
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationTransientErrorCountThreshold");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationTransientErrorCountThreshold");
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000070A1 File Offset: 0x000052A1
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000070AE File Offset: 0x000052AE
		[ConfigurationProperty("SyncMigrationProcessorMinWaitingJobDelay", DefaultValue = "00:00:10")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "12:00:00", ExcludeRange = false)]
		public TimeSpan ProcessorMinWaitingJobDelay
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ProcessorMinWaitingJobDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ProcessorMinWaitingJobDelay");
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000070BC File Offset: 0x000052BC
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000070C9 File Offset: 0x000052C9
		[TimeSpanValidator(MinValueString = "00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("MigrationTransientErrorIntervalThreshold", DefaultValue = "00:30:00")]
		public TimeSpan MigrationTransientErrorIntervalThreshold
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MigrationTransientErrorIntervalThreshold");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MigrationTransientErrorIntervalThreshold");
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000070D7 File Offset: 0x000052D7
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000070E4 File Offset: 0x000052E4
		[IntegerValidator(MinValue = 0, MaxValue = 100, ExcludeRange = false)]
		[ConfigurationProperty("SyncMigrationFailureRatioForAutoCancel", DefaultValue = 10)]
		public int FailureRatioForAutoCancel
		{
			get
			{
				return this.InternalGetConfig<int>("FailureRatioForAutoCancel");
			}
			set
			{
				this.InternalSetConfig<int>(value, "FailureRatioForAutoCancel");
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000070F2 File Offset: 0x000052F2
		// (set) Token: 0x06000112 RID: 274 RVA: 0x000070FF File Offset: 0x000052FF
		[ConfigurationProperty("SyncMigrationAbsoluteFailureCountForAutoCancel", DefaultValue = 10000)]
		[IntegerValidator(MinValue = 1, MaxValue = 100000, ExcludeRange = false)]
		public int AbsoluteFailureCountForAutoCancel
		{
			get
			{
				return this.InternalGetConfig<int>("AbsoluteFailureCountForAutoCancel");
			}
			set
			{
				this.InternalSetConfig<int>(value, "AbsoluteFailureCountForAutoCancel");
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000710D File Offset: 0x0000530D
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000711A File Offset: 0x0000531A
		[IntegerValidator(MinValue = 1, MaxValue = 100000, ExcludeRange = false)]
		[ConfigurationProperty("SyncMigrationMinimumFailureCountForAutoCancel", DefaultValue = 500)]
		public int MinimumFailureCountForAutoCancel
		{
			get
			{
				return this.InternalGetConfig<int>("MinimumFailureCountForAutoCancel");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MinimumFailureCountForAutoCancel");
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00007128 File Offset: 0x00005328
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00007135 File Offset: 0x00005335
		[IntegerValidator(MinValue = 1, MaxValue = 1000, ExcludeRange = false)]
		[ConfigurationProperty("MaxNumberOfMailEnabledPublicFoldersToProcessInOnePass", DefaultValue = 1)]
		public int MaxNumberOfMailEnabledPublicFoldersToProcessInOnePass
		{
			get
			{
				return this.InternalGetConfig<int>("MaxNumberOfMailEnabledPublicFoldersToProcessInOnePass");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxNumberOfMailEnabledPublicFoldersToProcessInOnePass");
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00007143 File Offset: 0x00005343
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00007150 File Offset: 0x00005350
		[ConfigurationProperty("IMAPSessionVersion", DefaultValue = 5L)]
		[LongValidator(MinValue = 1L, MaxValue = 5L, ExcludeRange = false)]
		public long IMAPSessionVersion
		{
			get
			{
				return this.InternalGetConfig<long>("IMAPSessionVersion");
			}
			set
			{
				this.InternalSetConfig<long>(value, "IMAPSessionVersion");
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000715E File Offset: 0x0000535E
		// (set) Token: 0x0600011A RID: 282 RVA: 0x0000716B File Offset: 0x0000536B
		[ConfigurationProperty("MigrationMaximumJobItemsPerBatch", DefaultValue = 200000)]
		[IntegerValidator(MinValue = 0, ExcludeRange = false)]
		public int MigrationMaximumJobItemsPerBatch
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationMaximumJobItemsPerBatch");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationMaximumJobItemsPerBatch");
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00007179 File Offset: 0x00005379
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00007186 File Offset: 0x00005386
		[LongValidator(MinValue = 1L, MaxValue = 5L, ExcludeRange = false)]
		[ConfigurationProperty("ExchangeSessionVersion", DefaultValue = 5L)]
		public long ExchangeSessionVersion
		{
			get
			{
				return this.InternalGetConfig<long>("ExchangeSessionVersion");
			}
			set
			{
				this.InternalSetConfig<long>(value, "ExchangeSessionVersion");
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00007194 File Offset: 0x00005394
		// (set) Token: 0x0600011E RID: 286 RVA: 0x000071A1 File Offset: 0x000053A1
		[LongValidator(MinValue = 1L, MaxValue = 5L, ExcludeRange = false)]
		[ConfigurationProperty("BulkProvisioningSessionVersion", DefaultValue = 1L)]
		public long BulkProvisioningSessionVersion
		{
			get
			{
				return this.InternalGetConfig<long>("BulkProvisioningSessionVersion");
			}
			set
			{
				this.InternalSetConfig<long>(value, "BulkProvisioningSessionVersion");
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000071AF File Offset: 0x000053AF
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000071BC File Offset: 0x000053BC
		[LongValidator(MinValue = 4L, MaxValue = 5L, ExcludeRange = false)]
		[ConfigurationProperty("LocalMoveSessionVersion", DefaultValue = 4L)]
		public long LocalMoveSessionVersion
		{
			get
			{
				return this.InternalGetConfig<long>("LocalMoveSessionVersion");
			}
			set
			{
				this.InternalSetConfig<long>(value, "LocalMoveSessionVersion");
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000071CA File Offset: 0x000053CA
		// (set) Token: 0x06000122 RID: 290 RVA: 0x000071D7 File Offset: 0x000053D7
		[LongValidator(MinValue = 4L, MaxValue = 5L, ExcludeRange = false)]
		[ConfigurationProperty("RemoteMoveSessionVersion", DefaultValue = 4L)]
		public long RemoteMoveSessionVersion
		{
			get
			{
				return this.InternalGetConfig<long>("RemoteMoveSessionVersion");
			}
			set
			{
				this.InternalSetConfig<long>(value, "RemoteMoveSessionVersion");
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000071E5 File Offset: 0x000053E5
		// (set) Token: 0x06000124 RID: 292 RVA: 0x000071F2 File Offset: 0x000053F2
		[LongValidator(MinValue = 1L, MaxValue = 5L, ExcludeRange = false)]
		[ConfigurationProperty("SessionCurrentVersion", DefaultValue = 2L)]
		public long SessionCurrentVersion
		{
			get
			{
				return this.InternalGetConfig<long>("SessionCurrentVersion");
			}
			set
			{
				this.InternalSetConfig<long>(value, "SessionCurrentVersion");
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00007200 File Offset: 0x00005400
		// (set) Token: 0x06000126 RID: 294 RVA: 0x0000720D File Offset: 0x0000540D
		[ConfigurationProperty("SyncMigrationEnabledMigrationTypes")]
		public string SyncMigrationEnabledMigrationsTypes
		{
			get
			{
				return this.InternalGetConfig<string>("SyncMigrationEnabledMigrationsTypes");
			}
			set
			{
				this.InternalSetConfig<string>(value, "SyncMigrationEnabledMigrationsTypes");
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000721B File Offset: 0x0000541B
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00007228 File Offset: 0x00005428
		[ConfigurationProperty("MigrationSlowOperationThreshold", DefaultValue = "00:05:00")]
		[TimeSpanValidator(MinValueString = "00:00:01", ExcludeRange = false)]
		public TimeSpan MigrationSlowOperationThreshold
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MigrationSlowOperationThreshold");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MigrationSlowOperationThreshold");
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00007236 File Offset: 0x00005436
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00007243 File Offset: 0x00005443
		[ConfigurationProperty("MigrationSourceNspiHttpPort", DefaultValue = 6004)]
		[IntegerValidator(MinValue = 1, MaxValue = 100000, ExcludeRange = false)]
		public int MigrationNspiPort
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationNspiPort");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationNspiPort");
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00007251 File Offset: 0x00005451
		// (set) Token: 0x0600012C RID: 300 RVA: 0x0000725E File Offset: 0x0000545E
		[ConfigurationProperty("MigrationSourceRfrHttpPort", DefaultValue = 6002)]
		[IntegerValidator(MinValue = 1, MaxValue = 100000, ExcludeRange = false)]
		public int MigrationNspiRfrPort
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationNspiRfrPort");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationNspiRfrPort");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000726C File Offset: 0x0000546C
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00007279 File Offset: 0x00005479
		[ConfigurationProperty("MigrationGroupMembersBatchSize", DefaultValue = 100)]
		[IntegerValidator(MinValue = 1, MaxValue = 100000, ExcludeRange = false)]
		public int MigrationGroupMembersBatchSize
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationGroupMembersBatchSize");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationGroupMembersBatchSize");
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00007287 File Offset: 0x00005487
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00007294 File Offset: 0x00005494
		[IntegerValidator(MinValue = 0, MaxValue = 100000, ExcludeRange = false)]
		[ConfigurationProperty("MaximumNumberOfBatchesPerSession", DefaultValue = 100)]
		public int MaximumNumberOfBatchesPerSession
		{
			get
			{
				return this.InternalGetConfig<int>("MaximumNumberOfBatchesPerSession");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaximumNumberOfBatchesPerSession");
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000072A2 File Offset: 0x000054A2
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000072AF File Offset: 0x000054AF
		[ConfigurationProperty("MigrationReportingLoggingEnabled", DefaultValue = true)]
		public bool MigrationReportingLoggingEnabledKey
		{
			get
			{
				return this.InternalGetConfig<bool>("MigrationReportingLoggingEnabledKey");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "MigrationReportingLoggingEnabledKey");
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000072BD File Offset: 0x000054BD
		// (set) Token: 0x06000134 RID: 308 RVA: 0x000072CA File Offset: 0x000054CA
		[ConfigurationProperty("MigrationReportingLoggingFolder", DefaultValue = "")]
		public string MigrationReportingLoggingFolder
		{
			get
			{
				return this.InternalGetConfig<string>("MigrationReportingLoggingFolder");
			}
			set
			{
				this.InternalSetConfig<string>(value, "MigrationReportingLoggingFolder");
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000072D8 File Offset: 0x000054D8
		// (set) Token: 0x06000136 RID: 310 RVA: 0x000072E5 File Offset: 0x000054E5
		[ConfigurationProperty("MigrationReportingMaxLogAge", DefaultValue = "10.0:00:00")]
		[TimeSpanValidator(MinValueString = "1.00:00:00", MaxValueString = "180.0:00:00", ExcludeRange = false)]
		public TimeSpan MigrationReportingMaxLogAge
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MigrationReportingMaxLogAge");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MigrationReportingMaxLogAge");
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000072F3 File Offset: 0x000054F3
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00007300 File Offset: 0x00005500
		[ConfigurationProperty("MigrationReportingJobMaxDirSize", DefaultValue = 1073741824L)]
		[LongValidator(MinValue = 16777216L, ExcludeRange = false)]
		public long MigrationReportingJobMaxDirSize
		{
			get
			{
				return this.InternalGetConfig<long>("MigrationReportingJobMaxDirSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "MigrationReportingJobMaxDirSize");
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000730E File Offset: 0x0000550E
		// (set) Token: 0x0600013A RID: 314 RVA: 0x0000731B File Offset: 0x0000551B
		[ConfigurationProperty("MigrationReportingJobItemMaxDirSize", DefaultValue = 1073741824L)]
		[LongValidator(MinValue = 16777216L, ExcludeRange = false)]
		public long MigrationReportingJobItemMaxDirSize
		{
			get
			{
				return this.InternalGetConfig<long>("MigrationReportingJobItemMaxDirSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "MigrationReportingJobItemMaxDirSize");
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00007329 File Offset: 0x00005529
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00007336 File Offset: 0x00005536
		[LongValidator(MinValue = 16777216L, ExcludeRange = false)]
		[ConfigurationProperty("MigrationReportingEndpointMaxDirSizeKey", DefaultValue = 1073741824L)]
		public long MigrationReportingEndpointMaxDirSize
		{
			get
			{
				return this.InternalGetConfig<long>("MigrationReportingEndpointMaxDirSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "MigrationReportingEndpointMaxDirSize");
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00007344 File Offset: 0x00005544
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00007351 File Offset: 0x00005551
		[ConfigurationProperty("MigrationReportingJobMaxFileSize", DefaultValue = 1073741824L)]
		[LongValidator(MinValue = 8388608L, ExcludeRange = false)]
		public long MigrationReportingJobMaxFileSize
		{
			get
			{
				return this.InternalGetConfig<long>("MigrationReportingJobMaxFileSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "MigrationReportingJobMaxFileSize");
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000735F File Offset: 0x0000555F
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000736C File Offset: 0x0000556C
		[ConfigurationProperty("MigrationReportingJobItemMaxFileSize", DefaultValue = 1073741824L)]
		[LongValidator(MinValue = 8388608L, ExcludeRange = false)]
		public long MigrationReportingJobItemMaxFileSize
		{
			get
			{
				return this.InternalGetConfig<long>("MigrationReportingJobItemMaxFileSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "MigrationReportingJobItemMaxFileSize");
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000737A File Offset: 0x0000557A
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00007387 File Offset: 0x00005587
		[ConfigurationProperty("MigrationReportingEndpointMaxFileSize", DefaultValue = 1073741824L)]
		[LongValidator(MinValue = 8388608L, ExcludeRange = false)]
		public long MigrationReportingEndpointMaxFileSize
		{
			get
			{
				return this.InternalGetConfig<long>("MigrationReportingEndpointMaxFileSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "MigrationReportingEndpointMaxFileSize");
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00007395 File Offset: 0x00005595
		// (set) Token: 0x06000144 RID: 324 RVA: 0x000073A2 File Offset: 0x000055A2
		[ConfigurationProperty("MigrationErrorTransitionThreshold", DefaultValue = 2)]
		[IntegerValidator(MinValue = 1, MaxValue = 100000, ExcludeRange = false)]
		public int MigrationErrorTransitionThreshold
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationErrorTransitionThreshold");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationErrorTransitionThreshold");
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000073B0 File Offset: 0x000055B0
		// (set) Token: 0x06000146 RID: 326 RVA: 0x000073BD File Offset: 0x000055BD
		[ConfigurationProperty("MigrationUpgradeConstraintExpirationPeriod", DefaultValue = "14.0:00:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", ExcludeRange = false)]
		public TimeSpan MigrationUpgradeConstraintExpirationPeriod
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MigrationUpgradeConstraintExpirationPeriod");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MigrationUpgradeConstraintExpirationPeriod");
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000073CB File Offset: 0x000055CB
		// (set) Token: 0x06000148 RID: 328 RVA: 0x000073D8 File Offset: 0x000055D8
		[ConfigurationProperty("MigrationUpgradeConstraintEnforcementPeriod", DefaultValue = "1.0:00:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", ExcludeRange = false)]
		public TimeSpan MigrationUpgradeConstraintEnforcementPeriod
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MigrationUpgradeConstraintEnforcementPeriod");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MigrationUpgradeConstraintEnforcementPeriod");
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000073E6 File Offset: 0x000055E6
		// (set) Token: 0x0600014A RID: 330 RVA: 0x000073F3 File Offset: 0x000055F3
		[TimeSpanValidator(MinValueString = "00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("SuspendedCacheEntryDelay", DefaultValue = "12:00:00")]
		public TimeSpan MigrationSuspendedCacheEntryDelay
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MigrationSuspendedCacheEntryDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MigrationSuspendedCacheEntryDelay");
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00007401 File Offset: 0x00005601
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000740E File Offset: 0x0000560E
		[ConfigurationProperty("BlockedMigrationFeatures", DefaultValue = MigrationFeature.None)]
		public MigrationFeature BlockedMigrationFeatures
		{
			get
			{
				return this.InternalGetConfig<MigrationFeature>("BlockedMigrationFeatures");
			}
			set
			{
				this.InternalSetConfig<MigrationFeature>(value, "BlockedMigrationFeatures");
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000741C File Offset: 0x0000561C
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00007429 File Offset: 0x00005629
		[ConfigurationProperty("PublishedMigrationFeatures", DefaultValue = MigrationFeature.MultiBatch)]
		public MigrationFeature PublishedMigrationFeatures
		{
			get
			{
				return this.InternalGetConfig<MigrationFeature>("PublishedMigrationFeatures");
			}
			set
			{
				this.InternalSetConfig<MigrationFeature>(value, "PublishedMigrationFeatures");
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00007437 File Offset: 0x00005637
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00007444 File Offset: 0x00005644
		[ConfigurationProperty("MigrationAsyncNotificationEnabled", DefaultValue = true)]
		public bool UseAsyncNotifications
		{
			get
			{
				return this.InternalGetConfig<bool>("UseAsyncNotifications");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "UseAsyncNotifications");
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00007452 File Offset: 0x00005652
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000745F File Offset: 0x0000565F
		[ConfigurationProperty("MigrationJobStoppedThreshold", DefaultValue = "30.0:00:00")]
		[TimeSpanValidator(MinValueString = "00:00:01", ExcludeRange = false)]
		public TimeSpan MigrationJobStoppedThreshold
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MigrationJobStoppedThreshold");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MigrationJobStoppedThreshold");
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000746D File Offset: 0x0000566D
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000747A File Offset: 0x0000567A
		[ConfigurationProperty("MigrationJobInactiveThreshold", DefaultValue = "30.0:00:00")]
		[TimeSpanValidator(MinValueString = "00:00:01", ExcludeRange = false)]
		public TimeSpan MigrationJobInactiveThreshold
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MigrationJobInactiveThreshold");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MigrationJobInactiveThreshold");
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00007488 File Offset: 0x00005688
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00007495 File Offset: 0x00005695
		[TimeSpanValidator(MinValueString = "00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("EndpointCountsRefreshThreshold", DefaultValue = "00:00:30")]
		public TimeSpan EndpointCountsRefreshThreshold
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("EndpointCountsRefreshThreshold");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "EndpointCountsRefreshThreshold");
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000074A3 File Offset: 0x000056A3
		// (set) Token: 0x06000158 RID: 344 RVA: 0x000074B0 File Offset: 0x000056B0
		[ConfigurationProperty("CacheEntrySuspendedDuration", DefaultValue = "06:00:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", ExcludeRange = false)]
		public TimeSpan CacheEntrySuspendedDuration
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("CacheEntrySuspendedDuration");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "CacheEntrySuspendedDuration");
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000074BE File Offset: 0x000056BE
		// (set) Token: 0x0600015A RID: 346 RVA: 0x000074CB File Offset: 0x000056CB
		[ConfigurationProperty("IssueCacheIsEnabled", DefaultValue = true)]
		public bool IssueCacheIsEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("IssueCacheIsEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "IssueCacheIsEnabled");
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000074D9 File Offset: 0x000056D9
		// (set) Token: 0x0600015C RID: 348 RVA: 0x000074E6 File Offset: 0x000056E6
		[TimeSpanValidator(MinValueString = "00:00:01", ExcludeRange = false)]
		[ConfigurationProperty("IssueCacheScanFrequency", DefaultValue = "02:00:00")]
		public TimeSpan IssueCacheScanFrequency
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("IssueCacheScanFrequency");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "IssueCacheScanFrequency");
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000074F4 File Offset: 0x000056F4
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00007501 File Offset: 0x00005701
		[IntegerValidator(MinValue = 0, MaxValue = 100000, ExcludeRange = false)]
		[ConfigurationProperty("IssueCacheItemLimit", DefaultValue = 50)]
		public int IssueCacheItemLimit
		{
			get
			{
				return this.InternalGetConfig<int>("IssueCacheItemLimit");
			}
			set
			{
				this.InternalSetConfig<int>(value, "IssueCacheItemLimit");
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000750F File Offset: 0x0000570F
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000751C File Offset: 0x0000571C
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		[ConfigurationProperty("MigrationIncrementalSyncFailureThreshold", DefaultValue = 30)]
		public int MigrationIncrementalSyncFailureThreshold
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationIncrementalSyncFailureThreshold");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationIncrementalSyncFailureThreshold");
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000752A File Offset: 0x0000572A
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00007537 File Offset: 0x00005737
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		[ConfigurationProperty("MigrationPublicFolderCompletionFailureThreshold", DefaultValue = 5)]
		public int MigrationPublicFolderCompletionFailureThreshold
		{
			get
			{
				return this.InternalGetConfig<int>("MigrationPublicFolderCompletionFailureThreshold");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MigrationPublicFolderCompletionFailureThreshold");
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00007545 File Offset: 0x00005745
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00007552 File Offset: 0x00005752
		[IntegerValidator(MinValue = 1, MaxValue = 100000, ExcludeRange = false)]
		[ConfigurationProperty("MaxReportItemsPerJob", DefaultValue = 10)]
		public int MaxReportItemsPerJob
		{
			get
			{
				return this.InternalGetConfig<int>("MaxReportItemsPerJob");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxReportItemsPerJob");
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00007560 File Offset: 0x00005760
		// (set) Token: 0x06000166 RID: 358 RVA: 0x0000756D File Offset: 0x0000576D
		[ConfigurationProperty("SendGenericWatson", DefaultValue = false)]
		public bool SendGenericWatson
		{
			get
			{
				return this.InternalGetConfig<bool>("SendGenericWatson");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "SendGenericWatson");
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000757B File Offset: 0x0000577B
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00007588 File Offset: 0x00005788
		[ConfigurationProperty("ReportInitial", DefaultValue = true)]
		public bool ReportInitial
		{
			get
			{
				return this.InternalGetConfig<bool>("ReportInitial");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "ReportInitial");
			}
		}

		// Token: 0x04000094 RID: 148
		public const int MaxIntValue = 100000;

		// Token: 0x04000095 RID: 149
		public const long OneGBSize = 1073741824L;

		// Token: 0x04000096 RID: 150
		public const long SixteenMBSize = 16777216L;

		// Token: 0x04000097 RID: 151
		public const long EightMBSize = 8388608L;
	}
}

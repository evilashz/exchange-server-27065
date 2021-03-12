using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000168 RID: 360
	[Serializable]
	internal class MRSConfigSchema : ConfigSchemaBase
	{
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0001EE12 File Offset: 0x0001D012
		public override string Name
		{
			get
			{
				return "MRS";
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0001EE19 File Offset: 0x0001D019
		public override string SectionName
		{
			get
			{
				return "MRSConfiguration";
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0001EE20 File Offset: 0x0001D020
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x0001EE48 File Offset: 0x0001D048
		[ConfigurationProperty("LoggingPath", DefaultValue = "")]
		public string LoggingPath
		{
			get
			{
				string text = this.InternalGetConfig<string>("LoggingPath");
				if (string.IsNullOrEmpty(text))
				{
					text = MRSConfigSchema.DefaultLoggingPath;
				}
				return text;
			}
			set
			{
				this.InternalSetConfig<string>(value, "LoggingPath");
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0001EE56 File Offset: 0x0001D056
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x0001EE63 File Offset: 0x0001D063
		[ConfigurationProperty("MrsVersion", DefaultValue = 1f)]
		public float MrsVersion
		{
			get
			{
				return this.InternalGetConfig<float>("MrsVersion");
			}
			set
			{
				this.InternalSetConfig<float>(value, "MrsVersion");
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0001EE71 File Offset: 0x0001D071
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x0001EE7E File Offset: 0x0001D07E
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "99999.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("MaxLogAge", DefaultValue = "30.00:00:00")]
		public TimeSpan MaxLogAge
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MaxLogAge");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MaxLogAge");
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0001EE8C File Offset: 0x0001D08C
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x0001EE99 File Offset: 0x0001D099
		[ConfigurationProperty("RequestLogEnabled", DefaultValue = false)]
		public bool RequestLogEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("RequestLogEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "RequestLogEnabled");
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0001EEA7 File Offset: 0x0001D0A7
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x0001EEB4 File Offset: 0x0001D0B4
		[LongValidator(MinValue = 0L, MaxValue = 1048576000L, ExcludeRange = false)]
		[ConfigurationProperty("RequestLogMaxDirSize", DefaultValue = "50000000")]
		public long RequestLogMaxDirSize
		{
			get
			{
				return this.InternalGetConfig<long>("RequestLogMaxDirSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "RequestLogMaxDirSize");
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0001EEC2 File Offset: 0x0001D0C2
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x0001EECF File Offset: 0x0001D0CF
		[LongValidator(MinValue = 0L, MaxValue = 10485760L, ExcludeRange = false)]
		[ConfigurationProperty("RequestLogMaxFileSize", DefaultValue = "500000")]
		public long RequestLogMaxFileSize
		{
			get
			{
				return this.InternalGetConfig<long>("RequestLogMaxFileSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "RequestLogMaxFileSize");
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0001EEDD File Offset: 0x0001D0DD
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x0001EEEA File Offset: 0x0001D0EA
		[ConfigurationProperty("IsEnabled", DefaultValue = true)]
		public bool IsEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("IsEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "IsEnabled");
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0001EEF8 File Offset: 0x0001D0F8
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x0001EF05 File Offset: 0x0001D105
		[ConfigurationProperty("IsJobPickupEnabled", DefaultValue = true)]
		public bool IsJobPickupEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("IsJobPickupEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "IsJobPickupEnabled");
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0001EF13 File Offset: 0x0001D113
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x0001EF20 File Offset: 0x0001D120
		[IntegerValidator(MinValue = 0, MaxValue = 1000, ExcludeRange = false)]
		[ConfigurationProperty("MaxRetries", DefaultValue = "60")]
		public int MaxRetries
		{
			get
			{
				return this.InternalGetConfig<int>("MaxRetries");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxRetries");
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0001EF2E File Offset: 0x0001D12E
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x0001EF3B File Offset: 0x0001D13B
		[ConfigurationProperty("WLMResourceStatsLogEnabled", DefaultValue = false)]
		public bool WLMResourceStatsLogEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("WLMResourceStatsLogEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "WLMResourceStatsLogEnabled");
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0001EF49 File Offset: 0x0001D149
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x0001EF56 File Offset: 0x0001D156
		[ConfigurationProperty("WLMResourceStatsLogMaxDirSize", DefaultValue = "50000000")]
		[LongValidator(MinValue = 0L, MaxValue = 1048576000L, ExcludeRange = false)]
		public long WLMResourceStatsLogMaxDirSize
		{
			get
			{
				return this.InternalGetConfig<long>("WLMResourceStatsLogMaxDirSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "WLMResourceStatsLogMaxDirSize");
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0001EF64 File Offset: 0x0001D164
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x0001EF71 File Offset: 0x0001D171
		[ConfigurationProperty("WLMResourceStatsLogMaxFileSize", DefaultValue = "500000")]
		[LongValidator(MinValue = 0L, MaxValue = 10485760L, ExcludeRange = false)]
		public long WLMResourceStatsLogMaxFileSize
		{
			get
			{
				return this.InternalGetConfig<long>("WLMResourceStatsLogMaxFileSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "WLMResourceStatsLogMaxFileSize");
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0001EF7F File Offset: 0x0001D17F
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x0001EF8C File Offset: 0x0001D18C
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("WLMResourceStatsLoggingPeriod", DefaultValue = "00:30:00")]
		public TimeSpan WLMResourceStatsLoggingPeriod
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("WLMResourceStatsLoggingPeriod");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "WLMResourceStatsLoggingPeriod");
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0001EF9A File Offset: 0x0001D19A
		// (set) Token: 0x06000CFD RID: 3325 RVA: 0x0001EFA7 File Offset: 0x0001D1A7
		[ConfigurationProperty("ShowJobPickupStatusInRequestStatisticsMessage", DefaultValue = true)]
		public bool ShowJobPickupStatusInRequestStatisticsMessage
		{
			get
			{
				return this.InternalGetConfig<bool>("ShowJobPickupStatusInRequestStatisticsMessage");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "ShowJobPickupStatusInRequestStatisticsMessage");
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0001EFB5 File Offset: 0x0001D1B5
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x0001EFC2 File Offset: 0x0001D1C2
		[ConfigurationProperty("MRSSettingsLogEnabled", DefaultValue = false)]
		public bool MRSSettingsLogEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("MRSSettingsLogEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "MRSSettingsLogEnabled");
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0001EFD0 File Offset: 0x0001D1D0
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x0001EFDD File Offset: 0x0001D1DD
		[TypeConverter(typeof(MRSSettingsLogCollection.MRSSettingsLogCollectionConverter))]
		[ConfigurationProperty("MRSSettingsLogList", DefaultValue = null)]
		public MRSSettingsLogCollection MRSSettingsLogList
		{
			get
			{
				return this.InternalGetConfig<MRSSettingsLogCollection>("MRSSettingsLogList");
			}
			set
			{
				this.InternalSetConfig<MRSSettingsLogCollection>(value, "MRSSettingsLogList");
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0001EFEB File Offset: 0x0001D1EB
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x0001EFF8 File Offset: 0x0001D1F8
		[LongValidator(MinValue = 0L, MaxValue = 1048576000L, ExcludeRange = false)]
		[ConfigurationProperty("MRSSettingsLogMaxDirSize", DefaultValue = "50000000")]
		public long MRSSettingsLogMaxDirSize
		{
			get
			{
				return this.InternalGetConfig<long>("MRSSettingsLogMaxDirSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "MRSSettingsLogMaxDirSize");
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0001F006 File Offset: 0x0001D206
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x0001F013 File Offset: 0x0001D213
		[LongValidator(MinValue = 0L, MaxValue = 10485760L, ExcludeRange = false)]
		[ConfigurationProperty("MRSSettingsLogMaxFileSize", DefaultValue = "500000")]
		public long MRSSettingsLogMaxFileSize
		{
			get
			{
				return this.InternalGetConfig<long>("MRSSettingsLogMaxFileSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "MRSSettingsLogMaxFileSize");
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0001F021 File Offset: 0x0001D221
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x0001F02E File Offset: 0x0001D22E
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("MRSSettingsLoggingPeriod", DefaultValue = "08:00:00")]
		public TimeSpan MRSSettingsLoggingPeriod
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MRSSettingsLoggingPeriod");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MRSSettingsLoggingPeriod");
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0001F03C File Offset: 0x0001D23C
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x0001F049 File Offset: 0x0001D249
		[ConfigurationProperty("MRSScheduledLogsCheckFrequency", DefaultValue = "00:10:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10.00:00:00", ExcludeRange = false)]
		public TimeSpan MRSScheduledLogsCheckFrequency
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MRSScheduledLogsCheckFrequency");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MRSScheduledLogsCheckFrequency");
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0001F057 File Offset: 0x0001D257
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x0001F064 File Offset: 0x0001D264
		[TimeSpanValidator(MinValueString = "00:00:10", MaxValueString = "00:30:00", ExcludeRange = false)]
		[ConfigurationProperty("RetryDelay", DefaultValue = "00:00:30")]
		public TimeSpan RetryDelay
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("RetryDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "RetryDelay");
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x0001F072 File Offset: 0x0001D272
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x0001F07F File Offset: 0x0001D27F
		[IntegerValidator(MinValue = 0, MaxValue = 600, ExcludeRange = false)]
		[ConfigurationProperty("MaxCleanupRetries", DefaultValue = "480")]
		public int MaxCleanupRetries
		{
			get
			{
				return this.InternalGetConfig<int>("MaxCleanupRetries");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxCleanupRetries");
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0001F08D File Offset: 0x0001D28D
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x0001F09A File Offset: 0x0001D29A
		[TimeSpanValidator(MinValueString = "00:00:10", MaxValueString = "05:00:00", ExcludeRange = false)]
		[ConfigurationProperty("MaxStallRetryPeriod", DefaultValue = "00:15:00")]
		public TimeSpan MaxStallRetryPeriod
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MaxStallRetryPeriod");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MaxStallRetryPeriod");
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0001F0A8 File Offset: 0x0001D2A8
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x0001F0B5 File Offset: 0x0001D2B5
		[ConfigurationProperty("ExportBufferSizeKB", DefaultValue = "128")]
		[IntegerValidator(MinValue = 1, MaxValue = 131072, ExcludeRange = false)]
		public int ExportBufferSizeKB
		{
			get
			{
				return this.InternalGetConfig<int>("ExportBufferSizeKB");
			}
			set
			{
				this.InternalSetConfig<int>(value, "ExportBufferSizeKB");
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0001F0C3 File Offset: 0x0001D2C3
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x0001F0D0 File Offset: 0x0001D2D0
		[ConfigurationProperty("ExportBufferSizeOverrideKB", DefaultValue = "0")]
		[IntegerValidator(MinValue = 0, MaxValue = 131072, ExcludeRange = false)]
		public int ExportBufferSizeOverrideKB
		{
			get
			{
				return this.InternalGetConfig<int>("ExportBufferSizeOverrideKB");
			}
			set
			{
				this.InternalSetConfig<int>(value, "ExportBufferSizeOverrideKB");
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0001F0DE File Offset: 0x0001D2DE
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x0001F0EB File Offset: 0x0001D2EB
		[ConfigurationProperty("MinBatchSize", DefaultValue = "300")]
		[IntegerValidator(MinValue = 1, MaxValue = 10000, ExcludeRange = false)]
		public int MinBatchSize
		{
			get
			{
				return this.InternalGetConfig<int>("MinBatchSize");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MinBatchSize");
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0001F0F9 File Offset: 0x0001D2F9
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x0001F106 File Offset: 0x0001D306
		[ConfigurationProperty("MinBatchSizeKB", DefaultValue = "7168")]
		[IntegerValidator(MinValue = 1, MaxValue = 131072, ExcludeRange = false)]
		public int MinBatchSizeKB
		{
			get
			{
				return this.InternalGetConfig<int>("MinBatchSizeKB");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MinBatchSizeKB");
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0001F114 File Offset: 0x0001D314
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x0001F121 File Offset: 0x0001D321
		[ConfigurationProperty("MaxMoveHistoryLength", DefaultValue = "5")]
		[IntegerValidator(MinValue = 0, MaxValue = 100, ExcludeRange = false)]
		public int MaxMoveHistoryLength
		{
			get
			{
				return this.InternalGetConfig<int>("MaxMoveHistoryLength");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxMoveHistoryLength");
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0001F12F File Offset: 0x0001D32F
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x0001F13C File Offset: 0x0001D33C
		[ConfigurationProperty("MaxActiveMovesPerSourceMDB", DefaultValue = "20")]
		[IntegerValidator(MinValue = 0, MaxValue = 100, ExcludeRange = false)]
		public int MaxActiveMovesPerSourceMDB
		{
			get
			{
				return this.InternalGetConfig<int>("MaxActiveMovesPerSourceMDB");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxActiveMovesPerSourceMDB");
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0001F14A File Offset: 0x0001D34A
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x0001F157 File Offset: 0x0001D357
		[ConfigurationProperty("MaxActiveMovesPerTargetMDB", DefaultValue = "20")]
		[IntegerValidator(MinValue = 0, MaxValue = 100, ExcludeRange = false)]
		public int MaxActiveMovesPerTargetMDB
		{
			get
			{
				return this.InternalGetConfig<int>("MaxActiveMovesPerTargetMDB");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxActiveMovesPerTargetMDB");
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0001F165 File Offset: 0x0001D365
		// (set) Token: 0x06000D1F RID: 3359 RVA: 0x0001F172 File Offset: 0x0001D372
		[IntegerValidator(MinValue = 0, MaxValue = 1000, ExcludeRange = false)]
		[ConfigurationProperty("MaxActiveMovesPerSourceServer", DefaultValue = "100")]
		public int MaxActiveMovesPerSourceServer
		{
			get
			{
				return this.InternalGetConfig<int>("MaxActiveMovesPerSourceServer");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxActiveMovesPerSourceServer");
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0001F180 File Offset: 0x0001D380
		// (set) Token: 0x06000D21 RID: 3361 RVA: 0x0001F18D File Offset: 0x0001D38D
		[IntegerValidator(MinValue = 0, MaxValue = 1000, ExcludeRange = false)]
		[ConfigurationProperty("MaxActiveMovesPerTargetServer", DefaultValue = "100")]
		public int MaxActiveMovesPerTargetServer
		{
			get
			{
				return this.InternalGetConfig<int>("MaxActiveMovesPerTargetServer");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxActiveMovesPerTargetServer");
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0001F19B File Offset: 0x0001D39B
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x0001F1A8 File Offset: 0x0001D3A8
		[ConfigurationProperty("MaxActiveJobsPerSourceMailbox", DefaultValue = "5")]
		[IntegerValidator(MinValue = 0, MaxValue = 100, ExcludeRange = false)]
		public int MaxActiveJobsPerSourceMailbox
		{
			get
			{
				return this.InternalGetConfig<int>("MaxActiveJobsPerSourceMailbox");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxActiveJobsPerSourceMailbox");
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0001F1B6 File Offset: 0x0001D3B6
		// (set) Token: 0x06000D25 RID: 3365 RVA: 0x0001F1C3 File Offset: 0x0001D3C3
		[IntegerValidator(MinValue = 0, MaxValue = 100, ExcludeRange = false)]
		[ConfigurationProperty("MaxActiveJobsPerTargetMailbox", DefaultValue = "2")]
		public int MaxActiveJobsPerTargetMailbox
		{
			get
			{
				return this.InternalGetConfig<int>("MaxActiveJobsPerTargetMailbox");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxActiveJobsPerTargetMailbox");
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0001F1D1 File Offset: 0x0001D3D1
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x0001F1DE File Offset: 0x0001D3DE
		[IntegerValidator(MinValue = 0, MaxValue = 1024, ExcludeRange = false)]
		[ConfigurationProperty("MaxTotalRequestsPerMRS", DefaultValue = "100")]
		public int MaxTotalRequestsPerMRS
		{
			get
			{
				return this.InternalGetConfig<int>("MaxTotalRequestsPerMRS");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxTotalRequestsPerMRS");
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x0001F1EC File Offset: 0x0001D3EC
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x0001F1F9 File Offset: 0x0001D3F9
		[ConfigurationProperty("FullScanMoveJobsPollingPeriod", DefaultValue = "00:15:00")]
		[TimeSpanValidator(MinValueString = "00:03:00", MaxValueString = "1.00:00:00", ExcludeRange = false)]
		public TimeSpan FullScanMoveJobsPollingPeriod
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("FullScanMoveJobsPollingPeriod");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "FullScanMoveJobsPollingPeriod");
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x0001F207 File Offset: 0x0001D407
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x0001F214 File Offset: 0x0001D414
		[ConfigurationProperty("FullScanLightJobsPollingPeriod", DefaultValue = "00:15:00")]
		[TimeSpanValidator(MinValueString = "00:00:30", MaxValueString = "1.00:00:00", ExcludeRange = false)]
		public TimeSpan FullScanLightJobsPollingPeriod
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("FullScanLightJobsPollingPeriod");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "FullScanLightJobsPollingPeriod");
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0001F222 File Offset: 0x0001D422
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x0001F22F File Offset: 0x0001D42F
		[ConfigurationProperty("ADInconsistencyCleanUpPeriod", DefaultValue = "1.00:00:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10.00:00:00", ExcludeRange = false)]
		public TimeSpan ADInconsistencyCleanUpPeriod
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ADInconsistencyCleanUpPeriod");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ADInconsistencyCleanUpPeriod");
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0001F23D File Offset: 0x0001D43D
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x0001F24A File Offset: 0x0001D44A
		[ConfigurationProperty("HeavyJobPickupPeriod", DefaultValue = "00:00:05")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10.00:00:00", ExcludeRange = false)]
		public TimeSpan HeavyJobPickupPeriod
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("HeavyJobPickupPeriod");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "HeavyJobPickupPeriod");
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0001F258 File Offset: 0x0001D458
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x0001F265 File Offset: 0x0001D465
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("LightJobPickupPeriod", DefaultValue = "00:00:10")]
		public TimeSpan LightJobPickupPeriod
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("LightJobPickupPeriod");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "LightJobPickupPeriod");
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0001F273 File Offset: 0x0001D473
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x0001F280 File Offset: 0x0001D480
		[ConfigurationProperty("MinimumDatabaseScanInterval", DefaultValue = "00:01:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "00:30:00", ExcludeRange = false)]
		public TimeSpan MinimumDatabaseScanInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MinimumDatabaseScanInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MinimumDatabaseScanInterval");
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0001F28E File Offset: 0x0001D48E
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x0001F29B File Offset: 0x0001D49B
		[TimeSpanValidator(MinValueString = "00:01:00", MaxValueString = "12:00:00", ExcludeRange = false)]
		[ConfigurationProperty("ReservationExpirationInterval", DefaultValue = "00:05:00")]
		public TimeSpan ReservationExpirationInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ReservationExpirationInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ReservationExpirationInterval");
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0001F2A9 File Offset: 0x0001D4A9
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x0001F2B6 File Offset: 0x0001D4B6
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "12:00:00", ExcludeRange = false)]
		[ConfigurationProperty("JobStuckDetectionTime", DefaultValue = "03:00:00")]
		public TimeSpan JobStuckDetectionTime
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("JobStuckDetectionTime");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "JobStuckDetectionTime");
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0001F2C4 File Offset: 0x0001D4C4
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x0001F2D1 File Offset: 0x0001D4D1
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "12:00:00", ExcludeRange = false)]
		[ConfigurationProperty("JobStuckDetectionWarmupTime", DefaultValue = "01:00:00")]
		public TimeSpan JobStuckDetectionWarmupTime
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("JobStuckDetectionWarmupTime");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "JobStuckDetectionWarmupTime");
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0001F2DF File Offset: 0x0001D4DF
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x0001F2EC File Offset: 0x0001D4EC
		[ConfigurationProperty("BackoffIntervalForProxyConnectionLimitReached", DefaultValue = "00:05:00")]
		[TimeSpanValidator(MinValueString = "00:00:30", MaxValueString = "1.00:00:00", ExcludeRange = false)]
		public TimeSpan BackoffIntervalForProxyConnectionLimitReached
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("BackoffIntervalForProxyConnectionLimitReached");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "BackoffIntervalForProxyConnectionLimitReached");
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x0001F2FA File Offset: 0x0001D4FA
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x0001F307 File Offset: 0x0001D507
		[TimeSpanValidator(MinValueString = "00:00:01", MaxValueString = "02:00:00", ExcludeRange = false)]
		[ConfigurationProperty("DataGuaranteeCheckPeriod", DefaultValue = "00:00:05")]
		public TimeSpan DataGuaranteeCheckPeriod
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("DataGuaranteeCheckPeriod");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "DataGuaranteeCheckPeriod");
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x0001F315 File Offset: 0x0001D515
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x0001F322 File Offset: 0x0001D522
		[ConfigurationProperty("EnableDataGuaranteeCheck", DefaultValue = true)]
		public bool EnableDataGuaranteeCheck
		{
			get
			{
				return this.InternalGetConfig<bool>("EnableDataGuaranteeCheck");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "EnableDataGuaranteeCheck");
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0001F330 File Offset: 0x0001D530
		// (set) Token: 0x06000D41 RID: 3393 RVA: 0x0001F33D File Offset: 0x0001D53D
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "12:00:00", ExcludeRange = false)]
		[ConfigurationProperty("DataGuaranteeTimeout", DefaultValue = "00:10:00")]
		public TimeSpan DataGuaranteeTimeout
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("DataGuaranteeTimeout");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "DataGuaranteeTimeout");
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0001F34B File Offset: 0x0001D54B
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x0001F358 File Offset: 0x0001D558
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "12:00:00", ExcludeRange = false)]
		[ConfigurationProperty("DataGuaranteeLogRollDelay", DefaultValue = "00:01:00")]
		public TimeSpan DataGuaranteeLogRollDelay
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("DataGuaranteeLogRollDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "DataGuaranteeLogRollDelay");
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x0001F366 File Offset: 0x0001D566
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x0001F373 File Offset: 0x0001D573
		[ConfigurationProperty("DataGuaranteeRetryInterval", DefaultValue = "00:15:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "12:00:00", ExcludeRange = false)]
		public TimeSpan DataGuaranteeRetryInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("DataGuaranteeRetryInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "DataGuaranteeRetryInterval");
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0001F381 File Offset: 0x0001D581
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x0001F38E File Offset: 0x0001D58E
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "7.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("DataGuaranteeMaxWait", DefaultValue = "1.00:00:00")]
		public TimeSpan DataGuaranteeMaxWait
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("DataGuaranteeMaxWait");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "DataGuaranteeMaxWait");
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0001F39C File Offset: 0x0001D59C
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x0001F3A9 File Offset: 0x0001D5A9
		[TimeSpanValidator(MinValueString = "00:00:01", MaxValueString = "00:01:00", ExcludeRange = false)]
		[ConfigurationProperty("DelayCheckPeriod", DefaultValue = "00:00:05")]
		public TimeSpan DelayCheckPeriod
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("DelayCheckPeriod");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "DelayCheckPeriod");
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x0001F3B7 File Offset: 0x0001D5B7
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x0001F3C4 File Offset: 0x0001D5C4
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "5.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("CanceledRequestAge", DefaultValue = "1.00:00:00")]
		public TimeSpan CanceledRequestAge
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("CanceledRequestAge");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "CanceledRequestAge");
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x0001F3D2 File Offset: 0x0001D5D2
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x0001F3DF File Offset: 0x0001D5DF
		[TimeSpanValidator(MinValueString = "00:00:05", MaxValueString = "1.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("OfflineMoveTransientFailureRelinquishPeriod", DefaultValue = "01:00:00")]
		public TimeSpan OfflineMoveTransientFailureRelinquishPeriod
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("OfflineMoveTransientFailureRelinquishPeriod");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "OfflineMoveTransientFailureRelinquishPeriod");
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0001F3ED File Offset: 0x0001D5ED
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x0001F3FA File Offset: 0x0001D5FA
		[ConfigurationProperty("DisableMrsProxyBuffering", DefaultValue = false)]
		public bool DisableMrsProxyBuffering
		{
			get
			{
				return this.InternalGetConfig<bool>("DisableMrsProxyBuffering");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "DisableMrsProxyBuffering");
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0001F408 File Offset: 0x0001D608
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x0001F454 File Offset: 0x0001D654
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "12:00:00", ExcludeRange = false)]
		[ConfigurationProperty("MailboxLockoutTimeout", DefaultValue = "02:00:00")]
		public TimeSpan MailboxLockoutTimeout
		{
			get
			{
				TimeSpan timeSpan = this.InternalGetConfig<TimeSpan>("MailboxLockoutTimeout");
				if (timeSpan != TimeSpan.Zero && timeSpan < TimeSpan.FromMinutes(1.0))
				{
					timeSpan = TimeSpan.FromMinutes(1.0);
				}
				return timeSpan;
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MailboxLockoutTimeout");
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x0001F462 File Offset: 0x0001D662
		// (set) Token: 0x06000D53 RID: 3411 RVA: 0x0001F46F File Offset: 0x0001D66F
		[TimeSpanValidator(MinValueString = "00:00:30", MaxValueString = "12:00:00", ExcludeRange = false)]
		[ConfigurationProperty("MailboxLockoutRetryInterval", DefaultValue = "00:05:00")]
		public TimeSpan MailboxLockoutRetryInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MailboxLockoutRetryInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MailboxLockoutRetryInterval");
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0001F47D File Offset: 0x0001D67D
		// (set) Token: 0x06000D55 RID: 3413 RVA: 0x0001F48A File Offset: 0x0001D68A
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "12:00:00", ExcludeRange = false)]
		[ConfigurationProperty("WlmThrottlingJobTimeout", DefaultValue = "00:05:00")]
		public TimeSpan WlmThrottlingJobTimeout
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("WlmThrottlingJobTimeout");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "WlmThrottlingJobTimeout");
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x0001F498 File Offset: 0x0001D698
		// (set) Token: 0x06000D57 RID: 3415 RVA: 0x0001F4A5 File Offset: 0x0001D6A5
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "12:00:00", ExcludeRange = false)]
		[ConfigurationProperty("WlmThrottlingJobRetryInterval", DefaultValue = "00:10:00")]
		public TimeSpan WlmThrottlingJobRetryInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("WlmThrottlingJobRetryInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "WlmThrottlingJobRetryInterval");
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0001F4B3 File Offset: 0x0001D6B3
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x0001F4C0 File Offset: 0x0001D6C0
		[ConfigurationProperty("MRSProxyLongOperationTimeout", DefaultValue = "00:20:00")]
		[TimeSpanValidator(MinValueString = "00:01:00", MaxValueString = "02:00:00", ExcludeRange = false)]
		public TimeSpan MRSProxyLongOperationTimeout
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("MRSProxyLongOperationTimeout");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "MRSProxyLongOperationTimeout");
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0001F4CE File Offset: 0x0001D6CE
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x0001F4DB File Offset: 0x0001D6DB
		[ConfigurationProperty("ContentVerificationIgnoreFAI", DefaultValue = false)]
		public bool ContentVerificationIgnoreFAI
		{
			get
			{
				return this.InternalGetConfig<bool>("ContentVerificationIgnoreFAI");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "ContentVerificationIgnoreFAI");
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0001F4E9 File Offset: 0x0001D6E9
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x0001F4F6 File Offset: 0x0001D6F6
		[ConfigurationProperty("ContentVerificationIgnorableMsgClasses", DefaultValue = "")]
		public string ContentVerificationIgnorableMsgClasses
		{
			get
			{
				return this.InternalGetConfig<string>("ContentVerificationIgnorableMsgClasses");
			}
			set
			{
				this.InternalSetConfig<string>(value, "ContentVerificationIgnorableMsgClasses");
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0001F504 File Offset: 0x0001D704
		// (set) Token: 0x06000D5F RID: 3423 RVA: 0x0001F511 File Offset: 0x0001D711
		[ConfigurationProperty("ContentVerificationMissingItemThreshold", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		public int ContentVerificationMissingItemThreshold
		{
			get
			{
				return this.InternalGetConfig<int>("ContentVerificationMissingItemThreshold");
			}
			set
			{
				this.InternalSetConfig<int>(value, "ContentVerificationMissingItemThreshold");
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0001F51F File Offset: 0x0001D71F
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x0001F52C File Offset: 0x0001D72C
		[ConfigurationProperty("DisableContentVerification", DefaultValue = false)]
		public bool DisableContentVerification
		{
			get
			{
				return this.InternalGetConfig<bool>("DisableContentVerification");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "DisableContentVerification");
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0001F53A File Offset: 0x0001D73A
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x0001F547 File Offset: 0x0001D747
		[IntegerValidator(MinValue = 0, MaxValue = 100, ExcludeRange = false)]
		[ConfigurationProperty("PoisonLimit", DefaultValue = 5)]
		public int PoisonLimit
		{
			get
			{
				return this.InternalGetConfig<int>("PoisonLimit");
			}
			set
			{
				this.InternalSetConfig<int>(value, "PoisonLimit");
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0001F555 File Offset: 0x0001D755
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x0001F562 File Offset: 0x0001D762
		[IntegerValidator(MinValue = 0, MaxValue = 100, ExcludeRange = false)]
		[ConfigurationProperty("HardPoisonLimit", DefaultValue = 20)]
		public int HardPoisonLimit
		{
			get
			{
				return this.InternalGetConfig<int>("HardPoisonLimit");
			}
			set
			{
				this.InternalSetConfig<int>(value, "HardPoisonLimit");
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0001F570 File Offset: 0x0001D770
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x0001F57D File Offset: 0x0001D77D
		[ConfigurationProperty("FailureHistoryLength", DefaultValue = 60)]
		[IntegerValidator(MinValue = 0, MaxValue = 1000, ExcludeRange = false)]
		public int FailureHistoryLength
		{
			get
			{
				return this.InternalGetConfig<int>("FailureHistoryLength");
			}
			set
			{
				this.InternalSetConfig<int>(value, "FailureHistoryLength");
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0001F58B File Offset: 0x0001D78B
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x0001F598 File Offset: 0x0001D798
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "7.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("LongRunningJobRelinquishInterval", DefaultValue = "04:00:00")]
		public TimeSpan LongRunningJobRelinquishInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("LongRunningJobRelinquishInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "LongRunningJobRelinquishInterval");
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x0001F5A6 File Offset: 0x0001D7A6
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x0001F5B3 File Offset: 0x0001D7B3
		[ConfigurationProperty("DisableDynamicThrottling", DefaultValue = true)]
		public bool DisableDynamicThrottling
		{
			get
			{
				return this.InternalGetConfig<bool>("DisableDynamicThrottling");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "DisableDynamicThrottling");
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x0001F5C1 File Offset: 0x0001D7C1
		// (set) Token: 0x06000D6D RID: 3437 RVA: 0x0001F5CE File Offset: 0x0001D7CE
		[ConfigurationProperty("UseExtendedAclInformation", DefaultValue = true)]
		public bool UseExtendedAclInformation
		{
			get
			{
				return this.InternalGetConfig<bool>("UseExtendedAclInformation");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "UseExtendedAclInformation");
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0001F5DC File Offset: 0x0001D7DC
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x0001F5E9 File Offset: 0x0001D7E9
		[ConfigurationProperty("SkipWordBreaking", DefaultValue = false)]
		public bool SkipWordBreaking
		{
			get
			{
				return this.InternalGetConfig<bool>("SkipWordBreaking");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "SkipWordBreaking");
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0001F5F7 File Offset: 0x0001D7F7
		// (set) Token: 0x06000D71 RID: 3441 RVA: 0x0001F604 File Offset: 0x0001D804
		[ConfigurationProperty("SkipKnownCorruptionsDefault", DefaultValue = false)]
		public bool SkipKnownCorruptionsDefault
		{
			get
			{
				return this.InternalGetConfig<bool>("SkipKnownCorruptionsDefault");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "SkipKnownCorruptionsDefault");
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0001F612 File Offset: 0x0001D812
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x0001F61F File Offset: 0x0001D81F
		[ConfigurationProperty("IgnoreHealthMonitor", DefaultValue = false)]
		public bool IgnoreHealthMonitor
		{
			get
			{
				return this.InternalGetConfig<bool>("IgnoreHealthMonitor");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "IgnoreHealthMonitor");
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0001F62D File Offset: 0x0001D82D
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x0001F63A File Offset: 0x0001D83A
		[TimeSpanValidator(MinValueString = "1", MaxValueString = "18250", ExcludeRange = false)]
		[ConfigurationProperty("OldItemAge", DefaultValue = "366")]
		public TimeSpan OldItemAge
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("OldItemAge");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "OldItemAge");
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0001F648 File Offset: 0x0001D848
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x0001F655 File Offset: 0x0001D855
		[ConfigurationProperty("BadItemLimitOldNonContact", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		public int BadItemLimitOldNonContact
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimitOldNonContact");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimitOldNonContact");
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0001F663 File Offset: 0x0001D863
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x0001F670 File Offset: 0x0001D870
		[ConfigurationProperty("BadItemLimitContact", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		public int BadItemLimitContact
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimitContact");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimitContact");
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0001F67E File Offset: 0x0001D87E
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x0001F68B File Offset: 0x0001D88B
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		[ConfigurationProperty("BadItemLimitDistributionList", DefaultValue = 0)]
		public int BadItemLimitDistributionList
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimitDistributionList");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimitDistributionList");
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0001F699 File Offset: 0x0001D899
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x0001F6A6 File Offset: 0x0001D8A6
		[ConfigurationProperty("BadItemLimitDefault", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		public int BadItemLimitDefault
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimitDefault");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimitDefault");
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0001F6B4 File Offset: 0x0001D8B4
		// (set) Token: 0x06000D7F RID: 3455 RVA: 0x0001F6C1 File Offset: 0x0001D8C1
		[ConfigurationProperty("BadItemLimitInDumpster", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		public int BadItemLimitInDumpster
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimitInDumpster");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimitInDumpster");
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x0001F6CF File Offset: 0x0001D8CF
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x0001F6DC File Offset: 0x0001D8DC
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		[ConfigurationProperty("BadItemLimitCalendarRecurrenceCorruption", DefaultValue = 0)]
		public int BadItemLimitCalendarRecurrenceCorruption
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimitCalendarRecurrenceCorruption");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimitCalendarRecurrenceCorruption");
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x0001F6EA File Offset: 0x0001D8EA
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x0001F6F7 File Offset: 0x0001D8F7
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		[ConfigurationProperty("BadItemLimitStartGreaterThanEndCalendarCorruption", DefaultValue = 0)]
		public int BadItemLimitStartGreaterThanEndCalendarCorruption
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimitStartGreaterThanEndCalendarCorruption");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimitStartGreaterThanEndCalendarCorruption");
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0001F705 File Offset: 0x0001D905
		// (set) Token: 0x06000D85 RID: 3461 RVA: 0x0001F712 File Offset: 0x0001D912
		[ConfigurationProperty("BadItemLimitConflictEntryIdCorruption", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		public int BadItemLimitConflictEntryIdCorruption
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimitConflictEntryIdCorruption");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimitConflictEntryIdCorruption");
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0001F720 File Offset: 0x0001D920
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x0001F72D File Offset: 0x0001D92D
		[ConfigurationProperty("BadItemLimitRecipientCorruption", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		public int BadItemLimitRecipientCorruption
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimitRecipientCorruption");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimitRecipientCorruption");
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0001F73B File Offset: 0x0001D93B
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x0001F748 File Offset: 0x0001D948
		[ConfigurationProperty("BadItemLimitUnifiedMessagingReportRecipientCorruption", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		public int BadItemLimitUnifiedMessagingReportRecipientCorruption
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimitUnifiedMessagingReportRecipientCorruption");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimitUnifiedMessagingReportRecipientCorruption");
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x0001F756 File Offset: 0x0001D956
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x0001F763 File Offset: 0x0001D963
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		[ConfigurationProperty("BadItemLimitNonCanonicalAclCorruption", DefaultValue = 0)]
		public int BadItemLimitNonCanonicalAclCorruption
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimitNonCanonicalAclCorruption");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimitNonCanonicalAclCorruption");
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0001F771 File Offset: 0x0001D971
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x0001F77E File Offset: 0x0001D97E
		[ConfigurationProperty("BadItemLimitStringArrayCorruption", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		public int BadItemLimitStringArrayCorruption
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimitStringArrayCorruption");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimitStringArrayCorruption");
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0001F78C File Offset: 0x0001D98C
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x0001F799 File Offset: 0x0001D999
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		[ConfigurationProperty("BadItemLimitInvalidMultivalueElementCorruption", DefaultValue = 0)]
		public int BadItemLimitInvalidMultivalueElementCorruption
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimitInvalidMultivalueElementCorruption");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimitInvalidMultivalueElementCorruption");
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x0001F7A7 File Offset: 0x0001D9A7
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
		[ConfigurationProperty("BadItemLimitNonUnicodeValueCorruption", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		public int BadItemLimitNonUnicodeValueCorruption
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimitNonUnicodeValueCorruption");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimitNonUnicodeValueCorruption");
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x0001F7C2 File Offset: 0x0001D9C2
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x0001F7CF File Offset: 0x0001D9CF
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		[ConfigurationProperty("BadItemLimitFolderPropertyMismatchCorruption", DefaultValue = 0)]
		public int BadItemLimitFolderPropertyMismatchCorruption
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimitFolderPropertyMismatchCorruption");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimitFolderPropertyMismatchCorruption");
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0001F7DD File Offset: 0x0001D9DD
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x0001F7EA File Offset: 0x0001D9EA
		[ConfigurationProperty("BadItemLimiFolderPropertyCorruption", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		public int BadItemLimiFolderPropertyCorruption
		{
			get
			{
				return this.InternalGetConfig<int>("BadItemLimiFolderPropertyCorruption");
			}
			set
			{
				this.InternalSetConfig<int>(value, "BadItemLimiFolderPropertyCorruption");
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0001F7F8 File Offset: 0x0001D9F8
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x0001F805 File Offset: 0x0001DA05
		[TimeSpanValidator(MinValueString = "00:00:10", MaxValueString = "05:00:00", ExcludeRange = false)]
		[ConfigurationProperty("InProgressRequestJobLogInterval", DefaultValue = "00:15:00")]
		public TimeSpan InProgressRequestJobLogInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("InProgressRequestJobLogInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "InProgressRequestJobLogInterval");
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0001F813 File Offset: 0x0001DA13
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x0001F820 File Offset: 0x0001DA20
		[ConfigurationProperty("FailureLogEnabled", DefaultValue = false)]
		public bool FailureLogEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("FailureLogEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "FailureLogEnabled");
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0001F82E File Offset: 0x0001DA2E
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x0001F83B File Offset: 0x0001DA3B
		[LongValidator(MinValue = 0L, MaxValue = 1048576000L, ExcludeRange = false)]
		[ConfigurationProperty("FailureLogMaxDirSize", DefaultValue = "50000000")]
		public long FailureLogMaxDirSize
		{
			get
			{
				return this.InternalGetConfig<long>("FailureLogMaxDirSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "FailureLogMaxDirSize");
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0001F849 File Offset: 0x0001DA49
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x0001F856 File Offset: 0x0001DA56
		[ConfigurationProperty("FailureLogMaxFileSize", DefaultValue = "500000")]
		[LongValidator(MinValue = 0L, MaxValue = 10485760L, ExcludeRange = false)]
		public long FailureLogMaxFileSize
		{
			get
			{
				return this.InternalGetConfig<long>("FailureLogMaxFileSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "FailureLogMaxFileSize");
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0001F864 File Offset: 0x0001DA64
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x0001F871 File Offset: 0x0001DA71
		[ConfigurationProperty("CommonFailureLogEnabled", DefaultValue = false)]
		public bool CommonFailureLogEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("CommonFailureLogEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "CommonFailureLogEnabled");
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0001F87F File Offset: 0x0001DA7F
		// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x0001F88C File Offset: 0x0001DA8C
		[LongValidator(MinValue = 0L, MaxValue = 1048576000L, ExcludeRange = false)]
		[ConfigurationProperty("CommonFailureLogMaxDirSize", DefaultValue = "50000000")]
		public long CommonFailureLogMaxDirSize
		{
			get
			{
				return this.InternalGetConfig<long>("CommonFailureLogMaxDirSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "CommonFailureLogMaxDirSize");
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x0001F89A File Offset: 0x0001DA9A
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x0001F8A7 File Offset: 0x0001DAA7
		[LongValidator(MinValue = 0L, MaxValue = 10485760L, ExcludeRange = false)]
		[ConfigurationProperty("CommonFailureLogMaxFileSize", DefaultValue = "500000")]
		public long CommonFailureLogMaxFileSize
		{
			get
			{
				return this.InternalGetConfig<long>("CommonFailureLogMaxFileSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "CommonFailureLogMaxFileSize");
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0001F8B5 File Offset: 0x0001DAB5
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x0001F8C2 File Offset: 0x0001DAC2
		[ConfigurationProperty("TraceLogMaxDirSize", DefaultValue = "50000000")]
		[LongValidator(MinValue = 0L, MaxValue = 1048576000L, ExcludeRange = false)]
		public long TraceLogMaxDirSize
		{
			get
			{
				return this.InternalGetConfig<long>("TraceLogMaxDirSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "TraceLogMaxDirSize");
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0001F8D0 File Offset: 0x0001DAD0
		// (set) Token: 0x06000DA7 RID: 3495 RVA: 0x0001F8DD File Offset: 0x0001DADD
		[ConfigurationProperty("TraceLogMaxFileSize", DefaultValue = "500000")]
		[LongValidator(MinValue = 0L, MaxValue = 10485760L, ExcludeRange = false)]
		public long TraceLogMaxFileSize
		{
			get
			{
				return this.InternalGetConfig<long>("TraceLogMaxFileSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "TraceLogMaxFileSize");
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0001F8EB File Offset: 0x0001DAEB
		// (set) Token: 0x06000DA9 RID: 3497 RVA: 0x0001F8F8 File Offset: 0x0001DAF8
		[ConfigurationProperty("TraceLogLevels", DefaultValue = "")]
		public string TraceLogLevels
		{
			get
			{
				return this.InternalGetConfig<string>("TraceLogLevels");
			}
			set
			{
				this.InternalSetConfig<string>(value, "TraceLogLevels");
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x0001F906 File Offset: 0x0001DB06
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x0001F913 File Offset: 0x0001DB13
		[ConfigurationProperty("TraceLogTracers", DefaultValue = "")]
		public string TraceLogTracers
		{
			get
			{
				return this.InternalGetConfig<string>("TraceLogTracers");
			}
			set
			{
				this.InternalSetConfig<string>(value, "TraceLogTracers");
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0001F921 File Offset: 0x0001DB21
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x0001F92E File Offset: 0x0001DB2E
		[ConfigurationProperty("BadItemLogEnabled", DefaultValue = false)]
		public bool BadItemLogEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("BadItemLogEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "BadItemLogEnabled");
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x0001F93C File Offset: 0x0001DB3C
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x0001F949 File Offset: 0x0001DB49
		[ConfigurationProperty("BadItemLogMaxDirSize", DefaultValue = "50000000")]
		[LongValidator(MinValue = 0L, MaxValue = 1048576000L, ExcludeRange = false)]
		public long BadItemLogMaxDirSize
		{
			get
			{
				return this.InternalGetConfig<long>("BadItemLogMaxDirSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "BadItemLogMaxDirSize");
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x0001F957 File Offset: 0x0001DB57
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x0001F964 File Offset: 0x0001DB64
		[ConfigurationProperty("BadItemLogMaxFileSize", DefaultValue = "500000")]
		[LongValidator(MinValue = 0L, MaxValue = 10485760L, ExcludeRange = false)]
		public long BadItemLogMaxFileSize
		{
			get
			{
				return this.InternalGetConfig<long>("BadItemLogMaxFileSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "BadItemLogMaxFileSize");
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x0001F972 File Offset: 0x0001DB72
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x0001F97F File Offset: 0x0001DB7F
		[ConfigurationProperty("SessionStatisticsLogEnabled", DefaultValue = false)]
		public bool SessionStatisticsLogEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("SessionStatisticsLogEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "SessionStatisticsLogEnabled");
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x0001F98D File Offset: 0x0001DB8D
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x0001F99A File Offset: 0x0001DB9A
		[ConfigurationProperty("SessionStatisticsLogMaxDirSize", DefaultValue = "50000000")]
		[LongValidator(MinValue = 0L, MaxValue = 1048576000L, ExcludeRange = false)]
		public long SessionStatisticsLogMaxDirSize
		{
			get
			{
				return this.InternalGetConfig<long>("SessionStatisticsLogMaxDirSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "SessionStatisticsLogMaxDirSize");
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0001F9A8 File Offset: 0x0001DBA8
		// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x0001F9B5 File Offset: 0x0001DBB5
		[ConfigurationProperty("SessionStatisticsLogMaxFileSize", DefaultValue = "500000")]
		[LongValidator(MinValue = 0L, MaxValue = 10485760L, ExcludeRange = false)]
		public long SessionStatisticsLogMaxFileSize
		{
			get
			{
				return this.InternalGetConfig<long>("SessionStatisticsLogMaxFileSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "SessionStatisticsLogMaxFileSize");
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0001F9C3 File Offset: 0x0001DBC3
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x0001F9D0 File Offset: 0x0001DBD0
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

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0001F9DE File Offset: 0x0001DBDE
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x0001F9EB File Offset: 0x0001DBEB
		[ConfigurationProperty("MaxIncrementalChanges", DefaultValue = 1000)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		public int MaxIncrementalChanges
		{
			get
			{
				return this.InternalGetConfig<int>("MaxIncrementalChanges");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxIncrementalChanges");
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x0001F9F9 File Offset: 0x0001DBF9
		// (set) Token: 0x06000DBD RID: 3517 RVA: 0x0001FA06 File Offset: 0x0001DC06
		[ConfigurationProperty("IssueCacheItemLimit", DefaultValue = 50)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
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

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x0001FA14 File Offset: 0x0001DC14
		// (set) Token: 0x06000DBF RID: 3519 RVA: 0x0001FA21 File Offset: 0x0001DC21
		[ConfigurationProperty("IssueCacheScanFrequency", DefaultValue = "2:00:00")]
		[TimeSpanValidator(MinValueString = "00:01:00", MaxValueString = "365.00:00:00", ExcludeRange = false)]
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

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x0001FA2F File Offset: 0x0001DC2F
		// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x0001FA3C File Offset: 0x0001DC3C
		[ConfigurationProperty("CheckInitialProvisioningForMoves", DefaultValue = "true")]
		public bool CheckInitialProvisioningForMoves
		{
			get
			{
				return this.InternalGetConfig<bool>("CheckInitialProvisioningForMoves");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "CheckInitialProvisioningForMoves");
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0001FA4A File Offset: 0x0001DC4A
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x0001FA57 File Offset: 0x0001DC57
		[ConfigurationProperty("SendGenericWatson", DefaultValue = "false")]
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

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0001FA65 File Offset: 0x0001DC65
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x0001FA72 File Offset: 0x0001DC72
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647, ExcludeRange = false)]
		[ConfigurationProperty("CrawlerPageSize", DefaultValue = 10)]
		public int CrawlerPageSize
		{
			get
			{
				return this.InternalGetConfig<int>("CrawlerPageSize");
			}
			set
			{
				this.InternalSetConfig<int>(value, "CrawlerPageSize");
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0001FA80 File Offset: 0x0001DC80
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x0001FA8D File Offset: 0x0001DC8D
		[ConfigurationProperty("EnumerateMessagesPageSize", DefaultValue = 500)]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647, ExcludeRange = false)]
		public int EnumerateMessagesPageSize
		{
			get
			{
				return this.InternalGetConfig<int>("EnumerateMessagesPageSize");
			}
			set
			{
				this.InternalSetConfig<int>(value, "EnumerateMessagesPageSize");
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0001FA9B File Offset: 0x0001DC9B
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x0001FAA8 File Offset: 0x0001DCA8
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647, ExcludeRange = false)]
		[ConfigurationProperty("MaxFolderOpened", DefaultValue = 10)]
		public int MaxFolderOpened
		{
			get
			{
				return this.InternalGetConfig<int>("MaxFolderOpened");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaxFolderOpened");
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0001FAB6 File Offset: 0x0001DCB6
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x0001FAC3 File Offset: 0x0001DCC3
		[TimeSpanValidator(MinValueString = "00:00:01", MaxValueString = "00:01:00", ExcludeRange = false)]
		[ConfigurationProperty("CrawlAndCopyFolderTimeout", DefaultValue = "00:00:10")]
		public TimeSpan CrawlAndCopyFolderTimeout
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("CrawlAndCopyFolderTimeout");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "CrawlAndCopyFolderTimeout");
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0001FAD1 File Offset: 0x0001DCD1
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0001FADE File Offset: 0x0001DCDE
		[ConfigurationProperty("CopyInferenceProperties", DefaultValue = true)]
		public bool CopyInferenceProperties
		{
			get
			{
				return this.InternalGetConfig<bool>("CopyInferenceProperties");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "CopyInferenceProperties");
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0001FAEC File Offset: 0x0001DCEC
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x0001FAF9 File Offset: 0x0001DCF9
		[IntegerValidator(MinValue = 8192, MaxValue = 2147483647, ExcludeRange = false)]
		[ConfigurationProperty("MrsBindingMaxMessageSize", DefaultValue = 35000000)]
		public int MrsBindingMaxMessageSize
		{
			get
			{
				return this.InternalGetConfig<int>("MrsBindingMaxMessageSize");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MrsBindingMaxMessageSize");
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0001FB07 File Offset: 0x0001DD07
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x0001FB14 File Offset: 0x0001DD14
		[ConfigurationProperty("DCNameValidityInterval", DefaultValue = "02:00:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "7.00:00:00", ExcludeRange = false)]
		public TimeSpan DCNameValidityInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("DCNameValidityInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "DCNameValidityInterval");
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0001FB22 File Offset: 0x0001DD22
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0001FB2F File Offset: 0x0001DD2F
		[ConfigurationProperty("ActivatedJobIncrementalSyncInterval", DefaultValue = "00:01:00")]
		[TimeSpanValidator(MinValueString = "00:00:05", MaxValueString = "1.00:00:00", ExcludeRange = false)]
		public TimeSpan ActivatedJobIncrementalSyncInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ActivatedJobIncrementalSyncInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ActivatedJobIncrementalSyncInterval");
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0001FB3D File Offset: 0x0001DD3D
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x0001FB4A File Offset: 0x0001DD4A
		[TimeSpanValidator(MinValueString = "00:01:00", MaxValueString = "1.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("DeactivateJobInterval", DefaultValue = "00:17:00")]
		public TimeSpan DeactivateJobInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("DeactivateJobInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "DeactivateJobInterval");
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0001FB58 File Offset: 0x0001DD58
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x0001FB65 File Offset: 0x0001DD65
		[ConfigurationProperty("AllAggregationSyncJobsInteractive", DefaultValue = "false")]
		public bool AllAggregationSyncJobsInteractive
		{
			get
			{
				return this.InternalGetConfig<bool>("AllAggregationSyncJobsInteractive");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "AllAggregationSyncJobsInteractive");
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0001FB73 File Offset: 0x0001DD73
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x0001FB80 File Offset: 0x0001DD80
		[ConfigurationProperty("GetActionsPageSize", DefaultValue = 1000)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647, ExcludeRange = false)]
		public int GetActionsPageSize
		{
			get
			{
				return this.InternalGetConfig<int>("GetActionsPageSize");
			}
			set
			{
				this.InternalSetConfig<int>(value, "GetActionsPageSize");
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0001FB8E File Offset: 0x0001DD8E
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x0001FB9B File Offset: 0x0001DD9B
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "365.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("ReconnectAbandonInterval", DefaultValue = "2.00:00:00")]
		public TimeSpan ReconnectAbandonInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ReconnectAbandonInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ReconnectAbandonInterval");
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0001FBA9 File Offset: 0x0001DDA9
		// (set) Token: 0x06000DDD RID: 3549 RVA: 0x0001FBB6 File Offset: 0x0001DDB6
		[ConfigurationProperty("DisableAutomaticRepair", DefaultValue = "false")]
		public bool DisableAutomaticRepair
		{
			get
			{
				return this.InternalGetConfig<bool>("DisableAutomaticRepair");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "DisableAutomaticRepair");
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0001FBC4 File Offset: 0x0001DDC4
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x0001FBD1 File Offset: 0x0001DDD1
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "1.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("AutomaticRepairAbandonInterval", DefaultValue = "03:00:00")]
		public TimeSpan AutomaticRepairAbandonInterval
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("AutomaticRepairAbandonInterval");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "AutomaticRepairAbandonInterval");
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0001FBDF File Offset: 0x0001DDDF
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x0001FBEC File Offset: 0x0001DDEC
		[ConfigurationProperty("AdditionalSidsForMrsProxyAuthorization", DefaultValue = null)]
		internal string AdditionalSidsForMrsProxyAuthorization
		{
			get
			{
				return this.InternalGetConfig<string>("AdditionalSidsForMrsProxyAuthorization");
			}
			set
			{
				this.InternalSetConfig<string>(value, "AdditionalSidsForMrsProxyAuthorization");
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0001FBFA File Offset: 0x0001DDFA
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x0001FC07 File Offset: 0x0001DE07
		[ConfigurationProperty("ProxyClientTrustedCertificateThumbprints", DefaultValue = null)]
		internal string ProxyClientTrustedCertificateThumbprints
		{
			get
			{
				return this.InternalGetConfig<string>("ProxyClientTrustedCertificateThumbprints");
			}
			set
			{
				this.InternalSetConfig<string>(value, "ProxyClientTrustedCertificateThumbprints");
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0001FC15 File Offset: 0x0001DE15
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x0001FC22 File Offset: 0x0001DE22
		[ConfigurationProperty("AllowRestoreFromConnectedMailbox", DefaultValue = false)]
		internal bool AllowRestoreFromConnectedMailbox
		{
			get
			{
				return this.InternalGetConfig<bool>("AllowRestoreFromConnectedMailbox");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "AllowRestoreFromConnectedMailbox");
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0001FC30 File Offset: 0x0001DE30
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x0001FC3D File Offset: 0x0001DE3D
		[ConfigurationProperty("CheckMailUserPlanQuotasForOnboarding", DefaultValue = true)]
		internal bool CheckMailUserPlanQuotasForOnboarding
		{
			get
			{
				return this.InternalGetConfig<bool>("CheckMailUserPlanQuotasForOnboarding");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "CheckMailUserPlanQuotasForOnboarding");
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x0001FC4B File Offset: 0x0001DE4B
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x0001FC58 File Offset: 0x0001DE58
		[ConfigurationProperty("CacheJobQueues", DefaultValue = false)]
		internal bool CacheJobQueues
		{
			get
			{
				return this.InternalGetConfig<bool>("CacheJobQueues");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "CacheJobQueues");
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0001FC66 File Offset: 0x0001DE66
		// (set) Token: 0x06000DEB RID: 3563 RVA: 0x0001FC73 File Offset: 0x0001DE73
		[ConfigurationProperty("QuarantineEnabled", DefaultValue = false)]
		internal bool QuarantineEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("QuarantineEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "QuarantineEnabled");
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x0001FC81 File Offset: 0x0001DE81
		// (set) Token: 0x06000DED RID: 3565 RVA: 0x0001FC8E File Offset: 0x0001DE8E
		[ConfigurationProperty("StalledByHigherPriorityJobsTimeout", DefaultValue = "3.00:00:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "365.00:00:00", ExcludeRange = false)]
		public TimeSpan StalledByHigherPriorityJobsTimeout
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("StalledByHigherPriorityJobsTimeout");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "StalledByHigherPriorityJobsTimeout");
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0001FC9C File Offset: 0x0001DE9C
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x0001FCA9 File Offset: 0x0001DEA9
		[ConfigurationProperty("CanStoreCreatePFDumpster", DefaultValue = true)]
		internal bool CanStoreCreatePFDumpster
		{
			get
			{
				return this.InternalGetConfig<bool>("CanStoreCreatePFDumpster");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "CanStoreCreatePFDumpster");
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0001FCB7 File Offset: 0x0001DEB7
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x0001FCC4 File Offset: 0x0001DEC4
		[ConfigurationProperty("DisableContactSync", DefaultValue = false)]
		internal bool DisableContactSync
		{
			get
			{
				return this.InternalGetConfig<bool>("DisableContactSync");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "DisableContactSync");
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0001FCD2 File Offset: 0x0001DED2
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x0001FCDF File Offset: 0x0001DEDF
		[TimeSpanValidator(MinValueString = "00:05:00", MaxValueString = "1.00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("ServerBusyBackoffExpired", DefaultValue = "01:00:00")]
		public TimeSpan ServerBusyBackoffExpired
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ServerBusyBackoffExpired");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ServerBusyBackoffExpired");
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0001FCED File Offset: 0x0001DEED
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x0001FCFA File Offset: 0x0001DEFA
		[ConfigurationProperty("CanExportFoldersInBatch", DefaultValue = true)]
		internal bool CanExportFoldersInBatch
		{
			get
			{
				return this.InternalGetConfig<bool>("CanExportFoldersInBatch");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "CanExportFoldersInBatch");
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0001FD08 File Offset: 0x0001DF08
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x0001FD15 File Offset: 0x0001DF15
		[ConfigurationProperty("ChangesPerIcsManifestPage", DefaultValue = 500)]
		[IntegerValidator(MinValue = 1, MaxValue = 50000, ExcludeRange = false)]
		internal int ChangesPerIcsManifestPage
		{
			get
			{
				return this.InternalGetConfig<int>("ChangesPerIcsManifestPage");
			}
			set
			{
				this.InternalSetConfig<int>(value, "ChangesPerIcsManifestPage");
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0001FD23 File Offset: 0x0001DF23
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x0001FD30 File Offset: 0x0001DF30
		[ConfigurationProperty("FoldersPerHierarchySyncBatch", DefaultValue = 500)]
		[IntegerValidator(MinValue = 1, MaxValue = 50000, ExcludeRange = false)]
		internal int FoldersPerHierarchySyncBatch
		{
			get
			{
				return this.InternalGetConfig<int>("FoldersPerHierarchySyncBatch");
			}
			set
			{
				this.InternalSetConfig<int>(value, "FoldersPerHierarchySyncBatch");
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0001FD3E File Offset: 0x0001DF3E
		// (set) Token: 0x06000DFB RID: 3579 RVA: 0x0001FD4B File Offset: 0x0001DF4B
		[ConfigurationProperty("ProxyClientCertificateSubject", DefaultValue = "CN=outlook.com, OU=Exchange, O=Microsoft Corporation, L=Redmond, S=Washington, C=US")]
		internal string ProxyClientCertificateSubject
		{
			get
			{
				return this.InternalGetConfig<string>("ProxyClientCertificateSubject");
			}
			set
			{
				this.InternalSetConfig<string>(value, "ProxyClientCertificateSubject");
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0001FD59 File Offset: 0x0001DF59
		// (set) Token: 0x06000DFD RID: 3581 RVA: 0x0001FD66 File Offset: 0x0001DF66
		[ConfigurationProperty("ProxyServiceCertificateEndpointEnabled", DefaultValue = false)]
		internal bool ProxyServiceCertificateEndpointEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("ProxyServiceCertificateEndpointEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "ProxyServiceCertificateEndpointEnabled");
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0001FD74 File Offset: 0x0001DF74
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x0001FD81 File Offset: 0x0001DF81
		[ConfigurationProperty("CrossResourceForestEnabled", DefaultValue = false)]
		internal bool CrossResourceForestEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("CrossResourceForestEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "CrossResourceForestEnabled");
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0001FD8F File Offset: 0x0001DF8F
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x0001FD9C File Offset: 0x0001DF9C
		[ConfigurationProperty("OwnerLogonToMergeDestination", DefaultValue = true)]
		internal bool OwnerLogonToMergeDestination
		{
			get
			{
				return this.InternalGetConfig<bool>("OwnerLogonToMergeDestination");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "OwnerLogonToMergeDestination");
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0001FDAA File Offset: 0x0001DFAA
		// (set) Token: 0x06000E03 RID: 3587 RVA: 0x0001FDB7 File Offset: 0x0001DFB7
		[ConfigurationProperty("WlmWorkloadType", DefaultValue = WorkloadType.MailboxReplicationService)]
		public WorkloadType WlmWorkloadType
		{
			get
			{
				return this.InternalGetConfig<WorkloadType>("WlmWorkloadType");
			}
			set
			{
				this.InternalSetConfig<WorkloadType>(value, "WlmWorkloadType");
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0001FDC5 File Offset: 0x0001DFC5
		// (set) Token: 0x06000E05 RID: 3589 RVA: 0x0001FDD2 File Offset: 0x0001DFD2
		[ConfigurationProperty("DisabledFeatures", DefaultValue = MRSConfigurableFeatures.None)]
		public MRSConfigurableFeatures DisabledFeatures
		{
			get
			{
				return this.InternalGetConfig<MRSConfigurableFeatures>("DisabledFeatures");
			}
			set
			{
				this.InternalSetConfig<MRSConfigurableFeatures>(value, "DisabledFeatures");
			}
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0001FDE0 File Offset: 0x0001DFE0
		public MRSConfigSchema()
		{
			if (CommonUtils.IsMultiTenantEnabled())
			{
				base.SetDefaultConfigValue<bool>("RequestLogEnabled", true);
				base.SetDefaultConfigValue<bool>("FailureLogEnabled", true);
				base.SetDefaultConfigValue<bool>("CommonFailureLogEnabled", true);
				base.SetDefaultConfigValue<bool>("BadItemLogEnabled", true);
				base.SetDefaultConfigValue<bool>("SessionStatisticsLogEnabled", true);
				base.SetDefaultConfigValue<bool>("WLMResourceStatsLogEnabled", true);
				base.SetDefaultConfigValue<bool>("ShowJobPickupStatusInRequestStatisticsMessage", false);
				base.SetDefaultConfigValue<bool>("MRSSettingsLogEnabled", true);
				base.SetDefaultConfigValue<MRSSettingsLogCollection>("MRSSettingsLogList", new MRSSettingsLogCollection(string.Join(";", new string[]
				{
					"IsJobPickupEnabled",
					"MaxActiveMovesPerSourceMDB",
					"MaxActiveMovesPerTargetMDB",
					"MaxActiveMovesPerSourceServer",
					"MaxActiveMovesPerTargetServer",
					"MaxActiveJobsPerSourceMailbox",
					"MaxActiveJobsPerTargetMailbox",
					"MaxTotalRequestsPerMRS",
					"DisableDynamicThrottling",
					"IgnoreHealthMonitor"
				})));
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0001FED1 File Offset: 0x0001E0D1
		protected override ExchangeConfigurationSection ScopeSchema
		{
			get
			{
				return MRSConfigSchema.scopeSchema;
			}
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0001FED8 File Offset: 0x0001E0D8
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			if (ConfigBase<MRSConfigSchema>.Provider.IsInitialized)
			{
				MrsTracer.Service.Warning("Ignoring unrecognized configuration attribute {0}={1}", new object[]
				{
					name,
					value
				});
			}
			return base.OnDeserializeUnrecognizedAttribute(name, value);
		}

		// Token: 0x0400074C RID: 1868
		private static readonly MRSConfigSchema.MrsScopeSchema scopeSchema = new MRSConfigSchema.MrsScopeSchema();

		// Token: 0x0400074D RID: 1869
		public static readonly string DefaultLoggingPath = Path.Combine(CommonUtils.SafeInstallPath, "Logging\\MailboxReplicationService");

		// Token: 0x02000169 RID: 361
		[Serializable]
		public static class Setting
		{
			// Token: 0x0400074E RID: 1870
			public const string MrsVersion = "MrsVersion";

			// Token: 0x0400074F RID: 1871
			public const string IsEnabled = "IsEnabled";

			// Token: 0x04000750 RID: 1872
			public const string IsJobPickupEnabled = "IsJobPickupEnabled";

			// Token: 0x04000751 RID: 1873
			public const string MaxRetries = "MaxRetries";

			// Token: 0x04000752 RID: 1874
			public const string MaxCleanupRetries = "MaxCleanupRetries";

			// Token: 0x04000753 RID: 1875
			public const string MaxStallRetryPeriod = "MaxStallRetryPeriod";

			// Token: 0x04000754 RID: 1876
			public const string RetryDelay = "RetryDelay";

			// Token: 0x04000755 RID: 1877
			public const string ExportBufferSizeKBDefaultValue = "128";

			// Token: 0x04000756 RID: 1878
			public const string ExportBufferSizeKB = "ExportBufferSizeKB";

			// Token: 0x04000757 RID: 1879
			public const string ExportBufferSizeOverrideKB = "ExportBufferSizeOverrideKB";

			// Token: 0x04000758 RID: 1880
			public const string MinBatchSize = "MinBatchSize";

			// Token: 0x04000759 RID: 1881
			public const string MinBatchSizeKB = "MinBatchSizeKB";

			// Token: 0x0400075A RID: 1882
			public const string MaxMoveHistoryLength = "MaxMoveHistoryLength";

			// Token: 0x0400075B RID: 1883
			public const string MaxActiveMovesPerSourceMDB = "MaxActiveMovesPerSourceMDB";

			// Token: 0x0400075C RID: 1884
			public const string MaxActiveMovesPerTargetMDB = "MaxActiveMovesPerTargetMDB";

			// Token: 0x0400075D RID: 1885
			public const string MaxActiveMovesPerSourceServer = "MaxActiveMovesPerSourceServer";

			// Token: 0x0400075E RID: 1886
			public const string MaxActiveMovesPerTargetServer = "MaxActiveMovesPerTargetServer";

			// Token: 0x0400075F RID: 1887
			public const string MaxTotalRequestsPerMRS = "MaxTotalRequestsPerMRS";

			// Token: 0x04000760 RID: 1888
			public const string MaxActiveJobsPerSourceMailbox = "MaxActiveJobsPerSourceMailbox";

			// Token: 0x04000761 RID: 1889
			public const string MaxActiveJobsPerTargetMailbox = "MaxActiveJobsPerTargetMailbox";

			// Token: 0x04000762 RID: 1890
			public const string FullScanMoveJobsPollingPeriod = "FullScanMoveJobsPollingPeriod";

			// Token: 0x04000763 RID: 1891
			public const string FullScanLightJobsPollingPeriod = "FullScanLightJobsPollingPeriod";

			// Token: 0x04000764 RID: 1892
			public const string ADInconsistencyCleanUpPeriod = "ADInconsistencyCleanUpPeriod";

			// Token: 0x04000765 RID: 1893
			public const string HeavyJobPickupPeriod = "HeavyJobPickupPeriod";

			// Token: 0x04000766 RID: 1894
			public const string LightJobPickupPeriod = "LightJobPickupPeriod";

			// Token: 0x04000767 RID: 1895
			public const string MinimumDatabaseScanInterval = "MinimumDatabaseScanInterval";

			// Token: 0x04000768 RID: 1896
			public const string MRSAbandonedMoveJobDetectionTime = "MRSAbandonedMoveJobDetectionTime";

			// Token: 0x04000769 RID: 1897
			public const string JobStuckDetectionTime = "JobStuckDetectionTime";

			// Token: 0x0400076A RID: 1898
			public const string JobStuckDetectionWarmupTime = "JobStuckDetectionWarmupTime";

			// Token: 0x0400076B RID: 1899
			public const string BackoffIntervalForProxyConnectionLimitReached = "BackoffIntervalForProxyConnectionLimitReached";

			// Token: 0x0400076C RID: 1900
			public const string DataGuaranteeCheckPeriod = "DataGuaranteeCheckPeriod";

			// Token: 0x0400076D RID: 1901
			public const string EnableDataGuaranteeCheck = "EnableDataGuaranteeCheck";

			// Token: 0x0400076E RID: 1902
			public const string DisableMrsProxyBuffering = "DisableMrsProxyBuffering";

			// Token: 0x0400076F RID: 1903
			public const string DataGuaranteeTimeout = "DataGuaranteeTimeout";

			// Token: 0x04000770 RID: 1904
			public const string DataGuaranteeLogRollDelay = "DataGuaranteeLogRollDelay";

			// Token: 0x04000771 RID: 1905
			public const string DataGuaranteeRetryInterval = "DataGuaranteeRetryInterval";

			// Token: 0x04000772 RID: 1906
			public const string DataGuaranteeMaxWait = "DataGuaranteeMaxWait";

			// Token: 0x04000773 RID: 1907
			public const string DelayCheckPeriod = "DelayCheckPeriod";

			// Token: 0x04000774 RID: 1908
			public const string CanceledRequestAge = "CanceledRequestAge";

			// Token: 0x04000775 RID: 1909
			public const string OfflineMoveTransientFailureRelinquishPeriod = "OfflineMoveTransientFailureRelinquishPeriod";

			// Token: 0x04000776 RID: 1910
			public const string MailboxLockoutTimeout = "MailboxLockoutTimeout";

			// Token: 0x04000777 RID: 1911
			public const string MailboxLockoutRetryInterval = "MailboxLockoutRetryInterval";

			// Token: 0x04000778 RID: 1912
			public const string WlmThrottlingJobTimeout = "WlmThrottlingJobTimeout";

			// Token: 0x04000779 RID: 1913
			public const string WlmThrottlingJobRetryInterval = "WlmThrottlingJobRetryInterval";

			// Token: 0x0400077A RID: 1914
			public const string MRSProxyLongOperationTimeoutDefaultValue = "00:20:00";

			// Token: 0x0400077B RID: 1915
			public const string MRSProxyLongOperationTimeout = "MRSProxyLongOperationTimeout";

			// Token: 0x0400077C RID: 1916
			public const string ContentVerificationMissingItemThreshold = "ContentVerificationMissingItemThreshold";

			// Token: 0x0400077D RID: 1917
			public const string ContentVerificationIgnoreFAI = "ContentVerificationIgnoreFAI";

			// Token: 0x0400077E RID: 1918
			public const string ContentVerificationIgnorableMsgClasses = "ContentVerificationIgnorableMsgClasses";

			// Token: 0x0400077F RID: 1919
			public const string DisableContentVerification = "DisableContentVerification";

			// Token: 0x04000780 RID: 1920
			public const string PoisonLimit = "PoisonLimit";

			// Token: 0x04000781 RID: 1921
			public const string HardPoisonLimit = "HardPoisonLimit";

			// Token: 0x04000782 RID: 1922
			public const string ReservationExpirationInterval = "ReservationExpirationInterval";

			// Token: 0x04000783 RID: 1923
			public const string DisableDynamicThrottling = "DisableDynamicThrottling";

			// Token: 0x04000784 RID: 1924
			public const string UseExtendedAclInformation = "UseExtendedAclInformation";

			// Token: 0x04000785 RID: 1925
			public const string SkipWordBreaking = "SkipWordBreaking";

			// Token: 0x04000786 RID: 1926
			public const string SkipKnownCorruptionsDefault = "SkipKnownCorruptionsDefault";

			// Token: 0x04000787 RID: 1927
			public const string IgnoreHealthMonitor = "IgnoreHealthMonitor";

			// Token: 0x04000788 RID: 1928
			public const string FailureHistoryLength = "FailureHistoryLength";

			// Token: 0x04000789 RID: 1929
			public const string LongRunningJobRelinquishInterval = "LongRunningJobRelinquishInterval";

			// Token: 0x0400078A RID: 1930
			public const string LoggingPath = "LoggingPath";

			// Token: 0x0400078B RID: 1931
			public const string MaxLogAge = "MaxLogAge";

			// Token: 0x0400078C RID: 1932
			public const string RequestLogEnabled = "RequestLogEnabled";

			// Token: 0x0400078D RID: 1933
			public const string RequestLogMaxDirSize = "RequestLogMaxDirSize";

			// Token: 0x0400078E RID: 1934
			public const string RequestLogMaxFileSize = "RequestLogMaxFileSize";

			// Token: 0x0400078F RID: 1935
			public const string FailureLogEnabled = "FailureLogEnabled";

			// Token: 0x04000790 RID: 1936
			public const string FailureLogMaxDirSize = "FailureLogMaxDirSize";

			// Token: 0x04000791 RID: 1937
			public const string FailureLogMaxFileSize = "FailureLogMaxFileSize";

			// Token: 0x04000792 RID: 1938
			public const string CommonFailureLogEnabled = "CommonFailureLogEnabled";

			// Token: 0x04000793 RID: 1939
			public const string CommonFailureLogMaxDirSize = "CommonFailureLogMaxDirSize";

			// Token: 0x04000794 RID: 1940
			public const string CommonFailureLogMaxFileSize = "CommonFailureLogMaxFileSize";

			// Token: 0x04000795 RID: 1941
			public const string TraceLogMaxDirSize = "TraceLogMaxDirSize";

			// Token: 0x04000796 RID: 1942
			public const string TraceLogMaxFileSize = "TraceLogMaxFileSize";

			// Token: 0x04000797 RID: 1943
			public const string TraceLogLevels = "TraceLogLevels";

			// Token: 0x04000798 RID: 1944
			public const string TraceLogTracers = "TraceLogTracers";

			// Token: 0x04000799 RID: 1945
			public const string WLMResourceStatsLogEnabled = "WLMResourceStatsLogEnabled";

			// Token: 0x0400079A RID: 1946
			public const string WLMResourceStatsLogMaxDirSize = "WLMResourceStatsLogMaxDirSize";

			// Token: 0x0400079B RID: 1947
			public const string WLMResourceStatsLogMaxFileSize = "WLMResourceStatsLogMaxFileSize";

			// Token: 0x0400079C RID: 1948
			public const string WLMResourceStatsLoggingPeriod = "WLMResourceStatsLoggingPeriod";

			// Token: 0x0400079D RID: 1949
			public const string ShowJobPickupStatusInRequestStatisticsMessage = "ShowJobPickupStatusInRequestStatisticsMessage";

			// Token: 0x0400079E RID: 1950
			public const string MRSSettingsLogEnabled = "MRSSettingsLogEnabled";

			// Token: 0x0400079F RID: 1951
			public const string MRSSettingsLogList = "MRSSettingsLogList";

			// Token: 0x040007A0 RID: 1952
			public const string MRSSettingsLogMaxDirSize = "MRSSettingsLogMaxDirSize";

			// Token: 0x040007A1 RID: 1953
			public const string MRSSettingsLogMaxFileSize = "MRSSettingsLogMaxFileSize";

			// Token: 0x040007A2 RID: 1954
			public const string MRSSettingsLoggingPeriod = "MRSSettingsLoggingPeriod";

			// Token: 0x040007A3 RID: 1955
			public const string MRSScheduledLogsCheckFrequency = "MRSScheduledLogsCheckFrequency";

			// Token: 0x040007A4 RID: 1956
			public const string OldItemAge = "OldItemAge";

			// Token: 0x040007A5 RID: 1957
			public const string BadItemLimitOldNonContact = "BadItemLimitOldNonContact";

			// Token: 0x040007A6 RID: 1958
			public const string BadItemLimitContact = "BadItemLimitContact";

			// Token: 0x040007A7 RID: 1959
			public const string BadItemLimitDistributionList = "BadItemLimitDistributionList";

			// Token: 0x040007A8 RID: 1960
			public const string BadItemLimitDefault = "BadItemLimitDefault";

			// Token: 0x040007A9 RID: 1961
			public const string BadItemLimitCalendarRecurrenceCorruption = "BadItemLimitCalendarRecurrenceCorruption";

			// Token: 0x040007AA RID: 1962
			public const string BadItemLimitInDumpster = "BadItemLimitInDumpster";

			// Token: 0x040007AB RID: 1963
			public const string BadItemLimitStartGreaterThanEndCalendarCorruption = "BadItemLimitStartGreaterThanEndCalendarCorruption";

			// Token: 0x040007AC RID: 1964
			public const string BadItemLimitConflictEntryIdCorruption = "BadItemLimitConflictEntryIdCorruption";

			// Token: 0x040007AD RID: 1965
			public const string BadItemLimitRecipientCorruption = "BadItemLimitRecipientCorruption";

			// Token: 0x040007AE RID: 1966
			public const string BadItemLimitUnifiedMessagingReportRecipientCorruption = "BadItemLimitUnifiedMessagingReportRecipientCorruption";

			// Token: 0x040007AF RID: 1967
			public const string BadItemLimitNonCanonicalAclCorruption = "BadItemLimitNonCanonicalAclCorruption";

			// Token: 0x040007B0 RID: 1968
			public const string BadItemLimitStringArrayCorruption = "BadItemLimitStringArrayCorruption";

			// Token: 0x040007B1 RID: 1969
			public const string BadItemLimitInvalidMultivalueElementCorruption = "BadItemLimitInvalidMultivalueElementCorruption";

			// Token: 0x040007B2 RID: 1970
			public const string BadItemLimitNonUnicodeValueCorruption = "BadItemLimitNonUnicodeValueCorruption";

			// Token: 0x040007B3 RID: 1971
			public const string BadItemLimitFolderPropertyMismatchCorruption = "BadItemLimitFolderPropertyMismatchCorruption";

			// Token: 0x040007B4 RID: 1972
			public const string BadItemLimiFolderPropertyCorruption = "BadItemLimiFolderPropertyCorruption";

			// Token: 0x040007B5 RID: 1973
			public const string BadItemLogEnabled = "BadItemLogEnabled";

			// Token: 0x040007B6 RID: 1974
			public const string BadItemLogMaxDirSize = "BadItemLogMaxDirSize";

			// Token: 0x040007B7 RID: 1975
			public const string BadItemLogMaxFileSize = "BadItemLogMaxFileSize";

			// Token: 0x040007B8 RID: 1976
			public const string SessionStatisticsLogEnabled = "SessionStatisticsLogEnabled";

			// Token: 0x040007B9 RID: 1977
			public const string SessionStatisticsLogMaxDirSize = "SessionStatisticsLogMaxDirSize";

			// Token: 0x040007BA RID: 1978
			public const string SessionStatisticsLogMaxFileSize = "SessionStatisticsLogMaxFileSize";

			// Token: 0x040007BB RID: 1979
			public const string InProgressRequestJobLogInterval = "InProgressRequestJobLogInterval";

			// Token: 0x040007BC RID: 1980
			public const string IssueCacheIsEnabled = "IssueCacheIsEnabled";

			// Token: 0x040007BD RID: 1981
			public const string MaxIncrementalChanges = "MaxIncrementalChanges";

			// Token: 0x040007BE RID: 1982
			public const string IssueCacheItemLimit = "IssueCacheItemLimit";

			// Token: 0x040007BF RID: 1983
			public const string IssueCacheScanFrequency = "IssueCacheScanFrequency";

			// Token: 0x040007C0 RID: 1984
			public const string CheckInitialProvisioningForMoves = "CheckInitialProvisioningForMoves";

			// Token: 0x040007C1 RID: 1985
			public const string SendGenericWatson = "SendGenericWatson";

			// Token: 0x040007C2 RID: 1986
			public const string CrawlerPageSize = "CrawlerPageSize";

			// Token: 0x040007C3 RID: 1987
			public const string EnumerateMessagesPageSize = "EnumerateMessagesPageSize";

			// Token: 0x040007C4 RID: 1988
			public const string MaxFolderOpened = "MaxFolderOpened";

			// Token: 0x040007C5 RID: 1989
			public const string CrawlAndCopyFolderTimeout = "CrawlAndCopyFolderTimeout";

			// Token: 0x040007C6 RID: 1990
			public const string CopyInferenceProperties = "CopyInferenceProperties";

			// Token: 0x040007C7 RID: 1991
			public const string MrsBindingMaxMessageSize = "MrsBindingMaxMessageSize";

			// Token: 0x040007C8 RID: 1992
			public const string DCNameValidityInterval = "DCNameValidityInterval";

			// Token: 0x040007C9 RID: 1993
			public const string DeactivateJobInterval = "DeactivateJobInterval";

			// Token: 0x040007CA RID: 1994
			public const string ActivatedJobIncrementalSyncInterval = "ActivatedJobIncrementalSyncInterval";

			// Token: 0x040007CB RID: 1995
			public const string AllAggregationSyncJobsInteractive = "AllAggregationSyncJobsInteractive";

			// Token: 0x040007CC RID: 1996
			public const string GetActionsPageSize = "GetActionsPageSize";

			// Token: 0x040007CD RID: 1997
			public const string ReconnectAbandonInterval = "ReconnectAbandonInterval";

			// Token: 0x040007CE RID: 1998
			public const string DisableAutomaticRepair = "DisableAutomaticRepair";

			// Token: 0x040007CF RID: 1999
			public const string AutomaticRepairAbandonInterval = "AutomaticRepairAbandonInterval";

			// Token: 0x040007D0 RID: 2000
			public const string AdditionalSidsForMrsProxyAuthorization = "AdditionalSidsForMrsProxyAuthorization";

			// Token: 0x040007D1 RID: 2001
			public const string ProxyClientTrustedCertificateThumbprints = "ProxyClientTrustedCertificateThumbprints";

			// Token: 0x040007D2 RID: 2002
			public const string AllowRestoreFromConnectedMailbox = "AllowRestoreFromConnectedMailbox";

			// Token: 0x040007D3 RID: 2003
			public const string CheckMailUserPlanQuotasForOnboarding = "CheckMailUserPlanQuotasForOnboarding";

			// Token: 0x040007D4 RID: 2004
			public const string CacheJobQueues = "CacheJobQueues";

			// Token: 0x040007D5 RID: 2005
			public const string StalledByHigherPriorityJobsTimeout = "StalledByHigherPriorityJobsTimeout";

			// Token: 0x040007D6 RID: 2006
			public const string CanStoreCreatePFDumpster = "CanStoreCreatePFDumpster";

			// Token: 0x040007D7 RID: 2007
			public const string DisableContactSync = "DisableContactSync";

			// Token: 0x040007D8 RID: 2008
			public const string QuarantineEnabled = "QuarantineEnabled";

			// Token: 0x040007D9 RID: 2009
			public const string ServerBusyBackoffExpired = "ServerBusyBackoffExpired";

			// Token: 0x040007DA RID: 2010
			public const string CanExportFoldersInBatch = "CanExportFoldersInBatch";

			// Token: 0x040007DB RID: 2011
			public const string ChangesPerIcsManifestPage = "ChangesPerIcsManifestPage";

			// Token: 0x040007DC RID: 2012
			public const string FoldersPerHierarchySyncBatch = "FoldersPerHierarchySyncBatch";

			// Token: 0x040007DD RID: 2013
			public const string ProxyClientCertificateSubject = "ProxyClientCertificateSubject";

			// Token: 0x040007DE RID: 2014
			public const string ProxyServiceCertificateEndpointEnabled = "ProxyServiceCertificateEndpointEnabled";

			// Token: 0x040007DF RID: 2015
			public const string CrossResourceForestEnabled = "CrossResourceForestEnabled";

			// Token: 0x040007E0 RID: 2016
			public const string WlmWorkloadType = "WlmWorkloadType";

			// Token: 0x040007E1 RID: 2017
			public const string OwnerLogonToMergeDestination = "OwnerLogonToMergeDestination";

			// Token: 0x040007E2 RID: 2018
			public const string DisabledFeatures = "DisabledFeatures";
		}

		// Token: 0x0200016A RID: 362
		[Serializable]
		public static class Scope
		{
			// Token: 0x040007E3 RID: 2019
			public const string RequestWorkloadType = "RequestWorkloadType";

			// Token: 0x040007E4 RID: 2020
			public const string WlmHealthMonitor = "WlmHealthMonitor";

			// Token: 0x040007E5 RID: 2021
			public const string FailureType = "FailureType";

			// Token: 0x040007E6 RID: 2022
			public const string WorkloadType = "WorkloadType";

			// Token: 0x040007E7 RID: 2023
			public const string RequestType = "RequestType";

			// Token: 0x040007E8 RID: 2024
			public const string SyncProtocol = "SyncProtocol";
		}

		// Token: 0x0200016B RID: 363
		[Serializable]
		private class MrsScopeSchema : ExchangeConfigurationSection
		{
			// Token: 0x1700045F RID: 1119
			// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0001FF38 File Offset: 0x0001E138
			// (set) Token: 0x06000E0B RID: 3595 RVA: 0x0001FF45 File Offset: 0x0001E145
			[ConfigurationProperty("RequestWorkloadType", DefaultValue = RequestWorkloadType.None)]
			public RequestWorkloadType RequestWorkloadType
			{
				get
				{
					return this.InternalGetConfig<RequestWorkloadType>("RequestWorkloadType");
				}
				set
				{
					this.InternalSetConfig<RequestWorkloadType>(value, "RequestWorkloadType");
				}
			}

			// Token: 0x17000460 RID: 1120
			// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0001FF53 File Offset: 0x0001E153
			// (set) Token: 0x06000E0D RID: 3597 RVA: 0x0001FF60 File Offset: 0x0001E160
			[ConfigurationProperty("WlmHealthMonitor", DefaultValue = "")]
			public string WlmHealthMonitor
			{
				get
				{
					return this.InternalGetConfig<string>("WlmHealthMonitor");
				}
				set
				{
					this.InternalSetConfig<string>(value, "WlmHealthMonitor");
				}
			}

			// Token: 0x17000461 RID: 1121
			// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0001FF6E File Offset: 0x0001E16E
			// (set) Token: 0x06000E0F RID: 3599 RVA: 0x0001FF7B File Offset: 0x0001E17B
			[ConfigurationProperty("FailureType", DefaultValue = "")]
			public string FailureType
			{
				get
				{
					return this.InternalGetConfig<string>("FailureType");
				}
				set
				{
					this.InternalSetConfig<string>(value, "FailureType");
				}
			}

			// Token: 0x17000462 RID: 1122
			// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0001FF89 File Offset: 0x0001E189
			// (set) Token: 0x06000E11 RID: 3601 RVA: 0x0001FF96 File Offset: 0x0001E196
			[ConfigurationProperty("WorkloadType", DefaultValue = WorkloadType.MailboxReplicationService)]
			public WorkloadType WorkloadType
			{
				get
				{
					return this.InternalGetConfig<WorkloadType>("WorkloadType");
				}
				set
				{
					this.InternalSetConfig<WorkloadType>(value, "WorkloadType");
				}
			}

			// Token: 0x17000463 RID: 1123
			// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0001FFA4 File Offset: 0x0001E1A4
			// (set) Token: 0x06000E13 RID: 3603 RVA: 0x0001FFB1 File Offset: 0x0001E1B1
			[ConfigurationProperty("RequestType", DefaultValue = MRSRequestType.Move)]
			public MRSRequestType RequestType
			{
				get
				{
					return this.InternalGetConfig<MRSRequestType>("RequestType");
				}
				set
				{
					this.InternalSetConfig<MRSRequestType>(value, "RequestType");
				}
			}

			// Token: 0x17000464 RID: 1124
			// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0001FFBF File Offset: 0x0001E1BF
			// (set) Token: 0x06000E15 RID: 3605 RVA: 0x0001FFCC File Offset: 0x0001E1CC
			[ConfigurationProperty("SyncProtocol", DefaultValue = SyncProtocol.None)]
			public SyncProtocol SyncProtocol
			{
				get
				{
					return this.InternalGetConfig<SyncProtocol>("SyncProtocol");
				}
				set
				{
					this.InternalSetConfig<SyncProtocol>(value, "SyncProtocol");
				}
			}
		}
	}
}

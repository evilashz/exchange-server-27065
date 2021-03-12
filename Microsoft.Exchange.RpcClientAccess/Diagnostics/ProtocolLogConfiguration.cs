using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x02000033 RID: 51
	internal sealed class ProtocolLogConfiguration
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x00006C50 File Offset: 0x00004E50
		public ProtocolLogConfiguration(ConfigurationPropertyBag propertyBag)
		{
			propertyBag.Freeze();
			this.configurationPropertyBag = propertyBag;
			this.isLoggingEnabledCache = propertyBag.Get<bool>(ProtocolLogConfiguration.Schema.IsLoggingEnabled);
			this.enabledTagsCache = propertyBag.Get<ProtocolLoggingTag>(ProtocolLogConfiguration.Schema.EnabledTags);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006C87 File Offset: 0x00004E87
		public static void SetDefaults(string logFilePath, string logTypeName, string logFilePrefix, string logComponent)
		{
			ProtocolLogConfiguration.defaultLogFilePath = logFilePath;
			ProtocolLogConfiguration.defaultLogTypeName = logTypeName;
			ProtocolLogConfiguration.defaultLogFilePrefix = logFilePrefix;
			ProtocolLogConfiguration.defaultLogComponent = logComponent;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00006CA1 File Offset: 0x00004EA1
		public string SoftwareName
		{
			get
			{
				return "Microsoft Exchange";
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00006CA8 File Offset: 0x00004EA8
		public string SoftwareVersion
		{
			get
			{
				return "15.00.1497.010";
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00006CAF File Offset: 0x00004EAF
		public string LogTypeName
		{
			get
			{
				return ProtocolLogConfiguration.defaultLogTypeName ?? "RCA Protocol Logs";
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00006CBF File Offset: 0x00004EBF
		public string LogFilePrefix
		{
			get
			{
				return ProtocolLogConfiguration.defaultLogFilePrefix ?? "RCA_";
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00006CCF File Offset: 0x00004ECF
		public string LogComponent
		{
			get
			{
				return ProtocolLogConfiguration.defaultLogComponent ?? "RCAProtocolLogs";
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00006CDF File Offset: 0x00004EDF
		public bool IsEnabled
		{
			get
			{
				return this.isLoggingEnabledCache;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00006CE7 File Offset: 0x00004EE7
		public string LogFilePath
		{
			get
			{
				return this.configurationPropertyBag.Get<string>(ProtocolLogConfiguration.Schema.LogFilePath).Replace(ProtocolLogConfiguration.ExchangeInstallDirEnvironmentVariable, this.configurationPropertyBag.Get<string>(ProtocolLogConfiguration.Schema.ExchangeInstallPath));
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00006D13 File Offset: 0x00004F13
		public TimeSpan AgeQuota
		{
			get
			{
				return this.configurationPropertyBag.Get<TimeSpan>(ProtocolLogConfiguration.Schema.AgeQuota);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00006D25 File Offset: 0x00004F25
		public long DirectorySizeQuota
		{
			get
			{
				return this.configurationPropertyBag.Get<long>(ProtocolLogConfiguration.Schema.DirectorySizeQuota);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00006D37 File Offset: 0x00004F37
		public long PerFileSizeQuota
		{
			get
			{
				return this.configurationPropertyBag.Get<long>(ProtocolLogConfiguration.Schema.PerFileSizeQuota);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00006D49 File Offset: 0x00004F49
		public bool ApplyHourPrecision
		{
			get
			{
				return this.configurationPropertyBag.Get<bool>(ProtocolLogConfiguration.Schema.ApplyHourPrecision);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00006D5B File Offset: 0x00004F5B
		public ProtocolLoggingTag EnabledTags
		{
			get
			{
				return this.enabledTagsCache;
			}
		}

		// Token: 0x0400017A RID: 378
		internal const string LogPathConfigKey = "LogPath";

		// Token: 0x0400017B RID: 379
		internal const string LoggingEnabledConfigKey = "ProtocolLoggingEnabled";

		// Token: 0x0400017C RID: 380
		internal const string LoggingTagConfigKey = "LoggingTag";

		// Token: 0x0400017D RID: 381
		private const string SoftwareNameValue = "Microsoft Exchange";

		// Token: 0x0400017E RID: 382
		private const string SoftwareVersionValue = "15.00.1497.010";

		// Token: 0x0400017F RID: 383
		private const string LogTypeNameValue = "RCA Protocol Logs";

		// Token: 0x04000180 RID: 384
		private const string LogFilePrefixValue = "RCA_";

		// Token: 0x04000181 RID: 385
		private const string LogComponentValue = "RCAProtocolLogs";

		// Token: 0x04000182 RID: 386
		public static ProtocolLogConfiguration Default = new ProtocolLogConfiguration(new ConfigurationPropertyBag(null, null));

		// Token: 0x04000183 RID: 387
		internal static string ExchangeInstallDirEnvironmentVariable = "%ExchangeInstallDir%";

		// Token: 0x04000184 RID: 388
		private static string defaultLogFilePath = null;

		// Token: 0x04000185 RID: 389
		private static string defaultLogTypeName = null;

		// Token: 0x04000186 RID: 390
		private static string defaultLogFilePrefix = null;

		// Token: 0x04000187 RID: 391
		private static string defaultLogComponent = null;

		// Token: 0x04000188 RID: 392
		private readonly ConfigurationPropertyBag configurationPropertyBag;

		// Token: 0x04000189 RID: 393
		private readonly bool isLoggingEnabledCache;

		// Token: 0x0400018A RID: 394
		private readonly ProtocolLoggingTag enabledTagsCache;

		// Token: 0x02000034 RID: 52
		internal class Schema : ConfigurationSchema<ProtocolLogConfiguration.Schema>
		{
			// Token: 0x060001F1 RID: 497 RVA: 0x00006D98 File Offset: 0x00004F98
			private static bool TryKilobytesToBytes(ulong kiloBytes, out long bytes)
			{
				bytes = (long)(kiloBytes * 1024UL);
				return kiloBytes <= 9007199254740991UL;
			}

			// Token: 0x0400018B RID: 395
			private static readonly ConfigurationSchema.TryConvert<string, ulong> ulongTryParse = new ConfigurationSchema.TryConvert<string, ulong>(ulong.TryParse);

			// Token: 0x0400018C RID: 396
			internal static readonly ConfigurationSchema.Property<string> ExchangeInstallPath = ConfigurationSchema.Property<string>.Declare(ConfigurationSchema<ProtocolLogConfiguration.Schema>.ConstantDataSource, () => ExchangeSetupContext.InstallPath);

			// Token: 0x0400018D RID: 397
			private static ConfigurationSchema.AppSettingsDataSource appSettings = new ConfigurationSchema.AppSettingsDataSource(ConfigurationSchema<ProtocolLogConfiguration.Schema>.AllDataSources);

			// Token: 0x0400018E RID: 398
			internal static ConfigurationSchema.Property<bool> IsLoggingEnabled = ConfigurationSchema.Property<bool>.Declare<string, string, string>(ProtocolLogConfiguration.Schema.appSettings, "ProtocolLoggingEnabled", new ConfigurationSchema.TryConvert<string, bool>(bool.TryParse), false);

			// Token: 0x0400018F RID: 399
			internal static ConfigurationSchema.Property<string> LogFilePath = ConfigurationSchema.Property<string>.Declare<string, string>(ProtocolLogConfiguration.Schema.appSettings, "LogPath", ProtocolLogConfiguration.defaultLogFilePath ?? "%ExchangeInstallDir%\\Logging\\RPC Client Access\\");

			// Token: 0x04000190 RID: 400
			internal static ConfigurationSchema.Property<TimeSpan> AgeQuota = ConfigurationSchema.Property<TimeSpan>.Declare<string, string, ulong>(ProtocolLogConfiguration.Schema.appSettings, "MaxRetentionPeriod", ProtocolLogConfiguration.Schema.ulongTryParse, delegate(ulong hours, out TimeSpan value)
			{
				value = TimeSpan.FromHours(hours);
				return true;
			}, TimeSpan.FromHours(720.0));

			// Token: 0x04000191 RID: 401
			internal static ConfigurationSchema.Property<long> DirectorySizeQuota = ConfigurationSchema.Property<long>.Declare<string, string, ulong>(ProtocolLogConfiguration.Schema.appSettings, "MaxDirectorySize", ProtocolLogConfiguration.Schema.ulongTryParse, new ConfigurationSchema.TryConvert<ulong, long>(ProtocolLogConfiguration.Schema.TryKilobytesToBytes), 1073741824L);

			// Token: 0x04000192 RID: 402
			internal static ConfigurationSchema.Property<long> PerFileSizeQuota = ConfigurationSchema.Property<long>.Declare<string, string, ulong>(ProtocolLogConfiguration.Schema.appSettings, "PerFileMaxSize", ProtocolLogConfiguration.Schema.ulongTryParse, new ConfigurationSchema.TryConvert<ulong, long>(ProtocolLogConfiguration.Schema.TryKilobytesToBytes), 10485760L);

			// Token: 0x04000193 RID: 403
			internal static ConfigurationSchema.Property<bool> ApplyHourPrecision = ConfigurationSchema.Property<bool>.Declare<string, string, string>(ProtocolLogConfiguration.Schema.appSettings, "ApplyHourPrecision", new ConfigurationSchema.TryConvert<string, bool>(bool.TryParse), true);

			// Token: 0x04000194 RID: 404
			internal static ConfigurationSchema.Property<ProtocolLoggingTag> EnabledTags = ConfigurationSchema.Property<ProtocolLoggingTag>.Declare<string, string, string>(ProtocolLogConfiguration.Schema.appSettings, "LoggingTag", delegate(string enumString, out ProtocolLoggingTag value)
			{
				return EnumValidator.TryParse<ProtocolLoggingTag>(enumString, EnumParseOptions.IgnoreCase, out value);
			}, ProtocolLoggingTag.ConnectDisconnect | ProtocolLoggingTag.ApplicationData | ProtocolLoggingTag.Failures | ProtocolLoggingTag.Logon);
		}
	}
}

using System;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AnchorConfig : ConfigSchemaBase, IDiagnosable
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002FC0 File Offset: 0x000011C0
		internal AnchorConfig(string applicationName)
		{
			AnchorConfig <>4__this = this;
			this.applicationName = applicationName;
			this.lazyConfigProvider = new Lazy<IConfigProvider>(() => AnchorConfig.AnchorConfigProvider.CreateProvider(applicationName, <>4__this));
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003011 File Offset: 0x00001211
		public override string Name
		{
			get
			{
				return this.applicationName;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003019 File Offset: 0x00001219
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003026 File Offset: 0x00001226
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003034 File Offset: 0x00001234
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003041 File Offset: 0x00001241
		[ConfigurationProperty("LoggingLevel", DefaultValue = MigrationEventType.Information)]
		public MigrationEventType LoggingLevel
		{
			get
			{
				return this.InternalGetConfig<MigrationEventType>("LoggingLevel");
			}
			set
			{
				this.InternalSetConfig<MigrationEventType>(value, "LoggingLevel");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000304F File Offset: 0x0000124F
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000305C File Offset: 0x0000125C
		[ConfigurationProperty("LogFilePath", DefaultValue = null)]
		public string LogFilePath
		{
			get
			{
				return this.InternalGetConfig<string>("LogFilePath");
			}
			set
			{
				this.InternalSetConfig<string>(value, "LogFilePath");
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000306A File Offset: 0x0000126A
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003077 File Offset: 0x00001277
		[ConfigurationProperty("LogMaxAge", DefaultValue = "30.00:00:00")]
		[TimeSpanValidator(MinValueString = "1.00:00:00", MaxValueString = "180.0:00:00", ExcludeRange = false)]
		public TimeSpan LogMaxAge
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("LogMaxAge");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "LogMaxAge");
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003085 File Offset: 0x00001285
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003092 File Offset: 0x00001292
		[ConfigurationProperty("LogMaxDirectorySize", DefaultValue = "50000000")]
		[LongValidator(MinValue = 0L, MaxValue = 1048576000L, ExcludeRange = false)]
		public long LogMaxDirectorySize
		{
			get
			{
				return this.InternalGetConfig<long>("LogMaxDirectorySize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "LogMaxDirectorySize");
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000030A0 File Offset: 0x000012A0
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000030AD File Offset: 0x000012AD
		[LongValidator(MinValue = 0L, MaxValue = 1048576000L, ExcludeRange = false)]
		[ConfigurationProperty("LogMaxFileSize", DefaultValue = "50000000")]
		public long LogMaxFileSize
		{
			get
			{
				return this.InternalGetConfig<long>("LogMaxFileSize");
			}
			set
			{
				this.InternalSetConfig<long>(value, "LogMaxFileSize");
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000030BB File Offset: 0x000012BB
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000030C8 File Offset: 0x000012C8
		[ConfigurationProperty("FaultInjectionHandler", DefaultValue = null)]
		public string FaultInjectionHandler
		{
			get
			{
				return this.InternalGetConfig<string>("FaultInjectionHandler");
			}
			set
			{
				this.InternalSetConfig<string>(value, "FaultInjectionHandler");
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000030D6 File Offset: 0x000012D6
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000030E3 File Offset: 0x000012E3
		[ConfigurationProperty("ShouldWakeOnCacheUpdate", DefaultValue = true)]
		public bool ShouldWakeOnCacheUpdate
		{
			get
			{
				return this.InternalGetConfig<bool>("ShouldWakeOnCacheUpdate");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "ShouldWakeOnCacheUpdate");
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000030F1 File Offset: 0x000012F1
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000030FE File Offset: 0x000012FE
		[TimeSpanValidator(MinValueString = "00:00:01", MaxValueString = "180.0:00:00", ExcludeRange = false)]
		[ConfigurationProperty("TransientErrorRunDelay", DefaultValue = "00:05:00")]
		public TimeSpan TransientErrorRunDelay
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("TransientErrorRunDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "TransientErrorRunDelay");
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000310C File Offset: 0x0000130C
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003119 File Offset: 0x00001319
		[TimeSpanValidator(MinValueString = "00:00:01", MaxValueString = "180.0:00:00", ExcludeRange = false)]
		[ConfigurationProperty("ActiveRunDelay", DefaultValue = "00:01:00")]
		public TimeSpan ActiveRunDelay
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ActiveRunDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ActiveRunDelay");
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003127 File Offset: 0x00001327
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003134 File Offset: 0x00001334
		[ConfigurationProperty("IdleRunDelay", DefaultValue = "00:05:00")]
		[TimeSpanValidator(MinValueString = "00:00:01", MaxValueString = "180.0:00:00", ExcludeRange = false)]
		public TimeSpan IdleRunDelay
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("IdleRunDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "IdleRunDelay");
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003142 File Offset: 0x00001342
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000314F File Offset: 0x0000134F
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "365.0:00:00", ExcludeRange = false)]
		[ConfigurationProperty("ScannerTimeDelay", DefaultValue = "00:05:00")]
		public TimeSpan ScannerTimeDelay
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ScannerTimeDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ScannerTimeDelay");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000315D File Offset: 0x0000135D
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000316A File Offset: 0x0000136A
		[ConfigurationProperty("ScannerInitialTimeDelay", DefaultValue = "00:00:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "7.0:00:00", ExcludeRange = false)]
		public TimeSpan ScannerInitialTimeDelay
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("ScannerInitialTimeDelay");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "ScannerInitialTimeDelay");
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003178 File Offset: 0x00001378
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003185 File Offset: 0x00001385
		[ConfigurationProperty("SlowOperationThreshold", DefaultValue = "00:05:00")]
		[TimeSpanValidator(MinValueString = "00:00:01", MaxValueString = "365.0:00:00", ExcludeRange = false)]
		public TimeSpan SlowOperationThreshold
		{
			get
			{
				return this.InternalGetConfig<TimeSpan>("SlowOperationThreshold");
			}
			set
			{
				this.InternalSetConfig<TimeSpan>(value, "SlowOperationThreshold");
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003193 File Offset: 0x00001393
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000031A0 File Offset: 0x000013A0
		[ConfigurationProperty("MaximumCacheEntrySchedulerRun", DefaultValue = -1)]
		[IntegerValidator(MinValue = -1, ExcludeRange = false)]
		public int MaximumCacheEntrySchedulerRun
		{
			get
			{
				return this.InternalGetConfig<int>("MaximumCacheEntrySchedulerRun");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaximumCacheEntrySchedulerRun");
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000031AE File Offset: 0x000013AE
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000031BB File Offset: 0x000013BB
		[IntegerValidator(MinValue = -1, ExcludeRange = false)]
		[ConfigurationProperty("MaximumCacheEntryCountPerOrganization", DefaultValue = 1)]
		public int MaximumCacheEntryCountPerOrganization
		{
			get
			{
				return this.InternalGetConfig<int>("MaximumCacheEntryCountPerOrganization");
			}
			set
			{
				this.InternalSetConfig<int>(value, "MaximumCacheEntryCountPerOrganization");
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000031C9 File Offset: 0x000013C9
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000031D6 File Offset: 0x000013D6
		[ConfigurationProperty("ScannerClearCacheOnRefresh", DefaultValue = true)]
		public bool ScannerClearCacheOnRefresh
		{
			get
			{
				return this.InternalGetConfig<bool>("ScannerClearCacheOnRefresh");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "ScannerClearCacheOnRefresh");
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000031E4 File Offset: 0x000013E4
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000031F1 File Offset: 0x000013F1
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000031FF File Offset: 0x000013FF
		// (set) Token: 0x06000083 RID: 131 RVA: 0x0000320C File Offset: 0x0000140C
		[TimeSpanValidator(MinValueString = "00:00:01", ExcludeRange = false)]
		[ConfigurationProperty("IssueCacheScanFrequency", DefaultValue = "00:05:00")]
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000321A File Offset: 0x0000141A
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003227 File Offset: 0x00001427
		[IntegerValidator(MinValue = 0, ExcludeRange = false)]
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003235 File Offset: 0x00001435
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003242 File Offset: 0x00001442
		[ConfigurationProperty("MonitoringComponentName", DefaultValue = null)]
		public string MonitoringComponentName
		{
			get
			{
				return this.InternalGetConfig<string>("MonitoringComponentName");
			}
			set
			{
				this.InternalSetConfig<string>(value, "MonitoringComponentName");
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003250 File Offset: 0x00001450
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000325D File Offset: 0x0000145D
		[ConfigurationProperty("CacheEntryPoisonNotificationReason", DefaultValue = "CacheEntryIsPoisoned")]
		public string CacheEntryPoisonNotificationReason
		{
			get
			{
				return this.InternalGetConfig<string>("CacheEntryPoisonNotificationReason");
			}
			set
			{
				this.InternalSetConfig<string>(value, "CacheEntryPoisonNotificationReason");
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000326B File Offset: 0x0000146B
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003278 File Offset: 0x00001478
		[ConfigurationProperty("CacheEntryPoisonThreshold", DefaultValue = 10)]
		public int CacheEntryPoisonThreshold
		{
			get
			{
				return this.InternalGetConfig<int>("CacheEntryPoisonThreshold");
			}
			set
			{
				this.InternalSetConfig<int>(value, "CacheEntryPoisonThreshold");
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003286 File Offset: 0x00001486
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00003293 File Offset: 0x00001493
		[ConfigurationProperty("UseWatson", DefaultValue = true)]
		public bool UseWatson
		{
			get
			{
				return this.InternalGetConfig<bool>("UseWatson");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "UseWatson");
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000032A1 File Offset: 0x000014A1
		private IConfigProvider Provider
		{
			get
			{
				return this.lazyConfigProvider.Value;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000032AE File Offset: 0x000014AE
		public T GetConfig<T>([CallerMemberName] string settingName = null)
		{
			AnchorUtil.ThrowOnNullOrEmptyArgument(settingName, "settingName");
			return this.Provider.GetConfig<T>(settingName);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000032C7 File Offset: 0x000014C7
		public T GetConfig<T>(ISettingsContext context, string settingName)
		{
			return this.Provider.GetConfig<T>(context, settingName);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000032D6 File Offset: 0x000014D6
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return string.Format("{0}_{1}", this.Name, this.Provider.GetDiagnosticComponentName());
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000032F3 File Offset: 0x000014F3
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return this.Provider.GetDiagnosticInfo(parameters);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003301 File Offset: 0x00001501
		internal void UpdateConfig<T>(string settingName, T value)
		{
			base.SetDefaultConfigValue<T>(base.GetConfigurationProperty(settingName, null), value);
		}

		// Token: 0x04000013 RID: 19
		private readonly Lazy<IConfigProvider> lazyConfigProvider;

		// Token: 0x04000014 RID: 20
		private readonly string applicationName;

		// Token: 0x02000009 RID: 9
		public static class Setting
		{
			// Token: 0x04000015 RID: 21
			public const string IsEnabled = "IsEnabled";

			// Token: 0x04000016 RID: 22
			public const string LoggingLevel = "LoggingLevel";

			// Token: 0x04000017 RID: 23
			public const string LogFilePath = "LogFilePath";

			// Token: 0x04000018 RID: 24
			public const string LogMaxAge = "LogMaxAge";

			// Token: 0x04000019 RID: 25
			public const string LogMaxDirectorySize = "LogMaxDirectorySize";

			// Token: 0x0400001A RID: 26
			public const string LogMaxFileSize = "LogMaxFileSize";

			// Token: 0x0400001B RID: 27
			public const string FaultInjectionHandler = "FaultInjectionHandler";

			// Token: 0x0400001C RID: 28
			public const string ShouldWakeOnCacheUpdate = "ShouldWakeOnCacheUpdate";

			// Token: 0x0400001D RID: 29
			public const string TransientErrorRunDelay = "TransientErrorRunDelay";

			// Token: 0x0400001E RID: 30
			public const string ActiveRunDelay = "ActiveRunDelay";

			// Token: 0x0400001F RID: 31
			public const string IdleRunDelay = "IdleRunDelay";

			// Token: 0x04000020 RID: 32
			public const string ScannerTimeDelay = "ScannerTimeDelay";

			// Token: 0x04000021 RID: 33
			public const string ScannerInitialTimeDelay = "ScannerInitialTimeDelay";

			// Token: 0x04000022 RID: 34
			public const string SlowOperationThreshold = "SlowOperationThreshold";

			// Token: 0x04000023 RID: 35
			public const string MaximumCacheEntrySchedulerRun = "MaximumCacheEntrySchedulerRun";

			// Token: 0x04000024 RID: 36
			public const string MaximumCacheEntryCountPerOrganization = "MaximumCacheEntryCountPerOrganization";

			// Token: 0x04000025 RID: 37
			public const string ScannerClearCacheOnRefresh = "ScannerClearCacheOnRefresh";

			// Token: 0x04000026 RID: 38
			public const string IssueCacheIsEnabled = "IssueCacheIsEnabled";

			// Token: 0x04000027 RID: 39
			public const string IssueCacheScanFrequency = "IssueCacheScanFrequency";

			// Token: 0x04000028 RID: 40
			public const string IssueCacheItemLimit = "IssueCacheItemLimit";

			// Token: 0x04000029 RID: 41
			public const string MonitoringComponentName = "MonitoringComponentName";

			// Token: 0x0400002A RID: 42
			public const string CacheEntryPoisonNotificationReason = "CacheEntryPoisonNotificationReason";

			// Token: 0x0400002B RID: 43
			public const string CacheEntryPoisonThreshold = "CacheEntryPoisonThreshold";

			// Token: 0x0400002C RID: 44
			public const string UseWatson = "UseWatson";
		}

		// Token: 0x0200000A RID: 10
		private class AnchorConfigProvider : ConfigProvider
		{
			// Token: 0x06000094 RID: 148 RVA: 0x00003312 File Offset: 0x00001512
			private AnchorConfigProvider(ConfigSchemaBase schema) : base(schema)
			{
			}

			// Token: 0x06000095 RID: 149 RVA: 0x0000331C File Offset: 0x0000151C
			public static IConfigProvider CreateProvider(string applicationName, AnchorConfig schema)
			{
				ConfigDriverBase configDriverBase = null;
				ConfigDriverBase configDriverBase2 = null;
				ConfigFlags overrideFlags = ConfigProviderBase.OverrideFlags;
				if ((overrideFlags & ConfigFlags.DisallowADConfig) != ConfigFlags.DisallowADConfig)
				{
					configDriverBase = new ADConfigDriver(schema);
				}
				if ((overrideFlags & ConfigFlags.DisallowAppConfig) != ConfigFlags.DisallowAppConfig)
				{
					configDriverBase2 = new AnchorConfig.AnchorAppConfigDriver(schema.Name, schema);
				}
				AnchorConfig.AnchorConfigProvider anchorConfigProvider = new AnchorConfig.AnchorConfigProvider(schema);
				if (configDriverBase != null && configDriverBase2 != null && (overrideFlags & ConfigFlags.LowADConfigPriority) == ConfigFlags.LowADConfigPriority)
				{
					anchorConfigProvider.AddConfigDriver(configDriverBase2);
					anchorConfigProvider.AddConfigDriver(configDriverBase);
				}
				else
				{
					if (configDriverBase != null)
					{
						anchorConfigProvider.AddConfigDriver(configDriverBase);
					}
					if (configDriverBase2 != null)
					{
						anchorConfigProvider.AddConfigDriver(configDriverBase2);
					}
				}
				return anchorConfigProvider;
			}
		}

		// Token: 0x0200000B RID: 11
		private class AnchorAppConfigDriver : AppConfigDriver
		{
			// Token: 0x06000096 RID: 150 RVA: 0x0000338B File Offset: 0x0000158B
			public AnchorAppConfigDriver(string applicationName, IConfigSchema schema) : base(schema, new TimeSpan?(ConfigDriverBase.DefaultErrorThresholdInterval))
			{
				this.applicationName = applicationName;
			}

			// Token: 0x06000097 RID: 151 RVA: 0x000033A8 File Offset: 0x000015A8
			public override bool TryGetBoxedSetting(ISettingsContext context, string settingName, Type settingType, out object settingValue)
			{
				settingValue = null;
				AppSettingsSection appSettingsSection = base.Section as AppSettingsSection;
				if (appSettingsSection == null)
				{
					return false;
				}
				string key = string.Format("{0}_{1}", this.applicationName, settingName);
				KeyValueConfigurationElement keyValueConfigurationElement = appSettingsSection.Settings[key];
				if (keyValueConfigurationElement != null)
				{
					settingValue = base.ParseAndValidateConfigValue(settingName, keyValueConfigurationElement.Value, settingType);
					return true;
				}
				return false;
			}

			// Token: 0x06000098 RID: 152 RVA: 0x00003400 File Offset: 0x00001600
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<AnchorConfig.AnchorAppConfigDriver>(this);
			}

			// Token: 0x0400002D RID: 45
			private readonly string applicationName;
		}
	}
}

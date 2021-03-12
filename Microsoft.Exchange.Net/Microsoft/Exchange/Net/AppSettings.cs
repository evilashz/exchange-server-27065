using System;
using System.Management.Automation;
using System.Web;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BBA RID: 3002
	internal static class AppSettings
	{
		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x0600406B RID: 16491 RVA: 0x000AAB7E File Offset: 0x000A8D7E
		internal static IAppSettings Current
		{
			get
			{
				if (EventLogConstants.IsPowerShellWebService)
				{
					return PswsAppSettings.Instance;
				}
				if (HttpContext.Current != null)
				{
					return AutoLoadAppSettings.Instance;
				}
				if (AppSettings.manualLoadAppSettings == null)
				{
					return DefaultAppSettings.Instance;
				}
				return AppSettings.manualLoadAppSettings;
			}
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x0600406C RID: 16492 RVA: 0x000AABAC File Offset: 0x000A8DAC
		internal static bool RpsAuthZAppSettingsInitialized
		{
			get
			{
				return AppSettings.manualLoadAppSettings != null;
			}
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x000AABBC File Offset: 0x000A8DBC
		internal static void InitializeManualLoadAppSettings(string connectionUri, Action postLoadAction)
		{
			if (AppSettings.manualLoadAppSettings != null)
			{
				return;
			}
			lock (AppSettings.SyncObj)
			{
				if (AppSettings.manualLoadAppSettings == null)
				{
					AppSettings.manualLoadAppSettings = new ManualLoadAppSettings(connectionUri);
					postLoadAction();
				}
			}
		}

		// Token: 0x040037B4 RID: 14260
		internal const string PodRedirectTemplateAppSettingKey = "PodRedirectTemplate";

		// Token: 0x040037B5 RID: 14261
		internal const string SiteRedirectTemplateAppSettingKey = "SiteRedirectTemplate";

		// Token: 0x040037B6 RID: 14262
		internal const string TenantRedirectionEnabledAppSettingKey = "TenantRedirectionEnabled";

		// Token: 0x040037B7 RID: 14263
		internal const string RedirectionEnabledAppSettingKey = "RedirectionEnabled";

		// Token: 0x040037B8 RID: 14264
		internal const string MaxPowershellAppPoolConnectionsAppSettingKey = "MaxPowershellAppPoolConnections";

		// Token: 0x040037B9 RID: 14265
		internal const string ProvisioningCacheIdentificationAppSettingKey = "ProvisioningCacheIdentification";

		// Token: 0x040037BA RID: 14266
		internal const string DedicatedMailboxPlansCustomAttributeNameAppSettingKey = "DedicatedMailboxPlansCustomAttributeName";

		// Token: 0x040037BB RID: 14267
		internal const string DedicatedMailboxPlansEnabledAppSettingKey = "DedicatedMailboxPlansEnabled";

		// Token: 0x040037BC RID: 14268
		internal const string ShouldShowFismaBannerAppSettingKey = "ShouldShowFismaBanner";

		// Token: 0x040037BD RID: 14269
		internal const string MaxWorkerThreadsAppSettingKey = "ThreadPool.MaxWorkerThreads";

		// Token: 0x040037BE RID: 14270
		internal const string MaxCompletionPortThreadsAppSettingKey = "ThreadPool.MaxCompletionPortThreads";

		// Token: 0x040037BF RID: 14271
		internal const string PSLanguageModeAppSettingKey = "PSLanguageMode";

		// Token: 0x040037C0 RID: 14272
		internal const string SupportedEMCVersionsAppSettingKey = "SupportedEMCVersions";

		// Token: 0x040037C1 RID: 14273
		internal const string FailFastEnabledAppSettingKey = "FailFastEnabled";

		// Token: 0x040037C2 RID: 14274
		internal const string LogSubFolderNameAppSettingKey = "LogSubFolderName";

		// Token: 0x040037C3 RID: 14275
		internal const string LogEnabledAppSettingKey = "LogEnabled";

		// Token: 0x040037C4 RID: 14276
		internal const string CustomLogFolderPathAppSettingsKey = "ConfigurationCoreLogger.LogFolder";

		// Token: 0x040037C5 RID: 14277
		internal const string LogFileAgeInDaysAppSettingKey = "LogFileAgeInDays";

		// Token: 0x040037C6 RID: 14278
		internal const string MaxLogDirectorySizeInGBAppSettingsKey = "MaxLogDirectorySizeInGB";

		// Token: 0x040037C7 RID: 14279
		internal const string MaxLogFileSizeInMBAppSettingsKey = "MaxLogFileSizeInMB";

		// Token: 0x040037C8 RID: 14280
		internal const string LogDirectoryPathAppSettingKey = "LogDirectoryPath";

		// Token: 0x040037C9 RID: 14281
		internal const string ThresholdToLogActivityLatencyAppSettingsKey = "ThresholdToLogActivityLatency";

		// Token: 0x040037CA RID: 14282
		internal const string LogCPUMemoryIntervalInMinutesAppSettingsKey = "LogCPUMemoryIntervalInMinutes";

		// Token: 0x040037CB RID: 14283
		internal const string SidsCacheTimeoutInHoursAppSettingKey = "SidsCacheTimeoutInHours";

		// Token: 0x040037CC RID: 14284
		internal const string ClientAccessRulesLimitAppSettingsKey = "ClientAccessRulesLimit";

		// Token: 0x040037CD RID: 14285
		internal const string MaxCmdletRetryCntAppSettingsKey = "MaxCmdletRetryCnt";

		// Token: 0x040037CE RID: 14286
		internal const string DefaultPodRedirectTemplate = null;

		// Token: 0x040037CF RID: 14287
		internal const string DefaultSiteRedirectTemplate = null;

		// Token: 0x040037D0 RID: 14288
		internal const bool DefaultTenantRedirectionEnabled = false;

		// Token: 0x040037D1 RID: 14289
		internal const bool DefaultRedirectionEnabled = true;

		// Token: 0x040037D2 RID: 14290
		internal const int DefaultMaxPowershellAppPoolConnections = 0;

		// Token: 0x040037D3 RID: 14291
		internal const string DefaultProvisioningCacheIdentification = null;

		// Token: 0x040037D4 RID: 14292
		internal const string DefaultDedicatedMailboxPlansCustomAttributeName = null;

		// Token: 0x040037D5 RID: 14293
		internal const bool DefaultDedicatedMailboxPlansEnabled = false;

		// Token: 0x040037D6 RID: 14294
		internal const bool DefaultShouldShowFismaBanner = false;

		// Token: 0x040037D7 RID: 14295
		internal const string DefaultSupportedEMCVersions = null;

		// Token: 0x040037D8 RID: 14296
		internal const bool DefaultFailFastEnabled = false;

		// Token: 0x040037D9 RID: 14297
		internal const string DefaultLogDirectoryPath = null;

		// Token: 0x040037DA RID: 14298
		internal const PSLanguageMode DefaultLanguageMode = PSLanguageMode.NoLanguage;

		// Token: 0x040037DB RID: 14299
		internal const string DefaultLogSubFolderName = "Others";

		// Token: 0x040037DC RID: 14300
		internal const bool DefaultLogEnabled = true;

		// Token: 0x040037DD RID: 14301
		internal const int DefaultMaxLogDirectorySizeInGB = 1;

		// Token: 0x040037DE RID: 14302
		internal const int DefaultThresholdToLogActivityLatency = 1000;

		// Token: 0x040037DF RID: 14303
		internal const int DefaultMaxLogFileSizeInMB = 10;

		// Token: 0x040037E0 RID: 14304
		internal static readonly int DefaultThreadPoolMaxThreads = Environment.ProcessorCount * 150;

		// Token: 0x040037E1 RID: 14305
		internal static readonly int DefaultThreadPoolMaxCompletionPorts = Environment.ProcessorCount * 150;

		// Token: 0x040037E2 RID: 14306
		internal static readonly int DefaultPSMaximumReceivedObjectSizeByte = 78643200;

		// Token: 0x040037E3 RID: 14307
		internal static readonly int DefaultPSMaximumReceivedDataSizePerCommandByte = 524288000;

		// Token: 0x040037E4 RID: 14308
		internal static readonly TimeSpan DefaultLogFileAgeInDays = TimeSpan.FromDays(30.0);

		// Token: 0x040037E5 RID: 14309
		internal static readonly int DefaultLogCPUMemoryIntervalInMinutes = 5;

		// Token: 0x040037E6 RID: 14310
		internal static readonly TimeSpan DefaultSidsCacheTimeoutInHours = TimeSpan.FromHours(24.0);

		// Token: 0x040037E7 RID: 14311
		internal static readonly int DefaultClientAccessRulesLimit = 20;

		// Token: 0x040037E8 RID: 14312
		internal static readonly int DefaultMaxCmdletRetryCnt = 2;

		// Token: 0x040037E9 RID: 14313
		private static readonly object SyncObj = new object();

		// Token: 0x040037EA RID: 14314
		private static ManualLoadAppSettings manualLoadAppSettings;
	}
}

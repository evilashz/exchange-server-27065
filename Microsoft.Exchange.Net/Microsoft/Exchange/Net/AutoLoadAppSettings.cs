using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BC4 RID: 3012
	public class AutoLoadAppSettings : IAppSettings
	{
		// Token: 0x060040DF RID: 16607 RVA: 0x000ACF8F File Offset: 0x000AB18F
		protected AutoLoadAppSettings()
		{
		}

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x060040E0 RID: 16608 RVA: 0x000ACF97 File Offset: 0x000AB197
		public static AutoLoadAppSettings Instance
		{
			get
			{
				AutoLoadAppSettings result;
				if ((result = AutoLoadAppSettings.instance) == null)
				{
					result = (AutoLoadAppSettings.instance = new AutoLoadAppSettings());
				}
				return result;
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x060040E1 RID: 16609 RVA: 0x000ACFAD File Offset: 0x000AB1AD
		string IAppSettings.PodRedirectTemplate
		{
			get
			{
				return AutoLoadAppSettings.PopRedirectTemplateEntry.Value;
			}
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x060040E2 RID: 16610 RVA: 0x000ACFB9 File Offset: 0x000AB1B9
		string IAppSettings.SiteRedirectTemplate
		{
			get
			{
				return AutoLoadAppSettings.SiteRedirectTemplateEntry.Value;
			}
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x060040E3 RID: 16611 RVA: 0x000ACFC5 File Offset: 0x000AB1C5
		bool IAppSettings.TenantRedirectionEnabled
		{
			get
			{
				return AutoLoadAppSettings.TenantRedirectionEnabledEntry.Value;
			}
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x060040E4 RID: 16612 RVA: 0x000ACFD1 File Offset: 0x000AB1D1
		bool IAppSettings.RedirectionEnabled
		{
			get
			{
				return AutoLoadAppSettings.RedirectionEnabledEntry.Value;
			}
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x060040E5 RID: 16613 RVA: 0x000ACFDD File Offset: 0x000AB1DD
		int IAppSettings.MaxPowershellAppPoolConnections
		{
			get
			{
				throw new NotSupportedException("MaxPowershellAppPoolConnections is not supposed to be used in AutoLoadAppSettings.");
			}
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x060040E6 RID: 16614 RVA: 0x000ACFE9 File Offset: 0x000AB1E9
		string IAppSettings.ProvisioningCacheIdentification
		{
			get
			{
				return AutoLoadAppSettings.ProvisioningCacheIdentificationEntry.Value;
			}
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x060040E7 RID: 16615 RVA: 0x000ACFF5 File Offset: 0x000AB1F5
		string IAppSettings.DedicatedMailboxPlansCustomAttributeName
		{
			get
			{
				return AutoLoadAppSettings.DedicatedMailboxPlansCustomAttributeNameEntry.Value;
			}
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x060040E8 RID: 16616 RVA: 0x000AD001 File Offset: 0x000AB201
		bool IAppSettings.DedicatedMailboxPlansEnabled
		{
			get
			{
				return AutoLoadAppSettings.DedicatedMailboxPlansEnabledEntry.Value;
			}
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x060040E9 RID: 16617 RVA: 0x000AD00D File Offset: 0x000AB20D
		bool IAppSettings.ShouldShowFismaBanner
		{
			get
			{
				return AutoLoadAppSettings.ShouldShowFismaBannerEntry.Value;
			}
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x060040EA RID: 16618 RVA: 0x000AD019 File Offset: 0x000AB219
		int IAppSettings.ThreadPoolMaxThreads
		{
			get
			{
				throw new NotSupportedException("ThreadPoolMaxThreads is not supposed to be used in RemotePS AutoLoadAppSettings.");
			}
		}

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x060040EB RID: 16619 RVA: 0x000AD025 File Offset: 0x000AB225
		int IAppSettings.ThreadPoolMaxCompletionPorts
		{
			get
			{
				throw new NotSupportedException("ThreadPoolMaxCompletionPorts is not supposed to be used in RemotePS AutoLoadAppSettings.");
			}
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x060040EC RID: 16620 RVA: 0x000AD031 File Offset: 0x000AB231
		PSLanguageMode IAppSettings.PSLanguageMode
		{
			get
			{
				throw new NotSupportedException("PSLanguageMode is not supposed to be used in RemotePS AutoLoadAppSettings.");
			}
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x060040ED RID: 16621 RVA: 0x000AD03D File Offset: 0x000AB23D
		string IAppSettings.SupportedEMCVersions
		{
			get
			{
				throw new NotSupportedException("SupportedEMCVersions is not supposed to be used in RemotePS AutoLoadAppSettings.");
			}
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x060040EE RID: 16622 RVA: 0x000AD049 File Offset: 0x000AB249
		bool IAppSettings.FailFastEnabled
		{
			get
			{
				return AutoLoadAppSettings.FailFastEnabledEntry.Value;
			}
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x060040EF RID: 16623 RVA: 0x000AD055 File Offset: 0x000AB255
		int IAppSettings.PSMaximumReceivedObjectSizeMB
		{
			get
			{
				throw new NotSupportedException("PSMaximumReceivedObjectSizeMB is not supposed to be used in RemotePS AutoLoadAppSettings.");
			}
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x060040F0 RID: 16624 RVA: 0x000AD061 File Offset: 0x000AB261
		int IAppSettings.PSMaximumReceivedDataSizePerCommandMB
		{
			get
			{
				throw new NotSupportedException("PSMaximumReceivedDataSizePerCommandMB is not supposed to be used in RemotePS AutoLoadAppSettings.");
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x060040F1 RID: 16625 RVA: 0x000AD06D File Offset: 0x000AB26D
		string IAppSettings.WebSiteName
		{
			get
			{
				throw new NotSupportedException("WebSiteName is not supposed to be used in RemotePS AutoLoadAppSettings.");
			}
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x060040F2 RID: 16626 RVA: 0x000AD079 File Offset: 0x000AB279
		string IAppSettings.VDirName
		{
			get
			{
				throw new NotSupportedException("VDirName is not supposed to be used in RemotePS AutoLoadAppSettings.");
			}
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x060040F3 RID: 16627 RVA: 0x000AD085 File Offset: 0x000AB285
		string IAppSettings.ConfigurationFilePath
		{
			get
			{
				throw new NotSupportedException("ConfigurationFilePath is not supposed to be used in RemotePS AutoLoadAppSettings.");
			}
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x060040F4 RID: 16628 RVA: 0x000AD091 File Offset: 0x000AB291
		string IAppSettings.LogSubFolderName
		{
			get
			{
				return AutoLoadAppSettings.LogSubFolderNameEntry.Value;
			}
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x060040F5 RID: 16629 RVA: 0x000AD09D File Offset: 0x000AB29D
		bool IAppSettings.LogEnabled
		{
			get
			{
				return AutoLoadAppSettings.LogEnabledEntry.Value;
			}
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x060040F6 RID: 16630 RVA: 0x000AD0A9 File Offset: 0x000AB2A9
		string IAppSettings.LogDirectoryPath
		{
			get
			{
				return AutoLoadAppSettings.LogDirectoryPathEntry.Value;
			}
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x060040F7 RID: 16631 RVA: 0x000AD0B5 File Offset: 0x000AB2B5
		int IAppSettings.MaxLogDirectorySizeInGB
		{
			get
			{
				return AutoLoadAppSettings.MaxLogDirectorySizeInGBEntry.Value;
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x060040F8 RID: 16632 RVA: 0x000AD0C1 File Offset: 0x000AB2C1
		int IAppSettings.MaxLogFileSizeInMB
		{
			get
			{
				return AutoLoadAppSettings.MaxLogFileSizeInMBEntry.Value;
			}
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x060040F9 RID: 16633 RVA: 0x000AD0CD File Offset: 0x000AB2CD
		TimeSpan IAppSettings.LogFileAgeInDays
		{
			get
			{
				return AutoLoadAppSettings.LogFileAgeInDaysEntry.Value;
			}
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x060040FA RID: 16634 RVA: 0x000AD0D9 File Offset: 0x000AB2D9
		int IAppSettings.ThresholdToLogActivityLatency
		{
			get
			{
				return AutoLoadAppSettings.ThresholdToLogActivityLatencyEntry.Value;
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x060040FB RID: 16635 RVA: 0x000AD0E5 File Offset: 0x000AB2E5
		int IAppSettings.LogCPUMemoryIntervalInMinutes
		{
			get
			{
				return AutoLoadAppSettings.LogCPUMemoryIntervalInMinutesEntry.Value;
			}
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x060040FC RID: 16636 RVA: 0x000AD0F1 File Offset: 0x000AB2F1
		public TimeSpan SidsCacheTimeoutInHours
		{
			get
			{
				return AutoLoadAppSettings.SidsCacheTimeoutInHoursEntry.Value;
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x060040FD RID: 16637 RVA: 0x000AD0FD File Offset: 0x000AB2FD
		int IAppSettings.ClientAccessRulesLimit
		{
			get
			{
				return AutoLoadAppSettings.ClientAccessRulesLimitEntry.Value;
			}
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x060040FE RID: 16638 RVA: 0x000AD10C File Offset: 0x000AB30C
		int IAppSettings.MaxCmdletRetryCnt
		{
			get
			{
				int value = AutoLoadAppSettings.MaxCmdletRetryCntEntry.Value;
				if (value >= 0)
				{
					return value;
				}
				return 0;
			}
		}

		// Token: 0x04003823 RID: 14371
		private static readonly StringAppSettingsEntry PopRedirectTemplateEntry = new StringAppSettingsEntry("PodRedirectTemplate", null, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x04003824 RID: 14372
		private static readonly StringAppSettingsEntry SiteRedirectTemplateEntry = new StringAppSettingsEntry("SiteRedirectTemplate", null, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x04003825 RID: 14373
		private static readonly StringAppSettingsEntry ProvisioningCacheIdentificationEntry = new StringAppSettingsEntry("ProvisioningCacheIdentification", null, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x04003826 RID: 14374
		private static readonly StringAppSettingsEntry DedicatedMailboxPlansCustomAttributeNameEntry = new StringAppSettingsEntry("DedicatedMailboxPlansCustomAttributeName", null, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x04003827 RID: 14375
		private static readonly BoolAppSettingsEntry DedicatedMailboxPlansEnabledEntry = new BoolAppSettingsEntry("DedicatedMailboxPlansEnabled", false, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x04003828 RID: 14376
		private static readonly BoolAppSettingsEntry TenantRedirectionEnabledEntry = new BoolAppSettingsEntry("TenantRedirectionEnabled", false, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x04003829 RID: 14377
		private static readonly BoolAppSettingsEntry RedirectionEnabledEntry = new BoolAppSettingsEntry("RedirectionEnabled", true, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x0400382A RID: 14378
		private static readonly BoolAppSettingsEntry FailFastEnabledEntry = new BoolAppSettingsEntry("FailFastEnabled", false, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x0400382B RID: 14379
		private static readonly StringAppSettingsEntry LogSubFolderNameEntry = new StringAppSettingsEntry("LogSubFolderName", "Others", ExTraceGlobals.InstrumentationTracer);

		// Token: 0x0400382C RID: 14380
		private static readonly BoolAppSettingsEntry LogEnabledEntry = new BoolAppSettingsEntry("LogEnabled", true, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x0400382D RID: 14381
		private static readonly StringAppSettingsEntry LogDirectoryPathEntry = new StringAppSettingsEntry("LogDirectoryPath", null, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x0400382E RID: 14382
		private static readonly TimeSpanAppSettingsEntry LogFileAgeInDaysEntry = new TimeSpanAppSettingsEntry("LogFileAgeInDays", TimeSpanUnit.Days, AppSettings.DefaultLogFileAgeInDays, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x0400382F RID: 14383
		private static readonly IntAppSettingsEntry MaxLogDirectorySizeInGBEntry = new IntAppSettingsEntry("MaxLogDirectorySizeInGB", 1, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x04003830 RID: 14384
		private static readonly IntAppSettingsEntry MaxLogFileSizeInMBEntry = new IntAppSettingsEntry("MaxLogFileSizeInMB", 10, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x04003831 RID: 14385
		private static readonly IntAppSettingsEntry ThresholdToLogActivityLatencyEntry = new IntAppSettingsEntry("ThresholdToLogActivityLatency", 1000, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x04003832 RID: 14386
		private static readonly BoolAppSettingsEntry ShouldShowFismaBannerEntry = new BoolAppSettingsEntry("ShouldShowFismaBanner", false, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x04003833 RID: 14387
		private static readonly IntAppSettingsEntry LogCPUMemoryIntervalInMinutesEntry = new IntAppSettingsEntry("LogCPUMemoryIntervalInMinutes", AppSettings.DefaultLogCPUMemoryIntervalInMinutes, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x04003834 RID: 14388
		private static readonly TimeSpanAppSettingsEntry SidsCacheTimeoutInHoursEntry = new TimeSpanAppSettingsEntry("SidsCacheTimeoutInHours", TimeSpanUnit.Hours, AppSettings.DefaultSidsCacheTimeoutInHours, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x04003835 RID: 14389
		private static readonly IntAppSettingsEntry ClientAccessRulesLimitEntry = new IntAppSettingsEntry("ClientAccessRulesLimit", AppSettings.DefaultClientAccessRulesLimit, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x04003836 RID: 14390
		private static readonly IntAppSettingsEntry MaxCmdletRetryCntEntry = new IntAppSettingsEntry("MaxCmdletRetryCnt", AppSettings.DefaultMaxCmdletRetryCnt, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x04003837 RID: 14391
		private static AutoLoadAppSettings instance;
	}
}

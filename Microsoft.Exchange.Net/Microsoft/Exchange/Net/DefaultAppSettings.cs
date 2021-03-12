using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BE2 RID: 3042
	internal class DefaultAppSettings : IAppSettings
	{
		// Token: 0x0600425D RID: 16989 RVA: 0x000B0E27 File Offset: 0x000AF027
		private DefaultAppSettings()
		{
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x0600425E RID: 16990 RVA: 0x000B0E2F File Offset: 0x000AF02F
		public static DefaultAppSettings Instance
		{
			get
			{
				DefaultAppSettings result;
				if ((result = DefaultAppSettings.instance) == null)
				{
					result = (DefaultAppSettings.instance = new DefaultAppSettings());
				}
				return result;
			}
		}

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x0600425F RID: 16991 RVA: 0x000B0E45 File Offset: 0x000AF045
		string IAppSettings.PodRedirectTemplate
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x06004260 RID: 16992 RVA: 0x000B0E48 File Offset: 0x000AF048
		string IAppSettings.SiteRedirectTemplate
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x06004261 RID: 16993 RVA: 0x000B0E4B File Offset: 0x000AF04B
		bool IAppSettings.TenantRedirectionEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x06004262 RID: 16994 RVA: 0x000B0E4E File Offset: 0x000AF04E
		bool IAppSettings.RedirectionEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06004263 RID: 16995 RVA: 0x000B0E51 File Offset: 0x000AF051
		int IAppSettings.MaxPowershellAppPoolConnections
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06004264 RID: 16996 RVA: 0x000B0E54 File Offset: 0x000AF054
		string IAppSettings.ProvisioningCacheIdentification
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x06004265 RID: 16997 RVA: 0x000B0E57 File Offset: 0x000AF057
		string IAppSettings.DedicatedMailboxPlansCustomAttributeName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x06004266 RID: 16998 RVA: 0x000B0E5A File Offset: 0x000AF05A
		bool IAppSettings.DedicatedMailboxPlansEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x06004267 RID: 16999 RVA: 0x000B0E5D File Offset: 0x000AF05D
		bool IAppSettings.ShouldShowFismaBanner
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06004268 RID: 17000 RVA: 0x000B0E60 File Offset: 0x000AF060
		int IAppSettings.ThreadPoolMaxThreads
		{
			get
			{
				return AppSettings.DefaultThreadPoolMaxThreads;
			}
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x06004269 RID: 17001 RVA: 0x000B0E67 File Offset: 0x000AF067
		int IAppSettings.ThreadPoolMaxCompletionPorts
		{
			get
			{
				return AppSettings.DefaultThreadPoolMaxCompletionPorts;
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x0600426A RID: 17002 RVA: 0x000B0E6E File Offset: 0x000AF06E
		PSLanguageMode IAppSettings.PSLanguageMode
		{
			get
			{
				return PSLanguageMode.NoLanguage;
			}
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x0600426B RID: 17003 RVA: 0x000B0E71 File Offset: 0x000AF071
		string IAppSettings.SupportedEMCVersions
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x0600426C RID: 17004 RVA: 0x000B0E74 File Offset: 0x000AF074
		bool IAppSettings.FailFastEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x0600426D RID: 17005 RVA: 0x000B0E77 File Offset: 0x000AF077
		int IAppSettings.PSMaximumReceivedObjectSizeMB
		{
			get
			{
				return AppSettings.DefaultPSMaximumReceivedObjectSizeByte;
			}
		}

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x0600426E RID: 17006 RVA: 0x000B0E7E File Offset: 0x000AF07E
		int IAppSettings.PSMaximumReceivedDataSizePerCommandMB
		{
			get
			{
				return AppSettings.DefaultPSMaximumReceivedDataSizePerCommandByte;
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x0600426F RID: 17007 RVA: 0x000B0E85 File Offset: 0x000AF085
		string IAppSettings.LogSubFolderName
		{
			get
			{
				return "Others";
			}
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x06004270 RID: 17008 RVA: 0x000B0E8C File Offset: 0x000AF08C
		bool IAppSettings.LogEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x06004271 RID: 17009 RVA: 0x000B0E8F File Offset: 0x000AF08F
		string IAppSettings.LogDirectoryPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x06004272 RID: 17010 RVA: 0x000B0E92 File Offset: 0x000AF092
		TimeSpan IAppSettings.LogFileAgeInDays
		{
			get
			{
				return AppSettings.DefaultLogFileAgeInDays;
			}
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x06004273 RID: 17011 RVA: 0x000B0E99 File Offset: 0x000AF099
		int IAppSettings.MaxLogDirectorySizeInGB
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x06004274 RID: 17012 RVA: 0x000B0E9C File Offset: 0x000AF09C
		int IAppSettings.MaxLogFileSizeInMB
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x06004275 RID: 17013 RVA: 0x000B0EA0 File Offset: 0x000AF0A0
		int IAppSettings.ThresholdToLogActivityLatency
		{
			get
			{
				return 1000;
			}
		}

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x06004276 RID: 17014 RVA: 0x000B0EA7 File Offset: 0x000AF0A7
		string IAppSettings.WebSiteName
		{
			get
			{
				throw new InvalidOperationException("WebSiteName is not supported in DefaultAppSettings.");
			}
		}

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x06004277 RID: 17015 RVA: 0x000B0EB3 File Offset: 0x000AF0B3
		string IAppSettings.VDirName
		{
			get
			{
				throw new InvalidOperationException("VDirName is not supported in DefaultAppSettings.");
			}
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06004278 RID: 17016 RVA: 0x000B0EBF File Offset: 0x000AF0BF
		string IAppSettings.ConfigurationFilePath
		{
			get
			{
				throw new InvalidOperationException("ConfigurationFilePath is not supported in DefaultAppSettings.");
			}
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x06004279 RID: 17017 RVA: 0x000B0ECB File Offset: 0x000AF0CB
		int IAppSettings.LogCPUMemoryIntervalInMinutes
		{
			get
			{
				return AppSettings.DefaultLogCPUMemoryIntervalInMinutes;
			}
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x000B0ED2 File Offset: 0x000AF0D2
		TimeSpan IAppSettings.SidsCacheTimeoutInHours
		{
			get
			{
				return AppSettings.DefaultSidsCacheTimeoutInHours;
			}
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x0600427B RID: 17019 RVA: 0x000B0ED9 File Offset: 0x000AF0D9
		int IAppSettings.ClientAccessRulesLimit
		{
			get
			{
				return AppSettings.DefaultClientAccessRulesLimit;
			}
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x0600427C RID: 17020 RVA: 0x000B0EE0 File Offset: 0x000AF0E0
		int IAppSettings.MaxCmdletRetryCnt
		{
			get
			{
				return AppSettings.DefaultMaxCmdletRetryCnt;
			}
		}

		// Token: 0x040038D7 RID: 14551
		private static DefaultAppSettings instance;
	}
}

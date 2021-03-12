using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.EdgeSync.Common;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000016 RID: 22
	internal class EdgeSyncAppConfig
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00002C2C File Offset: 0x00000E2C
		private EdgeSyncAppConfig()
		{
			string exePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Microsoft.Exchange.EdgeSyncSvc.exe");
			this.config = ConfigurationManager.OpenExeConfiguration(exePath);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002C65 File Offset: 0x00000E65
		public bool DelayLdapEnabled
		{
			get
			{
				return this.delayLdapEnabled;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002C6D File Offset: 0x00000E6D
		public TimeSpan DelayLdapSearchRequest
		{
			get
			{
				return this.delayLdapSearchRequest;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002C75 File Offset: 0x00000E75
		public TimeSpan DelayLdapUpdateRequest
		{
			get
			{
				return this.delayLdapUpdateRequest;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002C7D File Offset: 0x00000E7D
		public string DelayLdapUpdateRequestContainingString
		{
			get
			{
				return this.delayLdapUpdateRequestContainingString;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002C85 File Offset: 0x00000E85
		public TimeSpan DelayStart
		{
			get
			{
				return this.delayStart;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002C8D File Offset: 0x00000E8D
		public SyncTreeType EnabledSyncType
		{
			get
			{
				return this.enabledSyncType;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002C95 File Offset: 0x00000E95
		public List<SynchronizationProviderInfo> SynchronizationProviderList
		{
			get
			{
				return this.synchronizationProviderList;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002C9D File Offset: 0x00000E9D
		public long TenantSyncControlCacheSize
		{
			get
			{
				return this.tenantSyncControlCacheSize;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002CA5 File Offset: 0x00000EA5
		public TimeSpan TenantSyncControlCacheExpiryInterval
		{
			get
			{
				return this.tenantSyncControlCacheExpiryInterval;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002CAD File Offset: 0x00000EAD
		public TimeSpan TenantSyncControlCacheCleanupInterval
		{
			get
			{
				return this.tenantSyncControlCacheCleanupInterval;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002CB5 File Offset: 0x00000EB5
		public TimeSpan TenantSyncControlCachePurgeInterval
		{
			get
			{
				return this.tenantSyncControlCachePurgeInterval;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002CBD File Offset: 0x00000EBD
		public bool TrackDuplicatedAddEntries
		{
			get
			{
				return this.trackDuplicatedAddEntries;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002CC5 File Offset: 0x00000EC5
		public int DuplicatedAddEntriesCacheSize
		{
			get
			{
				return this.duplicatedAddEntriesCacheSize;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002CCD File Offset: 0x00000ECD
		public int PodSiteStartRange
		{
			get
			{
				return this.podSiteStartRange;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002CD5 File Offset: 0x00000ED5
		public int PodSiteEndRange
		{
			get
			{
				return this.podSiteEndRange;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public static EdgeSyncAppConfig Load()
		{
			if (EdgeSyncAppConfig.instance == null)
			{
				lock (EdgeSyncAppConfig.initializationLock)
				{
					if (EdgeSyncAppConfig.instance == null)
					{
						EdgeSyncAppConfig edgeSyncAppConfig = new EdgeSyncAppConfig();
						edgeSyncAppConfig.delayStart = edgeSyncAppConfig.GetConfigTimeSpan("DelayStart", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TimeSpan.MinValue);
						edgeSyncAppConfig.enabledSyncType = (SyncTreeType)edgeSyncAppConfig.GetConfigEnum("EnabledSyncType", typeof(SyncTreeType), SyncTreeType.Configuration | SyncTreeType.Recipients);
						edgeSyncAppConfig.delayLdapEnabled = edgeSyncAppConfig.GetConfigBool("DelayLdapEnabled", false);
						if (edgeSyncAppConfig.delayLdapEnabled)
						{
							edgeSyncAppConfig.delayLdapSearchRequest = edgeSyncAppConfig.GetConfigTimeSpan("DelayLdapSearchRequest", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TimeSpan.MinValue);
							edgeSyncAppConfig.delayLdapUpdateRequest = edgeSyncAppConfig.GetConfigTimeSpan("DelayLdapUpdateRequest", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TimeSpan.MinValue);
							edgeSyncAppConfig.delayLdapUpdateRequestContainingString = edgeSyncAppConfig.GetConfigString("DelayLdapUpdateRequestContainingString", string.Empty);
						}
						edgeSyncAppConfig.tenantSyncControlCacheSize = edgeSyncAppConfig.GetConfigLong("TenantSyncControlCacheSize", 1024L, 1073741824L, 36700160L);
						edgeSyncAppConfig.tenantSyncControlCacheExpiryInterval = edgeSyncAppConfig.GetConfigTimeSpan("TenantSyncControlCacheExpiryInterval", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TimeSpan.FromHours(4.0));
						edgeSyncAppConfig.tenantSyncControlCacheCleanupInterval = edgeSyncAppConfig.GetConfigTimeSpan("TenantSyncControlCacheCleanupInterval", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TimeSpan.FromMinutes(15.0));
						edgeSyncAppConfig.tenantSyncControlCachePurgeInterval = edgeSyncAppConfig.GetConfigTimeSpan("TenantSyncControlCachePurgeInterval", TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, TimeSpan.FromMinutes(5.0));
						edgeSyncAppConfig.trackDuplicatedAddEntries = edgeSyncAppConfig.GetConfigBool("TrackDuplicatedAddEntries", true);
						edgeSyncAppConfig.duplicatedAddEntriesCacheSize = edgeSyncAppConfig.GetConfigInt("DuplicatedAddEntriesCacheSize", 1, int.MaxValue, 1500);
						edgeSyncAppConfig.podSiteStartRange = edgeSyncAppConfig.GetConfigInt("PodSiteStartRange", 0, int.MaxValue, 50000);
						edgeSyncAppConfig.podSiteEndRange = edgeSyncAppConfig.GetConfigInt("PodSiteEndRange", 0, int.MaxValue, 59999);
						edgeSyncAppConfig.synchronizationProviderList = edgeSyncAppConfig.LoadSynchronizationProviders();
						Thread.MemoryBarrier();
						EdgeSyncAppConfig.instance = edgeSyncAppConfig;
					}
				}
			}
			return EdgeSyncAppConfig.instance;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002F4C File Offset: 0x0000114C
		public int GetConfigInt(string label, int min, int max, int defaultValue)
		{
			if (max < min)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Minimum must be smaller than or equal to Maximum (Config='{0}', Min='{1}', Max='{2}', Default='{3}').", new object[]
				{
					label,
					min,
					max,
					defaultValue
				}));
			}
			string text = (this.config.AppSettings.Settings[label] != null) ? this.config.AppSettings.Settings[label].Value : null;
			int num = defaultValue;
			if (string.IsNullOrEmpty(text) || !int.TryParse(text, out num) || num < min || num > max)
			{
				return defaultValue;
			}
			return num;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002FF4 File Offset: 0x000011F4
		public long GetConfigLong(string label, long min, long max, long defaultValue)
		{
			if (max < min)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Minimum must be smaller than or equal to Maximum (Config='{0}', Min='{1}', Max='{2}', Default='{3}').", new object[]
				{
					label,
					min,
					max,
					defaultValue
				}));
			}
			string text = (this.config.AppSettings.Settings[label] != null) ? this.config.AppSettings.Settings[label].Value : null;
			long num = defaultValue;
			if (string.IsNullOrEmpty(text) || !long.TryParse(text, out num) || num < min || num > max)
			{
				return defaultValue;
			}
			return num;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000309C File Offset: 0x0000129C
		public TimeSpan GetConfigTimeSpan(string label, TimeSpan min, TimeSpan max, TimeSpan defaultValue)
		{
			if (max < min)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Minimum must be smaller than or equal to Maximum (Config='{0}', Min='{1}', Max='{2}', Default='{3}').", new object[]
				{
					label,
					min,
					max,
					defaultValue
				}));
			}
			string text = (this.config.AppSettings.Settings[label] != null) ? this.config.AppSettings.Settings[label].Value : null;
			TimeSpan timeSpan = defaultValue;
			if (string.IsNullOrEmpty(text) || !TimeSpan.TryParse(text, out timeSpan) || timeSpan < min || timeSpan > max)
			{
				return defaultValue;
			}
			return timeSpan;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003154 File Offset: 0x00001354
		public string GetConfigString(string label, string defaultValue)
		{
			KeyValueConfigurationElement keyValueConfigurationElement = this.config.AppSettings.Settings[label];
			if (keyValueConfigurationElement != null)
			{
				return keyValueConfigurationElement.Value ?? defaultValue;
			}
			return defaultValue;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003188 File Offset: 0x00001388
		public bool GetConfigBool(string label, bool defaultValue)
		{
			KeyValueConfigurationElement keyValueConfigurationElement = this.config.AppSettings.Settings[label];
			if (keyValueConfigurationElement == null)
			{
				return defaultValue;
			}
			string value = keyValueConfigurationElement.Value;
			bool result = defaultValue;
			if (string.IsNullOrEmpty(value) || !bool.TryParse(value, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000031D0 File Offset: 0x000013D0
		protected List<SynchronizationProviderInfo> LoadSynchronizationProviders()
		{
			List<SynchronizationProviderInfo> list = new List<SynchronizationProviderInfo>();
			SyncProviderSection syncProviderSection = this.config.GetSection("SyncProviderSection") as SyncProviderSection;
			if (syncProviderSection != null)
			{
				foreach (object obj in syncProviderSection.SyncProviderElements)
				{
					SyncProviderElement syncProviderElement = (SyncProviderElement)obj;
					try
					{
						list.Add(new SynchronizationProviderInfo(syncProviderElement.Name, syncProviderElement.AssemblyPath, syncProviderElement.SynchronizationProvider, syncProviderElement.Enabled));
					}
					catch (ConfigurationErrorsException)
					{
					}
				}
			}
			return list;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000327C File Offset: 0x0000147C
		private Enum GetConfigEnum(string label, Type enumType, Enum defaultValue)
		{
			string value = (this.config.AppSettings.Settings[label] != null) ? this.config.AppSettings.Settings[label].Value : null;
			if (string.IsNullOrEmpty(value))
			{
				return defaultValue;
			}
			Enum result;
			try
			{
				result = (Enum)Enum.Parse(enumType, value, true);
			}
			catch (ArgumentException)
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x04000038 RID: 56
		private static object initializationLock = new object();

		// Token: 0x04000039 RID: 57
		private static EdgeSyncAppConfig instance;

		// Token: 0x0400003A RID: 58
		private TimeSpan delayStart;

		// Token: 0x0400003B RID: 59
		private SyncTreeType enabledSyncType;

		// Token: 0x0400003C RID: 60
		private bool delayLdapEnabled;

		// Token: 0x0400003D RID: 61
		private TimeSpan delayLdapSearchRequest;

		// Token: 0x0400003E RID: 62
		private TimeSpan delayLdapUpdateRequest;

		// Token: 0x0400003F RID: 63
		private string delayLdapUpdateRequestContainingString;

		// Token: 0x04000040 RID: 64
		private List<SynchronizationProviderInfo> synchronizationProviderList;

		// Token: 0x04000041 RID: 65
		private long tenantSyncControlCacheSize;

		// Token: 0x04000042 RID: 66
		private TimeSpan tenantSyncControlCacheExpiryInterval;

		// Token: 0x04000043 RID: 67
		private TimeSpan tenantSyncControlCacheCleanupInterval;

		// Token: 0x04000044 RID: 68
		private TimeSpan tenantSyncControlCachePurgeInterval;

		// Token: 0x04000045 RID: 69
		private bool trackDuplicatedAddEntries;

		// Token: 0x04000046 RID: 70
		private int duplicatedAddEntriesCacheSize;

		// Token: 0x04000047 RID: 71
		private int podSiteStartRange;

		// Token: 0x04000048 RID: 72
		private int podSiteEndRange;

		// Token: 0x04000049 RID: 73
		private Configuration config;
	}
}

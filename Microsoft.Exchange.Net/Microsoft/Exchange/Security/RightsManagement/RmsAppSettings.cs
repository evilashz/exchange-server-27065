using System;
using System.Configuration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009B7 RID: 2487
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RmsAppSettings
	{
		// Token: 0x060035F2 RID: 13810 RVA: 0x00089280 File Offset: 0x00087480
		private RmsAppSettings()
		{
			this.activeAgentCapDeferInterval = RmsAppSettings.GetConfigTimeSpan("RmsActiveAgentCapDeferInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromHours(1.0), TimeSpan.FromSeconds(10.0));
			this.transientErrorDeferInterval = RmsAppSettings.GetConfigTimeSpan("RmsTransientErrorDeferInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromHours(1.0), TimeSpan.FromSeconds(10.0));
			this.encryptionTransientErrorDeferInterval = RmsAppSettings.GetConfigTimeSpan("RmsEncryptionTransientErrorDeferInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromHours(1.0), TimeSpan.FromMinutes(10.0));
			this.jrdTransientErrorDeferInterval = RmsAppSettings.GetConfigTimeSpan("RmsJrdTransientErrorDeferInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromHours(1.0), TimeSpan.FromMinutes(5.0));
			int configInt = RmsAppSettings.GetConfigInt("RmsRacClcCacheSizeInMB", 1, 50, 10);
			this.racClcCacheSizeInBytes = (long)(configInt * 1048576);
			this.racClcCacheExpirationInterval = RmsAppSettings.GetConfigTimeSpan("RmsRacClcCacheExpirationInterval", TimeSpan.FromMinutes(1.0), TimeSpan.FromDays(30.0), TimeSpan.FromDays(10.0));
			this.racClcStoreExpirationInterval = RmsAppSettings.GetConfigTimeSpan("RmsRacClcStoreExpirationInterval", TimeSpan.FromMinutes(1.0), TimeSpan.FromDays(365.0), TimeSpan.FromMinutes(0.0));
			this.maxRacClcEntryCount = RmsAppSettings.GetConfigInt("RmsMaxRacClcEntryCount", 1, 10000, 5000);
			this.maxServerInfoEntryCount = RmsAppSettings.GetConfigInt("RmsMaxServerInfoEntryCount", 1, 10000, 5000);
			int configInt2 = RmsAppSettings.GetConfigInt("RmsTemplateCacheSizeInMB", 1, 50, 20);
			this.templateCacheSizeInBytes = (long)(configInt2 * 1048576);
			this.templateCacheExpirationInterval = RmsAppSettings.GetConfigTimeSpan("RmsTemplateCacheExpirationInterval", TimeSpan.FromMinutes(1.0), TimeSpan.FromDays(1.0), TimeSpan.FromDays(1.0));
			this.negativeServerInfoCacheExpirationInterval = RmsAppSettings.GetConfigTimeSpan("RmsNegativeServerInfoCacheExpirationInterval", TimeSpan.FromMinutes(1.0), TimeSpan.FromDays(1.0), TimeSpan.FromMinutes(5.0));
			this.timeoutForRmsSoapQueries = RmsAppSettings.GetConfigTimeSpan("RmsSoapQueriesTimeOut", TimeSpan.FromSeconds(10.0), TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(30.0));
			this.isSamlAuthenticationEnabledForInternalRMS = RmsAppSettings.GetConfigBool("RmsEnableSamlAuthenticationforInternalRMS", false);
			this.acquireLicenseBatchSize = RmsAppSettings.GetConfigInt("AcquireLicenseBatchSize", 1, 100, 100);
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x060035F3 RID: 13811 RVA: 0x00089538 File Offset: 0x00087738
		public static RmsAppSettings Instance
		{
			get
			{
				RmsAppSettings rmsAppSettings = RmsAppSettings.instance;
				if (rmsAppSettings == null)
				{
					lock (RmsAppSettings.syncRoot)
					{
						rmsAppSettings = RmsAppSettings.instance;
						if (rmsAppSettings == null)
						{
							rmsAppSettings = new RmsAppSettings();
							RmsAppSettings.instance = rmsAppSettings;
						}
					}
				}
				return rmsAppSettings;
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x060035F4 RID: 13812 RVA: 0x00089590 File Offset: 0x00087790
		public TimeSpan TransientErrorDeferInterval
		{
			get
			{
				return this.transientErrorDeferInterval;
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x060035F5 RID: 13813 RVA: 0x00089598 File Offset: 0x00087798
		public TimeSpan EncryptionTransientErrorDeferInterval
		{
			get
			{
				return this.encryptionTransientErrorDeferInterval;
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x060035F6 RID: 13814 RVA: 0x000895A0 File Offset: 0x000877A0
		public TimeSpan JrdTransientErrorDeferInterval
		{
			get
			{
				return this.jrdTransientErrorDeferInterval;
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x060035F7 RID: 13815 RVA: 0x000895A8 File Offset: 0x000877A8
		public TimeSpan ActiveAgentCapDeferInterval
		{
			get
			{
				return this.activeAgentCapDeferInterval;
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x060035F8 RID: 13816 RVA: 0x000895B0 File Offset: 0x000877B0
		public long RacClcCacheSizeInBytes
		{
			get
			{
				return this.racClcCacheSizeInBytes;
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x060035F9 RID: 13817 RVA: 0x000895B8 File Offset: 0x000877B8
		public TimeSpan RacClcCacheExpirationInterval
		{
			get
			{
				return this.racClcCacheExpirationInterval;
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x060035FA RID: 13818 RVA: 0x000895C0 File Offset: 0x000877C0
		public TimeSpan RacClcStoreExpirationInterval
		{
			get
			{
				return this.racClcStoreExpirationInterval;
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x060035FB RID: 13819 RVA: 0x000895C8 File Offset: 0x000877C8
		public int MaxRacClcEntryCount
		{
			get
			{
				return this.maxRacClcEntryCount;
			}
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x060035FC RID: 13820 RVA: 0x000895D0 File Offset: 0x000877D0
		public int MaxServerInfoEntryCount
		{
			get
			{
				return this.maxServerInfoEntryCount;
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x060035FD RID: 13821 RVA: 0x000895D8 File Offset: 0x000877D8
		public long TemplateCacheSizeInBytes
		{
			get
			{
				return this.templateCacheSizeInBytes;
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x060035FE RID: 13822 RVA: 0x000895E0 File Offset: 0x000877E0
		public TimeSpan TemplateCacheExpirationInterval
		{
			get
			{
				return this.templateCacheExpirationInterval;
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x060035FF RID: 13823 RVA: 0x000895E8 File Offset: 0x000877E8
		public TimeSpan NegativeServerInfoCacheExpirationInterval
		{
			get
			{
				return this.negativeServerInfoCacheExpirationInterval;
			}
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06003600 RID: 13824 RVA: 0x000895F0 File Offset: 0x000877F0
		public TimeSpan RmsSoapQueriesTimeout
		{
			get
			{
				return this.timeoutForRmsSoapQueries;
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06003601 RID: 13825 RVA: 0x000895F8 File Offset: 0x000877F8
		// (set) Token: 0x06003602 RID: 13826 RVA: 0x00089600 File Offset: 0x00087800
		public bool IsSamlAuthenticationEnabledForInternalRMS
		{
			get
			{
				return this.isSamlAuthenticationEnabledForInternalRMS;
			}
			set
			{
				this.isSamlAuthenticationEnabledForInternalRMS = value;
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06003603 RID: 13827 RVA: 0x00089609 File Offset: 0x00087809
		public int AcquireLicenseBatchSize
		{
			get
			{
				return this.acquireLicenseBatchSize;
			}
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x00089614 File Offset: 0x00087814
		private static TimeSpan GetConfigTimeSpan(string label, TimeSpan min, TimeSpan max, TimeSpan defaultValue)
		{
			string text;
			try
			{
				text = ConfigurationManager.AppSettings[label];
			}
			catch (ConfigurationErrorsException)
			{
				return defaultValue;
			}
			TimeSpan timeSpan = defaultValue;
			if (string.IsNullOrEmpty(text) || !TimeSpan.TryParse(text, out timeSpan) || timeSpan < min || timeSpan > max)
			{
				return defaultValue;
			}
			return timeSpan;
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x00089670 File Offset: 0x00087870
		private static int GetConfigInt(string label, int min, int max, int defaultValue)
		{
			string text;
			try
			{
				text = ConfigurationManager.AppSettings[label];
			}
			catch (ConfigurationErrorsException)
			{
				return defaultValue;
			}
			int num = defaultValue;
			if (string.IsNullOrEmpty(text) || !int.TryParse(text, out num) || num < min || num > max)
			{
				return defaultValue;
			}
			return num;
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000896C4 File Offset: 0x000878C4
		private static bool GetConfigBool(string label, bool defaultValue)
		{
			string value;
			try
			{
				value = ConfigurationManager.AppSettings[label];
			}
			catch (ConfigurationErrorsException)
			{
				return defaultValue;
			}
			bool result;
			if (!bool.TryParse(value, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x04002E6A RID: 11882
		private const int NumberOfBytesPerMB = 1048576;

		// Token: 0x04002E6B RID: 11883
		private static object syncRoot = new object();

		// Token: 0x04002E6C RID: 11884
		private static RmsAppSettings instance;

		// Token: 0x04002E6D RID: 11885
		private TimeSpan transientErrorDeferInterval;

		// Token: 0x04002E6E RID: 11886
		private TimeSpan encryptionTransientErrorDeferInterval;

		// Token: 0x04002E6F RID: 11887
		private TimeSpan jrdTransientErrorDeferInterval;

		// Token: 0x04002E70 RID: 11888
		private TimeSpan activeAgentCapDeferInterval;

		// Token: 0x04002E71 RID: 11889
		private long racClcCacheSizeInBytes;

		// Token: 0x04002E72 RID: 11890
		private TimeSpan racClcCacheExpirationInterval;

		// Token: 0x04002E73 RID: 11891
		private TimeSpan racClcStoreExpirationInterval;

		// Token: 0x04002E74 RID: 11892
		private int maxRacClcEntryCount;

		// Token: 0x04002E75 RID: 11893
		private int maxServerInfoEntryCount;

		// Token: 0x04002E76 RID: 11894
		private long templateCacheSizeInBytes;

		// Token: 0x04002E77 RID: 11895
		private TimeSpan templateCacheExpirationInterval;

		// Token: 0x04002E78 RID: 11896
		private TimeSpan negativeServerInfoCacheExpirationInterval;

		// Token: 0x04002E79 RID: 11897
		private TimeSpan timeoutForRmsSoapQueries;

		// Token: 0x04002E7A RID: 11898
		private bool isSamlAuthenticationEnabledForInternalRMS;

		// Token: 0x04002E7B RID: 11899
		private int acquireLicenseBatchSize;
	}
}

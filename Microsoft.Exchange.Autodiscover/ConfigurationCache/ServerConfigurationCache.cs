using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Autodiscover.ConfigurationCache
{
	// Token: 0x0200002B RID: 43
	internal sealed class ServerConfigurationCache
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00007828 File Offset: 0x00005A28
		static ServerConfigurationCache()
		{
			int num = 15;
			int.TryParse(ConfigurationManager.AppSettings["Autodiscover.CacheDefaultRefreshCycleInterval"], out num);
			if (num < 1 || num > 60)
			{
				ExTraceGlobals.FrameworkTracer.TraceError<int, int>(0L, "[ServerConfigurationCache] refresh cycle interval value {0} is out of range. Reset to default value {1}", num, 15);
				num = 15;
			}
			using (ActivityContext.SuppressThreadScope())
			{
				ServerConfigurationCache.cacheRefreshTimer = new Timer(new TimerCallback(ServerConfigurationCache.UpdateCacheCallback), null, TimeSpan.FromSeconds(0.0), TimeSpan.FromMinutes((double)num));
				AppDomain.CurrentDomain.DomainUnload += ServerConfigurationCache.ApplicationDomainUnload;
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000079F0 File Offset: 0x00005BF0
		private ServerConfigurationCache()
		{
			this.oabExtensionAttribute = null;
			this.DoWrappedRegistryRead("SYSTEM\\CurrentControlSet\\Services\\MSExchange Autodiscover", delegate(RegistryKey key)
			{
				this.oabExtensionAttribute = (string)key.GetValue("OAB Extension Attribute");
			});
			this.serverCache = new ServerCache();
			this.mdbCache = new MailboxDatabaseCache();
			this.oabCache = new OabCache();
			this.outlookProviderCache = new OutlookProviderCache();
			this.clientAccessArrayCache = new ClientAccessArrayCache();
			this.siteCache = new SiteCache();
			this.scpCache = new ADServiceConnectionPointCache();
			this.allCaches = new List<IConfigCache>();
			this.allCaches.Add(this.serverCache);
			this.allCaches.Add(this.mdbCache);
			this.allCaches.Add(this.outlookProviderCache);
			this.allCaches.Add(this.clientAccessArrayCache);
			this.allCaches.Add(this.siteCache);
			this.allCaches.Add(this.scpCache);
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Autodiscover.LoadNegoExSspNames.Enabled)
			{
				this.DoWrappedRegistryRead("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ExchangeLiveServices", delegate(RegistryKey key)
				{
					string[] array = (string[])key.GetValue("NegoExSSPNames");
					if (array != null)
					{
						foreach (string item in array)
						{
							this.negoExSSPNames.Add(item);
						}
					}
				});
			}
			this.DoWrappedRegistryRead("SYSTEM\\CurrentControlSet\\Services\\MSExchangeRPC", delegate(RegistryKey key)
			{
				string text = key.GetValue("MinimumMapiHttpAutodiscoverVersion") as string;
				if (string.IsNullOrEmpty(text))
				{
					this.enableMapiHttpAutodiscover = null;
					this.minimumMapiHttpAutodiscoverVersion = null;
					return;
				}
				int num;
				if (int.TryParse(text, out num) && num == 0)
				{
					this.enableMapiHttpAutodiscover = new bool?(false);
					this.minimumMapiHttpAutodiscoverVersion = null;
					return;
				}
				Version version;
				if (Version.TryParse(text, out version))
				{
					this.minimumMapiHttpAutodiscoverVersion = new Version(version.Major, version.Minor, version.Build, 0);
					this.enableMapiHttpAutodiscover = new bool?(true);
					return;
				}
				this.enableMapiHttpAutodiscover = null;
				this.minimumMapiHttpAutodiscoverVersion = null;
			});
			this.allCachesAreLoaded = false;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007B5C File Offset: 0x00005D5C
		private void DoWrappedRegistryRead(string registrySubkeyName, ServerConfigurationCache.WrappedRegistryDelegate registryDelegate)
		{
			Exception ex = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registrySubkeyName))
				{
					if (registryKey != null)
					{
						registryDelegate(registryKey);
					}
				}
			}
			catch (SecurityException ex2)
			{
				ex = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ExTraceGlobals.FrameworkTracer.TraceError<string, Exception>(0L, "Failed to check for registry key {0}. The exception is: {1}", registrySubkeyName, ex);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00007BD8 File Offset: 0x00005DD8
		public bool AllCachesAreLoaded
		{
			get
			{
				return this.allCachesAreLoaded;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00007BE0 File Offset: 0x00005DE0
		internal static ServerConfigurationCache Singleton
		{
			get
			{
				return ServerConfigurationCache.singleton;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00007BE7 File Offset: 0x00005DE7
		internal ServerCache ServerCache
		{
			get
			{
				return this.serverCache;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00007BEF File Offset: 0x00005DEF
		internal MailboxDatabaseCache MdbCache
		{
			get
			{
				return this.mdbCache;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00007BF7 File Offset: 0x00005DF7
		internal OabCache OabCache
		{
			get
			{
				return this.oabCache;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00007BFF File Offset: 0x00005DFF
		internal OutlookProviderCache OutlookProviderCache
		{
			get
			{
				return this.outlookProviderCache;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00007C07 File Offset: 0x00005E07
		internal ClientAccessArrayCache ClientAccessArrayCache
		{
			get
			{
				return this.clientAccessArrayCache;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00007C0F File Offset: 0x00005E0F
		internal SiteCache SiteCache
		{
			get
			{
				return this.siteCache;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00007C17 File Offset: 0x00005E17
		internal ADServiceConnectionPointCache ScpCache
		{
			get
			{
				return this.scpCache;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00007C1F File Offset: 0x00005E1F
		internal string OabExtensionAttribute
		{
			get
			{
				return this.oabExtensionAttribute;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00007C27 File Offset: 0x00005E27
		internal HashSet<string> NegoExSSPNames
		{
			get
			{
				return this.negoExSSPNames;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00007C2F File Offset: 0x00005E2F
		internal bool? EnableMapiHttpAutodiscover
		{
			get
			{
				return this.enableMapiHttpAutodiscover;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00007C38 File Offset: 0x00005E38
		internal Version MinimumMapiHttpAutodiscoverVersion
		{
			get
			{
				if (this.EnableMapiHttpAutodiscover == null || !this.EnableMapiHttpAutodiscover.Value)
				{
					throw new InvalidOperationException("EnableMapiHttpAutodiscover must be true for this property to be valid.");
				}
				return this.minimumMapiHttpAutodiscoverVersion;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007C76 File Offset: 0x00005E76
		public static void ApplicationDomainUnload(object sender, EventArgs e)
		{
			if (ServerConfigurationCache.cacheRefreshTimer != null)
			{
				ServerConfigurationCache.cacheRefreshTimer.Dispose();
				ServerConfigurationCache.cacheRefreshTimer = null;
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007C90 File Offset: 0x00005E90
		internal static void UpdateCacheCallback(object stateInfo)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 460, "UpdateCacheCallback", "f:\\15.00.1497\\sources\\dev\\autodisc\\src\\ConfigurationCache\\ServerConfigurationCache.cs");
			tenantOrTopologyConfigurationSession.ServerTimeout = new TimeSpan?(ServerConfigurationCache.systemConfigLookupTimeout);
			ServerConfigurationCache.Singleton.RefreshCaches(tenantOrTopologyConfigurationSession);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007CDC File Offset: 0x00005EDC
		internal void RefreshCaches(IConfigurationSession configSession)
		{
			int num = 0;
			foreach (IConfigCache configCache in this.allCaches)
			{
				try
				{
					configCache.Refresh(configSession);
					num++;
				}
				catch (LocalizedException ex)
				{
					ExTraceGlobals.FrameworkTracer.TraceError<string, string>(0L, "[RefreshCaches()] 'LocalizedException' Message=\"{0}\";StackTrace=\"{1}\"", ex.Message, ex.StackTrace);
					Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_ErrWebException, Common.PeriodicKey, new object[]
					{
						ex.Message,
						ex.StackTrace
					});
				}
			}
			if (num == this.allCaches.Count)
			{
				this.allCachesAreLoaded = true;
			}
		}

		// Token: 0x0400015F RID: 351
		internal const string RpcConfigurationKeyName = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeRPC";

		// Token: 0x04000160 RID: 352
		internal const string MapiHttpValueName = "MinimumMapiHttpAutodiscoverVersion";

		// Token: 0x04000161 RID: 353
		private const int DefaultRefreshCycleInterval = 15;

		// Token: 0x04000162 RID: 354
		private const int MinRefreshCycleInterval = 1;

		// Token: 0x04000163 RID: 355
		private const int MaxRefreshCycleInterval = 60;

		// Token: 0x04000164 RID: 356
		private static readonly ServerConfigurationCache singleton = new ServerConfigurationCache();

		// Token: 0x04000165 RID: 357
		private static readonly TimeSpan systemConfigLookupTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000166 RID: 358
		private static Timer cacheRefreshTimer;

		// Token: 0x04000167 RID: 359
		private readonly ServerCache serverCache;

		// Token: 0x04000168 RID: 360
		private readonly MailboxDatabaseCache mdbCache;

		// Token: 0x04000169 RID: 361
		private readonly OabCache oabCache;

		// Token: 0x0400016A RID: 362
		private readonly ClientAccessArrayCache clientAccessArrayCache;

		// Token: 0x0400016B RID: 363
		private readonly SiteCache siteCache;

		// Token: 0x0400016C RID: 364
		private readonly OutlookProviderCache outlookProviderCache;

		// Token: 0x0400016D RID: 365
		private readonly ADServiceConnectionPointCache scpCache;

		// Token: 0x0400016E RID: 366
		private readonly List<IConfigCache> allCaches;

		// Token: 0x0400016F RID: 367
		private string oabExtensionAttribute;

		// Token: 0x04000170 RID: 368
		private bool allCachesAreLoaded;

		// Token: 0x04000171 RID: 369
		private HashSet<string> negoExSSPNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000172 RID: 370
		private bool? enableMapiHttpAutodiscover;

		// Token: 0x04000173 RID: 371
		private Version minimumMapiHttpAutodiscoverVersion;

		// Token: 0x0200002C RID: 44
		// (Invoke) Token: 0x0600015A RID: 346
		private delegate void WrappedRegistryDelegate(RegistryKey key);
	}
}

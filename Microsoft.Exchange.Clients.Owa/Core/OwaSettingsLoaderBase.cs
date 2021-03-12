using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Win32;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000201 RID: 513
	public abstract class OwaSettingsLoaderBase
	{
		// Token: 0x060010D9 RID: 4313 RVA: 0x00067450 File Offset: 0x00065650
		internal OwaSettingsLoaderBase()
		{
			if (OwaSettingsLoaderBase.instanceCreated)
			{
				throw new InvalidOperationException("Cannot load more than one OwaSettings");
			}
			OwaSettingsLoaderBase.instanceCreated = true;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0006747B File Offset: 0x0006567B
		internal virtual void Load()
		{
			OwaRegistryKeys.Initialize();
			OwaConfigurationManager.CreateAndLoadConfigurationManager();
			this.ReadServerCulture();
			this.InitializeLocalVersionFolders();
			this.ReadAutoSaveInterval();
			this.ReadChangeExpiredPasswordEnabled();
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0006749F File Offset: 0x0006569F
		internal virtual void Unload()
		{
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x000674A4 File Offset: 0x000656A4
		internal void InitializeLocalVersionFolders()
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "Globals.LoadVersionFolders");
			this.localHostVersion = ServerVersion.CreateFromVersionString(Globals.ApplicationVersion);
			if (this.localHostVersion == null)
			{
				throw new OwaInvalidOperationException("The local host version is invalid");
			}
			string appDomainAppPath = HttpRuntime.AppDomainAppPath;
			string[] directories = Directory.GetDirectories(appDomainAppPath);
			this.localVersionFolders = new Dictionary<ServerVersion, ServerVersion>(new ServerVersion.ServerVersionComparer());
			ExTraceGlobals.CoreDataTracer.TraceDebug<string>(0L, "Looking for version folders under \"{0}\"...", appDomainAppPath);
			foreach (string path in directories)
			{
				string fileName = Path.GetFileName(path);
				ServerVersion serverVersion = ServerVersion.CreateFromVersionString(fileName);
				if (serverVersion != null)
				{
					ExTraceGlobals.CoreDataTracer.TraceDebug<string>(0L, "Added version folder \"{0}\"", serverVersion.ToString());
					this.localVersionFolders.Add(serverVersion, serverVersion);
				}
			}
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00067564 File Offset: 0x00065764
		private void ReadChangeExpiredPasswordEnabled()
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "Globals.ReadChangeExpiredPasswordEnabled");
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA"))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("ChangeExpiredPasswordEnabled");
					if (value is int)
					{
						this.changeExpiredPasswordEnabled = ((int)value != 0);
					}
				}
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x000675D8 File Offset: 0x000657D8
		private void ReadServerCulture()
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "Globals.ReadServerCulture");
			int num = 0;
			bool flag = false;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Language"))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("ENU", 1033);
					if (value is int)
					{
						num = (int)value;
						flag = true;
					}
				}
			}
			if (!flag)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<int>(0L, "Failed to read server language from the registry, defaulting to {0}", 1033);
				this.serverCulture = Culture.GetCultureInfoInstance(1033);
				return;
			}
			ExTraceGlobals.CoreTracer.TraceDebug<int>(0L, "Succesfully read server language from registry = {0}", num);
			if (Culture.IsSupportedCulture(num))
			{
				this.serverCulture = Culture.GetCultureInfoInstance(num);
				return;
			}
			ExTraceGlobals.CoreTracer.TraceDebug<int, int>(0L, "The server culture read from the registry ({0}) is unsupported, defaulting to {1}", num, 1033);
			this.serverCulture = Culture.GetCultureInfoInstance(1033);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x000676C8 File Offset: 0x000658C8
		private void ReadAutoSaveInterval()
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "Globals.ReadAutoSaveInterval");
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA"))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("AutoSaveInterval");
					if (value is int && (int)value > 0)
					{
						this.autoSaveInterval = (int)value * 60;
					}
				}
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060010E0 RID: 4320
		public abstract bool IsPushNotificationsEnabled { get; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060010E1 RID: 4321
		public abstract bool IsPullNotificationsEnabled { get; }

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060010E2 RID: 4322
		public abstract bool IsFolderContentNotificationsEnabled { get; }

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x00067744 File Offset: 0x00065944
		public int MaxSearchStringLength
		{
			get
			{
				return 256;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x0006774B File Offset: 0x0006594B
		public CultureInfo ServerCulture
		{
			get
			{
				if (!Globals.IsInitialized && this.serverCulture == null)
				{
					this.serverCulture = Culture.GetCultureInfoInstance(1033);
				}
				return this.serverCulture;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x00067772 File Offset: 0x00065972
		public ServerVersion LocalHostVersion
		{
			get
			{
				return this.localHostVersion;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x0006777A File Offset: 0x0006597A
		public Dictionary<ServerVersion, ServerVersion> LocalVersionFolders
		{
			get
			{
				return this.localVersionFolders;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00067782 File Offset: 0x00065982
		public bool ArePerfCountersEnabled
		{
			get
			{
				return PerformanceCounterManager.ArePerfCountersEnabled;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x00067789 File Offset: 0x00065989
		public int AutoSaveInterval
		{
			get
			{
				return this.autoSaveInterval;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00067791 File Offset: 0x00065991
		public bool ChangeExpiredPasswordEnabled
		{
			get
			{
				return this.changeExpiredPasswordEnabled;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060010EA RID: 4330
		public abstract int ConnectionCacheSize { get; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0006779C File Offset: 0x0006599C
		public bool ShowDebugInformation
		{
			get
			{
				bool flag = false;
				bool flag2 = bool.TryParse(ConfigurationManager.AppSettings["ShowDebugInformation"], out flag);
				return flag2 && flag;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x000677C8 File Offset: 0x000659C8
		public bool EnableEmailReports
		{
			get
			{
				if (!this.ShowDebugInformation)
				{
					return false;
				}
				bool flag = false;
				bool flag2 = bool.TryParse(ConfigurationManager.AppSettings["EnableEmailReports"], out flag);
				return flag2 && flag;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060010ED RID: 4333
		public abstract bool IsPreCheckinApp { get; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060010EE RID: 4334
		public abstract bool ListenAdNotifications { get; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060010EF RID: 4335
		public abstract bool RenderBreadcrumbsInAboutPage { get; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x00067800 File Offset: 0x00065A00
		public bool CollectPerRequestPerformanceStats
		{
			get
			{
				bool flag = true;
				bool flag2 = bool.TryParse(ConfigurationManager.AppSettings["CollectPerRequestPerformanceStats"], out flag);
				return flag2 && flag;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0006782C File Offset: 0x00065A2C
		public bool CollectSearchStrings
		{
			get
			{
				bool flag = true;
				bool flag2 = bool.TryParse(ConfigurationManager.AppSettings["CollectSearchStrings"], out flag);
				return flag2 && flag;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x00067858 File Offset: 0x00065A58
		public bool DisablePrefixSearch
		{
			get
			{
				bool flag = true;
				bool flag2 = bool.TryParse(ConfigurationManager.AppSettings["DisablePrefixSearch"], out flag);
				return !flag2 || flag;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x00067884 File Offset: 0x00065A84
		public bool FilterETag
		{
			get
			{
				bool flag = false;
				bool flag2 = bool.TryParse(ConfigurationManager.AppSettings["FilterETag"], out flag);
				return flag2 && flag;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x000678B0 File Offset: 0x00065AB0
		public string ContentDeliveryNetworkEndpoint
		{
			get
			{
				string text = ConfigurationManager.AppSettings["ContentDeliveryNetworkEndpoint"];
				if (!string.IsNullOrEmpty(text))
				{
					if (text.EndsWith("/", StringComparison.OrdinalIgnoreCase))
					{
						text = text + Globals.VirtualRootName + "/";
					}
					else
					{
						text = text + "/" + Globals.VirtualRootName + "/";
					}
				}
				return text;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00067910 File Offset: 0x00065B10
		public string ErrorReportAddress
		{
			get
			{
				string text = ConfigurationManager.AppSettings["ErrorReportAddress"];
				if (string.IsNullOrEmpty(text))
				{
					return "owadbg@microsoft.com";
				}
				return text;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060010F6 RID: 4342
		public abstract int MaximumTemporaryFilteredViewPerUser { get; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060010F7 RID: 4343
		public abstract int MaximumFilteredViewInFavoritesPerUser { get; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x0006793C File Offset: 0x00065B3C
		public bool SendWatsonReports
		{
			get
			{
				bool flag = true;
				bool flag2 = bool.TryParse(ConfigurationManager.AppSettings["SendWatsonReport"], out flag);
				return !flag2 || flag;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00067968 File Offset: 0x00065B68
		public bool SendClientWatsonReports
		{
			get
			{
				bool flag2;
				bool flag = bool.TryParse(ConfigurationManager.AppSettings["SendClientWatsonReport"], out flag2);
				return flag && flag2;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060010FA RID: 4346
		public abstract bool DisableBreadcrumbs { get; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00067994 File Offset: 0x00065B94
		public int ServicePointConnectionLimit
		{
			get
			{
				int result = 0;
				bool flag = int.TryParse(ConfigurationManager.AppSettings["ServicePointConnectionLimit"], out result);
				if (flag)
				{
					return result;
				}
				return 65000;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x000679C4 File Offset: 0x00065BC4
		public bool ProxyToLocalHost
		{
			get
			{
				bool flag = false;
				bool flag2 = bool.TryParse(ConfigurationManager.AppSettings["ProxyToLocalHost"], out flag);
				return flag2 && flag;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060010FD RID: 4349
		public abstract int MaxBreadcrumbs { get; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060010FE RID: 4350
		public abstract bool StoreTransientExceptionEventLogEnabled { get; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060010FF RID: 4351
		public abstract int StoreTransientExceptionEventLogThreshold { get; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001100 RID: 4352
		public abstract int StoreTransientExceptionEventLogFrequencyInSeconds { get; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001101 RID: 4353
		public abstract int MaxPendingRequestLifeInSeconds { get; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001102 RID: 4354
		public abstract int MaxItemsInConversationExpansion { get; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001103 RID: 4355
		public abstract int MaxItemsInConversationReadingPane { get; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001104 RID: 4356
		public abstract long MaxBytesInConversationReadingPane { get; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001105 RID: 4357
		public abstract bool HideDeletedItems { get; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001106 RID: 4358
		public abstract string OCSServerName { get; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001107 RID: 4359
		public abstract int ActivityBasedPresenceDuration { get; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001108 RID: 4360
		public abstract int MailTipsMaxClientCacheSize { get; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001109 RID: 4361
		public abstract int MailTipsMaxMailboxSourcedRecipientSize { get; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600110A RID: 4362
		public abstract int MailTipsClientCacheEntryExpiryInHours { get; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600110B RID: 4363
		internal abstract PhishingLevel MinimumSuspiciousPhishingLevel { get; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600110C RID: 4364
		internal abstract int UserContextLockTimeout { get; }

		// Token: 0x04000B74 RID: 2932
		private const int FailoverServerLcid = 1033;

		// Token: 0x04000B75 RID: 2933
		private const bool DefaultProxyToLocalHost = false;

		// Token: 0x04000B76 RID: 2934
		private const int MaximumSearchStringLength = 256;

		// Token: 0x04000B77 RID: 2935
		private const int DefaultServicePointConnectionLimit = 65000;

		// Token: 0x04000B78 RID: 2936
		private static bool instanceCreated;

		// Token: 0x04000B79 RID: 2937
		private ServerVersion localHostVersion;

		// Token: 0x04000B7A RID: 2938
		private Dictionary<ServerVersion, ServerVersion> localVersionFolders;

		// Token: 0x04000B7B RID: 2939
		private CultureInfo serverCulture;

		// Token: 0x04000B7C RID: 2940
		private int autoSaveInterval = 300;

		// Token: 0x04000B7D RID: 2941
		private bool changeExpiredPasswordEnabled;
	}
}

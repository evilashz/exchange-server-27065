using System;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000CF RID: 207
	internal class ConnectedAccountsConfiguration : IConnectedAccountsConfiguration
	{
		// Token: 0x0600082D RID: 2093 RVA: 0x0001AF0D File Offset: 0x0001910D
		private ConnectedAccountsConfiguration()
		{
			this.Load();
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0001AF1C File Offset: 0x0001911C
		public static ConnectedAccountsConfiguration Instance
		{
			get
			{
				if (ConnectedAccountsConfiguration.instance == null)
				{
					lock (ConnectedAccountsConfiguration.instanceSyncLock)
					{
						if (ConnectedAccountsConfiguration.instance == null)
						{
							ConnectedAccountsConfiguration.instance = new ConnectedAccountsConfiguration();
						}
					}
				}
				return ConnectedAccountsConfiguration.instance;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0001AF74 File Offset: 0x00019174
		public bool LogonTriggeredSyncNowEnabled
		{
			get
			{
				return this.logonTriggeredSyncNowEnabled;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0001AF7C File Offset: 0x0001917C
		public bool RefreshButtonTriggeredSyncNowEnabled
		{
			get
			{
				return this.refreshButtonTriggeredSyncNowEnabled;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0001AF84 File Offset: 0x00019184
		public TimeSpan RefreshButtonTriggeredSyncNowSuppressThreshold
		{
			get
			{
				return this.refreshButtonTriggeredSyncNowSuppressThreshold;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0001AF8C File Offset: 0x0001918C
		public bool PeriodicSyncNowEnabled
		{
			get
			{
				return this.periodicSyncNowEnabled;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x0001AF94 File Offset: 0x00019194
		public TimeSpan PeriodicSyncNowInterval
		{
			get
			{
				return this.periodicSyncNowInterval;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x0001AF9C File Offset: 0x0001919C
		public bool NotificationsEnabled
		{
			get
			{
				return this.notificationsEnabled;
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001AFA4 File Offset: 0x000191A4
		protected virtual void Load()
		{
			this.logonTriggeredSyncNowEnabled = BaseApplication.GetAppSetting<bool>("UserLogonTriggeredSyncNowEnabled", true);
			this.refreshButtonTriggeredSyncNowEnabled = BaseApplication.GetAppSetting<bool>("RefreshButtonTriggeredSyncNowEnabled", true);
			this.refreshButtonTriggeredSyncNowSuppressThreshold = BaseApplication.GetTimeSpanAppSetting("RefreshButtonTriggeredSyncNowSuppressThreshold", TimeSpan.FromSeconds(5.0));
			this.periodicSyncNowEnabled = BaseApplication.GetAppSetting<bool>("PeriodicSyncNowEnabled", true);
			this.periodicSyncNowInterval = BaseApplication.GetTimeSpanAppSetting("PeriodicSyncNowInterval", TimeSpan.FromMinutes(15.0));
			this.notificationsEnabled = (this.refreshButtonTriggeredSyncNowEnabled || this.logonTriggeredSyncNowEnabled || this.periodicSyncNowEnabled);
			ExTraceGlobals.ConnectedAccountsTracer.TraceDebug((long)this.GetHashCode(), "ConnectedAccountsConfiguration::Configs Loaded - logonTriggeredSyncNowEnabled:{0},periodicSyncNowEnabled:{1},refreshButtonTriggeredSyncNowEnabled:{2},periodicSyncNowInterval:{3},refreshButtonTriggeredSyncNowSuppressThreshold:{4}.", new object[]
			{
				this.logonTriggeredSyncNowEnabled,
				this.periodicSyncNowEnabled,
				this.refreshButtonTriggeredSyncNowEnabled,
				this.periodicSyncNowInterval,
				this.refreshButtonTriggeredSyncNowSuppressThreshold
			});
		}

		// Token: 0x0400048E RID: 1166
		private static readonly object instanceSyncLock = new object();

		// Token: 0x0400048F RID: 1167
		private static ConnectedAccountsConfiguration instance;

		// Token: 0x04000490 RID: 1168
		private bool logonTriggeredSyncNowEnabled;

		// Token: 0x04000491 RID: 1169
		private bool refreshButtonTriggeredSyncNowEnabled;

		// Token: 0x04000492 RID: 1170
		private TimeSpan refreshButtonTriggeredSyncNowSuppressThreshold;

		// Token: 0x04000493 RID: 1171
		private bool periodicSyncNowEnabled;

		// Token: 0x04000494 RID: 1172
		private TimeSpan periodicSyncNowInterval;

		// Token: 0x04000495 RID: 1173
		private bool notificationsEnabled;
	}
}

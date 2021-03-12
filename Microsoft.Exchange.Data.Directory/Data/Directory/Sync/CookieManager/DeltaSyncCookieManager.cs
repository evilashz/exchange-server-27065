using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007D0 RID: 2000
	internal abstract class DeltaSyncCookieManager : CookieManager
	{
		// Token: 0x1700234F RID: 9039
		// (get) Token: 0x0600635A RID: 25434 RVA: 0x00158B36 File Offset: 0x00156D36
		// (set) Token: 0x0600635B RID: 25435 RVA: 0x00158B3E File Offset: 0x00156D3E
		internal string ServiceInstanceName { get; private set; }

		// Token: 0x17002350 RID: 9040
		// (get) Token: 0x0600635C RID: 25436 RVA: 0x00158B47 File Offset: 0x00156D47
		// (set) Token: 0x0600635D RID: 25437 RVA: 0x00158B4F File Offset: 0x00156D4F
		public ServerVersion SyncPropertySetVersion { get; protected set; }

		// Token: 0x17002351 RID: 9041
		// (get) Token: 0x0600635E RID: 25438 RVA: 0x00158B58 File Offset: 0x00156D58
		// (set) Token: 0x0600635F RID: 25439 RVA: 0x00158B60 File Offset: 0x00156D60
		public bool IsSyncPropertySetUpgrading { get; protected set; }

		// Token: 0x06006360 RID: 25440 RVA: 0x00158B6C File Offset: 0x00156D6C
		internal DeltaSyncCookieManager(string serviceInstanceName, int maxCookieHistoryCount, TimeSpan cookieHistoryInterval)
		{
			if (string.IsNullOrEmpty(serviceInstanceName))
			{
				throw new ArgumentNullException("serviceInstanceName");
			}
			this.ServiceInstanceName = serviceInstanceName;
			this.maxCookieHistoryCount = maxCookieHistoryCount;
			this.cookieHistoryInterval = cookieHistoryInterval;
			this.SyncPropertySetVersion = new ServerVersion(0, 0, 0, 0);
			this.IsSyncPropertySetUpgrading = false;
			this.nextTimeToUpdateIsSyncPropertySetUpgradeAllowed = DateTime.UtcNow;
			this.isSyncPropertySetUpgradeAllowed = false;
		}

		// Token: 0x06006361 RID: 25441 RVA: 0x00158BCF File Offset: 0x00156DCF
		public bool SyncPropertySetUpgradeAvailable(ServerVersion version)
		{
			return this.IsSyncPropertySetUpgradeAllowed() && !this.IsSyncPropertySetUpgrading && ServerVersion.Compare(this.SyncPropertySetVersion, version) < 0;
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x00158BF7 File Offset: 0x00156DF7
		public sealed override void WriteCookie(byte[] cookie, DateTime timestamp)
		{
			this.WriteCookie(cookie, null, timestamp, false, null, true);
		}

		// Token: 0x06006363 RID: 25443
		public abstract void WriteCookie(byte[] cookie, IEnumerable<string> filteredContextIds, DateTime timestamp, bool isUpgradingCookie, ServerVersion version, bool more);

		// Token: 0x06006364 RID: 25444 RVA: 0x00158C08 File Offset: 0x00156E08
		public bool IsSyncPropertySetUpgradeAllowed()
		{
			if (this.nextTimeToUpdateIsSyncPropertySetUpgradeAllowed < DateTime.UtcNow)
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 152, "IsSyncPropertySetUpgradeAllowed", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\CookieManager\\DeltaSyncCookieManager.cs");
				Organization orgContainer = topologyConfigurationSession.GetOrgContainer();
				this.isSyncPropertySetUpgradeAllowed = orgContainer.IsSyncPropertySetUpgradeAllowed;
				this.nextTimeToUpdateIsSyncPropertySetUpgradeAllowed = DateTime.UtcNow.AddMinutes(10.0);
			}
			return this.isSyncPropertySetUpgradeAllowed;
		}

		// Token: 0x06006365 RID: 25445 RVA: 0x00158C7C File Offset: 0x00156E7C
		protected void UpdateSyncPropertySetVersion(bool isUpgradingCookie, ServerVersion version, bool more)
		{
			if (isUpgradingCookie)
			{
				this.IsSyncPropertySetUpgrading = true;
				this.SyncPropertySetVersion = version;
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_SyncPropertySetStartingUpgrade, this.ServiceInstanceName, new object[]
				{
					this.SyncPropertySetVersion
				});
				return;
			}
			if (this.IsSyncPropertySetUpgrading && !more)
			{
				this.IsSyncPropertySetUpgrading = false;
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_SyncPropertySetFinishedUpgrade, this.ServiceInstanceName, new object[]
				{
					this.SyncPropertySetVersion
				});
			}
		}

		// Token: 0x04004241 RID: 16961
		protected int maxCookieHistoryCount;

		// Token: 0x04004242 RID: 16962
		protected TimeSpan cookieHistoryInterval;

		// Token: 0x04004243 RID: 16963
		protected bool isSyncPropertySetUpgradeAllowed;

		// Token: 0x04004244 RID: 16964
		protected DateTime nextTimeToUpdateIsSyncPropertySetUpgradeAllowed;
	}
}

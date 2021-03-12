using System;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000341 RID: 833
	internal sealed class ArbitrationConfigFromAD
	{
		// Token: 0x06001CC3 RID: 7363 RVA: 0x0007F67C File Offset: 0x0007D87C
		public ArbitrationConfigFromAD(SyncServiceInstance syncDaemonArbitrationConfig, RidMasterInfo ridMasterInfo)
		{
			this.SyncDaemonArbitrationConfig = syncDaemonArbitrationConfig;
			this.RidMasterInfo = ridMasterInfo;
			this.PassiveInstanceSleepInterval = TimeSpan.FromSeconds((double)this.SyncDaemonArbitrationConfig.PassiveInstanceSleepInterval);
			this.ActiveInstanceSleepInterval = TimeSpan.FromSeconds((double)this.SyncDaemonArbitrationConfig.ActiveInstanceSleepInterval);
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x0007F6CB File Offset: 0x0007D8CB
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x0007F6D3 File Offset: 0x0007D8D3
		public TimeSpan ActiveInstanceSleepInterval { get; private set; }

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x0007F6DC File Offset: 0x0007D8DC
		// (set) Token: 0x06001CC7 RID: 7367 RVA: 0x0007F6E4 File Offset: 0x0007D8E4
		public TimeSpan PassiveInstanceSleepInterval { get; private set; }

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x0007F6ED File Offset: 0x0007D8ED
		public Guid CurrentSiteGuid
		{
			get
			{
				return ArbitrationConfigFromAD.SiteGuid.Value;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x0007F6F9 File Offset: 0x0007D8F9
		// (set) Token: 0x06001CCA RID: 7370 RVA: 0x0007F701 File Offset: 0x0007D901
		public SyncServiceInstance SyncDaemonArbitrationConfig { get; private set; }

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x0007F70A File Offset: 0x0007D90A
		// (set) Token: 0x06001CCC RID: 7372 RVA: 0x0007F712 File Offset: 0x0007D912
		public RidMasterInfo RidMasterInfo { get; private set; }

		// Token: 0x0400185A RID: 6234
		private static readonly Lazy<Guid> SiteGuid = new Lazy<Guid>(() => LocalSiteCache.LocalSite.Guid);
	}
}

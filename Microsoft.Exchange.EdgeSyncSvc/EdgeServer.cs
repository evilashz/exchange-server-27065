using System;
using System.Collections.Generic;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Logging;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000002 RID: 2
	internal class EdgeServer
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public EdgeServer(TargetServerConfig config, int leaseLockTryCount)
		{
			this.config = config;
			this.leaseLockTryCount = leaseLockTryCount;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020F1 File Offset: 0x000002F1
		public TargetServerConfig Config
		{
			get
			{
				return this.config;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020F9 File Offset: 0x000002F9
		public string Name
		{
			get
			{
				return this.config.Name;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002106 File Offset: 0x00000306
		public int Port
		{
			get
			{
				return this.config.Port;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002113 File Offset: 0x00000313
		public string Host
		{
			get
			{
				return this.config.Host;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002120 File Offset: 0x00000320
		public Lease EdgeLease
		{
			get
			{
				return this.lease;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002128 File Offset: 0x00000328
		public IEnumerable<SyncTreeType> Types
		{
			get
			{
				return this.synchronizerManagers.Keys;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002135 File Offset: 0x00000335
		public void InitializeLease(string leaseHolderPath, int leaseHolderVersion, EdgeSyncLogSession logSession, TestShutdownDelegate testShutdown)
		{
			this.lease = new Lease(leaseHolderPath, leaseHolderVersion, logSession, this.leaseLockTryCount, testShutdown);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002150 File Offset: 0x00000350
		public void RefreshConfig(TargetServerConfig config, SynchronizationProvider provider)
		{
			this.config = config;
			foreach (KeyValuePair<SyncTreeType, SynchronizerManager> keyValuePair in this.synchronizerManagers)
			{
				keyValuePair.Value.Interval = ((keyValuePair.Key == SyncTreeType.Configuration) ? provider.ConfigurationSyncInterval : provider.RecipientSyncInterval);
				keyValuePair.Value.LogSession.LoggingLevel = EdgeSyncSvc.EdgeSync.Config.ServiceConfig.LogLevel;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021F4 File Offset: 0x000003F4
		public void ConfigureSynchronizerManager(SyncTreeType type, SynchronizationProvider provider, EdgeSyncLogSession logSession)
		{
			List<TypeSynchronizer> list = provider.CreateTypeSynchronizer(type);
			if (list.Count == 0)
			{
				return;
			}
			if (this.synchronizerManagers.ContainsKey(type))
			{
				SynchronizerManager synchronizerManager = this.synchronizerManagers[type];
				foreach (TypeSynchronizer item in list)
				{
					synchronizerManager.TypeSynchronizers.Add(item);
				}
				return;
			}
			if (type == SyncTreeType.Configuration)
			{
				this.synchronizerManagers.Add(type, new ConfigSynchronizerManager(this, provider, list, EdgeSyncSvc.EdgeSync.ConfigSession, EdgeSyncSvc.EdgeSync.ConfigSession, EdgeSyncSvc.EdgeSync.SyncNowState, logSession));
				return;
			}
			this.synchronizerManagers.Add(type, new RecipientSynchronizerManager(this, provider, list, EdgeSyncSvc.EdgeSync.ConfigSession, EdgeSyncSvc.EdgeSync.RecipientSession, EdgeSyncSvc.EdgeSync.SyncNowState, logSession));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022E0 File Offset: 0x000004E0
		public void Synchronize()
		{
			foreach (SynchronizerManager synchronizerManager in this.synchronizerManagers.Values)
			{
				synchronizerManager.BeginExecute();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002338 File Offset: 0x00000538
		public void SynchronizeNow(bool forceFullSync, bool forceUpdateCookie)
		{
			foreach (SynchronizerManager synchronizerManager in this.synchronizerManagers.Values)
			{
				synchronizerManager.StartNow = true;
				synchronizerManager.ForceFullSync = forceFullSync;
				synchronizerManager.ForceUpdateCookie = forceUpdateCookie;
				synchronizerManager.BeginExecute();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023A4 File Offset: 0x000005A4
		public void InitiateShutdown()
		{
			foreach (SynchronizerManager synchronizerManager in this.synchronizerManagers.Values)
			{
				synchronizerManager.InitiateShutdown();
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023FC File Offset: 0x000005FC
		public bool HasShutdown()
		{
			foreach (SynchronizerManager synchronizerManager in this.synchronizerManagers.Values)
			{
				if (!synchronizerManager.HasShutdown)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000001 RID: 1
		private int leaseLockTryCount;

		// Token: 0x04000002 RID: 2
		private TargetServerConfig config;

		// Token: 0x04000003 RID: 3
		private Lease lease;

		// Token: 0x04000004 RID: 4
		private Dictionary<SyncTreeType, SynchronizerManager> synchronizerManagers = new Dictionary<SyncTreeType, SynchronizerManager>();
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Replay.MountPoint;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200027C RID: 636
	internal class AutoReseedContext
	{
		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x00065610 File Offset: 0x00063810
		// (set) Token: 0x060018B4 RID: 6324 RVA: 0x00065618 File Offset: 0x00063818
		public IADDatabase Database { get; private set; }

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x00065621 File Offset: 0x00063821
		// (set) Token: 0x060018B6 RID: 6326 RVA: 0x00065629 File Offset: 0x00063829
		public IEnumerable<IADDatabase> Databases { get; private set; }

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x00065632 File Offset: 0x00063832
		// (set) Token: 0x060018B8 RID: 6328 RVA: 0x0006563A File Offset: 0x0006383A
		public AmServerName TargetServerName { get; private set; }

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x00065643 File Offset: 0x00063843
		// (set) Token: 0x060018BA RID: 6330 RVA: 0x0006564B File Offset: 0x0006384B
		public IADServer TargetServer { get; private set; }

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x00065654 File Offset: 0x00063854
		// (set) Token: 0x060018BC RID: 6332 RVA: 0x0006565C File Offset: 0x0006385C
		public CopyStatusClientCachedEntry TargetCopyStatus { get; private set; }

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x00065665 File Offset: 0x00063865
		// (set) Token: 0x060018BE RID: 6334 RVA: 0x0006566D File Offset: 0x0006386D
		public IEnumerable<CopyStatusClientCachedEntry> CopyStatusesForTargetDatabase { get; private set; }

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x00065676 File Offset: 0x00063876
		// (set) Token: 0x060018C0 RID: 6336 RVA: 0x0006567E File Offset: 0x0006387E
		public IEnumerable<CopyStatusClientCachedEntry> CopyStatusesForTargetServer { get; private set; }

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x00065687 File Offset: 0x00063887
		// (set) Token: 0x060018C2 RID: 6338 RVA: 0x0006568F File Offset: 0x0006388F
		public Dictionary<AmServerName, IEnumerable<CopyStatusClientCachedEntry>> CopyStatusesForDag { get; private set; }

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x00065698 File Offset: 0x00063898
		// (set) Token: 0x060018C4 RID: 6340 RVA: 0x000656A0 File Offset: 0x000638A0
		public IADDatabaseAvailabilityGroup Dag { get; private set; }

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x000656A9 File Offset: 0x000638A9
		// (set) Token: 0x060018C6 RID: 6342 RVA: 0x000656B1 File Offset: 0x000638B1
		public IVolumeManager VolumeManager { get; private set; }

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x000656BA File Offset: 0x000638BA
		// (set) Token: 0x060018C8 RID: 6344 RVA: 0x000656C2 File Offset: 0x000638C2
		public IReplicaInstanceManager ReplicaInstanceManager { get; private set; }

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x000656CB File Offset: 0x000638CB
		// (set) Token: 0x060018CA RID: 6346 RVA: 0x000656D3 File Offset: 0x000638D3
		public AutoReseedServerLimiter ReseedLimiter { get; private set; }

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x000656DC File Offset: 0x000638DC
		// (set) Token: 0x060018CC RID: 6348 RVA: 0x000656E4 File Offset: 0x000638E4
		public IMonitoringADConfig AdConfig { get; private set; }

		// Token: 0x060018CD RID: 6349 RVA: 0x000656F0 File Offset: 0x000638F0
		public AutoReseedContext(IVolumeManager volumeManager, IReplicaInstanceManager replicaInstanceManager, IADDatabaseAvailabilityGroup dag, IADDatabase database, IEnumerable<IADDatabase> databases, AmServerName targetServerName, IADServer targetServer, CopyStatusClientCachedEntry targetCopyStatus, IEnumerable<CopyStatusClientCachedEntry> dbCopyStatuses, IEnumerable<CopyStatusClientCachedEntry> serverCopyStatuses, Dictionary<AmServerName, IEnumerable<CopyStatusClientCachedEntry>> dagCopyStatuses, AutoReseedServerLimiter reseedLimiter, IMonitoringADConfig adConfig)
		{
			this.VolumeManager = volumeManager;
			this.ReplicaInstanceManager = replicaInstanceManager;
			this.Dag = dag;
			this.Database = database;
			this.Databases = databases;
			this.TargetServerName = targetServerName;
			this.TargetServer = targetServer;
			this.TargetCopyStatus = targetCopyStatus;
			this.CopyStatusesForTargetDatabase = dbCopyStatuses;
			this.CopyStatusesForTargetServer = serverCopyStatuses;
			this.CopyStatusesForDag = dagCopyStatuses;
			this.ReseedLimiter = reseedLimiter;
			this.AdConfig = adConfig;
		}
	}
}

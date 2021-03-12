using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000312 RID: 786
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplicaInstancePreMountCallback : ReplicaInstanceAmQueuedItem
	{
		// Token: 0x06002065 RID: 8293 RVA: 0x00096AF0 File Offset: 0x00094CF0
		public ReplicaInstancePreMountCallback(int storeMountFlags, AmMountFlags amMountFlags, MountDirectPerformanceTracker mountPerf, ReplicaInstance instance) : base(instance)
		{
			this.StoreMountFlags = storeMountFlags;
			this.AmMountFlags = amMountFlags;
			this.MountPerfTracker = mountPerf;
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x00096B0F File Offset: 0x00094D0F
		// (set) Token: 0x06002067 RID: 8295 RVA: 0x00096B17 File Offset: 0x00094D17
		internal int StoreMountFlags
		{
			get
			{
				return this.m_storeMountFlags;
			}
			set
			{
				this.m_storeMountFlags = value;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x00096B20 File Offset: 0x00094D20
		// (set) Token: 0x06002069 RID: 8297 RVA: 0x00096B28 File Offset: 0x00094D28
		private AmMountFlags AmMountFlags { get; set; }

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x00096B31 File Offset: 0x00094D31
		// (set) Token: 0x0600206B RID: 8299 RVA: 0x00096B39 File Offset: 0x00094D39
		internal MountDirectPerformanceTracker MountPerfTracker { get; private set; }

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x00096B42 File Offset: 0x00094D42
		// (set) Token: 0x0600206D RID: 8301 RVA: 0x00096B4A File Offset: 0x00094D4A
		internal ReplayQueuedItemBase RestartOperation { get; private set; }

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x00096B53 File Offset: 0x00094D53
		// (set) Token: 0x0600206F RID: 8303 RVA: 0x00096B5B File Offset: 0x00094D5B
		internal LogStreamResetOnMount LogReset { get; private set; }

		// Token: 0x06002070 RID: 8304 RVA: 0x00096B64 File Offset: 0x00094D64
		protected override Exception GetReplicaInstanceNotFoundException()
		{
			return new AmPreMountCallbackFailedNoReplicaInstanceException(base.DbName, Environment.MachineName);
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x00096B76 File Offset: 0x00094D76
		protected override void CheckOperationApplicable()
		{
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x00096B78 File Offset: 0x00094D78
		protected override void ExecuteInternal()
		{
			base.ExecuteInternal();
			LogStreamResetOnMount logReset;
			ReplayQueuedItemBase restartOperation = base.ReplicaInstance.AmPreMountCallback(base.DbGuid, ref this.m_storeMountFlags, this.AmMountFlags, this.MountPerfTracker, out logReset);
			this.RestartOperation = restartOperation;
			this.LogReset = logReset;
		}

		// Token: 0x04000D3E RID: 3390
		private int m_storeMountFlags;
	}
}

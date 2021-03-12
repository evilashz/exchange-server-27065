using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000310 RID: 784
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplicaInstanceRestartOperation : ReplicaInstanceQueuedItem
	{
		// Token: 0x06002058 RID: 8280 RVA: 0x000969BE File Offset: 0x00094BBE
		public ReplicaInstanceRestartOperation(RestartInstanceWrapper instanceWrapper, ReplicaInstanceManager riManager) : base(instanceWrapper.OldReplicaInstance.ReplicaInstance)
		{
			base.IsDuplicateAllowed = false;
			this.m_instanceWrapper = instanceWrapper;
			this.m_replicaInstanceManager = riManager;
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x000969E6 File Offset: 0x00094BE6
		protected override void CheckOperationApplicable()
		{
			if (base.ReplicaInstance.CurrentContext.IsFailoverPending())
			{
				throw new ReplayServiceRestartInvalidDuringMoveException(base.DbCopyName);
			}
			if (base.ReplicaInstance.CurrentContext.Seeding)
			{
				throw new ReplayServiceRestartInvalidSeedingException(base.DbCopyName);
			}
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x00096A24 File Offset: 0x00094C24
		protected override void ExecuteInternal()
		{
			base.ExecuteInternal();
			this.m_replicaInstanceManager.RestartInstance(this.m_instanceWrapper);
		}

		// Token: 0x04000D39 RID: 3385
		private ReplicaInstanceManager m_replicaInstanceManager;

		// Token: 0x04000D3A RID: 3386
		private RestartInstanceWrapper m_instanceWrapper;
	}
}

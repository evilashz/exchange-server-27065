using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000315 RID: 789
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplicaInstanceRequestSuspend : ReplicaInstanceQueuedItem
	{
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x00096C58 File Offset: 0x00094E58
		public override string Name
		{
			get
			{
				return ReplayStrings.SuspendOperationName;
			}
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x00096C64 File Offset: 0x00094E64
		public ReplicaInstanceRequestSuspend(ReplicaInstance instance) : base(instance)
		{
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x00096C6D File Offset: 0x00094E6D
		// (set) Token: 0x06002084 RID: 8324 RVA: 0x00096C75 File Offset: 0x00094E75
		internal string SuspendComment { get; set; }

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002085 RID: 8325 RVA: 0x00096C7E File Offset: 0x00094E7E
		// (set) Token: 0x06002086 RID: 8326 RVA: 0x00096C86 File Offset: 0x00094E86
		internal DatabaseCopyActionFlags Flags { get; set; }

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x00096C8F File Offset: 0x00094E8F
		// (set) Token: 0x06002088 RID: 8328 RVA: 0x00096C97 File Offset: 0x00094E97
		internal ActionInitiatorType ActionInitiator { get; set; }

		// Token: 0x06002089 RID: 8329 RVA: 0x00096CA0 File Offset: 0x00094EA0
		protected override void CheckOperationApplicable()
		{
			if (base.ReplicaInstance.CurrentContext.IsFailoverPending())
			{
				throw new ReplayServiceSuspendInvalidDuringMoveException(base.DbCopyName);
			}
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x00096CC0 File Offset: 0x00094EC0
		protected override void ExecuteInternal()
		{
			base.ExecuteInternal();
			base.ReplicaInstance.RequestSuspend(this.SuspendComment, this.Flags, this.ActionInitiator);
		}
	}
}

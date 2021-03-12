using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000318 RID: 792
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplicaInstanceSyncSuspendResumeState : ReplicaInstanceQueuedItem
	{
		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x00096E10 File Offset: 0x00095010
		public override string Name
		{
			get
			{
				return ReplayStrings.SyncSuspendResumeOperationName;
			}
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00096E1C File Offset: 0x0009501C
		public ReplicaInstanceSyncSuspendResumeState(ReplicaInstance instance) : base(instance)
		{
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x00096E25 File Offset: 0x00095025
		protected override void CheckOperationApplicable()
		{
			if (base.ReplicaInstance.CurrentContext.IsFailoverPending())
			{
				throw new ReplayServiceSyncStateInvalidDuringMoveException(base.DbCopyName);
			}
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x00096E45 File Offset: 0x00095045
		protected override void ExecuteInternal()
		{
			base.ExecuteInternal();
			base.ReplicaInstance.SyncSuspendResumeState();
		}
	}
}

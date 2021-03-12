using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000314 RID: 788
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplicaInstanceEnableReplayLag : ReplicaInstanceQueuedItem
	{
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x00096C17 File Offset: 0x00094E17
		public override string Name
		{
			get
			{
				return ReplayStrings.EnableReplayLagOperationName;
			}
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x00096C23 File Offset: 0x00094E23
		public ReplicaInstanceEnableReplayLag(ReplicaInstance instance) : base(instance)
		{
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x00096C2C File Offset: 0x00094E2C
		// (set) Token: 0x0600207E RID: 8318 RVA: 0x00096C34 File Offset: 0x00094E34
		internal ActionInitiatorType ActionInitiator { get; set; }

		// Token: 0x0600207F RID: 8319 RVA: 0x00096C3D File Offset: 0x00094E3D
		protected override void CheckOperationApplicable()
		{
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x00096C3F File Offset: 0x00094E3F
		protected override void ExecuteInternal()
		{
			base.ExecuteInternal();
			base.ReplicaInstance.EnableReplayLag(this.ActionInitiator);
		}
	}
}

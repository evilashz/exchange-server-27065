using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000313 RID: 787
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplicaInstanceDisableReplayLag : ReplicaInstanceQueuedItem
	{
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002073 RID: 8307 RVA: 0x00096BBF File Offset: 0x00094DBF
		public override string Name
		{
			get
			{
				return ReplayStrings.DisableReplayLagOperationName;
			}
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x00096BCB File Offset: 0x00094DCB
		public ReplicaInstanceDisableReplayLag(ReplicaInstance instance) : base(instance)
		{
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x00096BD4 File Offset: 0x00094DD4
		// (set) Token: 0x06002076 RID: 8310 RVA: 0x00096BDC File Offset: 0x00094DDC
		internal string Reason { get; set; }

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x00096BE5 File Offset: 0x00094DE5
		// (set) Token: 0x06002078 RID: 8312 RVA: 0x00096BED File Offset: 0x00094DED
		internal ActionInitiatorType ActionInitiator { get; set; }

		// Token: 0x06002079 RID: 8313 RVA: 0x00096BF6 File Offset: 0x00094DF6
		protected override void CheckOperationApplicable()
		{
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x00096BF8 File Offset: 0x00094DF8
		protected override void ExecuteInternal()
		{
			base.ExecuteInternal();
			base.ReplicaInstance.DisableReplayLag(this.Reason, this.ActionInitiator);
		}
	}
}

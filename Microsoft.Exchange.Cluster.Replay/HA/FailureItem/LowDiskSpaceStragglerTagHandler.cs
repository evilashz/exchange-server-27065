using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001B8 RID: 440
	internal class LowDiskSpaceStragglerTagHandler : TagHandler
	{
		// Token: 0x06001125 RID: 4389 RVA: 0x0004644D File Offset: 0x0004464D
		internal LowDiskSpaceStragglerTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("LowDiskSpaceStragglerTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x0004645C File Offset: 0x0004465C
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtLowDiskSpaceStraggler9a);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x00046468 File Offset: 0x00044668
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtLowDiskSpaceStraggler9b);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x00046474 File Offset: 0x00044674
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00046477 File Offset: 0x00044677
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0004647C File Offset: 0x0004467C
		internal override void PassiveRecoveryActionInternal()
		{
			base.SuspendAndFailLocalCopy(false, false, false);
			Exception ex = AutoReseedWorkflowState.TriggerInPlaceReseed(base.Database.Guid, base.Database.Name);
			if (ex != null)
			{
				ExTraceGlobals.FailureItemTracer.TraceError<Guid, Exception>((long)this.GetHashCode(), "LowDiskSpaceStragglerTagHandler({0}) failed to write autoReseedWorkflowState: {1}", base.Database.Guid, ex);
			}
		}
	}
}

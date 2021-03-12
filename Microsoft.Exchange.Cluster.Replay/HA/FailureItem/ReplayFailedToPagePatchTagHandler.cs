using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001A9 RID: 425
	internal class ReplayFailedToPagePatchTagHandler : TagHandler
	{
		// Token: 0x060010B8 RID: 4280 RVA: 0x00045EE7 File Offset: 0x000440E7
		internal ReplayFailedToPagePatchTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("ReplayFailedToPagePatchTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x00045EF6 File Offset: 0x000440F6
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtFailedToRepair9a);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x00045F02 File Offset: 0x00044102
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtFailedToRepair9b);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x00045F0E File Offset: 0x0004410E
		internal override bool RaiseMANotificationItem
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x00045F11 File Offset: 0x00044111
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00045F14 File Offset: 0x00044114
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00045F16 File Offset: 0x00044116
		internal override void PassiveRecoveryActionInternal()
		{
			base.SuspendAndFailLocalCopy(false, false, false);
		}
	}
}

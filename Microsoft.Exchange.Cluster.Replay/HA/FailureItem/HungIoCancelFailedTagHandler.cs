using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001B0 RID: 432
	internal class HungIoCancelFailedTagHandler : TagHandler
	{
		// Token: 0x060010EF RID: 4335 RVA: 0x00046213 File Offset: 0x00044413
		internal HungIoCancelFailedTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("HungIoCancelFailedTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x00046222 File Offset: 0x00044422
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcHungIoCancelFailed9a);
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0004622E File Offset: 0x0004442E
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtHungIoCancelFailed9a);
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x0004623A File Offset: 0x0004443A
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0004623D File Offset: 0x0004443D
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0004623F File Offset: 0x0004443F
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

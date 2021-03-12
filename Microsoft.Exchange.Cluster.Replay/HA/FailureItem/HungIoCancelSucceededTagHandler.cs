using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001B1 RID: 433
	internal class HungIoCancelSucceededTagHandler : TagHandler
	{
		// Token: 0x060010F5 RID: 4341 RVA: 0x00046241 File Offset: 0x00044441
		internal HungIoCancelSucceededTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("HungIoCancelSucceededTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x00046250 File Offset: 0x00044450
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcHungIoCancelSucceeded9a);
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0004625C File Offset: 0x0004445C
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtHungIoCancelSucceeded9a);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x00046268 File Offset: 0x00044468
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0004626B File Offset: 0x0004446B
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0004626D File Offset: 0x0004446D
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

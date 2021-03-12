using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x02000194 RID: 404
	internal class CorruptionTagHandler : TagHandler
	{
		// Token: 0x06001027 RID: 4135 RVA: 0x000458AA File Offset: 0x00043AAA
		internal CorruptionTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("Corruption", watcher, dbfi)
		{
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x000458B9 File Offset: 0x00043AB9
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcCorruption9a);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x000458C5 File Offset: 0x00043AC5
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtCorruption9a);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x000458D1 File Offset: 0x00043AD1
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcCorruption9b);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x000458DD File Offset: 0x00043ADD
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtCorruption9b);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x000458E9 File Offset: 0x00043AE9
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x000458EC File Offset: 0x00043AEC
		internal override void ActiveRecoveryActionInternal()
		{
			base.MoveAfterSuspendAndFailLocalCopy(false, false, false);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x000458F7 File Offset: 0x00043AF7
		internal override void PassiveRecoveryActionInternal()
		{
			base.SuspendAndFailLocalCopy(false, false, false);
		}
	}
}

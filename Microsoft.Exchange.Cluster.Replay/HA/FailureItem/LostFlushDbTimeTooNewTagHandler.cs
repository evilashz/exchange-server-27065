using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001AB RID: 427
	internal class LostFlushDbTimeTooNewTagHandler : TagHandler
	{
		// Token: 0x060010C6 RID: 4294 RVA: 0x00045F64 File Offset: 0x00044164
		internal LostFlushDbTimeTooNewTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("LostFlushDbTimeTooNewTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00045F73 File Offset: 0x00044173
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcLostFlushDbTimeTooNew9a);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x00045F7F File Offset: 0x0004417F
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtLostFlushDbTimeTooNew9a);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00045F8B File Offset: 0x0004418B
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtLostFlushDbTimeTooNew9b);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x00045F97 File Offset: 0x00044197
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00045F9A File Offset: 0x0004419A
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00045F9C File Offset: 0x0004419C
		internal override void PassiveRecoveryActionInternal()
		{
			base.SuspendAndFailLocalCopy(false, true, false);
		}
	}
}

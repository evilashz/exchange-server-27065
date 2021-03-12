using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001AA RID: 426
	internal class LostFlushDbTimeTooOldTagHandler : TagHandler
	{
		// Token: 0x060010BF RID: 4287 RVA: 0x00045F21 File Offset: 0x00044121
		internal LostFlushDbTimeTooOldTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("LostFlushDbTimeTooOldTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x00045F30 File Offset: 0x00044130
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_LostFlushDbTimeTooOld9a);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00045F3C File Offset: 0x0004413C
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_LostFlushDbTimeTooOld9a);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x00045F48 File Offset: 0x00044148
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_LostFlushDbTimeTooOld9b);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00045F54 File Offset: 0x00044154
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00045F57 File Offset: 0x00044157
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00045F59 File Offset: 0x00044159
		internal override void PassiveRecoveryActionInternal()
		{
			base.SuspendAndFailLocalCopy(true, false, false);
		}
	}
}

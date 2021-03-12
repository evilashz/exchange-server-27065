using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001B7 RID: 439
	internal class LogGapFatalTagHandler : TagHandler
	{
		// Token: 0x0600111F RID: 4383 RVA: 0x00046416 File Offset: 0x00044616
		internal LogGapFatalTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("LogGapFatalTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x00046425 File Offset: 0x00044625
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_LogGapFatal9a);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x00046431 File Offset: 0x00044631
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_LogGapFatal9b);
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0004643D File Offset: 0x0004463D
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00046440 File Offset: 0x00044640
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00046442 File Offset: 0x00044642
		internal override void PassiveRecoveryActionInternal()
		{
			base.SuspendAndFailLocalCopy(false, false, false);
		}
	}
}

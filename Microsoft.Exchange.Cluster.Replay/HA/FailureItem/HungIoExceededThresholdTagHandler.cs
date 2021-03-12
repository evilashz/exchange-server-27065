using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001AE RID: 430
	internal class HungIoExceededThresholdTagHandler : TagHandler
	{
		// Token: 0x060010DD RID: 4317 RVA: 0x00046053 File Offset: 0x00044253
		internal HungIoExceededThresholdTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("HungIoExceededThresholdTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x00046062 File Offset: 0x00044262
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcHungIoExceededThreshold9a);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x0004606E File Offset: 0x0004426E
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcHungIoExceededThreshold9b);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x0004607A File Offset: 0x0004427A
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtHungIoExceededThreshold9a);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x00046086 File Offset: 0x00044286
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x000460CB File Offset: 0x000442CB
		internal override void ActiveRecoveryActionInternal()
		{
			base.PostProcessingAction = delegate()
			{
				BugcheckHelper.TriggerBugcheckIfRequired(base.FailureItem.CreationTime.ToUniversalTime(), string.Format("HungIoExceededThreshold FailureItem at {0}", base.FailureItem.CreationTime));
			};
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0004611F File Offset: 0x0004431F
		internal override void PassiveRecoveryActionInternal()
		{
			base.PostProcessingAction = delegate()
			{
				BugcheckHelper.TriggerBugcheckIfRequired(base.FailureItem.CreationTime.ToUniversalTime(), string.Format("HungIoExceededThreshold FailureItem at {0}", base.FailureItem.CreationTime));
			};
		}
	}
}

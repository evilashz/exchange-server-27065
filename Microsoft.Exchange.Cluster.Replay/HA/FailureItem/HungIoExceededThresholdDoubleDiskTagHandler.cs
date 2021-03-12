using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001AF RID: 431
	internal class HungIoExceededThresholdDoubleDiskTagHandler : TagHandler
	{
		// Token: 0x060010E6 RID: 4326 RVA: 0x00046133 File Offset: 0x00044333
		internal HungIoExceededThresholdDoubleDiskTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("HungIoExceededThresholdDoubleDiskTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00046142 File Offset: 0x00044342
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcHungIoExceededThresholdDoubleDisk9a);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0004614E File Offset: 0x0004434E
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcHungIoExceededThresholdDoubleDisk9b);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0004615A File Offset: 0x0004435A
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtHungIoExceededThresholdDoubleDisk9a);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x00046166 File Offset: 0x00044366
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x000461AB File Offset: 0x000443AB
		internal override void ActiveRecoveryActionInternal()
		{
			base.PostProcessingAction = delegate()
			{
				BugcheckHelper.TriggerBugcheckIfRequired(base.FailureItem.CreationTime.ToUniversalTime(), string.Format("HungIoExceededThresholdDoubleDiskTagHandler FailureItem at {0}", base.FailureItem.CreationTime));
			};
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x000461FF File Offset: 0x000443FF
		internal override void PassiveRecoveryActionInternal()
		{
			base.PostProcessingAction = delegate()
			{
				BugcheckHelper.TriggerBugcheckIfRequired(base.FailureItem.CreationTime.ToUniversalTime(), string.Format("HungIoExceededThresholdDoubleDiskTagHandler FailureItem at {0}", base.FailureItem.CreationTime));
			};
		}
	}
}

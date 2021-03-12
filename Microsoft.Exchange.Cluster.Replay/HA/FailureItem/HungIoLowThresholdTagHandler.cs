using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001AC RID: 428
	internal class HungIoLowThresholdTagHandler : TagHandler
	{
		// Token: 0x060010CD RID: 4301 RVA: 0x00045FA7 File Offset: 0x000441A7
		internal HungIoLowThresholdTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("HungIoLowThresholdTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x00045FB6 File Offset: 0x000441B6
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcHungIoLowThreshold9a);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x00045FC2 File Offset: 0x000441C2
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcHungIoLowThreshold9b);
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x00045FCE File Offset: 0x000441CE
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtHungIoLowThreshold9a);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00045FDA File Offset: 0x000441DA
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtHungIoLowThreshold9b);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x00045FE6 File Offset: 0x000441E6
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00045FE9 File Offset: 0x000441E9
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.Move(base.Database, Environment.MachineName);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00045FFB File Offset: 0x000441FB
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

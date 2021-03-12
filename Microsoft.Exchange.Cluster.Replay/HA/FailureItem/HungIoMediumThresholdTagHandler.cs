using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001AD RID: 429
	internal class HungIoMediumThresholdTagHandler : TagHandler
	{
		// Token: 0x060010D5 RID: 4309 RVA: 0x00045FFD File Offset: 0x000441FD
		internal HungIoMediumThresholdTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("HungIoMediumThresholdTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x0004600C File Offset: 0x0004420C
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcHungIoMediumThreshold9a);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00046018 File Offset: 0x00044218
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcHungIoMediumThreshold9b);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x00046024 File Offset: 0x00044224
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtHungIoMediumThreshold9a);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x00046030 File Offset: 0x00044230
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtHungIoMediumThreshold9b);
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x0004603C File Offset: 0x0004423C
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0004603F File Offset: 0x0004423F
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.Move(base.Database, Environment.MachineName);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00046051 File Offset: 0x00044251
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

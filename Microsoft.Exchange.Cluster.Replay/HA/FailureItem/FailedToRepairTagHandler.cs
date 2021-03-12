using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001A8 RID: 424
	internal class FailedToRepairTagHandler : TagHandler
	{
		// Token: 0x060010B2 RID: 4274 RVA: 0x00045EA9 File Offset: 0x000440A9
		internal FailedToRepairTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("FailedToRepairTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00045EB8 File Offset: 0x000440B8
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcFailedToRepair9a);
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x00045EC4 File Offset: 0x000440C4
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcFailedToRepair9b);
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x00045ED0 File Offset: 0x000440D0
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00045ED3 File Offset: 0x000440D3
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.Move(base.Database, Environment.MachineName);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00045EE5 File Offset: 0x000440E5
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

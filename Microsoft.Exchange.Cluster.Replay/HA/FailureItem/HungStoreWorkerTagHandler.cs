using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001B4 RID: 436
	internal class HungStoreWorkerTagHandler : TagHandler
	{
		// Token: 0x06001109 RID: 4361 RVA: 0x00046323 File Offset: 0x00044523
		internal HungStoreWorkerTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("HungStoreWorkerTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x00046332 File Offset: 0x00044532
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcHungStoreWorker9a);
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x0004633E File Offset: 0x0004453E
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcHungStoreWorker9b);
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x0004634A File Offset: 0x0004454A
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtHungStoreWorker9a);
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00046356 File Offset: 0x00044556
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00046359 File Offset: 0x00044559
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.Move(base.Database, Environment.MachineName);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0004636B File Offset: 0x0004456B
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001B5 RID: 437
	internal class UnaccessibleStoreWorkerTagHandler : TagHandler
	{
		// Token: 0x06001110 RID: 4368 RVA: 0x0004636D File Offset: 0x0004456D
		internal UnaccessibleStoreWorkerTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("UnaccessibleStoreWorkerTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x0004637C File Offset: 0x0004457C
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcUnaccessibleStoreWorker9a);
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00046388 File Offset: 0x00044588
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcUnaccessibleStoreWorker9b);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00046394 File Offset: 0x00044594
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtUnaccessibleStoreWorker9a);
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x000463A0 File Offset: 0x000445A0
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000463A3 File Offset: 0x000445A3
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.Move(base.Database, Environment.MachineName);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x000463B5 File Offset: 0x000445B5
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

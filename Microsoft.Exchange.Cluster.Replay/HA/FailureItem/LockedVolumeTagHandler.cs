using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001B9 RID: 441
	internal class LockedVolumeTagHandler : TagHandler
	{
		// Token: 0x0600112B RID: 4395 RVA: 0x000464D3 File Offset: 0x000446D3
		internal LockedVolumeTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("LockedVolumeTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x000464E2 File Offset: 0x000446E2
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcLockedVolume9a);
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x000464EE File Offset: 0x000446EE
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcLockedVolume9b);
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x000464FA File Offset: 0x000446FA
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtLockedVolume9a);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x00046506 File Offset: 0x00044706
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00046509 File Offset: 0x00044709
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.Move(base.Database, Environment.MachineName);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0004651B File Offset: 0x0004471B
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

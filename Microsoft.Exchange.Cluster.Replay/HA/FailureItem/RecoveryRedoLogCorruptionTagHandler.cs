using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001B3 RID: 435
	internal class RecoveryRedoLogCorruptionTagHandler : TagHandler
	{
		// Token: 0x06001101 RID: 4353 RVA: 0x000462AD File Offset: 0x000444AD
		internal RecoveryRedoLogCorruptionTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("RecoveryRedoLogCorruptionTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x000462BC File Offset: 0x000444BC
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcLogCorruptionDetectedByESE9a);
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x000462C8 File Offset: 0x000444C8
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcLogCorruptionDetectedByESE9b);
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x000462D4 File Offset: 0x000444D4
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtLogCorruptionDetectedByESE9a);
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x000462E0 File Offset: 0x000444E0
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtLogCorruptionDetectedByESE9b);
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x000462EC File Offset: 0x000444EC
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x000462EF File Offset: 0x000444EF
		internal override void ActiveRecoveryActionInternal()
		{
			LogRepair.EnableLogRepair(base.Database.Guid);
			DatabaseTasks.Move(base.Database, Environment.MachineName);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00046311 File Offset: 0x00044511
		internal override void PassiveRecoveryActionInternal()
		{
			LogRepair.EnableLogRepair(base.Database.Guid);
		}
	}
}

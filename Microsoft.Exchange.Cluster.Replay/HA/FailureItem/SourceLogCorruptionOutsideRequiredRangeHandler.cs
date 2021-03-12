using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001B2 RID: 434
	internal class SourceLogCorruptionOutsideRequiredRangeHandler : TagHandler
	{
		// Token: 0x060010FB RID: 4347 RVA: 0x0004626F File Offset: 0x0004446F
		internal SourceLogCorruptionOutsideRequiredRangeHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("SourceLogCorruptionOutsideRequiredRangeHandler", watcher, dbfi)
		{
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0004627E File Offset: 0x0004447E
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcGeneric9a);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x0004628A File Offset: 0x0004448A
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtGeneric9a);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00046296 File Offset: 0x00044496
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00046299 File Offset: 0x00044499
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.Move(base.Database, Environment.MachineName);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x000462AB File Offset: 0x000444AB
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

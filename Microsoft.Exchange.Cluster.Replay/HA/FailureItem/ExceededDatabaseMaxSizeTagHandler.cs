using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001BA RID: 442
	internal class ExceededDatabaseMaxSizeTagHandler : TagHandler
	{
		// Token: 0x06001132 RID: 4402 RVA: 0x0004651D File Offset: 0x0004471D
		internal ExceededDatabaseMaxSizeTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("ExceededDatabaseMaxSizeTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0004652C File Offset: 0x0004472C
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcExceededDatabaseMaxSize9a);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x00046538 File Offset: 0x00044738
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtExceededDatabaseMaxSize9a);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x00046544 File Offset: 0x00044744
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcExceededDatabaseMaxSize9b);
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x00046550 File Offset: 0x00044750
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00046554 File Offset: 0x00044754
		internal override void ActiveRecoveryActionInternal()
		{
			Exception ex = null;
			try
			{
				base.SuspendAndFailLocalCopyNoThrow(false, false, false);
			}
			finally
			{
				ex = DatabaseTasks.TryToDismountClean(base.Database);
			}
			if (ex != null)
			{
				ReplayCrimsonEvents.DbMaxSizeDismountCleanFailure.Log<IADDatabase, string>(base.Database, ex.Message);
				throw new FailureItemRecoveryException(base.Database.Name, ex.ToString(), ex);
			}
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x000465C0 File Offset: 0x000447C0
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

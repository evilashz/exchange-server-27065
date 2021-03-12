using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001B6 RID: 438
	internal class MonitoredDatabaseFailedTagHandler : TagHandler
	{
		// Token: 0x06001117 RID: 4375 RVA: 0x000463B7 File Offset: 0x000445B7
		internal MonitoredDatabaseFailedTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("MonitoredDatabaseFailedTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x000463C6 File Offset: 0x000445C6
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcMonitoredDatabaseFailed9a);
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x000463D2 File Offset: 0x000445D2
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcMonitoredDatabaseFailed9b);
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x000463DE File Offset: 0x000445DE
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtMonitoredDatabaseFailed9a);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x000463EA File Offset: 0x000445EA
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtMonitoredDatabaseFailed9b);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x000463F6 File Offset: 0x000445F6
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x000463F9 File Offset: 0x000445F9
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.Move(base.Database, Environment.MachineName);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0004640B File Offset: 0x0004460B
		internal override void PassiveRecoveryActionInternal()
		{
			base.SuspendAndFailLocalCopy(false, false, false);
		}
	}
}

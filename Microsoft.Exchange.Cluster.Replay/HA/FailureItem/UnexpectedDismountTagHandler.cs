using System;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x0200019F RID: 415
	internal class UnexpectedDismountTagHandler : TagHandler
	{
		// Token: 0x06001075 RID: 4213 RVA: 0x00045C0C File Offset: 0x00043E0C
		internal UnexpectedDismountTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("UnexpectedDismountTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00045C1B File Offset: 0x00043E1B
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				if (this.m_moveWasSkipped)
				{
					return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_UnexpectedDismountMoveWasSkipped9a);
				}
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_UnexpectedDismount9a);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x00045C3A File Offset: 0x00043E3A
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_UnexpectedDismount9a);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x00045C46 File Offset: 0x00043E46
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_UnexpectedDismount9b);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x00045C52 File Offset: 0x00043E52
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return !AmStoreHelper.IsMountedLocally(base.Database.Guid);
			}
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00045C69 File Offset: 0x00043E69
		internal override void ActiveRecoveryActionInternal()
		{
			if (!AmStoreHelper.IsMountedLocally(base.Database.Guid))
			{
				DatabaseTasks.Move(base.Database, Environment.MachineName);
				return;
			}
			this.m_moveWasSkipped = true;
			base.Trace("Skipping failover since the database is already mounted", new object[0]);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00045CA6 File Offset: 0x00043EA6
		internal override void PassiveRecoveryActionInternal()
		{
		}

		// Token: 0x04000685 RID: 1669
		private bool m_moveWasSkipped;
	}
}

using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x02000196 RID: 406
	internal class UnrecoverableTagHandler : TagHandler
	{
		// Token: 0x06001037 RID: 4151 RVA: 0x00045961 File Offset: 0x00043B61
		internal UnrecoverableTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("Unrecoverable", watcher, dbfi)
		{
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x00045970 File Offset: 0x00043B70
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcUnrecoverable9a);
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x0004597C File Offset: 0x00043B7C
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtUnrecoverable9a);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x00045988 File Offset: 0x00043B88
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcUnrecoverable9b);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x00045994 File Offset: 0x00043B94
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtUnrecoverable9b);
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x000459A0 File Offset: 0x00043BA0
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000459A4 File Offset: 0x00043BA4
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
				ReplayCrimsonEvents.UnrecoverableDismountCleanFailure.Log<IADDatabase, string>(base.Database, ex.Message);
				throw new FailureItemRecoveryException(base.Database.Name, ex.ToString(), ex);
			}
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00045A10 File Offset: 0x00043C10
		internal override void PassiveRecoveryActionInternal()
		{
			base.SuspendAndFailLocalCopy(false, false, false);
		}
	}
}

using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x02000191 RID: 401
	internal class IoHardTagHandler : TagHandler
	{
		// Token: 0x0600100D RID: 4109 RVA: 0x0004579E File Offset: 0x0004399E
		internal IoHardTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("I/O Hard", watcher, dbfi)
		{
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x000457AD File Offset: 0x000439AD
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcIoHard9a);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x000457B9 File Offset: 0x000439B9
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtIoHard9a);
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x000457C5 File Offset: 0x000439C5
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcIoHard9b);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x000457D1 File Offset: 0x000439D1
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtIoHard9b);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x000457DD File Offset: 0x000439DD
		internal override bool RaiseMANotificationItem
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x000457E0 File Offset: 0x000439E0
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x000457E3 File Offset: 0x000439E3
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.Move(base.Database, Environment.MachineName);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x000457F5 File Offset: 0x000439F5
		internal override void PassiveRecoveryActionInternal()
		{
			base.SuspendAndFailLocalCopy(false, false, false);
		}
	}
}

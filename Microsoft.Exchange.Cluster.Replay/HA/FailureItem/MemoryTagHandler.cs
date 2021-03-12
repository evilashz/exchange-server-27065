using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x0200019B RID: 411
	internal class MemoryTagHandler : TagHandler
	{
		// Token: 0x0600105C RID: 4188 RVA: 0x00045B28 File Offset: 0x00043D28
		internal MemoryTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("Memory", watcher, dbfi)
		{
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x00045B37 File Offset: 0x00043D37
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcMemory9a);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x00045B43 File Offset: 0x00043D43
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtMemory9a);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x00045B4F File Offset: 0x00043D4F
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcMemory9b);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x00045B5B File Offset: 0x00043D5B
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00045B5E File Offset: 0x00043D5E
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.Move(base.Database, Environment.MachineName);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00045B70 File Offset: 0x00043D70
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

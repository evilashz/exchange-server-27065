using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x02000197 RID: 407
	internal class RemountTagHandler : TagHandler
	{
		// Token: 0x0600103F RID: 4159 RVA: 0x00045A1B File Offset: 0x00043C1B
		internal RemountTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("Remount", watcher, dbfi)
		{
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x00045A2A File Offset: 0x00043C2A
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcRemount9a);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x00045A36 File Offset: 0x00043C36
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtRemount9a);
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x00045A42 File Offset: 0x00043C42
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcRemount9b);
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x00045A4E File Offset: 0x00043C4E
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00045A51 File Offset: 0x00043C51
		internal override void ActiveRecoveryActionInternal()
		{
			DatabaseTasks.Remount(base.Database, Environment.MachineName);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00045A63 File Offset: 0x00043C63
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

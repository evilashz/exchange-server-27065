using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x0200018F RID: 399
	internal class RepairableTagHandler : TagHandler
	{
		// Token: 0x06000FFF RID: 4095 RVA: 0x0004572A File Offset: 0x0004392A
		internal RepairableTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("Repairable", watcher, dbfi)
		{
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x00045739 File Offset: 0x00043939
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcRepairable9a);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x00045745 File Offset: 0x00043945
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtRepairable9a);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x00045751 File Offset: 0x00043951
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcRepairable9b);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x0004575D File Offset: 0x0004395D
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00045760 File Offset: 0x00043960
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00045762 File Offset: 0x00043962
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

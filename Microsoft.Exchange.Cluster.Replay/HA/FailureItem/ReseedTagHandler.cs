using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x02000198 RID: 408
	internal class ReseedTagHandler : TagHandler
	{
		// Token: 0x06001046 RID: 4166 RVA: 0x00045A65 File Offset: 0x00043C65
		internal ReseedTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("Reseed", watcher, dbfi)
		{
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x00045A74 File Offset: 0x00043C74
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcReseed9a);
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x00045A80 File Offset: 0x00043C80
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtReseed9a);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x00045A8C File Offset: 0x00043C8C
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcReseed9a);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x00045A98 File Offset: 0x00043C98
		internal override ExEventLog.EventTuple? Event9bTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtReseed9b);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x00045AA4 File Offset: 0x00043CA4
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00045AA7 File Offset: 0x00043CA7
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00045AA9 File Offset: 0x00043CA9
		internal override void PassiveRecoveryActionInternal()
		{
			base.SuspendAndFailLocalCopy(false, false, false);
		}
	}
}

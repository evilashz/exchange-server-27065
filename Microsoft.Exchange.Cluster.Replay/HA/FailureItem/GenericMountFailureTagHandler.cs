using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001A2 RID: 418
	internal class GenericMountFailureTagHandler : TagHandler
	{
		// Token: 0x0600108B RID: 4235 RVA: 0x00045D51 File Offset: 0x00043F51
		internal GenericMountFailureTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("GenericMountFailureTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x00045D60 File Offset: 0x00043F60
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcGenericMountFailure9a);
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x00045D6C File Offset: 0x00043F6C
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_TgtGenericMountFailure9a);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x00045D78 File Offset: 0x00043F78
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_SrcGenericMountFailure9b);
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00045D84 File Offset: 0x00043F84
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00045D87 File Offset: 0x00043F87
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00045D89 File Offset: 0x00043F89
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

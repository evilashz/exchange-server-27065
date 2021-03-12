using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001A4 RID: 420
	internal class PagePatchCompletedTagHandler : TagHandler
	{
		// Token: 0x06001099 RID: 4249 RVA: 0x00045DC5 File Offset: 0x00043FC5
		internal PagePatchCompletedTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("PagePatchCompletedTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x00045DD4 File Offset: 0x00043FD4
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_PagePatchCompleted9a);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x00045DE0 File Offset: 0x00043FE0
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_PagePatchCompleted9a);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x00045DEC File Offset: 0x00043FEC
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00045DEF File Offset: 0x00043FEF
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00045DF1 File Offset: 0x00043FF1
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

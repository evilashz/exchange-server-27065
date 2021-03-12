using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x020001A3 RID: 419
	internal class PagePatchRequestedTagHandler : TagHandler
	{
		// Token: 0x06001092 RID: 4242 RVA: 0x00045D8B File Offset: 0x00043F8B
		internal PagePatchRequestedTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("PagePatchRequestedTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00045D9A File Offset: 0x00043F9A
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_PagePatchRequested9a);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00045DA6 File Offset: 0x00043FA6
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_PagePatchRequested9a);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x00045DB2 File Offset: 0x00043FB2
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_PagePatchRequested9b);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x00045DBE File Offset: 0x00043FBE
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00045DC1 File Offset: 0x00043FC1
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00045DC3 File Offset: 0x00043FC3
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

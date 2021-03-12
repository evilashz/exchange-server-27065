using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x02000190 RID: 400
	internal class SpaceTagHandler : TagHandler
	{
		// Token: 0x06001006 RID: 4102 RVA: 0x00045764 File Offset: 0x00043964
		internal SpaceTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("Space", watcher, dbfi)
		{
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x00045773 File Offset: 0x00043973
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_Space9a);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x0004577F File Offset: 0x0004397F
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_Space9a);
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x0004578B File Offset: 0x0004398B
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_Space9b);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x00045797 File Offset: 0x00043997
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0004579A File Offset: 0x0004399A
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0004579C File Offset: 0x0004399C
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

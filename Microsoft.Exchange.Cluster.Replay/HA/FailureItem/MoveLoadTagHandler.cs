using System;
using Microsoft.Exchange.Cluster.FailureItemEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x0200019A RID: 410
	internal class MoveLoadTagHandler : TagHandler
	{
		// Token: 0x06001055 RID: 4181 RVA: 0x00045AEE File Offset: 0x00043CEE
		internal MoveLoadTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("Move load", watcher, dbfi)
		{
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x00045AFD File Offset: 0x00043CFD
		internal override ExEventLog.EventTuple? Event9aSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_MoveLoad9a);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x00045B09 File Offset: 0x00043D09
		internal override ExEventLog.EventTuple? Event9aTgt
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_MoveLoad9a);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x00045B15 File Offset: 0x00043D15
		internal override ExEventLog.EventTuple? Event9bSrc
		{
			get
			{
				return new ExEventLog.EventTuple?(FailureItemEventLogConstants.Tuple_MoveLoad9b);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x00045B21 File Offset: 0x00043D21
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00045B24 File Offset: 0x00043D24
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00045B26 File Offset: 0x00043D26
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

using System;
using Microsoft.Exchange.Common.HA;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x0200018D RID: 397
	internal class NoOpTagHandler : TagHandler
	{
		// Token: 0x06000FF3 RID: 4083 RVA: 0x000456BC File Offset: 0x000438BC
		internal NoOpTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("NoOp", watcher, dbfi)
		{
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x000456CB File Offset: 0x000438CB
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000456CE File Offset: 0x000438CE
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x000456D0 File Offset: 0x000438D0
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

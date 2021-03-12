using System;
using Microsoft.Exchange.Common.HA;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x0200019E RID: 414
	internal class ExpectedDismountTagHandler : TagHandler
	{
		// Token: 0x06001071 RID: 4209 RVA: 0x00045BF6 File Offset: 0x00043DF6
		internal ExpectedDismountTagHandler(FailureItemWatcher watcher, DatabaseFailureItem dbfi) : base("ExpectedDismountTagHandler", watcher, dbfi)
		{
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00045C05 File Offset: 0x00043E05
		internal override bool IsTPRMoveTheActiveRecoveryAction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00045C08 File Offset: 0x00043E08
		internal override void ActiveRecoveryActionInternal()
		{
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00045C0A File Offset: 0x00043E0A
		internal override void PassiveRecoveryActionInternal()
		{
		}
	}
}

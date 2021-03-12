using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Worker.Agents
{
	// Token: 0x0200001E RID: 30
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SubscriptionWorkItemEventResult : SubscriptionEventResult
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00008ED8 File Offset: 0x000070D8
		public bool DeleteSubscription
		{
			get
			{
				return this.deleteSubscription;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008EE0 File Offset: 0x000070E0
		public void SetDeleteSubscription()
		{
			this.deleteSubscription = true;
		}

		// Token: 0x0400010D RID: 269
		private bool deleteSubscription;
	}
}

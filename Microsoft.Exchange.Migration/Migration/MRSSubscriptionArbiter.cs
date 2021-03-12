using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200015A RID: 346
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MRSSubscriptionArbiter : SubscriptionArbiterBase
	{
		// Token: 0x06001112 RID: 4370 RVA: 0x00047F1B File Offset: 0x0004611B
		private MRSSubscriptionArbiter()
		{
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00047F23 File Offset: 0x00046123
		public static MRSSubscriptionArbiter Instance
		{
			get
			{
				return MRSSubscriptionArbiter.soleInstance;
			}
		}

		// Token: 0x040005F4 RID: 1524
		private static readonly MRSSubscriptionArbiter soleInstance = new MRSSubscriptionArbiter();
	}
}

using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000324 RID: 804
	internal interface ILockableQueue
	{
		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x060022BF RID: 8895
		NextHopSolutionKey Key { get; }

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060022C0 RID: 8896
		int LockedCount { get; }

		// Token: 0x060022C1 RID: 8897
		void Lock(ILockableItem item, WaitCondition condition, string lockReason, int dehydrateThreshold);

		// Token: 0x060022C2 RID: 8898
		bool ActivateOne(WaitCondition condition, DeliveryPriority suggestedPriority, AccessToken token);

		// Token: 0x060022C3 RID: 8899
		ILockableItem DequeueInternal();

		// Token: 0x060022C4 RID: 8900
		ILockableItem DequeueInternal(DeliveryPriority priority);
	}
}

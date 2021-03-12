using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000C4 RID: 196
	[Flags]
	internal enum SubscriptionEvents
	{
		// Token: 0x0400031C RID: 796
		None = 0,
		// Token: 0x0400031D RID: 797
		WorkItemCompleted = 1,
		// Token: 0x0400031E RID: 798
		WorkItemFailedLoadSubscription = 2,
		// Token: 0x0400031F RID: 799
		All = 255
	}
}

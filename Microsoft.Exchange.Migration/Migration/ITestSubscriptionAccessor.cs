using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200017E RID: 382
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ITestSubscriptionAccessor
	{
		// Token: 0x060011D2 RID: 4562
		string GetDebuggingContext();

		// Token: 0x060011D3 RID: 4563
		TestSubscriptionSnapshot CreateSubscription(TestSubscriptionAspect aspect);

		// Token: 0x060011D4 RID: 4564
		TestSubscriptionSnapshot TestCreateSubscription(TestSubscriptionAspect aspect);

		// Token: 0x060011D5 RID: 4565
		SnapshotStatus RetrieveSubscriptionStatus(Guid identity);

		// Token: 0x060011D6 RID: 4566
		TestSubscriptionSnapshot RetrieveSubscriptionSnapshot(Guid identity);

		// Token: 0x060011D7 RID: 4567
		void UpdateSubscriptionStatus(Guid identity, SnapshotStatus status);

		// Token: 0x060011D8 RID: 4568
		void UpdateSubscription(Guid identity, TestSubscriptionAspect aspect);
	}
}

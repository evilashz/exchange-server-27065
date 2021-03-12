using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.DataAccessLayer;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200013A RID: 314
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISubscriptionAccessor
	{
		// Token: 0x06000FCF RID: 4047
		SubscriptionSnapshot CreateSubscription(MigrationJobItem jobItem);

		// Token: 0x06000FD0 RID: 4048
		SubscriptionSnapshot TestCreateSubscription(MigrationJobItem jobItem);

		// Token: 0x06000FD1 RID: 4049
		SnapshotStatus RetrieveSubscriptionStatus(ISubscriptionId subscriptionId);

		// Token: 0x06000FD2 RID: 4050
		SubscriptionSnapshot RetrieveSubscriptionSnapshot(ISubscriptionId subscriptionId);

		// Token: 0x06000FD3 RID: 4051
		bool UpdateSubscription(ISubscriptionId subscriptionId, MigrationEndpointBase endpoint, MigrationJobItem jobItem, bool adoptingSubscription);

		// Token: 0x06000FD4 RID: 4052
		bool ResumeSubscription(ISubscriptionId subscriptionId, bool finalize = false);

		// Token: 0x06000FD5 RID: 4053
		bool SuspendSubscription(ISubscriptionId subscriptionId);

		// Token: 0x06000FD6 RID: 4054
		bool RemoveSubscription(ISubscriptionId subscriptionId);
	}
}

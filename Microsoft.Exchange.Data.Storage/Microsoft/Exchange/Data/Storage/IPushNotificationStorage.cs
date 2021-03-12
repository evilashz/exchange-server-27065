using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002DD RID: 733
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPushNotificationStorage : IDisposable
	{
		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06001F5B RID: 8027
		string TenantId { get; }

		// Token: 0x06001F5C RID: 8028
		List<PushNotificationServerSubscription> GetActiveNotificationSubscriptions(IMailboxSession mailboxSession, uint expirationInHours);

		// Token: 0x06001F5D RID: 8029
		List<StoreObjectId> GetExpiredNotificationSubscriptions(uint expirationInHours);

		// Token: 0x06001F5E RID: 8030
		List<PushNotificationServerSubscription> GetNotificationSubscriptions(IMailboxSession mailboxSession);

		// Token: 0x06001F5F RID: 8031
		IPushNotificationSubscriptionItem CreateOrUpdateSubscriptionItem(IMailboxSession mailboxSession, string subscriptionId, PushNotificationServerSubscription subscription);

		// Token: 0x06001F60 RID: 8032
		void DeleteAllSubscriptions();

		// Token: 0x06001F61 RID: 8033
		void DeleteExpiredSubscriptions(uint expirationInHours);

		// Token: 0x06001F62 RID: 8034
		void DeleteSubscription(StoreObjectId itemId);

		// Token: 0x06001F63 RID: 8035
		void DeleteSubscription(string subscriptionId);
	}
}

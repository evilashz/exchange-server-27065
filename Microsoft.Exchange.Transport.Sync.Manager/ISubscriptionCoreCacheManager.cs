using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISubscriptionCoreCacheManager
	{
		// Token: 0x06000016 RID: 22
		void DeleteCacheMessage(MailboxSession systemMailboxSession, StoreObjectId cacheMessageId);

		// Token: 0x06000017 RID: 23
		void SaveCacheMessage(SubscriptionCacheMessage cacheMessage);

		// Token: 0x06000018 RID: 24
		SubscriptionCacheMessage BindCacheMessage(MailboxSession systemMailboxSession, StoreObjectId cacheFolderId, Guid mailboxGuid, bool loadSubscriptions);

		// Token: 0x06000019 RID: 25
		SubscriptionCacheMessage BindCacheMessage(MailboxSession systemMailboxSession, StoreObjectId cacheMessageId, bool loadSubscriptions);

		// Token: 0x0600001A RID: 26
		SubscriptionCacheMessage CreateCacheMessage(MailboxSession systemMailboxSession, StoreObjectId cacheFolderId, Guid mailboxGuid, ExDateTime subscriptionListTimestamp);
	}
}

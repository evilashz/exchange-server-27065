using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000B8 RID: 184
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISubscriptionManager
	{
		// Token: 0x06000521 RID: 1313
		void UpdateSubscriptionToMailbox(MailboxSession mailboxSession, ISyncWorkerData subscription);

		// Token: 0x06000522 RID: 1314
		void DeleteSubscription(MailboxSession mailboxSession, ISyncWorkerData subscription, bool sendRpcNotification);
	}
}

using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect
{
	// Token: 0x020000D9 RID: 217
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConnectSubscriptionCleanup
	{
		// Token: 0x06000684 RID: 1668
		void Cleanup(MailboxSession mailbox, IConnectSubscription subscription, bool sendRPCNotification = true);
	}
}

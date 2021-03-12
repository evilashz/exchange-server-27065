using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISetConnectSubscription
	{
		// Token: 0x0600013F RID: 319
		void StampChangesOn(ConnectSubscriptionProxy subscription);

		// Token: 0x06000140 RID: 320
		void NotifyApps(MailboxSession mailbox);
	}
}

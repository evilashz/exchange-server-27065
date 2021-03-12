using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000025 RID: 37
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface INewConnectSubscription
	{
		// Token: 0x0600013B RID: 315
		IConfigurable PrepareSubscription(MailboxSession mailbox, ConnectSubscriptionProxy subscription);

		// Token: 0x0600013C RID: 316
		void InitializeFolderAndNotifyApps(MailboxSession mailbox, ConnectSubscriptionProxy subscription);

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600013D RID: 317
		string SubscriptionName { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600013E RID: 318
		string SubscriptionDisplayName { get; }
	}
}

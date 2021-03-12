using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationService
	{
		// Token: 0x0600000C RID: 12
		CreateSyncSubscriptionResult CreateSyncSubscription(AbstractCreateSyncSubscriptionArgs args);

		// Token: 0x0600000D RID: 13
		UpdateSyncSubscriptionResult UpdateSyncSubscription(UpdateSyncSubscriptionArgs args);

		// Token: 0x0600000E RID: 14
		GetSyncSubscriptionStateResult GetSyncSubscriptionState(GetSyncSubscriptionStateArgs args);
	}
}

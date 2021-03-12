using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect
{
	// Token: 0x020000DB RID: 219
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IRemoveConnectSubscription
	{
		// Token: 0x06000692 RID: 1682
		void TryRemovePermissions(IConnectSubscription subscription);
	}
}

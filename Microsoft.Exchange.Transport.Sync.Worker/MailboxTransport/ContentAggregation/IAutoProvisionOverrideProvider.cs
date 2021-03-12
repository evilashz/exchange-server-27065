using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200003F RID: 63
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IAutoProvisionOverrideProvider
	{
		// Token: 0x0600031E RID: 798
		bool TryGetOverrides(string domain, AggregationSubscriptionType type, out string[] overrideHosts, out bool trustForSendAs);
	}
}

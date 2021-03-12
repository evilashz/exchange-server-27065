using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000040 RID: 64
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AutoProvisionOverrideProvider : IAutoProvisionOverrideProvider
	{
		// Token: 0x0600031F RID: 799 RVA: 0x0000F1BE File Offset: 0x0000D3BE
		public bool TryGetOverrides(string domain, AggregationSubscriptionType type, out string[] overrideHosts, out bool trustForSendAs)
		{
			return AutoProvisionOverride.TryGetOverrides(domain, type, out overrideHosts, out trustForSendAs);
		}
	}
}

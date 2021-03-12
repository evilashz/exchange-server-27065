using System;
using System.Management.Automation;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x0200001C RID: 28
	[Cmdlet("Get", "HotmailSubscription", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class GetHotmailSubscription : GetSubscriptionBase<HotmailSubscriptionProxy>
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000736C File Offset: 0x0000556C
		protected override AggregationSubscriptionType IdentityType
		{
			get
			{
				return AggregationSubscriptionType.DeltaSyncMail;
			}
		}
	}
}

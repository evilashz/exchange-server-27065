using System;
using System.Management.Automation;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x0200001E RID: 30
	[Cmdlet("Get", "PopSubscription", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class GetPopSubscription : GetSubscriptionBase<PopSubscriptionProxy>
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00007383 File Offset: 0x00005583
		protected override AggregationSubscriptionType IdentityType
		{
			get
			{
				return AggregationSubscriptionType.Pop;
			}
		}
	}
}

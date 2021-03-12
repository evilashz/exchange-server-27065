using System;
using System.Management.Automation;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x0200001B RID: 27
	[Cmdlet("Get", "ConnectSubscription", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class GetConnectSubscription : GetSubscriptionBase<ConnectSubscriptionProxy>
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00007349 File Offset: 0x00005549
		protected override AggregationSubscriptionType IdentityType
		{
			get
			{
				return AggregationSubscriptionType.AllThatSupportPolicyInducedDeletion;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000734D File Offset: 0x0000554D
		protected override AggregationType AggregationTypeValue
		{
			get
			{
				return AggregationType.PeopleConnection;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00007351 File Offset: 0x00005551
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || ConnectSubscriptionTaskKnownExceptions.IsKnown(exception);
		}
	}
}

using System;
using System.Management.Automation;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000030 RID: 48
	[Cmdlet("Remove", "ConnectSubscription", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveConnectSubscription : RemoveSubscriptionBase<ConnectSubscriptionProxy>
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00009D0A File Offset: 0x00007F0A
		protected override AggregationType AggregationType
		{
			get
			{
				return AggregationType.PeopleConnection;
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009D0E File Offset: 0x00007F0E
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || ConnectSubscriptionTaskKnownExceptions.IsKnown(exception);
		}
	}
}

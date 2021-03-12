using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000C9 RID: 201
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SendAsSubscriptionsAndPeopleConnectResult
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001D4FF File Offset: 0x0001B6FF
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0001D507 File Offset: 0x0001B707
		public List<PimAggregationSubscription> PimSendAsAggregationSubscriptionList { get; private set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001D510 File Offset: 0x0001B710
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0001D518 File Offset: 0x0001B718
		public bool PeopleConnectionsExist { get; private set; }

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001D521 File Offset: 0x0001B721
		public SendAsSubscriptionsAndPeopleConnectResult(List<PimAggregationSubscription> pimSendAsAggregationSubscriptionList, bool peopleConnectionExist)
		{
			this.PimSendAsAggregationSubscriptionList = pimSendAsAggregationSubscriptionList;
			this.PeopleConnectionsExist = peopleConnectionExist;
		}
	}
}

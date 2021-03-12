using System;
using Microsoft.Exchange.MailboxLoadBalance.Band;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000DE RID: 222
	internal interface IRebalancingRequestProcessor
	{
		// Token: 0x060006E3 RID: 1763
		void ProcessRebalanceRequest(BandMailboxRebalanceData rebalanceRequest);
	}
}

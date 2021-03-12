using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002AC RID: 684
	[DataContract]
	public class HotmailSubscription : PimSubscription
	{
		// Token: 0x06002BCF RID: 11215 RVA: 0x0008854A File Offset: 0x0008674A
		public HotmailSubscription(HotmailSubscriptionProxy subscription) : base(subscription)
		{
		}
	}
}

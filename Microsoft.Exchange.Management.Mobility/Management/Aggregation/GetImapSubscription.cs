using System;
using System.Management.Automation;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x0200001D RID: 29
	[Cmdlet("Get", "ImapSubscription", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class GetImapSubscription : GetSubscriptionBase<IMAPSubscriptionProxy>
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00007377 File Offset: 0x00005577
		protected override AggregationSubscriptionType IdentityType
		{
			get
			{
				return AggregationSubscriptionType.IMAP;
			}
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect
{
	// Token: 0x020000D8 RID: 216
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConnectSubscriptionsEnumerator : IEnumerable<ConnectSubscription>, IEnumerable
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x0001FE0A File Offset: 0x0001E00A
		public ConnectSubscriptionsEnumerator(MailboxSession session, string provider)
		{
			SyncUtilities.ThrowIfArgumentNull("session", session);
			this.session = session;
			this.provider = this.NormalizeProvider(provider);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001FE34 File Offset: 0x0001E034
		public IEnumerator<ConnectSubscription> GetEnumerator()
		{
			if (string.IsNullOrEmpty(this.provider))
			{
				return Enumerable.Empty<ConnectSubscription>().GetEnumerator();
			}
			AggregationSubscriptionType providerFilter = this.GetProviderFilter();
			if (providerFilter == AggregationSubscriptionType.Unknown)
			{
				return Enumerable.Empty<ConnectSubscription>().GetEnumerator();
			}
			return SubscriptionManager.GetAllSubscriptions(this.session, providerFilter).Cast<ConnectSubscription>().GetEnumerator();
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001FE84 File Offset: 0x0001E084
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generic version of GetEnumerator.");
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001FE90 File Offset: 0x0001E090
		private AggregationSubscriptionType GetProviderFilter()
		{
			if (WellKnownNetworkNames.Facebook.Equals(this.provider, StringComparison.OrdinalIgnoreCase))
			{
				return AggregationSubscriptionType.Facebook;
			}
			if (WellKnownNetworkNames.LinkedIn.Equals(this.provider, StringComparison.OrdinalIgnoreCase))
			{
				return AggregationSubscriptionType.LinkedIn;
			}
			return AggregationSubscriptionType.Unknown;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001FEBF File Offset: 0x0001E0BF
		private string NormalizeProvider(string provider)
		{
			if (string.IsNullOrEmpty(provider))
			{
				return string.Empty;
			}
			return provider.Trim();
		}

		// Token: 0x04000385 RID: 901
		private readonly MailboxSession session;

		// Token: 0x04000386 RID: 902
		private readonly string provider;
	}
}

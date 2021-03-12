using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Net.LinkedIn;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect
{
	// Token: 0x020000DD RID: 221
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RemoveLinkedInSubscription : IRemoveConnectSubscription
	{
		// Token: 0x06000696 RID: 1686 RVA: 0x00020268 File Offset: 0x0001E468
		public RemoveLinkedInSubscription(ILinkedInWebClient client)
		{
			SyncUtilities.ThrowIfArgumentNull("client", client);
			this.linkedInClient = client;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00020284 File Offset: 0x0001E484
		public void TryRemovePermissions(IConnectSubscription subscription)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			try
			{
				this.linkedInClient.RemoveApplicationPermissions(subscription.AccessTokenInClearText, subscription.AccessTokenSecretInClearText);
			}
			catch (Exception ex)
			{
				CommonLoggingHelper.SyncLogSession.LogError((TSLID)177UL, RemoveLinkedInSubscription.Tracer, "RemoveLinkedInSubscription.TryRemovePermissions: {0} hit exception: {1}.", new object[]
				{
					subscription.SubscriptionGuid,
					ex
				});
			}
		}

		// Token: 0x0400038C RID: 908
		private static readonly Trace Tracer = ExTraceGlobals.SubscriptionRemoveTracer;

		// Token: 0x0400038D RID: 909
		private readonly ILinkedInWebClient linkedInClient;
	}
}

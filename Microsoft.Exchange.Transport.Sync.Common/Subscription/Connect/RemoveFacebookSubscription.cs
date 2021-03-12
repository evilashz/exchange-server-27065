using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Net.Facebook;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect
{
	// Token: 0x020000DC RID: 220
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RemoveFacebookSubscription : IRemoveConnectSubscription
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x000201CE File Offset: 0x0001E3CE
		public RemoveFacebookSubscription(IFacebookClient client)
		{
			SyncUtilities.ThrowIfArgumentNull("client", client);
			this.facebookClient = client;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000201E8 File Offset: 0x0001E3E8
		public void TryRemovePermissions(IConnectSubscription subscription)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			try
			{
				this.facebookClient.RemoveApplication(subscription.AccessTokenInClearText);
			}
			catch (Exception ex)
			{
				CommonLoggingHelper.SyncLogSession.LogError((TSLID)97UL, RemoveFacebookSubscription.Tracer, "RemoveFacebookSubscription.TryRemovePermissions: {0} hit exception: {1}.", new object[]
				{
					subscription.SubscriptionGuid,
					ex
				});
			}
		}

		// Token: 0x0400038A RID: 906
		private static readonly Trace Tracer = ExTraceGlobals.SubscriptionRemoveTracer;

		// Token: 0x0400038B RID: 907
		private readonly IFacebookClient facebookClient;
	}
}

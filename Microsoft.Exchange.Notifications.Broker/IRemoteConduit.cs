using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IRemoteConduit
	{
		// Token: 0x060001AF RID: 431
		void Start();

		// Token: 0x060001B0 RID: 432
		void Stop();

		// Token: 0x060001B1 RID: 433
		void Subscribe(HttpListenerContext context, OrganizationId resolvedOrganizationId);

		// Token: 0x060001B2 RID: 434
		void Unsubscribe(HttpListenerContext context, OrganizationId resolvedOrganizationId);

		// Token: 0x060001B3 RID: 435
		void DeliverNotificationBatch(HttpListenerContext context);

		// Token: 0x060001B4 RID: 436
		void Ping(HttpListenerContext context);

		// Token: 0x060001B5 RID: 437
		Task<RemoteCommandStatus> ForwardSubscribeAsync(BrokerSubscription brokerSubscription);

		// Token: 0x060001B6 RID: 438
		Task<RemoteCommandStatus> ForwardUnsubscribeAsync(BrokerSubscription brokerSubscription);
	}
}

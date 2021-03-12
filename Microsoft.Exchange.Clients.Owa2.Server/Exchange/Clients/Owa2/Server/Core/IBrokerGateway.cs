using System;
using Microsoft.Exchange.Notifications.Broker;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000155 RID: 341
	internal interface IBrokerGateway
	{
		// Token: 0x06000C67 RID: 3175
		void Subscribe(BrokerSubscription brokerSubscription, BrokerHandler handler);

		// Token: 0x06000C68 RID: 3176
		void Unsubscribe(BrokerSubscription brokerSubscription);
	}
}

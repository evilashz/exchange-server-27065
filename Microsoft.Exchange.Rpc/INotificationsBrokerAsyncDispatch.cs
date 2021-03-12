using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Notifications.Broker;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001E1 RID: 481
	internal interface INotificationsBrokerAsyncDispatch
	{
		// Token: 0x06000A41 RID: 2625
		ICancelableAsyncResult BeginSubscribe(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, string subscription, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A42 RID: 2626
		BrokerStatus EndSubscribe(ICancelableAsyncResult asyncResult);

		// Token: 0x06000A43 RID: 2627
		ICancelableAsyncResult BeginUnsubscribe(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, string subscription, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A44 RID: 2628
		BrokerStatus EndUnsubscribe(ICancelableAsyncResult asyncResult);

		// Token: 0x06000A45 RID: 2629
		ICancelableAsyncResult BeginGetNextNotification(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, int consumerId, Guid ackNotificationId, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A46 RID: 2630
		BrokerStatus EndGetNextNotification(ICancelableAsyncResult asyncResult, out string notification);
	}
}

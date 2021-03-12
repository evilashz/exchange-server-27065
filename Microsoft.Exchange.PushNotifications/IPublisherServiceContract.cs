using System;
using System.ServiceModel;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200000B RID: 11
	[ServiceContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal interface IPublisherServiceContract
	{
		// Token: 0x06000049 RID: 73
		[OperationContract(AsyncPattern = true)]
		[FaultContract(typeof(PushNotificationFault))]
		IAsyncResult BeginPublishNotifications(MailboxNotificationBatch notifications, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600004A RID: 74
		void EndPublishNotifications(IAsyncResult result);
	}
}

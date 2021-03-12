using System;
using System.ServiceModel;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000030 RID: 48
	[ServiceContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal interface ILocalUserNotificationPublisherServiceContract
	{
		// Token: 0x06000143 RID: 323
		[FaultContract(typeof(PushNotificationFault))]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginPublishUserNotifications(LocalUserNotificationBatch notifications, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000144 RID: 324
		void EndPublishUserNotifications(IAsyncResult result);
	}
}

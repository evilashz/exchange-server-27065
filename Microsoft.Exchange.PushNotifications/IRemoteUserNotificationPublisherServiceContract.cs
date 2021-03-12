using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000031 RID: 49
	[ServiceContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal interface IRemoteUserNotificationPublisherServiceContract
	{
		// Token: 0x06000145 RID: 325
		[WebInvoke(Method = "POST", UriTemplate = "PublishUserNotification", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		[FaultContract(typeof(PushNotificationFault))]
		IAsyncResult BeginPublishUserNotification(RemoteUserNotification notification, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000146 RID: 326
		void EndPublishUserNotification(IAsyncResult result);
	}
}

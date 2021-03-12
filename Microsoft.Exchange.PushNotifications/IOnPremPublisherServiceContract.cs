using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200001E RID: 30
	[ServiceContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal interface IOnPremPublisherServiceContract
	{
		// Token: 0x060000D6 RID: 214
		[WebInvoke(Method = "POST", UriTemplate = "PublishOnPremNotifications", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		[OperationContract(AsyncPattern = true)]
		[FaultContract(typeof(PushNotificationFault))]
		IAsyncResult BeginPublishOnPremNotifications(MailboxNotificationBatch notifications, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060000D7 RID: 215
		void EndPublishOnPremNotifications(IAsyncResult result);
	}
}

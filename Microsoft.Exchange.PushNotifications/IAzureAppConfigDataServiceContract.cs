using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000011 RID: 17
	[ServiceContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal interface IAzureAppConfigDataServiceContract
	{
		// Token: 0x06000079 RID: 121
		[FaultContract(typeof(PushNotificationFault))]
		[WebInvoke(Method = "POST", UriTemplate = "GetAppConfigData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetAppConfigData(AzureAppConfigRequestInfo requestConfig, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600007A RID: 122
		AzureAppConfigResponseInfo EndGetAppConfigData(IAsyncResult result);
	}
}

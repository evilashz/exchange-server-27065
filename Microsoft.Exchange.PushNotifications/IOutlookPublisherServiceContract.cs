using System;
using System.ServiceModel;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000029 RID: 41
	[ServiceContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal interface IOutlookPublisherServiceContract
	{
		// Token: 0x06000123 RID: 291
		[FaultContract(typeof(PushNotificationFault))]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginPublishOutlookNotifications(OutlookNotificationBatch notifications, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000124 RID: 292
		void EndPublishOutlookNotifications(IAsyncResult result);
	}
}

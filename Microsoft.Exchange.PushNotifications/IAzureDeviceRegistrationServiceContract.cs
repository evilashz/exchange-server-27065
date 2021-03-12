using System;
using System.ServiceModel;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000019 RID: 25
	[ServiceContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal interface IAzureDeviceRegistrationServiceContract
	{
		// Token: 0x060000B7 RID: 183
		[FaultContract(typeof(PushNotificationFault))]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginDeviceRegistration(AzureDeviceRegistrationInfo deviceRegistrationInfo, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060000B8 RID: 184
		void EndDeviceRegistration(IAsyncResult result);
	}
}

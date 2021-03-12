using System;
using System.ServiceModel;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200001B RID: 27
	[ServiceContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal interface IAzureHubCreationServiceContract
	{
		// Token: 0x060000C3 RID: 195
		[OperationContract(AsyncPattern = true)]
		[FaultContract(typeof(PushNotificationFault))]
		IAsyncResult BeginCreateHub(AzureHubDefinition hubDefinition, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060000C4 RID: 196
		void EndCreateHub(IAsyncResult result);
	}
}

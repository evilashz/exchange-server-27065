using System;
using System.ServiceModel;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000016 RID: 22
	[ServiceContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal interface IAzureChallengeRequestServiceContract
	{
		// Token: 0x0600009F RID: 159
		[OperationContract(AsyncPattern = true)]
		[FaultContract(typeof(PushNotificationFault))]
		IAsyncResult BeginChallengeRequest(AzureChallengeRequestInfo issueSecret, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060000A0 RID: 160
		void EndChallengeRequest(IAsyncResult result);
	}
}

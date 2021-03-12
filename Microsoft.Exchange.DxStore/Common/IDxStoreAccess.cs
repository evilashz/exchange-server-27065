using System;
using System.ServiceModel;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200004E RID: 78
	[ServiceContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	public interface IDxStoreAccess
	{
		// Token: 0x06000286 RID: 646
		[OperationContract]
		DxStoreAccessReply.CheckKey CheckKey(DxStoreAccessRequest.CheckKey request);

		// Token: 0x06000287 RID: 647
		[OperationContract]
		DxStoreAccessReply.DeleteKey DeleteKey(DxStoreAccessRequest.DeleteKey request);

		// Token: 0x06000288 RID: 648
		[OperationContract]
		DxStoreAccessReply.SetProperty SetProperty(DxStoreAccessRequest.SetProperty request);

		// Token: 0x06000289 RID: 649
		[OperationContract]
		DxStoreAccessReply.DeleteProperty DeleteProperty(DxStoreAccessRequest.DeleteProperty request);

		// Token: 0x0600028A RID: 650
		[OperationContract]
		DxStoreAccessReply.ExecuteBatch ExecuteBatch(DxStoreAccessRequest.ExecuteBatch request);

		// Token: 0x0600028B RID: 651
		[OperationContract]
		DxStoreAccessReply.GetProperty GetProperty(DxStoreAccessRequest.GetProperty request);

		// Token: 0x0600028C RID: 652
		[OperationContract]
		DxStoreAccessReply.GetAllProperties GetAllProperties(DxStoreAccessRequest.GetAllProperties request);

		// Token: 0x0600028D RID: 653
		[OperationContract]
		DxStoreAccessReply.GetPropertyNames GetPropertyNames(DxStoreAccessRequest.GetPropertyNames request);

		// Token: 0x0600028E RID: 654
		[OperationContract]
		DxStoreAccessReply.GetSubkeyNames GetSubkeyNames(DxStoreAccessRequest.GetSubkeyNames request);
	}
}

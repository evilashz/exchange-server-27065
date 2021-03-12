using System;
using System.ServiceModel;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000057 RID: 87
	[ServiceContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	public interface IDxStoreManager
	{
		// Token: 0x0600034E RID: 846
		[OperationContract]
		void StartInstance(string groupName, bool isForce = false);

		// Token: 0x0600034F RID: 847
		[OperationContract]
		void RestartInstance(string groupName, bool isForce = false);

		// Token: 0x06000350 RID: 848
		[OperationContract]
		void RemoveInstance(string groupName);

		// Token: 0x06000351 RID: 849
		[OperationContract]
		void StopInstance(string groupName, bool isDisable = true);

		// Token: 0x06000352 RID: 850
		[OperationContract]
		InstanceGroupConfig GetInstanceConfig(string groupName, bool isForce = false);

		// Token: 0x06000353 RID: 851
		[OperationContract]
		void TriggerRefresh(string reason, bool isForceRefreshCache);
	}
}

using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004CF RID: 1231
	[ServiceContract(Namespace = "ECP", Name = "UMMailbox")]
	public interface IUMMailboxService : IEditObjectService<SetUMMailboxConfiguration, SetUMMailboxParameters>, IGetObjectService<SetUMMailboxConfiguration>, INewGetObjectService<UMMailboxFeatureInfo, NewUMMailboxParameters, RecipientRow>, INewObjectService<UMMailboxFeatureInfo, NewUMMailboxParameters>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>
	{
		// Token: 0x06003C5D RID: 15453
		[OperationContract]
		PowerShellResults<NewUMMailboxConfiguration> GetConfigurationForNewUMMailbox(Identity identity, UMEnableSelectedPolicyParameters properties);
	}
}

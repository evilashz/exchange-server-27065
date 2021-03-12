using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200023F RID: 575
	[ServiceContract(Namespace = "ECP", Name = "SearchAllGroups")]
	public interface ISearchAllGroups : IGetListService<SearchAllGroupFilter, GroupRecipientRow>, IGetObjectService<ViewDistributionGroupData>
	{
		// Token: 0x0600282F RID: 10287
		[OperationContract]
		PowerShellResults JoinGroups(Identity[] identities);

		// Token: 0x06002830 RID: 10288
		[OperationContract]
		PowerShellResults<ViewDistributionGroupData> JoinGroup(Identity identity);

		// Token: 0x06002831 RID: 10289
		[OperationContract]
		PowerShellResults<ViewDistributionGroupData> LeaveGroup(Identity identity);
	}
}

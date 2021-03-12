using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000239 RID: 569
	[ServiceContract(Namespace = "ECP", Name = "OwnedGroups")]
	public interface IOwnedGroups : IGetListService<OwnedGroupFilter, DistributionGroupRow>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectForListService<DistributionGroup, SetMyDistributionGroup, DistributionGroupRow>, IGetObjectService<DistributionGroup>, IGetObjectForListService<DistributionGroupRow>
	{
	}
}

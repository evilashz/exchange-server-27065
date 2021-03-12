using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000273 RID: 627
	[ServiceContract(Namespace = "ECP", Name = "RetentionPolicyTags")]
	public interface IRetentionPolicyTags : IGetListService<AllAssociatedRPTsFilter, RetentionPolicyTagRow>, IGetObjectService<ViewRetentionPolicyTagRow>, INewObjectService<RetentionPolicyTagRow, AddRetentionPolicyTag>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>
	{
	}
}

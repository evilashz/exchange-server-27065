using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000231 RID: 561
	[ServiceContract(Namespace = "ECP", Name = "MemberOfGroups")]
	public interface IMemberOfGroups : IGetListService<MemberOfGroupFilter, RecipientRow>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>
	{
	}
}

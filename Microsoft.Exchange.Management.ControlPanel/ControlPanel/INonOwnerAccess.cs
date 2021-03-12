using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003E4 RID: 996
	[ServiceContract(Namespace = "ECP", Name = "NonOwnerAccess")]
	public interface INonOwnerAccess : IGetListService<NonOwnerAccessFilter, NonOwnerAccessResultRow>
	{
	}
}

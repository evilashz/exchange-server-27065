using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003EA RID: 1002
	[ServiceContract(Namespace = "ECP", Name = "NonOwnerAccessDetails")]
	public interface INonOwnerAccessDetails : IGetObjectService<NonOwnerAccessDetailRow>
	{
	}
}

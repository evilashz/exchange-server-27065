using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001E4 RID: 484
	[ServiceContract(Namespace = "ECP", Name = "OrgExtensions")]
	public interface IOrgExtensions : IGetListService<ExtensionFilter, ExtensionRow>, IGetObjectService<ExtensionRow>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>
	{
	}
}

using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001E2 RID: 482
	[ServiceContract(Namespace = "ECP", Name = "InstalledExtensions")]
	public interface IInstalledExtensions : IGetListService<ExtensionFilter, ExtensionRow>, IGetObjectService<ExtensionRow>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>
	{
		// Token: 0x060025BE RID: 9662
		[OperationContract]
		PowerShellResults<ExtensionRow> Disable(Identity[] identities, BaseWebServiceParameters parameters);

		// Token: 0x060025BF RID: 9663
		[OperationContract]
		PowerShellResults<ExtensionRow> Enable(Identity[] identities, BaseWebServiceParameters parameters);
	}
}

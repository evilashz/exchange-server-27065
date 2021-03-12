using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000254 RID: 596
	[ServiceContract(Namespace = "ECP", Name = "Organization")]
	public interface IOrganizationService : IAsyncService
	{
		// Token: 0x060028AC RID: 10412
		[OperationContract]
		PowerShellResults EnableOrganizationCustomization();
	}
}

using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000526 RID: 1318
	[ServiceContract(Namespace = "ECP", Name = "ManagementScopes")]
	public interface IManagementScopes : IGetListService<ManagementScopeFilter, ManagementScopeRow>
	{
	}
}

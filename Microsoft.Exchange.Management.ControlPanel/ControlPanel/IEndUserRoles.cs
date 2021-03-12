using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004FE RID: 1278
	[ServiceContract(Namespace = "ECP", Name = "EndUserRoles")]
	public interface IEndUserRoles : IGetListService<ManagementRoleFilter, EndUserRoleRow>
	{
	}
}

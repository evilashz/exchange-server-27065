using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000522 RID: 1314
	[ServiceContract(Namespace = "ECP", Name = "ManagementRoles")]
	public interface IManagementRoles : IGetListService<ManagementRoleFilter, ManagementRoleRow>
	{
	}
}

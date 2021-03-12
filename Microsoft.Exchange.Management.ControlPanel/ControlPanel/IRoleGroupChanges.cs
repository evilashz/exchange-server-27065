using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003F1 RID: 1009
	[ServiceContract(Namespace = "ECP", Name = "RoleGroupChanges")]
	public interface IRoleGroupChanges : IGetListService<AdminAuditLogSearchFilter, AdminAuditLogResultRow>
	{
	}
}

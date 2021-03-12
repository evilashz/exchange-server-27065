using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003EF RID: 1007
	[ServiceContract(Namespace = "ECP", Name = "RoleGroupChangeDetails")]
	public interface IRoleGroupChangeDetails : IGetObjectService<AdminAuditLogDetailRow>
	{
	}
}

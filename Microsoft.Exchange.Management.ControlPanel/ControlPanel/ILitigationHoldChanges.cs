using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003D5 RID: 981
	[ServiceContract(Namespace = "ECP", Name = "LitigationHoldChanges")]
	public interface ILitigationHoldChanges : IGetListService<AdminAuditLogSearchFilter, AdminAuditLogResultRow>
	{
	}
}

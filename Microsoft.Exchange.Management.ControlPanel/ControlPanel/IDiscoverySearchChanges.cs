using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003BC RID: 956
	[ServiceContract(Namespace = "ECP", Name = "DiscoverySearchChanges")]
	public interface IDiscoverySearchChanges : IGetListService<AdminAuditLogSearchFilter, AdminAuditLogResultRow>
	{
	}
}

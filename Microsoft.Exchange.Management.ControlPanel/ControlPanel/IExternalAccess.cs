using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003CA RID: 970
	[ServiceContract(Namespace = "ECP", Name = "ExternalAccess")]
	public interface IExternalAccess : IGetListService<ExternalAccessFilter, AdminAuditLogResultRow>
	{
	}
}

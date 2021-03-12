using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003CD RID: 973
	[ServiceContract(Namespace = "ECP", Name = "ExternalAccessDetails")]
	public interface IExternalAccessDetails : IGetObjectService<AdminAuditLogDetailRow>
	{
	}
}

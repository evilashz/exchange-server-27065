using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003D3 RID: 979
	[ServiceContract(Namespace = "ECP", Name = "LitigationHoldChangeDetails")]
	public interface ILitigationHoldChangeDetails : IGetObjectService<AdminAuditLogDetailRow>
	{
	}
}

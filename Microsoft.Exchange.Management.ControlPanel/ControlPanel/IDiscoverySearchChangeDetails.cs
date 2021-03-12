using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003BE RID: 958
	[ServiceContract(Namespace = "ECP", Name = "DiscoverySearchChangeDetails")]
	public interface IDiscoverySearchChangeDetails : IGetObjectService<AdminAuditLogDetailRow>
	{
	}
}

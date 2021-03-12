using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200052E RID: 1326
	[ServiceContract(Namespace = "ECP", Name = "OrgConfigs")]
	public interface IOrgConfigs : IEditObjectService<OrgConfig, SetOrgConfig>, IGetObjectService<OrgConfig>
	{
	}
}

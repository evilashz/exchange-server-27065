using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000BB RID: 187
	[ServiceContract(Namespace = "ECP", Name = "SchedulingPermissions")]
	public interface ISchedulingPermissions : IResourceBase<SchedulingPermissionsConfiguration, SetSchedulingPermissionsConfiguration>, IEditObjectService<SchedulingPermissionsConfiguration, SetSchedulingPermissionsConfiguration>, IGetObjectService<SchedulingPermissionsConfiguration>
	{
	}
}

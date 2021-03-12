using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000540 RID: 1344
	[ServiceContract(Namespace = "ECP", Name = "ViewAdminRoleGroups")]
	public interface IViewAdminRoleGroups : IGetObjectService<AdminRoleGroupObject>
	{
	}
}

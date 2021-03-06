using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004DE RID: 1246
	[ServiceContract(Namespace = "ECP", Name = "RoleGroups")]
	public interface IRoleGroups : IDataSourceService<AdminRoleGroupFilter, AdminRoleGroupRow, AdminRoleGroupObject, SetAdminRoleGroupParameter, NewAdminRoleGroupParameter>, IDataSourceService<AdminRoleGroupFilter, AdminRoleGroupRow, AdminRoleGroupObject, SetAdminRoleGroupParameter, NewAdminRoleGroupParameter, BaseWebServiceParameters>, IEditListService<AdminRoleGroupFilter, AdminRoleGroupRow, AdminRoleGroupObject, NewAdminRoleGroupParameter, BaseWebServiceParameters>, IGetListService<AdminRoleGroupFilter, AdminRoleGroupRow>, INewObjectService<AdminRoleGroupRow, NewAdminRoleGroupParameter>, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectForListService<AdminRoleGroupObject, SetAdminRoleGroupParameter, AdminRoleGroupRow>, IGetObjectService<AdminRoleGroupObject>, IGetObjectForListService<AdminRoleGroupRow>
	{
	}
}

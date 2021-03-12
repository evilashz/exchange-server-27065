using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000537 RID: 1335
	[ServiceContract(Namespace = "ECP", Name = "RoleAssignmentPolicies")]
	public interface IRoleAssignmentPolicies : IDataSourceService<RoleAssignmentPolicyFilter, RoleAssignmentPolicyRow, RoleAssignmentPolicy, SetRoleAssignmentPolicy, NewRoleAssignmentPolicy>, IDataSourceService<RoleAssignmentPolicyFilter, RoleAssignmentPolicyRow, RoleAssignmentPolicy, SetRoleAssignmentPolicy, NewRoleAssignmentPolicy, BaseWebServiceParameters>, IEditListService<RoleAssignmentPolicyFilter, RoleAssignmentPolicyRow, RoleAssignmentPolicy, NewRoleAssignmentPolicy, BaseWebServiceParameters>, IGetListService<RoleAssignmentPolicyFilter, RoleAssignmentPolicyRow>, INewObjectService<RoleAssignmentPolicyRow, NewRoleAssignmentPolicy>, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectForListService<RoleAssignmentPolicy, SetRoleAssignmentPolicy, RoleAssignmentPolicyRow>, IGetObjectService<RoleAssignmentPolicy>, IGetObjectForListService<RoleAssignmentPolicyRow>
	{
	}
}

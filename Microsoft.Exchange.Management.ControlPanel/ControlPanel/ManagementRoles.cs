using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000523 RID: 1315
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class ManagementRoles : DataSourceService, IManagementRoles, IGetListService<ManagementRoleFilter, ManagementRoleRow>
	{
		// Token: 0x06003ECC RID: 16076 RVA: 0x000BD3E8 File Offset: 0x000BB5E8
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-ManagementRole@R:Organization")]
		public PowerShellResults<ManagementRoleRow> GetList(ManagementRoleFilter filter, SortOptions sort)
		{
			PowerShellResults<ManagementRoleRow> list = base.GetList<ManagementRoleRow, ManagementRoleFilter>("Get-ManagementRole", filter, sort, "Name");
			list.Output = Array.FindAll<ManagementRoleRow>(list.Output, (ManagementRoleRow x) => !x.IsEndUserRole);
			return list;
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x000BD437 File Offset: 0x000BB637
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-ManagementRole?Identity@R:Organization")]
		public PowerShellResults<ManagementRoleObject> GetObject(Identity identity)
		{
			return base.GetObject<ManagementRoleObject>("Get-ManagementRole", identity);
		}

		// Token: 0x040028AA RID: 10410
		internal const string ReadScope = "@R:Organization";

		// Token: 0x040028AB RID: 10411
		internal const string GetListRole = "Get-ManagementRole@R:Organization";

		// Token: 0x040028AC RID: 10412
		private const string GetObjectRole = "Get-ManagementRole?Identity@R:Organization";
	}
}

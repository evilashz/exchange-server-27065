using System;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004FF RID: 1279
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class EndUserRoles : DataSourceService, IEndUserRoles, IGetListService<ManagementRoleFilter, EndUserRoleRow>
	{
		// Token: 0x06003D9D RID: 15773 RVA: 0x000B8C7C File Offset: 0x000B6E7C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-ManagementRole@R:Organization")]
		public PowerShellResults<EndUserRoleRow> GetList(ManagementRoleFilter filter, SortOptions sort)
		{
			PowerShellResults<EndUserRoleRow> list = base.GetList<EndUserRoleRow, ManagementRoleFilter>("Get-ManagementRole", filter, sort);
			if (!list.Succeeded)
			{
				return list;
			}
			list.Output = Array.FindAll<EndUserRoleRow>(list.Output, (EndUserRoleRow x) => x.IsEndUserRole);
			if (Util.IsDataCenter && filter != null && filter.Policy != null)
			{
				PowerShellResults<RoleAssignmentPolicy> result = EndUserRoles.policyService.GetObject(filter.Policy);
				if (result.HasValue)
				{
					bool flag = (from role in list.Output
					where !string.IsNullOrEmpty(role.MailboxPlanIndex)
					select role).Count<EndUserRoleRow>() > 0;
					if (flag)
					{
						list.Output = (from role in list.Output
						where string.IsNullOrEmpty(role.MailboxPlanIndex) || role.MailboxPlanIndex == result.Value.MailboxPlanIndex
						select role).ToArray<EndUserRoleRow>();
					}
				}
				else
				{
					list.Output = new EndUserRoleRow[0];
				}
			}
			return list;
		}

		// Token: 0x04002819 RID: 10265
		internal const string ReadScope = "@R:Organization";

		// Token: 0x0400281A RID: 10266
		internal const string GetListRole = "Get-ManagementRole@R:Organization";

		// Token: 0x0400281B RID: 10267
		private static RoleAssignmentPolicies policyService = new RoleAssignmentPolicies();
	}
}

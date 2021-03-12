using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003F2 RID: 1010
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class RoleGroupChanges : DataSourceService, IRoleGroupChanges, IGetListService<AdminAuditLogSearchFilter, AdminAuditLogResultRow>
	{
		// Token: 0x06003376 RID: 13174 RVA: 0x000A03BC File Offset: 0x0009E5BC
		[PrincipalPermission(SecurityAction.Demand, Role = "Search-AdminAuditLog?StartDate&EndDate&ObjectIds&Cmdlets@R:Organization")]
		public PowerShellResults<AdminAuditLogResultRow> GetList(AdminAuditLogSearchFilter filter, SortOptions sort)
		{
			if (filter == null)
			{
				filter = new AdminAuditLogSearchFilter();
			}
			filter.Cmdlets = "Add-RoleGroupMember,Remove-RoleGroupMember,Update-RoleGroupMember,New-RoleGroup,Remove-RoleGroup";
			PowerShellResults<AdminAuditLogResultRow> list = base.GetList<AdminAuditLogResultRow, AdminAuditLogSearchFilter>("Search-AdminAuditLog", filter, sort);
			PowerShellResults<AdminAuditLogResultRow> powerShellResults = new PowerShellResults<AdminAuditLogResultRow>();
			if (list.Succeeded)
			{
				Dictionary<string, AdminAuditLogResultRow> dictionary = new Dictionary<string, AdminAuditLogResultRow>();
				foreach (AdminAuditLogResultRow adminAuditLogResultRow in list.Output)
				{
					if (!dictionary.ContainsKey(adminAuditLogResultRow.ObjectModified.ToLower()))
					{
						dictionary[adminAuditLogResultRow.ObjectModified.ToLower()] = adminAuditLogResultRow;
					}
				}
				using (Dictionary<string, AdminAuditLogResultRow>.ValueCollection.Enumerator enumerator = dictionary.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AdminAuditLogResultRow adminAuditLogResultRow2 = enumerator.Current;
						Identity id = AuditHelper.CreateIdentity(adminAuditLogResultRow2.ObjectModified, filter.StartDate, filter.EndDate);
						powerShellResults.MergeOutput(new AdminAuditLogResultRow[]
						{
							new AdminAuditLogResultRow(id, adminAuditLogResultRow2.AuditReportSearchBaseResult)
						});
					}
					return powerShellResults;
				}
			}
			powerShellResults.MergeErrors<AdminAuditLogResultRow>(list);
			return powerShellResults;
		}

		// Token: 0x040024F6 RID: 9462
		internal const string NoStart = "NoStart";

		// Token: 0x040024F7 RID: 9463
		internal const string NoEnd = "NoEnd";

		// Token: 0x040024F8 RID: 9464
		internal const string GetCmdlet = "Search-AdminAuditLog";

		// Token: 0x040024F9 RID: 9465
		internal const string ReadScope = "@R:Organization";

		// Token: 0x040024FA RID: 9466
		internal const string AddRoleGroupMember = "Add-RoleGroupMember";

		// Token: 0x040024FB RID: 9467
		internal const string RemoveRoleGroupMember = "Remove-RoleGroupMember";

		// Token: 0x040024FC RID: 9468
		internal const string UpdateRoleGroupMember = "Update-RoleGroupMember";

		// Token: 0x040024FD RID: 9469
		internal const string NewRoleGroup = "New-RoleGroup";

		// Token: 0x040024FE RID: 9470
		internal const string RemoveRoleGroup = "Remove-RoleGroup";

		// Token: 0x040024FF RID: 9471
		internal const string CmdletsAudited = "Add-RoleGroupMember,Remove-RoleGroupMember,Update-RoleGroupMember,New-RoleGroup,Remove-RoleGroup";

		// Token: 0x04002500 RID: 9472
		private const string GetListRole = "Search-AdminAuditLog?StartDate&EndDate&ObjectIds&Cmdlets@R:Organization";
	}
}

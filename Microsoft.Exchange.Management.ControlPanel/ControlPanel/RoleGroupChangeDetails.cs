using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003F0 RID: 1008
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class RoleGroupChangeDetails : DataSourceService, IRoleGroupChangeDetails, IGetObjectService<AdminAuditLogDetailRow>
	{
		// Token: 0x06003374 RID: 13172 RVA: 0x000A027C File Offset: 0x0009E47C
		[PrincipalPermission(SecurityAction.Demand, Role = "Search-AdminAuditLog?StartDate&EndDate&ObjectIds&Cmdlets@R:Organization")]
		public PowerShellResults<AdminAuditLogDetailRow> GetObject(Identity identity)
		{
			AuditLogDetailsId auditLogDetailsId = new AuditLogDetailsId(identity);
			AdminAuditLogSearchFilter adminAuditLogSearchFilter = new AdminAuditLogSearchFilter();
			adminAuditLogSearchFilter.Cmdlets = "Add-RoleGroupMember,Remove-RoleGroupMember,Update-RoleGroupMember,New-RoleGroup,Remove-RoleGroup";
			if (auditLogDetailsId.StartDate != "NoStart")
			{
				adminAuditLogSearchFilter.StartDate = auditLogDetailsId.StartDate;
			}
			if (auditLogDetailsId.EndDate != "NoEnd")
			{
				adminAuditLogSearchFilter.EndDate = auditLogDetailsId.EndDate;
			}
			adminAuditLogSearchFilter.ObjectIds = auditLogDetailsId.Object;
			PSCommand pscommand = new PSCommand().AddCommand("Search-AdminAuditLog").AddParameters(adminAuditLogSearchFilter);
			pscommand.AddParameter("resultSize", 501);
			PowerShellResults<AdminAuditLogEvent> powerShellResults = base.Invoke<AdminAuditLogEvent>(pscommand);
			PowerShellResults<AdminAuditLogDetailRow> powerShellResults2 = new PowerShellResults<AdminAuditLogDetailRow>();
			if (powerShellResults.Succeeded)
			{
				if (powerShellResults.Output.Length == 501)
				{
					powerShellResults2.Warnings = new string[]
					{
						Strings.TooManyAuditLogsInDetailsPane
					};
				}
				List<AdminAuditLogDetailRow> list = new List<AdminAuditLogDetailRow>();
				for (int i = 0; i < powerShellResults.Output.Length; i++)
				{
					list.Add(new AdminAuditLogDetailRow(identity, auditLogDetailsId.Object, powerShellResults.Output[i]));
				}
				powerShellResults2.MergeOutput(list.ToArray());
				return powerShellResults2;
			}
			powerShellResults2.MergeErrors<AdminAuditLogEvent>(powerShellResults);
			return powerShellResults2;
		}

		// Token: 0x040024F4 RID: 9460
		internal const string ReadScope = "@R:Organization";

		// Token: 0x040024F5 RID: 9461
		private const string GetObjectRole = "Search-AdminAuditLog?StartDate&EndDate&ObjectIds&Cmdlets@R:Organization";
	}
}

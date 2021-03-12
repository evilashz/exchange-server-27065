using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003D6 RID: 982
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class LitigationHoldChanges : DataSourceService, ILitigationHoldChanges, IGetListService<AdminAuditLogSearchFilter, AdminAuditLogResultRow>
	{
		// Token: 0x06003257 RID: 12887 RVA: 0x0009C934 File Offset: 0x0009AB34
		[PrincipalPermission(SecurityAction.Demand, Role = "Search-AdminAuditLog?StartDate&EndDate&ObjectIds&Cmdlets@R:Organization")]
		public PowerShellResults<AdminAuditLogResultRow> GetList(AdminAuditLogSearchFilter filter, SortOptions sort)
		{
			if (filter == null)
			{
				filter = new AdminAuditLogSearchFilter();
			}
			filter.Cmdlets = "Set-Mailbox";
			filter.Parameters = "LitigationHoldEnabled";
			if (filter != null && filter.ObjectIds != null && filter.ObjectIds.Length > 0)
			{
				SmtpAddress[] addresses = filter.ObjectIds.ToSmtpAddressArray();
				string[] identitiesFromSmtpAddresses = AuditHelper.GetIdentitiesFromSmtpAddresses(addresses);
				if (identitiesFromSmtpAddresses.Length != 0)
				{
					filter.ObjectIds = string.Join(",", identitiesFromSmtpAddresses);
				}
			}
			PowerShellResults<AdminAuditLogResultRow> list = base.GetList<AdminAuditLogResultRow, AdminAuditLogSearchFilter>("Search-AdminAuditLog", filter, sort);
			PowerShellResults<AdminAuditLogResultRow> powerShellResults = new PowerShellResults<AdminAuditLogResultRow>();
			if (list.Succeeded)
			{
				string a = null;
				foreach (AdminAuditLogResultRow adminAuditLogResultRow in list.Output)
				{
					if (!string.Equals(a, adminAuditLogResultRow.ObjectModified, StringComparison.InvariantCultureIgnoreCase))
					{
						a = adminAuditLogResultRow.ObjectModified;
						powerShellResults.MergeOutput(new AdminAuditLogResultRow[]
						{
							adminAuditLogResultRow
						});
					}
				}
				int num = powerShellResults.Output.Length;
				AdminAuditLogResultRow[] array = new AdminAuditLogResultRow[num];
				for (int j = 0; j < num; j++)
				{
					Identity id = AuditHelper.CreateIdentity(powerShellResults.Output[j].ObjectModified, filter.StartDate, filter.EndDate);
					array[j] = new AdminAuditLogResultRow(id, powerShellResults.Output[j].AuditReportSearchBaseResult);
				}
				powerShellResults.Output = array;
				return powerShellResults;
			}
			return list;
		}

		// Token: 0x04002487 RID: 9351
		internal const string GetCmdlet = "Search-AdminAuditLog";

		// Token: 0x04002488 RID: 9352
		internal const string ReadScope = "@R:Organization";

		// Token: 0x04002489 RID: 9353
		internal const string GetListRole = "Search-AdminAuditLog?StartDate&EndDate&ObjectIds&Cmdlets@R:Organization";

		// Token: 0x0400248A RID: 9354
		internal const string CmdletsAudited = "Set-Mailbox";

		// Token: 0x0400248B RID: 9355
		internal const string ParametersAudited = "LitigationHoldEnabled";
	}
}

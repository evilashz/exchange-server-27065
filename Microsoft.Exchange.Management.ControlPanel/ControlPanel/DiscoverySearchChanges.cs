using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003BD RID: 957
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class DiscoverySearchChanges : DataSourceService, IDiscoverySearchChanges, IGetListService<AdminAuditLogSearchFilter, AdminAuditLogResultRow>
	{
		// Token: 0x06003204 RID: 12804 RVA: 0x0009B300 File Offset: 0x00099500
		[PrincipalPermission(SecurityAction.Demand, Role = "Search-AdminAuditLog?StartDate&EndDate&ObjectIds&Cmdlets@R:Organization")]
		public PowerShellResults<AdminAuditLogResultRow> GetList(AdminAuditLogSearchFilter filter, SortOptions sort)
		{
			if (filter == null)
			{
				filter = new AdminAuditLogSearchFilter();
			}
			filter.Cmdlets = "New-MailboxSearch, Start-MailboxSearch, Get-MailboxSearch, Stop-MailboxSearch, Remove-MailboxSearch, Set-MailboxSearch";
			filter.Parameters = "*";
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
					if (!string.Equals(a, adminAuditLogResultRow.SearchObject, StringComparison.InvariantCultureIgnoreCase))
					{
						a = adminAuditLogResultRow.SearchObject;
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
					Identity id = AuditHelper.CreateIdentity(powerShellResults.Output[j].SearchObject, filter.StartDate, filter.EndDate);
					array[j] = new AdminAuditLogResultRow(id, powerShellResults.Output[j].AuditReportSearchBaseResult);
				}
				powerShellResults.Output = array;
				return powerShellResults;
			}
			return list;
		}

		// Token: 0x04002463 RID: 9315
		internal const string GetCmdlet = "Search-AdminAuditLog";

		// Token: 0x04002464 RID: 9316
		internal const string ReadScope = "@R:Organization";

		// Token: 0x04002465 RID: 9317
		internal const string GetListRole = "Search-AdminAuditLog?StartDate&EndDate&ObjectIds&Cmdlets@R:Organization";

		// Token: 0x04002466 RID: 9318
		internal const string CmdletsAudited = "New-MailboxSearch, Start-MailboxSearch, Get-MailboxSearch, Stop-MailboxSearch, Remove-MailboxSearch, Set-MailboxSearch";

		// Token: 0x04002467 RID: 9319
		internal const string ParametersAudited = "*";
	}
}

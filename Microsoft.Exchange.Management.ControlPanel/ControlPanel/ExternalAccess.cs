using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003CB RID: 971
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class ExternalAccess : DataSourceService, IExternalAccess, IGetListService<ExternalAccessFilter, AdminAuditLogResultRow>
	{
		// Token: 0x06003232 RID: 12850 RVA: 0x0009BE50 File Offset: 0x0009A050
		[PrincipalPermission(SecurityAction.Demand, Role = "Search-AdminAuditLog?ResultSize&StartDate&EndDate&ExternalAccess@R:Organization")]
		public PowerShellResults<AdminAuditLogResultRow> GetList(ExternalAccessFilter filter, SortOptions sort)
		{
			filter.ExternalAccess = new bool?(true);
			PowerShellResults<AdminAuditLogResultRow> list = base.GetList<AdminAuditLogResultRow, ExternalAccessFilter>("Search-AdminAuditLog", filter, sort);
			if (list.Succeeded)
			{
				PowerShellResults<AdminAuditLogResultRow> powerShellResults = new PowerShellResults<AdminAuditLogResultRow>();
				int num = list.Output.Length;
				AdminAuditLogResultRow[] array = new AdminAuditLogResultRow[num];
				for (int i = 0; i < num; i++)
				{
					string text = string.Format("{0};{1};{2};{3}", new object[]
					{
						list.Output[i].AuditReportSearchBaseResult.ObjectModified,
						list.Output[i].AuditReportSearchBaseResult.CmdletName,
						filter.StartDate,
						filter.EndDate
					});
					Identity id = new Identity(text, text);
					array[i] = new AdminAuditLogResultRow(id, list.Output[i].AuditReportSearchBaseResult);
				}
				powerShellResults.Output = array;
				return powerShellResults;
			}
			return list;
		}

		// Token: 0x04002476 RID: 9334
		internal const string NoStart = "NoStart";

		// Token: 0x04002477 RID: 9335
		internal const string NoEnd = "NoEnd";

		// Token: 0x04002478 RID: 9336
		internal const string GetCmdlet = "Search-AdminAuditLog";

		// Token: 0x04002479 RID: 9337
		private const string GetListRole = "Search-AdminAuditLog?ResultSize&StartDate&EndDate&ExternalAccess@R:Organization";
	}
}

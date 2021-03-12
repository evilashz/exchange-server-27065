using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004B9 RID: 1209
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class UMCallSummaryReportService : DataSourceService, IUMCallSummaryReportService, IGetListService<UMCallSummaryReportFilter, UMCallSummaryReportRow>
	{
		// Token: 0x06003BB9 RID: 15289 RVA: 0x000B4264 File Offset: 0x000B2464
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMCallSummaryReport?GroupBy@R:Organization")]
		public PowerShellResults<UMCallSummaryReportRow> GetList(UMCallSummaryReportFilter filter, SortOptions sort)
		{
			bool isExportCallDataEnabled = false;
			if (filter != null && string.Equals(filter.GroupBy, GroupBy.Day.ToString()))
			{
				isExportCallDataEnabled = true;
			}
			PowerShellResults<UMCallSummaryReportRow> list = base.GetList<UMCallSummaryReportRow, UMCallSummaryReportFilter>("Get-UMCallSummaryReport", filter, sort);
			if (list.Succeeded)
			{
				foreach (UMCallSummaryReportRow umcallSummaryReportRow in list.Output)
				{
					umcallSummaryReportRow.IsExportCallDataEnabled = isExportCallDataEnabled;
					umcallSummaryReportRow.UMDialPlanID = filter.UMDialPlan;
					umcallSummaryReportRow.UMIPGatewayID = filter.UMIPGateway;
				}
			}
			return list;
		}

		// Token: 0x0400277B RID: 10107
		private const string Noun = "UMCallSummaryReport";

		// Token: 0x0400277C RID: 10108
		internal const string GetCmdlet = "Get-UMCallSummaryReport";

		// Token: 0x0400277D RID: 10109
		internal const string ReadScope = "@R:Organization";

		// Token: 0x0400277E RID: 10110
		private const string GetListRole = "Get-UMCallSummaryReport?GroupBy@R:Organization";
	}
}

using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006E8 RID: 1768
	[Cmdlet("Get", "ScorecardMetricsReport")]
	[OutputType(new Type[]
	{
		typeof(ScorecardMetricsReport)
	})]
	public sealed class GetScorecardMetricsReport : TenantReportBase<ScorecardMetricsReport>
	{
	}
}

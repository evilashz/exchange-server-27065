using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006EB RID: 1771
	[Cmdlet("Get", "SPOOneDriveForBusinessUserStatisticsReport")]
	[OutputType(new Type[]
	{
		typeof(SPOODBUserStatisticsReport)
	})]
	public sealed class GetSPOODBUserStatisticsReport : ScaledTenantReportBase<SPOODBUserStatisticsReport>
	{
	}
}

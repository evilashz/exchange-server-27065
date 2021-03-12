using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006DE RID: 1758
	[Cmdlet("Get", "LicenseVsUsageSummaryReport")]
	[OutputType(new Type[]
	{
		typeof(LicenseVsUsageSummaryReport)
	})]
	public sealed class GetLicenseVsUsageSummaryReport : TenantReportBase<LicenseVsUsageSummaryReport>
	{
	}
}

using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006EA RID: 1770
	[Cmdlet("Get", "SPOOneDriveForBusinessFileActivityReport")]
	[OutputType(new Type[]
	{
		typeof(SPOODBFileActivityReport)
	})]
	public sealed class GetSPOODBFileActivityReport : ScaledTenantReportBase<SPOODBFileActivityReport>
	{
	}
}

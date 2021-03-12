using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006F2 RID: 1778
	[Cmdlet("Get", "StaleMailboxReport")]
	[OutputType(new Type[]
	{
		typeof(StaleMailboxReport)
	})]
	public sealed class GetStaleMailboxReport : TenantReportBase<StaleMailboxReport>
	{
	}
}

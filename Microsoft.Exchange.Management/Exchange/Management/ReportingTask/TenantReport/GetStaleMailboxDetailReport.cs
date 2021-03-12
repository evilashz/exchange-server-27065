using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006F1 RID: 1777
	[OutputType(new Type[]
	{
		typeof(StaleMailboxDetailReport)
	})]
	[Cmdlet("Get", "StaleMailboxDetailReport")]
	public sealed class GetStaleMailboxDetailReport : ScaledTenantReportBase<StaleMailboxDetailReport>
	{
	}
}

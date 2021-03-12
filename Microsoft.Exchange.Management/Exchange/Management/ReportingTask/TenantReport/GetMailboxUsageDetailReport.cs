using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006E1 RID: 1761
	[Cmdlet("Get", "MailboxUsageDetailReport")]
	[OutputType(new Type[]
	{
		typeof(MailboxUsageDetailReport)
	})]
	public sealed class GetMailboxUsageDetailReport : ScaledTenantReportBase<MailboxUsageDetailReport>
	{
	}
}

using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006E2 RID: 1762
	[Cmdlet("Get", "MailboxUsageReport")]
	[OutputType(new Type[]
	{
		typeof(MailboxUsageReport)
	})]
	public sealed class GetMailboxUsageReport : TenantReportBase<MailboxUsageReport>
	{
	}
}

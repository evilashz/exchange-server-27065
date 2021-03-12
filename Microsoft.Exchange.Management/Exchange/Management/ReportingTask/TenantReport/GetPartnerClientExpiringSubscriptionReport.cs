using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006E3 RID: 1763
	[Cmdlet("Get", "PartnerClientExpiringSubscriptionReport")]
	[OutputType(new Type[]
	{
		typeof(PartnerClientExpiringSubscriptionReport)
	})]
	public sealed class GetPartnerClientExpiringSubscriptionReport : TenantReportBase<PartnerClientExpiringSubscriptionReport>
	{
	}
}

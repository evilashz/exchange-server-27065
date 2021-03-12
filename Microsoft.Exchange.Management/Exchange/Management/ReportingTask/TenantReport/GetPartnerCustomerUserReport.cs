using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006E4 RID: 1764
	[Cmdlet("Get", "PartnerCustomerUserReport")]
	[OutputType(new Type[]
	{
		typeof(PartnerCustomerUserReport)
	})]
	public sealed class GetPartnerCustomerUserReport : TenantReportBase<PartnerCustomerUserReport>
	{
	}
}

using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006D5 RID: 1749
	[OutputType(new Type[]
	{
		typeof(CsClientDeviceReport)
	})]
	[Cmdlet("Get", "CsClientDeviceReport")]
	public sealed class GetCsClientDeviceReport : TenantReportBase<CsClientDeviceReport>
	{
	}
}

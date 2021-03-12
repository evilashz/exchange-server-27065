using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004B8 RID: 1208
	[ServiceContract(Namespace = "ECP", Name = "UMCallSummaryReport")]
	public interface IUMCallSummaryReportService : IGetListService<UMCallSummaryReportFilter, UMCallSummaryReportRow>
	{
	}
}

using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200014A RID: 330
	internal interface IUMAggregatedData
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000ABA RID: 2746
		DateTime WaterMark { get; }

		// Token: 0x06000ABB RID: 2747
		void AddCDR(CDRData cdrData);

		// Token: 0x06000ABC RID: 2748
		void Cleanup(OrganizationId orgId);

		// Token: 0x06000ABD RID: 2749
		UMReportRawCounters[] QueryAggregatedData(Guid dialPlanGuid, Guid gatewayGuid, GroupBy groupBy);
	}
}

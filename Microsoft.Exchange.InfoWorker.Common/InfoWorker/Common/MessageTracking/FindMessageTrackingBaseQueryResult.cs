using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002A5 RID: 677
	internal sealed class FindMessageTrackingBaseQueryResult : BaseQueryResult
	{
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x00056CD6 File Offset: 0x00054ED6
		// (set) Token: 0x060012ED RID: 4845 RVA: 0x00056CDE File Offset: 0x00054EDE
		public FindMessageTrackingReportResponseMessageType Response { get; internal set; }

		// Token: 0x060012EE RID: 4846 RVA: 0x00056CE7 File Offset: 0x00054EE7
		internal FindMessageTrackingBaseQueryResult()
		{
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00056CEF File Offset: 0x00054EEF
		internal FindMessageTrackingBaseQueryResult(LocalizedException exception) : base(exception)
		{
		}
	}
}

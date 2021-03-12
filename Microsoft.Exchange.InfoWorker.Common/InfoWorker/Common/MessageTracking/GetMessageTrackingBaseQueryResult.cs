using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002AA RID: 682
	internal sealed class GetMessageTrackingBaseQueryResult : BaseQueryResult
	{
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00057202 File Offset: 0x00055402
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x0005720A File Offset: 0x0005540A
		public GetMessageTrackingReportResponseMessageType Response { get; internal set; }

		// Token: 0x0600130E RID: 4878 RVA: 0x00057213 File Offset: 0x00055413
		internal GetMessageTrackingBaseQueryResult()
		{
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0005721B File Offset: 0x0005541B
		internal GetMessageTrackingBaseQueryResult(LocalizedException exception) : base(exception)
		{
		}
	}
}

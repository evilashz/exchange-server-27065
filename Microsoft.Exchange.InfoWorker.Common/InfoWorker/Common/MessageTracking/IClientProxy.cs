using System;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000286 RID: 646
	internal interface IClientProxy
	{
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001263 RID: 4707
		string TargetInfoForLogging { get; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001264 RID: 4708
		string TargetInfoForDisplay { get; }

		// Token: 0x06001265 RID: 4709
		FindMessageTrackingReportResponseMessageType FindMessageTrackingReport(FindMessageTrackingReportRequestTypeWrapper request, TimeSpan timeout);

		// Token: 0x06001266 RID: 4710
		InternalGetMessageTrackingReportResponse GetMessageTrackingReport(GetMessageTrackingReportRequestTypeWrapper request, TimeSpan timeout);
	}
}

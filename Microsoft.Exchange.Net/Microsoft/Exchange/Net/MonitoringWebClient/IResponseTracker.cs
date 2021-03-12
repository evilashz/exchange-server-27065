using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007C5 RID: 1989
	internal interface IResponseTracker
	{
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x0600291C RID: 10524
		List<ResponseTrackerItem> Items { get; }

		// Token: 0x0600291D RID: 10525
		ResponseTrackerItem TrackRequest(TestId testId, RequestTarget requestTarget, HttpWebRequestWrapper request);

		// Token: 0x0600291E RID: 10526
		void TrackSentRequest(ResponseTrackerItem item, HttpWebRequestWrapper request);

		// Token: 0x0600291F RID: 10527
		void TrackResolvedRequest(HttpWebRequestWrapper request);

		// Token: 0x06002920 RID: 10528
		void TrackResponse(ResponseTrackerItem item, HttpWebResponseWrapper response);

		// Token: 0x06002921 RID: 10529
		void TrackFailedResponse(HttpWebResponseWrapper response, ScenarioException exception);

		// Token: 0x06002922 RID: 10530
		void TrackFailedTcpConnection(HttpWebRequestWrapper request, Exception exception);

		// Token: 0x06002923 RID: 10531
		void TrackFailedRequest(TestId testId, RequestTarget requestTarget, HttpWebRequestWrapper request, Exception exception);

		// Token: 0x06002924 RID: 10532
		void TrackItemCausingScenarioTimeout(ResponseTrackerItem item, Exception exception);
	}
}

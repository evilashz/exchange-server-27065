using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002C1 RID: 705
	internal interface IWebServiceBinding
	{
		// Token: 0x060013A9 RID: 5033
		InternalGetMessageTrackingReportResponse GetMessageTrackingReport(string messageTrackingReportId, ReportTemplate reportTemplate, SmtpAddress[] recipientFilter, SearchScope scope, bool returnQueueEvents, TrackingEventBudget eventBudget);

		// Token: 0x060013AA RID: 5034
		FindMessageTrackingReportResponseMessageType FindMessageTrackingReport(string domain, SmtpAddress? senderAddress, SmtpAddress? recipientAddress, string serverHint, SmtpAddress? federatedDeliveryMailbox, SearchScope scope, string messageId, string subject, bool expandTree, bool searchAsRecip, bool searchForModerationResult, DateTime start, DateTime end, TrackingEventBudget eventBudget);

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060013AB RID: 5035
		string TargetInfoForDisplay { get; }
	}
}

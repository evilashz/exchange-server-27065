using System;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000287 RID: 647
	internal class ClientProxyEWS : IClientProxy
	{
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x00055591 File Offset: 0x00053791
		// (set) Token: 0x06001268 RID: 4712 RVA: 0x00055599 File Offset: 0x00053799
		public string TargetInfoForLogging { get; private set; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x000555A2 File Offset: 0x000537A2
		public string TargetInfoForDisplay
		{
			get
			{
				return this.TargetInfoForLogging;
			}
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x000555AA File Offset: 0x000537AA
		public ClientProxyEWS(ExchangeServiceBinding ewsBinding, Uri uri, int serverVersion)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.ewsBinding = ewsBinding;
			this.TargetInfoForLogging = uri.ToString();
			this.serverVersion = serverVersion;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x000555E0 File Offset: 0x000537E0
		public FindMessageTrackingReportResponseMessageType FindMessageTrackingReport(FindMessageTrackingReportRequestTypeWrapper request, TimeSpan timeout)
		{
			this.ewsBinding.Timeout = (int)Math.Min(timeout.TotalMilliseconds, 2147483647.0);
			return this.ewsBinding.FindMessageTrackingReport(request.PrepareEWSRequest(this.serverVersion));
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0005561C File Offset: 0x0005381C
		public InternalGetMessageTrackingReportResponse GetMessageTrackingReport(GetMessageTrackingReportRequestTypeWrapper request, TimeSpan timeout)
		{
			this.ewsBinding.Timeout = (int)Math.Min(timeout.TotalMilliseconds, 2147483647.0);
			GetMessageTrackingReportResponseMessageType messageTrackingReport = this.ewsBinding.GetMessageTrackingReport(request.PrepareEWSRequest(this.serverVersion));
			MessageTrackingReportId messageTrackingReportId;
			if (!MessageTrackingReportId.TryParse(request.WrappedRequest.MessageTrackingReportId, out messageTrackingReportId))
			{
				throw new ArgumentException("Invalid MessageTrackingReportId, caller should have validated");
			}
			return InternalGetMessageTrackingReportResponse.Create(messageTrackingReportId.Domain, messageTrackingReport);
		}

		// Token: 0x04000C08 RID: 3080
		private ExchangeServiceBinding ewsBinding;

		// Token: 0x04000C09 RID: 3081
		private int serverVersion;
	}
}

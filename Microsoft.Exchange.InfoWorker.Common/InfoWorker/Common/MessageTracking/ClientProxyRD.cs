using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000288 RID: 648
	internal class ClientProxyRD : IClientProxy
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x0005568D File Offset: 0x0005388D
		// (set) Token: 0x0600126E RID: 4718 RVA: 0x00055695 File Offset: 0x00053895
		public string TargetInfoForLogging { get; private set; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x0005569E File Offset: 0x0005389E
		// (set) Token: 0x06001270 RID: 4720 RVA: 0x000556A6 File Offset: 0x000538A6
		public string TargetInfoForDisplay { get; private set; }

		// Token: 0x06001271 RID: 4721 RVA: 0x000556B0 File Offset: 0x000538B0
		public ClientProxyRD(DirectoryContext directoryContext, SmtpAddress proxyRecipient, string domain, ExchangeVersion ewsVersionRequested)
		{
			if (SmtpAddress.Empty.Equals(proxyRecipient) && string.IsNullOrEmpty(domain))
			{
				throw new ArgumentException("Either proxyRecipient or domain must be supplied");
			}
			this.directoryContext = directoryContext;
			this.proxyRecipient = proxyRecipient;
			this.domain = domain;
			this.ewsVersionRequested = ewsVersionRequested;
			string arg = proxyRecipient.ToString();
			string text = (domain == null) ? string.Empty : domain;
			this.TargetInfoForLogging = string.Format("({0}+{1})", arg, text);
			this.TargetInfoForDisplay = text;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00055738 File Offset: 0x00053938
		Microsoft.Exchange.SoapWebClient.EWS.FindMessageTrackingReportResponseMessageType IClientProxy.FindMessageTrackingReport(FindMessageTrackingReportRequestTypeWrapper request, TimeSpan timeout)
		{
			FindMessageTrackingQuery findMessageTrackingQuery = new FindMessageTrackingQuery(this.proxyRecipient, this.domain, this.directoryContext, request, this.ewsVersionRequested, timeout);
			FindMessageTrackingQueryResult findMessageTrackingQueryResult = findMessageTrackingQuery.Execute();
			if (findMessageTrackingQueryResult == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Empty result in Request Dispatcher FindMessageTrackingQuery.Execute", new object[0]);
				return null;
			}
			Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.FindMessageTrackingReportResponseMessageType response = findMessageTrackingQueryResult.Response;
			return MessageConverter.CopyDispatcherTypeToEWSType(findMessageTrackingQueryResult.Response);
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x000557A0 File Offset: 0x000539A0
		InternalGetMessageTrackingReportResponse IClientProxy.GetMessageTrackingReport(GetMessageTrackingReportRequestTypeWrapper request, TimeSpan timeout)
		{
			GetMessageTrackingQuery getMessageTrackingQuery = new GetMessageTrackingQuery(this.proxyRecipient, this.directoryContext, request, this.ewsVersionRequested, timeout);
			GetMessageTrackingQueryResult getMessageTrackingQueryResult = getMessageTrackingQuery.Execute();
			if (getMessageTrackingQueryResult == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Empty result in Request Dispatcher FindMessageTrackingQuery.Execute", new object[0]);
				return null;
			}
			Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.GetMessageTrackingReportResponseMessageType response = getMessageTrackingQueryResult.Response;
			MessageTrackingReportId messageTrackingReportId;
			if (!MessageTrackingReportId.TryParse(request.WrappedRequest.MessageTrackingReportId, out messageTrackingReportId))
			{
				throw new ArgumentException("Invalid MessageTrackingReportId, caller should have validated");
			}
			return InternalGetMessageTrackingReportResponse.Create(messageTrackingReportId.Domain, response);
		}

		// Token: 0x04000C0B RID: 3083
		private DirectoryContext directoryContext;

		// Token: 0x04000C0C RID: 3084
		private SmtpAddress proxyRecipient;

		// Token: 0x04000C0D RID: 3085
		private string domain;

		// Token: 0x04000C0E RID: 3086
		private ExchangeVersion ewsVersionRequested;
	}
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002A8 RID: 680
	internal sealed class GetMessageTrackingApplication : MessageTrackingApplication
	{
		// Token: 0x060012F9 RID: 4857 RVA: 0x00056FA0 File Offset: 0x000551A0
		public GetMessageTrackingApplication(GetMessageTrackingReportRequestTypeWrapper request, ExchangeVersion ewsRequestedVersion) : base(false, false, ewsRequestedVersion)
		{
			this.traceId = this.GetHashCode();
			this.request = request;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00056FC4 File Offset: 0x000551C4
		public override IAsyncResult BeginProxyWebRequest(IService service, MailboxData[] mailboxArray, AsyncCallback callback, object asyncState)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.traceId, "Entering GetMessageTrackingApplication.BeginProxyWebRequest", new object[0]);
			if (Testability.WebServiceCredentials != null)
			{
				service.Credentials = Testability.WebServiceCredentials;
				ServicePointManager.ServerCertificateValidationCallback = ((object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => true);
			}
			service.RequestServerVersionValue.Version = VersionConverter.GetRdExchangeVersionType(service.ServiceVersion);
			return service.BeginGetMessageTrackingReport(this.request.PrepareRDRequest(service.ServiceVersion), callback, asyncState);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00057050 File Offset: 0x00055250
		public override void EndProxyWebRequest(ProxyWebRequest proxyWebRequest, QueryList queryList, IService service, IAsyncResult asyncResult)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.traceId, "Entering GetMessageTrackingApplication.EndProxyWebRequest", new object[0]);
			GetMessageTrackingReportResponseMessageType getMessageTrackingReportResponseMessageType = service.EndGetMessageTrackingReport(asyncResult);
			if (getMessageTrackingReportResponseMessageType == null)
			{
				base.HandleNullResponse(proxyWebRequest);
				return;
			}
			int hashCode = proxyWebRequest.GetHashCode();
			if (getMessageTrackingReportResponseMessageType.ResponseClass != ResponseClassType.Success)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<object, string, string>(this.traceId, "{0}: GetMTR proxy web request returned {1} and response code {2}", TraceContext.Get(), Names<ResponseClassType>.Map[(int)getMessageTrackingReportResponseMessageType.ResponseClass], getMessageTrackingReportResponseMessageType.ResponseCode);
			}
			this.ProcessResponseMessages(hashCode, queryList, getMessageTrackingReportResponseMessageType);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x000570D0 File Offset: 0x000552D0
		public override string GetParameterDataString()
		{
			return this.traceId.ToString() + " " + this.request.WrappedRequest.MessageTrackingReportId;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x000570F7 File Offset: 0x000552F7
		public override BaseQueryResult CreateQueryResult(LocalizedException exception)
		{
			return new GetMessageTrackingBaseQueryResult(exception);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x000570FF File Offset: 0x000552FF
		public override BaseQuery CreateFromUnknown(RecipientData recipientData, LocalizedException exception)
		{
			return GetMessageTrackingBaseQuery.CreateFromUnknown(recipientData, exception);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00057108 File Offset: 0x00055308
		public override BaseQuery CreateFromIndividual(RecipientData recipientData)
		{
			return GetMessageTrackingBaseQuery.CreateFromIndividual(recipientData);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00057110 File Offset: 0x00055310
		public override BaseQuery CreateFromIndividual(RecipientData recipientData, LocalizedException exception)
		{
			return GetMessageTrackingBaseQuery.CreateFromIndividual(recipientData, exception);
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x00057119 File Offset: 0x00055319
		public override ThreadCounter Worker
		{
			get
			{
				return GetMessageTrackingApplication.MessageTrackingWorker;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00057120 File Offset: 0x00055320
		public override ThreadCounter IOCompletion
		{
			get
			{
				return GetMessageTrackingApplication.MessageTrackingIOCompletion;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x00057127 File Offset: 0x00055327
		public override LocalizedString Name
		{
			get
			{
				return Strings.MessageTrackingApplicationName;
			}
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00057130 File Offset: 0x00055330
		private void ProcessResponseMessages(int traceId, QueryList queryList, GetMessageTrackingReportResponseMessageType response)
		{
			if (response == null)
			{
				Application.ProxyWebRequestTracer.TraceError((long)traceId, "{0}: Proxy web request returned NULL GetMessageTrackingReportResponseMessageType", new object[]
				{
					TraceContext.Get()
				});
				return;
			}
			foreach (BaseQuery baseQuery in ((IEnumerable<BaseQuery>)queryList))
			{
				GetMessageTrackingBaseQuery getMessageTrackingBaseQuery = (GetMessageTrackingBaseQuery)baseQuery;
				getMessageTrackingBaseQuery.SetResultOnFirstCall(new GetMessageTrackingBaseQueryResult
				{
					Response = response
				});
			}
		}

		// Token: 0x04000CC1 RID: 3265
		private int traceId;

		// Token: 0x04000CC2 RID: 3266
		private GetMessageTrackingReportRequestTypeWrapper request;

		// Token: 0x04000CC3 RID: 3267
		public static readonly ThreadCounter MessageTrackingWorker = new ThreadCounter();

		// Token: 0x04000CC4 RID: 3268
		public static readonly ThreadCounter MessageTrackingIOCompletion = new ThreadCounter();
	}
}

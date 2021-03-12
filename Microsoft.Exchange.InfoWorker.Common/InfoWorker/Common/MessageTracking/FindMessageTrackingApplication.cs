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
	// Token: 0x020002A3 RID: 675
	internal sealed class FindMessageTrackingApplication : MessageTrackingApplication
	{
		// Token: 0x060012D9 RID: 4825 RVA: 0x00056A78 File Offset: 0x00054C78
		public FindMessageTrackingApplication(FindMessageTrackingReportRequestTypeWrapper request, ExchangeVersion ewsRequestedVersion) : base(false, false, ewsRequestedVersion)
		{
			this.traceId = this.GetHashCode();
			this.request = request;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00056A9C File Offset: 0x00054C9C
		public override IAsyncResult BeginProxyWebRequest(IService service, MailboxData[] mailboxArray, AsyncCallback callback, object asyncState)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.traceId, "Entering FindMessageTrackingApplication.BeginProxyWebRequest", new object[0]);
			if (Testability.WebServiceCredentials != null)
			{
				service.Credentials = Testability.WebServiceCredentials;
				ServicePointManager.ServerCertificateValidationCallback = ((object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => true);
			}
			service.RequestServerVersionValue.Version = VersionConverter.GetRdExchangeVersionType(service.ServiceVersion);
			return service.BeginFindMessageTrackingReport(this.request.PrepareRDRequest(service.ServiceVersion), callback, asyncState);
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00056B28 File Offset: 0x00054D28
		public override void EndProxyWebRequest(ProxyWebRequest proxyWebRequest, QueryList queryList, IService service, IAsyncResult asyncResult)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.traceId, "Entering FindMessageTrackingApplication.EndProxyWebRequest", new object[0]);
			FindMessageTrackingReportResponseMessageType findMessageTrackingReportResponseMessageType = service.EndFindMessageTrackingReport(asyncResult);
			if (findMessageTrackingReportResponseMessageType == null)
			{
				base.HandleNullResponse(proxyWebRequest);
				return;
			}
			if (findMessageTrackingReportResponseMessageType.ResponseClass != ResponseClassType.Success)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<object, string, string>(this.traceId, "{0}: FindMTR proxy web request returned {1} and response code {2}", TraceContext.Get(), Names<ResponseClassType>.Map[(int)findMessageTrackingReportResponseMessageType.ResponseClass], findMessageTrackingReportResponseMessageType.ResponseCode);
			}
			this.ProcessResponseMessages(this.traceId, queryList, findMessageTrackingReportResponseMessageType);
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00056BA6 File Offset: 0x00054DA6
		public override string GetParameterDataString()
		{
			return this.traceId.ToString() + " " + this.request.WrappedRequest.MessageId;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00056BCD File Offset: 0x00054DCD
		public override BaseQueryResult CreateQueryResult(LocalizedException exception)
		{
			return new FindMessageTrackingBaseQueryResult(exception);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00056BD5 File Offset: 0x00054DD5
		public override BaseQuery CreateFromUnknown(RecipientData recipientData, LocalizedException exception)
		{
			return FindMessageTrackingBaseQuery.CreateFromUnknown(recipientData, exception);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00056BDE File Offset: 0x00054DDE
		public override BaseQuery CreateFromIndividual(RecipientData recipientData)
		{
			return FindMessageTrackingBaseQuery.CreateFromIndividual(recipientData);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00056BE6 File Offset: 0x00054DE6
		public override BaseQuery CreateFromIndividual(RecipientData recipientData, LocalizedException exception)
		{
			return FindMessageTrackingBaseQuery.CreateFromIndividual(recipientData, exception);
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x00056BEF File Offset: 0x00054DEF
		public override ThreadCounter Worker
		{
			get
			{
				return FindMessageTrackingApplication.MessageTrackingWorker;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x00056BF6 File Offset: 0x00054DF6
		public override ThreadCounter IOCompletion
		{
			get
			{
				return FindMessageTrackingApplication.MessageTrackingIOCompletion;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x00056BFD File Offset: 0x00054DFD
		public override LocalizedString Name
		{
			get
			{
				return Strings.MessageTrackingApplicationName;
			}
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00056C04 File Offset: 0x00054E04
		private void ProcessResponseMessages(int traceId, QueryList queryList, FindMessageTrackingReportResponseMessageType response)
		{
			if (response == null)
			{
				Application.ProxyWebRequestTracer.TraceError((long)traceId, "{0}: Proxy web request returned NULL FindMessageTrackingReportResponseMessageType", new object[]
				{
					TraceContext.Get()
				});
				return;
			}
			foreach (BaseQuery baseQuery in ((IEnumerable<BaseQuery>)queryList))
			{
				FindMessageTrackingBaseQuery findMessageTrackingBaseQuery = (FindMessageTrackingBaseQuery)baseQuery;
				findMessageTrackingBaseQuery.SetResultOnFirstCall(new FindMessageTrackingBaseQueryResult
				{
					Response = response
				});
			}
		}

		// Token: 0x04000CB4 RID: 3252
		private int traceId;

		// Token: 0x04000CB5 RID: 3253
		private FindMessageTrackingReportRequestTypeWrapper request;

		// Token: 0x04000CB6 RID: 3254
		public static readonly ThreadCounter MessageTrackingWorker = new ThreadCounter();

		// Token: 0x04000CB7 RID: 3255
		public static readonly ThreadCounter MessageTrackingIOCompletion = new ThreadCounter();
	}
}

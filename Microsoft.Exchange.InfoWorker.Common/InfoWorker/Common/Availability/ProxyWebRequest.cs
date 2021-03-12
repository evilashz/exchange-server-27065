using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.SoapWebClient;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000B5 RID: 181
	internal sealed class ProxyWebRequest : AsyncWebRequest, IDisposable
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x00010D4C File Offset: 0x0000EF4C
		internal ProxyWebRequest(Application application, ClientContext clientContext, RequestType requestType, RequestLogger requestLogger, QueryList queryList, TargetServerVersion targetVersion, ProxyAuthenticator proxyAuthenticator, WebServiceUri webServiceUri, UriSource source) : base(application, clientContext, requestLogger, "ProxyWebRequest")
		{
			this.proxyAuthenticator = proxyAuthenticator;
			this.url = webServiceUri.Uri.OriginalString;
			this.protocol = webServiceUri.Protocol;
			this.source = source;
			this.queryList = queryList;
			this.requestType = requestType;
			this.service = application.CreateService(webServiceUri, targetVersion, requestType);
			if (proxyAuthenticator.AuthenticatorType == AuthenticatorType.WSSecurity || proxyAuthenticator.AuthenticatorType == AuthenticatorType.OAuth)
			{
				if (webServiceUri.ServerVersion >= Globals.E15Version && clientContext.RequestSchemaVersion >= ExchangeVersionType.Exchange2012)
				{
					this.service.RequestServerVersionValue = new RequestServerVersion
					{
						Version = ExchangeVersionType.Exchange2012
					};
				}
				else
				{
					this.service.RequestServerVersionValue = new RequestServerVersion
					{
						Version = ExchangeVersionType.Exchange2009
					};
				}
			}
			else if (targetVersion >= TargetServerVersion.E15 && clientContext.RequestSchemaVersion == ExchangeVersionType.Exchange2012)
			{
				this.service.RequestServerVersionValue = new RequestServerVersion
				{
					Version = ExchangeVersionType.Exchange2012
				};
			}
			this.service.CookieContainer = new CookieContainer();
			this.service.requestTypeValue = new RequestTypeHeader();
			if (requestType == RequestType.CrossSite || requestType == RequestType.IntraSite)
			{
				if (Configuration.BypassProxyForCrossSiteRequests)
				{
					this.service.Proxy = new WebProxy();
				}
				this.service.requestTypeValue.RequestType = ProxyRequestType.CrossSite;
				if (requestType == RequestType.CrossSite)
				{
					this.failedCounter = PerformanceCounters.CrossSiteCalendarFailuresPerSecond;
					this.averageProcessingTimeCounter = PerformanceCounters.AverageCrossSiteFreeBusyRequestProcessingTime;
					this.averageProcessingTimeCounterBase = PerformanceCounters.AverageCrossSiteFreeBusyRequestProcessingTimeBase;
					this.requestStatisticsType = RequestStatisticsType.CrossSiteProxy;
				}
				else
				{
					this.failedCounter = PerformanceCounters.IntraSiteProxyFreeBusyFailuresPerSecond;
					this.averageProcessingTimeCounter = PerformanceCounters.AverageIntraSiteProxyFreeBusyRequestProcessingTime;
					this.averageProcessingTimeCounterBase = PerformanceCounters.AverageIntraSiteProxyFreeBusyRequestProcessingTimeBase;
					this.requestStatisticsType = RequestStatisticsType.IntraSiteProxy;
				}
			}
			else
			{
				bool flag = false;
				if (Configuration.BypassProxyForCrossForestRequests)
				{
					this.service.Proxy = new WebProxy();
					flag = true;
				}
				this.service.requestTypeValue.RequestType = ProxyRequestType.CrossForest;
				if (requestType == RequestType.FederatedCrossForest)
				{
					if (proxyAuthenticator.AuthenticatorType == AuthenticatorType.OAuth)
					{
						this.failedCounter = PerformanceCounters.FederatedByOAuthFreeBusyFailuresPerSecond;
						this.averageProcessingTimeCounter = PerformanceCounters.AverageFederatedByOAuthFreeBusyRequestProcessingTime;
						this.averageProcessingTimeCounterBase = PerformanceCounters.AverageFederatedByOAuthFreeBusyRequestProcessingTimeBase;
						this.requestStatisticsType = RequestStatisticsType.OAuthProxy;
						this.service.requestTypeValue = null;
					}
					else
					{
						this.failedCounter = PerformanceCounters.FederatedFreeBusyFailuresPerSecond;
						this.averageProcessingTimeCounter = PerformanceCounters.AverageFederatedFreeBusyRequestProcessingTime;
						this.averageProcessingTimeCounterBase = PerformanceCounters.AverageFederatedFreeBusyRequestProcessingTimeBase;
						this.requestStatisticsType = RequestStatisticsType.FederatedProxy;
					}
					if (!flag)
					{
						Server localServer = LocalServerCache.LocalServer;
						if (localServer != null && localServer.InternetWebProxy != null)
						{
							ProxyWebRequest.ProxyWebRequestTracer.TraceDebug<object, Uri>((long)this.GetHashCode(), "{0}: Using custom InternetWebProxy {1}", TraceContext.Get(), localServer.InternetWebProxy);
							this.service.Proxy = new WebProxy(localServer.InternetWebProxy);
						}
					}
				}
				else
				{
					this.failedCounter = PerformanceCounters.CrossForestCalendarFailuresPerSecond;
					this.averageProcessingTimeCounter = PerformanceCounters.AverageCrossForestFreeBusyRequestProcessingTime;
					this.averageProcessingTimeCounterBase = PerformanceCounters.AverageCrossForestFreeBusyRequestProcessingTimeBase;
					this.requestStatisticsType = RequestStatisticsType.CrossForestProxy;
				}
			}
			if (!Configuration.DisableGzipForProxyRequests)
			{
				this.service.EnableDecompression = true;
			}
			string address = this.queryList[0].Email.Address;
			ProxyWebRequest.ProxyWebRequestTracer.TraceDebug<object, string, WebServiceUri>((long)this.GetHashCode(), "{0}: Adding Anchor Mailbox Header {1} to the request to {2}.", TraceContext.Get(), address, webServiceUri);
			this.service.HttpHeaders.Add("X-AnchorMailbox", address);
			if (!string.IsNullOrEmpty(base.ClientContext.RequestId))
			{
				this.service.HttpHeaders.Add("client-request-id", base.ClientContext.RequestId);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x000110D2 File Offset: 0x0000F2D2
		internal string Url
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000110DC File Offset: 0x0000F2DC
		protected override IAsyncResult BeginInvoke()
		{
			this.service.UserAgent = this.GetUserAgent();
			MailboxData[] array = new MailboxData[this.queryList.Count];
			for (int i = 0; i < this.queryList.Count; i++)
			{
				this.queryList[i].Target = this.Url;
				array[i] = new MailboxData(this.queryList[i].Email);
			}
			ProxyWebRequest.ProxyWebRequestTracer.TraceDebug<object, RequestType, string>((long)this.GetHashCode(), "{0}: Sending request {1} to {2}", TraceContext.Get(), this.requestType, this.url);
			this.serverRequestId = Microsoft.Exchange.Diagnostics.Trace.TraceCasStart(CasTraceEventType.Availability);
			if (this.service.SupportsProxyAuthentication)
			{
				this.proxyAuthenticator.Authenticate((CustomSoapHttpClientProtocol)this.service);
			}
			this.webServiceCallTimer = Stopwatch.StartNew();
			Stopwatch stopwatch = Stopwatch.StartNew();
			IAsyncResult result = base.Application.BeginProxyWebRequest(this.service, array, new AsyncCallback(base.Complete), null);
			stopwatch.Stop();
			this.queryList.LogLatency("PWRBI", stopwatch.ElapsedMilliseconds);
			return result;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x000111F5 File Offset: 0x0000F3F5
		protected override bool ShouldCallBeginInvokeByNewThread
		{
			get
			{
				return this.proxyAuthenticator.AuthenticatorType == AuthenticatorType.OAuth;
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00011205 File Offset: 0x0000F405
		public override void Abort()
		{
			base.Abort();
			if (this.service != null)
			{
				this.service.Abort();
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00011220 File Offset: 0x0000F420
		protected override bool IsImpersonating
		{
			get
			{
				return this.proxyAuthenticator.AuthenticatorType == AuthenticatorType.NetworkCredentials;
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00011230 File Offset: 0x0000F430
		protected override void EndInvoke(IAsyncResult asyncResult)
		{
			try
			{
				ProxyWebRequest.FaultInjectionTracer.TraceTest(2607164733U);
				base.Application.EndProxyWebRequest(this, this.queryList, this.service, asyncResult);
				if (this.requestType == RequestType.CrossForest || this.requestType == RequestType.FederatedCrossForest)
				{
					RemoteServiceUriCache.Validate(this.url);
				}
				this.IncrementPerfCounter();
			}
			finally
			{
				if (!base.Aborted)
				{
					this.queryList.LogLatency("PWRC", this.webServiceCallTimer.ElapsedMilliseconds);
				}
				this.TraceCompleteRequest();
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000112C4 File Offset: 0x0000F4C4
		private void TraceCompleteRequest()
		{
			if (ETWTrace.ShouldTraceCasStop(this.serverRequestId))
			{
				string serverAddress;
				string clientOperation;
				this.GetUrlData(out serverAddress, out clientOperation);
				string serviceProviderOperationData = this.GetServiceProviderOperationData();
				Microsoft.Exchange.Diagnostics.Trace.TraceCasStop(CasTraceEventType.Availability, this.serverRequestId, 0, 0, serverAddress, TraceContext.Get(), "ProxyWebRequest::CompleteRequest", serviceProviderOperationData, clientOperation);
			}
			this.serverRequestId = Guid.Empty;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00011318 File Offset: 0x0000F518
		protected override void HandleException(Exception exception)
		{
			if (ProxyWebRequest.ProxyWebRequestTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				ProxyWebRequest.ProxyWebRequestTracer.TraceError<object, Exception, ProxyAuthenticator>((long)this.GetHashCode(), "{0}: Exception occurred while completing proxy web request. Exception info is {1}. Caller SIDs: {2}", TraceContext.Get(), exception, this.proxyAuthenticator);
			}
			if (this.requestType == RequestType.CrossForest || this.requestType == RequestType.FederatedCrossForest)
			{
				Exception ex = exception;
				if (exception is GrayException)
				{
					ex = exception.InnerException;
				}
				if (ex is UriFormatException)
				{
					RemoteServiceUriCache.Invalidate(this.url);
				}
				if (HttpWebRequestExceptionHandler.IsConnectionException(ex, ProxyWebRequest.ProxyWebRequestTracer))
				{
					RemoteServiceUriCache.Invalidate(this.url);
				}
			}
			ProxyWebRequestProcessingException ex2 = this.GenerateException(exception);
			this.SetExceptionInResultList(ex2);
			Globals.AvailabilityLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_ProxyWebRequestFailed, this.url, new object[]
			{
				Globals.ProcessId,
				this,
				this.proxyAuthenticator,
				ex2
			});
			this.failedCounter.Increment();
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000113F9 File Offset: 0x0000F5F9
		private ProxyWebRequestProcessingException GenerateException(Exception exception)
		{
			return new ProxyWebRequestProcessingException(Strings.descProxyRequestFailed, exception);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00011406 File Offset: 0x0000F606
		private void SetExceptionInResultList(LocalizedException exception)
		{
			ProxyWebRequest.ProxyWebRequestTracer.TraceDebug<object, LocalizedException>((long)this.GetHashCode(), "{0}: Setting exception to all queries: {1}", TraceContext.Get(), exception);
			this.queryList.SetResultInAllQueries(base.Application.CreateQueryResult(exception));
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001143C File Offset: 0x0000F63C
		private string GetServiceProviderOperationData()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Mailbox list = ");
			foreach (BaseQuery baseQuery in ((IEnumerable<BaseQuery>)this.queryList))
			{
				stringBuilder.Append((baseQuery.Email != null) ? baseQuery.Email.ToString() : "<null>");
				stringBuilder.Append(", ");
			}
			stringBuilder.AppendFormat(base.Application.GetParameterDataString(), new object[0]);
			return stringBuilder.ToString();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000114E0 File Offset: 0x0000F6E0
		private void GetUrlData(out string serverName, out string clientOperation)
		{
			serverName = string.Empty;
			clientOperation = string.Empty;
			if (!string.IsNullOrEmpty(this.url))
			{
				try
				{
					Uri uri = new Uri(this.url);
					serverName = uri.Host;
					clientOperation = uri.PathAndQuery;
				}
				catch (UriFormatException ex)
				{
					serverName = this.url;
					clientOperation = this.url;
					ProxyWebRequest.ProxyWebRequestTracer.TraceDebug(0L, ex.ToString());
				}
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001155C File Offset: 0x0000F75C
		public void Dispose()
		{
			if (this.service != null)
			{
				this.service.Dispose();
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00011574 File Offset: 0x0000F774
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"ProxyWebRequest ",
				this.requestType,
				" from ",
				TraceContext.Get(),
				" to ",
				this.url
			});
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000115C8 File Offset: 0x0000F7C8
		private string GetUserAgent()
		{
			string type = string.Empty;
			switch (this.requestType)
			{
			case RequestType.IntraSite:
			case RequestType.CrossSite:
				type = "CrossSite";
				break;
			case RequestType.CrossForest:
			case RequestType.FederatedCrossForest:
				type = "CrossForest";
				break;
			}
			UserAgent userAgent = new UserAgent("ASProxy", type, (this.source == UriSource.Directory) ? "Directory" : "EmailDomain", this.protocol);
			return userAgent.ToString();
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00011638 File Offset: 0x0000F838
		private void IncrementPerfCounter()
		{
			this.webServiceCallTimer.Stop();
			this.averageProcessingTimeCounter.IncrementBy(this.webServiceCallTimer.ElapsedTicks);
			this.averageProcessingTimeCounterBase.Increment();
			base.RequestLogger.Add(RequestStatistics.Create(this.requestStatisticsType, this.webServiceCallTimer.ElapsedMilliseconds, this.queryList.Count, this.url));
		}

		// Token: 0x04000279 RID: 633
		private const CasTraceEventType AvailabilityTraceEventType = CasTraceEventType.Availability;

		// Token: 0x0400027A RID: 634
		public const string ProxyWebRequestBeginInvokeMarker = "PWRBI";

		// Token: 0x0400027B RID: 635
		public const string ProxyWebRequestCompleteMarker = "PWRC";

		// Token: 0x0400027C RID: 636
		internal const string AnchorMailboxHeader = "X-AnchorMailbox";

		// Token: 0x0400027D RID: 637
		private static readonly Microsoft.Exchange.Diagnostics.Trace ProxyWebRequestTracer = ExTraceGlobals.ProxyWebRequestTracer;

		// Token: 0x0400027E RID: 638
		private static readonly FaultInjectionTrace FaultInjectionTracer = ExTraceGlobals.FaultInjectionTracer;

		// Token: 0x0400027F RID: 639
		private Guid serverRequestId = Guid.Empty;

		// Token: 0x04000280 RID: 640
		private UriSource source = UriSource.Unknown;

		// Token: 0x04000281 RID: 641
		private string protocol = string.Empty;

		// Token: 0x04000282 RID: 642
		private QueryList queryList;

		// Token: 0x04000283 RID: 643
		private RequestType requestType;

		// Token: 0x04000284 RID: 644
		private ProxyAuthenticator proxyAuthenticator;

		// Token: 0x04000285 RID: 645
		private string url;

		// Token: 0x04000286 RID: 646
		private IService service;

		// Token: 0x04000287 RID: 647
		private Stopwatch webServiceCallTimer;

		// Token: 0x04000288 RID: 648
		private RequestStatisticsType requestStatisticsType;

		// Token: 0x04000289 RID: 649
		private ExPerformanceCounter failedCounter;

		// Token: 0x0400028A RID: 650
		private ExPerformanceCounter averageProcessingTimeCounter;

		// Token: 0x0400028B RID: 651
		private ExPerformanceCounter averageProcessingTimeCounterBase;
	}
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000774 RID: 1908
	internal abstract class BaseExceptionAnalyzer : IExceptionAnalyzer
	{
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060025C0 RID: 9664 RVA: 0x0004F60C File Offset: 0x0004D80C
		protected virtual bool AffinityCheck
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x0004F610 File Offset: 0x0004D810
		public BaseExceptionAnalyzer(Dictionary<string, RequestTarget> hostNameSourceMapping)
		{
			if (hostNameSourceMapping != null)
			{
				this.hostNameSourceMapping = new ConcurrentDictionary<string, RequestTarget>();
				foreach (string key in hostNameSourceMapping.Keys)
				{
					this.AddHostNameMapping(key, hostNameSourceMapping[key]);
				}
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060025C2 RID: 9666
		protected abstract Dictionary<RequestTarget, Dictionary<FailureReason, FailingComponent>> ComponentMatrix { get; }

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x060025C3 RID: 9667
		protected abstract FailingComponent DefaultComponent { get; }

		// Token: 0x060025C4 RID: 9668 RVA: 0x0004F680 File Offset: 0x0004D880
		private static HashSet<WebExceptionStatus> GetNetworkRelatedErrors()
		{
			return new HashSet<WebExceptionStatus>
			{
				WebExceptionStatus.ConnectFailure,
				WebExceptionStatus.ConnectionClosed,
				WebExceptionStatus.KeepAliveFailure,
				WebExceptionStatus.NameResolutionFailure,
				WebExceptionStatus.PipelineFailure,
				WebExceptionStatus.ReceiveFailure,
				WebExceptionStatus.RequestProhibitedByProxy,
				WebExceptionStatus.SecureChannelFailure,
				WebExceptionStatus.SendFailure,
				WebExceptionStatus.TrustFailure,
				WebExceptionStatus.Timeout
			};
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x0004F6F1 File Offset: 0x0004D8F1
		public static bool IsNetworkRelatedError(WebExceptionStatus status)
		{
			return BaseExceptionAnalyzer.NetworkRelatedErrors.Contains(status);
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x0004F700 File Offset: 0x0004D900
		public static Dictionary<WebExceptionStatus, int> GetRetriableErrors()
		{
			return new Dictionary<WebExceptionStatus, int>
			{
				{
					WebExceptionStatus.Timeout,
					1
				}
			};
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x0004F720 File Offset: 0x0004D920
		public static Dictionary<HttpStatusCode, int> GetStatusCodes()
		{
			return new Dictionary<HttpStatusCode, int>
			{
				{
					(HttpStatusCode)449,
					3
				}
			};
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x0004F740 File Offset: 0x0004D940
		public void Analyze(TestId currentTestStep, HttpWebRequestWrapper request, HttpWebResponseWrapper response, Exception exception, IResponseTracker responseTracker, Action<ScenarioException> trackingDelegate)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			if (exception is ScenarioException)
			{
				throw exception;
			}
			exception = this.PreProcessException(exception, request, response, responseTracker);
			RequestTarget requestTarget = this.GetRequestTarget(request);
			FailureReason failureReason = this.GetFailureReason(exception, request, response);
			FailingComponent failingComponent = this.GetFailingComponent(requestTarget, failureReason);
			string text = null;
			HttpWebResponseWrapperException ex = exception as HttpWebResponseWrapperException;
			if (ex != null)
			{
				text = ex.ExceptionHint;
				if (requestTarget == RequestTarget.Ecp && failureReason == FailureReason.RequestTimeout)
				{
					ExDateTime now = ExDateTime.Now;
					text = "Execution time (ms): " + (now - request.SentTime).TotalMilliseconds.ToString() + Environment.NewLine + text;
				}
			}
			ScenarioException ex2 = new ScenarioException(string.Empty, exception, requestTarget, failureReason, failingComponent, text);
			trackingDelegate(ex2);
			throw ex2;
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x0004F808 File Offset: 0x0004DA08
		private Exception PreProcessException(Exception exception, HttpWebRequestWrapper request, HttpWebResponseWrapper response, IResponseTracker responseTracker)
		{
			HttpWebResponseWrapperException ex = exception as HttpWebResponseWrapperException;
			if (ex != null && ex.Status != null && ex.Status.Value == WebExceptionStatus.TrustFailure)
			{
				SslError sslError = SslValidationHelper.ReadSslError(request.ConnectionGroupName);
				if (sslError != null)
				{
					return new SslNegotiationException(sslError.Description, request, sslError, exception);
				}
			}
			if (ex is MissingKeywordException && this.AffinityCheck)
			{
				Exception ex2 = this.CheckAffinityBreak(responseTracker, ex);
				if (ex2 != null)
				{
					return ex2;
				}
			}
			return exception;
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x0004F880 File Offset: 0x0004DA80
		private Exception CheckAffinityBreak(IResponseTracker responseTracker, HttpWebResponseWrapperException httpWrapperException)
		{
			if (responseTracker == null)
			{
				return null;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (ResponseTrackerItem responseTrackerItem in responseTracker.Items)
			{
				if (responseTrackerItem.IsE14CasServer.Equals(true))
				{
					if (dictionary.ContainsKey(responseTrackerItem.TargetHost))
					{
						string text = dictionary[responseTrackerItem.TargetHost];
						if (!text.Equals(responseTrackerItem.RespondingServer, StringComparison.OrdinalIgnoreCase))
						{
							return new BrokenAffinityException(string.Format("Affinity has been broken for hostname {0}. Both servers {1} and {2} responded through this VIP ({3}). Check with networking team for broken affinity rules in the LB.", new object[]
							{
								responseTrackerItem.TargetHost,
								text,
								responseTrackerItem.RespondingServer,
								responseTrackerItem.TargetIpAddress
							}), httpWrapperException.Request, httpWrapperException.Response, responseTrackerItem.TargetHost, text, responseTrackerItem.RespondingServer, httpWrapperException);
						}
					}
					else
					{
						dictionary.Add(responseTrackerItem.TargetHost, responseTrackerItem.RespondingServer);
					}
				}
			}
			return null;
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x0004F994 File Offset: 0x0004DB94
		public ScenarioException GetExceptionForScenarioTimeout(TimeSpan maxAllowedTime, TimeSpan totalTime, ResponseTrackerItem item)
		{
			RequestTarget failureSource = (RequestTarget)Enum.Parse(typeof(RequestTarget), item.TargetType);
			FailureReason failureReason = FailureReason.ScenarioTimeout;
			FailingComponent failingComponentForScenarioTimeout = this.GetFailingComponentForScenarioTimeout(failureSource, failureReason, item);
			ScenarioTimeoutException ex = new ScenarioTimeoutException(MonitoringWebClientStrings.ScenarioTimedOut(maxAllowedTime.TotalSeconds, totalTime.TotalSeconds), (item.Response != null) ? item.Response.Request : null, item.Response);
			return new ScenarioException(ex.Message, ex, failureSource, failureReason, failingComponentForScenarioTimeout, ex.ExceptionHint);
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x0004FA34 File Offset: 0x0004DC34
		public virtual RequestTarget GetRequestTarget(HttpWebRequestWrapper request)
		{
			if (request == null)
			{
				return RequestTarget.Monitor;
			}
			if (this.hostNameSourceMapping == null)
			{
				return RequestTarget.Unknown;
			}
			if (request.StepId == TestId.MeasureClientLatency)
			{
				return RequestTarget.LocalClient;
			}
			string text = this.hostNameSourceMapping.Keys.FirstOrDefault((string hostKey) => request.RequestUri.Host.EndsWith(hostKey, StringComparison.OrdinalIgnoreCase));
			if (!string.IsNullOrEmpty(text))
			{
				return this.hostNameSourceMapping[text];
			}
			if (AdfsLogonPage.IsAdfsRequest(request))
			{
				this.AddHostNameMapping(request.RequestUri.Host, RequestTarget.Adfs);
				return RequestTarget.Adfs;
			}
			return RequestTarget.Unknown;
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x0004FAD0 File Offset: 0x0004DCD0
		public virtual List<string> GetHostNames(RequestTarget requestTarget)
		{
			List<string> list = new List<string>();
			if (this.hostNameSourceMapping != null)
			{
				foreach (string text in this.hostNameSourceMapping.Keys)
				{
					if (this.hostNameSourceMapping[text] == requestTarget)
					{
						list.Add(text);
					}
				}
			}
			return list;
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x0004FB40 File Offset: 0x0004DD40
		protected virtual FailingComponent GetFailingComponentForScenarioTimeout(RequestTarget failureSource, FailureReason failureReason, ResponseTrackerItem item)
		{
			return this.GetFailingComponent(failureSource, failureReason);
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x0004FB4A File Offset: 0x0004DD4A
		protected FailingComponent GetFailingComponent(RequestTarget failureSource, FailureReason failureReason)
		{
			if (this.ComponentMatrix.ContainsKey(failureSource) && this.ComponentMatrix[failureSource].ContainsKey(failureReason))
			{
				return this.ComponentMatrix[failureSource][failureReason];
			}
			return this.DefaultComponent;
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x0004FB88 File Offset: 0x0004DD88
		public virtual HttpWebResponseWrapperException VerifyResponse(HttpWebRequestWrapper request, HttpWebResponseWrapper response, CafeErrorPageValidationRules cafeErrorPageValidationRules)
		{
			CafeErrorPage cafeErrorPage;
			if (CafeErrorPage.TryParse(response, cafeErrorPageValidationRules, out cafeErrorPage))
			{
				return new CafeErrorPageException(MonitoringWebClientStrings.CafeErrorPage, request, response, cafeErrorPage);
			}
			return null;
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x0004FBB0 File Offset: 0x0004DDB0
		protected virtual FailureReason GetFailureReason(Exception exception, HttpWebRequestWrapper request, HttpWebResponseWrapper response)
		{
			if (exception is PassiveDatabaseException)
			{
				return FailureReason.PassiveDatabase;
			}
			if (exception is SslNegotiationException)
			{
				return FailureReason.SslNegotiation;
			}
			if (exception is BrokenAffinityException)
			{
				return FailureReason.BrokenAffinity;
			}
			if (exception is LogonException)
			{
				LogonException ex = exception as LogonException;
				switch (ex.LogonErrorType)
				{
				case LogonErrorType.BadUserNameOrPassword:
					return FailureReason.BadUserNameOrPassword;
				case LogonErrorType.AccountLocked:
					return FailureReason.AccountLocked;
				default:
					return FailureReason.LogonError;
				}
			}
			else
			{
				if (exception is TaskCanceledException)
				{
					return FailureReason.CancelationRequested;
				}
				if (exception is CafeErrorPageException)
				{
					CafeErrorPageException ex2 = exception as CafeErrorPageException;
					return ex2.CafeErrorPage.FailureReason;
				}
				if (exception is HttpWebResponseWrapperException)
				{
					HttpWebResponseWrapperException ex3 = exception as HttpWebResponseWrapperException;
					if (ex3 != null)
					{
						WebExceptionStatus valueOrDefault = ex3.Status.GetValueOrDefault();
						WebExceptionStatus? webExceptionStatus;
						if (webExceptionStatus != null)
						{
							switch (valueOrDefault)
							{
							case WebExceptionStatus.NameResolutionFailure:
							case WebExceptionStatus.ProxyNameResolutionFailure:
								return FailureReason.NameResolution;
							case WebExceptionStatus.ConnectFailure:
							case WebExceptionStatus.ReceiveFailure:
							case WebExceptionStatus.SendFailure:
							case WebExceptionStatus.PipelineFailure:
							case WebExceptionStatus.ProtocolError:
							case WebExceptionStatus.ConnectionClosed:
							case WebExceptionStatus.TrustFailure:
							case WebExceptionStatus.SecureChannelFailure:
							case WebExceptionStatus.ServerProtocolViolation:
							case WebExceptionStatus.KeepAliveFailure:
							case WebExceptionStatus.RequestProhibitedByProxy:
								return FailureReason.NetworkConnection;
							case WebExceptionStatus.RequestCanceled:
								return FailureReason.RequestTimeout;
							case WebExceptionStatus.Timeout:
								return FailureReason.ConnectionTimeout;
							}
						}
					}
					if (exception is UnexpectedStatusCodeException)
					{
						return FailureReason.UnexpectedHttpResponseCode;
					}
					if (exception is MissingKeywordException)
					{
						return FailureReason.MissingKeyword;
					}
				}
				if (exception is NameResolutionException)
				{
					return FailureReason.NameResolution;
				}
				if (exception is SocketException)
				{
					return FailureReason.NetworkConnection;
				}
				if (exception is IOException)
				{
					return FailureReason.NetworkConnection;
				}
				return FailureReason.Unknown;
			}
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x0004FCF9 File Offset: 0x0004DEF9
		private void AddHostNameMapping(string key, RequestTarget value)
		{
			this.hostNameSourceMapping.AddOrUpdate(key, value, (string addKey, RequestTarget originalValue) => originalValue);
		}

		// Token: 0x040022EF RID: 8943
		private ConcurrentDictionary<string, RequestTarget> hostNameSourceMapping;

		// Token: 0x040022F0 RID: 8944
		private static readonly HashSet<WebExceptionStatus> NetworkRelatedErrors = BaseExceptionAnalyzer.GetNetworkRelatedErrors();
	}
}

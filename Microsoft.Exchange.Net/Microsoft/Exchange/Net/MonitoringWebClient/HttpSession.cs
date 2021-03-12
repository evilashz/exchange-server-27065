using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007BD RID: 1981
	internal class HttpSession : IHttpSession
	{
		// Token: 0x0600288A RID: 10378 RVA: 0x0005613C File Offset: 0x0005433C
		static HttpSession()
		{
			Array values = Enum.GetValues(typeof(HttpStatusCode));
			HttpSession.AllHttpStatusCodes = new HttpStatusCode[values.Length];
			int num = 0;
			foreach (object obj in values)
			{
				HttpSession.AllHttpStatusCodes[num] = (HttpStatusCode)obj;
				num++;
			}
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x00056230 File Offset: 0x00054430
		public HttpSession(IRequestAdapter requestAdapter, IExceptionAnalyzer exceptionAnalyzer, IResponseTracker responseTracker)
		{
			if (requestAdapter == null)
			{
				throw new ArgumentNullException("requestAdapter");
			}
			if (exceptionAnalyzer == null)
			{
				throw new ArgumentNullException("exceptionAnalyzer");
			}
			this.requestAdapter = requestAdapter;
			this.exceptionAnalyzer = exceptionAnalyzer;
			this.responseTracker = responseTracker;
			this.requestAdapter.ResponseTracker = this.responseTracker;
			this.retryCountMapping[RequestTarget.LiveIdConsumer] = 2;
			this.retryCountMapping[RequestTarget.LiveIdBusiness] = 2;
			this.retryCountMapping[RequestTarget.Hotmail] = 2;
			this.retryCountMapping[RequestTarget.Akamai] = 2;
			this.dynamicHeaders.Add(new DynamicHeader("x-owa-canary", delegate(Uri uri)
			{
				Cookie cookie = this.cookieContainer.GetCookies(uri)["X-OWA-CANARY"];
				if (cookie != null)
				{
					return cookie.Value;
				}
				return null;
			}));
		}

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x0600288C RID: 10380 RVA: 0x0005632C File Offset: 0x0005452C
		// (remove) Token: 0x0600288D RID: 10381 RVA: 0x00056364 File Offset: 0x00054564
		public event EventHandler<HttpWebEventArgs> SendingRequest;

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x0600288E RID: 10382 RVA: 0x0005639C File Offset: 0x0005459C
		// (remove) Token: 0x0600288F RID: 10383 RVA: 0x000563D4 File Offset: 0x000545D4
		public event EventHandler<HttpWebEventArgs> ResponseReceived;

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x06002890 RID: 10384 RVA: 0x0005640C File Offset: 0x0005460C
		// (remove) Token: 0x06002891 RID: 10385 RVA: 0x00056444 File Offset: 0x00054644
		public event EventHandler<TestEventArgs> TestStarted;

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x06002892 RID: 10386 RVA: 0x0005647C File Offset: 0x0005467C
		// (remove) Token: 0x06002893 RID: 10387 RVA: 0x000564B4 File Offset: 0x000546B4
		public event EventHandler<TestEventArgs> TestFinished;

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x000564E9 File Offset: 0x000546E9
		// (set) Token: 0x06002895 RID: 10389 RVA: 0x000564F1 File Offset: 0x000546F1
		public string UserAgent
		{
			get
			{
				return this.userAgent;
			}
			set
			{
				this.userAgent = value;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x000564FA File Offset: 0x000546FA
		// (set) Token: 0x06002897 RID: 10391 RVA: 0x00056502 File Offset: 0x00054702
		public object EventState { get; set; }

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x0005650B File Offset: 0x0005470B
		public IResponseTracker ResponseTracker
		{
			get
			{
				return this.responseTracker;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002899 RID: 10393 RVA: 0x00056513 File Offset: 0x00054713
		public Dictionary<RequestTarget, int> RetryCountMapping
		{
			get
			{
				return this.retryCountMapping;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x0600289A RID: 10394 RVA: 0x0005651B File Offset: 0x0005471B
		// (set) Token: 0x0600289B RID: 10395 RVA: 0x00056523 File Offset: 0x00054723
		public ExCookieContainer CookieContainer
		{
			get
			{
				return this.cookieContainer;
			}
			set
			{
				this.cookieContainer = value;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x0600289C RID: 10396 RVA: 0x0005652C File Offset: 0x0005472C
		public Dictionary<string, string> PersistentHeaders
		{
			get
			{
				return this.persistentHeaders;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x0600289D RID: 10397 RVA: 0x00056534 File Offset: 0x00054734
		// (set) Token: 0x0600289E RID: 10398 RVA: 0x0005653C File Offset: 0x0005473C
		public AuthenticationData? AuthenticationData { get; set; }

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x0600289F RID: 10399 RVA: 0x00056545 File Offset: 0x00054745
		// (set) Token: 0x060028A0 RID: 10400 RVA: 0x0005654D File Offset: 0x0005474D
		public SslValidationOptions SslValidationOptions
		{
			get
			{
				return this.sslValidationOptions;
			}
			set
			{
				this.sslValidationOptions = value;
			}
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x00056558 File Offset: 0x00054758
		public void NotifyTestStarted(TestId testId)
		{
			lock (this.testStack)
			{
				this.testStack.Push(testId);
			}
			this.OnTestStarted(testId);
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x000565A8 File Offset: 0x000547A8
		public void NotifyTestFinished(TestId testId)
		{
			lock (this.testStack)
			{
				if (this.testStack.Count > 0)
				{
					this.testStack.Pop();
				}
			}
			this.OnTestFinished(testId);
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x00056604 File Offset: 0x00054804
		public IAsyncResult BeginGet(TestId stepId, string uri, AsyncCallback callback, Dictionary<string, object> asyncState)
		{
			Uri requestUri = new Uri(uri);
			HttpWebRequestWrapper request = HttpWebRequestWrapper.CreateRequest(stepId, requestUri, "GET", null, this.cookieContainer, this.persistentHeaders, this.userAgent, this.dynamicHeaders);
			return this.BeginSend(request, callback, asyncState);
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x00056648 File Offset: 0x00054848
		public T EndGet<T>(IAsyncResult result, Func<HttpWebResponseWrapper, T> processResponse)
		{
			return this.EndSend<T>(result, HttpSession.OkStatusCode, processResponse, true);
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x00056658 File Offset: 0x00054858
		public T EndGet<T>(IAsyncResult result, HttpStatusCode[] expectedStatusCodes, Func<HttpWebResponseWrapper, T> processResponse)
		{
			return this.EndSend<T>(result, expectedStatusCodes, processResponse, true);
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x00056664 File Offset: 0x00054864
		public IAsyncResult BeginPost(TestId stepId, string uri, RequestBody body, string contentType, Dictionary<string, string> headers, AsyncCallback callback, Dictionary<string, object> asyncState)
		{
			Uri requestUri = new Uri(uri);
			HttpWebRequestWrapper request = this.BuildPostRequest(stepId, requestUri, body, contentType, headers);
			return this.BeginSend(request, callback, asyncState);
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x00056694 File Offset: 0x00054894
		private HttpWebRequestWrapper BuildPostRequest(TestId stepId, Uri requestUri, RequestBody body, string contentType, Dictionary<string, string> headers)
		{
			HttpWebRequestWrapper httpWebRequestWrapper = HttpWebRequestWrapper.CreateRequest(stepId, requestUri, "POST", body, this.cookieContainer, this.persistentHeaders, this.userAgent, this.dynamicHeaders);
			httpWebRequestWrapper.Headers.Add("Content-Type", contentType);
			if (headers != null)
			{
				foreach (string text in headers.Keys)
				{
					httpWebRequestWrapper.Headers.Add(text, headers[text]);
				}
			}
			return httpWebRequestWrapper;
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x00056734 File Offset: 0x00054934
		public IAsyncResult BeginPost(TestId stepId, string uri, RequestBody body, string contentType, AsyncCallback callback, Dictionary<string, object> asyncState)
		{
			return this.BeginPost(stepId, uri, body, contentType, null, callback, asyncState);
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x00056746 File Offset: 0x00054946
		public T EndPost<T>(IAsyncResult result, Func<HttpWebResponseWrapper, T> processResponse)
		{
			return this.EndPost<T>(result, HttpSession.OkOrRedirectionStatusCode, processResponse);
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x00056755 File Offset: 0x00054955
		public T EndPost<T>(IAsyncResult result, HttpStatusCode[] expectedStatusCodes, Func<HttpWebResponseWrapper, T> processResponse)
		{
			return this.EndSend<T>(result, expectedStatusCodes, processResponse, true);
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x00056761 File Offset: 0x00054961
		public IAsyncResult BeginGetFollowingRedirections(TestId stepId, string uri, AsyncCallback callback, Dictionary<string, object> asyncState)
		{
			return this.BeginGetFollowingRedirections(stepId, uri, RedirectionOptions.FollowUntilNo302, callback, asyncState);
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x00056770 File Offset: 0x00054970
		public IAsyncResult BeginGetFollowingRedirections(TestId stepId, string uri, RedirectionOptions redirectionOptions, AsyncCallback callback, Dictionary<string, object> asyncState)
		{
			Uri requestUri = new Uri(uri);
			return this.BeginGetFollowingRedirections(stepId, requestUri, redirectionOptions, callback, asyncState);
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x00056794 File Offset: 0x00054994
		public IAsyncResult BeginGetFollowingRedirections(TestId stepId, Uri requestUri, RedirectionOptions redirectionOptions, AsyncCallback callback, Dictionary<string, object> asyncState)
		{
			if (asyncState == null)
			{
				asyncState = new Dictionary<string, object>();
			}
			HttpWebRequestWrapper request = HttpWebRequestWrapper.CreateRequest(stepId, requestUri, "GET", null, this.cookieContainer, this.persistentHeaders, this.userAgent, this.dynamicHeaders);
			return this.BeginSendFollowingRedirections(requestUri, request, redirectionOptions, callback, asyncState);
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x000567E0 File Offset: 0x000549E0
		public IAsyncResult BeginPostFollowingRedirections(TestId stepId, string uri, RequestBody body, string contentType, Dictionary<string, string> headers, RedirectionOptions redirectionOptions, AsyncCallback callback, Dictionary<string, object> asyncState)
		{
			Uri requestUri = new Uri(uri);
			return this.BeginPostFollowingRedirections(stepId, requestUri, body, contentType, headers, redirectionOptions, callback, asyncState);
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x00056808 File Offset: 0x00054A08
		public IAsyncResult BeginPostFollowingRedirections(TestId stepId, Uri requestUri, RequestBody body, string contentType, Dictionary<string, string> headers, RedirectionOptions redirectionOptions, AsyncCallback callback, Dictionary<string, object> asyncState)
		{
			if (asyncState == null)
			{
				asyncState = new Dictionary<string, object>();
			}
			HttpWebRequestWrapper request = this.BuildPostRequest(stepId, requestUri, body, contentType, headers);
			return this.BeginSendFollowingRedirections(requestUri, request, redirectionOptions, callback, asyncState);
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x0005683C File Offset: 0x00054A3C
		public IAsyncResult BeginSendFollowingRedirections(Uri uri, HttpWebRequestWrapper request, RedirectionOptions redirectionOptions, AsyncCallback callback, Dictionary<string, object> asyncState)
		{
			asyncState["RequestWrapper"] = request;
			asyncState["RedirectionCount"] = 0;
			asyncState["RedirectionOptions"] = redirectionOptions;
			asyncState["OriginalUri"] = uri;
			IAsyncResult asyncResult = new LazyAsyncResult(callback, asyncState);
			asyncState["OperationAsyncResult"] = asyncResult;
			this.BeginSend(request, new AsyncCallback(this.RedirectionCallback), asyncState);
			return asyncResult;
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x000568B5 File Offset: 0x00054AB5
		public T EndPostFollowingRedirections<T>(IAsyncResult result, Func<HttpWebResponseWrapper, T> processResponse)
		{
			return this.EndGetFollowingRedirections<T>(result, processResponse);
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x000568F0 File Offset: 0x00054AF0
		public T EndGetFollowingRedirections<T>(IAsyncResult result, Func<HttpWebResponseWrapper, T> processResponse)
		{
			LazyAsyncResult lazyAsyncResult = result as LazyAsyncResult;
			if (!lazyAsyncResult.IsCompleted)
			{
				lazyAsyncResult.AsyncWaitHandle.WaitOne();
			}
			if (lazyAsyncResult.Exception != null)
			{
				throw lazyAsyncResult.Exception as Exception;
			}
			if (processResponse != null)
			{
				HttpWebResponseWrapper response = lazyAsyncResult.ResultObject as HttpWebResponseWrapper;
				try
				{
					return processResponse(response);
				}
				catch (Exception exception)
				{
					this.exceptionAnalyzer.Analyze(response.Request.StepId, response.Request, response, exception, this.responseTracker, delegate(ScenarioException scenarioException)
					{
						if (this.responseTracker != null)
						{
							this.responseTracker.TrackFailedResponse(response, scenarioException);
						}
					});
					throw;
				}
			}
			return default(T);
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x000569C4 File Offset: 0x00054BC4
		public void CloseConnections()
		{
			this.requestAdapter.CloseConnections();
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x00056A08 File Offset: 0x00054C08
		public ScenarioException VerifyScenarioExceededRunTime(TimeSpan? maxAllowedTime)
		{
			if (maxAllowedTime == null || this.responseTracker == null)
			{
				return null;
			}
			TimeSpan timeSpan = TimeSpan.FromTicks(this.responseTracker.Items.Sum((ResponseTrackerItem r) => r.TotalLatency.Ticks));
			if (timeSpan <= maxAllowedTime.Value)
			{
				return null;
			}
			long largestLatency = this.responseTracker.Items.Max((ResponseTrackerItem r) => r.TotalLatency.Ticks);
			ResponseTrackerItem responseTrackerItem = (from r in this.responseTracker.Items
			where r.TotalLatency.Ticks == largestLatency
			select r).First<ResponseTrackerItem>();
			responseTrackerItem.FailingServer = responseTrackerItem.RespondingServer;
			ScenarioException exceptionForScenarioTimeout = this.exceptionAnalyzer.GetExceptionForScenarioTimeout(maxAllowedTime.Value, timeSpan, responseTrackerItem);
			this.responseTracker.TrackItemCausingScenarioTimeout(responseTrackerItem, exceptionForScenarioTimeout);
			return exceptionForScenarioTimeout;
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x00056AF6 File Offset: 0x00054CF6
		public virtual List<string> GetHostNames(RequestTarget requestTarget)
		{
			return this.exceptionAnalyzer.GetHostNames(requestTarget);
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x00056B24 File Offset: 0x00054D24
		private void RedirectionCallback(IAsyncResult result)
		{
			Dictionary<string, object> dictionary = result.AsyncState as Dictionary<string, object>;
			HttpStatusCode[] expectedStatusCodes = null;
			int? num = dictionary["RedirectionCount"] as int?;
			ExTraceGlobals.MonitoringWebClientTracer.TraceDebug<int?>((long)this.GetHashCode(), "Redirection callback invoked. Current redir count: {0}", num);
			if (num >= 15)
			{
				expectedStatusCodes = HttpSession.OkStatusCode;
				ExTraceGlobals.MonitoringWebClientTracer.TraceDebug((long)this.GetHashCode(), "Max redirection count reached");
			}
			LazyAsyncResult lazyAsyncResult = dictionary["OperationAsyncResult"] as LazyAsyncResult;
			RedirectionOptions redirectionOptions = (RedirectionOptions)dictionary["RedirectionOptions"];
			Uri uri = dictionary["OriginalUri"] as Uri;
			try
			{
				var <>f__AnonymousType = this.EndSend(result, expectedStatusCodes, (HttpWebResponseWrapper response) => new
				{
					StatusCode = response.StatusCode,
					Location = response.Headers["Location"],
					Response = response
				}, false);
				HttpStatusCode httpStatusCode = <>f__AnonymousType.StatusCode;
				string text = <>f__AnonymousType.Location;
				CasRedirectPage casRedirectPage = null;
				if (<>f__AnonymousType.StatusCode == HttpStatusCode.OK && CasRedirectPage.TryParse(<>f__AnonymousType.Response, out casRedirectPage))
				{
					text = casRedirectPage.TargetUrl;
					httpStatusCode = HttpStatusCode.Found;
				}
				LiveIdRedirectPage liveIdRedirectPage = null;
				if (<>f__AnonymousType.StatusCode == HttpStatusCode.OK && LiveIdRedirectPage.TryParse(<>f__AnonymousType.Response, out liveIdRedirectPage))
				{
					text = liveIdRedirectPage.TargetUrl;
					httpStatusCode = HttpStatusCode.Found;
				}
				if (httpStatusCode == HttpStatusCode.Found || httpStatusCode == HttpStatusCode.MovedPermanently)
				{
					HttpWebRequestWrapper httpWebRequestWrapper = dictionary["RequestWrapper"] as HttpWebRequestWrapper;
					Uri uri2;
					if (Uri.IsWellFormedUriString(text, UriKind.Absolute))
					{
						uri2 = new Uri(text);
					}
					else if (Uri.IsWellFormedUriString(text, UriKind.Relative))
					{
						uri2 = new Uri(httpWebRequestWrapper.RequestUri, text);
					}
					else if (!Uri.TryCreate(text, UriKind.Absolute, out uri2) && !Uri.TryCreate(httpWebRequestWrapper.RequestUri, text, out uri2))
					{
						throw new ArgumentException("Server returned malformed location: " + text);
					}
					if (redirectionOptions == RedirectionOptions.StopOnFirstCrossDomainRedirect)
					{
						if (!uri2.Host.EndsWith(uri.Host, StringComparison.OrdinalIgnoreCase))
						{
							lazyAsyncResult.Complete(<>f__AnonymousType.Response, null);
							return;
						}
					}
					else if (redirectionOptions == RedirectionOptions.FollowUntilNo302ExpectCrossDomainOnFirstRedirect)
					{
						if (num == 0 && uri2.Host.Equals(uri.Host))
						{
							lazyAsyncResult.Complete(<>f__AnonymousType.Response, null);
							return;
						}
					}
					else if (redirectionOptions == RedirectionOptions.FollowUntilNo302OrSpecificRedirection)
					{
						string[] array = dictionary["LastExpectedRedirection"] as string[];
						if (array != null && array.Length > 0)
						{
							foreach (string value in array)
							{
								if (uri2.PathAndQuery.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0)
								{
									lazyAsyncResult.Complete(<>f__AnonymousType.Response, null);
									return;
								}
							}
						}
					}
					ExTraceGlobals.MonitoringWebClientTracer.TraceDebug<Uri>((long)this.GetHashCode(), "Following redirection to {0}", uri2);
					this.OnResponseReceived(<>f__AnonymousType.Response.Request, <>f__AnonymousType.Response);
					HttpWebRequestWrapper request = HttpWebRequestWrapper.CreateRequest(httpWebRequestWrapper.StepId, uri2, "GET", null, this.cookieContainer, this.persistentHeaders, this.userAgent, this.dynamicHeaders);
					num++;
					dictionary["RedirectionCount"] = num;
					this.BeginSend(request, new AsyncCallback(this.RedirectionCallback), dictionary);
				}
				else
				{
					lazyAsyncResult.Complete(<>f__AnonymousType.Response, null);
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.MonitoringWebClientTracer.TraceError<Exception>((long)this.GetHashCode(), "Exception thrown: {0}", ex);
				lazyAsyncResult.Complete(null, ex);
			}
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x00056EDC File Offset: 0x000550DC
		private IAsyncResult BeginSend(HttpWebRequestWrapper request, AsyncCallback callback, Dictionary<string, object> asyncState)
		{
			ExTraceGlobals.MonitoringWebClientTracer.TraceDebug<Uri>((long)this.GetHashCode(), "BeginSend invoked for {0}", request.RequestUri);
			if (asyncState == null)
			{
				asyncState = new Dictionary<string, object>();
			}
			this.OnSendingRequest(request);
			asyncState["RequestWrapper"] = request;
			ResponseTrackerItem responseTrackerItem = null;
			if (this.responseTracker != null)
			{
				RequestTarget requestTarget = this.exceptionAnalyzer.GetRequestTarget(request);
				responseTrackerItem = this.responseTracker.TrackRequest(request.StepId, requestTarget, request);
				asyncState["ResponseTrackerItem"] = responseTrackerItem;
			}
			IAsyncResult result;
			try
			{
				result = this.requestAdapter.BeginGetResponse(request, this.cookieContainer, this.sslValidationOptions, this.AuthenticationData, this.GetRetryCount(request), callback, asyncState);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.MonitoringWebClientTracer.TraceError<Exception>((long)this.GetHashCode(), "Exception thrown: {0}", ex);
				LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(callback, asyncState);
				lazyAsyncResult.Complete(null, ex);
				result = lazyAsyncResult;
			}
			finally
			{
				if (responseTrackerItem != null)
				{
					this.responseTracker.TrackSentRequest(responseTrackerItem, request);
				}
			}
			return result;
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x00056FE0 File Offset: 0x000551E0
		private int GetRetryCount(HttpWebRequestWrapper request)
		{
			RequestTarget requestTarget = this.exceptionAnalyzer.GetRequestTarget(request);
			if (this.retryCountMapping.ContainsKey(requestTarget))
			{
				return this.retryCountMapping[requestTarget];
			}
			return 0;
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x00057018 File Offset: 0x00055218
		private T EndSend<T>(IAsyncResult result, HttpStatusCode[] expectedStatusCodes, Func<HttpWebResponseWrapper, T> processResponse, bool fireResponseReceivedEvent = true)
		{
			Exception exception = null;
			Dictionary<string, object> dictionary = result.AsyncState as Dictionary<string, object>;
			HttpWebRequestWrapper httpWebRequestWrapper = dictionary["RequestWrapper"] as HttpWebRequestWrapper;
			ResponseTrackerItem item = dictionary["ResponseTrackerItem"] as ResponseTrackerItem;
			CafeErrorPageValidationRules cafeErrorPageValidationRules = CafeErrorPageValidationRules.None;
			if (dictionary.ContainsKey("CafeErrorPageValidationRules"))
			{
				cafeErrorPageValidationRules = ((dictionary["CafeErrorPageValidationRules"] as CafeErrorPageValidationRules?) ?? CafeErrorPageValidationRules.None);
			}
			ExTraceGlobals.MonitoringWebClientTracer.TraceDebug<Uri>((long)this.GetHashCode(), "EndSend invoked for {0}", httpWebRequestWrapper.RequestUri);
			HttpWebResponseWrapper httpWebResponseWrapper = null;
			try
			{
				httpWebResponseWrapper = this.requestAdapter.EndGetResponse(result);
				if (httpWebResponseWrapper == null)
				{
					throw new ArgumentOutOfRangeException("response shouldn't be null on a successful request");
				}
			}
			catch (HttpWebResponseWrapperException ex)
			{
				ExTraceGlobals.MonitoringWebClientTracer.TraceError<HttpWebResponseWrapperException>((long)this.GetHashCode(), "HttpWebResponseWrapperException thrown: {0}", ex);
				httpWebResponseWrapper = ex.Response;
				exception = ex;
			}
			catch (Exception ex2)
			{
				ExTraceGlobals.MonitoringWebClientTracer.TraceError<Exception>((long)this.GetHashCode(), "Exception thrown: {0}", ex2);
				exception = ex2;
			}
			if (httpWebResponseWrapper != null)
			{
				if (fireResponseReceivedEvent)
				{
					this.OnResponseReceived(httpWebRequestWrapper, httpWebResponseWrapper);
				}
				if (this.responseTracker != null)
				{
					this.responseTracker.TrackResponse(item, httpWebResponseWrapper);
				}
			}
			else if (this.responseTracker != null)
			{
				RequestTarget requestTarget = this.exceptionAnalyzer.GetRequestTarget(httpWebRequestWrapper);
				this.responseTracker.TrackFailedRequest(httpWebRequestWrapper.StepId, requestTarget, httpWebRequestWrapper, exception);
			}
			return this.AnalyzeResponse<T>(httpWebRequestWrapper, httpWebResponseWrapper, exception, expectedStatusCodes, cafeErrorPageValidationRules, processResponse);
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000571D4 File Offset: 0x000553D4
		private T AnalyzeResponse<T>(HttpWebRequestWrapper request, HttpWebResponseWrapper response, Exception exception, HttpStatusCode[] expectedStatusCodes, CafeErrorPageValidationRules cafeErrorPageValidationRules, Func<HttpWebResponseWrapper, T> processResponse)
		{
			T result = default(T);
			if (response != null)
			{
				exception = this.exceptionAnalyzer.VerifyResponse(request, response, cafeErrorPageValidationRules);
				if (exception == null && expectedStatusCodes != null)
				{
					if (!Array.Exists<HttpStatusCode>(expectedStatusCodes, (HttpStatusCode statusCode) => statusCode == response.StatusCode))
					{
						ExTraceGlobals.MonitoringWebClientTracer.TraceDebug<HttpStatusCode>((long)this.GetHashCode(), "Unexpected response code received: {0}", response.StatusCode);
						exception = new UnexpectedStatusCodeException(MonitoringWebClientStrings.UnexpectedResponseCodeReceived, request, response, expectedStatusCodes, response.StatusCode);
					}
				}
				if (exception == null)
				{
					try
					{
						if (processResponse != null)
						{
							ExTraceGlobals.MonitoringWebClientTracer.TraceDebug((long)this.GetHashCode(), "Invoking response processing callback");
							result = processResponse(response);
						}
					}
					catch (Exception ex)
					{
						exception = ex;
					}
				}
			}
			if (exception != null)
			{
				ExTraceGlobals.MonitoringWebClientTracer.TraceDebug<Exception>((long)this.GetHashCode(), "Invoking exception analyzer for: {0}", exception);
				this.exceptionAnalyzer.Analyze(request.StepId, request, response, exception, this.responseTracker, delegate(ScenarioException scenarioException)
				{
					if (this.responseTracker != null)
					{
						this.responseTracker.TrackFailedResponse(response, scenarioException);
					}
				});
			}
			return result;
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x00057324 File Offset: 0x00055524
		private void OnSendingRequest(HttpWebRequestWrapper request)
		{
			EventHandler<HttpWebEventArgs> sendingRequest = this.SendingRequest;
			if (sendingRequest != null)
			{
				sendingRequest(this, new HttpWebEventArgs(request, this.EventState));
			}
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x00057350 File Offset: 0x00055550
		private void OnResponseReceived(HttpWebRequestWrapper request, HttpWebResponseWrapper response)
		{
			EventHandler<HttpWebEventArgs> responseReceived = this.ResponseReceived;
			if (responseReceived != null)
			{
				responseReceived(this, new HttpWebEventArgs(request, response, this.EventState));
			}
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x0005737C File Offset: 0x0005557C
		private void OnTestStarted(TestId testId)
		{
			EventHandler<TestEventArgs> testStarted = this.TestStarted;
			if (testStarted != null)
			{
				testStarted(this, new TestEventArgs(testId, this.EventState));
			}
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000573A8 File Offset: 0x000555A8
		private void OnTestFinished(TestId testId)
		{
			EventHandler<TestEventArgs> testFinished = this.TestFinished;
			if (testFinished != null)
			{
				testFinished(this, new TestEventArgs(testId, this.EventState));
			}
		}

		// Token: 0x04002431 RID: 9265
		public const string LastExpectedRedirectionParameterName = "LastExpectedRedirection";

		// Token: 0x04002432 RID: 9266
		public const string CafeErrorPageValidationRulesParameterName = "CafeErrorPageValidationRules";

		// Token: 0x04002433 RID: 9267
		internal static readonly HttpStatusCode[] OkStatusCode = new HttpStatusCode[]
		{
			HttpStatusCode.OK
		};

		// Token: 0x04002434 RID: 9268
		internal static readonly HttpStatusCode[] OkOrRedirectionStatusCode = new HttpStatusCode[]
		{
			HttpStatusCode.OK,
			HttpStatusCode.Found,
			HttpStatusCode.MovedPermanently
		};

		// Token: 0x04002435 RID: 9269
		internal static readonly HttpStatusCode[] AllHttpStatusCodes;

		// Token: 0x04002436 RID: 9270
		private string userAgent = "Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 6.1; MSEXCHMON; MONITORINGWEBCLIENT)";

		// Token: 0x04002437 RID: 9271
		private Stack<TestId> testStack = new Stack<TestId>();

		// Token: 0x04002438 RID: 9272
		private SslValidationOptions sslValidationOptions = SslValidationOptions.BasicCertificateValidation;

		// Token: 0x0400243D RID: 9277
		private readonly Dictionary<string, string> persistentHeaders = new Dictionary<string, string>();

		// Token: 0x0400243E RID: 9278
		private ExCookieContainer cookieContainer = new ExCookieContainer();

		// Token: 0x0400243F RID: 9279
		private IRequestAdapter requestAdapter;

		// Token: 0x04002440 RID: 9280
		private IExceptionAnalyzer exceptionAnalyzer;

		// Token: 0x04002441 RID: 9281
		private IResponseTracker responseTracker;

		// Token: 0x04002442 RID: 9282
		private Dictionary<RequestTarget, int> retryCountMapping = new Dictionary<RequestTarget, int>();

		// Token: 0x04002443 RID: 9283
		private List<DynamicHeader> dynamicHeaders = new List<DynamicHeader>();
	}
}

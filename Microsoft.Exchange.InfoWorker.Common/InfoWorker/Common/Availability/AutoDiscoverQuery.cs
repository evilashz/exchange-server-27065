using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000066 RID: 102
	internal abstract class AutoDiscoverQuery : AsyncTask, IDisposable
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0000B964 File Offset: 0x00009B64
		protected AutoDiscoverQuery(Application application, ClientContext clientContext, RequestLogger requestLogger, Uri autoDiscoverUrl, AutoDiscoverAuthenticator authenticator, AutoDiscoverQueryItem[] queryItems, int redirectionDepth, CreateAutoDiscoverRequestDelegate createAutoDiscoverRequest, AutodiscoverType autodiscoverType, QueryList queryList)
		{
			this.application = application;
			this.clientContext = clientContext;
			this.requestLogger = requestLogger;
			this.autoDiscoverUrl = autoDiscoverUrl;
			this.authenticator = authenticator;
			this.queryItems = queryItems;
			this.redirectionDepth = redirectionDepth;
			this.createAutoDiscoverRequest = createAutoDiscoverRequest;
			this.autodiscoverType = autodiscoverType;
			this.queryList = queryList;
			this.emailAddresses = new EmailAddress[queryItems.Length];
			for (int i = 0; i < queryItems.Length; i++)
			{
				this.emailAddresses[i] = queryItems[i].EmailAddress;
			}
			StringBuilder stringBuilder = new StringBuilder(200);
			foreach (AutoDiscoverQueryItem autoDiscoverQueryItem in this.queryItems)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(autoDiscoverQueryItem.EmailAddress.ToString());
			}
			this.emailAddressesString = stringBuilder.ToString();
			AutoDiscoverQuery.AutoDiscoverTracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Created AutoDiscoverQuery for {1}", TraceContext.Get(), this.emailAddressesString);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000BA70 File Offset: 0x00009C70
		public override void Abort()
		{
			base.Abort();
			if (this.timeTaken != null && this.timeTaken.IsRunning)
			{
				this.timeTaken.Stop();
			}
			if (this.queryItems != null)
			{
				foreach (AutoDiscoverQueryItem autoDiscoverQueryItem in this.queryItems)
				{
					autoDiscoverQueryItem.SetResult(AutoDiscoverQuery.TimeoutResult);
				}
			}
			if (this.pendingRequest != null)
			{
				this.pendingRequest.Abort();
			}
			if (this.pendingRedirectRequests != null)
			{
				this.pendingRedirectRequests.Abort();
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		public void Dispose()
		{
			if (this.pendingRequest != null)
			{
				this.pendingRequest.Dispose();
			}
			if (this.redirectRequests != null)
			{
				foreach (AutoDiscoverQuery disposable in this.redirectRequests)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000BB40 File Offset: 0x00009D40
		public override void BeginInvoke(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			int num = this.redirectionDepth;
			if (num == 0)
			{
				this.serverRequestId = Microsoft.Exchange.Diagnostics.Trace.TraceCasStart(CasTraceEventType.AutoDiscover);
			}
			this.timeTaken = Stopwatch.StartNew();
			this.pendingRequest = this.createAutoDiscoverRequest(this.application, this.clientContext, this.requestLogger, this.autoDiscoverUrl, this.authenticator, this.emailAddresses, UriSource.EmailDomain, this.autodiscoverType);
			this.pendingRequest.BeginInvoke(new TaskCompleteCallback(this.CompleteRequest));
			if (this.queryList != null && this.queryList.Count > 0)
			{
				this.queryList.LogLatency("ADQBI_" + num, this.timeTaken.ElapsedMilliseconds);
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000BC04 File Offset: 0x00009E04
		protected void AddWebServiceUriToCache(AutoDiscoverQueryItem queryItem, WebServiceUri webServiceUri)
		{
			int autodiscoverVersionBucket = this.Application.GetAutodiscoverVersionBucket(this.autodiscoverType);
			RemoteServiceUriCache.Add(queryItem.EmailAddress, webServiceUri, autodiscoverVersionBucket);
			if (queryItem.InitialEmailAddress != queryItem.EmailAddress)
			{
				RemoteServiceUriCache.Add(queryItem.InitialEmailAddress, webServiceUri, autodiscoverVersionBucket);
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000BC4C File Offset: 0x00009E4C
		protected QueryList GetQueryListFromQueryItems(AutoDiscoverQueryItem[] queryItems)
		{
			QueryList queryList = new QueryList();
			foreach (AutoDiscoverQueryItem autoDiscoverQueryItem in queryItems)
			{
				if (autoDiscoverQueryItem.SourceQuery != null)
				{
					queryList.Add(autoDiscoverQueryItem.SourceQuery);
				}
			}
			return queryList;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000BC88 File Offset: 0x00009E88
		private void CompleteRequest(AsyncTask task)
		{
			int num = this.redirectionDepth;
			bool flag = this.CompleteRequestInternal();
			if (flag)
			{
				this.timeTaken.Stop();
				this.TraceCasStop();
				base.Complete();
			}
			if (!base.Aborted && this.queryList != null && this.queryList.Count > 0)
			{
				this.queryList.LogLatency("ADQC_" + num, this.timeTaken.ElapsedMilliseconds);
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000BD04 File Offset: 0x00009F04
		private bool CompleteRequestInternal()
		{
			Uri httpRedirect = this.GetHttpRedirect(this.pendingRequest.Exception);
			if (httpRedirect != null)
			{
				this.redirectionDepth++;
				if (this.redirectionDepth > 3)
				{
					this.SetErrorRedirectionDepthExceeded();
					return true;
				}
				this.pendingRequest.Dispose();
				this.pendingRequest = this.createAutoDiscoverRequest(this.Application, this.clientContext, this.requestLogger, httpRedirect, this.authenticator, this.emailAddresses, UriSource.EmailDomain, this.autodiscoverType);
				this.pendingRequest.BeginInvoke(new TaskCompleteCallback(this.CompleteRequest));
				return false;
			}
			else
			{
				if (this.pendingRequest.Exception != null)
				{
					AutoDiscoverQuery.AutoDiscoverTracer.TraceError((long)this.GetHashCode(), "{0}: Failed to get results from AutoDiscover for {1}, error: {2}. FE: {3}, BE: {4}", new object[]
					{
						TraceContext.Get(),
						this.emailAddressesString,
						this.pendingRequest.Exception,
						this.pendingRequest.FrontEndServerName,
						this.pendingRequest.BackEndServerName
					});
					foreach (AutoDiscoverQueryItem autoDiscoverQueryItem in this.queryItems)
					{
						autoDiscoverQueryItem.SetResult(new AutoDiscoverResult(new AutoDiscoverFailedException(Strings.descAutoDiscoverFailedWithException(autoDiscoverQueryItem.EmailAddress.ToString(), this.pendingRequest.Exception.ToString()), this.pendingRequest.Exception)));
						if (!base.Aborted)
						{
							autoDiscoverQueryItem.SourceQuery.LogAutoDiscRequestDetails(this.pendingRequest.FrontEndServerName, this.pendingRequest.BackEndServerName, null);
						}
					}
					return true;
				}
				Dictionary<Uri, List<AutoDiscoverQueryItem>> dictionary = null;
				Dictionary<string, List<AutoDiscoverQueryItem>> dictionary2 = null;
				for (int j = 0; j < this.queryItems.Length; j++)
				{
					AutoDiscoverQueryItem autoDiscoverQueryItem2 = this.queryItems[j];
					AutoDiscoverRequestResult autoDiscoverRequestResult = this.pendingRequest.Results[j];
					if (autoDiscoverRequestResult.WebServiceUri != null)
					{
						AutoDiscoverQuery.AutoDiscoverTracer.TraceDebug((long)this.GetHashCode(), "{0}: Found WebServiceUri {1} for user {2} in the response for request at {3}", new object[]
						{
							TraceContext.Get(),
							autoDiscoverRequestResult.WebServiceUri,
							autoDiscoverQueryItem2.EmailAddress,
							autoDiscoverRequestResult.Url
						});
						this.SetResult(autoDiscoverQueryItem2, autoDiscoverRequestResult.WebServiceUri);
					}
					else if (autoDiscoverRequestResult.Exception != null)
					{
						autoDiscoverQueryItem2.SetResult(new AutoDiscoverResult(new AutoDiscoverFailedException(Strings.descAutoDiscoverFailedWithException(autoDiscoverQueryItem2.EmailAddress.ToString(), autoDiscoverRequestResult.Exception.ToString()), autoDiscoverRequestResult.Exception)));
						if (!base.Aborted)
						{
							autoDiscoverQueryItem2.SourceQuery.LogAutoDiscRequestDetails(autoDiscoverRequestResult.FrontEndServerName, autoDiscoverRequestResult.BackEndServerName, null);
						}
					}
					else if (autoDiscoverRequestResult.AutoDiscoverRedirectUri != null)
					{
						AutoDiscoverQuery.AutoDiscoverTracer.TraceDebug((long)this.GetHashCode(), "{0}: Got redirect autodiscover URL for user {1} to {2}. FE: {3}, BE: {4}", new object[]
						{
							TraceContext.Get(),
							autoDiscoverQueryItem2.EmailAddress,
							autoDiscoverRequestResult.AutoDiscoverRedirectUri,
							autoDiscoverRequestResult.FrontEndServerName,
							autoDiscoverRequestResult.BackEndServerName
						});
						if (!base.Aborted)
						{
							autoDiscoverQueryItem2.SourceQuery.LogAutoDiscRequestDetails(autoDiscoverRequestResult.FrontEndServerName, autoDiscoverRequestResult.BackEndServerName, autoDiscoverRequestResult.AutoDiscoverRedirectUri.ToString());
						}
						if (dictionary == null)
						{
							dictionary = new Dictionary<Uri, List<AutoDiscoverQueryItem>>(1);
						}
						List<AutoDiscoverQueryItem> list;
						if (!dictionary.TryGetValue(autoDiscoverRequestResult.AutoDiscoverRedirectUri, out list))
						{
							list = new List<AutoDiscoverQueryItem>(1);
							dictionary.Add(autoDiscoverRequestResult.AutoDiscoverRedirectUri, list);
						}
						list.Add(autoDiscoverQueryItem2);
					}
					else if (autoDiscoverRequestResult.RedirectAddress != null)
					{
						AutoDiscoverQuery.AutoDiscoverTracer.TraceDebug((long)this.GetHashCode(), "{0}: Got redirect e-mail address from {1} to {2}. FE: {3}, BE: {4}", new object[]
						{
							TraceContext.Get(),
							autoDiscoverQueryItem2.EmailAddress,
							autoDiscoverRequestResult.RedirectAddress,
							autoDiscoverRequestResult.FrontEndServerName,
							autoDiscoverRequestResult.BackEndServerName
						});
						if (!base.Aborted)
						{
							autoDiscoverQueryItem2.SourceQuery.LogAutoDiscRequestDetails(autoDiscoverRequestResult.FrontEndServerName, autoDiscoverRequestResult.BackEndServerName, autoDiscoverRequestResult.RedirectAddress);
						}
						if (StringComparer.InvariantCultureIgnoreCase.Equals(autoDiscoverRequestResult.RedirectAddress, autoDiscoverQueryItem2.EmailAddress))
						{
							AutoDiscoverQuery.AutoDiscoverTracer.TraceError<object, EmailAddress>((long)this.GetHashCode(), "{0}: Got redirect e-mail address with same value as the original e-mail address {1}", TraceContext.Get(), autoDiscoverQueryItem2.EmailAddress);
							autoDiscoverQueryItem2.SetResult(new AutoDiscoverResult(new AutoDiscoverFailedException(Strings.descInvalidTargetAddress(autoDiscoverRequestResult.RedirectAddress), 52540U)));
						}
						else if (!SmtpAddress.IsValidSmtpAddress(autoDiscoverRequestResult.RedirectAddress))
						{
							AutoDiscoverQuery.AutoDiscoverTracer.TraceError<object, string>((long)this.GetHashCode(), "{0}: Got redirect e-mail address which is not valid {1}", TraceContext.Get(), autoDiscoverRequestResult.RedirectAddress);
							this.SetResult(autoDiscoverQueryItem2, Strings.descInvalidTargetAddress(autoDiscoverRequestResult.RedirectAddress), 46396U);
						}
						else
						{
							EmailAddress emailAddress = new EmailAddress(null, autoDiscoverRequestResult.RedirectAddress);
							if (dictionary2 == null)
							{
								dictionary2 = new Dictionary<string, List<AutoDiscoverQueryItem>>(1);
							}
							List<AutoDiscoverQueryItem> list2;
							if (!dictionary2.TryGetValue(emailAddress.Domain, out list2))
							{
								list2 = new List<AutoDiscoverQueryItem>(1);
								dictionary2.Add(emailAddress.Domain, list2);
							}
							autoDiscoverQueryItem2.EmailAddress = emailAddress;
							list2.Add(autoDiscoverQueryItem2);
						}
					}
					else
					{
						AutoDiscoverQuery.AutoDiscoverTracer.TraceError<object, EmailAddress, Uri>((long)this.GetHashCode(), "{0}: Found no WebServiceUri for user {1} in the response for request at {2}", TraceContext.Get(), autoDiscoverQueryItem2.EmailAddress, autoDiscoverRequestResult.Url);
						this.SetResult(autoDiscoverQueryItem2, Strings.descCrossForestServiceMissing(autoDiscoverQueryItem2.EmailAddress.ToString()), 62780U);
					}
				}
				if (dictionary == null && dictionary2 == null)
				{
					return true;
				}
				this.redirectionDepth++;
				if (this.redirectionDepth > 3)
				{
					this.SetErrorRedirectionDepthExceeded();
					return true;
				}
				List<AutoDiscoverQuery> list3 = new List<AutoDiscoverQuery>(((dictionary != null) ? dictionary.Count : 0) + ((dictionary2 != null) ? dictionary2.Count : 0));
				if (dictionary != null)
				{
					foreach (KeyValuePair<Uri, List<AutoDiscoverQueryItem>> keyValuePair in dictionary)
					{
						Uri key = keyValuePair.Key;
						AutoDiscoverQueryItem[] array2 = keyValuePair.Value.ToArray();
						AutoDiscoverQuery item = this.CreateAutoDiscoverQuery(key, array2, this.redirectionDepth);
						list3.Add(item);
					}
				}
				if (dictionary2 != null)
				{
					foreach (KeyValuePair<string, List<AutoDiscoverQueryItem>> keyValuePair2 in dictionary2)
					{
						string key2 = keyValuePair2.Key;
						AutoDiscoverQueryItem[] array3 = keyValuePair2.Value.ToArray();
						try
						{
							AutoDiscoverQuery item2 = this.CreateAutoDiscoverQuery(key2, array3, this.redirectionDepth);
							list3.Add(item2);
						}
						catch (LocalizedException ex)
						{
							AutoDiscoverQuery.AutoDiscoverTracer.TraceError<object, string, LocalizedException>((long)this.GetHashCode(), "{0}: Failed to create AutoDiscoverQuery for {1} due exception: {2}", TraceContext.Get(), key2, ex);
							foreach (AutoDiscoverQueryItem autoDiscoverQueryItem3 in array3)
							{
								autoDiscoverQueryItem3.SetResult(new AutoDiscoverResult(new AutoDiscoverFailedException(Strings.descAutoDiscoverFailedWithException(autoDiscoverQueryItem3.EmailAddress.ToString(), ex.ToString()), 38204U)));
							}
						}
					}
				}
				if (list3.Count == 0)
				{
					AutoDiscoverQuery.AutoDiscoverTracer.TraceError((long)this.GetHashCode(), "{0}: No redirect requests", new object[]
					{
						TraceContext.Get()
					});
					return true;
				}
				this.redirectRequests = list3.ToArray();
				this.pendingRedirectRequests = new AsyncTaskParallel(this.redirectRequests);
				this.pendingRedirectRequests.BeginInvoke(new TaskCompleteCallback(this.CompleteRedirectRequests));
				return false;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000C46C File Offset: 0x0000A66C
		private void CompleteRedirectRequests(AsyncTask task)
		{
			this.timeTaken.Stop();
			this.TraceCasStop();
			base.Complete();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000C488 File Offset: 0x0000A688
		private Uri GetHttpRedirect(Exception exception)
		{
			if (exception == null)
			{
				return null;
			}
			WebException ex = exception as WebException;
			if (ex == null)
			{
				ex = (exception.InnerException as WebException);
				if (ex == null)
				{
					AutoDiscoverQuery.AutoDiscoverTracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: check for HTTP redirection did not find WebException. This is instead {1}", TraceContext.Get(), exception.GetType().Name);
					return null;
				}
			}
			HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
			if (httpWebResponse == null)
			{
				AutoDiscoverQuery.AutoDiscoverTracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: check for HTTP redirection did not find HttpWebResponse in WebException. Instead it found: {1}", TraceContext.Get(), (ex.Response == null) ? "<null>" : ex.Response.GetType().Name);
				return null;
			}
			if (httpWebResponse.StatusCode != HttpStatusCode.Found)
			{
				AutoDiscoverQuery.AutoDiscoverTracer.TraceDebug<object, HttpStatusCode>((long)this.GetHashCode(), "{0}: check for HTTP redirection found HttpResponse is not an redirect. Instead it found: {1}", TraceContext.Get(), httpWebResponse.StatusCode);
				return null;
			}
			string text = httpWebResponse.Headers[HttpResponseHeader.Location];
			if (!Uri.IsWellFormedUriString(text, UriKind.Absolute))
			{
				AutoDiscoverQuery.AutoDiscoverTracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: check for HTTP redirection did not find valid redirect URL: {1}", TraceContext.Get(), text);
				return null;
			}
			Uri uri = new Uri(text, UriKind.Absolute);
			if (uri.Scheme != Uri.UriSchemeHttps)
			{
				AutoDiscoverQuery.AutoDiscoverTracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: check for HTTP redirection did not find HTTPS redirect URL: {1}", TraceContext.Get(), text);
				return null;
			}
			AutoDiscoverQuery.AutoDiscoverTracer.TraceDebug<object, Uri>((long)this.GetHashCode(), "{0}: check for HTTP redirection found redirect URL: {1}", TraceContext.Get(), uri);
			return uri;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		private void SetErrorRedirectionDepthExceeded()
		{
			AutoDiscoverQuery.AutoDiscoverTracer.TraceError((long)this.GetHashCode(), "{0}: We exceeded the number of times we can do autodiscover redirection. Failing autodiscover.", new object[]
			{
				TraceContext.Get()
			});
			foreach (AutoDiscoverQueryItem autoDiscoverQueryItem in this.queryItems)
			{
				autoDiscoverQueryItem.SetResult(new AutoDiscoverResult(new AutoDiscoverFailedException(Strings.descExceededMaxRedirectionDepth(autoDiscoverQueryItem.EmailAddress.ToString(), 3))));
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000C650 File Offset: 0x0000A850
		private void TraceCasStop()
		{
			if (this.serverRequestId != Guid.Empty && ETWTrace.ShouldTraceCasStop(this.serverRequestId))
			{
				Microsoft.Exchange.Diagnostics.Trace.TraceCasStop(CasTraceEventType.AutoDiscover, this.serverRequestId, 0, 0, Query<AvailabilityQueryResult>.GetCurrentHttpRequestServerName(), TraceContext.Get(), "AutoDiscoverQuery::CompleteRequest", "emailAddresses: " + this.emailAddressesString, string.Empty);
			}
		}

		// Token: 0x0600027C RID: 636
		protected abstract void SetResult(AutoDiscoverQueryItem queryItem, WebServiceUri webServiceUri);

		// Token: 0x0600027D RID: 637
		protected abstract void SetResult(AutoDiscoverQueryItem queryItem, LocalizedString exceptionString, uint locationIdentifier);

		// Token: 0x0600027E RID: 638
		protected abstract AutoDiscoverQuery CreateAutoDiscoverQuery(Uri autoDiscoverUrl, AutoDiscoverQueryItem[] queryItems, int redirectionDepth);

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000C6AE File Offset: 0x0000A8AE
		protected Application Application
		{
			get
			{
				return this.application;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000C6B6 File Offset: 0x0000A8B6
		protected ClientContext ClientContext
		{
			get
			{
				return this.clientContext;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000C6BE File Offset: 0x0000A8BE
		protected RequestLogger RequestLogger
		{
			get
			{
				return this.requestLogger;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000C6C6 File Offset: 0x0000A8C6
		protected AutoDiscoverAuthenticator Authenticator
		{
			get
			{
				return this.authenticator;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000C6CE File Offset: 0x0000A8CE
		protected CreateAutoDiscoverRequestDelegate CreateAutoDiscoverRequest
		{
			get
			{
				return this.createAutoDiscoverRequest;
			}
		}

		// Token: 0x06000284 RID: 644
		protected abstract AutoDiscoverQuery CreateAutoDiscoverQuery(string domain, AutoDiscoverQueryItem[] queryItems, int redirectionDepth);

		// Token: 0x06000285 RID: 645 RVA: 0x0000C6D6 File Offset: 0x0000A8D6
		public override string ToString()
		{
			return "AutoDiscoverQuery for " + this.emailAddressesString;
		}

		// Token: 0x0400018E RID: 398
		private const string AutoDiscoverQueryBeginInvokeMarker = "ADQBI_";

		// Token: 0x0400018F RID: 399
		private const string AutoDiscoverQueryCompleteMarker = "ADQC_";

		// Token: 0x04000190 RID: 400
		private const int MaximumAllowedRedirections = 3;

		// Token: 0x04000191 RID: 401
		private Application application;

		// Token: 0x04000192 RID: 402
		private ClientContext clientContext;

		// Token: 0x04000193 RID: 403
		private RequestLogger requestLogger;

		// Token: 0x04000194 RID: 404
		private AutoDiscoverQueryItem[] queryItems;

		// Token: 0x04000195 RID: 405
		private int redirectionDepth;

		// Token: 0x04000196 RID: 406
		private AutoDiscoverAuthenticator authenticator;

		// Token: 0x04000197 RID: 407
		private Uri autoDiscoverUrl;

		// Token: 0x04000198 RID: 408
		private EmailAddress[] emailAddresses;

		// Token: 0x04000199 RID: 409
		private string emailAddressesString;

		// Token: 0x0400019A RID: 410
		private CreateAutoDiscoverRequestDelegate createAutoDiscoverRequest;

		// Token: 0x0400019B RID: 411
		private AutodiscoverType autodiscoverType;

		// Token: 0x0400019C RID: 412
		protected QueryList queryList;

		// Token: 0x0400019D RID: 413
		private AutoDiscoverRequestOperation pendingRequest;

		// Token: 0x0400019E RID: 414
		private AutoDiscoverQuery[] redirectRequests;

		// Token: 0x0400019F RID: 415
		private AsyncTask pendingRedirectRequests;

		// Token: 0x040001A0 RID: 416
		private Guid serverRequestId;

		// Token: 0x040001A1 RID: 417
		private Stopwatch timeTaken;

		// Token: 0x040001A2 RID: 418
		private static readonly AutoDiscoverResult TimeoutResult = new AutoDiscoverResult(new TimeoutExpiredException("Auto-Discover-Request"));

		// Token: 0x040001A3 RID: 419
		protected static readonly Microsoft.Exchange.Diagnostics.Trace AutoDiscoverTracer = ExTraceGlobals.AutoDiscoverTracer;
	}
}

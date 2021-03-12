using System;
using System.Diagnostics;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000AD RID: 173
	internal sealed class ExternalProxyWebRequestWithAutoDiscover : AsyncRequestWithQueryList
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x0000FBED File Offset: 0x0000DDED
		public ExternalProxyWebRequestWithAutoDiscover(Application application, InternalClientContext clientContext, RequestLogger requestLogger, QueryList queryList, ExternalAuthenticationRequest autoDiscoverExternalAuthenticationRequest, ExternalAuthenticationRequest webProxyExternalAuthenticationRequest, Uri autoDiscoverUrl, SmtpAddress sharingKey, CreateAutoDiscoverRequestDelegate createAutoDiscoverRequest) : base(application, clientContext, RequestType.FederatedCrossForest, requestLogger, queryList)
		{
			this.autoDiscoverExternalAuthenticationRequest = autoDiscoverExternalAuthenticationRequest;
			this.webProxyExternalAuthenticationRequest = webProxyExternalAuthenticationRequest;
			this.autoDiscoverUrl = autoDiscoverUrl;
			this.sharingKey = sharingKey;
			this.createAutoDiscoverRequest = createAutoDiscoverRequest;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000FC24 File Offset: 0x0000DE24
		public override void Abort()
		{
			base.Abort();
			if (this.parallel != null)
			{
				this.parallel.Abort();
			}
			if (this.autoDiscoverQuery != null)
			{
				this.autoDiscoverQuery.Abort();
			}
			if (this.dispatcher != null)
			{
				this.dispatcher.Abort();
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000FC70 File Offset: 0x0000DE70
		public override void BeginInvoke(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			this.parallel = new AsyncTaskParallel(new AsyncTask[]
			{
				this.autoDiscoverExternalAuthenticationRequest,
				this.webProxyExternalAuthenticationRequest
			});
			this.requestTimer = Stopwatch.StartNew();
			Stopwatch stopwatch = Stopwatch.StartNew();
			this.parallel.BeginInvoke(new TaskCompleteCallback(this.Complete1));
			stopwatch.Stop();
			base.QueryList.LogLatency("EPWRADBI", stopwatch.ElapsedMilliseconds);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		private void Complete1(AsyncTask task)
		{
			this.requestTimer.Stop();
			if (!base.Aborted)
			{
				base.QueryList.LogLatency("EPWRADC1", this.requestTimer.ElapsedMilliseconds);
			}
			if (this.autoDiscoverExternalAuthenticationRequest.Exception != null)
			{
				base.SetExceptionInResultList(this.autoDiscoverExternalAuthenticationRequest.Exception);
				base.Complete();
				return;
			}
			if (this.webProxyExternalAuthenticationRequest.Exception != null)
			{
				base.SetExceptionInResultList(this.webProxyExternalAuthenticationRequest.Exception);
				base.Complete();
				return;
			}
			ProxyAuthenticator proxyAuthenticator = ProxyAuthenticator.Create(this.autoDiscoverExternalAuthenticationRequest.RequestedToken, this.sharingKey, base.ClientContext.MessageId);
			this.queryItems = new AutoDiscoverQueryItem[base.QueryList.Count];
			string target = this.autoDiscoverUrl.ToString();
			for (int i = 0; i < base.QueryList.Count; i++)
			{
				base.QueryList[i].Target = target;
				this.queryItems[i] = new AutoDiscoverQueryItem(base.QueryList[i].RecipientData, base.Application.Name, base.QueryList[i]);
			}
			this.autoDiscoverQuery = new AutoDiscoverQueryExternal(base.Application, base.ClientContext, base.RequestLogger, this.autoDiscoverUrl, proxyAuthenticator, this.queryItems, this.createAutoDiscoverRequest, base.QueryList);
			this.autoDiscoverQuery.BeginInvoke(new TaskCompleteCallback(this.Complete2));
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000FE64 File Offset: 0x0000E064
		private void Complete2(AsyncTask task)
		{
			ProxyAuthenticator proxyAuthenticator = ProxyAuthenticator.Create(this.webProxyExternalAuthenticationRequest.RequestedToken, this.sharingKey, base.ClientContext.MessageId);
			this.dispatcher = new DispatcherWithAutoDiscoverResults(base.Application, base.QueryList, this.queryItems, proxyAuthenticator, RequestType.FederatedCrossForest, new DispatcherWithAutoDiscoverResults.CreateRequestWithQueryListDelegate(this.CreateRequest));
			this.dispatcher.BeginInvoke(new TaskCompleteCallback(this.Complete3));
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000FED8 File Offset: 0x0000E0D8
		private AsyncRequest CreateRequest(QueryList queryList, ProxyAuthenticator proxyAuthenticator, WebServiceUri webServiceUri, UriSource source)
		{
			return new ProxyWebRequest(base.Application, base.ClientContext, RequestType.FederatedCrossForest, base.RequestLogger, queryList, TargetServerVersion.Unknown, proxyAuthenticator, webServiceUri, source);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000FF03 File Offset: 0x0000E103
		private void Complete3(AsyncTask task)
		{
			base.Complete();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000FF0C File Offset: 0x0000E10C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"ExternalProxyWebRequestWithAutoDiscover for ",
				base.QueryList.Count,
				" mailboxes to ",
				this.autoDiscoverUrl
			});
		}

		// Token: 0x0400024C RID: 588
		public const string ExternalProxyWebRequestWithAutoDiscoverBeginInvokeMarker = "EPWRADBI";

		// Token: 0x0400024D RID: 589
		public const string ExternalProxyWebRequestWithAutoDiscoverComplete1Marker = "EPWRADC1";

		// Token: 0x0400024E RID: 590
		private ExternalAuthenticationRequest autoDiscoverExternalAuthenticationRequest;

		// Token: 0x0400024F RID: 591
		private ExternalAuthenticationRequest webProxyExternalAuthenticationRequest;

		// Token: 0x04000250 RID: 592
		private AutoDiscoverQueryItem[] queryItems;

		// Token: 0x04000251 RID: 593
		private AutoDiscoverQuery autoDiscoverQuery;

		// Token: 0x04000252 RID: 594
		private DispatcherWithAutoDiscoverResults dispatcher;

		// Token: 0x04000253 RID: 595
		private AsyncTaskParallel parallel;

		// Token: 0x04000254 RID: 596
		private SmtpAddress sharingKey;

		// Token: 0x04000255 RID: 597
		private Uri autoDiscoverUrl;

		// Token: 0x04000256 RID: 598
		private CreateAutoDiscoverRequestDelegate createAutoDiscoverRequest;

		// Token: 0x04000257 RID: 599
		private Stopwatch requestTimer;
	}
}

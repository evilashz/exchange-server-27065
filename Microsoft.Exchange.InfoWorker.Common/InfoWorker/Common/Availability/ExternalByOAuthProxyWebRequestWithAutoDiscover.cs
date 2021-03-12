using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000AA RID: 170
	internal sealed class ExternalByOAuthProxyWebRequestWithAutoDiscover : AsyncRequestWithQueryList
	{
		// Token: 0x060003BC RID: 956 RVA: 0x0000F654 File Offset: 0x0000D854
		public ExternalByOAuthProxyWebRequestWithAutoDiscover(Application application, InternalClientContext clientContext, RequestLogger requestLogger, QueryList queryList, Uri autoDiscoverUrl, CreateAutoDiscoverRequestDelegate createAutoDiscoverRequest) : base(application, clientContext, RequestType.FederatedCrossForest, requestLogger, queryList)
		{
			this.autoDiscoverUrl = autoDiscoverUrl;
			this.createAutoDiscoverRequest = createAutoDiscoverRequest;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000F672 File Offset: 0x0000D872
		public override void Abort()
		{
			base.Abort();
			if (this.autoDiscoverQuery != null)
			{
				this.autoDiscoverQuery.Abort();
			}
			if (this.dispatcher != null)
			{
				this.dispatcher.Abort();
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
		public override void BeginInvoke(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			ProxyAuthenticator proxyAuthenticator = ProxyAuthenticator.Create(OAuthCredentialsFactory.CreateAsApp(base.ClientContext as InternalClientContext, base.RequestLogger), base.ClientContext.MessageId, true);
			this.queryItems = AutoDiscoverQueryItem.CreateAutoDiscoverQueryItems(base.Application, base.QueryList, this.autoDiscoverUrl);
			this.autoDiscoverQuery = new AutoDiscoverQueryExternalByOAuth(base.Application, base.ClientContext, base.RequestLogger, this.autoDiscoverUrl, proxyAuthenticator, this.queryItems, this.createAutoDiscoverRequest, base.QueryList);
			this.autoDiscoverQuery.BeginInvoke(new TaskCompleteCallback(this.Complete1));
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000F748 File Offset: 0x0000D948
		private void Complete1(AsyncTask task)
		{
			ProxyAuthenticator proxyAuthenticator = ProxyAuthenticator.Create(OAuthCredentialsFactory.Create(base.ClientContext as InternalClientContext, base.RequestLogger), base.ClientContext.MessageId, false);
			this.dispatcher = new DispatcherWithAutoDiscoverResults(base.Application, base.QueryList, this.queryItems, proxyAuthenticator, RequestType.FederatedCrossForest, new DispatcherWithAutoDiscoverResults.CreateRequestWithQueryListDelegate(this.CreateRequest));
			this.dispatcher.BeginInvoke(new TaskCompleteCallback(this.Complete2));
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		private AsyncRequest CreateRequest(QueryList queryList, ProxyAuthenticator proxyAuthenticator, WebServiceUri webServiceUri, UriSource source)
		{
			return new ProxyWebRequest(base.Application, base.ClientContext, RequestType.FederatedCrossForest, base.RequestLogger, queryList, TargetServerVersion.Unknown, proxyAuthenticator, webServiceUri, source);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000F7EB File Offset: 0x0000D9EB
		private void Complete2(AsyncTask task)
		{
			base.Complete();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000F7F4 File Offset: 0x0000D9F4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"ExternalByOAuthProxyWebRequestWithAutoDiscover for ",
				base.QueryList.Count,
				" mailboxes to ",
				this.autoDiscoverUrl
			});
		}

		// Token: 0x04000237 RID: 567
		private AutoDiscoverQueryItem[] queryItems;

		// Token: 0x04000238 RID: 568
		private AutoDiscoverQuery autoDiscoverQuery;

		// Token: 0x04000239 RID: 569
		private DispatcherWithAutoDiscoverResults dispatcher;

		// Token: 0x0400023A RID: 570
		private Uri autoDiscoverUrl;

		// Token: 0x0400023B RID: 571
		private CreateAutoDiscoverRequestDelegate createAutoDiscoverRequest;
	}
}

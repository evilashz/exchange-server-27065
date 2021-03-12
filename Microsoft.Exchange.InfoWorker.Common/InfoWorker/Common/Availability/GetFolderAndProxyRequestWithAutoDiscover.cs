using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000AF RID: 175
	internal sealed class GetFolderAndProxyRequestWithAutoDiscover : AsyncRequestWithQueryList, IDisposable
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x00010349 File Offset: 0x0000E549
		public GetFolderAndProxyRequestWithAutoDiscover(Application application, InternalClientContext clientContext, RequestLogger requestLogger, QueryList queryList, TargetForestConfiguration targetForestConfiguration) : base(application, clientContext, RequestType.CrossForest, requestLogger, queryList)
		{
			this.targetForestConfiguration = targetForestConfiguration;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001035F File Offset: 0x0000E55F
		public void Dispose()
		{
			if (this.autoDiscoverQuery != null)
			{
				this.autoDiscoverQuery.Dispose();
			}
			if (this.dispatcher != null)
			{
				this.dispatcher.Dispose();
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00010387 File Offset: 0x0000E587
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

		// Token: 0x060003E7 RID: 999 RVA: 0x000103B8 File Offset: 0x0000E5B8
		public override void BeginInvoke(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			this.queryItems = AutoDiscoverQueryItem.CreateAutoDiscoverQueryItems(base.Application, base.QueryList, this.targetForestConfiguration.AutoDiscoverUrl);
			this.autoDiscoverQuery = new AutoDiscoverQueryInternal(base.Application, base.ClientContext, base.RequestLogger, this.targetForestConfiguration, this.queryItems, new CreateAutoDiscoverRequestDelegate(AutoDiscoverRequestXmlByUser.Create), base.QueryList);
			this.autoDiscoverQuery.BeginInvoke(new TaskCompleteCallback(this.Complete1));
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00010440 File Offset: 0x0000E640
		private void Complete1(AsyncTask task)
		{
			this.dispatcher = new DispatcherWithAutoDiscoverResults(base.Application, base.QueryList, this.queryItems, null, RequestType.CrossForest, new DispatcherWithAutoDiscoverResults.CreateRequestWithQueryListDelegate(this.CreateRequest));
			this.dispatcher.BeginInvoke(new TaskCompleteCallback(this.Complete2));
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001048F File Offset: 0x0000E68F
		private AsyncRequestWithQueryList CreateRequest(QueryList queryList, ProxyAuthenticator proxyAuthenticator, WebServiceUri webServiceUri, UriSource source)
		{
			return new GetFolderAndProxyRequest(base.Application, (InternalClientContext)base.ClientContext, RequestType.CrossForest, base.RequestLogger, queryList, TargetServerVersion.Unknown, proxyAuthenticator, webServiceUri);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000104B2 File Offset: 0x0000E6B2
		private void Complete2(AsyncTask task)
		{
			base.Complete();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000104BA File Offset: 0x0000E6BA
		public override string ToString()
		{
			return "GetFolderAndProxyRequestWithAutoDiscover for " + base.QueryList.Count + " mailboxes";
		}

		// Token: 0x0400025F RID: 607
		private TargetForestConfiguration targetForestConfiguration;

		// Token: 0x04000260 RID: 608
		private AutoDiscoverQuery autoDiscoverQuery;

		// Token: 0x04000261 RID: 609
		private AutoDiscoverQueryItem[] queryItems;

		// Token: 0x04000262 RID: 610
		private DispatcherWithAutoDiscoverResults dispatcher;
	}
}

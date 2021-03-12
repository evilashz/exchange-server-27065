using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000B6 RID: 182
	internal sealed class ProxyWebRequestWithAutoDiscover : AsyncRequestWithQueryList, IDisposable
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x000116BB File Offset: 0x0000F8BB
		public ProxyWebRequestWithAutoDiscover(Application application, ClientContext clientContext, RequestLogger requestLogger, QueryList queryList, TargetForestConfiguration targetForestConfiguration, CreateAutoDiscoverRequestDelegate createAutoDiscoverRequest) : base(application, clientContext, RequestType.CrossForest, requestLogger, queryList)
		{
			this.targetForestConfiguration = targetForestConfiguration;
			this.createAutoDiscoverRequest = createAutoDiscoverRequest;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000116D9 File Offset: 0x0000F8D9
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

		// Token: 0x0600041D RID: 1053 RVA: 0x00011707 File Offset: 0x0000F907
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

		// Token: 0x0600041E RID: 1054 RVA: 0x00011730 File Offset: 0x0000F930
		public override void BeginInvoke(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			this.queryItems = AutoDiscoverQueryItem.CreateAutoDiscoverQueryItems(base.Application, base.QueryList, this.targetForestConfiguration.AutoDiscoverUrl);
			this.autoDiscoverQuery = new AutoDiscoverQueryInternal(base.Application, base.ClientContext, base.RequestLogger, this.targetForestConfiguration, this.queryItems, this.createAutoDiscoverRequest, base.QueryList);
			this.requestTimer = Stopwatch.StartNew();
			this.autoDiscoverQuery.BeginInvoke(new TaskCompleteCallback(this.Complete1));
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000117C0 File Offset: 0x0000F9C0
		private void Complete1(AsyncTask task)
		{
			this.requestTimer.Stop();
			if (!base.Aborted)
			{
				base.QueryList.LogLatency("PWRADC1", this.requestTimer.ElapsedMilliseconds);
			}
			this.dispatcher = new DispatcherWithAutoDiscoverResults(base.Application, base.QueryList, this.queryItems, null, RequestType.CrossForest, new DispatcherWithAutoDiscoverResults.CreateRequestWithQueryListDelegate(this.CreateRequest));
			this.dispatcher.BeginInvoke(new TaskCompleteCallback(this.Complete2));
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00011840 File Offset: 0x0000FA40
		private AsyncRequest CreateRequest(QueryList queryList, ProxyAuthenticator proxyAuthenticator, WebServiceUri webServiceUri, UriSource source)
		{
			return new ProxyWebRequest(base.Application, base.ClientContext, RequestType.CrossForest, base.RequestLogger, queryList, TargetServerVersion.Unknown, proxyAuthenticator, webServiceUri, source);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001186B File Offset: 0x0000FA6B
		private void Complete2(AsyncTask task)
		{
			base.Complete();
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00011873 File Offset: 0x0000FA73
		public override string ToString()
		{
			return "ProxyWebRequestWithAutoDiscover for " + base.QueryList.Count + " mailboxes";
		}

		// Token: 0x0400028C RID: 652
		private const string ProxyWebRequestWithAutoDiscoveryComplete1Marker = "PWRADC1";

		// Token: 0x0400028D RID: 653
		private TargetForestConfiguration targetForestConfiguration;

		// Token: 0x0400028E RID: 654
		private AutoDiscoverQuery autoDiscoverQuery;

		// Token: 0x0400028F RID: 655
		private AutoDiscoverQueryItem[] queryItems;

		// Token: 0x04000290 RID: 656
		private DispatcherWithAutoDiscoverResults dispatcher;

		// Token: 0x04000291 RID: 657
		private CreateAutoDiscoverRequestDelegate createAutoDiscoverRequest;

		// Token: 0x04000292 RID: 658
		private Stopwatch requestTimer;

		// Token: 0x04000293 RID: 659
		private static readonly Microsoft.Exchange.Diagnostics.Trace RequestRoutingTracer = ExTraceGlobals.RequestRoutingTracer;
	}
}

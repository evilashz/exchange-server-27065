using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000A9 RID: 169
	internal sealed class ExternalByOAuthProxyWebRequest : AsyncRequestWithQueryList
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x0000F53F File Offset: 0x0000D73F
		public ExternalByOAuthProxyWebRequest(Application application, ClientContext clientContext, RequestLogger requestLogger, QueryList queryList, WebServiceUri webServiceUri) : base(application, clientContext, RequestType.FederatedCrossForest, requestLogger, queryList)
		{
			this.webServiceUri = webServiceUri;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000F555 File Offset: 0x0000D755
		public override void Abort()
		{
			base.Abort();
			if (this.proxyWebRequest != null)
			{
				this.proxyWebRequest.Abort();
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000F570 File Offset: 0x0000D770
		public override void BeginInvoke(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			ProxyAuthenticator proxyAuthenticator = ProxyAuthenticator.Create(OAuthCredentialsFactory.Create(base.ClientContext as InternalClientContext, base.RequestLogger), base.ClientContext.MessageId, false);
			this.proxyWebRequest = new ProxyWebRequest(base.Application, base.ClientContext, RequestType.FederatedCrossForest, base.RequestLogger, base.QueryList, TargetServerVersion.Unknown, proxyAuthenticator, this.webServiceUri, this.webServiceUri.Source);
			this.proxyWebRequest.BeginInvoke(new TaskCompleteCallback(this.CompleteWebRequest));
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000F5FA File Offset: 0x0000D7FA
		private void CompleteWebRequest(AsyncTask task)
		{
			base.Complete();
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000F604 File Offset: 0x0000D804
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"ExternalByOAuthProxyWebRequest for ",
				base.QueryList.Count,
				" mailboxes at ",
				this.webServiceUri.Uri.OriginalString
			});
		}

		// Token: 0x04000235 RID: 565
		private ProxyWebRequest proxyWebRequest;

		// Token: 0x04000236 RID: 566
		private readonly WebServiceUri webServiceUri;
	}
}

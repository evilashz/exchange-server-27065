using System;
using System.Diagnostics;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000AB RID: 171
	internal sealed class ExternalProxyWebRequest : AsyncRequestWithQueryList
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x0000F83A File Offset: 0x0000DA3A
		public ExternalProxyWebRequest(Application application, ClientContext clientContext, RequestLogger requestLogger, QueryList queryList, ExternalAuthenticationRequest externalAuthenticationRequest, WebServiceUri webServiceUri, SmtpAddress sharingKey) : base(application, clientContext, RequestType.FederatedCrossForest, requestLogger, queryList)
		{
			this.externalAuthenticationRequest = externalAuthenticationRequest;
			this.webServiceUri = webServiceUri;
			this.sharingKey = sharingKey;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000F860 File Offset: 0x0000DA60
		public override void Abort()
		{
			base.Abort();
			this.externalAuthenticationRequest.Abort();
			if (this.proxyWebRequest != null)
			{
				this.proxyWebRequest.Abort();
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000F888 File Offset: 0x0000DA88
		public override void BeginInvoke(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			Stopwatch stopwatch = Stopwatch.StartNew();
			this.externalAuthenticationRequest.BeginInvoke(new TaskCompleteCallback(this.CompleteAuthenticator));
			stopwatch.Stop();
			base.QueryList.LogLatency("EPRBI", stopwatch.ElapsedMilliseconds);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
		private void CompleteAuthenticator(AsyncTask task)
		{
			if (this.externalAuthenticationRequest.Exception != null)
			{
				base.SetExceptionInResultList(this.externalAuthenticationRequest.Exception);
				base.Complete();
				return;
			}
			ProxyAuthenticator proxyAuthenticator = ProxyAuthenticator.Create(this.externalAuthenticationRequest.RequestedToken, this.sharingKey, base.ClientContext.MessageId);
			this.proxyWebRequest = new ProxyWebRequest(base.Application, base.ClientContext, RequestType.FederatedCrossForest, base.RequestLogger, base.QueryList, TargetServerVersion.Unknown, proxyAuthenticator, this.webServiceUri, this.webServiceUri.Source);
			this.proxyWebRequest.BeginInvoke(new TaskCompleteCallback(this.CompleteWebRequest));
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000F97A File Offset: 0x0000DB7A
		private void CompleteWebRequest(AsyncTask task)
		{
			base.Complete();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000F984 File Offset: 0x0000DB84
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"ExternalProxyWebRequest for ",
				base.QueryList.Count,
				" mailboxes at ",
				this.webServiceUri.Uri.OriginalString,
				" with ",
				this.externalAuthenticationRequest.ToString()
			});
		}

		// Token: 0x0400023C RID: 572
		public const string ExternalProxyWebReqeustBeginInvokeMarker = "EPRBI";

		// Token: 0x0400023D RID: 573
		private ExternalAuthenticationRequest externalAuthenticationRequest;

		// Token: 0x0400023E RID: 574
		private ProxyWebRequest proxyWebRequest;

		// Token: 0x0400023F RID: 575
		private WebServiceUri webServiceUri;

		// Token: 0x04000240 RID: 576
		private SmtpAddress sharingKey;
	}
}

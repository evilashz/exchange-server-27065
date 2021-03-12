using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000062 RID: 98
	internal sealed class AutoDiscoverRequestByUser : AutoDiscoverRequestOperation
	{
		// Token: 0x0600024E RID: 590 RVA: 0x0000B5CE File Offset: 0x000097CE
		public static AutoDiscoverRequestOperation Create(Application application, ClientContext clientContext, RequestLogger requestLogger, Uri targetUri, AutoDiscoverAuthenticator authenticator, EmailAddress[] emailAddresses, UriSource uriSource, AutodiscoverType autodiscoverType)
		{
			return new AutoDiscoverRequestByUser(application, clientContext, requestLogger, targetUri, authenticator, emailAddresses, autodiscoverType);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000B5DF File Offset: 0x000097DF
		private AutoDiscoverRequestByUser(Application application, ClientContext clientContext, RequestLogger requestLogger, Uri targetUri, AutoDiscoverAuthenticator authenticator, EmailAddress[] emailAddresses, AutodiscoverType autodiscoverType) : base(application, clientContext, requestLogger, targetUri, authenticator, emailAddresses, autodiscoverType)
		{
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B5F2 File Offset: 0x000097F2
		public override void Abort()
		{
			base.Abort();
			if (this.request != null)
			{
				this.request.Abort();
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000B60D File Offset: 0x0000980D
		public override void Dispose()
		{
			base.Dispose();
			if (this.request != null)
			{
				this.request.Dispose();
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B628 File Offset: 0x00009828
		public override void BeginInvoke(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			this.request = new UserSoapAutoDiscoverRequest(base.Application, base.ClientContext, RequestType.FederatedCrossForest, base.RequestLogger, base.Authenticator, base.TargetUri, base.EmailAddresses, base.AutodiscoverType);
			this.request.BeginInvoke(new TaskCompleteCallback(this.CompleteRequest));
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B689 File Offset: 0x00009889
		private void CompleteRequest(AsyncTask task)
		{
			base.HandleResultsAndCompleteRequest(this.request);
		}

		// Token: 0x04000183 RID: 387
		private UserSoapAutoDiscoverRequest request;
	}
}

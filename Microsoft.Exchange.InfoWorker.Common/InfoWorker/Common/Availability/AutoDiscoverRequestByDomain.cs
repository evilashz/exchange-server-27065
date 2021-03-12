using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000061 RID: 97
	internal sealed class AutoDiscoverRequestByDomain : AutoDiscoverRequestOperation
	{
		// Token: 0x06000247 RID: 583 RVA: 0x0000B4E4 File Offset: 0x000096E4
		public static AutoDiscoverRequestOperation CreateForCrossForest(Application application, ClientContext clientContext, RequestLogger requestLogger, Uri targetUri, AutoDiscoverAuthenticator authenticator, EmailAddress[] emailAddresses, UriSource uriSource, AutodiscoverType autodiscoverType)
		{
			return new AutoDiscoverRequestByDomain(application, clientContext, requestLogger, new AutoDiscoverAuthenticator(ProxyAuthenticator.CreateForSoap(clientContext.MessageId)), targetUri, emailAddresses, autodiscoverType);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000B503 File Offset: 0x00009703
		public static AutoDiscoverRequestOperation CreateForExternal(Application application, ClientContext clientContext, RequestLogger requestLogger, Uri targetUri, AutoDiscoverAuthenticator authenticator, EmailAddress[] emailAddresses, UriSource uriSource, AutodiscoverType autodiscoverType)
		{
			return new AutoDiscoverRequestByDomain(application, clientContext, requestLogger, authenticator, targetUri, emailAddresses, autodiscoverType);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000B514 File Offset: 0x00009714
		private AutoDiscoverRequestByDomain(Application application, ClientContext clientContext, RequestLogger requestLogger, AutoDiscoverAuthenticator authenticator, Uri targetUri, EmailAddress[] emailAddresses, AutodiscoverType autodiscoverType) : base(application, clientContext, requestLogger, targetUri, authenticator, emailAddresses, autodiscoverType)
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000B527 File Offset: 0x00009727
		public override void Abort()
		{
			base.Abort();
			if (this.domainSoapAutoDiscoverRequest != null)
			{
				this.domainSoapAutoDiscoverRequest.Abort();
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000B542 File Offset: 0x00009742
		public override void Dispose()
		{
			base.Dispose();
			if (this.domainSoapAutoDiscoverRequest != null)
			{
				this.domainSoapAutoDiscoverRequest.Dispose();
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000B560 File Offset: 0x00009760
		public override void BeginInvoke(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			this.domainSoapAutoDiscoverRequest = new DomainSoapAutoDiscoverRequest(base.Application, base.ClientContext, base.RequestLogger, base.Authenticator, base.TargetUri, base.EmailAddresses, base.AutodiscoverType);
			this.domainSoapAutoDiscoverRequest.BeginInvoke(new TaskCompleteCallback(this.CompleteRequest));
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000B5C0 File Offset: 0x000097C0
		private void CompleteRequest(AsyncTask task)
		{
			base.HandleResultsAndCompleteRequest(this.domainSoapAutoDiscoverRequest);
		}

		// Token: 0x04000182 RID: 386
		private DomainSoapAutoDiscoverRequest domainSoapAutoDiscoverRequest;
	}
}

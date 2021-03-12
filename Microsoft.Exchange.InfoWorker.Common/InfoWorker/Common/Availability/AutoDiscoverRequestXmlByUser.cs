using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000063 RID: 99
	internal sealed class AutoDiscoverRequestXmlByUser : AutoDiscoverRequestOperation
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000B697 File Offset: 0x00009897
		public static AutoDiscoverRequestOperation Create(Application application, ClientContext clientContext, RequestLogger requestLogger, Uri targetUri, AutoDiscoverAuthenticator authenticator, EmailAddress[] emailAddresses, UriSource uriSource, AutodiscoverType autodiscoverType)
		{
			return new AutoDiscoverRequestXmlByUser(application, clientContext, requestLogger, targetUri, authenticator, emailAddresses, uriSource, autodiscoverType);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B6AA File Offset: 0x000098AA
		private AutoDiscoverRequestXmlByUser(Application application, ClientContext clientContext, RequestLogger requestLogger, Uri targetUri, AutoDiscoverAuthenticator authenticator, EmailAddress[] emailAddresses, UriSource uriSource, AutodiscoverType autodiscoverType) : base(application, clientContext, requestLogger, targetUri, authenticator, emailAddresses, autodiscoverType)
		{
			this.uriSource = uriSource;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B6C5 File Offset: 0x000098C5
		public override void Abort()
		{
			base.Abort();
			if (this.parallel != null)
			{
				this.parallel.Abort();
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B6E0 File Offset: 0x000098E0
		public override void BeginInvoke(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			this.requests = new AutoDiscoverRequest[base.EmailAddresses.Length];
			for (int i = 0; i < base.EmailAddresses.Length; i++)
			{
				this.requests[i] = this.GetRequest(base.EmailAddresses[i]);
			}
			this.parallel = new AsyncTaskParallel(this.requests);
			this.parallel.BeginInvoke(new TaskCompleteCallback(this.CompleteRequest));
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B758 File Offset: 0x00009958
		private void CompleteRequest(AsyncTask task)
		{
			AutoDiscoverRequestResult[] array = new AutoDiscoverRequestResult[this.requests.Length];
			for (int i = 0; i < this.requests.Length; i++)
			{
				array[i] = this.GetResult(this.requests[i]);
			}
			base.HandleResultsAndCompleteRequest(null, array);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B79F File Offset: 0x0000999F
		private AutoDiscoverRequest GetRequest(EmailAddress emailAddress)
		{
			return new AutoDiscoverRequest(base.Application, base.ClientContext, base.RequestLogger, base.TargetUri, emailAddress, base.Authenticator.Credentials, this.uriSource);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B7D0 File Offset: 0x000099D0
		private AutoDiscoverRequestResult GetResult(AutoDiscoverRequest request)
		{
			return request.Result;
		}

		// Token: 0x04000184 RID: 388
		private UriSource uriSource;

		// Token: 0x04000185 RID: 389
		private AutoDiscoverRequest[] requests;

		// Token: 0x04000186 RID: 390
		private AsyncTask parallel;
	}
}

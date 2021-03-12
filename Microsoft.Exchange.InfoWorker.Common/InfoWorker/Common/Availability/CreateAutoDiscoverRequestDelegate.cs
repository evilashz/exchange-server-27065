using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000065 RID: 101
	// (Invoke) Token: 0x0600026D RID: 621
	internal delegate AutoDiscoverRequestOperation CreateAutoDiscoverRequestDelegate(Application application, ClientContext clientContext, RequestLogger requestLogger, Uri targetUri, AutoDiscoverAuthenticator authenticator, EmailAddress[] emailAddresses, UriSource uriSource, AutodiscoverType autodiscoverType);
}

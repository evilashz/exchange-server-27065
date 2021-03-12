using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x02000743 RID: 1859
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ILinkedInWebClient
	{
		// Token: 0x0600241A RID: 9242
		void SubscribeDownloadCompletedEvent(EventHandler<DownloadCompleteEventArgs> eventHandler);

		// Token: 0x0600241B RID: 9243
		void Abort(IAsyncResult ar);

		// Token: 0x0600241C RID: 9244
		LinkedInResponse AuthenticateApplication(string url, string requestAuthorizationHeader, TimeSpan requestTimeout, IWebProxy proxy);

		// Token: 0x0600241D RID: 9245
		LinkedInPerson GetProfile(string accessToken, string accessTokenSecret, string fields);

		// Token: 0x0600241E RID: 9246
		IAsyncResult BeginGetLinkedInConnections(string url, string authorizationHeader, TimeSpan requestTimeout, IWebProxy proxy, AsyncCallback callback, object state);

		// Token: 0x0600241F RID: 9247
		LinkedInConnections EndGetLinkedInConnections(IAsyncResult ar);

		// Token: 0x06002420 RID: 9248
		HttpStatusCode RemoveApplicationPermissions(string accessToken, string accessSecret);
	}
}

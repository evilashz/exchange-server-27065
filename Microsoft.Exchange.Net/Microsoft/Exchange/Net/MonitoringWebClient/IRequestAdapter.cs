using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007C4 RID: 1988
	internal interface IRequestAdapter
	{
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002915 RID: 10517
		// (set) Token: 0x06002916 RID: 10518
		TimeSpan RequestTimeout { get; set; }

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002917 RID: 10519
		// (set) Token: 0x06002918 RID: 10520
		IResponseTracker ResponseTracker { get; set; }

		// Token: 0x06002919 RID: 10521
		IAsyncResult BeginGetResponse(HttpWebRequestWrapper request, ExCookieContainer cookieContainer, SslValidationOptions sslValidationOptions, AuthenticationData? authenticationData, int maxRetryCount, AsyncCallback callback, Dictionary<string, object> asyncState);

		// Token: 0x0600291A RID: 10522
		HttpWebResponseWrapper EndGetResponse(IAsyncResult result);

		// Token: 0x0600291B RID: 10523
		void CloseConnections();
	}
}

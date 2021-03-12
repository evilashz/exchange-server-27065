using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x02000051 RID: 81
	internal interface IHomeRealmDiscovery
	{
		// Token: 0x06000228 RID: 552
		IAsyncResult StartRequestChain(object user, AsyncCallback callback, object state);

		// Token: 0x06000229 RID: 553
		IAsyncResult ProcessRequest(IAsyncResult asyncResult, AsyncCallback callback, object state);

		// Token: 0x0600022A RID: 554
		DomainConfig ProcessResponse(IAsyncResult asyncResult);

		// Token: 0x0600022B RID: 555
		string GetLatency();

		// Token: 0x0600022C RID: 556
		void Abort();

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600022D RID: 557
		LiveIdInstanceType Instance { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600022E RID: 558
		string RealmDiscoveryUri { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600022F RID: 559
		string ErrorString { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000230 RID: 560
		string LiveServer { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000231 RID: 561
		string StsTag { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000232 RID: 562
		long Latency { get; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000233 RID: 563
		long SSLConnectionLatency { get; }
	}
}

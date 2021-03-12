using System;
using System.Threading;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200000F RID: 15
	internal interface IServiceClientFactory
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000054 RID: 84
		// (set) Token: 0x06000055 RID: 85
		ICredentialHandler CredentialHandler { get; set; }

		// Token: 0x06000056 RID: 86
		IServiceClient<ISourceDataProvider> CreateSourceDataProvider(Uri serviceEndpoint, CancellationToken cancellationToken);

		// Token: 0x06000057 RID: 87
		IServiceClient<IAutoDiscoverClient> CreateAutoDiscoverClient(Uri serviceEndpoint, CancellationToken cancellationToken);
	}
}

using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001B8 RID: 440
	internal interface IProxyHubSelectorComponent : ITransportComponent
	{
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001449 RID: 5193
		IProxyHubSelector ProxyHubSelector { get; }

		// Token: 0x0600144A RID: 5194
		void SetLoadTimeDependencies(IMailRouter router, ITransportConfiguration configuration);
	}
}

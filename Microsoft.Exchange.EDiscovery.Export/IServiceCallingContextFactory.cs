using System;
using Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000044 RID: 68
	internal interface IServiceCallingContextFactory
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000618 RID: 1560
		// (set) Token: 0x06000619 RID: 1561
		ICredentialHandler CredentialHandler { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600061A RID: 1562
		IServiceCallingContext<DefaultBinding_Autodiscover> AutoDiscoverCallingContext { get; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600061B RID: 1563
		IServiceCallingContext<ExchangeServiceBinding> EwsCallingContext { get; }
	}
}

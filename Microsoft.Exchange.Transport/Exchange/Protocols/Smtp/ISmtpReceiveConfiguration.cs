using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004CE RID: 1230
	internal interface ISmtpReceiveConfiguration
	{
		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x06003895 RID: 14485
		IDiagnosticsConfigProvider DiagnosticsConfiguration { get; }

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x06003896 RID: 14486
		IRoutingConfigProvider RoutingConfiguration { get; }

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x06003897 RID: 14487
		ITransportConfigProvider TransportConfiguration { get; }
	}
}

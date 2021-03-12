using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020006B3 RID: 1715
	[Flags]
	internal enum OutboundConnectorFlagTypes
	{
		// Token: 0x04003614 RID: 13844
		RouteAllMessagesViaOnPremises = 1,
		// Token: 0x04003615 RID: 13845
		TransportRuleScoped = 4096,
		// Token: 0x04003616 RID: 13846
		CloudServicesMailEnabled = 8192,
		// Token: 0x04003617 RID: 13847
		AllAcceptedDomains = 16384
	}
}

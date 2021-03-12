using System;
using System.ComponentModel;
using System.Security.Principal;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001E2 RID: 482
	internal static class WellKnownSids
	{
		// Token: 0x04000A5E RID: 2654
		[Description("MS Exchange\\Partner Servers")]
		public static SecurityIdentifier PartnerServers = new SecurityIdentifier("S-1-9-1419165041-1139599005-3936102811-1022490595-10");

		// Token: 0x04000A5F RID: 2655
		[Description("MS Exchange\\Hub Transport Servers")]
		public static SecurityIdentifier HubTransportServers = new SecurityIdentifier("S-1-9-1419165041-1139599005-3936102811-1022490595-21");

		// Token: 0x04000A60 RID: 2656
		[Description("MS Exchange\\Edge Transport Servers")]
		public static SecurityIdentifier EdgeTransportServers = new SecurityIdentifier("S-1-9-1419165041-1139599005-3936102811-1022490595-22");

		// Token: 0x04000A61 RID: 2657
		[Description("MS Exchange\\Externally Secured Servers")]
		public static SecurityIdentifier ExternallySecuredServers = new SecurityIdentifier("S-1-9-1419165041-1139599005-3936102811-1022490595-23");

		// Token: 0x04000A62 RID: 2658
		[Description("MS Exchange\\Legacy Exchange Servers")]
		public static SecurityIdentifier LegacyExchangeServers = new SecurityIdentifier("S-1-9-1419165041-1139599005-3936102811-1022490595-24");

		// Token: 0x04000A63 RID: 2659
		[Description("MS Exchange\\SiteMailbox Granted Access Members")]
		public static SecurityIdentifier SiteMailboxGrantedAccessMembers = new SecurityIdentifier("S-1-9-1419165041-1139599005-3936102811-1022490595-25");
	}
}

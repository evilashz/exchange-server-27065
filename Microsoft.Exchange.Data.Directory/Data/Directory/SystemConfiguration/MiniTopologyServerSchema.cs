using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004E8 RID: 1256
	internal class MiniTopologyServerSchema : ADObjectSchema
	{
		// Token: 0x040025E2 RID: 9698
		public static readonly ADPropertyDefinition Fqdn = ServerSchema.Fqdn;

		// Token: 0x040025E3 RID: 9699
		public static readonly ADPropertyDefinition VersionNumber = ServerSchema.VersionNumber;

		// Token: 0x040025E4 RID: 9700
		public static readonly ADPropertyDefinition MajorVersion = ServerSchema.MajorVersion;

		// Token: 0x040025E5 RID: 9701
		public static readonly ADPropertyDefinition AdminDisplayVersion = ServerSchema.AdminDisplayVersion;

		// Token: 0x040025E6 RID: 9702
		public static readonly ADPropertyDefinition IsE14OrLater = ServerSchema.IsE14OrLater;

		// Token: 0x040025E7 RID: 9703
		public static readonly ADPropertyDefinition IsExchange2007OrLater = ServerSchema.IsExchange2007OrLater;

		// Token: 0x040025E8 RID: 9704
		public static readonly ADPropertyDefinition IsE15OrLater = ServerSchema.IsE15OrLater;

		// Token: 0x040025E9 RID: 9705
		public static readonly ADPropertyDefinition ServerSite = ServerSchema.ServerSite;

		// Token: 0x040025EA RID: 9706
		public static readonly ADPropertyDefinition ExchangeLegacyDN = ServerSchema.ExchangeLegacyDN;

		// Token: 0x040025EB RID: 9707
		public static readonly ADPropertyDefinition CurrentServerRole = ServerSchema.CurrentServerRole;

		// Token: 0x040025EC RID: 9708
		public static readonly ADPropertyDefinition IsClientAccessServer = ServerSchema.IsClientAccessServer;

		// Token: 0x040025ED RID: 9709
		public static readonly ADPropertyDefinition IsCafeServer = ServerSchema.IsCafeServer;

		// Token: 0x040025EE RID: 9710
		public static readonly ADPropertyDefinition IsHubTransportServer = ServerSchema.IsHubTransportServer;

		// Token: 0x040025EF RID: 9711
		public static readonly ADPropertyDefinition IsEdgeServer = ServerSchema.IsEdgeServer;

		// Token: 0x040025F0 RID: 9712
		public static readonly ADPropertyDefinition IsFrontendTransportServer = ServerSchema.IsFrontendTransportServer;

		// Token: 0x040025F1 RID: 9713
		public static readonly ADPropertyDefinition IsMailboxServer = ServerSchema.IsMailboxServer;

		// Token: 0x040025F2 RID: 9714
		public static readonly ADPropertyDefinition DatabaseAvailabilityGroup = ServerSchema.DatabaseAvailabilityGroup;

		// Token: 0x040025F3 RID: 9715
		public static readonly ADPropertyDefinition ComponentStates = ServerSchema.ComponentStates;

		// Token: 0x040025F4 RID: 9716
		public static readonly ADPropertyDefinition HomeRoutingGroup = ServerSchema.HomeRoutingGroup;

		// Token: 0x040025F5 RID: 9717
		public static readonly ADPropertyDefinition ExternalPostmasterAddress = ServerSchema.ExternalPostmasterAddress;

		// Token: 0x040025F6 RID: 9718
		public static readonly ADPropertyDefinition IsOutOfService = ActiveDirectoryServerSchema.IsOutOfService;
	}
}

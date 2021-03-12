using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004E4 RID: 1252
	internal class MiniReceiveConnectorSchema : ADObjectSchema
	{
		// Token: 0x040025D2 RID: 9682
		public static readonly ADPropertyDefinition Server = ReceiveConnectorSchema.Server;

		// Token: 0x040025D3 RID: 9683
		public static readonly ADPropertyDefinition SecurityFlags = ReceiveConnectorSchema.SecurityFlags;

		// Token: 0x040025D4 RID: 9684
		public static readonly ADPropertyDefinition Bindings = ReceiveConnectorSchema.Bindings;

		// Token: 0x040025D5 RID: 9685
		public static readonly ADPropertyDefinition AdvertiseClientSettings = ReceiveConnectorSchema.AdvertiseClientSettings;

		// Token: 0x040025D6 RID: 9686
		public static readonly ADPropertyDefinition Fqdn = ReceiveConnectorSchema.Fqdn;

		// Token: 0x040025D7 RID: 9687
		public static readonly ADPropertyDefinition ServiceDiscoveryFqdn = ReceiveConnectorSchema.ServiceDiscoveryFqdn;
	}
}

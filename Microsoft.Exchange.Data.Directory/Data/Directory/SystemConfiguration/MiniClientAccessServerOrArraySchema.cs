using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004DE RID: 1246
	internal class MiniClientAccessServerOrArraySchema : ADObjectSchema
	{
		// Token: 0x040025BC RID: 9660
		public static readonly ADPropertyDefinition Fqdn = ServerSchema.Fqdn;

		// Token: 0x040025BD RID: 9661
		public static readonly ADPropertyDefinition ExchangeLegacyDN = ServerSchema.ExchangeLegacyDN;

		// Token: 0x040025BE RID: 9662
		public static readonly ADPropertyDefinition Site = ServerSchema.ServerSite;

		// Token: 0x040025BF RID: 9663
		public static readonly ADPropertyDefinition IsClientAccessArray = new ADPropertyDefinition("IsClientAccessArray", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.ObjectClass
		}, null, new GetterDelegate(ClientAccessArray.IsClientAccessArrayGetter), null, null, null);

		// Token: 0x040025C0 RID: 9664
		public static readonly ADPropertyDefinition IsClientAccessServer = ServerSchema.IsClientAccessServer;
	}
}

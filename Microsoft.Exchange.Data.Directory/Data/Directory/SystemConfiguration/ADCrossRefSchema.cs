using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200032B RID: 811
	internal sealed class ADCrossRefSchema : ADNonExchangeObjectSchema
	{
		// Token: 0x04001705 RID: 5893
		public static readonly ADPropertyDefinition NCName = new ADPropertyDefinition("NCName", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "nCName", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001706 RID: 5894
		public static readonly ADPropertyDefinition DnsRoot = new ADPropertyDefinition("DnsRoot", ExchangeObjectVersion.Exchange2003, typeof(string), "dnsRoot", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001707 RID: 5895
		public static readonly ADPropertyDefinition NetBiosName = new ADPropertyDefinition("NetBiosName", ExchangeObjectVersion.Exchange2003, typeof(string), "netBiosName", ADPropertyDefinitionFlags.ReadOnly, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001708 RID: 5896
		public static readonly ADPropertyDefinition TrustParent = new ADPropertyDefinition("TrustParent", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "trustParent", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}

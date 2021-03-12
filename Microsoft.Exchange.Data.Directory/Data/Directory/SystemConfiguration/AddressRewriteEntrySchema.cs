using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000341 RID: 833
	internal sealed class AddressRewriteEntrySchema : ADConfigurationObjectSchema
	{
		// Token: 0x040017AD RID: 6061
		public static readonly ADPropertyDefinition InternalAddress = new ADPropertyDefinition("InternalAddress", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchAddressRewriteInternalName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017AE RID: 6062
		public static readonly ADPropertyDefinition ExternalAddress = new ADPropertyDefinition("ExternalAddress", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchAddressRewriteExternalName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017AF RID: 6063
		public static readonly ADPropertyDefinition ExceptionList = new ADPropertyDefinition("ExceptionList", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchAddressRewriteExceptionList", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017B0 RID: 6064
		public static readonly ADPropertyDefinition Direction = new ADPropertyDefinition("Direction", ExchangeObjectVersion.Exchange2007, typeof(EntryDirection), "msExchAddressRewriteMappingType", ADPropertyDefinitionFlags.None, EntryDirection.Bidirectional, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}

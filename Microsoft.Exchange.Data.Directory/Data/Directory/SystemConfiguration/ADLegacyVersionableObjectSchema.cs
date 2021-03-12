using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002D1 RID: 721
	internal abstract class ADLegacyVersionableObjectSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040013F0 RID: 5104
		public static readonly ADPropertyDefinition MinAdminVersion = new ADPropertyDefinition("MinAdminVersion", ExchangeObjectVersion.Exchange2003, typeof(int?), "msExchMinAdminVersion", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}

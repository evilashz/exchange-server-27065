using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000496 RID: 1174
	internal class LegacyGwartSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002417 RID: 9239
		public static readonly ADPropertyDefinition GwartLastModified = new ADPropertyDefinition("GwartLastModified", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), DateTimeFormatProvider.UTC, "gWARTLastModified", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);
	}
}

using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000564 RID: 1380
	internal class ResourceBookingConfigSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002A0B RID: 10763
		public static readonly ADPropertyDefinition ResourcePropertySchema = new ADPropertyDefinition("ResourcePropertySchema", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchResourcePropertySchema", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ResourcePropertyConstraint()
		}, null, null);
	}
}

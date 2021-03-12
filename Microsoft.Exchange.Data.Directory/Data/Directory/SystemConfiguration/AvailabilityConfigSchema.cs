using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003B1 RID: 945
	internal sealed class AvailabilityConfigSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040019F2 RID: 6642
		public static readonly ADPropertyDefinition PerUserAccount = new ADPropertyDefinition("PerUserAccount", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchAvailabilityPerUserAccount", ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040019F3 RID: 6643
		public static readonly ADPropertyDefinition OrgWideAccount = new ADPropertyDefinition("OrgWideAccount", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchAvailabilityOrgWideAccount", ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}

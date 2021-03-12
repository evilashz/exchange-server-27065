using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x02000104 RID: 260
	internal class TenantIPFilterSettingSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04000543 RID: 1347
		public static readonly ADPropertyDefinition AllowedIPRanges = new ADPropertyDefinition("AllowedIPRanges", ExchangeObjectVersion.Exchange2007, typeof(IPRange), "AllowedIPRanges", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04000544 RID: 1348
		public static readonly ADPropertyDefinition BlockedIPRanges = new ADPropertyDefinition("BlockedIPRanges", ExchangeObjectVersion.Exchange2007, typeof(IPRange), "BlockedIPRanges", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}

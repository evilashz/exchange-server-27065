using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000468 RID: 1128
	internal sealed class ExtendedRightSchema : ADNonExchangeObjectSchema
	{
		// Token: 0x0400228C RID: 8844
		public static readonly ADPropertyDefinition RightsGuid = new ADPropertyDefinition("RightsGuid", ExchangeObjectVersion.Exchange2003, typeof(Guid), "rightsGuid", ADPropertyDefinitionFlags.ReadOnly, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400228D RID: 8845
		public static readonly ADPropertyDefinition DisplayName = new ADPropertyDefinition("DisplayName", ExchangeObjectVersion.Exchange2003, typeof(string), "displayName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new MandatoryStringLengthConstraint(1, 256)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);
	}
}

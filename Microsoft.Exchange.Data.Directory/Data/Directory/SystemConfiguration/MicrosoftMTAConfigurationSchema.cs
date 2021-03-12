using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005B7 RID: 1463
	internal sealed class MicrosoftMTAConfigurationSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x04002DA7 RID: 11687
		public static readonly ADPropertyDefinition ExchangeLegacyDN = ServerSchema.ExchangeLegacyDN;

		// Token: 0x04002DA8 RID: 11688
		public static readonly ADPropertyDefinition LocalDesig = new ADPropertyDefinition("LocalDesig", ExchangeObjectVersion.Exchange2003, typeof(string), "mTALocalDesig", ADPropertyDefinitionFlags.Mandatory, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DA9 RID: 11689
		public static readonly ADPropertyDefinition TransRetryMins = new ADPropertyDefinition("TransRetryMins", ExchangeObjectVersion.Exchange2003, typeof(int), "transRetryMins", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002DAA RID: 11690
		public static readonly ADPropertyDefinition TransTimeoutMins = new ADPropertyDefinition("TransTimeoutMins", ExchangeObjectVersion.Exchange2003, typeof(int), "transTimeoutMins", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}

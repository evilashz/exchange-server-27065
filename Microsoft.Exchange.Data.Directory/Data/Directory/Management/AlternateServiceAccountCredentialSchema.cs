using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006E1 RID: 1761
	internal class AlternateServiceAccountCredentialSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04003723 RID: 14115
		public static SimpleProviderPropertyDefinition Domain = new SimpleProviderPropertyDefinition("Domain", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.Mandatory, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003724 RID: 14116
		public static SimpleProviderPropertyDefinition UserName = new SimpleProviderPropertyDefinition("UserName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.Mandatory, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003725 RID: 14117
		public static SimpleProviderPropertyDefinition WhenAddedUTC = new SimpleProviderPropertyDefinition("WhenAddedUTC", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003726 RID: 14118
		public static SimpleProviderPropertyDefinition WhenAdded = new SimpleProviderPropertyDefinition("WhenAdded", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated | PropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new SimpleProviderPropertyDefinition[]
		{
			AlternateServiceAccountCredentialSchema.WhenAddedUTC
		}, null, new GetterDelegate(AlternateServiceAccountCredential.WhenAddedGetter), null);
	}
}

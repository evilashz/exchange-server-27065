using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200059D RID: 1437
	internal class SharingPolicySchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002D60 RID: 11616
		public static readonly ADPropertyDefinition Enabled = new ADPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchSharingPolicyIsEnabled", ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002D61 RID: 11617
		public static readonly ADPropertyDefinition Domains = new ADPropertyDefinition("Domains", ExchangeObjectVersion.Exchange2010, typeof(SharingPolicyDomain), "msExchSharingPolicyDomains", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Mandatory, null, SharingPolicyDomainsConstraint.Constrains, SharingPolicyDomainsConstraint.Constrains, null, null);

		// Token: 0x04002D62 RID: 11618
		public static readonly ADPropertyDefinition Default = new ADPropertyDefinition("Default", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SharingPolicyNonAdProperties.DefaultPropetyDefinition
		}, null, new GetterDelegate(SharingPolicyNonAdProperties.GetDefault), new SetterDelegate(SharingPolicyNonAdProperties.SetDefault), null, null);
	}
}

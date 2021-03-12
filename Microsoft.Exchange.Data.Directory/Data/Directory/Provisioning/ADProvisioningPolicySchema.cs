using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x0200078F RID: 1935
	internal class ADProvisioningPolicySchema : ADConfigurationObjectSchema
	{
		// Token: 0x040040D0 RID: 16592
		public static readonly ADPropertyDefinition TargetObjects = new ADPropertyDefinition("TargetObjects", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchProvisioningPolicyTargetObjects", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040040D1 RID: 16593
		public static readonly ADPropertyDefinition PolicyType = new ADPropertyDefinition("PolicyType", ExchangeObjectVersion.Exchange2010, typeof(ProvisioningPolicyType), "msExchProvisioningPolicyType", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue | ADPropertyDefinitionFlags.WriteOnce, ProvisioningPolicyType.Template, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(ProvisioningPolicyType))
		}, null, null);

		// Token: 0x040040D2 RID: 16594
		public static readonly ADPropertyDefinition Scopes = new ADPropertyDefinition("Scopes", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchProvisioningPolicyScopeLinks", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}

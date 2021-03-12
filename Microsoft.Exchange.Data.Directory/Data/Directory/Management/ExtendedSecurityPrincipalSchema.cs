using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200070A RID: 1802
	internal sealed class ExtendedSecurityPrincipalSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040038FA RID: 14586
		public static readonly ADPropertyDefinition SID = IADSecurityPrincipalSchema.Sid;

		// Token: 0x040038FB RID: 14587
		public static readonly ADPropertyDefinition SecurityPrincipalTypes = new ADPropertyDefinition("SecurityPrincipalTypes", ExchangeObjectVersion.Exchange2003, typeof(SecurityPrincipalType), null, ADPropertyDefinitionFlags.Calculated, SecurityPrincipalType.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.ObjectClass,
			ADObjectSchema.ObjectCategory
		}, null, new GetterDelegate(ExtendedSecurityPrincipal.SecurityPrincipalTypeDetailsGetter), new SetterDelegate(ExtendedSecurityPrincipal.SecurityPrincipalTypeDetailsSetter), null, null);

		// Token: 0x040038FC RID: 14588
		public static readonly ADPropertyDefinition DisplayName = ADRecipientSchema.DisplayName;

		// Token: 0x040038FD RID: 14589
		public static readonly ADPropertyDefinition RecipientTypeDetails = ADRecipientSchema.RecipientTypeDetails;
	}
}

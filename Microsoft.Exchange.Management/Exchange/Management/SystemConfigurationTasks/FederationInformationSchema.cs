using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009DC RID: 2524
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FederationInformationSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040033B8 RID: 13240
		public static readonly SimpleProviderPropertyDefinition TargetApplicationUri = new SimpleProviderPropertyDefinition("TargetApplicationUri", ExchangeObjectVersion.Exchange2010, typeof(Uri), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040033B9 RID: 13241
		public static readonly SimpleProviderPropertyDefinition TokenIssuerUris = new SimpleProviderPropertyDefinition("TokenIssuerUris", ExchangeObjectVersion.Exchange2010, typeof(Uri), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040033BA RID: 13242
		public static readonly SimpleProviderPropertyDefinition DomainNames = new SimpleProviderPropertyDefinition("DomainNames", ExchangeObjectVersion.Exchange2010, typeof(SmtpDomain), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040033BB RID: 13243
		public static readonly SimpleProviderPropertyDefinition TargetAutodiscoverEpr = new SimpleProviderPropertyDefinition("TargetAutodiscoverEpr", ExchangeObjectVersion.Exchange2010, typeof(Uri), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040033BC RID: 13244
		public new static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(SmtpDomain), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}

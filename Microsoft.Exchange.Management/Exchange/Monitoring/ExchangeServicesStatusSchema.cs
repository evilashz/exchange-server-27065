using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005EA RID: 1514
	internal class ExchangeServicesStatusSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040024E1 RID: 9441
		public static readonly SimpleProviderPropertyDefinition Role = new SimpleProviderPropertyDefinition("Role", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040024E2 RID: 9442
		public static readonly SimpleProviderPropertyDefinition RequiredServicesRunning = new SimpleProviderPropertyDefinition("RequiredServicesRunning", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040024E3 RID: 9443
		public static readonly SimpleProviderPropertyDefinition ServicesNotRunning = new SimpleProviderPropertyDefinition("ServicesNotRunning", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040024E4 RID: 9444
		public static readonly SimpleProviderPropertyDefinition ServicesRunning = new SimpleProviderPropertyDefinition("ServicesRunning", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}

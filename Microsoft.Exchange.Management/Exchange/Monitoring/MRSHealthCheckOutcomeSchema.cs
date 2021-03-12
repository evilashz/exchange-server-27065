using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005AB RID: 1451
	internal class MRSHealthCheckOutcomeSchema : SimpleProviderObjectSchema
	{
		// Token: 0x0400239E RID: 9118
		public static readonly SimpleProviderPropertyDefinition Check = new SimpleProviderPropertyDefinition("Check", ExchangeObjectVersion.Exchange2010, typeof(MRSHealthCheckId), PropertyDefinitionFlags.None, MRSHealthCheckId.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400239F RID: 9119
		public static readonly SimpleProviderPropertyDefinition Message = new SimpleProviderPropertyDefinition("Message", ExchangeObjectVersion.Exchange2010, typeof(LocalizedString), PropertyDefinitionFlags.None, LocalizedString.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023A0 RID: 9120
		public static readonly SimpleProviderPropertyDefinition Passed = new SimpleProviderPropertyDefinition("Passed", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023A1 RID: 9121
		public static readonly SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}

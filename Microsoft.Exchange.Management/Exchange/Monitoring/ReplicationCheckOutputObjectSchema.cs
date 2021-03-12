using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000553 RID: 1363
	internal class ReplicationCheckOutputObjectSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04002280 RID: 8832
		public static SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002281 RID: 8833
		public static SimpleProviderPropertyDefinition CheckIdProperty = new SimpleProviderPropertyDefinition("CheckId", ExchangeObjectVersion.Exchange2010, typeof(CheckId), PropertyDefinitionFlags.None, CheckId.Undefined, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002282 RID: 8834
		public static SimpleProviderPropertyDefinition Check = new SimpleProviderPropertyDefinition("Check", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002283 RID: 8835
		public static SimpleProviderPropertyDefinition IdentityProperty = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002284 RID: 8836
		public static SimpleProviderPropertyDefinition DbFailureEventId = new SimpleProviderPropertyDefinition("DbFailureEventId", ExchangeObjectVersion.Exchange2010, typeof(uint?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002285 RID: 8837
		public static SimpleProviderPropertyDefinition Result = new SimpleProviderPropertyDefinition("Result", ExchangeObjectVersion.Exchange2010, typeof(ReplicationCheckResult), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002286 RID: 8838
		public static SimpleProviderPropertyDefinition Error = new SimpleProviderPropertyDefinition("Error", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}

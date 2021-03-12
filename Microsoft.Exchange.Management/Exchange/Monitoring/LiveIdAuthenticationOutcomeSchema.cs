using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000028 RID: 40
	internal class LiveIdAuthenticationOutcomeSchema : ObjectSchema
	{
		// Token: 0x040000CE RID: 206
		public static readonly SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000CF RID: 207
		public static readonly SimpleProviderPropertyDefinition Mailbox = new SimpleProviderPropertyDefinition("Mailbox", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000D0 RID: 208
		public static readonly SimpleProviderPropertyDefinition Latency = new SimpleProviderPropertyDefinition("Latency", ExchangeObjectVersion.Exchange2003, typeof(TimeSpan?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000D1 RID: 209
		public static readonly SimpleProviderPropertyDefinition Error = new SimpleProviderPropertyDefinition("Error", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000D2 RID: 210
		public static readonly SimpleProviderPropertyDefinition Result = new SimpleProviderPropertyDefinition("Result", ExchangeObjectVersion.Exchange2003, typeof(LiveIdAuthenticationResult), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}

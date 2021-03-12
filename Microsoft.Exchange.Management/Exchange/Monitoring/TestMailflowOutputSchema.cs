using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005FD RID: 1533
	internal class TestMailflowOutputSchema : SimpleProviderObjectSchema
	{
		// Token: 0x0400255C RID: 9564
		public static readonly SimpleProviderPropertyDefinition TestMailflowResult = new SimpleProviderPropertyDefinition("TestMailflowResult", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400255D RID: 9565
		public static readonly SimpleProviderPropertyDefinition MessageLatencyTime = new SimpleProviderPropertyDefinition("MessageLatencyTime", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), PropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromSeconds(0.0), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400255E RID: 9566
		public static readonly SimpleProviderPropertyDefinition IsRemoteTest = new SimpleProviderPropertyDefinition("IsRemoteTest", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}

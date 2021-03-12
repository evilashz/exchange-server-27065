using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000017 RID: 23
	internal class TopologyServiceOutcomeSchema : ObjectSchema
	{
		// Token: 0x04000069 RID: 105
		public static readonly SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400006A RID: 106
		public static readonly SimpleProviderPropertyDefinition OperationType = new SimpleProviderPropertyDefinition("OperationType", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400006B RID: 107
		public static readonly SimpleProviderPropertyDefinition Latency = new SimpleProviderPropertyDefinition("Latency", ExchangeObjectVersion.Exchange2003, typeof(TimeSpan?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400006C RID: 108
		public static readonly SimpleProviderPropertyDefinition Error = new SimpleProviderPropertyDefinition("Error", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400006D RID: 109
		public static readonly SimpleProviderPropertyDefinition Output = new SimpleProviderPropertyDefinition("Output", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400006E RID: 110
		public static readonly SimpleProviderPropertyDefinition Result = new SimpleProviderPropertyDefinition("Result", ExchangeObjectVersion.Exchange2003, typeof(TopologyServiceResult), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}

using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.AgentTasks
{
	// Token: 0x0200001D RID: 29
	internal class TransportEventSchema : SimpleProviderObjectSchema
	{
		// Token: 0x0400003A RID: 58
		public static readonly SimpleProviderPropertyDefinition EventTopic = new SimpleProviderPropertyDefinition("EventTopic", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400003B RID: 59
		public static readonly SimpleProviderPropertyDefinition TransportAgentIdentities = new SimpleProviderPropertyDefinition("TransportAgentIdentities", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}

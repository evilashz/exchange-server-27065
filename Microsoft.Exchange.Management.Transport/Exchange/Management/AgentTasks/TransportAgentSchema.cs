using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.AgentTasks
{
	// Token: 0x0200001A RID: 26
	internal class TransportAgentSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000033 RID: 51
		public static readonly SimpleProviderPropertyDefinition Enabled = new SimpleProviderPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000034 RID: 52
		public static readonly SimpleProviderPropertyDefinition Priority = new SimpleProviderPropertyDefinition("Priority", ExchangeObjectVersion.Exchange2007, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000035 RID: 53
		public static readonly SimpleProviderPropertyDefinition AgentFactory = new SimpleProviderPropertyDefinition("AgentFactory", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000036 RID: 54
		public static readonly SimpleProviderPropertyDefinition AssemblyPath = new SimpleProviderPropertyDefinition("AssemblyPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}

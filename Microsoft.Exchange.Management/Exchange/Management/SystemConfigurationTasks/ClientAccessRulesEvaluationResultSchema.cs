using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000882 RID: 2178
	internal sealed class ClientAccessRulesEvaluationResultSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04002D2E RID: 11566
		public static readonly SimpleProviderPropertyDefinition Name = new SimpleProviderPropertyDefinition("Name", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002D2F RID: 11567
		public static readonly SimpleProviderPropertyDefinition Action = new SimpleProviderPropertyDefinition("Action", ExchangeObjectVersion.Exchange2012, typeof(ClientAccessRulesAction), PropertyDefinitionFlags.None, ClientAccessRulesAction.AllowAccess, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}

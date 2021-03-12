using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B6F RID: 2927
	internal class RulePhraseSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04003870 RID: 14448
		public static readonly SimpleProviderPropertyDefinition Name = new SimpleProviderPropertyDefinition("Name", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003871 RID: 14449
		public static readonly SimpleProviderPropertyDefinition Rank = new SimpleProviderPropertyDefinition("Rank", ExchangeObjectVersion.Exchange2007, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003872 RID: 14450
		public static readonly SimpleProviderPropertyDefinition LinkedDisplayText = new SimpleProviderPropertyDefinition("LinkedDisplayText", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}

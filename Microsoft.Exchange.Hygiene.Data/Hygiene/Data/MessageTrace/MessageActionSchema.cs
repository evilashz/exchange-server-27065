using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200014E RID: 334
	internal class MessageActionSchema
	{
		// Token: 0x0400066F RID: 1647
		internal static readonly HygienePropertyDefinition RuleActionIdProperty = new HygienePropertyDefinition("RuleActionId", typeof(Guid));

		// Token: 0x04000670 RID: 1648
		internal static readonly HygienePropertyDefinition EventRuleIdProperty = CommonMessageTraceSchema.EventRuleIdProperty;

		// Token: 0x04000671 RID: 1649
		internal static readonly HygienePropertyDefinition NameProperty = new HygienePropertyDefinition("Name", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}

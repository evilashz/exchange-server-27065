using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200015C RID: 348
	internal class MessageEventRuleSchema
	{
		// Token: 0x04000690 RID: 1680
		internal static readonly HygienePropertyDefinition EventRuleIdProperty = CommonMessageTraceSchema.EventRuleIdProperty;

		// Token: 0x04000691 RID: 1681
		internal static readonly HygienePropertyDefinition RuleIdProperty = new HygienePropertyDefinition("RuleId", typeof(Guid));

		// Token: 0x04000692 RID: 1682
		internal static readonly HygienePropertyDefinition EventIdProperty = CommonMessageTraceSchema.EventIdProperty;

		// Token: 0x04000693 RID: 1683
		internal static readonly HygienePropertyDefinition RuleTypeProperty = new HygienePropertyDefinition("RuleType", typeof(string));
	}
}

using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200000C RID: 12
	public sealed class FalseCondition : Condition
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002DD5 File Offset: 0x00000FD5
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.False;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public override bool Evaluate(RulesEvaluationContext context)
		{
			return false;
		}
	}
}

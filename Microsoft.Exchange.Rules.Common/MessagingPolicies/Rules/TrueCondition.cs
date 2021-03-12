using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200003D RID: 61
	public sealed class TrueCondition : Condition
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00006E4F File Offset: 0x0000504F
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.True;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006E52 File Offset: 0x00005052
		public override bool Evaluate(RulesEvaluationContext context)
		{
			return true;
		}
	}
}

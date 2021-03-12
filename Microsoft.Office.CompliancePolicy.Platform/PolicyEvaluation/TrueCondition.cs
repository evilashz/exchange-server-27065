using System;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000C3 RID: 195
	public sealed class TrueCondition : Condition
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0000EAFF File Offset: 0x0000CCFF
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.True;
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000EB02 File Offset: 0x0000CD02
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			return true;
		}
	}
}

using System;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000C0 RID: 192
	public sealed class FalseCondition : Condition
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000E963 File Offset: 0x0000CB63
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.False;
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000E966 File Offset: 0x0000CB66
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			return false;
		}
	}
}

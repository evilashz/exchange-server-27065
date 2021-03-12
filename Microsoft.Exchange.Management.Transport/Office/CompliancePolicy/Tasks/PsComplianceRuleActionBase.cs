using System;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000C2 RID: 194
	public abstract class PsComplianceRuleActionBase
	{
		// Token: 0x06000705 RID: 1797
		internal abstract Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action ToEngineAction();

		// Token: 0x06000706 RID: 1798 RVA: 0x0001E314 File Offset: 0x0001C514
		internal static PsComplianceRuleActionBase FromEngineAction(Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action action)
		{
			if (action is HoldAction)
			{
				return PsHoldContentAction.FromEngineAction(action as HoldAction);
			}
			if (action is BlockAccessAction)
			{
				return PsBlockAccessAction.FromEngineAction(action as BlockAccessAction);
			}
			throw new UnexpectedConditionOrActionDetectedException();
		}
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000C4 RID: 196
	public class PsBlockAccessAction : PsComplianceRuleActionBase
	{
		// Token: 0x0600070F RID: 1807 RVA: 0x0001E3EA File Offset: 0x0001C5EA
		internal override Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action ToEngineAction()
		{
			return new BlockAccessAction(new List<Argument>(), null);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001E3F7 File Offset: 0x0001C5F7
		internal static PsBlockAccessAction FromEngineAction(BlockAccessAction action)
		{
			return new PsBlockAccessAction();
		}
	}
}

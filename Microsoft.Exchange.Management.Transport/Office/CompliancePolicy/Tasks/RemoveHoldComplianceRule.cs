using System;
using System.Management.Automation;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000126 RID: 294
	[Cmdlet("Remove", "HoldComplianceRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveHoldComplianceRule : RemoveComplianceRuleBase
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x0002F7A7 File Offset: 0x0002D9A7
		public RemoveHoldComplianceRule() : base(PolicyScenario.Hold)
		{
		}
	}
}

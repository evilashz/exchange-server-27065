using System;
using System.Management.Automation;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000122 RID: 290
	[Cmdlet("Remove", "HoldCompliancePolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveHoldCompliancePolicy : RemoveCompliancePolicyBase
	{
		// Token: 0x06000D0B RID: 3339 RVA: 0x0002F18C File Offset: 0x0002D38C
		public RemoveHoldCompliancePolicy() : base(PolicyScenario.Hold)
		{
		}
	}
}

using System;
using System.Management.Automation;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000121 RID: 289
	[Cmdlet("Set", "HoldCompliancePolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetHoldCompliancePolicy : SetCompliancePolicyBase
	{
		// Token: 0x06000D0A RID: 3338 RVA: 0x0002F183 File Offset: 0x0002D383
		public SetHoldCompliancePolicy() : base(PolicyScenario.Hold)
		{
		}
	}
}

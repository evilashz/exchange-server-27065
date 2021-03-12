using System;
using System.Management.Automation;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x0200012A RID: 298
	[Cmdlet("Remove", "DlpCompliancePolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveDlpCompliancePolicy : RemoveCompliancePolicyBase
	{
		// Token: 0x06000D31 RID: 3377 RVA: 0x0002F96C File Offset: 0x0002DB6C
		public RemoveDlpCompliancePolicy() : base(PolicyScenario.Dlp)
		{
		}
	}
}

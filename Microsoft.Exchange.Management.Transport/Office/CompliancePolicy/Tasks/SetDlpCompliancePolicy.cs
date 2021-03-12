using System;
using System.Management.Automation;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000129 RID: 297
	[Cmdlet("Set", "DlpCompliancePolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetDlpCompliancePolicy : SetCompliancePolicyBase
	{
		// Token: 0x06000D30 RID: 3376 RVA: 0x0002F963 File Offset: 0x0002DB63
		public SetDlpCompliancePolicy() : base(PolicyScenario.Dlp)
		{
		}
	}
}

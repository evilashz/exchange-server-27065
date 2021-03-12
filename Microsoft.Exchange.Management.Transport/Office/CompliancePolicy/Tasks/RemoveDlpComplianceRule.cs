using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x0200012E RID: 302
	[Cmdlet("Remove", "DlpComplianceRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveDlpComplianceRule : RemoveComplianceRuleBase
	{
		// Token: 0x06000D4E RID: 3406 RVA: 0x0002FE8C File Offset: 0x0002E08C
		public RemoveDlpComplianceRule() : base(PolicyScenario.Dlp)
		{
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0002FE98 File Offset: 0x0002E098
		protected override void InternalValidate()
		{
			base.InternalValidate();
			PsDlpComplianceRule psDlpComplianceRule = new PsDlpComplianceRule(base.DataObject);
			psDlpComplianceRule.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
			if (psDlpComplianceRule.ReadOnly && !base.ForceDeletion)
			{
				throw new TaskRuleIsTooAdvancedToModifyException(psDlpComplianceRule.Name);
			}
		}
	}
}

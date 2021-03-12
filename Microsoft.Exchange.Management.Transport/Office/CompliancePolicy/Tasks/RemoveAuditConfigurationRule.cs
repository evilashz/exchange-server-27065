using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000119 RID: 281
	[Cmdlet("Remove", "AuditConfigurationRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveAuditConfigurationRule : RemoveComplianceRuleBase
	{
		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002E667 File Offset: 0x0002C867
		public RemoveAuditConfigurationRule() : base(PolicyScenario.AuditSettings)
		{
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002E670 File Offset: 0x0002C870
		protected override void InternalValidate()
		{
			this.ValidateAuditConfigurationRuleParameter();
			base.InternalValidate();
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0002E67E File Offset: 0x0002C87E
		private void ValidateAuditConfigurationRuleParameter()
		{
			if (this.Identity != null && !AuditPolicyUtility.IsAuditConfigurationRule(this.Identity.ToString()))
			{
				base.WriteError(new ArgumentException(Strings.CanOnlyManipulateAuditConfigurationRule), ErrorCategory.InvalidArgument, null);
			}
		}
	}
}

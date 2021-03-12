using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000115 RID: 277
	[Cmdlet("Remove", "AuditConfigurationPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveAuditConfigurationPolicy : RemoveCompliancePolicyBase
	{
		// Token: 0x06000CBE RID: 3262 RVA: 0x0002E24B File Offset: 0x0002C44B
		public RemoveAuditConfigurationPolicy() : base(PolicyScenario.AuditSettings)
		{
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002E254 File Offset: 0x0002C454
		protected override void InternalValidate()
		{
			this.ValidateAuditConfigurationPolicyParameter();
			base.InternalValidate();
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002E262 File Offset: 0x0002C462
		private void ValidateAuditConfigurationPolicyParameter()
		{
			if (this.Identity != null && !AuditPolicyUtility.IsAuditConfigurationPolicy(this.Identity.ToString()))
			{
				base.WriteError(new ArgumentException(Strings.CanOnlyManipulateAuditConfigurationPolicy), ErrorCategory.InvalidArgument, null);
			}
		}
	}
}

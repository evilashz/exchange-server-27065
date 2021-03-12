using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000113 RID: 275
	[Cmdlet("Get", "AuditConfigurationPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetAuditConfigurationPolicy : GetCompliancePolicyBase
	{
		// Token: 0x06000CB1 RID: 3249 RVA: 0x0002E07C File Offset: 0x0002C27C
		public GetAuditConfigurationPolicy() : base(PolicyScenario.AuditSettings)
		{
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002E088 File Offset: 0x0002C288
		protected override void WriteResult(IConfigurable dataObject)
		{
			AuditConfigurationPolicy auditConfigurationPolicy = new AuditConfigurationPolicy(dataObject as PolicyStorage);
			base.PopulateDistributionStatus(auditConfigurationPolicy, dataObject as PolicyStorage);
			base.WriteResult(auditConfigurationPolicy);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002E0B5 File Offset: 0x0002C2B5
		protected override void InternalValidate()
		{
			if (this.Identity != null && !AuditPolicyUtility.IsAuditConfigurationPolicy(this.Identity.ToString()))
			{
				base.WriteError(new ArgumentException(Strings.CanOnlyManipulateAuditConfigurationPolicy), ErrorCategory.InvalidArgument, null);
			}
			base.InternalValidate();
		}
	}
}

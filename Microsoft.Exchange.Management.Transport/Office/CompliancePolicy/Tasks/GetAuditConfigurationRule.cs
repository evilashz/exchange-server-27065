using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000116 RID: 278
	[Cmdlet("Get", "AuditConfigurationRule", DefaultParameterSetName = "Identity")]
	public sealed class GetAuditConfigurationRule : GetComplianceRuleBase
	{
		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002E295 File Offset: 0x0002C495
		public GetAuditConfigurationRule() : base(PolicyScenario.AuditSettings)
		{
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002E2BB File Offset: 0x0002C4BB
		protected override IEnumerable<RuleStorage> GetPagedData()
		{
			return from p in base.GetPagedData()
			where p.Scenario == base.Scenario || AuditPolicyUtility.IsAuditConfigurationRule(p.Name)
			select p;
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002E2D4 File Offset: 0x0002C4D4
		protected override void InternalValidate()
		{
			if (this.Identity != null && !AuditPolicyUtility.IsAuditConfigurationRule(this.Identity.ToString()))
			{
				base.WriteError(new ArgumentException(Strings.CanOnlyManipulateAuditConfigurationRule), ErrorCategory.InvalidArgument, null);
			}
			base.InternalValidate();
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002E310 File Offset: 0x0002C510
		protected override void WriteResult(IConfigurable dataObject)
		{
			AuditConfigurationRule auditConfigurationRule = new AuditConfigurationRule(dataObject as RuleStorage);
			auditConfigurationRule.PopulateTaskProperties();
			base.WriteResult(auditConfigurationRule);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000123 RID: 291
	[Cmdlet("Get", "HoldComplianceRule", DefaultParameterSetName = "Identity")]
	public sealed class GetHoldComplianceRule : GetComplianceRuleBase
	{
		// Token: 0x06000D0C RID: 3340 RVA: 0x0002F195 File Offset: 0x0002D395
		public GetHoldComplianceRule() : base(PolicyScenario.Hold)
		{
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0002F1FF File Offset: 0x0002D3FF
		protected override IEnumerable<RuleStorage> GetPagedData()
		{
			return from p in base.GetPagedData()
			where p.Scenario == base.Scenario || (!AuditPolicyUtility.IsAuditConfigurationRule(p.Name) && !DevicePolicyUtility.IsDeviceConfigurationRule(p.Name) && !DevicePolicyUtility.IsDeviceConditionalAccessRule(p.Name) && !DevicePolicyUtility.IsDeviceTenantRule(p.Name) && p.Scenario != PolicyScenario.Dlp)
			select p;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0002F218 File Offset: 0x0002D418
		protected override void WriteResult(IConfigurable dataObject)
		{
			PsHoldRule psHoldRule = new PsHoldRule(dataObject as RuleStorage);
			psHoldRule.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
			if (psHoldRule.ReadOnly)
			{
				this.WriteWarning(Strings.WarningTaskRuleIsTooAdvancedToRead(psHoldRule.Name));
			}
			base.WriteResult(psHoldRule);
		}
	}
}

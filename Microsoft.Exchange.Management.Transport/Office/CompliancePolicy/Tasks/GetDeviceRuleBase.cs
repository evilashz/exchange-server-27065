using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000FC RID: 252
	public abstract class GetDeviceRuleBase : GetComplianceRuleBase
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x00029726 File Offset: 0x00027926
		protected GetDeviceRuleBase(PolicyScenario scenario) : base(scenario)
		{
			DevicePolicyUtility.ValidateDeviceScenarioArgument(scenario);
		}

		// Token: 0x06000A42 RID: 2626
		protected abstract bool IsDeviceRule(string identity);

		// Token: 0x06000A43 RID: 2627
		protected abstract DeviceRuleBase CreateDeviceRuleObject(RuleStorage ruleStorage);

		// Token: 0x06000A44 RID: 2628
		protected abstract string GetCanOnlyManipulateErrorString();

		// Token: 0x06000A45 RID: 2629 RVA: 0x00029753 File Offset: 0x00027953
		protected override IEnumerable<RuleStorage> GetPagedData()
		{
			return from p in base.GetPagedData()
			where p.Scenario == base.Scenario || this.IsDeviceRule(p.Name)
			select p;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002976C File Offset: 0x0002796C
		protected override void InternalValidate()
		{
			if (this.Identity != null && !this.IsDeviceRule(this.Identity.ToString()))
			{
				base.WriteError(new ArgumentException(this.GetCanOnlyManipulateErrorString()), ErrorCategory.InvalidArgument, null);
			}
			base.InternalValidate();
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000297A4 File Offset: 0x000279A4
		protected override void WriteResult(IConfigurable dataObject)
		{
			DeviceRuleBase deviceRuleBase = this.CreateDeviceRuleObject(dataObject as RuleStorage);
			deviceRuleBase.PopulateTaskProperties(this, base.DataSession as IConfigurationSession);
			base.WriteResult(deviceRuleBase);
		}
	}
}

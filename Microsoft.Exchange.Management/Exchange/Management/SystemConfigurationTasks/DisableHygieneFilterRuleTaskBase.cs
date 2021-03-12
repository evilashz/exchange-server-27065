using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A44 RID: 2628
	public abstract class DisableHygieneFilterRuleTaskBase : DisableRuleTaskBase
	{
		// Token: 0x06005E23 RID: 24099 RVA: 0x0018AD52 File Offset: 0x00188F52
		protected DisableHygieneFilterRuleTaskBase(string ruleCollectionName) : base(ruleCollectionName)
		{
		}

		// Token: 0x06005E24 RID: 24100 RVA: 0x0018AD5C File Offset: 0x00188F5C
		protected override IConfigurable PrepareDataObject()
		{
			TransportRule transportRule = (TransportRule)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			TransportRule transportRule2 = (TransportRule)TransportRuleParser.Instance.GetRule(transportRule.Xml);
			if (transportRule2.IsTooAdvancedToParse)
			{
				base.WriteError(new InvalidOperationException(Strings.CannotModifyRuleDueToVersion(transportRule2.Name)), ErrorCategory.InvalidOperation, null);
			}
			transportRule2.Enabled = RuleState.Disabled;
			transportRule.Xml = TransportRuleSerializer.Instance.SaveRuleToString(transportRule2);
			return transportRule;
		}
	}
}

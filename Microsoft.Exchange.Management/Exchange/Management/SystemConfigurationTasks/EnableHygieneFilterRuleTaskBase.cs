using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A46 RID: 2630
	public abstract class EnableHygieneFilterRuleTaskBase : EnableRuleTaskBase
	{
		// Token: 0x06005E28 RID: 24104 RVA: 0x0018AE08 File Offset: 0x00189008
		protected EnableHygieneFilterRuleTaskBase(string ruleCollectionName) : base(ruleCollectionName)
		{
		}

		// Token: 0x06005E29 RID: 24105 RVA: 0x0018AE14 File Offset: 0x00189014
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
			transportRule2.Enabled = RuleState.Enabled;
			transportRule.Xml = TransportRuleSerializer.Instance.SaveRuleToString(transportRule2);
			return transportRule;
		}
	}
}

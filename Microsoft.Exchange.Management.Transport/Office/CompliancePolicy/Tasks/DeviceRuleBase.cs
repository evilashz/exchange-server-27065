using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000CA RID: 202
	[Serializable]
	public abstract class DeviceRuleBase : PsComplianceRuleBase
	{
		// Token: 0x06000784 RID: 1924 RVA: 0x0001EFFE File Offset: 0x0001D1FE
		protected DeviceRuleBase()
		{
		}

		// Token: 0x06000785 RID: 1925
		protected abstract IEnumerable<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition> GetTaskConditions();

		// Token: 0x06000786 RID: 1926
		protected abstract void SetTaskConditions(IEnumerable<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition> conditions);

		// Token: 0x06000787 RID: 1927 RVA: 0x0001F006 File Offset: 0x0001D206
		protected DeviceRuleBase(RuleStorage ruleStorage) : base(ruleStorage)
		{
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x0001F00F File Offset: 0x0001D20F
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x0001F017 File Offset: 0x0001D217
		public MultiValuedProperty<Guid> TargetGroups { get; set; }

		// Token: 0x0600078A RID: 1930 RVA: 0x0001F020 File Offset: 0x0001D220
		internal override void PopulateTaskProperties(Task task, IConfigurationSession configurationSession)
		{
			base.PopulateTaskProperties(task, configurationSession);
			if (!string.IsNullOrEmpty(base.RuleBlob))
			{
				this.SetTaskConditions(this.ConvertEngineConditionToTaskConditions(this.GetPolicyRuleFromRuleBlob().Condition));
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001F050 File Offset: 0x0001D250
		internal override void UpdateStorageProperties(Task task, IConfigurationSession configurationSession, bool isNewRule)
		{
			base.UpdateStorageProperties(task, configurationSession, isNewRule);
			PolicyRule policyRule = new PolicyRule
			{
				Condition = this.ConvertTaskConditionsToEngineCondition(this.GetTaskConditions()),
				Actions = DeviceRuleBase.EmptyActionList,
				Comments = string.Empty,
				Enabled = RuleState.Disabled,
				ImmutableId = base.Guid,
				Name = base.Name
			};
			base.RuleBlob = this.GetRuleXmlFromPolicyRule(policyRule);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001F0C2 File Offset: 0x0001D2C2
		internal override PolicyRule GetPolicyRuleFromRuleBlob()
		{
			return new RuleParser(new SimplePolicyParserFactory()).GetRule(base.RuleBlob);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001F0DC File Offset: 0x0001D2DC
		internal IEnumerable<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition> ConvertEngineConditionToTaskConditions(Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition condition)
		{
			AndCondition andCondition = condition as AndCondition;
			if (andCondition == null)
			{
				return Enumerable.Empty<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition>();
			}
			return andCondition.SubConditions;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001F100 File Offset: 0x0001D300
		internal Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition ConvertTaskConditionsToEngineCondition(IEnumerable<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition> predicates)
		{
			AndCondition andCondition = new AndCondition();
			andCondition.SubConditions.AddRange(predicates);
			return andCondition;
		}

		// Token: 0x040002BE RID: 702
		private static readonly List<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action> EmptyActionList = new List<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action>();
	}
}

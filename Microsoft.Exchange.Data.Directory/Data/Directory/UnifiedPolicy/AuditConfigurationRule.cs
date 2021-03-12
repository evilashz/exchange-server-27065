using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A14 RID: 2580
	[Serializable]
	public class AuditConfigurationRule : ADPresentationObject
	{
		// Token: 0x0600773A RID: 30522 RVA: 0x00188953 File Offset: 0x00186B53
		public AuditConfigurationRule()
		{
		}

		// Token: 0x0600773B RID: 30523 RVA: 0x0018895B File Offset: 0x00186B5B
		public AuditConfigurationRule(RuleStorage ruleStorage) : base(ruleStorage)
		{
		}

		// Token: 0x17002A91 RID: 10897
		// (get) Token: 0x0600773C RID: 30524 RVA: 0x00188964 File Offset: 0x00186B64
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return AuditConfigurationRule.schema;
			}
		}

		// Token: 0x17002A92 RID: 10898
		// (get) Token: 0x0600773D RID: 30525 RVA: 0x0018896B File Offset: 0x00186B6B
		// (set) Token: 0x0600773E RID: 30526 RVA: 0x0018897D File Offset: 0x00186B7D
		internal Guid MasterIdentity
		{
			get
			{
				return (Guid)this[AuditConfigurationRuleSchema.MasterIdentity];
			}
			set
			{
				this[AuditConfigurationRuleSchema.MasterIdentity] = value;
			}
		}

		// Token: 0x17002A93 RID: 10899
		// (get) Token: 0x0600773F RID: 30527 RVA: 0x00188990 File Offset: 0x00186B90
		// (set) Token: 0x06007740 RID: 30528 RVA: 0x001889A2 File Offset: 0x00186BA2
		internal string RuleBlob
		{
			get
			{
				return (string)this[AuditConfigurationRuleSchema.RuleBlob];
			}
			set
			{
				this[AuditConfigurationRuleSchema.RuleBlob] = value;
			}
		}

		// Token: 0x17002A94 RID: 10900
		// (get) Token: 0x06007741 RID: 30529 RVA: 0x001889B0 File Offset: 0x00186BB0
		// (set) Token: 0x06007742 RID: 30530 RVA: 0x001889C2 File Offset: 0x00186BC2
		public Workload Workload
		{
			get
			{
				return (Workload)this[AuditConfigurationRuleSchema.Workload];
			}
			set
			{
				this[AuditConfigurationRuleSchema.Workload] = value;
			}
		}

		// Token: 0x17002A95 RID: 10901
		// (get) Token: 0x06007743 RID: 30531 RVA: 0x001889D5 File Offset: 0x00186BD5
		// (set) Token: 0x06007744 RID: 30532 RVA: 0x001889DD File Offset: 0x00186BDD
		public MultiValuedProperty<AuditableOperations> AuditOperation { get; set; }

		// Token: 0x17002A96 RID: 10902
		// (get) Token: 0x06007745 RID: 30533 RVA: 0x001889E6 File Offset: 0x00186BE6
		// (set) Token: 0x06007746 RID: 30534 RVA: 0x001889F8 File Offset: 0x00186BF8
		public Guid Policy
		{
			get
			{
				return (Guid)this[AuditConfigurationRuleSchema.Policy];
			}
			set
			{
				this[AuditConfigurationRuleSchema.Policy] = value;
			}
		}

		// Token: 0x17002A97 RID: 10903
		// (get) Token: 0x06007747 RID: 30535 RVA: 0x00188A0B File Offset: 0x00186C0B
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x06007748 RID: 30536 RVA: 0x00188A12 File Offset: 0x00186C12
		internal void PopulateTaskProperties()
		{
			if (!string.IsNullOrEmpty(this.RuleBlob))
			{
				this.SetTaskCondition(this.GetPolicyRuleFromRuleBlob().Condition as AuditOperationsPredicate);
			}
		}

		// Token: 0x06007749 RID: 30537 RVA: 0x00188A38 File Offset: 0x00186C38
		internal void UpdateStorageProperties()
		{
			PolicyRule policyRule = new PolicyRule
			{
				Condition = new AuditOperationsPredicate(this.GetTaskConditions()),
				Actions = AuditConfigurationRule.EmptyActionList,
				Comments = string.Empty,
				Enabled = RuleState.Disabled,
				ImmutableId = base.Guid,
				Name = base.Name
			};
			this.RuleBlob = this.GetRuleXmlFromPolicyRule(policyRule);
		}

		// Token: 0x0600774A RID: 30538 RVA: 0x00188AA0 File Offset: 0x00186CA0
		internal PolicyRule GetPolicyRuleFromRuleBlob()
		{
			return new RuleParser(new SimplePolicyParserFactory()).GetRule(this.RuleBlob);
		}

		// Token: 0x0600774B RID: 30539 RVA: 0x00188AB7 File Offset: 0x00186CB7
		internal string GetRuleXmlFromPolicyRule(PolicyRule policyRule)
		{
			return new RuleSerializer().SaveRuleToString(policyRule);
		}

		// Token: 0x0600774C RID: 30540 RVA: 0x00188AC4 File Offset: 0x00186CC4
		private List<string> GetTaskConditions()
		{
			List<string> list = new List<string>();
			if (this.AuditOperation != null)
			{
				foreach (AuditableOperations auditableOperations in this.AuditOperation)
				{
					list.Add(auditableOperations.ToString());
				}
			}
			if (!list.Any<string>())
			{
				list.Add(AuditableOperations.None.ToString());
			}
			return list;
		}

		// Token: 0x0600774D RID: 30541 RVA: 0x00188B4C File Offset: 0x00186D4C
		private void SetTaskCondition(AuditOperationsPredicate condition)
		{
			MultiValuedProperty<AuditableOperations> multiValuedProperty = new MultiValuedProperty<AuditableOperations>();
			if (condition != null && condition.Value != null && condition.Value.ParsedValue != null)
			{
				if (condition.Value.ParsedValue is string)
				{
					AuditableOperations item;
					if (Enum.TryParse<AuditableOperations>((string)condition.Value.ParsedValue, true, out item))
					{
						multiValuedProperty.Add(item);
					}
				}
				else if (condition.Value.ParsedValue is List<string>)
				{
					foreach (string text in ((List<string>)condition.Value.ParsedValue))
					{
						AuditableOperations item;
						if (text != null && Enum.TryParse<AuditableOperations>(text, true, out item))
						{
							multiValuedProperty.Add(item);
						}
					}
				}
			}
			this.AuditOperation = multiValuedProperty;
		}

		// Token: 0x04004C73 RID: 19571
		private static AuditConfigurationRuleSchema schema = ObjectSchema.GetInstance<AuditConfigurationRuleSchema>();

		// Token: 0x04004C74 RID: 19572
		private static List<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action> EmptyActionList = new List<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action>();
	}
}

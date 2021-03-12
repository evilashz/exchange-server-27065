using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000D9 RID: 217
	[Serializable]
	public sealed class PsHoldRule : PsComplianceRuleBase
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x00024E01 File Offset: 0x00023001
		public PsHoldRule()
		{
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00024E09 File Offset: 0x00023009
		public PsHoldRule(RuleStorage ruleStorage) : base(ruleStorage)
		{
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00024E12 File Offset: 0x00023012
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return PsHoldRule.schema;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00024E19 File Offset: 0x00023019
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x00024E2B File Offset: 0x0002302B
		public DateTime? ContentDateFrom
		{
			get
			{
				return (DateTime?)this[PsHoldRuleSchema.ContentDateFrom];
			}
			set
			{
				this[PsHoldRuleSchema.ContentDateFrom] = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00024E3E File Offset: 0x0002303E
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x00024E50 File Offset: 0x00023050
		public DateTime? ContentDateTo
		{
			get
			{
				return (DateTime?)this[PsHoldRuleSchema.ContentDateTo];
			}
			set
			{
				this[PsHoldRuleSchema.ContentDateTo] = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00024E63 File Offset: 0x00023063
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x00024E75 File Offset: 0x00023075
		public HoldDurationHint HoldDurationDisplayHint
		{
			get
			{
				return (HoldDurationHint)this[PsHoldRuleSchema.HoldDurationDisplayHint];
			}
			set
			{
				this[PsHoldRuleSchema.HoldDurationDisplayHint] = value;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00024E88 File Offset: 0x00023088
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x00024EDC File Offset: 0x000230DC
		public Unlimited<int>? HoldContent
		{
			get
			{
				int? num = (int?)this[PsHoldRuleSchema.HoldContent];
				if (num == null)
				{
					return null;
				}
				return new Unlimited<int>?((num.Value == 0) ? Unlimited<int>.UnlimitedValue : num.Value);
			}
			set
			{
				this[PsHoldRuleSchema.HoldContent] = ((value != null) ? new int?(value.Value.IsUnlimited ? 0 : value.Value.Value) : null);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00024F35 File Offset: 0x00023135
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00024F3C File Offset: 0x0002313C
		internal override void PopulateTaskProperties(Task task, IConfigurationSession configurationSession)
		{
			base.PopulateTaskProperties(task, configurationSession);
			if (!string.IsNullOrEmpty(base.RuleBlob))
			{
				PolicyRule policyRuleFromRuleBlob = this.GetPolicyRuleFromRuleBlob();
				if (policyRuleFromRuleBlob.IsTooAdvancedToParse)
				{
					base.ReadOnly = true;
				}
				else
				{
					this.SetTaskConditions(PsHoldRule.ConvertEngineConditionToTaskConditions(policyRuleFromRuleBlob.Condition));
					this.SetTaskActions(PsHoldRule.ConvertEngineActionsToTaskActions(policyRuleFromRuleBlob.Actions));
				}
				base.ResetChangeTracking();
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00024FA0 File Offset: 0x000231A0
		internal override void UpdateStorageProperties(Task task, IConfigurationSession configurationSession, bool isNewRule)
		{
			base.UpdateStorageProperties(task, configurationSession, isNewRule);
			if (base.ObjectState != ObjectState.Unchanged)
			{
				PolicyRule policyRule = new PolicyRule
				{
					Condition = PsHoldRule.ConvertTaskConditionsToEngineCondition(this.GetTaskConditions()),
					Actions = PsHoldRule.ConvertTaskActionsToEngineActions(this.GetTaskActions()),
					Comments = base.Comment,
					Enabled = (base.Enabled ? RuleState.Enabled : RuleState.Disabled),
					ImmutableId = base.Guid,
					Name = base.Name
				};
				base.RuleBlob = this.GetRuleXmlFromPolicyRule(policyRule);
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002502C File Offset: 0x0002322C
		internal override PolicyRule GetPolicyRuleFromRuleBlob()
		{
			RuleParser ruleParser = new RuleParser(new SimplePolicyParserFactory());
			return ruleParser.GetRule(base.RuleBlob);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00025050 File Offset: 0x00023250
		private IEnumerable<PsComplianceRulePredicateBase> GetTaskConditions()
		{
			List<PsComplianceRulePredicateBase> list = new List<PsComplianceRulePredicateBase>();
			if (this.ContentDateFrom != null)
			{
				list.Add(new PsContentDateFromPredicate(this.ContentDateFrom.Value));
			}
			if (this.ContentDateTo != null)
			{
				list.Add(new PsContentDateToPredicate(this.ContentDateTo.Value));
			}
			if (!string.IsNullOrEmpty(base.ContentMatchQuery))
			{
				list.Add(new PsContentMatchQueryPredicate(base.ContentMatchQuery));
			}
			return list;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000250D8 File Offset: 0x000232D8
		private void SetTaskConditions(IEnumerable<PsComplianceRulePredicateBase> conditions)
		{
			foreach (PsComplianceRulePredicateBase psComplianceRulePredicateBase in conditions)
			{
				if (psComplianceRulePredicateBase is PsContentMatchQueryPredicate)
				{
					base.ContentMatchQuery = (psComplianceRulePredicateBase as PsContentMatchQueryPredicate).TextQuery;
				}
				else if (psComplianceRulePredicateBase is PsContentDateFromPredicate)
				{
					PsContentDateFromPredicate psContentDateFromPredicate = psComplianceRulePredicateBase as PsContentDateFromPredicate;
					this.ContentDateFrom = new DateTime?(psContentDateFromPredicate.PropertyValue);
				}
				else
				{
					if (!(psComplianceRulePredicateBase is PsContentDateToPredicate))
					{
						throw new UnexpectedConditionOrActionDetectedException();
					}
					PsContentDateToPredicate psContentDateToPredicate = psComplianceRulePredicateBase as PsContentDateToPredicate;
					this.ContentDateTo = new DateTime?(psContentDateToPredicate.PropertyValue);
				}
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00025180 File Offset: 0x00023380
		internal IEnumerable<PsComplianceRuleActionBase> GetTaskActions()
		{
			List<PsComplianceRuleActionBase> list = new List<PsComplianceRuleActionBase>();
			if (this.HoldContent != null)
			{
				PsHoldContentAction item = new PsHoldContentAction(this.HoldContent.Value.IsUnlimited ? 0 : this.HoldContent.Value.Value, this.HoldDurationDisplayHint);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x000251EC File Offset: 0x000233EC
		private void SetTaskActions(IEnumerable<PsComplianceRuleActionBase> actions)
		{
			foreach (PsComplianceRuleActionBase psComplianceRuleActionBase in actions)
			{
				if (!(psComplianceRuleActionBase is PsHoldContentAction))
				{
					throw new UnexpectedConditionOrActionDetectedException();
				}
				PsHoldContentAction psHoldContentAction = psComplianceRuleActionBase as PsHoldContentAction;
				this.HoldContent = new Unlimited<int>?((psHoldContentAction.HoldDurationDays == 0) ? Unlimited<int>.UnlimitedValue : psHoldContentAction.HoldDurationDays);
				this.HoldDurationDisplayHint = psHoldContentAction.HoldDurationDisplayHint;
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00025278 File Offset: 0x00023478
		internal static IEnumerable<PsComplianceRulePredicateBase> ConvertEngineConditionToTaskConditions(Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition condition)
		{
			List<PsComplianceRulePredicateBase> list = new List<PsComplianceRulePredicateBase>();
			QueryPredicate queryPredicate = condition as QueryPredicate;
			if (queryPredicate != null)
			{
				AndCondition andCondition = queryPredicate.SubCondition as AndCondition;
				if (andCondition == null)
				{
					return list;
				}
				using (List<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition>.Enumerator enumerator = andCondition.SubConditions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition condition2 = enumerator.Current;
						list.AddRange(PsHoldRule.ConvertEngineConditionToTaskConditions(condition2));
					}
					return list;
				}
			}
			if (condition is PredicateCondition)
			{
				PredicateCondition predicate = condition as PredicateCondition;
				list.Add(PsComplianceRulePredicateBase.FromEnginePredicate(predicate));
			}
			else if (!(condition is TrueCondition))
			{
				throw new UnexpectedConditionOrActionDetectedException();
			}
			return list;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00025320 File Offset: 0x00023520
		internal static Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition ConvertTaskConditionsToEngineCondition(IEnumerable<PsComplianceRulePredicateBase> predicates)
		{
			AndCondition andCondition = new AndCondition();
			foreach (PsComplianceRulePredicateBase psComplianceRulePredicateBase in predicates)
			{
				andCondition.SubConditions.Add(psComplianceRulePredicateBase.ToEnginePredicate());
			}
			if (!andCondition.SubConditions.Any<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition>())
			{
				andCondition.SubConditions.Add(Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition.True);
			}
			return new QueryPredicate(andCondition);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0002539C File Offset: 0x0002359C
		internal static IEnumerable<PsComplianceRuleActionBase> ConvertEngineActionsToTaskActions(IList<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action> actions)
		{
			return actions.Select(new Func<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action, PsComplianceRuleActionBase>(PsComplianceRuleActionBase.FromEngineAction));
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x000253B8 File Offset: 0x000235B8
		internal static IList<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action> ConvertTaskActionsToEngineActions(IEnumerable<PsComplianceRuleActionBase> actions)
		{
			return (from action in actions
			select action.ToEngineAction()).ToList<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action>();
		}

		// Token: 0x040003BA RID: 954
		private static readonly PsHoldRuleSchema schema = ObjectSchema.GetInstance<PsHoldRuleSchema>();
	}
}

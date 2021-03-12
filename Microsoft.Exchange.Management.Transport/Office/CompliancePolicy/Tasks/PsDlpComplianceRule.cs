using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000DC RID: 220
	[Serializable]
	public sealed class PsDlpComplianceRule : PsComplianceRuleBase
	{
		// Token: 0x060008CD RID: 2253 RVA: 0x0002544E File Offset: 0x0002364E
		public PsDlpComplianceRule()
		{
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00025456 File Offset: 0x00023656
		public PsDlpComplianceRule(RuleStorage ruleStorage) : base(ruleStorage)
		{
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0002545F File Offset: 0x0002365F
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return PsDlpComplianceRule.schema;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00025466 File Offset: 0x00023666
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0002546D File Offset: 0x0002366D
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x00025475 File Offset: 0x00023675
		public Hashtable[] ContentContainsSensitiveInformation { get; internal set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0002547E File Offset: 0x0002367E
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x00025490 File Offset: 0x00023690
		public AccessScope? AccessScopeIs
		{
			get
			{
				return (AccessScope?)this[PsDlpComplianceRuleSchema.AccessScopeIs];
			}
			set
			{
				this[PsDlpComplianceRuleSchema.AccessScopeIs] = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x000254A3 File Offset: 0x000236A3
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x000254B5 File Offset: 0x000236B5
		public MultiValuedProperty<string> ContentPropertyContainsWords
		{
			get
			{
				return (MultiValuedProperty<string>)this[PsDlpComplianceRuleSchema.ContentPropertyContainsWords];
			}
			set
			{
				this[PsDlpComplianceRuleSchema.ContentPropertyContainsWords] = value;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x000254C3 File Offset: 0x000236C3
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x000254D5 File Offset: 0x000236D5
		public bool BlockAccess
		{
			get
			{
				return (bool)this[PsDlpComplianceRuleSchema.BlockAccess];
			}
			set
			{
				this[PsDlpComplianceRuleSchema.BlockAccess] = value;
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000254E8 File Offset: 0x000236E8
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
					this.SetTaskConditions(PsDlpComplianceRule.ConvertEngineConditionToTaskConditions(policyRuleFromRuleBlob.Condition));
					this.SetTaskActions(PsDlpComplianceRule.ConvertEngineActionsToTaskActions(policyRuleFromRuleBlob.Actions));
				}
				base.ResetChangeTracking();
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002554C File Offset: 0x0002374C
		internal override void UpdateStorageProperties(Task task, IConfigurationSession configurationSession, bool isNewRule)
		{
			base.UpdateStorageProperties(task, configurationSession, isNewRule);
			if (base.ObjectState != ObjectState.Unchanged)
			{
				PolicyRule policyRule = new PolicyRule
				{
					Condition = PsDlpComplianceRule.ConvertTaskConditionsToEngineCondition(this.GetTaskConditions()),
					Actions = PsDlpComplianceRule.ConvertTaskActionsToEngineActions(this.GetTaskActions()),
					Comments = base.Comment,
					Enabled = (base.Enabled ? RuleState.Enabled : RuleState.Disabled),
					ImmutableId = base.Guid,
					Name = base.Name
				};
				base.RuleBlob = this.GetRuleXmlFromPolicyRule(policyRule);
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000255D8 File Offset: 0x000237D8
		internal override PolicyRule GetPolicyRuleFromRuleBlob()
		{
			RuleParser ruleParser = new RuleParser(new SimplePolicyParserFactory());
			return ruleParser.GetRule(base.RuleBlob);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000255FC File Offset: 0x000237FC
		private IEnumerable<PsComplianceRulePredicateBase> GetTaskConditions()
		{
			List<PsComplianceRulePredicateBase> list = new List<PsComplianceRulePredicateBase>();
			if (this.AccessScopeIs != null)
			{
				list.Add(new PsAccessScopeIsPredicate(this.AccessScopeIs.Value));
			}
			if (this.ContentContainsSensitiveInformation != null && this.ContentContainsSensitiveInformation.Any<Hashtable>())
			{
				list.Add(new PsContentContainsSensitiveInformationPredicate(this.ContentContainsSensitiveInformation));
			}
			if (this.ContentPropertyContainsWords != null && this.ContentPropertyContainsWords.Any<string>())
			{
				list.Add(new PsContentPropertyContainsWordsPredicate(this.ContentPropertyContainsWords));
			}
			return list;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00025688 File Offset: 0x00023888
		private void SetTaskConditions(IEnumerable<PsComplianceRulePredicateBase> conditions)
		{
			foreach (PsComplianceRulePredicateBase psComplianceRulePredicateBase in conditions)
			{
				if (psComplianceRulePredicateBase is PsAccessScopeIsPredicate)
				{
					this.AccessScopeIs = new AccessScope?((psComplianceRulePredicateBase as PsAccessScopeIsPredicate).PropertyValue);
				}
				else if (psComplianceRulePredicateBase is PsContentContainsSensitiveInformationPredicate)
				{
					PsContentContainsSensitiveInformationPredicate psContentContainsSensitiveInformationPredicate = psComplianceRulePredicateBase as PsContentContainsSensitiveInformationPredicate;
					this.ContentContainsSensitiveInformation = psContentContainsSensitiveInformationPredicate.DataClassifications;
				}
				else
				{
					if (!(psComplianceRulePredicateBase is PsContentPropertyContainsWordsPredicate))
					{
						throw new UnexpectedConditionOrActionDetectedException();
					}
					PsContentPropertyContainsWordsPredicate psContentPropertyContainsWordsPredicate = psComplianceRulePredicateBase as PsContentPropertyContainsWordsPredicate;
					this.ContentPropertyContainsWords = psContentPropertyContainsWordsPredicate.Words;
				}
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002572C File Offset: 0x0002392C
		internal IEnumerable<PsComplianceRuleActionBase> GetTaskActions()
		{
			List<PsComplianceRuleActionBase> list = new List<PsComplianceRuleActionBase>();
			if (this.BlockAccess)
			{
				list.Add(new PsBlockAccessAction());
			}
			return list;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00025754 File Offset: 0x00023954
		private void SetTaskActions(IEnumerable<PsComplianceRuleActionBase> actions)
		{
			foreach (PsComplianceRuleActionBase psComplianceRuleActionBase in actions)
			{
				if (!(psComplianceRuleActionBase is PsBlockAccessAction))
				{
					throw new UnexpectedConditionOrActionDetectedException();
				}
				this.BlockAccess = true;
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000257AC File Offset: 0x000239AC
		internal static IEnumerable<PsComplianceRulePredicateBase> ConvertEngineConditionToTaskConditions(Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition condition)
		{
			List<PsComplianceRulePredicateBase> list = new List<PsComplianceRulePredicateBase>();
			if (condition is AndCondition)
			{
				AndCondition andCondition = condition as AndCondition;
				using (List<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition>.Enumerator enumerator = andCondition.SubConditions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition condition2 = enumerator.Current;
						if (condition2 is PredicateCondition)
						{
							list.Add(PsComplianceRulePredicateBase.FromEnginePredicate(condition2 as PredicateCondition));
						}
						else if (!(condition2 is TrueCondition))
						{
							throw new UnexpectedConditionOrActionDetectedException();
						}
					}
					return list;
				}
			}
			throw new UnexpectedConditionOrActionDetectedException();
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00025840 File Offset: 0x00023A40
		internal static Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition ConvertTaskConditionsToEngineCondition(IEnumerable<PsComplianceRulePredicateBase> predicates)
		{
			AndCondition andCondition = new AndCondition();
			foreach (PsComplianceRulePredicateBase psComplianceRulePredicateBase in predicates)
			{
				andCondition.SubConditions.Add(psComplianceRulePredicateBase.ToEnginePredicate());
			}
			if (!andCondition.SubConditions.Any<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Condition>())
			{
				andCondition.SubConditions.Add(new TrueCondition());
			}
			return andCondition;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x000258B8 File Offset: 0x00023AB8
		internal static IEnumerable<PsComplianceRuleActionBase> ConvertEngineActionsToTaskActions(IList<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action> actions)
		{
			return actions.Select(new Func<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action, PsComplianceRuleActionBase>(PsComplianceRuleActionBase.FromEngineAction));
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x000258D4 File Offset: 0x00023AD4
		internal static IList<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action> ConvertTaskActionsToEngineActions(IEnumerable<PsComplianceRuleActionBase> actions)
		{
			return (from action in actions
			select action.ToEngineAction()).ToList<Microsoft.Office.CompliancePolicy.PolicyEvaluation.Action>();
		}

		// Token: 0x040003C1 RID: 961
		private static readonly PsDlpComplianceRuleSchema schema = ObjectSchema.GetInstance<PsDlpComplianceRuleSchema>();
	}
}

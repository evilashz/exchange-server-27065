using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000033 RID: 51
	internal class RulesEvaluationHistory
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000615C File Offset: 0x0000435C
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00006164 File Offset: 0x00004364
		internal Dictionary<Guid, RuleEvaluationResult> History { get; private set; }

		// Token: 0x06000160 RID: 352 RVA: 0x0000616D File Offset: 0x0000436D
		internal RulesEvaluationHistory()
		{
			this.History = new Dictionary<Guid, RuleEvaluationResult>();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006180 File Offset: 0x00004380
		internal void AddRuleEvaluationResult(RulesEvaluationContext context)
		{
			if (context != null && context.CurrentRule != null && !this.History.ContainsKey(context.CurrentRule.ImmutableId))
			{
				this.History.Add(context.CurrentRule.ImmutableId, new RuleEvaluationResult());
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000061C0 File Offset: 0x000043C0
		internal PredicateEvaluationResult AddPredicateEvaluationResult(RulesEvaluationContext context)
		{
			RuleEvaluationResult ruleEvaluationResult;
			if (context != null && context.CurrentRule != null && this.History.TryGetValue(context.CurrentRule.ImmutableId, out ruleEvaluationResult))
			{
				PredicateEvaluationResult predicateEvaluationResult = new PredicateEvaluationResult();
				ruleEvaluationResult.Predicates.Add(predicateEvaluationResult);
				return predicateEvaluationResult;
			}
			return null;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006208 File Offset: 0x00004408
		internal PredicateEvaluationResult AddPredicateEvaluationResult(RulesEvaluationContext context, Type predicateType, bool isMatch, IList<string> matchingValues, int supplementalInfo)
		{
			RuleEvaluationResult ruleEvaluationResult;
			if (context != null && context.CurrentRule != null && this.History.TryGetValue(context.CurrentRule.ImmutableId, out ruleEvaluationResult))
			{
				PredicateEvaluationResult predicateEvaluationResult = new PredicateEvaluationResult(predicateType, isMatch, matchingValues, supplementalInfo);
				ruleEvaluationResult.Predicates.Add(predicateEvaluationResult);
				return predicateEvaluationResult;
			}
			return null;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006258 File Offset: 0x00004458
		internal void SetCurrentRuleIsMatch(RulesEvaluationContext context, bool isMatch)
		{
			RuleEvaluationResult currentRuleResult = this.GetCurrentRuleResult(context);
			if (currentRuleResult != null)
			{
				currentRuleResult.IsMatch = isMatch;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006278 File Offset: 0x00004478
		internal RuleEvaluationResult GetCurrentRuleResult(RulesEvaluationContext context)
		{
			RuleEvaluationResult result = null;
			if (context != null && context.Rules != null && context.CurrentRule != null)
			{
				this.History.TryGetValue(context.CurrentRule.ImmutableId, out result);
			}
			return result;
		}
	}
}

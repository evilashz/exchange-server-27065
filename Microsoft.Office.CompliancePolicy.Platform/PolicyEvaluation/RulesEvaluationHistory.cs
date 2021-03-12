using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000E0 RID: 224
	public class RulesEvaluationHistory
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x00012AFC File Offset: 0x00010CFC
		internal RulesEvaluationHistory()
		{
			this.History = new List<RuleEvaluationResult>();
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00012B0F File Offset: 0x00010D0F
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x00012B17 File Offset: 0x00010D17
		public IList<RuleEvaluationResult> History { get; private set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00012B30 File Offset: 0x00010D30
		public TimeSpan TimeSpent
		{
			get
			{
				TimeSpan seed = default(TimeSpan);
				return this.History.Aggregate(seed, (TimeSpan current, RuleEvaluationResult result) => current + result.TimeSpent);
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00012B6E File Offset: 0x00010D6E
		public void AddRuleEvaluationResult(PolicyEvaluationContext context)
		{
			if (context != null && context.CurrentRule != null && context.ComplianceItemPagedReader == null)
			{
				this.History.Add(new RuleEvaluationResult(context.CurrentRule.ImmutableId));
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00012BC4 File Offset: 0x00010DC4
		public PredicateEvaluationResult AddPredicateEvaluationResult(PolicyEvaluationContext context)
		{
			if (context != null && context.CurrentRule != null && context.ComplianceItemPagedReader == null)
			{
				RuleEvaluationResult ruleEvaluationResult = this.History.FirstOrDefault((RuleEvaluationResult h) => h.RuleId == context.CurrentRule.ImmutableId);
				if (ruleEvaluationResult != null)
				{
					PredicateEvaluationResult predicateEvaluationResult = new PredicateEvaluationResult();
					ruleEvaluationResult.Predicates.Add(predicateEvaluationResult);
					return predicateEvaluationResult;
				}
			}
			return null;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00012C5C File Offset: 0x00010E5C
		internal RuleEvaluationResult GetCurrentRuleResult(PolicyEvaluationContext context)
		{
			RuleEvaluationResult result = null;
			if (context != null && context.CurrentRule != null && context.ComplianceItemPagedReader == null)
			{
				result = this.History.FirstOrDefault((RuleEvaluationResult h) => h.RuleId == context.CurrentRule.ImmutableId);
			}
			return result;
		}
	}
}

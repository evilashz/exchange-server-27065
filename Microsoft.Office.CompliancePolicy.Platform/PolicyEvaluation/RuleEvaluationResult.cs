using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000DD RID: 221
	public class RuleEvaluationResult
	{
		// Token: 0x0600058E RID: 1422 RVA: 0x00010F09 File Offset: 0x0000F109
		public RuleEvaluationResult()
		{
			this.Predicates = new List<PredicateEvaluationResult>();
			this.Actions = new List<PolicyHistoryResult>();
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00010F27 File Offset: 0x0000F127
		public RuleEvaluationResult(Guid ruleId) : this()
		{
			this.RuleId = ruleId;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00010F3E File Offset: 0x0000F13E
		public bool IsMatch
		{
			get
			{
				return this.Predicates.All((PredicateEvaluationResult p) => p.IsMatch);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00010F68 File Offset: 0x0000F168
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x00010F70 File Offset: 0x0000F170
		public Guid RuleId { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00010F79 File Offset: 0x0000F179
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x00010F81 File Offset: 0x0000F181
		public IList<PredicateEvaluationResult> Predicates { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00010F8A File Offset: 0x0000F18A
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x00010F92 File Offset: 0x0000F192
		public IList<PolicyHistoryResult> Actions { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x00010FB8 File Offset: 0x0000F1B8
		public TimeSpan TimeSpent
		{
			get
			{
				TimeSpan timeSpan = default(TimeSpan);
				timeSpan = this.Predicates.Aggregate(timeSpan, (TimeSpan current, PredicateEvaluationResult predicate) => current + predicate.TimeSpent);
				return timeSpan + this.Actions.Aggregate(timeSpan, (TimeSpan current, PolicyHistoryResult action) => current + action.TimeSpent);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200002E RID: 46
	internal class RuleEvaluationResult
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004665 File Offset: 0x00002865
		// (set) Token: 0x0600010D RID: 269 RVA: 0x0000466D File Offset: 0x0000286D
		internal bool IsMatch { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00004676 File Offset: 0x00002876
		// (set) Token: 0x0600010F RID: 271 RVA: 0x0000467E File Offset: 0x0000287E
		internal IList<PredicateEvaluationResult> Predicates { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004687 File Offset: 0x00002887
		// (set) Token: 0x06000111 RID: 273 RVA: 0x0000468F File Offset: 0x0000288F
		internal IList<string> Actions { get; set; }

		// Token: 0x06000112 RID: 274 RVA: 0x00004698 File Offset: 0x00002898
		internal RuleEvaluationResult()
		{
			this.Predicates = new List<PredicateEvaluationResult>();
			this.Actions = new List<string>();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000046D4 File Offset: 0x000028D4
		internal static IList<PredicateEvaluationResult> GetPredicateEvaluationResult(Type predicateType, IList<PredicateEvaluationResult> predicates)
		{
			return (from predicateEvaluationResult in predicates
			where predicateEvaluationResult.Type == predicateType
			select predicateEvaluationResult).ToList<PredicateEvaluationResult>();
		}
	}
}

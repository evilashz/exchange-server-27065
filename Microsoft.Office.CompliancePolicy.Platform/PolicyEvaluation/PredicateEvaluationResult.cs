using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000C7 RID: 199
	public class PredicateEvaluationResult : PolicyHistoryResult
	{
		// Token: 0x0600050B RID: 1291 RVA: 0x0000F546 File Offset: 0x0000D746
		public PredicateEvaluationResult()
		{
			this.IsMatch = false;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000F555 File Offset: 0x0000D755
		public PredicateEvaluationResult(Type predicateType, bool isMatch, IEnumerable<string> matchingValues, int supplementalInfo, TimeSpan timeSpent) : base(predicateType, matchingValues, supplementalInfo, timeSpent)
		{
			this.IsMatch = isMatch;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000F56A File Offset: 0x0000D76A
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x0000F572 File Offset: 0x0000D772
		public bool IsMatch { get; set; }
	}
}

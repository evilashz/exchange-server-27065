using System;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000D2 RID: 210
	public sealed class PsContentMatchQueryPredicate : PsComplianceRulePredicateBase
	{
		// Token: 0x0600088F RID: 2191 RVA: 0x0002492E File Offset: 0x00022B2E
		public PsContentMatchQueryPredicate(string textQuery)
		{
			this.TextQuery = textQuery;
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0002493D File Offset: 0x00022B3D
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x00024945 File Offset: 0x00022B45
		public string TextQuery { get; private set; }

		// Token: 0x06000892 RID: 2194 RVA: 0x0002494E File Offset: 0x00022B4E
		internal override PredicateCondition ToEnginePredicate()
		{
			return new TextQueryPredicate(this.TextQuery);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0002495B File Offset: 0x00022B5B
		internal static PsContentMatchQueryPredicate FromEnginePredicate(TextQueryPredicate condition)
		{
			return new PsContentMatchQueryPredicate(condition.TextQuery);
		}
	}
}

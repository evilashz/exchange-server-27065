using System;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000D7 RID: 215
	public class QueryPredicate : Condition
	{
		// Token: 0x06000572 RID: 1394 RVA: 0x00010A6C File Offset: 0x0000EC6C
		public QueryPredicate(Condition subcondition)
		{
			this.SubCondition = subcondition;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00010A7B File Offset: 0x0000EC7B
		public virtual string Name
		{
			get
			{
				return "queryMatch";
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00010A82 File Offset: 0x0000EC82
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.DynamicQuery;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00010A85 File Offset: 0x0000EC85
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00010A8D File Offset: 0x0000EC8D
		public Condition SubCondition { get; set; }

		// Token: 0x06000577 RID: 1399 RVA: 0x00010A96 File Offset: 0x0000EC96
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			return true;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00010A99 File Offset: 0x0000EC99
		public override void GetSupplementalData(SupplementalData data)
		{
			this.SubCondition.GetSupplementalData(data);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00010AA7 File Offset: 0x0000ECA7
		public virtual string BuildQuery()
		{
			return string.Empty;
		}
	}
}

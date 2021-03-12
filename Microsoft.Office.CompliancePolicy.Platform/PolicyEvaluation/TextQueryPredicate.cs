using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000D8 RID: 216
	public class TextQueryPredicate : PredicateCondition
	{
		// Token: 0x0600057A RID: 1402 RVA: 0x00010AB0 File Offset: 0x0000ECB0
		public TextQueryPredicate(string textQuery) : this(new Property("QueryProperty", typeof(string)), new List<string>
		{
			textQuery
		})
		{
			this.TextQuery = textQuery;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00010AEC File Offset: 0x0000ECEC
		internal TextQueryPredicate(Property property, List<string> entries) : base(property, entries)
		{
			this.TextQuery = string.Join(" OR ", entries);
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00010B12 File Offset: 0x0000ED12
		public override string Name
		{
			get
			{
				return "textQueryMatch";
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00010B19 File Offset: 0x0000ED19
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.Predicate;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00010B1C File Offset: 0x0000ED1C
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x00010B24 File Offset: 0x0000ED24
		public string TextQuery
		{
			get
			{
				return this.textQuery;
			}
			set
			{
				if (value.Length > this.MaxSize)
				{
					throw new CompliancePolicyValidationException("Text query length of {0} exceeds maximum allowed length {1}", new object[]
					{
						value.Length,
						this.MaxSize
					});
				}
				this.textQuery = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00010B75 File Offset: 0x0000ED75
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x00010B7D File Offset: 0x0000ED7D
		public int MaxSize
		{
			get
			{
				return this.maxSize;
			}
			set
			{
				this.maxSize = value;
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00010B86 File Offset: 0x0000ED86
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			return true;
		}

		// Token: 0x04000339 RID: 825
		private int maxSize = 16384;

		// Token: 0x0400033A RID: 826
		private string textQuery;
	}
}

using System;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000D0 RID: 208
	public sealed class PsContentDateFromPredicate : PsSinglePropertyPredicate<DateTime>
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x000248E0 File Offset: 0x00022AE0
		protected override string PropertyNameForEnginePredicate
		{
			get
			{
				return "Item.WhenCreated";
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x000248E7 File Offset: 0x00022AE7
		public PsContentDateFromPredicate(DateTime contentDate) : base(contentDate)
		{
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000248F0 File Offset: 0x00022AF0
		internal static PsContentDateFromPredicate FromEnginePredicate(GreaterThanOrEqualPredicate condition)
		{
			return new PsContentDateFromPredicate((DateTime)condition.Value.ParsedValue);
		}
	}
}

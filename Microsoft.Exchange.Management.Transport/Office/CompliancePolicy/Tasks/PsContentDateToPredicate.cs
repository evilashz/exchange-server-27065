using System;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000D1 RID: 209
	public sealed class PsContentDateToPredicate : PsSinglePropertyPredicate<DateTime>
	{
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00024907 File Offset: 0x00022B07
		protected override string PropertyNameForEnginePredicate
		{
			get
			{
				return "Item.WhenCreated";
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0002490E File Offset: 0x00022B0E
		public PsContentDateToPredicate(DateTime contentDate) : base(contentDate)
		{
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00024917 File Offset: 0x00022B17
		internal static PsContentDateToPredicate FromEnginePredicate(LessThanOrEqualPredicate condition)
		{
			return new PsContentDateToPredicate((DateTime)condition.Value.ParsedValue);
		}
	}
}

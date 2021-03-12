using System;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000D3 RID: 211
	public sealed class PsAccessScopeIsPredicate : PsSinglePropertyPredicate<AccessScope>
	{
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x00024968 File Offset: 0x00022B68
		protected override string PropertyNameForEnginePredicate
		{
			get
			{
				return "Item.AccessScope";
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0002496F File Offset: 0x00022B6F
		public PsAccessScopeIsPredicate(AccessScope accessScope) : base(accessScope)
		{
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00024978 File Offset: 0x00022B78
		public static PsAccessScopeIsPredicate FromEnginePredicate(EqualPredicate condition)
		{
			return new PsAccessScopeIsPredicate((AccessScope)condition.Value.ParsedValue);
		}
	}
}

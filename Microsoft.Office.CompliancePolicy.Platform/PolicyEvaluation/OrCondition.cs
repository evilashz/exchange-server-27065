using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000C2 RID: 194
	public sealed class OrCondition : Condition
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000E9B7 File Offset: 0x0000CBB7
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.Or;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000E9BA File Offset: 0x0000CBBA
		public List<Condition> SubConditions
		{
			get
			{
				return this.subConditions;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0000E9C4 File Offset: 0x0000CBC4
		public override Version MinimumVersion
		{
			get
			{
				Version version = Condition.BaseVersion;
				foreach (Condition condition in this.SubConditions)
				{
					Version minimumVersion = condition.MinimumVersion;
					if (version < minimumVersion)
					{
						version = minimumVersion;
					}
				}
				return version;
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000EA2C File Offset: 0x0000CC2C
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			bool result = false;
			foreach (Condition condition in this.subConditions)
			{
				if (condition.Evaluate(context))
				{
					result = true;
					if (base.EvaluationMode == ConditionEvaluationMode.Optimized)
					{
						return true;
					}
				}
			}
			return result;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000EA98 File Offset: 0x0000CC98
		public override void GetSupplementalData(SupplementalData data)
		{
			foreach (Condition condition in this.SubConditions)
			{
				condition.GetSupplementalData(data);
			}
		}

		// Token: 0x040002FC RID: 764
		private List<Condition> subConditions = new List<Condition>();
	}
}

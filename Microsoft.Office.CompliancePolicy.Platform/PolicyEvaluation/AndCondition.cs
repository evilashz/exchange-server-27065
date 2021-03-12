using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000BD RID: 189
	public sealed class AndCondition : Condition
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000E83B File Offset: 0x0000CA3B
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.And;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000E83E File Offset: 0x0000CA3E
		public List<Condition> SubConditions
		{
			get
			{
				return this.subConditions;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000E848 File Offset: 0x0000CA48
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

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000E8C8 File Offset: 0x0000CAC8
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			return this.subConditions.All((Condition condition) => condition.Evaluate(context));
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000E8FC File Offset: 0x0000CAFC
		public override void GetSupplementalData(SupplementalData data)
		{
			foreach (Condition condition in this.SubConditions)
			{
				condition.GetSupplementalData(data);
			}
		}

		// Token: 0x040002EF RID: 751
		private List<Condition> subConditions = new List<Condition>();
	}
}

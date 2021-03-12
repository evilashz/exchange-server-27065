using System;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000C1 RID: 193
	public sealed class NotCondition : Condition
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x0000E971 File Offset: 0x0000CB71
		public NotCondition(Condition subCondition)
		{
			this.subCondition = subCondition;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000E980 File Offset: 0x0000CB80
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.Not;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000E983 File Offset: 0x0000CB83
		public Condition SubCondition
		{
			get
			{
				return this.subCondition;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000E98B File Offset: 0x0000CB8B
		public override Version MinimumVersion
		{
			get
			{
				return this.subCondition.MinimumVersion;
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000E998 File Offset: 0x0000CB98
		public override bool Evaluate(PolicyEvaluationContext context)
		{
			return !this.subCondition.Evaluate(context);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000E9A9 File Offset: 0x0000CBA9
		public override void GetSupplementalData(SupplementalData data)
		{
			this.SubCondition.GetSupplementalData(data);
		}

		// Token: 0x040002FB RID: 763
		private Condition subCondition;
	}
}

using System;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000BC RID: 188
	public abstract class Condition
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000E7DF File Offset: 0x0000C9DF
		public static TrueCondition True
		{
			get
			{
				return Condition.trueCondition;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000E7E6 File Offset: 0x0000C9E6
		public static FalseCondition False
		{
			get
			{
				return Condition.falseCondition;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000E7ED File Offset: 0x0000C9ED
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x0000E7F5 File Offset: 0x0000C9F5
		public ConditionEvaluationMode EvaluationMode
		{
			get
			{
				return this.evaluationMode;
			}
			set
			{
				this.evaluationMode = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004AD RID: 1197
		public abstract ConditionType ConditionType { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000E7FE File Offset: 0x0000C9FE
		public virtual Version MinimumVersion
		{
			get
			{
				return Condition.BaseVersion;
			}
		}

		// Token: 0x060004AF RID: 1199
		public abstract bool Evaluate(PolicyEvaluationContext context);

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000E805 File Offset: 0x0000CA05
		public virtual void GetSupplementalData(SupplementalData data)
		{
		}

		// Token: 0x040002EB RID: 747
		public static readonly Version BaseVersion = new Version("1.00.0000.000");

		// Token: 0x040002EC RID: 748
		private static readonly TrueCondition trueCondition = new TrueCondition();

		// Token: 0x040002ED RID: 749
		private static readonly FalseCondition falseCondition = new FalseCondition();

		// Token: 0x040002EE RID: 750
		private ConditionEvaluationMode evaluationMode = ConditionEvaluationMode.Optimized;
	}
}

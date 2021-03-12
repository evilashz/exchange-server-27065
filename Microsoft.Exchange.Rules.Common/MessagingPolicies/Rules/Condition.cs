using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000003 RID: 3
	public abstract class Condition
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002364 File Offset: 0x00000564
		public static TrueCondition True
		{
			get
			{
				return Condition.trueCondition;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000236B File Offset: 0x0000056B
		public static FalseCondition False
		{
			get
			{
				return Condition.falseCondition;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002372 File Offset: 0x00000572
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000237A File Offset: 0x0000057A
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21
		public abstract ConditionType ConditionType { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002383 File Offset: 0x00000583
		public virtual Version MinimumVersion
		{
			get
			{
				return Rule.BaseVersion;
			}
		}

		// Token: 0x06000017 RID: 23
		public abstract bool Evaluate(RulesEvaluationContext context);

		// Token: 0x06000018 RID: 24 RVA: 0x0000238A File Offset: 0x0000058A
		public virtual void GetSupplementalData(SupplementalData data)
		{
		}

		// Token: 0x04000005 RID: 5
		private static readonly TrueCondition trueCondition = new TrueCondition();

		// Token: 0x04000006 RID: 6
		private static readonly FalseCondition falseCondition = new FalseCondition();

		// Token: 0x04000007 RID: 7
		private ConditionEvaluationMode evaluationMode = ConditionEvaluationMode.Optimized;
	}
}

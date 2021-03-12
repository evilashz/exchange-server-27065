using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000014 RID: 20
	internal sealed class NotCondition : Condition
	{
		// Token: 0x0600006A RID: 106 RVA: 0x000030A3 File Offset: 0x000012A3
		public NotCondition(Condition subCondition)
		{
			this.subCondition = subCondition;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000030B2 File Offset: 0x000012B2
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.Not;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000030B5 File Offset: 0x000012B5
		public Condition SubCondition
		{
			get
			{
				return this.subCondition;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000030BD File Offset: 0x000012BD
		public override Version MinimumVersion
		{
			get
			{
				return this.subCondition.MinimumVersion;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000030CA File Offset: 0x000012CA
		public override bool Evaluate(RulesEvaluationContext context)
		{
			return !this.subCondition.Evaluate(context);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000030DB File Offset: 0x000012DB
		public override void GetSupplementalData(SupplementalData data)
		{
			this.subCondition.GetSupplementalData(data);
		}

		// Token: 0x04000024 RID: 36
		private Condition subCondition;
	}
}

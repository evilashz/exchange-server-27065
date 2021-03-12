using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000015 RID: 21
	public class OrCondition : Condition
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000030E9 File Offset: 0x000012E9
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.Or;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000030EC File Offset: 0x000012EC
		public List<Condition> SubConditions
		{
			get
			{
				return this.subConditions;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000030F4 File Offset: 0x000012F4
		public override Version MinimumVersion
		{
			get
			{
				Version version = Rule.BaseVersion;
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

		// Token: 0x06000073 RID: 115 RVA: 0x0000315C File Offset: 0x0000135C
		public override bool Evaluate(RulesEvaluationContext context)
		{
			bool result = false;
			foreach (Condition condition in this.subConditions)
			{
				if (condition.Evaluate(context))
				{
					context.Trace("Subcondition '{0}' evaluated as Match", new object[]
					{
						condition.ConditionType
					});
					result = true;
					if (base.EvaluationMode == ConditionEvaluationMode.Optimized)
					{
						return true;
					}
				}
				else
				{
					context.Trace("Subcondition '{0}' evaluated as Not Match", new object[]
					{
						condition.ConditionType
					});
				}
			}
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003210 File Offset: 0x00001410
		public override void GetSupplementalData(SupplementalData data)
		{
			foreach (Condition condition in this.subConditions)
			{
				condition.GetSupplementalData(data);
			}
		}

		// Token: 0x04000025 RID: 37
		private List<Condition> subConditions = new List<Condition>();
	}
}

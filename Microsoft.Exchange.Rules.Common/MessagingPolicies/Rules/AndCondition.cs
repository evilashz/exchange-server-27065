using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000004 RID: 4
	internal sealed class AndCondition : Condition
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000023B1 File Offset: 0x000005B1
		public override ConditionType ConditionType
		{
			get
			{
				return ConditionType.And;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000023B4 File Offset: 0x000005B4
		public List<Condition> SubConditions
		{
			get
			{
				return this.subConditions;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000023BC File Offset: 0x000005BC
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

		// Token: 0x0600001E RID: 30 RVA: 0x00002424 File Offset: 0x00000624
		public override bool Evaluate(RulesEvaluationContext context)
		{
			foreach (Condition condition in this.subConditions)
			{
				if (!condition.Evaluate(context))
				{
					context.Trace("Condition '{0}' evaluated as Not Match", new object[]
					{
						condition.ConditionType
					});
					return false;
				}
				context.Trace("Condition '{0}' evaluated as Match", new object[]
				{
					condition.ConditionType
				});
			}
			return true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024C8 File Offset: 0x000006C8
		public override void GetSupplementalData(SupplementalData data)
		{
			foreach (Condition condition in this.subConditions)
			{
				condition.GetSupplementalData(data);
			}
		}

		// Token: 0x04000008 RID: 8
		private List<Condition> subConditions = new List<Condition>();
	}
}

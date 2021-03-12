using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B9F RID: 2975
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MarkedAsSensitivityCondition : Condition
	{
		// Token: 0x06006AA7 RID: 27303 RVA: 0x001C75E3 File Offset: 0x001C57E3
		private MarkedAsSensitivityCondition(Rule rule, Sensitivity sensitivity) : base(ConditionType.MarkedAsSensitivityCondition, rule)
		{
			this.sensitivity = sensitivity;
		}

		// Token: 0x06006AA8 RID: 27304 RVA: 0x001C75F4 File Offset: 0x001C57F4
		public static MarkedAsSensitivityCondition Create(Rule rule, Sensitivity sensitivity)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			EnumValidator.ThrowIfInvalid<Sensitivity>(sensitivity, "sensitivity");
			return new MarkedAsSensitivityCondition(rule, sensitivity);
		}

		// Token: 0x17001D12 RID: 7442
		// (get) Token: 0x06006AA9 RID: 27305 RVA: 0x001C7624 File Offset: 0x001C5824
		public Sensitivity Sensitivity
		{
			get
			{
				return this.sensitivity;
			}
		}

		// Token: 0x06006AAA RID: 27306 RVA: 0x001C762C File Offset: 0x001C582C
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateIntPropertyRestriction(PropTag.Sensitivity, (int)this.Sensitivity, Restriction.RelOp.Equal);
		}

		// Token: 0x04003CE5 RID: 15589
		private readonly Sensitivity sensitivity;
	}
}

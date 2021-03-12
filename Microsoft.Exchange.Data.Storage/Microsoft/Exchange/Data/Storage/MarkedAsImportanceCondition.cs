using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B9E RID: 2974
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MarkedAsImportanceCondition : Condition
	{
		// Token: 0x06006AA3 RID: 27299 RVA: 0x001C7586 File Offset: 0x001C5786
		private MarkedAsImportanceCondition(Rule rule, Importance importance) : base(ConditionType.MarkedAsImportanceCondition, rule)
		{
			this.importance = importance;
		}

		// Token: 0x06006AA4 RID: 27300 RVA: 0x001C7598 File Offset: 0x001C5798
		public static MarkedAsImportanceCondition Create(Rule rule, Importance importance)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			EnumValidator.ThrowIfInvalid<Importance>(importance, "importance");
			return new MarkedAsImportanceCondition(rule, importance);
		}

		// Token: 0x17001D11 RID: 7441
		// (get) Token: 0x06006AA5 RID: 27301 RVA: 0x001C75C8 File Offset: 0x001C57C8
		public Importance Importance
		{
			get
			{
				return this.importance;
			}
		}

		// Token: 0x06006AA6 RID: 27302 RVA: 0x001C75D0 File Offset: 0x001C57D0
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateIntPropertyRestriction(PropTag.Importance, (int)this.Importance, Restriction.RelOp.Equal);
		}

		// Token: 0x04003CE4 RID: 15588
		private readonly Importance importance;
	}
}

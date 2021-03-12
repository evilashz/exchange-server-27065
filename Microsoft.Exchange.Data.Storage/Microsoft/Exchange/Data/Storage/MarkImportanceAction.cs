using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B7E RID: 2942
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MarkImportanceAction : ActionBase
	{
		// Token: 0x06006A2C RID: 27180 RVA: 0x001C5E87 File Offset: 0x001C4087
		private MarkImportanceAction(ActionType actionType, Importance importance, Rule rule) : base(actionType, rule)
		{
			this.importance = importance;
		}

		// Token: 0x06006A2D RID: 27181 RVA: 0x001C5E98 File Offset: 0x001C4098
		public static MarkImportanceAction Create(Importance importance, Rule rule)
		{
			ActionBase.CheckParams(new object[]
			{
				rule
			});
			EnumValidator.ThrowIfInvalid<Importance>(importance, "importance");
			return new MarkImportanceAction(ActionType.MarkImportanceAction, importance, rule);
		}

		// Token: 0x17001D02 RID: 7426
		// (get) Token: 0x06006A2E RID: 27182 RVA: 0x001C5EC9 File Offset: 0x001C40C9
		public Importance Importance
		{
			get
			{
				return this.importance;
			}
		}

		// Token: 0x17001D03 RID: 7427
		// (get) Token: 0x06006A2F RID: 27183 RVA: 0x001C5ED1 File Offset: 0x001C40D1
		public override Rule.ProviderIdEnum ProviderId
		{
			get
			{
				return Rule.ProviderIdEnum.Exchange14;
			}
		}

		// Token: 0x06006A30 RID: 27184 RVA: 0x001C5ED4 File Offset: 0x001C40D4
		internal override RuleAction BuildRuleAction()
		{
			return new RuleAction.Tag(new PropValue(PropTag.Importance, this.Importance));
		}

		// Token: 0x04003C6A RID: 15466
		private readonly Importance importance;
	}
}

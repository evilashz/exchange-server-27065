using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B82 RID: 2946
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FlagMessageAction : ActionBase
	{
		// Token: 0x06006A3D RID: 27197 RVA: 0x001C6034 File Offset: 0x001C4234
		private FlagMessageAction(ActionType actionType, FlagStatus flagStatus, Rule rule) : base(actionType, rule)
		{
			this.flagStatus = flagStatus;
		}

		// Token: 0x06006A3E RID: 27198 RVA: 0x001C6048 File Offset: 0x001C4248
		public static FlagMessageAction Create(FlagStatus flagStatus, Rule rule)
		{
			ActionBase.CheckParams(new object[]
			{
				rule
			});
			EnumValidator.ThrowIfInvalid<FlagStatus>(flagStatus, "flagStatus");
			return new FlagMessageAction(ActionType.FlagMessageAction, flagStatus, rule);
		}

		// Token: 0x17001D07 RID: 7431
		// (get) Token: 0x06006A3F RID: 27199 RVA: 0x001C607A File Offset: 0x001C427A
		public FlagStatus FlagStatus
		{
			get
			{
				return this.flagStatus;
			}
		}

		// Token: 0x06006A40 RID: 27200 RVA: 0x001C6084 File Offset: 0x001C4284
		internal override RuleAction BuildRuleAction()
		{
			PropTag propTag = (PropTag)277872643U;
			return new RuleAction.Tag(new PropValue(propTag, this.flagStatus));
		}

		// Token: 0x04003C6D RID: 15469
		private readonly FlagStatus flagStatus;
	}
}

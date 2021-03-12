using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B83 RID: 2947
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MarkAsReadAction : Action
	{
		// Token: 0x06006A41 RID: 27201 RVA: 0x001C60AF File Offset: 0x001C42AF
		private MarkAsReadAction(Rule rule) : base(ActionType.MarkAsReadAction, rule)
		{
		}

		// Token: 0x06006A42 RID: 27202 RVA: 0x001C60BC File Offset: 0x001C42BC
		public static MarkAsReadAction Create(Rule rule)
		{
			ActionBase.CheckParams(new object[]
			{
				rule
			});
			return new MarkAsReadAction(rule);
		}

		// Token: 0x17001D08 RID: 7432
		// (get) Token: 0x06006A43 RID: 27203 RVA: 0x001C60E0 File Offset: 0x001C42E0
		public override Rule.ProviderIdEnum ProviderId
		{
			get
			{
				return Rule.ProviderIdEnum.Exchange14;
			}
		}

		// Token: 0x06006A44 RID: 27204 RVA: 0x001C60E4 File Offset: 0x001C42E4
		internal override RuleAction BuildRuleAction()
		{
			return new RuleAction.MarkAsRead();
		}
	}
}

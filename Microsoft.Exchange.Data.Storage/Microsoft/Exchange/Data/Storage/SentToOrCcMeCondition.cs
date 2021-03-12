using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B8C RID: 2956
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SentToOrCcMeCondition : Condition
	{
		// Token: 0x06006A6F RID: 27247 RVA: 0x001C6C0B File Offset: 0x001C4E0B
		private SentToOrCcMeCondition(Rule rule) : base(ConditionType.SentToOrCcMeCondition, rule)
		{
		}

		// Token: 0x06006A70 RID: 27248 RVA: 0x001C6C18 File Offset: 0x001C4E18
		public static SentToOrCcMeCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			return new SentToOrCcMeCondition(rule);
		}

		// Token: 0x06006A71 RID: 27249 RVA: 0x001C6C3C File Offset: 0x001C4E3C
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateBooleanPropertyRestriction(PropTag.MessageRecipMe, true, Restriction.RelOp.Equal);
		}
	}
}

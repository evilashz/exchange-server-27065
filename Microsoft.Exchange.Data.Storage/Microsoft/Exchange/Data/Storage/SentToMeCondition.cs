using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B8A RID: 2954
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SentToMeCondition : Condition
	{
		// Token: 0x06006A69 RID: 27241 RVA: 0x001C6B93 File Offset: 0x001C4D93
		private SentToMeCondition(Rule rule) : base(ConditionType.SentToMeCondition, rule)
		{
		}

		// Token: 0x06006A6A RID: 27242 RVA: 0x001C6BA0 File Offset: 0x001C4DA0
		public static SentToMeCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			return new SentToMeCondition(rule);
		}

		// Token: 0x06006A6B RID: 27243 RVA: 0x001C6BC4 File Offset: 0x001C4DC4
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateBooleanPropertyRestriction(PropTag.MessageToMe, true, Restriction.RelOp.Equal);
		}
	}
}

using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B8D RID: 2957
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NotSentToMeCondition : Condition
	{
		// Token: 0x06006A72 RID: 27250 RVA: 0x001C6C4A File Offset: 0x001C4E4A
		private NotSentToMeCondition(Rule rule) : base(ConditionType.NotSentToMeCondition, rule)
		{
		}

		// Token: 0x06006A73 RID: 27251 RVA: 0x001C6C58 File Offset: 0x001C4E58
		public static NotSentToMeCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			return new NotSentToMeCondition(rule);
		}

		// Token: 0x06006A74 RID: 27252 RVA: 0x001C6C7C File Offset: 0x001C4E7C
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateBooleanPropertyRestriction(PropTag.MessageToMe, false, Restriction.RelOp.Equal);
		}
	}
}

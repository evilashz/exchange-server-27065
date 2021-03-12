using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B8B RID: 2955
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SentCcMeCondition : Condition
	{
		// Token: 0x06006A6C RID: 27244 RVA: 0x001C6BD2 File Offset: 0x001C4DD2
		private SentCcMeCondition(Rule rule) : base(ConditionType.SentCcMeCondition, rule)
		{
		}

		// Token: 0x06006A6D RID: 27245 RVA: 0x001C6BE0 File Offset: 0x001C4DE0
		public static SentCcMeCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			return new SentCcMeCondition(rule);
		}

		// Token: 0x06006A6E RID: 27246 RVA: 0x001C6C04 File Offset: 0x001C4E04
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateCcToMeRestriction();
		}
	}
}

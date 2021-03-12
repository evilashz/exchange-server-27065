using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B89 RID: 2953
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SentOnlyToMeCondition : Condition
	{
		// Token: 0x06006A66 RID: 27238 RVA: 0x001C6B5C File Offset: 0x001C4D5C
		private SentOnlyToMeCondition(Rule rule) : base(ConditionType.SentOnlyToMeCondition, rule)
		{
		}

		// Token: 0x06006A67 RID: 27239 RVA: 0x001C6B68 File Offset: 0x001C4D68
		public static SentOnlyToMeCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			return new SentOnlyToMeCondition(rule);
		}

		// Token: 0x06006A68 RID: 27240 RVA: 0x001C6B8C File Offset: 0x001C4D8C
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateOnlyToMeRestriction();
		}
	}
}

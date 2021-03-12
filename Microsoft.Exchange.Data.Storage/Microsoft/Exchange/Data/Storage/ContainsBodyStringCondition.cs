using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B9A RID: 2970
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ContainsBodyStringCondition : StringCondition
	{
		// Token: 0x06006A97 RID: 27287 RVA: 0x001C741C File Offset: 0x001C561C
		private ContainsBodyStringCondition(Rule rule, string[] text) : base(ConditionType.ContainsBodyStringCondition, rule, text)
		{
		}

		// Token: 0x06006A98 RID: 27288 RVA: 0x001C7428 File Offset: 0x001C5628
		public static ContainsBodyStringCondition Create(Rule rule, string[] text)
		{
			Condition.CheckParams(new object[]
			{
				rule,
				text
			});
			return new ContainsBodyStringCondition(rule, text);
		}

		// Token: 0x06006A99 RID: 27289 RVA: 0x001C7451 File Offset: 0x001C5651
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateORStringContentRestriction(base.Text, PropTag.Body, ContentFlags.SubString | ContentFlags.IgnoreCase);
		}
	}
}

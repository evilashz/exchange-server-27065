using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B9C RID: 2972
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ContainsHeaderStringCondition : StringCondition
	{
		// Token: 0x06006A9D RID: 27293 RVA: 0x001C74AA File Offset: 0x001C56AA
		private ContainsHeaderStringCondition(Rule rule, string[] text) : base(ConditionType.ContainsHeaderStringCondition, rule, text)
		{
		}

		// Token: 0x06006A9E RID: 27294 RVA: 0x001C74B8 File Offset: 0x001C56B8
		public static ContainsHeaderStringCondition Create(Rule rule, string[] text)
		{
			Condition.CheckParams(new object[]
			{
				rule,
				text
			});
			return new ContainsHeaderStringCondition(rule, text);
		}

		// Token: 0x06006A9F RID: 27295 RVA: 0x001C74E1 File Offset: 0x001C56E1
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateORStringContentRestriction(base.Text, PropTag.TransportMessageHeaders, ContentFlags.SubString | ContentFlags.IgnoreCase);
		}
	}
}

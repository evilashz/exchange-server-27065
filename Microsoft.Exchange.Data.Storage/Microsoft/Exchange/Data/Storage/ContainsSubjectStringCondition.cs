using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B99 RID: 2969
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ContainsSubjectStringCondition : StringCondition
	{
		// Token: 0x06006A94 RID: 27284 RVA: 0x001C73CF File Offset: 0x001C55CF
		private ContainsSubjectStringCondition(Rule rule, string[] text) : base(ConditionType.ContainsSubjectStringCondition, rule, text)
		{
		}

		// Token: 0x06006A95 RID: 27285 RVA: 0x001C73DC File Offset: 0x001C55DC
		public static ContainsSubjectStringCondition Create(Rule rule, string[] text)
		{
			Condition.CheckParams(new object[]
			{
				rule,
				text
			});
			return new ContainsSubjectStringCondition(rule, text);
		}

		// Token: 0x06006A96 RID: 27286 RVA: 0x001C7405 File Offset: 0x001C5605
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateORStringContentRestriction(base.Text, PropTag.Subject, ContentFlags.SubString | ContentFlags.IgnoreCase);
		}
	}
}

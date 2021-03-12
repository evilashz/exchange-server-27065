using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B9B RID: 2971
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ContainsSubjectOrBodyStringCondition : StringCondition
	{
		// Token: 0x06006A9A RID: 27290 RVA: 0x001C7468 File Offset: 0x001C5668
		private ContainsSubjectOrBodyStringCondition(Rule rule, string[] text) : base(ConditionType.ContainsSubjectOrBodyStringCondition, rule, text)
		{
		}

		// Token: 0x06006A9B RID: 27291 RVA: 0x001C7474 File Offset: 0x001C5674
		public static ContainsSubjectOrBodyStringCondition Create(Rule rule, string[] text)
		{
			Condition.CheckParams(new object[]
			{
				rule,
				text
			});
			return new ContainsSubjectOrBodyStringCondition(rule, text);
		}

		// Token: 0x06006A9C RID: 27292 RVA: 0x001C749D File Offset: 0x001C569D
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateSubjectOrBodyRestriction(base.Text);
		}
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B90 RID: 2960
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MarkedAsOofCondition : FormsCondition
	{
		// Token: 0x06006A7C RID: 27260 RVA: 0x001C6FE3 File Offset: 0x001C51E3
		private MarkedAsOofCondition(Rule rule, string[] text) : base(ConditionType.MarkedAsOofCondition, rule, text)
		{
		}

		// Token: 0x06006A7D RID: 27261 RVA: 0x001C6FF0 File Offset: 0x001C51F0
		public static MarkedAsOofCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			return new MarkedAsOofCondition(rule, new string[]
			{
				"IPM.Note.Rules.OofTemplate.Microsoft"
			});
		}
	}
}

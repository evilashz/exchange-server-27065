using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B8E RID: 2958
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class StringCondition : Condition
	{
		// Token: 0x06006A75 RID: 27253 RVA: 0x001C6C8A File Offset: 0x001C4E8A
		protected StringCondition(ConditionType conditionType, Rule rule, string[] text) : base(conditionType, rule)
		{
			this.text = text;
		}

		// Token: 0x17001D0E RID: 7438
		// (get) Token: 0x06006A76 RID: 27254 RVA: 0x001C6C9B File Offset: 0x001C4E9B
		public string[] Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x04003CDE RID: 15582
		private readonly string[] text;
	}
}

using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B91 RID: 2961
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class HasAttachmentCondition : Condition
	{
		// Token: 0x06006A7E RID: 27262 RVA: 0x001C7024 File Offset: 0x001C5224
		private HasAttachmentCondition(Rule rule) : base(ConditionType.HasAttachmentCondition, rule)
		{
		}

		// Token: 0x06006A7F RID: 27263 RVA: 0x001C7030 File Offset: 0x001C5230
		public static HasAttachmentCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			return new HasAttachmentCondition(rule);
		}

		// Token: 0x06006A80 RID: 27264 RVA: 0x001C7054 File Offset: 0x001C5254
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateHasAttachmentRestriction();
		}
	}
}

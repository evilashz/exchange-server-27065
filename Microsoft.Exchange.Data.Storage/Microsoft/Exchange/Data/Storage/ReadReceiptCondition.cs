using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BAB RID: 2987
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReadReceiptCondition : Condition
	{
		// Token: 0x06006AD5 RID: 27349 RVA: 0x001C7DA5 File Offset: 0x001C5FA5
		private ReadReceiptCondition(Rule rule) : base(ConditionType.ReadReceiptCondition, rule)
		{
		}

		// Token: 0x06006AD6 RID: 27350 RVA: 0x001C7DB0 File Offset: 0x001C5FB0
		public static ReadReceiptCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			return new ReadReceiptCondition(rule);
		}

		// Token: 0x06006AD7 RID: 27351 RVA: 0x001C7DD4 File Offset: 0x001C5FD4
		internal override Restriction BuildRestriction()
		{
			PropTag tag = base.Rule.PropertyDefinitionToPropTagFromCache(InternalSchema.IsReadReceipt);
			return new Restriction.PropertyRestriction(Restriction.RelOp.Equal, tag, true);
		}
	}
}

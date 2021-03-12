using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BA9 RID: 2985
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NdrCondition : Condition
	{
		// Token: 0x06006ACF RID: 27343 RVA: 0x001C7D30 File Offset: 0x001C5F30
		private NdrCondition(Rule rule) : base(ConditionType.NdrCondition, rule)
		{
		}

		// Token: 0x06006AD0 RID: 27344 RVA: 0x001C7D3C File Offset: 0x001C5F3C
		public static NdrCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			return new NdrCondition(rule);
		}

		// Token: 0x06006AD1 RID: 27345 RVA: 0x001C7D60 File Offset: 0x001C5F60
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateIsNdrRestrictions();
		}
	}
}

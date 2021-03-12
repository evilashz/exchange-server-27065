using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BAA RID: 2986
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AutomaticForwardCondition : Condition
	{
		// Token: 0x06006AD2 RID: 27346 RVA: 0x001C7D67 File Offset: 0x001C5F67
		private AutomaticForwardCondition(Rule rule) : base(ConditionType.AutomaticForwardCondition, rule)
		{
		}

		// Token: 0x06006AD3 RID: 27347 RVA: 0x001C7D74 File Offset: 0x001C5F74
		public static AutomaticForwardCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			return new AutomaticForwardCondition(rule);
		}

		// Token: 0x06006AD4 RID: 27348 RVA: 0x001C7D98 File Offset: 0x001C5F98
		internal override Restriction BuildRestriction()
		{
			return Condition.CreatePropertyRestriction<bool>(PropTag.AutoForwarded, true);
		}
	}
}

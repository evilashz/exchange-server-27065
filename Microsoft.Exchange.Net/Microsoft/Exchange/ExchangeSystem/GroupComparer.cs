using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000072 RID: 114
	internal sealed class GroupComparer : IComparer<ExTimeZoneRuleGroup>
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x00010B34 File Offset: 0x0000ED34
		public int Compare(ExTimeZoneRuleGroup g1, ExTimeZoneRuleGroup g2)
		{
			long num = (g1.EndTransition != null) ? g1.EndTransition.Value.Ticks : DateTime.MaxValue.Ticks;
			long num2 = (g2.EndTransition != null) ? g2.EndTransition.Value.Ticks : DateTime.MaxValue.Ticks;
			return Math.Sign(num - num2);
		}

		// Token: 0x040001F1 RID: 497
		public static GroupComparer Instance = new GroupComparer();
	}
}

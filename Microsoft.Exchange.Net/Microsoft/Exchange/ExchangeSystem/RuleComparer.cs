using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000070 RID: 112
	internal sealed class RuleComparer : IComparer<ExTimeZoneRule>
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x0001075A File Offset: 0x0000E95A
		private RuleComparer()
		{
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00010764 File Offset: 0x0000E964
		public int Compare(ExTimeZoneRule r1, ExTimeZoneRule r2)
		{
			if (r1 == null && r2 == null)
			{
				return 0;
			}
			if (r1 == null && r2 != null)
			{
				return -1;
			}
			if (r1 != null && r2 == null)
			{
				return 1;
			}
			if (r1.ObservanceEnd == null && r2.ObservanceEnd == null)
			{
				return 0;
			}
			if (r1.ObservanceEnd == null && r2.ObservanceEnd != null)
			{
				return -1;
			}
			if (r1.ObservanceEnd != null && r2.ObservanceEnd == null)
			{
				return 1;
			}
			return r1.ObservanceEnd.CompareTo(r2.ObservanceEnd);
		}

		// Token: 0x040001EA RID: 490
		public static RuleComparer Instance = new RuleComparer();
	}
}

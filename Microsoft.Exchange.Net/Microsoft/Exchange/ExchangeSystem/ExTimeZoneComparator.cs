using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000069 RID: 105
	internal class ExTimeZoneComparator : IComparer<ExTimeZone>
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x0000F424 File Offset: 0x0000D624
		public int Compare(ExTimeZone t1, ExTimeZone t2)
		{
			int num = t1.TimeZoneInformation.StandardBias.CompareTo(t2.TimeZoneInformation.StandardBias);
			if (num == 0)
			{
				return t1.LocalizableDisplayName.ToString().CompareTo(t2.LocalizableDisplayName.ToString());
			}
			return num;
		}
	}
}

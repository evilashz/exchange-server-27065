using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x02000684 RID: 1668
	internal class ExpiringValueComparer : IComparer<IExpiringValue>
	{
		// Token: 0x06001E37 RID: 7735 RVA: 0x00037811 File Offset: 0x00035A11
		public int Compare(IExpiringValue x, IExpiringValue y)
		{
			if (x != null)
			{
				return DateTime.Compare(x.ExpirationTime, y.ExpirationTime);
			}
			if (y == null)
			{
				return 0;
			}
			return -1;
		}
	}
}

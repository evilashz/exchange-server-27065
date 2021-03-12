using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000216 RID: 534
	[Serializable]
	internal class DagSubnetIdComparer : IComparer<DatabaseAvailabilityGroupSubnetId>
	{
		// Token: 0x060012B5 RID: 4789 RVA: 0x0003980E File Offset: 0x00037A0E
		private DagSubnetIdComparer()
		{
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00039818 File Offset: 0x00037A18
		public int Compare(DatabaseAvailabilityGroupSubnetId subnet1, DatabaseAvailabilityGroupSubnetId subnet2)
		{
			if (subnet1 == null || subnet2 == null)
			{
				if (subnet1 != null)
				{
					return 1;
				}
				if (subnet2 == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				IPRange iprange = subnet1.IPRange;
				IPRange iprange2 = subnet2.IPRange;
				if (iprange == iprange2)
				{
					return 0;
				}
				if (iprange.Contains(iprange2.LowerBound))
				{
					return 0;
				}
				if (iprange2.Contains(iprange.LowerBound))
				{
					return 0;
				}
				return iprange.CompareTo(iprange2);
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x00039877 File Offset: 0x00037A77
		internal static DagSubnetIdComparer Comparer
		{
			get
			{
				return DagSubnetIdComparer.s_comparer;
			}
		}

		// Token: 0x04000B1C RID: 2844
		private static DagSubnetIdComparer s_comparer = new DagSubnetIdComparer();
	}
}

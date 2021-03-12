using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000EE RID: 238
	public class PropertyComparerByTag : IComparer<Property>
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0002C185 File Offset: 0x0002A385
		public static PropertyComparerByTag Comparer
		{
			get
			{
				if (PropertyComparerByTag.comparer == null)
				{
					PropertyComparerByTag.comparer = new PropertyComparerByTag();
				}
				return PropertyComparerByTag.comparer;
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0002C1A0 File Offset: 0x0002A3A0
		public int Compare(Property x, Property y)
		{
			return x.Tag.CompareTo(y.Tag);
		}

		// Token: 0x04000551 RID: 1361
		private static PropertyComparerByTag comparer;
	}
}

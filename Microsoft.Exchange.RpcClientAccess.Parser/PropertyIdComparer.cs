using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001F5 RID: 501
	internal class PropertyIdComparer : IComparer<PropertyId>, IEqualityComparer<PropertyId>
	{
		// Token: 0x06000AAD RID: 2733 RVA: 0x00020AC9 File Offset: 0x0001ECC9
		public int Compare(PropertyId x, PropertyId y)
		{
			return (int)(y - x);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00020ACF File Offset: 0x0001ECCF
		public bool Equals(PropertyId x, PropertyId y)
		{
			return x == y;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00020AD5 File Offset: 0x0001ECD5
		public int GetHashCode(PropertyId tag)
		{
			return (int)tag;
		}

		// Token: 0x0400051C RID: 1308
		public static readonly PropertyIdComparer Instance = new PropertyIdComparer();
	}
}

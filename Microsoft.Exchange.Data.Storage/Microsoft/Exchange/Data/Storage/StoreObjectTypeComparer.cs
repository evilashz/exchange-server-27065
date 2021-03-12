using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002A2 RID: 674
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class StoreObjectTypeComparer : IComparer<StoreObjectType>, IEqualityComparer<StoreObjectType>
	{
		// Token: 0x06001C0B RID: 7179 RVA: 0x000811FF File Offset: 0x0007F3FF
		public int Compare(StoreObjectType x, StoreObjectType y)
		{
			return y - x;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00081204 File Offset: 0x0007F404
		public bool Equals(StoreObjectType x, StoreObjectType y)
		{
			return x == y;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x0008120A File Offset: 0x0007F40A
		public int GetHashCode(StoreObjectType tag)
		{
			return (int)tag;
		}
	}
}

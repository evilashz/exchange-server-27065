using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002E3 RID: 739
	internal class RopIdComparer : IComparer<RopId>, IEqualityComparer<RopId>
	{
		// Token: 0x06001124 RID: 4388 RVA: 0x0002FA15 File Offset: 0x0002DC15
		public int Compare(RopId x, RopId y)
		{
			return (int)(y - x);
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0002FA1B File Offset: 0x0002DC1B
		public bool Equals(RopId x, RopId y)
		{
			return x == y;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0002FA21 File Offset: 0x0002DC21
		public int GetHashCode(RopId tag)
		{
			return (int)tag;
		}
	}
}

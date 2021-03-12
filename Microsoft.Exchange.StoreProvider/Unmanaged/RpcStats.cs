using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002AF RID: 687
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct RpcStats
	{
		// Token: 0x040011DE RID: 4574
		internal uint rpcCount;

		// Token: 0x040011DF RID: 4575
		internal uint emptyRpcCount;

		// Token: 0x040011E0 RID: 4576
		internal uint releaseOnlyRpcCount;

		// Token: 0x040011E1 RID: 4577
		internal uint messagesCreated;
	}
}

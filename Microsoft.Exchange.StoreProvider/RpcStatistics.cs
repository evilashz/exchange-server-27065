using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200022B RID: 555
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct RpcStatistics
	{
		// Token: 0x060009BE RID: 2494 RVA: 0x000304D4 File Offset: 0x0002E6D4
		public static RpcStatistics CreateFromNative(RpcStats nativeStats)
		{
			return new RpcStatistics
			{
				rpcCount = nativeStats.rpcCount,
				emptyRpcCount = nativeStats.emptyRpcCount,
				releaseOnlyRpcCount = nativeStats.releaseOnlyRpcCount,
				messagesCreated = nativeStats.messagesCreated
			};
		}

		// Token: 0x04000FBD RID: 4029
		public uint rpcCount;

		// Token: 0x04000FBE RID: 4030
		public uint emptyRpcCount;

		// Token: 0x04000FBF RID: 4031
		public uint releaseOnlyRpcCount;

		// Token: 0x04000FC0 RID: 4032
		public uint messagesCreated;
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002AB RID: 683
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct PerformanceContext
	{
		// Token: 0x04001185 RID: 4485
		public uint rpcCount;

		// Token: 0x04001186 RID: 4486
		public ulong rpcLatency;

		// Token: 0x04001187 RID: 4487
		public uint currentActiveConnections;

		// Token: 0x04001188 RID: 4488
		public uint currentConnectionPoolSize;

		// Token: 0x04001189 RID: 4489
		public uint failedConnections;

		// Token: 0x0400118A RID: 4490
		private IntPtr prev;

		// Token: 0x0400118B RID: 4491
		private IntPtr next;
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000F2 RID: 242
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetTransportResultFactory : StandardResultFactory
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x0000F470 File Offset: 0x0000D670
		internal SetTransportResultFactory() : base(RopId.SetTransport)
		{
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000F47A File Offset: 0x0000D67A
		public RopResult CreateSuccessfulResult(StoreId transportQueueFolderId)
		{
			return new SuccessfulSetTransportResult(transportQueueFolderId);
		}
	}
}

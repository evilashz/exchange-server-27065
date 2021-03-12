using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000CE RID: 206
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferGetIncrementalStateResultFactory : StandardResultFactory
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x0000EF1D File Offset: 0x0000D11D
		internal FastTransferGetIncrementalStateResultFactory() : base(RopId.FastTransferGetIncrementalState)
		{
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000EF2A File Offset: 0x0000D12A
		public RopResult CreateSuccessfulResult(IServerObject fastTransferDownloadObject)
		{
			return new SuccessfulFastTransferGetIncrementalStateResult(fastTransferDownloadObject);
		}
	}
}

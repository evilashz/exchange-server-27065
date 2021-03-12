using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000D3 RID: 211
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferSourceCopyToResultFactory : StandardResultFactory
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x0000EF7A File Offset: 0x0000D17A
		internal FastTransferSourceCopyToResultFactory() : base(RopId.FastTransferSourceCopyTo)
		{
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000EF84 File Offset: 0x0000D184
		public SuccessfulFastTransferSourceCopyToResult CreateSuccessfulResult(IServerObject serverObject)
		{
			return new SuccessfulFastTransferSourceCopyToResult(serverObject);
		}
	}
}

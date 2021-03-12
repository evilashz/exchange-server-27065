using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000D1 RID: 209
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferSourceCopyMessagesResultFactory : StandardResultFactory
	{
		// Token: 0x06000494 RID: 1172 RVA: 0x0000EF56 File Offset: 0x0000D156
		internal FastTransferSourceCopyMessagesResultFactory() : base(RopId.FastTransferSourceCopyMessages)
		{
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000EF60 File Offset: 0x0000D160
		public SuccessfulFastTransferSourceCopyMessagesResult CreateSuccessfulResult(IServerObject serverObject)
		{
			return new SuccessfulFastTransferSourceCopyMessagesResult(serverObject);
		}
	}
}

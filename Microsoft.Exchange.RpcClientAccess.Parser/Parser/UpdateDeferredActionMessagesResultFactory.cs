using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000147 RID: 327
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UpdateDeferredActionMessagesResultFactory : StandardResultFactory
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x00011108 File Offset: 0x0000F308
		internal UpdateDeferredActionMessagesResultFactory() : base(RopId.UpdateDeferredActionMessages)
		{
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00011112 File Offset: 0x0000F312
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.UpdateDeferredActionMessages, ErrorCode.None);
		}
	}
}

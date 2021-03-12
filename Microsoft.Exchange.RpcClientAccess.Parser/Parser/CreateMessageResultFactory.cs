using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000C0 RID: 192
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CreateMessageResultFactory : StandardResultFactory
	{
		// Token: 0x0600045D RID: 1117 RVA: 0x0000EC04 File Offset: 0x0000CE04
		internal CreateMessageResultFactory() : base(RopId.CreateMessage)
		{
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000EC0D File Offset: 0x0000CE0D
		public RopResult CreateSuccessfulResult(IServerObject serverObject, StoreId? messageId)
		{
			return new SuccessfulCreateMessageResult(serverObject, messageId);
		}
	}
}

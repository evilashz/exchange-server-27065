using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000BF RID: 191
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CreateMessageExtendedResultFactory : StandardResultFactory
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x0000EBEE File Offset: 0x0000CDEE
		internal CreateMessageExtendedResultFactory() : base(RopId.CreateMessageExtended)
		{
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000EBFB File Offset: 0x0000CDFB
		public RopResult CreateSuccessfulResult(IServerObject serverObject, StoreId? messageId)
		{
			return new SuccessfulCreateMessageExtendedResult(serverObject, messageId);
		}
	}
}

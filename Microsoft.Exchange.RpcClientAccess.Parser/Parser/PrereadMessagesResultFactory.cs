using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200010A RID: 266
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PrereadMessagesResultFactory : StandardResultFactory
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x0000FFE2 File Offset: 0x0000E1E2
		internal PrereadMessagesResultFactory() : base(RopId.PrereadMessages)
		{
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000FFEF File Offset: 0x0000E1EF
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.PrereadMessages, ErrorCode.None);
		}
	}
}

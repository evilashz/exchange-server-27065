using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000FA RID: 250
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LongTermIdFromIdResultFactory : StandardResultFactory
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x0000F70B File Offset: 0x0000D90B
		internal LongTermIdFromIdResultFactory() : base(RopId.LongTermIdFromId)
		{
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000F715 File Offset: 0x0000D915
		public RopResult CreateSuccessfulResult(StoreLongTermId longTermId)
		{
			return new SuccessfulLongTermIdFromIdResult(longTermId);
		}
	}
}

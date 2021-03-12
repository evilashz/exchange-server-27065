using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000F5 RID: 245
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class IdFromLongTermIdResultFactory : StandardResultFactory
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0000F644 File Offset: 0x0000D844
		internal IdFromLongTermIdResultFactory() : base(RopId.IdFromLongTermId)
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000F64E File Offset: 0x0000D84E
		public RopResult CreateSuccessfulResult(StoreId storeId)
		{
			return new SuccessfulIdFromLongTermIdResult(storeId);
		}
	}
}

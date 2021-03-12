using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000F0 RID: 240
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetStoreStateResultFactory : StandardResultFactory
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x0000F44C File Offset: 0x0000D64C
		internal GetStoreStateResultFactory() : base(RopId.GetStoreState)
		{
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000F456 File Offset: 0x0000D656
		public RopResult CreateSuccessfulResult(StoreState storeState)
		{
			return new SuccessfulGetStoreStateResult(storeState);
		}
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000DF RID: 223
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetLocalReplicationIdsResultFactory : StandardResultFactory
	{
		// Token: 0x060004C4 RID: 1220 RVA: 0x0000F1DD File Offset: 0x0000D3DD
		internal GetLocalReplicationIdsResultFactory() : base(RopId.GetLocalReplicationIds)
		{
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000F1E7 File Offset: 0x0000D3E7
		public RopResult CreateSuccessfulResult(StoreLongTermId localReplicationId)
		{
			return new SuccessfulGetLocalReplicationIdsResult(localReplicationId);
		}
	}
}

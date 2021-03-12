using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000CF RID: 207
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class IncrementalConfigResultFactory : StandardResultFactory
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x0000EF32 File Offset: 0x0000D132
		internal IncrementalConfigResultFactory() : base(RopId.IncrementalConfig)
		{
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000EF3C File Offset: 0x0000D13C
		public RopResult CreateSuccessfulResult(IServerObject synchronizer)
		{
			return new SuccessfulIncrementalConfigResult(synchronizer);
		}
	}
}

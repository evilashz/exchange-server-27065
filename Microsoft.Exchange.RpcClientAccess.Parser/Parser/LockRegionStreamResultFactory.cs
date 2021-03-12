using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000F6 RID: 246
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LockRegionStreamResultFactory : StandardResultFactory
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x0000F656 File Offset: 0x0000D856
		internal LockRegionStreamResultFactory() : base(RopId.LockRegionStream)
		{
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000F660 File Offset: 0x0000D860
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.LockRegionStream, ErrorCode.None);
		}
	}
}

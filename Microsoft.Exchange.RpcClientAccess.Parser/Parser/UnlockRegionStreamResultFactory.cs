using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000146 RID: 326
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UnlockRegionStreamResultFactory : StandardResultFactory
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x000110F4 File Offset: 0x0000F2F4
		internal UnlockRegionStreamResultFactory() : base(RopId.UnlockRegionStream)
		{
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000110FE File Offset: 0x0000F2FE
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.UnlockRegionStream, ErrorCode.None);
		}
	}
}

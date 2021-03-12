using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000F1 RID: 241
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetStreamSizeResultFactory : StandardResultFactory
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x0000F45E File Offset: 0x0000D65E
		internal GetStreamSizeResultFactory() : base(RopId.GetStreamSize)
		{
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000F468 File Offset: 0x0000D668
		public RopResult CreateSuccessfulResult(uint streamSize)
		{
			return new SuccessfulGetStreamSizeResult(streamSize);
		}
	}
}

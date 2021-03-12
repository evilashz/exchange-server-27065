using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200011E RID: 286
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SeekRowBookmarkResultFactory : StandardResultFactory
	{
		// Token: 0x060005B5 RID: 1461 RVA: 0x00010C63 File Offset: 0x0000EE63
		internal SeekRowBookmarkResultFactory() : base(RopId.SeekRowBookmark)
		{
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00010C6D File Offset: 0x0000EE6D
		public RopResult CreateSuccessfulResult(bool positionChanged, bool soughtLessThanRequested, int rowsSought)
		{
			return new SuccessfulSeekRowBookmarkResult(positionChanged, soughtLessThanRequested, rowsSought);
		}
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000D7 RID: 215
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FreeBookmarkResultFactory : StandardResultFactory
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x0000F0BC File Offset: 0x0000D2BC
		internal FreeBookmarkResultFactory() : base(RopId.FreeBookmark)
		{
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000F0C9 File Offset: 0x0000D2C9
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.FreeBookmark, ErrorCode.None);
		}
	}
}

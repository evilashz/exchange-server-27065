using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000BD RID: 189
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CreateBookmarkResultFactory : StandardResultFactory
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x0000EBC4 File Offset: 0x0000CDC4
		internal CreateBookmarkResultFactory() : base(RopId.CreateBookmark)
		{
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000EBCE File Offset: 0x0000CDCE
		public RopResult CreateSuccessfulResult(byte[] bookmark)
		{
			return new SuccessfulCreateBookmarkResult(bookmark);
		}
	}
}

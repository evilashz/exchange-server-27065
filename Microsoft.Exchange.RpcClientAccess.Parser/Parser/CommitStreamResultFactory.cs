using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000B5 RID: 181
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CommitStreamResultFactory : StandardResultFactory
	{
		// Token: 0x0600042D RID: 1069 RVA: 0x0000E7EF File Offset: 0x0000C9EF
		internal CommitStreamResultFactory() : base(RopId.CommitStream)
		{
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000E7F9 File Offset: 0x0000C9F9
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.CommitStream, ErrorCode.None);
		}
	}
}

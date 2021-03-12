using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000B1 RID: 177
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AbortSubmitResultFactory : StandardResultFactory
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x0000E7A5 File Offset: 0x0000C9A5
		internal AbortSubmitResultFactory() : base(RopId.AbortSubmit)
		{
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000E7AF File Offset: 0x0000C9AF
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.AbortSubmit, ErrorCode.None);
		}
	}
}

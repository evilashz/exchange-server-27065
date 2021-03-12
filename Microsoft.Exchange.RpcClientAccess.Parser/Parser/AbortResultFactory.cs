using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000B0 RID: 176
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AbortResultFactory : StandardResultFactory
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x0000E793 File Offset: 0x0000C993
		internal AbortResultFactory() : base(RopId.Abort)
		{
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000E79D File Offset: 0x0000C99D
		public RopResult CreateSuccessfulResult(TableStatus status)
		{
			return new SuccessfulAbortResult(status);
		}
	}
}

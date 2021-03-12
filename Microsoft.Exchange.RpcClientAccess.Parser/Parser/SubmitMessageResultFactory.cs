using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000133 RID: 307
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SubmitMessageResultFactory : StandardResultFactory
	{
		// Token: 0x060005E7 RID: 1511 RVA: 0x00010F01 File Offset: 0x0000F101
		internal SubmitMessageResultFactory() : base(RopId.SubmitMessage)
		{
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00010F0B File Offset: 0x0000F10B
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.SubmitMessage, ErrorCode.None);
		}
	}
}

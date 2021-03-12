using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200012E RID: 302
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetSizeStreamResultFactory : StandardResultFactory
	{
		// Token: 0x060005DD RID: 1501 RVA: 0x00010E9B File Offset: 0x0000F09B
		internal SetSizeStreamResultFactory() : base(RopId.SetSizeStream)
		{
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00010EA5 File Offset: 0x0000F0A5
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.SetSizeStream, ErrorCode.None);
		}
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000125 RID: 293
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetMessageFlagsResultFactory : StandardResultFactory
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x00010CEE File Offset: 0x0000EEEE
		internal SetMessageFlagsResultFactory() : base(RopId.SetMessageFlags)
		{
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00010CFB File Offset: 0x0000EEFB
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.SetMessageFlags, ErrorCode.None);
		}
	}
}
